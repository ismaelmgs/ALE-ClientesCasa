using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

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
using System.Xml;
using ClosedXML.Excel;
using System.Text;

namespace ClientesCasa.Views.CuentasPorPagar
{
    public partial class frmConsultaCargaPolizas : System.Web.UI.Page, IViewConsultaCargaPolizas
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsultaCargaPolizas_Presenter(this, new DBConsultaCargaPolizas());

            if (!IsPostBack) { }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("groupFechas");

                if (Page.IsValid) 
                {
                    if ((!string.IsNullOrEmpty(txtFechaInicio.Text) && !string.IsNullOrEmpty(txtFechaFinal.Text)))
                    {
                        if (txtFechaInicio.Text.Dt() < txtFechaFinal.Text.Dt())
                        {
                            msgError.Visible = false;
                            lblError.Text = string.Empty;
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                            ConsultaPolizas();
                        }
                        else
                        {
                            msgError.Visible = true;
                            lblError.Text = "Ingrese un período válido.";
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                        }
                    }
                    else
                    {
                        msgError.Visible = true;
                        lblError.Text = "Ingrese una fecha en los campos solicitados.";
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void imgVisualizar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void gvDatosCargas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = e.CommandArgument.S().I();

                if (e.CommandName == "Ver")
                {
                    long lgFolio = gvDatosCargas.Rows[index].Cells[0].Text.I();
                    string sArchivo = gvDatosCargas.Rows[index].Cells[1].Text.S();
                    string sFechaCarga = gvDatosCargas.Rows[index].Cells[5].Text.S();
                    lgIdFolio = lgFolio;
                    lblNombreArchivo.Text = sArchivo;
                    lblFechaCarga.Text = sFechaCarga;

                    if (eSearchXFolio != null)
                        eSearchXFolio(sender, e);

                    if (dtConsultaXFolio != null && dtConsultaXFolio.Rows.Count > 0)
                    {
                        pnlConsulta.Visible = true;
                        gvConsultaCarga.DataSource = dtConsultaXFolio;
                        gvConsultaCarga.DataBind();
                    }
                }
                else if (e.CommandName == "Descargar")
                {
                    long lgFolio = gvDatosCargas.Rows[index].Cells[0].Text.I();
                    lgIdFolio = lgFolio;

                    if (eSearchXFolio != null)
                        eSearchXFolio(sender, e);

                    if (dtConsultaXFolio != null && dtConsultaXFolio.Rows.Count > 0)
                    {
                        ExportaExcel();
                    }
                }
                else if (e.CommandName == "Procesar")
                {
                    long lgFolio = gvDatosCargas.Rows[index].Cells[0].Text.I();
                    lgIdFolio = lgFolio;

                    Response.Redirect("frmPolizaResult.aspx?IdFolio=" + lgFolio.S());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MÉTODOS

        protected void ConsultaPolizas() 
        {
            try
            {
                sFechaIni = string.Empty;
                sFechaFin = string.Empty;

                sFechaIni = txtFechaInicio.Text;
                sFechaFin = txtFechaFinal.Text;

                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadPolizas(DataTable dt)
        {
            try
            {
                dtConsultaPoliza = null;
                dtConsultaPoliza = dt;

                gvDatosCargas.DataSource = dtConsultaPoliza;
                gvDatosCargas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadPolizaXFolio(DataTable dt) 
        {
            try
            {
                dtConsultaXFolio = null;
                dtConsultaXFolio = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmConsultaCargaPolizas.aspx.cs";
        private const string sPagina = "frmConsultaCargaPolizas.aspx";

        ConsultaCargaPolizas_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchXFolio;

        public string sFechaIni
        {
            get { return (string)ViewState["VSFechaInicio"]; }
            set { ViewState["VSFechaInicio"] = value; }
        }
        public string sFechaFin
        {
            get { return (string)ViewState["VSFechaFinal"]; }
            set { ViewState["VSFechaFinal"] = value; }
        }

        public long lgIdFolio
        {
            get { return (long)ViewState["VSIdFolio"]; }
            set { ViewState["VSIdFolio"] = value; }
        }

        public DataTable dtConsultaPoliza
        {
            get { return (DataTable)ViewState["VSConsultaPoliza"]; }
            set { ViewState["VSConsultaPoliza"] = value; }
        }

        public DataTable dtConsultaXFolio
        {
            get { return (DataTable)ViewState["VSConsultaXFolio"]; }
            set { ViewState["VSConsultaXFolio"] = value; }
        }

        #endregion

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportaExcel();
        }

        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        public void ExportaExcel() 
        {
            string sDia = DateTime.Now.Day.S();
            string sMes = DateTime.Now.Month.S();
            string sAnio = DateTime.Now.Year.S();
            string sNameReport = sDia.PadLeft(2, '0') + sMes.PadLeft(2, '0') + sAnio;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtConsultaXFolio, "Customers");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

    }
}