using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientesCasa.Views.Catalogos
{
    public partial class frmVistaPreviaDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["FileContrato"] != null)
            {
                byte[] vbFile = (byte[])Session["FileContrato"];

                Context.Response.Buffer = false;

                if (Context.Response.IsClientConnected)
                {
                    Response.ContentType = "application/pdf";
                    Context.Response.OutputStream.Write(vbFile, 0, vbFile.Length);
                    Context.Response.Flush();
                }

            }


        }
    }
}