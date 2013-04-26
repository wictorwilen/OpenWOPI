using OpenWOPI.Client.Mvc;
using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenWOPI.Client.Web.Models
{
    public class TextAppModel : OpenWOPIModel
    {
        public override OpenWOPIDocument ViewDocument(string source, OpenWOPIAccessToken accessToken, bool loadMetadata = false)
        {
            OpenWOPITextDocument doc = new OpenWOPITextDocument(source, accessToken, OpenWOPIProofKey.ReadFromConfiguration(OpenWOPIClientConfiguration.Current));
            if (loadMetadata)
                doc.CheckFileInfo();
            doc.GetFile();
            return doc;
        }
    }
}