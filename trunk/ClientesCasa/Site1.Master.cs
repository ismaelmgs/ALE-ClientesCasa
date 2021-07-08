using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientesCasa
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserIdentity"] == null)
                    Response.Redirect("~/login.aspx");

                if (!IsPostBack)
                {
                    UserIdentity oUser = (UserIdentity)Session["UserIdentity"];
                    lblUsuario.Text = oUser.sName;
                    lblPerfil.Text = oUser.sRolDescripcion;
                }
            }
            catch (Exception ex)
            {
                alert("Error de conexión");
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // El código siguiente ayuda a proteger frente a ataques XSRF
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Utilizar el token Anti-XSRF de la cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generar un nuevo token Anti-XSRF y guardarlo en la cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Establecer token Anti-XSRF
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validar el token Anti-XSRF
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Error de validación del token Anti-XSRF.");
                }
            }
        }
        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            //Context.GetOwinContext().Authentication.SignOut();
        }
        private void alert(string m)
        {
            m = "alert('" + m + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", m, true);
        }

        protected void lkbSalir_Click1(object sender, EventArgs e)
        {
            try
            {
                Session["UserIdentity"] = null;
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}