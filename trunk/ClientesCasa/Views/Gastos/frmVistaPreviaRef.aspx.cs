using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientesCasa.Views.Gastos
{
    public partial class frmVistaPreviaRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sPath = string.Empty;

            if (!String.IsNullOrEmpty(Request.QueryString["sRuta"]))
                sPath = @"" + Request.QueryString["sRuta"];
            else
                sPath = "";

            // string sRuta = sPath; //@"C:\\FilePDFTest.pdf"; //hdnPdf.Value;
            if (!string.IsNullOrEmpty(sPath))
            {
                Context.Response.Buffer = false;
                FileStream inStr = null;
                byte[] buffer = new byte[1024];
                long byteCount;
                inStr = File.OpenRead(sPath);

                while ((byteCount = inStr.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (Context.Response.IsClientConnected)
                    {
                        Response.ContentType = "application/pdf";
                        Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        Context.Response.Flush();
                    }
                }
            }
        }
    }
}