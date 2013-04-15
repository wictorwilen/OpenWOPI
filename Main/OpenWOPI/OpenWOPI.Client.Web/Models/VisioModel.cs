using OpenWOPI.Client.Mvc;
using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Web;

namespace OpenWOPI.Client.Web.Models
{
    public class VisioModel : OpenWOPIModel
    {
        public override OpenWOPIDocument ViewDocument(string source, OpenWOPIAccessToken accessToken, bool loadMetadata = false)
        {
            VisioDocument doc = new VisioDocument(source, accessToken, OpenWOPIProofKey.ReadFromConfiguration(OpenWOPIClientConfiguration.Current));
            if (loadMetadata)
                doc.CheckFileInfo();
            doc.GetFile();
            string t = doc.ExtractThumbnail();
            doc.ConvertEMFToPng(t, 200);
            return doc;
        }
    }

    public class VisioDocument : OpenWOPIDocument
    {
        public VisioDocument(string source, OpenWOPIAccessToken accessToken, OpenWOPIProofKey proofKey) : base(source, accessToken, proofKey) { }

        public string ExtractThumbnail()
        {
            OpenWOPIClientConfiguration configuration = OpenWOPIClientConfiguration.Current;
            string tempfile = configuration["cache-location"] + Guid.NewGuid().ToString("N");
            using (Stream stream = this.GetDataStream())
            {
                Package package = Package.Open(stream);

                using (Stream s = package.GetPart(new Uri("/docProps/thumbnail.emf", UriKind.RelativeOrAbsolute)).GetStream())
                {
                    using (FileStream fs = new FileStream(tempfile, FileMode.CreateNew))
                    {
                        int len = 16384;
                        byte[] buffer = new byte[len];
                        int read = s.Read(buffer, 0, len);
                        while (read > 0)
                        {
                            fs.Write(buffer, 0, read);
                            read = s.Read(buffer, 0, len);
                        }
                    }
                }

            }
            return tempfile;
        }
        public void ConvertEMFToPng(string emfFile, int width)
        {
            using (Metafile mf = new Metafile(emfFile))
            {
                using (Bitmap result = new Bitmap(width, width*mf.Height/mf.Width))
                {
                    using (Graphics graphics = Graphics.FromImage(result))
                    {
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;



                        PngId = Guid.NewGuid().ToString("N");

                        string tempfile = OpenWOPIClientConfiguration.Current["cache-location"] + PngId;
                        graphics.DrawImage(mf, 0, 0, result.Width, result.Height);
                        result.Save(tempfile, ImageFormat.Png);

                    }
                }
            }

        }

        public string PngId { get; set; }
    }
}