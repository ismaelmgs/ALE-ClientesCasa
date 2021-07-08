using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Clases;
using System.Data;

namespace ClientesCasa.Views.Catalogos
{
    public partial class frmRubros : System.Web.UI.Page, IViewRubros
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Rubros_Presenter(this, new DBRubros());

            if (!IsPostBack)
            {
                iIdRubro = 0;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }
        protected void btnAgregarRubr_Click(object sender, EventArgs e)
        {
            iIdRubro = 0;
            lblTituloRubros.Text = "Alta de Rubros";
            txtRubro.Text = string.Empty;
            mpeRubrosAgrega.Show();
        }
        protected void btnAceptarRubro_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdRubro == 0)
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else if (iIdRubro > 0)
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                mpeRubrosAgrega.Hide();
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                txtRubro.Text = string.Empty;
                mpeRubrosAgrega.Hide();
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvRubros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    iIdRubro = 0;
                    GridView gv = (GridView)e.Row.FindControl("gvDetalleRubro");
                    iIdRubro = gvRubros.DataKeys[e.Row.RowIndex]["IdRubro"].S().I();

                    if (eSearchCuentas != null)
                        eSearchCuentas(sender, e);

                    gv.DataSource = dtCuentasRubro;
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvDetalleRubro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv != null)
                {
                    iIdCuenta = gv.DataKeys[e.CommandArgument.I()].Value.S().I();

                    if (e.CommandName == "Eliminar")
                    {
                        if (eSetEliminaCuenta != null)
                            eSetEliminaCuenta(sender, e);

                        if (eSearchObj != null)
                            eSearchObj(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void imbAgrgar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtPrefijo.Text = string.Empty;
                ddlCuentas.DataSource = null;
                ddlCuentas.Items.Clear();
                txtCuentas.Text = string.Empty;
                int iIndex = ((ImageButton)sender).CommandArgument.S().I();
                iIdRubro = gvRubros.DataKeys[iIndex]["IdRubro"].S().I();

                mpeCuentas.Show();
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblTituloRubros.Text = "Edición de Rubros";

                GridViewRow Row = ((ImageButton)sender).NamingContainer as GridViewRow;
                txtRubro.Text = Row.Cells[1].Text.S();
                iIdRubro = gvRubros.DataKeys[Row.RowIndex]["IdRubro"].S().I();

                mpeRubrosAgrega.Show();
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
                iIdRubro = gvRubros.DataKeys[iIndex]["IdRubro"].S().I();

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
        protected void txtPrefijo_TextChanged(object sender, EventArgs e)
        {
            sFiltroCuentas = txtPrefijo.Text.S();

            if (eGetCuentas != null)
                eGetCuentas(sender, e);
        }
        protected void ddlCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCuentas.Text = string.Empty;
            txtCuentas.Text = ddlCuentas.Text;

            string[] sValores = ddlCuentas.SelectedItem.Text.Split('-');
            if (sValores.Length >= 3)
            {
                if(sValores.Length == 3)
                    lblNombreCuenta.Text = sValores[2];
                else
                {
                    string sNameCuenta = string.Empty;
                    for (int i = 2; i < sValores.Length; i++)
                    {
                        if(sNameCuenta == string.Empty)
                            sNameCuenta += sValores[i];
                        else
                            sNameCuenta += "-" + sValores[i];
                    }

                    lblNombreCuenta.Text = sNameCuenta;
                }
            }
        }
        protected void btnAceptarCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (eNewCuenta != null)
                    eNewCuenta(sender, e);
                
                if (eSearchObj != null)
                    eSearchObj(sender, e);

                mpeCuentas.Hide();
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnCancelarCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                txtCuentas.Text = string.Empty;
                mpeCuentas.Hide();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region METODOS
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }
        public void LoadRubros(DataTable dt)
        {
            try
            {
                gvRubros.DataSource = dt;
                gvRubros.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LLenaComboCuentas(DataTable dt)
        {
            try
            {
                ddlCuentas.DataSource = dt;
                ddlCuentas.DataValueField = "AcctCode";
                ddlCuentas.DataTextField = "AcctName";
                ddlCuentas.DataBind();

                txtCuentas.Text = ddlCuentas.Text;
                string[] sValores = ddlCuentas.SelectedItem.Text.Split('-');
                if (sValores.Length >= 3)
                {
                    if (sValores.Length == 3)
                        lblNombreCuenta.Text = sValores[2];
                    else
                    {
                        string sNameCuenta = string.Empty;
                        for (int i = 2; i < sValores.Length; i++)
                        {
                            if (sNameCuenta == string.Empty)
                                sNameCuenta += sValores[i];
                            else
                                sNameCuenta += "-" + sValores[i];
                        }

                        lblNombreCuenta.Text = sNameCuenta;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        Rubros_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eNewCuenta;
        public event EventHandler eSearchCuentas;
        public event EventHandler eGetCuentas;
        public event EventHandler eSetEliminaCuenta;

        public int iIdRubro
        {
            get { return (int)ViewState["VSIdRubro"]; }
            set { ViewState["VSIdRubro"] = value; }
        }
        public object [] oArrRubroI
        {
            get
            {
                return new object[] {
                                        "@DescripcionRubro", txtRubro.Text.S(),
                                        "@TipoRubro", ddlTipoRubro.SelectedValue.S().I(),
                                        "@UsuarioCreacion", Utils.GetUser
                                    };
            }
        }
        public string sDescRubro
        {
            get
            {
                return txtRubro.Text.S();
            }
        }
        public object[] oArrRubroU
        {
            get
            {
                return new object[] {
                                        "@IdRubro", iIdRubro,
                                        "@DescripcionRubro", txtRubro.Text.S().ToUpper(),
                                        "@TipoRubro", ddlTipoRubro.SelectedValue.S().I(),
                                        "@UsuarioCreacion", Utils.GetUser
                                    };
            }
        }
        public string sDescCuenta
        {
            get { return txtCuentas.Text.S(); }
            
        }
        public DataTable dtCuentasRubro
        {
            get { return (DataTable)ViewState["VSCuentas"]; }
            set { ViewState["VSCuentas"] = value; }
        }
        public string sFiltroCuentas
        {
            get { return (string)ViewState["VSFiltroCuenta"]; }
            set { ViewState["VSFiltroCuenta"] = value; }
        }
        public int iIdCuenta
        {
            get { return (int)ViewState["VSIdCuenta"]; }
            set { ViewState["VSIdCuenta"] = value; }
        }
        public string sNombreCuenta
        {
            get { return lblNombreCuenta.Text.S(); }
        }
        #endregion
    }
}