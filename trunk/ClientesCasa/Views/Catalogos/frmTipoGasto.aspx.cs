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
    public partial class frmTipoGasto : System.Web.UI.Page, IViewTipoGasto
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new TipoGasto_Presenter(this, new DBTipoGasto());

            if (!IsPostBack)
            {
                //iIdRubro = 0;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }
        protected void btnAgregarTipoG_Click(object sender, EventArgs e)
        {
            iIdConcepto = 0;
            lblTituloTipoG.Text = "Alta de Tipo de Gasto";
            txtTipoG.Text = string.Empty;
            mpeTipoGAgrega.Show();
        }

        protected void btnAceptarTipoG_Click(object sender, EventArgs e)
        {
            try
            {
                sDescConcepto = txtTipoG.Text;
                if (iIdConcepto == 0)
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else if (iIdConcepto > 0)
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                mpeTipoGAgrega.Hide();
            }
            catch (Exception ex)
            {

            }
        }

        protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblTituloTipoG.Text = "Edición de Tipo de Gasto";

                GridViewRow Row = ((ImageButton)sender).NamingContainer as GridViewRow;
                txtTipoG.Text = Row.Cells[0].Text.S();
                iIdConcepto = gvTipoG.DataKeys[Row.RowIndex]["IdConcepto"].S().I();

                mpeTipoGAgrega.Show();
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
                iIdConcepto = gvTipoG.DataKeys[iIndex]["IdConcepto"].S().I();

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

        protected void gvTipoG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                txtTipoG.Text = string.Empty;
                mpeTipoGAgrega.Hide();
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvTipoG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = dtTipoGastos;
                gvTipoG.PageIndex = e.NewPageIndex;
                LoadTipoGasto(dt);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
        #region METODOS

        public void LoadTipoGasto(DataTable dt)
        {
            try
            {
                dtTipoGastos = dt;
                gvTipoG.DataSource = dt;
                gvTipoG.DataBind();
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

        TipoGasto_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public DataTable dtTipoGastos
        {
            get { return (DataTable)ViewState["VdtTipoGastos"]; }
            set { ViewState["VdtTipoGastos"] = value; }
        }

        public int iIdConcepto
        {
            get { return (int)ViewState["VSIdConcepto"]; }
            set { ViewState["VSIdConcepto"] = value; }
        }

        public string sDescConcepto
        {
            get { return (string)ViewState["VSDescConcepto"]; }
            set { ViewState["VSDescConcepto"] = value; }
        }

        public object[] oArrTipoG
        {
            get
            {
                return new object[] {
                                        "@IdConcepto", iIdConcepto,
                                        "@DescConcepto", txtTipoG.Text,
                                        "@UsuarioCreacion", Utils.GetUser
                                    };
            }
        }

        #endregion
    }
}