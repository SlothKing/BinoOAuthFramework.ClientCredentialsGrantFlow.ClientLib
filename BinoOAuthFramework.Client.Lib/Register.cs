using Binodata.Crypto.Lib.UseCases;
using Binodata.Crypto.Lib.Utility;
using BinoOAuthFramework.Client.Lib.Entities;
using BinoOAuthFramework.Client.Lib.OAuth.Model.Authen;
using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using Newtonsoft.Json;
using System;
using System.Linq;
using Binodata.Crypto.Lib;
using BinoOAuthFramework.Client.Lib.Http;
using Binodata.Utility.Component.Standard.AdapterModel;
using BinoOAuthFramework.Client.Lib.Error;
using Jose;
using System.Text;
using System.Collections.Generic;

namespace BinoOAuthFramework.Client.Lib
{
    public class Register
    {
        private IAESCrypter aesCrypter;
        private ClientResource clientResource;
        private readonly int addMinuteExpiredTime;
        private readonly string authServerAuthenApiUrl;
        private readonly string protectedAuthenApiUrl;

        public Register()
        {

        }

        /// <summary>
        /// 用Client Key 、IV、Protected Server(s)相關資料 去AuthServer驗證 且取回對應的Token
        /// </summary>
        public void Authenticate()
        {
            long expiredTime = GetExpiredUtc0UnixTime();

            //客戶端初始化驗證資料
            ClientAuthMacModel macModel = new ClientAuthMacModel()
            {
                AuthClientCryptoModel = new SymCryptoModel()
                {
                    IV = clientResource.ClientIV,
                    Key = clientResource.ClientKey,
                },
                ClientId = clientResource.ClientId,
                ExpiredTime = expiredTime,
                Salt = "1",
                ProtectedIdIdList = clientResource.ProtectedServers.Select(x => x.ServerId).ToList()
            };

            string clientModelStr = JsonConvert.SerializeObject(macModel);
            string macValue = MD5Hasher.Hash(clientModelStr);

            //組出 
            ClientAuthCypherTextModel cypherTextModel = new ClientAuthCypherTextModel()
            {
                ClientId = clientResource.ClientId,
                ProtectedIdIdList = clientResource.ProtectedServers.Select(x => x.ServerId).ToList(),
                ClientMac = macValue,
                ExpiredTime = expiredTime,
                MacHashAlg = "MD5",
            };

            string cypherTextModelStr = JsonConvert.SerializeObject(cypherTextModel);
            aesCrypter.SetKey(clientResource.ClientKey);
            aesCrypter.SetIV(clientResource.ClientIV);
            string encryptCypherText = aesCrypter.Encrypt(cypherTextModelStr);

            //請求 Auth Server 驗證
            AuthClientReqModel authClientReqModel = new AuthClientReqModel()
            {
                ClientId = clientResource.ClientId,
                CypherText = encryptCypherText,
            };
            string reqStr = JsonConvert.SerializeObject(authClientReqModel);
            ApiResult<AuthClientRespModel> respones = AuthenHttpHandler.SendRequest<AuthClientRespModel>(authServerAuthenApiUrl, reqStr);
            
        }

