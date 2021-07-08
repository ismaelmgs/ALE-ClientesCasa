using System;
using System.Web.UI;
using ClientesCasa.Clases;
using ClientesCasa.Objetos;
using System.Data;
using System.Configuration;
using ClientesCasa.Presenter;
using ClientesCasa.Interfaces;
using NucleoBase.Core;
using ClientesCasa.DomainModel;
using System.Text.RegularExpressions;

namespace ClientesCasa
{
    public partial class login : System.Web.UI.Page, IViewLogin
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Login_Presenter(this, new DBRolAccion());
        }

        protected void btnIngregar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.S() == string.Empty)
            {
                MostrarMensaje("El Usuario es requerido", "Aviso");
            }
            else if (txtPassword.Text.S() == string.Empty)
            {
                MostrarMensaje("La contraseña es requerida", "Aviso");
            }
            else
            {
                bool ban = false;
                if (ban)
                {
                    oUsuario = ClsSecurity.IsValidAD(txtUsuario.Text.S(), txtPassword.Text.S());
                    if (oUsuario.bEncontrado)
                    {
                        MostrarMensaje(oUsuario.sEstatus, "Aviso");
                    }
                    else
                    {
                        Session["nombreI"] = oUsuario;

                        ObtieneValores();
                        oUsuario.dTPermisos = (DataTable)Session["oDatos"];
                        Session["UserIdentity"] = oUsuario;
                        string sFinal = string.Empty;

                        if (oUsuario.sUrlPaginaInicial == string.Empty)
                        {
                            string sSitio = ConfigurationManager.AppSettings["NombreSitio"].S();
                            sFinal = "/" + sSitio + "/frmDefault.aspx";
                        }
                        else
                            sFinal = oUsuario.sUrlPaginaInicial;

                        Response.Redirect(sFinal);
                    }
                }
                else
                {
                    oUsuario = new UserIdentity();
                    oUsuario.sUsuario = "i.morato";
                    oUsuario.sName = "Ismael Morato Gallegos";
                    oUsuario.sRolDescripcion = "Pruebas";
                    oUsuario.iRol = 1;
                    oUsuario.sUrlPaginaInicial = "";
                    oUsuario.sCorreoBaseUsuario = "ac.tlc@aerolineasejecutivas.com";

                    ObtieneValores();
                    oUsuario.dTPermisos = (DataTable)Session["oDatos"];
                    Session["UserIdentity"] = oUsuario;

                    string sFinal = string.Empty;

                    sFinal = "/frmDefault.aspx";

                    Response.Redirect(sFinal);
                }
            }
        }
        #endregion

        #region METODOS
        public void LoadObjects(DataTable dtObject)
        {
            Session["oDatos"] = dtObject;
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            Regex.Replace(sMensaje, "'", "/");
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }
        public void ObtieneValores()
        {
            if (eSearchObj != null)
                eSearchObj(null, EventArgs.Empty);

        }
        public string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl)
        {
            if (Request.IsSecureConnection)
                return string.Format("https://{0}{1}{2}", Request.Url.Host,
                    Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString(),
                    Page.ResolveUrl(relativeUrl));
            else
                return string.Format("http://{0}{1}{2}", Request.Url.Host,
                    Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString(),
                    Page.ResolveUrl(relativeUrl));
        }
        #endregion

        #region "Vars y Propiedades"
        Login_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public UserIdentity oUsuario
        {
            get { return (UserIdentity)ViewState["VSUsuario"]; }
            set { ViewState["VSUsuario"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                int iIdRol;
                int iEstatus;

                iIdRol = oUsuario.iRol.I();
                iEstatus = 1;

                return new object[]{
                                        "@IdRol",  iIdRol ,
                                        "@IdStatus", iEstatus
                                    };
            }

        }
        #endregion
    }
}