using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class ClientAuthCypherTextModel
    {
        /// <summary>
        /// 客戶端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 發出請求的資源保護者ID
        /// </summary>
        public List<string> ProtectedIdIdList { get; set; }

        /// <summary>
        /// 失效時間
        /// </summary>
        public long ExpiredTime { get; set; }

        /// <summary>
        /// 客戶端Mac值
        /// </summary>
        public string ClientMac { get; set; }

        /// <summary>
        /// 加密方式
        /// </summary>
        public string MacHashAlg { get; set; }

    }
}
