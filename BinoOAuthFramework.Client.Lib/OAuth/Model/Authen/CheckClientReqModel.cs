using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class CheckClientReqModel
    {
        /// <summary>
        /// 失效時間
        /// </summary>
        public long ExpiredTime { get; set; }

        /// <summary>
        /// Client 與 Protected Server 的MAC值
        /// </summary>
        public string ClientProtectedMac { get; set; }

        /// <summary>
        /// 客戶端識別Id
        /// </summary>
        public ClientTempIdentityModel ClientTempId { get; set; }
    }
}
