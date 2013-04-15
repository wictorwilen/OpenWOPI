using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Core
{
    public class OpenWOPITextDocument: OpenWOPIDocument
    {
        public OpenWOPITextDocument(string source, OpenWOPIAccessToken accessToken, OpenWOPIProofKey proofKey)
            : base(source, accessToken, proofKey)
        {
            DocumentEncoding = Encoding.UTF8;
        }
        public Encoding DocumentEncoding { get; set; }

        public string Content        
        {
            get
            {
                return DocumentEncoding.GetString(Data);
            }

            set {
                Data = DocumentEncoding.GetBytes(value);
            }
        }
    }
}