        /// <summary>
        /// 確認 Auth Server 驗證回應值，且請求資源保護者驗證
        /// </summary>
        /// <param name="cypherText"></param>
        /// <param name="protectedId"></param>
        /// <returns></returns>
        public AuthorizeValueModel CheckAuthServerResp(string cypherText, string protectedId)
        {
            aesCrypter.SetKey(clientResource.ClientKey);
            aesCrypter.SetIV(clientResource.ClientIV);
            string decryptResult = aesCrypter.Decrypt(cypherText);
            AuthClientCypherTextModel authClientCypherTextModel = JsonConvert.DeserializeObject<AuthClientCypherTextModel>(decryptResult);
            //check
            if (authClientCypherTextModel.ClientId != clientResource.ClientId)
            {
                throw new ClientNotEqualException("ClientId is not equal.");
            }
            if (authClientCypherTextModel.ProtectedId != protectedId)
            {
                throw new ProtectedServerNotEqualException("ProtectedId is not equal. ");
            }
            if (UnixTimeGenerator.GetUtcNowUnixTime() > authClientCypherTextModel.ExpiredTime)
            {
                throw new ClientAuthorizeTokenExpiredException("Client authorized token has expired, please re-authenticate and get new token");
            }

            //請求資源保護者驗證
            long expiredTime = GetExpiredUtc0UnixTime();
            string hashValue = HashMultipleTimes(authClientCypherTextModel.RandomValue, authClientCypherTextModel.AuthZTimes);
            ClientProtectedMacModel macModel = new ClientProtectedMacModel()
            {
                Salt = "2",
                ClientTempId = authClientCypherTextModel.ClientTempId,
                ProtectedId = authClientCypherTextModel.ProtectedId,
                AuthZTimes = authClientCypherTextModel.AuthZTimes,
                HashValue = hashValue,
                ExpiredTime = expiredTime,
                ClientProtectedCryptoModel = authClientCypherTextModel.ClientProtectedCryptoModel,
            };

            string clientResrcMacStr = JsonConvert.SerializeObject(macModel);
            string macValue = MD5Hasher.Hash(clientResrcMacStr);
            CheckClientReqModel reqModel = new CheckClientReqModel()
            {
                ClientProtectedMac = macValue,
                ExpiredTime = expiredTime,
                ClientTempId = authClientCypherTextModel.ClientTempId
            };
            string reqStr = JsonConvert.SerializeObject(reqModel);
            string resrcCheckClientReqUrl = string.Format("{0}{1}", protectedAuthenApiUrl, "CheckClientRequest");
            ApiResult<bool> resrcResp = AuthenHttpHandler.SendRequest<bool>(resrcCheckClientReqUrl, reqStr);
            //Protected Server 驗證結果
            if (!resrcResp.Value)
            {
                throw new Exception();
            }
            else
            {
                AuthorizeValueModel authorizeModel = new AuthorizeValueModel()
                {
                    AuthZTimes = authClientCypherTextModel.AuthZTimes,
                    ClientProtectedCryptoModel = authClientCypherTextModel.ClientProtectedCryptoModel,
                    ClientTempId = authClientCypherTextModel.ClientTempId,
                    CurrentTimes = 1,
                    RandomValue = authClientCypherTextModel.RandomValue,
                    ProtectedId = authClientCypherTextModel.ProtectedId,
                    ValidUrlList = authClientCypherTextModel.ValidUrlList,
                };
                return authorizeModel;
            }


        }


        public AuthorizeValueModel Authorize(string authZUrl, AuthorizeValueModel authorizeModel, AuthZReqModel reqAuthZValue)
        {
            //Hash(r)^(n-i)
            int minusValue = authorizeModel.AuthZTimes - authorizeModel.CurrentTimes;
            string hashNMinusI =  HashMultipleTimes(authorizeModel.RandomValue, minusValue);

            //初始化請求授權
            string hashNMinusIAddOne = MD5Hasher.Hash(hashNMinusI);

            string authZKey = GetResrcClientKeyAuthzTimesValue(authorizeModel.ClientProtectedCryptoModel.Key, authorizeModel.ClientTempId, authorizeModel.CurrentTimes);
            string authZIv = GetResrcClientKeyAuthzTimesValue(authorizeModel.ClientProtectedCryptoModel.IV, authorizeModel.ClientTempId, authorizeModel.CurrentTimes);
            
            AuthorizeCypherTextModel cypherTextModel = new AuthorizeCypherTextModel
            {
                ClientTempId = authorizeModel.ClientTempId,
                ExpiredTime = UnixTimeGenerator.GetExpiredUtc0UnixTime(addMinuteExpiredTime),
                HashValue = hashNMinusI,
                ProtectedId = authorizeModel.ProtectedId
            };
            string authorizeCypherTextStr = JsonConvert.SerializeObject(cypherTextModel);
            aesCrypter.SetKey(authZKey);
            aesCrypter.SetIV(authZIv.Substring(0, 16));
            string currentTimesCypherText = aesCrypter.Encrypt(authorizeCypherTextStr);

            ClientReqAuthZModel clientReqAuthZModel = new ClientReqAuthZModel
            {
                ClientTempId = authorizeModel.ClientTempId,
                CurrentTimes = authorizeModel.CurrentTimes,
                CurrentTimesCypherText = currentTimesCypherText,
                ProtectedId = authorizeModel.ProtectedId,
                ValidUrlList = authorizeModel.ValidUrlList
            };
            string clientReqAuthZStr = JsonConvert.SerializeObject(clientReqAuthZModel);

            //取得Token
            string token = JWTHasher.GetJWTValue(clientReqAuthZStr, authorizeModel.ClientProtectedCryptoModel.Key);

            //向資源保護者請求授權 undo
            string reqAuthZValueStr = JsonConvert.SerializeObject(reqAuthZValue);

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"ClientId",clientResource.ClientId },
                {"Token",token}
            };
            ApiResult<string> rescrAuthorizeRespOpt = AuthenHttpHandler.SendRequest<string>(authZUrl, reqAuthZValueStr, headers);
            if(rescrAuthorizeRespOpt.ResultCode != 0)
            {
                //undo
                throw new Exception();
            }
            
