using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.Error
{
    public class ClientAuthorizeTokenExpiredException : Exception
    {
        public ClientAuthorizeTokenExpiredException() : base()
        {

        }

        public ClientAuthorizeTokenExpiredException(string message) : base(message)
        {

        }

        public ClientAuthorizeTokenExpiredException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
