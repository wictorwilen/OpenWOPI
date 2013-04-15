using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace OpenWOPI.Client.Web.Models
{
    public class OpenWOPIDiscoveryModel
    {
        private List<OpenWOPIZone> _zones;
        public OpenWOPIDiscoveryModel()
        {
            _zones = new List<OpenWOPIZone>();

            OpenWOPIClientConfiguration configuration = OpenWOPIClientConfiguration.Current;
            string protocols = configuration["protocols"];
            InternalBaseUrl = configuration["internal-url"];
            ExternalBaseUrl = configuration["external-url"];

            ProofKey = OpenWOPIProofKey.ReadFromConfiguration(configuration);
            
            if(protocols == "http" || protocols == "both") {
                if (!String.IsNullOrEmpty(InternalBaseUrl))
                {
                    _zones.Add(OpenWOPIZone.InternalHttp);
                }
                if (!String.IsNullOrEmpty(ExternalBaseUrl))
                {
                    _zones.Add(OpenWOPIZone.ExternalHttp);
                }
            }
            if (protocols == "https" || protocols == "both")
            {
                if (!String.IsNullOrEmpty(InternalBaseUrl))
                {
                    _zones.Add(OpenWOPIZone.InternalHttps);
                }
                if (!String.IsNullOrEmpty(ExternalBaseUrl))
                {
                    _zones.Add(OpenWOPIZone.ExternalHttps);
                }
            }
            
            var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                         from type in assembly.GetTypes()
                         where (type.GetCustomAttributes(typeof(OpenWOPIAppAttribute), true).Length > 0)
                         select type).ToList();

            List<OpenWOPIApp> apps = new List<OpenWOPIApp>();
            foreach (var type in types)
            {
                apps.Add(new OpenWOPIApp(type));

            }
            foreach (var zone in _zones)
            {
                zone.Apps = apps;
            }
            

        }

        public string InternalBaseUrl
        {
            get;
            internal set;
        }
        public string ExternalBaseUrl
        {
            get;
            internal set;
        }
        public OpenWOPIProofKey ProofKey { get; internal set; }

        public IEnumerable<OpenWOPIZone> Zones {
            get {
                return _zones;
            }
        }
    }
}