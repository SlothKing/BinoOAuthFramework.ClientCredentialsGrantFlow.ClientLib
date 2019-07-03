using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.Error
{

    public class ProtectedServerAuthorizeException : Exception
    {
        public ProtectedServerAuthorizeException() : base()
        {

        }

        public ProtectedServerAuthorizeException(string message) : base(message)
        {

        }

        public ProtectedServerAuthorizeException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
