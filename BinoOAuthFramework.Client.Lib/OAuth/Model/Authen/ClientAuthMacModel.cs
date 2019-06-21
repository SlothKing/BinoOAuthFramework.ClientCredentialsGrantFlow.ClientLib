using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    /// <summary>
    /// 客戶端初始化驗證資料模型
    /// </summary>
    public class ClientAuthMacModel
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
        /// Auth 與 Client 間的 Key, Iv
        /// </summary>
        public SymCryptoModel AuthClientCryptoModel { get; set; }

        /// <summary>
        /// 加鹽字串
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 失效時間
        /// </summary>
        public long ExpiredTime { get; set; }
    }
}
