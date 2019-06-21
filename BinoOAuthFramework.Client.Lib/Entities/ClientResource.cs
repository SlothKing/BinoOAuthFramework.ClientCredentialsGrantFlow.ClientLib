using System;
using System.Collections.Generic;
using System.Text;

namespace BinoOAuthFramework.Client.Lib.Entities
{
    public class ClientResource
    {
        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientKey { get; set; }

        public string ClientIV { get; set; }

        public List<ClientToProtectedServerData> ProtectedServers { get; set; }
    }
}
