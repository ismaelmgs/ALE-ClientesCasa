using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Drawing;
using ClientesCasa.Clases;
using ClientesCasa.Objetos;

namespace ClientesCasa.Views.Principales
{
    public partial class frmMttoPagos : System.Web.UI.Page, IViewMttoPagos
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new MttoPagos_Presenter(this, new DBMttoPagos());

            if (!IsPostBack)
            {
                
            }

            if (Request[txtPeriodo.UniqueID] != null)
            {
                if (Request[txtPeriodo.UniqueID].Length > 0)
                {
                    txtPeriodo.Text = Request[txtPeriodo.UniqueID];
                }
            }
        }
        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes.Add("title", "De clic aqui para selecciona un cliente");
                        e.Row.Attributes.Add("OnMouseOver", "On(this);");
                        e.Row.Attributes.Add("OnMouseOut", "Off(this);");
                        e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gvClientes, "Select$" + e.Row.RowIndex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dtClientes;
                gvClientes.PageIndex = e.NewPageIndex;
                LLenaClientes(dt);
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sNumClienteg = string.Empty;
                string sNombreClienteg = string.Empty;
                string sIdContratog = string.Empty;
                string sMatriculag = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#D9E1E4");
                        row.ToolTip = string.Empty;

                        sNumClienteg = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                        sNombreClienteg = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
                        sIdContratog = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        sMatriculag = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();

                        ban = true;
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                        row.ToolTip = "Clic para seleccionar esta fila.";
                    }
                }

                if (ban)
                {
                    lblRespCustNum.Text = sNumClienteg;
                    lblRespNombreCliente.Text = sNombreClienteg;
                    lblRespMatricula.Text = sMatricula;

                    sClaveCliente = sNumClienteg;
                    sClaveContrato = sIdContratog;
                    sMatricula = sMatriculag;

                    txtPeriodo.Text = string.Empty;
                    mpePeriodo.Show();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnAceptarPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPeriodo.Text.S() != string.Empty)
                {
                    lblValPeriodo.Text = string.Empty;
                    lblValPeriodo.Visible = false;

                    string[] sPeriodo = txtPeriodo.Text.S().Split('/');

                    if (sPeriodo.Length == 1)
                        sPeriodo = txtPeriodo.Text.S().Split('-');

                    iMes = sPeriodo[1].S().I();
                    iAnio = sPeriodo[0].S().I();

                    pnlFacturantes.Visible = true;

                    string sMes = ObtieneNombreMes(iMes);
                    lblReqMes.Text = sMes;
                    lblReqAnio.Text = iAnio.S();


                    LlenaPagos();
                    LlenaddlFacturante();

                    btnActualizar.Visible = true;
                    mpePeriodo.Hide();
                }
                else
                {
                    lblValPeriodo.Text = "El campo es requerido";
                    lblValPeriodo.Visible = true;
                    mpePeriodo.Show();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eUpaListaPagos != null)
                    eUpaListaPagos(sender, e);

                if (eSearchPagos != null)
                    eSearchPagos(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnEstimados_Click(object sender, EventArgs e)
        {
            try
            {
                LlenaddlFacturante();
                if (ddlFacturante.Items.Count > 0)
                {
                    LimpiaControlesModal();
                    mpeEstimados.Show();
                }
                else
                {
                    MostrarMensaje("No se han asociado facturantes al cliente, favor de verificar.", "Aviso");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                foreach (GridViewRow gvr in gvFacturantes.Rows)
                {
                    iIdPago = gvFacturantes.DataKeys[gvr.RowIndex]["IdPago"].S().I();
                    
                    TextBox txtImporteN = (TextBox)gvFacturantes.Rows[gvr.RowIndex].FindControl("txtImporteNuevo");
                    if (txtImporteN != null)
                    {
                        dMonto = txtImporteN.Text.Replace(",","").S().D();

                        if (eUpaMontoPago != null)
                            eUpaMontoPago(sender, e);

                    }
                }

                MostrarMensaje("Los pagos se actualizaron de manera correcta.", "Aviso");
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvFacturantes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataTable dt = dsPagos.Tables[0];

                    ImageButton btnEliminar = (ImageButton)e.Row.FindControl("btnEliminar");
                    if (btnEliminar != null)
                    {
                        btnEliminar.Visible = dt.Rows[e.Row.RowIndex]["IdTipoPago"].S() == "2" ? true : false;
                    }

                    if (dt.Rows[e.Row.RowIndex]["ImporteNuevo"].S().D() < 0)
                    {
                        e.Row.BackColor = System.Drawing.Color.Beige;
                    }
                }

            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvFacturantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    iIdPago = gvFacturantes.DataKeys[e.CommandArgument.S().I()]["IdPago"].S().I();
                    dMonto = 0;

                    if (eDeleteObj != null)
                        eDeleteObj(sender, e);

                    if (eSearchPagos != null)
                        eSearchPagos(sender, e);

                    gvFacturantes.DataSource = dsPagos.Tables[0];
                    gvFacturantes.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void ddlFacturante_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeEstimados.Show();
        }
        protected void btnAceptarEstimado_Click(object sender, EventArgs e)
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(sender, e);

                LlenaPagos();

                mpeEstimados.Hide();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region METODOS
        public void LLenaClientes(DataTable dt)
        {
            try
            {
                gvClientes.DataSource = dt;
                gvClientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ObtieneNombreMes(int iMes)
        {
            string sMes = string.Empty;
            switch (iMes)
            {
                case 1:
                    sMes = "Enero";
                    break;
                case 2:
                    sMes = "Febrero";
                    break;
                case 3:
                    sMes = "Marzo";
                    break;
                case 4:
                    sMes = "Abril";
                    break;
                case 5:
                    sMes = "Mayo";
                    break;
                case 6:
                    sMes = "Junio";
                    break;
                case 7:
                    sMes = "Julio";
                    break;
                case 8:
                    sMes = "Agosto";
                    break;
                case 9:
                    sMes = "Septiembre";
                    break;
                case 10:
                    sMes = "Octubre";
                    break;
                case 11:
                    sMes = "Noviembre";
                    break;
                case 12:
                    sMes = "Diciembre";
                    break;
            }

            return sMes;
        }
        private void LimpiaControlesModal()
        {
            try
            {
                ddlFacturante.SelectedIndex = 0;
                txtReferencia.Text = string.Empty;
                txtImporte.Text = string.Empty;
                ddlFacturante_SelectedIndexChanged(null, EventArgs.Empty);
                txtFechaP.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LlenaPagos()
        {
            try
            {
                if (eSearchPagos != null)
                    eSearchPagos(null, EventArgs.Empty);
                
                if (dsPagos.Tables[0].Rows.Count > 0)
                {
                    gvFacturantes.DataSource = dsPagos.Tables[0];
                    gvFacturantes.DataBind();
                }
                else
                {
                    if (eUpaListaPagos != null)
                        eUpaListaPagos(null, EventArgs.Empty);

                    if (eSearchPagos != null)
                        eSearchPagos(null, EventArgs.Empty);

                    gvFacturantes.DataSource = dsPagos.Tables[0];
                    gvFacturantes.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LlenaddlFacturante()
        {
            DataTable dt = dsPagos.Tables[1];

            if (dt.Rows.Count > 0)
            {
                ddlFacturante.DataSource = dt.DefaultView;
                ddlFacturante.DataTextField = dt.Columns[0].ToString();
                ddlFacturante.DataValueField = dt.Columns[0].ToString();
                ddlFacturante.DataBind();
            }

        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        MttoPagos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchPagos;
        public event EventHandler eUpaListaPagos;
        public event EventHandler eUpaMontoPago;

        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["VClientes"]; }
            set { ViewState["VClientes"] = value; }
        }
        public object[] oArray
        {
            get
            {
                string sEstatus = string.Empty;
                string sClaveContrato = string.Empty;
                string sClaveCliente = string.Empty;
                string sMatricula = string.Empty;

                switch (ddlOpcBus.SelectedValue.S())
                {
                    case "0":
                    case "1":
                        sEstatus = ddlOpcBus.SelectedValue.S();
                        break;
                    case "2":
                        sEstatus = "2";
                        sClaveCliente = txtBusqueda.Text.S();
                        break;
                    case "3":
                        sEstatus = "2";
                        sClaveContrato = txtBusqueda.Text.S();
                        break;
                    case "4":
                        sEstatus = "2";
                        sMatricula = txtBusqueda.Text.S();
                        break;
                }

                return new object[]
                {

                    "@ClaveCliente", sClaveCliente,
                    "@ClaveContrato", sClaveContrato,
                    "@Estatus", sEstatus,
                    "@Matricula", sMatricula
                };
            }
        }
        public string sClaveCliente
        {
            get { return (string)ViewState["VSClaveCliente"]; }
            set { ViewState["VSClaveCliente"] = value; }
        }
        public string sClaveContrato
        {
            get { return (string)ViewState["VSClaveContrato"]; }
            set { ViewState["VSClaveContrato"] = value; }
        }
        public int iIdCliente
        {
            get { return (int)ViewState["VSIdCliente"]; }
            set { ViewState["VSIdCliente"] = value; }
        }
        public int iMes
        {
            get { return (int)ViewState["VSMes"]; }
            set { ViewState["VSMes"] = value; }
        }
        public int iAnio
        {
            get { return (int)ViewState["VSAnio"]; }
            set { ViewState["VSAnio"] = value; }
        }
        public DataSet dsPagos
        {
            get { return (DataSet)ViewState["VSPagos"]; }
            set { ViewState["VSPagos"] = value; }
        }
        public int iIdPago
        {
            get { return (int)ViewState["VSIdPago"]; }
            set { ViewState["VSIdPago"] = value; }
        }
        public decimal dMonto
        {
            get { return (decimal)ViewState["VSdMonto"]; }
            set { ViewState["VSdMonto"] = value; }
        }
        public string sMatricula
        {
            get { return (string)ViewState["VSMatricula"]; }
            set { ViewState["VSMatricula"] = value; }
        }
        public PagoEstimado oPagoEstimado
        {
            get
            {
                PagoEstimado oPago = new PagoEstimado();
                oPago.sClaveCliente = sClaveCliente;
                oPago.sClaveContrato = sClaveContrato;
                oPago.sClaveFacturante = ddlFacturante.SelectedValue.S();
                //oPago.sFacturante = "";
                oPago.dImporteNvo = txtImporte.Text.S().D();
                oPago.iMes = iMes;
                oPago.iAnio = iAnio;
                oPago.sDocNum = txtReferencia.Text.S();
                oPago.dtDocDate = txtFechaP.Text.S().Dt();
                oPago.sMoneda = ddlTipoMoneda.SelectedValue.S();
                oPago.sUsuario = Utils.GetUser;

                return oPago;
            }
        }
        #endregion

        protected void gvFacturantes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataTable dt = dsPagos.Tables[0];

                    if (dt.Rows[e.Row.RowIndex]["ImporteNuevo"].S().D() < 0)
                    {
                        e.Row.BackColor = System.Drawing.Color.Beige;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}