using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Core
{
    public class OpenWOPIVersion
    {
        static OpenWOPIVersion() {
            ClientVersion = String.Format("OpenWOPI.Client.{0}", typeof(OpenWOPIVersion).Assembly.GetName().Version.ToString());
            ServerVersion = String.Format("OpenWOPI.Server.{0}", typeof(OpenWOPIVersion).Assembly.GetName().Version.ToString());
        }

        public static readonly string ClientVersion;
        public static readonly string ServerVersion;
    }
}
