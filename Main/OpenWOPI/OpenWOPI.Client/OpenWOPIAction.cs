using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client
{
    public class OpenWOPIAction
    {
        public OpenWOPIAction(OpenWOPIActionAttribute attribute)
        {
            this.Name = attribute.Name;
            this.Extension = attribute.Extension;
            this.Default = attribute.IsDefault;
            this.UrlSource = attribute.UrlSource;
        }
        public OpenWOPIActionValues Name { get; set; }
        public string Extension { get; set; }
        public bool Default { get; set; }
        public string UrlSource { get; set; }
    }

  
   
}



