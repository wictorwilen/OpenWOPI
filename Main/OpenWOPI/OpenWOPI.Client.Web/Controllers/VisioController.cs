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
    [OpenWOPIApp("Visio", CheckLicense = false, IconUrl = "/favicon.ico")]
    public class VisioController : OpenWOPIController
    {
        [OpenWOPIAction(OpenWOPIActionValues.interactivepreview, "vsdx", "/Visio/Preview/?<ui=UI_LLCC&><rs=DC_LLCC&>", IsDefault = true)]
        [OpenWOPIAction(OpenWOPIActionValues.imagepreview, "vsdx", "/Visio/Preview/?<ui=UI_LLCC&><rs=DC_LLCC&>", IsDefault = true)]
        [OpenWOPIAction(OpenWOPIActionValues.embedview, "vsdx", "/Visio/Preview/?<ui=UI_LLCC&><rs=DC_LLCC&>", IsDefault = true)]
        [HttpPost]
        public ActionResult Preview(OpenWOPIAccessToken accessToken)
        {

            return View(new VisioModel().ViewDocument(SourceFile, accessToken, true));
        }

    }
}
