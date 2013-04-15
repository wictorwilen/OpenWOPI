using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client.Mvc
{
    public abstract class OpenWOPIModel
    {
        public abstract OpenWOPIDocument ViewDocument(string source, OpenWOPIAccessToken accessToken, bool loadMetadata = false);
    }
}
