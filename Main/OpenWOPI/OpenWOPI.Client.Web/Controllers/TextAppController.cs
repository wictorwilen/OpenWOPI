using OpenWOPI.Client.Mvc;
using OpenWOPI.Client.Web.Models;
using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OpenWOPI.Client.Web.Controllers
{ 
    
    [OpenWOPIApp("Text", CheckLicense = false, IconUrl = "/favicon.ico")]   
    [HandleError()] 
    public class TextAppController : OpenWOPIController
    {
        //
        // GET: /TextApp/
   [OpenWOPIAction(OpenWOPIActionValues.view, "txt", "/TextApp/View/?<ui=UI_LLCC&><rs=DC_LLCC&>", IsDefault = true)]
        [ActionName("View")]
        [HttpPost]
        public ActionResult DefaultView(OpenWOPIAccessToken accessToken)
        {
            TextAppModel model = new TextAppModel();
            return View(model.ViewDocument(SourceFile, accessToken, true));
        }
        [OpenWOPIAction(OpenWOPIActionValues.interactivepreview, "txt", "/TextApp/Preview/?<ui=UI_LLCC&><rs=DC_LLCC&>")]
        [OpenWOPIAction(OpenWOPIActionValues.embedview, "txt", "/TextApp/Preview/?<ui=UI_LLCC&><rs=DC_LLCC&>")]
        [HttpPost]
        public ActionResult Preview(OpenWOPIAccessToken accessToken)
        {
            TextAppModel model = new TextAppModel();
            return View(model.ViewDocument(SourceFile, accessToken, false));
        }
      
        [OpenWOPIAction(OpenWOPIActionValues.edit, "txt", "/TextApp/Edit/?<ui=UI_LLCC&><rs=DC_LLCC&>")]
        [HttpPost]
        public ActionResult Edit(OpenWOPIAccessToken accessToken)
        {
            TextAppModel model = new TextAppModel();
            return View(model.ViewDocument(SourceFile, accessToken, true));
        }
        [HttpPost]
        [HandleError(ExceptionType=typeof(WebException), View="WebException")]
        public ActionResult Save(OpenWOPIDocumentPostData data)
        {
            TextAppModel model = new TextAppModel();
            OpenWOPITextDocument doc = new OpenWOPITextDocument(SourceFile, data.AccessToken, OpenWOPIProofKey.ReadFromConfiguration(OpenWOPIClientConfiguration.Current));
            doc.CheckFileInfo();
            doc.Content = data.Content;
            
            doc.PutFile();
            // TODO: handle errors in the WebException view
            
            return Redirect(doc.HostEditUrl);
        }



    }
}
