using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Core
{
    public abstract class OpenWOPIConfiguration: System.Collections.Specialized.StringDictionary
    {
        public abstract void Save();
    }
}
