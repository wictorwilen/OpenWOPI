using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWOPI.Client
{
    /// <summary>
    /// According to MS-WOPI 3.1.4.1.1.2.3.1 st_wopi-action-values
    /// http://msdn.microsoft.com/en-us/library/hh695254(v=office.12).aspx
    /// </summary>
    public enum OpenWOPIActionValues
    {
        view,
        edit,
        mobileview,
        embedview,
        present,
        presentservice,
        attendservice,
        attend,
        editnew,
        imagepreview,
        interactivepreview,
        formsubmit,
        formedit,
        rest,

    }
}
