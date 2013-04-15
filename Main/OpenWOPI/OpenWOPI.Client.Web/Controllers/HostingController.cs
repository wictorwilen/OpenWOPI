using OpenWOPI.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenWOPI.Client.Web.Controllers
{
    public class HostingController : Controller
    {

        //
        // GET: /Hosting/

        public ActionResult Discovery()
        {
            return View(new OpenWOPIDiscoveryModel());
        }

    }
}
