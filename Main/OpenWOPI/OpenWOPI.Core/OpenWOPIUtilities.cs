using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OpenWOPI.Core
{
    public class OpenWOPIUtilities
    {
        static OpenWOPIUtilities() { }

        
        public static byte[] CreateProofData(string url, DateTime time, string accesstoken)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] accessbytes = encoding.GetBytes(
                HttpUtility.UrlDecode(accesstoken));
            byte[] urlbytes = encoding.GetBytes(
                new Uri(url).AbsoluteUri.ToUpperInvariant());
            byte[] ticksbytes = getNetworkOrderBytes(time.Ticks);

            List<byte> list = new List<byte>();
            list.AddRange(getNetworkOrderBytes(accessbytes.Length));
            list.AddRange(accessbytes);
            list.AddRange(getNetworkOrderBytes(urlbytes.Length));
            list.AddRange(urlbytes);
            list.AddRange(getNetworkOrderBytes(ticksbytes.Length));
            list.AddRange(ticksbytes);
            return list.ToArray();
        }
        private static byte[] getNetworkOrderBytes(int i)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
        }
        private static byte[] getNetworkOrderBytes(long i)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
        }
    }
    
}
