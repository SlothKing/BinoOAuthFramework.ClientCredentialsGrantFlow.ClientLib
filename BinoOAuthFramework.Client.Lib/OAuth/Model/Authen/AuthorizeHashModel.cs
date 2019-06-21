using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class AuthorizeHashModel
    {
        /// <summary>
        ///  Auth Server 與 Resource Protector 的 Key 或 IV
        /// </summary>
        public string ClientProtectedCryptoStr { get; set; }

        /// <summary>
        /// 客戶端識別Id
        /// </summary>
        public ClientTempIdentityModel ClientTempId { get; set; }

        /// <summary>
        /// 目前授權計數
        /// </summary>
        public int CurrentTimes { get; set; }

    }
}
