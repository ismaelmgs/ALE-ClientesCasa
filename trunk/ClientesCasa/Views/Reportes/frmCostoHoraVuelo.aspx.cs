using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Presenter;
using ClientesCasa.DomainModel;
using System.Drawing;
using System.IO;
using System.Text;
using ClientesCasa.Clases;

namespace ClientesCasa.Views.Reportes
{
    public partial class frmCostoHoraVuelo : System.Web.UI.Page, IViewCostoHoraVuelo
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new CostoHoraVuelo_Presenter(this, new DBCostoHoraVuelo());

            //if (Request[txtFechaInicio.UniqueID] != null)
            //{
            //    if (Request[txtFechaInicio.UniqueID].Length > 0)
            //    {
            //        txtFechaInicio.Text = Request[txtFechaInicio.UniqueID];
            //    }
            //}

            //if (Request[txtFechaFin.UniqueID] != null)
            //{
            //    if (Request[txtFechaFin.UniqueID].Length > 0)
            //    {
            //        txtFechaFin.Text = Request[txtFechaFin.UniqueID];
            //    }
            //}

            if (!IsPostBack)
            {
                LlenaAnios();
            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                sMatricula = string.Empty;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sNumCliente = string.Empty;
                string sRazonSocial = string.Empty;
                string sRFC = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#D9E1E4");
                        row.ToolTip = string.Empty;
                        sMatricula = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();
                        sClaveContrato = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();

                        sNombrecliente = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
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
                    ddlMesFinal.SelectedIndex = 0;
                    ddlAnio.SelectedIndex = 0;
                    mpePeriodo.Show();
                }
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

