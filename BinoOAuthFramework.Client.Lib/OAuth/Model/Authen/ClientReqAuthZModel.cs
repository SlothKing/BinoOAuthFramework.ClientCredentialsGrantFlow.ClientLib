using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    /// <summary>
    /// 客戶端請求授權資烙物件
    /// </summary>
    public class ClientReqAuthZModel
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
        /// 目前次數 i
        /// </summary>
        public int CurrentTimes { get; set; }

        /// <summary>
        /// 目前次數加密字串
        /// </summary>
        public string CurrentTimesCypherText { get; set; }

        /// <summary>
        /// 可允許Client進入的url列表 api/controller/action
        /// </summary>
        public List<string> ValidUrlList { get; set; }
    }
}
