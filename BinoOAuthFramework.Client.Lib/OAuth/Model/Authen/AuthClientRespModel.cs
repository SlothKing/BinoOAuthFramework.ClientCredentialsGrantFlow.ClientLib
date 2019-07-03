using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class AuthClientRespModel
    {
        /// <summary>
        /// 客戶端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 接收 Auth Server 的加密值
        /// </summary>
        public List<string> CypherTextList { get; set; }
    }
}
