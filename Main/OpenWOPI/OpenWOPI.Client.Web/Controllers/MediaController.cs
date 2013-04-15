using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenWOPI.Client.Web.Controllers
{
    public class MediaController : Controller
    {
        //
        // GET: /Media/

        [HttpGet]
        public ActionResult Png(string id)
        {
            return new FileStreamResult(new StreamReader(OpenWOPIClientConfiguration.Current["cache-location"] + id).BaseStream, "image/png");

        }

    }
}
