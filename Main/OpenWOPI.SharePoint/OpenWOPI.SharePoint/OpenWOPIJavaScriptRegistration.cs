using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace OpenWOPI.SharePoint
{
    [MdsCompliant(true)]
    public class OpenWOPIJavaScriptRegistration: WebControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            ScriptLink.RegisterScriptAfterUI(this.Page, "OpenWOPI.SharePoint/previews.js", false);

        }

    }
}
