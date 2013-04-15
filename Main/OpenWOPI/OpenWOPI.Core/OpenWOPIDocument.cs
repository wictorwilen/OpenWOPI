using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace OpenWOPI.Core
{
    public class OpenWOPIDocument
    {
        public const Int32 MaxDocumentSize = Int32.MaxValue;
        public OpenWOPIDocument()
        {
        }
        public OpenWOPIDocument(string source, OpenWOPIAccessToken accessToken, OpenWOPIProofKey proofKey): this()
        {
            AccessToken = accessToken;
            Source = source;
            ProofKey = proofKey;
        }
        public string Title { get; set; }
        public string Source { get; internal set; }
        public OpenWOPIAccessToken AccessToken { get; internal set; }
        public OpenWOPIProofKey ProofKey { get; internal set; }

        public string BreadcrumbFolderUrl { get; internal set; }
        public string BreadcrumbFolderName { get; internal set; }
        public string BaseFileName { get; internal set; }
        public string CloseUrl { get; internal set; }
        public string HostEditUrl { get; internal set; }
        public bool WebEditingDisabled { get; internal set; }
        public string DownloadUrl { get; set; }
        public bool SupportsUpdate { get; set; }
        public bool SupportsLocks { get; set; }
        public string Version { get; set; }
        public string UserFriendlyName { get; set; }
        public int Size { get; set; }
        public string OwnerId { get; set; }
        public string SHA256 { get; set; }

        public virtual byte[] Data { get; set; }

        public virtual void Parse() { }
        /// <summary>
        /// 3.3.5.1.1 CheckFileInfo
        /// [MS-WOPI] 3.3.5.1.1.2 Response Body
        /// </summary>
        public void CheckFileInfo()
        {
            string url = String.Format("{0}", Source);
            using (OpenWOPIWebClient client = new OpenWOPIWebClient(url, AccessToken.access_token, AccessToken.access_token_ttl, ProofKey))
            {
                string data = client.DownloadString(url);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var d = jss.Deserialize<Dictionary<string, string>>(data);
                BreadcrumbFolderUrl = d["BreadcrumbFolderUrl"];
                BreadcrumbFolderName= d["BreadcrumbFolderName"];
                BaseFileName= d["BaseFileName"];
                CloseUrl = d["CloseUrl"];
                HostEditUrl = d["HostEditUrl"];
                WebEditingDisabled = bool.Parse(d["WebEditingDisabled"]);
                SupportsUpdate = bool.Parse(d["SupportsUpdate"]);
                SupportsLocks = bool.Parse(d["SupportsLocks"]);
                DownloadUrl = d["DownloadUrl"];
                Version = d["Version"];
                OwnerId = d["OwnerId"];
                SHA256 = d["SHA256"];
                UserFriendlyName = d["UserFriendlyName"];
                Size = int.Parse(d["Size"]);
            }
        }
        /// <summary>
        /// 3.3.5.3.1 GetFile
        /// </summary>
        public void GetFile()
        {
            string url = String.Format("{0}/contents", Source);
            using (OpenWOPIWebClient client = new OpenWOPIWebClient(url, AccessToken.access_token, AccessToken.access_token_ttl, ProofKey))
            {
                client.Headers.Add("X-WOPI-MaxExpectedSize", MaxDocumentSize.ToString());
                Data = client.DownloadData(url);
            }

        }
        public System.IO.Stream GetDataStream()
        {
            return new MemoryStream(Data);
        }

        public string Lock { get; set; }
        /// <summary>
        /// X-WOPI-Lock
        /// </summary>
        public string LockFile()
        {
            string url = String.Format("{0}", Source);
            using (OpenWOPIWebClient client = new OpenWOPIWebClient(url, AccessToken.access_token, AccessToken.access_token_ttl, ProofKey))
            {
                Lock = Guid.NewGuid().ToString();
                client.Headers.Add("X-WOPI-Override", "LOCK");
                client.Headers.Add("X-WOPI-Lock", Lock);
                client.UploadString(url, string.Empty);
                return Lock;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public void PutFile()
        {
            string url = String.Format("{0}/contents", Source);
            using (OpenWOPIWebClient client = new OpenWOPIWebClient(url, AccessToken.access_token, AccessToken.access_token_ttl, ProofKey))
            {
                client.Headers.Add("X-WOPI-Override", "PUT");
                client.Headers.Add("X-WOPI-Size", Data.Length.ToString());
                //client.Headers.Add("X-WOPI-Lock", "Lock-ID");
                client.UploadData(url, Data);

            }
        }
    }
}
