using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client
{

    /// <summary>
    /// According to 3.1.5.1.1.2.2.3 ct_app-name
    /// http://msdn.microsoft.com/en-us/library/hh695217(v=office.12).aspx
    /// </summary>
    public class OpenWOPIApp
    {
        private List<OpenWOPIAction> _actions;
        public OpenWOPIApp(Type objectType) {

            //OpenWOPIApp app = Activator.CreateInstance(objectType) as OpenWOPIApp;
            OpenWOPIAppAttribute attr = Attribute.GetCustomAttribute(objectType, typeof(OpenWOPIAppAttribute)) as OpenWOPIAppAttribute;
            
            this.Name = attr.Name;
            this.IconUrl = attr.IconUrl;
            this.CheckLicense = attr.CheckLicense;
            var actionAttributes = objectType.GetMethods().Where(m => m.GetCustomAttributes(typeof(OpenWOPIActionAttribute), true).Count() > 0);
            _actions = new List<OpenWOPIAction>();
            foreach (var actionAttribute in actionAttributes)
            {

                IEnumerable<OpenWOPIActionAttribute> attributes = actionAttribute.GetCustomAttributes<OpenWOPIActionAttribute>();
                foreach (var a in attributes)
                {
                    _actions.Add(new OpenWOPIAction(a));
                }
                
            }
            
        }

        public string Name { get; set; }
        public string IconUrl { get; set; }
        public bool CheckLicense { get; set; }
        public IEnumerable<OpenWOPIAction> Actions{ 
            get {
                return _actions;
            }

             }

    }

   
}
