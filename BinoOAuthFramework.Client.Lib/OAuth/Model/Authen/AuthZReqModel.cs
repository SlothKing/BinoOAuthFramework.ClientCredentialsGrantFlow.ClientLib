using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.OAuth.Model.Authen
{
    public class AuthZReqModel
    {
        public string Token { get; set; }

        public string AppName { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
        
    }
}
