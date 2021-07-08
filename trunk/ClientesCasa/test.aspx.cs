using ClientesCasa.DomainModel;
using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientesCasa
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            btnCredencial.Visible = true;
        }

        protected void btnCredencial_Click(object sender, EventArgs e)
        {
            UserIdentity oUsuario = new UserIdentity();
            oUsuario.sUsuario = "i.morato";
            oUsuario.sName = "Ismael Morato Gallegos";
            oUsuario.sRolDescripcion = "Sistemas";
            oUsuario.iRol = 1;
            oUsuario.sUrlPaginaInicial = "";
            oUsuario.sCorreoBaseUsuario = "ac.tlc@aerolineasejecutivas.com";

            ObtieneValores();
            oUsuario.dTPermisos = (DataTable)Session["oDatos"];
            Session["UserIdentity"] = oUsuario;

            string sFinal = string.Empty;

            sFinal = "frmDefault.aspx";

            Response.Redirect(sFinal);
        }

        private void ObtieneValores()
        {
            object[] oArr = new object[]{
                                        "@IdRol",  1 ,
                                        "@IdStatus", 1
                                    };
            Session["oDatos"] = new DBRolAccion().DBSearchObj(oArr);
        }
    }
}