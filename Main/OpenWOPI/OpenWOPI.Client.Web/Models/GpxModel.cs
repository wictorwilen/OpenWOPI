using OpenWOPI.Client.Mvc;
using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenWOPI.Client.Web.Models
{
    public class GpxModel: OpenWOPIModel
    {
        public override OpenWOPIDocument ViewDocument(string source, Core.OpenWOPIAccessToken accessToken, bool loadMetadata = false)
        {
            GpxDocument doc = new GpxDocument(source, accessToken, OpenWOPIProofKey.ReadFromConfiguration(OpenWOPIClientConfiguration.Current));
            if (loadMetadata)
                doc.CheckFileInfo();
            doc.GetFile();
            doc.Parse();
            return doc;
        }
    }
    public class GpxDocument : OpenWOPITextDocument
    {
        public GpxDocument(string source, OpenWOPIAccessToken accessToken, OpenWOPIProofKey proofKey) : base(source, accessToken, proofKey) { }
        public override void Parse() { 

        }
    }
}