using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OpenWOPI.Client.Mvc
{
    public class OpenWOPIController: Controller
    {
        public string SourceFile
        {
            get
            {
                return Request.QueryString["WOPISrc"];
            }
        }

        public string UILanguage {
            get {
                return Request.QueryString["UI_LLC"];
            }
        }
        public string DataLanguage
        {
            get
            {
                return Request.QueryString["DC_LLC"];
            }
        }
        public OpenWOPITheme Theme
        {
            get
            {
                string theme = Request.QueryString["THEME_ID"];
                if (theme == "2")
                {
                    return OpenWOPITheme.Dark;
                }
                return OpenWOPITheme.Light;
            }
        }

        /// - UI_LLC - UI Language [RFC1766]
        /// - DC_LLC - Data Language
        /// - EMBEDDED
        /// - DISABLE_ASYNC
        /// - DISABLE_BROADCAST
        /// - FULLSCREEN
        /// - RECORDING
        /// - THEME_ID (1 light, 2 dark)
    }
}
