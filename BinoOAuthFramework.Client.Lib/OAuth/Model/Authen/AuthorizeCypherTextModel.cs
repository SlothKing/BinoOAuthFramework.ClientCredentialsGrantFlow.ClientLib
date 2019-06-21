using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    /// <summary>
    /// CT 資料模型
    /// </summary>
    public class AuthorizeCypherTextModel
    {

        /// <summary>
        /// 客戶端識別Id
        /// </summary>
        public ClientTempIdentityModel ClientTempId { get; set; }

        /// <summary>
        /// 資源保護者Id
        /// </summary>
        public string ProtectedId { get; set; }


        /// <summary>
        /// Hash random value (n-i)
        /// </summary>
        public string HashValue { get; set; }


        /// <summary>
        /// 失效時間
        /// </summary>
        public long ExpiredTime { get; set; }
    }
}