        protected void btnAceptarPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtFechaInicio.Text == string.Empty)
                //    lblErrorInicio.Visible = true;
                //else
                //    lblErrorInicio.Visible = false;

                
                //if (txtFechaFin.Text == string.Empty)
                //    lblErrorFin.Visible = true;
                //else
                //    lblErrorFin.Visible = false;

                //if (lblErrorInicio.Visible || lblErrorFin.Visible)
                //    mpePeriodo.Show();

                //dtInicio = txtFechaInicio.Text.S().Dt();
                //dtFin = txtFechaFin.Text.S().Dt();

                dtInicio = new DateTime(ddlAnio.SelectedValue.S().I(), 1, 1);
                
                if (chkAnioCompleto.Checked)
                {
                    dtFin = new DateTime(ddlAnio.SelectedValue.S().I(), 12, 31);
                }
                else
                {
                    if (ddlMesFinal.SelectedValue == "12")
                        dtFin = new DateTime(ddlAnio.SelectedValue.S().I(), 12, 31);
                    else
                    {
                        DateTime dtAux = new DateTime(ddlAnio.SelectedValue.S().I(), ddlMesFinal.SelectedValue.S().I(), 1);
                        dtAux = dtAux.AddMonths(1);
                        dtFin = dtAux.AddDays(-1);
                    }
                }

                sMoneda = ddlMonedaReporte.SelectedValue.S();
                lblMatricula.Text = sMatricula;
                lblMoneda.Text = ddlMonedaReporte.SelectedValue.S();
                lblPeriodo.Text = ObtieneNombreMes(dtInicio.Month) + "-" + ObtieneNombreMes(dtFin.Month);

                if (eObjSelected != null)
                    eObjSelected(sender, e);


                lblPromedioFijo.Text = (dTotalImporteFijo / 12).ToString("c");
                lblPromedioVar.Text = (dTotalImporteVar / 12).ToString("c");

                pnlReporte.Visible = true;
                btnGenerar.Visible = true;

                lblElaboro.Text = "Elaboró: " + Utils.GetUserName;
                lblFechaReporte.Text = DateTime.Now.ToLongDateString();

                gvMeses.DataSource = dtTotalesTiempo;
                gvMeses.DataBind();

                string sTiempoTotal = dtTotalesTiempo.Rows[0]["Total"].S();
                float fTiempo = Utils.ConvierteTiempoaDecimal(sTiempoTotal);

                string sTiempoFijo = Utils.ConvierteDecimalATiempo(fTiempo.S().D() / 12);
                lblPromedioVoladasFijo.Text = sTiempoFijo;
                lblPromedioVoladasVar.Text = sTiempoFijo;

                lblCostoVueloFijo.Text = ((dTotalImporteFijo / 12) / fTiempo.S().D()).ToString("c");
                lblCostoVueloVar.Text = ((dTotalImporteVar / 12) / fTiempo.S().D()).ToString("c");

                pnlGastosPesos.InnerHtml = sHTML;
            }
            catch (Exception ex)
            {    
                throw ex;
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=CostoHoraVuelo_" + sMatricula + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                this.EnableViewState = false;

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                pnlReporte.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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
                    sMes = "ENERO";
                    break;
                case 2:
                    sMes = "FEBRERO";
                    break;
                case 3:
                    sMes = "MARZO";
                    break;
                case 4:
                    sMes = "ABRIL";
                    break;
                case 5:
                    sMes = "MAYO";
                    break;
                case 6:
                    sMes = "JUNIO";
                    break;
                case 7:
                    sMes = "JULIO";
                    break;
                case 8:
                    sMes = "AGOSTO";
                    break;
                case 9:
                    sMes = "SEPTIEMBRE";
                    break;
                case 10:
                    sMes = "OCTUBRE";
                    break;
                case 11:
                    sMes = "NOVIEMBRE";
                    break;
                case 12:
                    sMes = "DICIEMBRE";
                    break;
            }

            return sMes;
        }

        private void LlenaAnios()
        {
            try
            {
                dtAnios = new DataTable();
                dtAnios.Columns.Add("Anio");

                int iAnio = DateTime.Now.Year;

                for (int i = 0; i < 4; i++)
                {
                    DataRow row = dtAnios.NewRow();
                    row["Anio"] = DateTime.Now.Year - i;

                    dtAnios.Rows.Add(row);
                }

                ddlAnio.DataSource = dtAnios;
                ddlAnio.DataValueField = "Anio";
                ddlAnio.DataTextField = "Anio";
                ddlAnio.DataBind();


                DataTable dtMeses = new DataTable();
                dtMeses.Columns.Add("Mes");
                dtMeses.Columns.Add("DescMes");

                for (int i = 1; i <= 12; i++)
                {
                    DataRow row = dtMeses.NewRow();
                    row["Mes"] = i.ToString();
                    row["DescMes"] = ObtieneNombreMes(i);

                    dtMeses.Rows.Add(row);
                }

                ddlMesFinal.DataSource = dtMeses;
                ddlMesFinal.DataValueField = "Mes";
                ddlMesFinal.DataTextField = "DescMes";
                ddlMesFinal.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        CostoHoraVuelo_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchTotales;

        public DataSet dsGastos
        {
            get { return (DataSet)ViewState["VdtGastos"]; }
            set { ViewState["VdtGastos"] = value; }
        }
        public object[] oArrFiltros
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
        public decimal dTipoCambio
        {
            set 
            {
                lblTipoCambioProm.Text = value.S();
            }
        }
        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["VClientes"]; }
            set { ViewState["VClientes"] = value; }
        }
        public string sMatricula
        {
            get { return (string)ViewState["VMatricula"]; }
            set { ViewState["VMatricula"] = value; }
        }
        public string sClaveContrato
        {
            get { return (string)ViewState["VContrato"]; }
            set { ViewState["VContrato"] = value; }
        }
        public DateTime dtInicio
        {
            get { return (DateTime)ViewState["VSdtInicio"]; }
            set { ViewState["VSdtInicio"] = value; }
        }
        public DateTime dtFin
        {
            get { return (DateTime)ViewState["VSdtFin"]; }
            set { ViewState["VSdtFin"] = value; }
        }
        public DataTable dtTotal
        {
            get { return (DataTable)ViewState["VSdtTotal"]; }
            set { ViewState["VSdtTotal"] = value; }
        }
        public string sNombrecliente
        {
            get { return (string)ViewState["VSNombreCliente"]; }
            set { ViewState["VSNombreCliente"] = value; }
        }
        public string sMoneda
        {
            get { return (string)ViewState["VSMoneda"]; }
            set { ViewState["VSMoneda"] = value; }
        }
        public string sHTML
        {
            get { return (string)ViewState["VSHTML"]; }
            set { ViewState["VSHTML"] = value; }
        }
        public DataTable dtTotalesTiempo
        {
            get { return (DataTable)ViewState["VSTotalesTiempo"]; }
            set { ViewState["VSTotalesTiempo"] = value; }
        }
        public DataTable dtAnios
        {
            get { return (DataTable)ViewState["VSAnios"]; }
            set { ViewState["VSAnios"] = value; }
        }

        public decimal dTotalImporteFijo
        {
            get { return (decimal)ViewState["VSdTotalImporteFijo"]; }
            set { ViewState["VSdTotalImporteFijo"] = value; }
        }
        public decimal dTotalImporteVar
        {
            get { return (decimal)ViewState["VSdTotalImporteVar"]; }
            set { ViewState["VSdTotalImporteVar"] = value; }
        }
        #endregion

        protected void chkAnioCompleto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAnioCompleto.Checked)
                {
                    lblMesFinal.Visible = false;
                    ddlMesFinal.Visible = false;
                }
                else
                {
                    lblMesFinal.Visible = true;
                    ddlMesFinal.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}