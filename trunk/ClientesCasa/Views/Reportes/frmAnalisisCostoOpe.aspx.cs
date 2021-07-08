using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Presenter;
using ClientesCasa.Interfaces;
using ClientesCasa.DomainModel;
using System.Text;
using System.IO;
using System.Drawing;
using ClientesCasa.Clases;

namespace ClientesCasa.Views.Reportes
{
    public partial class frmAnalisisCostoOpe : System.Web.UI.Page, IViewAnalisisCostoOpe
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new AnalisisCostoOpe_Presenter(this, new DBAnalisisCostoOpe());

            //tdTotal.Style.Add("BORDER-BOTTOM", "#000000 1px solid");

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
                sMatricula = string.Empty;

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

                        //sNumCliente = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                        sNombrecliente = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
                        //sRazonSocial = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        //sRFC = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();
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
                    txtPeriodo.Text = string.Empty;
                    mpePeriodo.Show();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                //lblReqServicios.Text = txtServicios.Text != string.Empty ? txtServicios.Text.S().D().ToString("c") : "$0";
                //lblReqIva.Text = txtIva.Text != string.Empty ? txtIva.Text.S().D().ToString("c") : "$0";
                //lblReqTotal.Text = txtTotal.Text != string.Empty ? txtTotal.Text.S().D().ToString("c") : "$0";
                //lblReqGranTotal.Text = txtGranTotal.Text != string.Empty ? txtGranTotal.Text.S().D().ToString("c") : "$0";
                //lblReqEstimados.Text = txtEstimados.Text != string.Empty ? txtEstimados.Text.S().D().ToString("c") : "$0";
                //lblReqSaldoAnt.Text = txtSaldoAnt.Text != string.Empty ? txtSaldoAnt.Text.S().D().ToString("c") : "$0";
                //lblReqSaldoActual.Text = txtSaldoActual.Text != string.Empty ? txtSaldoActual.Text.S().D().ToString("c") : "$0";

                //CambiaTxtPorLabels(true);

                if (eSaveObj != null)
                    eSaveObj(sender, e);
                
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=EstadoCuenta_" + sMatricula + ".xls");
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
        protected void gvReporte_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvReporte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].Style.Add("BORDER-BOTTOM", "#000000 1px solid");
                //    }
                //}


                //if (e.Row.RowType == DataControlRowType.Footer)
                //{
                //    decimal Suma = 0;
                //    gvReporte.FooterStyle.ForeColor = Color.Black;
                //    gvReporte.FooterStyle.Font.Bold = true;

                //    for (int i = 1; i < dtGastos.Columns.Count; i++)
                //    {
                //        for (int j = 0; j < dtGastos.Rows.Count; j++)
                //        {
                //            Suma += dtGastos.Rows[j][i].S().Replace("$", "").Replace(",", "").S().D();
                //        }

                //        e.Row.Cells[i].Text = "$" + Suma.S();
                //        dTotlaGrid = Suma;
                //        Suma = 0;
                //    }

                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].Style.Add("BORDER-TOP", "#000000 1px solid");
                //    }

                //    upaReporte.Update();
                //}
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnAceptarPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                string[] speriodo = txtPeriodo.Text.S().Split('/');

                if (speriodo.Length == 1)
                    speriodo = txtPeriodo.Text.S().Split('-');

                iMes = speriodo[1].S().I();
                iAnio = speriodo[0].S().I();

                if (eObjSelected != null)
                    eObjSelected(sender, e);


                pnlGastosPesos.InnerHtml = sHTML;
                pnlGastosUSD.InnerHtml = sHTML_USD;

                lblTotalMXN.Text = dTotalPesos.ToString("c");
                lblTotalUSD.Text = dTotalUSD.ToString("c");

                //gvGastosMXN.DataSource = dsGastos.Tables[0];
                //gvGastosMXN.DataBind();

                //gvGastosUSD.DataSource = dsGastos.Tables[1];
                //gvGastosUSD.DataBind();

                lblNombreCliente.Text = sNombrecliente;
                lblMatricula.Text = sMatricula;
                lblContrato.Text = sClaveContrato;
                lblElaboro.Text = Utils.GetUserName;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //lblDireccionCliente.Text = dsGastos.Tables[2].Rows[0]["DireccionCliente"].S();

                if (eSearchTotales != null)
                    eSearchTotales(sender, e);

                if (dtTotal != null && dtTotal.Rows.Count > 0)
                {
                    lblRespSaldoAntMXP.Text = dtTotal.Rows[0]["SaldoAnterior"].S().D().ToString("c");
                    lblRespPagCreditMXP.Text = dtTotal.Rows[0]["PagosCreditos"].S().D().ToString("c");
                    lblRespNuevosCargosMXP.Text = dtTotal.Rows[0]["NuevosCargos"].S().D().ToString("c");
                    lblRespSaldoActMXP.Text = dtTotal.Rows[0]["SaldoActual"].S().D().ToString("c");

                    lblRespSaldoAntUSD.Text = dtTotal.Rows[1]["SaldoAnterior"].S().D().ToString("c");
                    lblRespPagCreditUSD.Text = dtTotal.Rows[1]["PagosCreditos"].S().D().ToString("c");
                    lblRespNuevosCargosUSD.Text = dtTotal.Rows[1]["NuevosCargos"].S().D().ToString("c");
                    lblRespSaldoActUSD.Text = dtTotal.Rows[1]["SaldoActual"].S().D().ToString("c");
                }

                pnlReporte.Visible = true;
                btnGenerar.Visible = true;
                //upaReporte.Update();

                mpePeriodo.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        decimal dSumaImporte = 0;
        protected void gvGastosMXN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dSumaImporte += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"));
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[7].Text = dSumaImporte.ToString("c");
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].Font.Bold = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        decimal dSumaImporteUSD = 0;
        protected void gvGastosUSD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dSumaImporteUSD += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"));
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[7].Text = dSumaImporteUSD.ToString("c");
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].Font.Bold = true;
                }
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
        #endregion

        #region VARIABLES Y PROPIEDADES
        AnalisisCostoOpe_Presenter oPresenter;
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
        public decimal dTotlaGrid
        {
            get { return (decimal)ViewState["VTotalGrid"]; }
            set { ViewState["VTotalGrid"] = value; }
        }
        public int iMes
        {
            get { return (int)ViewState["ViMes"]; }
            set { ViewState["ViMes"] = value; }
        }
        public int iAnio
        {
            get { return (int)ViewState["ViAnio"]; }
            set { ViewState["ViAnio"] = value; }
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
        public string sHTML
        {
            get { return (string)ViewState["VSHTML"]; }
            set { ViewState["VSHTML"] = value; }
        }
        public string sHTML_USD
        {
            get { return (string)ViewState["VSHTML_USD"]; }
            set { ViewState["VSHTML_USD"] = value; }
        }
        public decimal dTotalPesos
        {
            get { return (decimal)ViewState["VTotalPesos"]; }
            set { ViewState["VTotalPesos"] = value; }
        }
        public decimal dTotalUSD
        {
            get { return (decimal)ViewState["VTotalUSD"]; }
            set { ViewState["VTotalUSD"] = value; }
        }

        #endregion

        
    }
}