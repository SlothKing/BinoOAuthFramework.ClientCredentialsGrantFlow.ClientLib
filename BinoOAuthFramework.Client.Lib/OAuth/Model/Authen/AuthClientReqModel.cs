using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    internal class AuthClientReqModel
    {
        /// <summary>
        /// 客戶端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 傳送給 Auth Server 的加密值
        /// </summary>
        public string CypherText { get; set; }
    }
}
