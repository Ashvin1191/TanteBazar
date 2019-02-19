using System;
using System.Collections.Generic;
using System.Text;

namespace TanteBazar.WebApi.Client
{
    public class Configuration
    {
        public Configuration(string serviceUrl, string clientSecret)
        {
            ServiceUrl = serviceUrl;
            ClientSecret = clientSecret;
        }

        public string ServiceUrl { get; }

        public string ClientSecret { get; }
    }
}
