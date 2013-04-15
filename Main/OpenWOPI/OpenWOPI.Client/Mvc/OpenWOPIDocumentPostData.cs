using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client.Mvc
{
    public class OpenWOPIDocumentPostData
    {
        public string Content { get; set; }
        public OpenWOPIAccessToken AccessToken { get; set; }
    }
}
