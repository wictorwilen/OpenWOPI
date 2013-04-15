using OpenWOPI.Client.Mvc;
using OpenWOPI.Client.Web.Models;
using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenWOPI.Client.Web.Controllers
{
    [OpenWOPIApp("GPX", CheckLicense = false, IconUrl = "/favicon.ico")]   
    public class GpxController : OpenWOPIController
    {
        [OpenWOPIAction(OpenWOPIActionValues.view, "gpx", "/Gpx/View/?<ui=UI_LLCC&><rs=DC_LLCC&>", IsDefault = true)]
        [ActionName("View")]
        [HttpPost]
        public ActionResult DefaultView(OpenWOPIAccessToken accessToken)
        {
            return View(new GpxModel().ViewDocument(SourceFile, accessToken, true));
        }

        [OpenWOPIAction(OpenWOPIActionValues.interactivepreview, "gpx", "/Gpx/Preview/?<ui=UI_LLCC&><rs=DC_LLCC&>")]
        [OpenWOPIAction(OpenWOPIActionValues.embedview, "gpx", "/Gpx/Preview/?<ui=UI_LLCC&><rs=DC_LLCC&>")]
        [HttpPost]
        public ActionResult Preview(OpenWOPIAccessToken accessToken)
        {
            return View(new GpxModel().ViewDocument(SourceFile, accessToken, true));
        }
        

    }
}
