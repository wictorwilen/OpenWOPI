using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OpenWOPIAppAttribute : Attribute
    {
        public OpenWOPIAppAttribute(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }
        public string IconUrl { get; set; }
        public bool CheckLicense { get; set; }

    }
}