            string cypherTextPrimeStr = rescrAuthorizeRespOpt.CypherCheckValue;
            //CheckProtectedServerRespAuthZValue
            string decryptStr = aesCrypter.Decrypt(cypherTextPrimeStr);
            TimesCypherTextPrimeModel timesCypherTextPrimeModel = JsonConvert.DeserializeObject<TimesCypherTextPrimeModel>(decryptStr);

            bool checkAuthZValueResult = CheckProtectedServerRespAuthZValue(timesCypherTextPrimeModel);
            if(checkAuthZValueResult == false)
            {
                //undo
                throw new Exception();
            }
            authorizeModel.CurrentTimes = authorizeModel.CurrentTimes + 1;
            authorizeModel.ClientTempId.HashValue = hashNMinusI;

            return authorizeModel;
        }

        /// <summary>
        /// 檢核 資源保護者回傳值
        /// </summary>
        /// <param name="timesCypherTextPrimeModel"></param>
        /// <returns></returns>
        private bool CheckProtectedServerRespAuthZValue(TimesCypherTextPrimeModel timesCypherTextPrimeModel)
        {
            bool result = false;
            string hashNMinusIAddOne = timesCypherTextPrimeModel.ClientTempId.HashValue;
            string hashNMinusI = timesCypherTextPrimeModel.ClientTempIdPrime.HashValue;
            if (MD5Hasher.Hash(hashNMinusI) == hashNMinusIAddOne)
            {
                result = true;
            }
            return result;
        }

        private string GetResrcClientKeyAuthzTimesValue(string crypto, ClientTempIdentityModel clientTempIdModel, int currentTimes)
        {
            AuthorizeHashModel authorizeKeyHashModel = new AuthorizeHashModel()
            {
                ClientProtectedCryptoStr = crypto,
                ClientTempId = clientTempIdModel,
                CurrentTimes = currentTimes
            };
            string resrcClientKeyAuthZTimes = JsonConvert.SerializeObject(authorizeKeyHashModel);
            string hashValue = MD5Hasher.Hash(resrcClientKeyAuthZTimes);
            AuthorizeKeyModel authorizeKeyModel = new AuthorizeKeyModel
            {
                HashKeyTIDCTimesValue = resrcClientKeyAuthZTimes,
                HashValue = hashValue
            };
            string authorizeCryptoStr = JsonConvert.SerializeObject(authorizeKeyModel);
            string hashResult = MD5Hasher.Hash(authorizeCryptoStr);
            return hashResult;
        }

        /// <summary>
        /// 依照設定值計算後取得 ExpiredTime
        /// </summary>
        /// <returns></returns>
        public virtual long GetExpiredUtc0UnixTime()
        {
            return UnixTimeGenerator.GetExpiredUtc0UnixTime(addMinuteExpiredTime);
        }

        /// <summary>
        /// 多次Hash 
        /// </summary>
        /// <returns></returns>
        private string HashMultipleTimes(string hashValue, int authorizeTimes)
        {
            string value = hashValue; //value need to hash
            int times = 0;

            while (times < authorizeTimes)
            {
                value = MD5Hasher.Hash(value);
                times++;
            }

            return value;
        }
    }
}
