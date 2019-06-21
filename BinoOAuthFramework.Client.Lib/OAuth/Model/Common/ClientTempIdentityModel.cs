using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Common
{
    public class ClientTempIdentityModel
    {
        /// <summary>
        /// 客戶端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// H^(n) * r   or  H^(n-i+1) * r 
        /// </summary>
        public string HashValue { get; set; }
    }
}
