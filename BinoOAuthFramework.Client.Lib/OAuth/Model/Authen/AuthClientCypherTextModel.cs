using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class AuthClientCypherTextModel
    {
        /// <summary>
        /// 客戶端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客戶端識別Id
        /// </summary>
        public ClientTempIdentityModel ClientTempId { get; set; }

        /// <summary>
        /// 資源保護者Id
        /// </summary>
        public string ProtectedId { get; set; }

        /// <summary>
        /// 失效時間
        /// </summary>
        public long ExpiredTime { get; set; }

        /// <summary>
        /// 授權次數
        /// </summary>
        public int AuthZTimes { get; set; }

        /// <summary>
        /// 任意值
        /// </summary>
        public string RandomValue { get; set; }

        /// <summary>
        ///  Client 與 Resource Protector 的金鑰
        /// </summary>
        public SymCryptoModel ClientProtectedCryptoModel { get; set; }

        /// <summary>
        /// 可允許Client進入的url列表
        /// </summary>
        public List<string> ValidUrlList { get; set; }
    }
}
