using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.Error
{
    public class ClientNotEqualException : Exception
    {
        public ClientNotEqualException() : base()
        {

        }

        public ClientNotEqualException(string message) : base(message)
        {

        }

        public ClientNotEqualException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
