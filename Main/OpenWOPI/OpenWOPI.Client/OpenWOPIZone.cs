using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client
{
    /// <summary>
    /// According to 3.1.5.1.1.2.3.4 st_wopi-zone
    /// http://msdn.microsoft.com/en-us/library/hh695374(v=office.12).aspx
    /// </summary>
    public class OpenWOPIZone
    {
        public OpenWOPIZone()
        {
            _apps = new List<OpenWOPIApp>();
        }
        private List<OpenWOPIApp> _apps;
        public string Name
        {
            get
            {
                return string.Format("{0}-{1}", Internal ? "internal" : "external", Protocol);
            }
        }
        public string Protocol { get; internal set; }
        public bool Internal { get; internal set; }
        public IEnumerable<OpenWOPIApp> Apps
        {
            get
            {
                return _apps;
            }
            set
            {
                _apps.Clear();
                _apps.AddRange(value);
            }
        }
        public static OpenWOPIZone InternalHttp
        {
            get
            {
                return new OpenWOPIZone()
                {
                    Internal = true,
                    Protocol = "http"
                };
            }
        }
        public static OpenWOPIZone InternalHttps
        {
            get
            {
                return new OpenWOPIZone()
                {
                    Internal = true,
                    Protocol = "https"
                };
            }
        }
        public static OpenWOPIZone ExternalHttp
        {
            get
            {
                return new OpenWOPIZone()
                {
                    Internal = false,
                    Protocol = "http"
                };
            }
        }
        public static OpenWOPIZone ExternalHttps
        {
            get
            {
                return new OpenWOPIZone()
                {
                    Internal = false,
                    Protocol = "https"
                };
            }
        }
    }
}
