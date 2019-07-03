using Binodata.Crypto.Lib;
using BinoOAuthFramework.Client.Lib;
using BinoOAuthFramework.Client.Lib.Entities;
using BinoOAuthFramework.Client.Lib.OAuth.Model.Authen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ClientResource clientResource = new ClientResource()
            {
                ClientId = "6365724719934223450001",
                ClientKey = "A25AD6A46FD945C7647AD34A993E01AF",
                ClientIV = "5687EC92759818B5",
                ClientName = "Sample",
                ProtectedServers = new List<ClientToProtectedServerData>(),

            };
            RegisterInitialModel registerInitialModel = new RegisterInitialModel()
            {
                AddMinuteExpiredTime = 30,
                AuthServerAuthenApiUrl = "http://localhost:21383/api/RegisterService/Authen/",
                ProtectedAuthenApiUrl = "http://localhost:21383/api/RegisterService/CheckClientRequest",
                
            };
            Register register = new Register(clientResource,registerInitialModel,new LocalMachineAESCrypter());
            var apiResult = register.Authenticate();
            if (apiResult == false)
            {
                Console.WriteLine(apiResult.ResultMessage);
                //Auth Server 驗證失敗
                Environment.Exit(1);
            }
            List<string> cypherTextList = apiResult.Value.CypherTextList;
            List<AuthClientCypherTextModel> authClientCyphersTextList = new List<AuthClientCypherTextModel>();
            cypherTextList.ForEach(x => authClientCyphersTextList.Add(register.DecryptAuthServerResp(x)));

            //當需要去Protected Server溝通時 取出相對應的 AuthClientCypherTextModel
            AuthClientCypherTextModel authClient =  authClientCyphersTextList.Where(x => x.ProtectedId == "目標Protected Server Id").Single();
            //先去 Protected Server 取得驗證相關資料
            AuthorizeValueModel authorizeValueModel = register.SendCypherTextToProtectedResourceForVerify(authClient, "目標Protected Server Id");

            PostSampleData postSampleData = new PostSampleData()
            {
                Data = "Sample1",
                Data2 = "Sample2"
            };

            //取得 afterPostAuthorizeValueModel 後，更新 AuthorizeValueModel 供下次呼叫此 Protected Server 使用
            var afterPostAuthorizeValueModel = register.SendRequestAndAuthorizeByPost<PostSampleData>("目標Protected Server URL", authorizeValueModel, postSampleData);
            
        }


    }

    class PostSampleData
    {
        public string Data { get; set; }

        public string Data2 { get; set; }
    }
}
