using BinoOAuthFramework.Client.Lib.OAuth.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class ClientProtectedMacModel
    {
        /// <summary>
        /// 加鹽值
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// TIDC
        /// </summary>
        public ClientTempIdentityModel ClientTempId { get; set; }

        /// <summary>
        /// 資源保護者Id
        /// </summary>
        public string ProtectedId { get; set; }

        /// <summary>
        /// 雜湊值
        /// </summary>
        public string HashValue { get; set; }

        /// <summary>
        /// 失效時間
        /// </summary>
        public long ExpiredTime { get; set; }

        /// <summary>
        /// 授權次數
        /// </summary>
        public int AuthZTimes { get; set; }

        /// <summary>
        ///  Client 與 Resource Protector 的KEY,IV資料物件
        /// </summary>
        public SymCryptoModel ClientProtectedCryptoModel { get; set; }

    }
}
