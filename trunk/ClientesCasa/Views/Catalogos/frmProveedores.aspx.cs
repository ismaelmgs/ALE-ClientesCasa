using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Clases;
using System.Data;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;

namespace ClientesCasa.Views.Catalogos
{
    public partial class frmProveedores : System.Web.UI.Page, IViewProveedores
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Proveedores_Presenter(this, new DBProveedores());

            if (!IsPostBack)
            {
                //iIdRubro = 0;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }
        protected void btnAgregarProv_Click(object sender, EventArgs e)
        {
            iIdProv = 0;
            lblTituloProveedor.Text = "Alta de Proveedores";
            txtProveedor.Text = string.Empty;
            mpeProvAgrega.Show();
        }

        protected void btnAceptarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                sDescProv = txtProveedor.Text;
                if (iIdProv == 0)
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else if (iIdProv > 0)
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                mpeProvAgrega.Hide();
            }
            catch (Exception ex)
            {

            }
        }

        protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblTituloProveedor.Text = "Edición de Proveedores";

                GridViewRow Row = ((ImageButton)sender).NamingContainer as GridViewRow;
                txtProveedor.Text = Row.Cells[0].Text.S();
                iIdProv = gvProveedores.DataKeys[Row.RowIndex]["IdProveedor"].S().I();

                mpeProvAgrega.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void imbEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int iIndex = ((ImageButton)sender).CommandArgument.S().I();
                iIdProv = gvProveedores.DataKeys[iIndex]["IdProveedor"].S().I();

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    iIdRubro = 0;
                //    GridView gv = (GridView)e.Row.FindControl("gvDetalleRubro");
                //    iIdRubro = gvRubros.DataKeys[e.Row.RowIndex]["IdRubro"].S().I();

                //    if (eSearchCuentas != null)
                //        eSearchCuentas(sender, e);

                //    gv.DataSource = dtCuentasRubro;
                //    gv.DataBind();
                //}
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                txtProveedor.Text = string.Empty;
                mpeProvAgrega.Hide();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
        #region METODOS

        public void LoadProveedores(DataTable dt)
        {
            try
            {
                gvProveedores.DataSource = dt;
                gvProveedores.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }

        #endregion


        #region VARIABLES Y PROPIEDADES

        Proveedores_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public int iIdProv
        {
            get { return (int)ViewState["VSiIdProv"]; }
            set { ViewState["VSiIdProv"] = value; }
        }

        public string sDescProv
        {
            get { return (string)ViewState["VSDescProv"]; }
            set { ViewState["VSDescProv"] = value; }
        }

        public object[] oArrProv
        {
            get
            {
                return new object[] {
                                        "@IdProv", iIdProv,
                                        "@DescripcionProv", txtProveedor.Text,
                                        "@UsuarioCreacion", Utils.GetUser
                                    };
            }
        }

        #endregion
    }
}