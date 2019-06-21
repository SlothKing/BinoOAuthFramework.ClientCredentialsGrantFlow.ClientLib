using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class AuthorizeValueModel
    {
        /// <summary>
        /// TIDC
        /// </summary>
        public ClientTempIdentityModel ClientTempId { get; set; }

        /// <summary>
        /// 資源保護者Id
        /// </summary>
        public string ProtectedId { get; set; }

        /// <summary>
        ///  Client 與 Resource Protector 的KEY,IV資料物件
        /// </summary>
        public SymCryptoModel ClientProtectedCryptoModel { get; set; }

        /// <summary>
        /// 任意值 r
        /// </summary>
        public string RandomValue { get; set; }

        /// <summary>
        /// 授權次數 回數票次數
        /// </summary>
        public int AuthZTimes { get; set; }

        /// <summary>
        /// 目前次數 i
        /// </summary>
        public int CurrentTimes { get; set; }

        /// <summary>
        /// 可允許Client進入的url列表
        /// </summary>
        public List<string> ValidUrlList { get; set; }
    }
}
