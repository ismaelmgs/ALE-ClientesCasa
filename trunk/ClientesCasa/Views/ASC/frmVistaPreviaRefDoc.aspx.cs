using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using ClientesCasa.Presenter;

using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

using NucleoBase.Core;
using System.Globalization;
using ClientesCasa.Clases;
using System.Text;
using System.Xml;
using System.Threading;

namespace ClientesCasa.Views.ASC
{
    public partial class frmVistaPreviaRefDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sPath = string.Empty;
            string sMes = string.Empty;
            string sAnio = string.Empty;
            string sArchivo = string.Empty;

            if (File.Exists(sCadArchivo)) { }

            if (!String.IsNullOrEmpty(Request.QueryString["archivo"]))
                sArchivo = @"" + Request.QueryString["archivo"];
            else
                sArchivo = "";

            if (!String.IsNullOrEmpty(Request.QueryString["mes"]))
                sMes = @"" + Request.QueryString["mes"];
            else
                sMes = "";

            if (!String.IsNullOrEmpty(Request.QueryString["anio"]))
                sAnio = @"" + Request.QueryString["anio"];
            else
                sAnio = "";

            sPath = ArmaRuta(2, sMes, sAnio, sArchivo); //sRaiz + @"\" + sAnio + "-" + sMes + @"\" + sProveedor + @"\" + sCarpetaFac + @"\" + sArchivo + ".pdf";

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

        public string ArmaRuta(int iOpcion, string sMes, string sAnio, string sArchivo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            string sTipoDoc = string.Empty;
            if (iOpcion == 1)
                sTipoDoc = ".xml";
            else
                sTipoDoc = ".pdf";

            string sCadFinal = string.Empty;
            string sFileName = sArchivo;

            if (sMes != string.Empty && sAnio != string.Empty)
            {
                string sRaiz = ConfigurationManager.AppSettings["RutaArchivos"].S();

                string sCardCode = ConfigurationManager.AppSettings["CardCodeASC"].S();
                string sGroupCode = new DBAccesoSAP().DBGetObtieneGruopCodeProveedor(sCardCode).Rows[0][0].S();
                string sCardName = new DBAccesoSAP().DBGetObtieneGruopCodeProveedor(sCardCode).Rows[0][1].S();
                string sFolderNalExt = string.Empty;
                string sFolder2 = string.Empty;

                switch (sGroupCode)
                {
                    // Nacionales
                    case "102":
                        sFolder2 = "nacionales";
                        sFolderNalExt = System.Configuration.ConfigurationManager.AppSettings["FolderNacional"].S();
                        break;
                    // Extranjeros
                    case "103":
                        sFolder2 = "extranjeros";
                        sFolderNalExt = System.Configuration.ConfigurationManager.AppSettings["FolderExtranjero"].S();
                        break;
                }

                string sAnioFac = sAnio;
                string sMesFac = string.Empty;

                if (sMes.Length == 1)
                    sMesFac = "0" + sMes;
                else
                    sMesFac = sMes;

                //\\192.168.1.225\Facturas Proveedores\Nacionales\2019\Proveedores nacionales TLC\ALE SERVICE CENTER, S. DE R.L. DE C.V\01

                sCadFinal = sRaiz + sFolderNalExt + @"\" + sAnio + @"\Proveedores " + sFolder2 + " " + "TLC" + @"\" + sCardName + @"\" + sMes + @"\" + sFileName + sTipoDoc;

            }

            return sCadFinal;
        }

        public string sCadArchivo
        {
            get { return (string)ViewState["VSValidaArchivo"]; }
            set { ViewState["VSValidaArchivo"] = value; }
        }
    }
}