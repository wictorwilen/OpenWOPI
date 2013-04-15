using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OpenWOPI.Core
{
    internal class OpenWOPIWebClient: WebClient
    {
        private readonly string _access_token;
        private readonly string _access_token_ttl;
        private readonly DateTime _utc;
        private readonly OpenWOPIProofKey _proofkey;

        public OpenWOPIWebClient(string url, string access_token, string access_token_ttl, OpenWOPIProofKey proofKey)
        {
            _access_token = access_token;
            _access_token_ttl = access_token_ttl;
            _utc = DateTime.UtcNow;
            _proofkey = proofKey;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            UriBuilder builder = new UriBuilder(address);

            string append = String.Format("access_token={0}&access_token_ttl={1}",
                _access_token,
                _access_token_ttl);

            if (builder.Query == null || builder.Query.Length <= 1)
            {
                builder.Query = append;
            }
            else
            {
                builder.Query = builder.Query.Substring(1) + "&" + append;
            }


            WebRequest request = base.GetWebRequest(builder.Uri);
            if (request is HttpWebRequest)
            {
                // Add AuthZ header
                request.Headers.Add(
                    HttpRequestHeader.Authorization,
                    String.Format("Bearer {0}", HttpUtility.UrlDecode(_access_token.Replace("\n", "").Replace("\r", ""))));

                request.Headers.Add(
                    "X-WOPI-Proof",
                    _proofkey.SignData(OpenWOPIUtilities.CreateProofData(builder.Uri.ToString(), _utc, _access_token)));

                if (!String.IsNullOrEmpty(_proofkey.OldProofWithKey))
                {
                    request.Headers.Add(
                        "X-WOPI-ProofOld",
                        _proofkey.SignData(OpenWOPIUtilities.CreateProofData(builder.Uri.ToString(), _utc, _access_token), true));
                }
                request.Headers.Add(
                    "X-WOPI-TimeStamp",
                    _utc.Ticks.ToString(CultureInfo.InvariantCulture));

                request.Headers.Add(
                    "X-WOPI-ClientVersion",
                    OpenWOPIVersion.ClientVersion);

                request.Headers.Add(
                    "X-WOPI-MachineName",
                    Environment.MachineName);
            }
            return request;
        }
    }
}
