using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Core
{
    public class OpenWOPIProofKey
    {
        private const int KeyLength = 2048;
        private const string HashAlgorithm = "SHA256";
        public string Proof { get; internal set; }
        public string ProofWithKey { get; internal set; }
        public string OldProof { get; internal set; }
        public string OldProofWithKey { get; internal set; }
        public bool Configured { get; internal set; }

        public string SignData(byte[] data, bool useOld = false)
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(KeyLength))
            {
                if (useOld)
                {
                    provider.ImportCspBlob(Convert.FromBase64String(this.OldProofWithKey));
                }
                else
                {
                    provider.ImportCspBlob(Convert.FromBase64String(this.ProofWithKey));
                }
                var signed = provider.SignData(data, HashAlgorithm);
                return Convert.ToBase64String(signed);
            }
        }

        public static OpenWOPIProofKey ReadFromConfiguration(OpenWOPIConfiguration configuration)
        {
            OpenWOPIProofKey key = new OpenWOPIProofKey();
            key.Proof = configuration["proof"];
            key.ProofWithKey = configuration["proof-key"];
            key.OldProof = configuration["old-proof"];
            key.OldProofWithKey = configuration["old-proof-key"];            
            return key;
        }
        public static void SaveToConfiguration(OpenWOPIProofKey key, string source)
        {
            string[] lines = new string[] {
                "proof:"+ key.Proof,
                "proof-key:" + key.ProofWithKey,
                "old-proof:" + key.OldProof,
                "old-proof-key:" + key.OldProofWithKey
            };
            System.IO.File.WriteAllLines(source, lines);
        }

        public static OpenWOPIProofKey GenerateNew(OpenWOPIProofKey oldKey) {

            using (RSACryptoServiceProvider serviceProvider = new RSACryptoServiceProvider(KeyLength))
            {
                OpenWOPIProofKey newKey = new OpenWOPIProofKey();
                newKey.OldProof = oldKey.Proof;
                newKey.OldProofWithKey = oldKey.ProofWithKey;
                newKey.Proof = System.Convert.ToBase64String(serviceProvider.ExportCspBlob(false));
                newKey.ProofWithKey = System.Convert.ToBase64String(serviceProvider.ExportCspBlob(true));
                return newKey;
            }
        }

    }
}
