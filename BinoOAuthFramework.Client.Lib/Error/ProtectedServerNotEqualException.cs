using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.Error
{

    public class ProtectedServerNotEqualException : Exception
    {
        public ProtectedServerNotEqualException() : base()
        {

        }

        public ProtectedServerNotEqualException(string message) : base(message)
        {

        }

        public ProtectedServerNotEqualException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
