using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class AuthorizeKeyModel
    {
        /// <summary>
        /// Hash random value (n-i +1)
        /// </summary>
        public string HashValue { get; set; }

        /// <summary>
        /// Hash Resource Protector and Client Key , ClientTempId, Current Authorize Times
        /// </summary>
        public string HashKeyTIDCTimesValue { get; set; }
    }
}
