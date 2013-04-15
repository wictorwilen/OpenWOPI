using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OpenWOPIActionAttribute : Attribute
    {
        public OpenWOPIActionAttribute(OpenWOPIActionValues Name, string Extension, string UrlSource)
        {
            this.Name = Name;
            this.Extension = Extension;
            this.UrlSource = UrlSource;
        }
        public OpenWOPIActionValues Name { get; set; }
        public string Extension { get; set; }
        public bool IsDefault { get; set; }
        /// <summary>
        /// Placeholders:
        /// - UI_LLC - UI Language [RFC1766]
        /// - DC_LLC - Data Language
        /// - EMBEDDED
        /// - DISABLE_ASYNC
        /// - DISABLE_BROADCAST
        /// - FULLSCREEN
        /// - RECORDING
        /// - THEME_ID (1 light, 2 dark)
        /// </summary>
        public string UrlSource { get; set; }
    }

}
