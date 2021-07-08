using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using ClientesCasa.Presenter;
using NucleoBase.Core;

namespace ClientesCasa.Views.CuentasPorPagar
{
    public partial class frmPolizaResult : System.Web.UI.Page, IViewResultNomina
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ResultadoNomina_Presenter(this, new DBResultadoNomina());

            if (!IsPostBack)
            {
                oLstPoliza = new List<PolizaNominaSAP>();

                if (Request.QueryString["IdFolio"] != null)
                {
                    iIdFolio = Request.QueryString["IdFolio"].S().I();

                    if (eSearchObj != null)
                        eSearchObj(sender, e);
                }
            }

            fuXML.Attributes.Add("onchange", "return checkFileExtension(this);");
            fuPDF.Attributes.Add("onchange", "return checkFileExtensionPDF(this);");
        }

        //protected void gvArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            GridView gvDetalle = (GridView)e.Row.FindControl("gvDetalle");
        //            if (gvDetalle != null)
        //            {
        //                DataTable dtDetalle = dsDetalleNomina.Tables[1].Copy();
        //                dtDetalle.Columns.Add("CardCode");

        //                foreach (DataRow row in dtDetalle.Rows)
        //                {
        //                    string sCardCodeEmpresa = new DBAccesoSAP().DBGetCardCodePorCardName(row["Empresa"].S());
        //                    row["CardCode"] = sCardCodeEmpresa;
        //                }

        //                dtDetalle.AcceptChanges();
                        
        //                gvDetalle.DataSource = dtDetalle;
        //                gvDetalle.DataBind();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (eNewObj != null)
                eNewObj(sender, e);
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idx = e.CommandArgument.S().I();
                sEmpresa = dsDetalleNomina.Tables[1].Rows[idx]["Empresa"].S();
                iNoFact = dsDetalleNomina.Tables[1].Rows[idx]["NumeroFactura"].S().I();
                
                if (e.CommandName == "Detalle")
                {
                    pnlEmpresas.Visible = false;
                    pnlDetEmpresas.Visible = true;

                    lblRespNoFactura.Text = iNoFact.S();
                    lblRespCardCode.Text = new DBAccesoSAP().DBGetCardCodePorCardName(dsDetalleNomina.Tables[1].Rows[idx]["Empresa"].S());
                    lblRespNombreEmpresa.Text = dsDetalleNomina.Tables[1].Rows[idx]["Empresa"].S();

                    if (eObjSelected != null)
                        eObjSelected(sender, e);
                }

                if (e.CommandName == "Aprobar")
                {
                    sEmpresa = dsDetalleNomina.Tables[1].Rows[idx]["Empresa"].S();
                    iNoFact = dsDetalleNomina.Tables[1].Rows[idx]["NumeroFactura"].S().I();

                    if (eValidaPDFFile != null)
                    {
                        eValidaPDFFile(sender, e);
                        if (iExistePDF == 1)
                        {
                            if (eValidaXMLFile != null)
                            {
                                eValidaXMLFile(sender, e);
                                if (iExisteXML == 1)
                                {
                                    if (eUpdApruebaFactura != null)
                                        eUpdApruebaFactura(sender, e);
                                }
                                else
                                    MostrarMensaje("Se deben cargar los archivos PDF y XML antes de aprobar la factura", "Aviso");
                            }
                            
                        }
                        else
                            MostrarMensaje("Se deben cargar los archivos PDF y XML antes de aprobar la factura", "Aviso");
                    }

                    
                }
                
                if (e.CommandName == "UploadXML")
                {
                    mpeXML.Show();
                }

                if (e.CommandName == "UploadPDF")
                {
                    mpePDF.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvEmpleadosEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int Idx = e.CommandArgument.S().I();
                string sRFCEmp = gvEmpleadosEmpresa.Rows[Idx].Cells[3].Text.S();

                if (e.CommandName == "Excel")
                {
                    DataSet ds = new DBResultadoNomina().DBGetObtieneCalculoPolizaNomina(iIdFolio, sRFCEmp, lblRespNombreEmpresa.Text.S(),lblRespNoFactura.Text.S().I());
                    GridView gv = new GridView();

                    DataTable dtFinal = ds.Tables[2].Copy();
                    dtFinal.Merge(ds.Tables[3]);

                    gv.DataSource = dtFinal;
                    gv.DataBind();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment;filename=Poliza_" + sRFCEmp + ".xls");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    this.EnableViewState = false;

                    StringWriter stringWrite = new StringWriter();
                    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    gv.RenderControl(htmlWrite);
                    Response.Write(stringWrite.ToString());
                    Response.End();
                }

                if (e.CommandName == "SAP")
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                sEmpresa = dsDetalleNomina.Tables[1].Rows[e.Row.RowIndex]["Empresa"].S();
                iNoFact = dsDetalleNomina.Tables[1].Rows[e.Row.RowIndex]["NumeroFactura"].S().I(); 
               // iNoFact = 

                if ((e.Row.Cells[11].Text == "-1") || (e.Row.Cells[11].Text == "0"))
                {
                    Button btn = (Button) e.Row.FindControl("btnAprobar");
                    if (btn != null)
                        btn.Enabled = true;
                }

                ImageButton ibtnPDF = (ImageButton)e.Row.FindControl("imbPDF");
                ImageButton ibtnXML = (ImageButton)e.Row.FindControl("imbModal");
                if (eValidaPDFFile != null)
                {
                    eValidaPDFFile(sender, e);
                    if(iExistePDF == 1)
                        ibtnPDF.ImageUrl = "../../Images/icons/pdf_2.png";
                }

                if (eValidaXMLFile != null)
                {
                    eValidaXMLFile(sender, e);
                    if (iExisteXML == 1)
                        ibtnXML.ImageUrl = "../../Images/icons/xml_2.png";
                }
            }
        }

        protected void btnRegresar1_Click(object sender, EventArgs e)
        {
            pnlEmpresas.Visible = true;
            pnlDetEmpresas.Visible = false;
            
        }

        protected void btnExportarUsu_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=Poliza_" + lblRespCardCode.Text + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            pnlDetEmpresas.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        protected void btnAceptarXML_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuXML.HasFile)
                {
                    if ((System.IO.Path.GetExtension(fuXML.FileName) == ".xml"))
                    {
                        sFileName = fuXML.FileName;
                        oArrFile = fuXML.FileBytes;

                        if (eSaveXMLFile != null)
                            eSaveXMLFile(sender, e);

                        mpeXML.Hide();
                    }
                    else
                    {
                        MostrarMensaje("Por favor, seleccione un archivo tipo XML", "Aviso");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAceptarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuPDF.HasFile)
                {
                    if ((System.IO.Path.GetExtension(fuPDF.FileName) == ".pdf"))
                    {
                        sFileName = fuPDF.FileName;
                        oArrFile = fuPDF.FileBytes;

                        if (eSavePDFFile != null)
                            eSavePDFFile(sender, e);

                        mpePDF.Hide();
                    }
                    else
                    {
                        MostrarMensaje("Por favor, seleccione un archivo tipo XML", "Aviso");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region METODOS
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }

        public void LoadParametros(DataTable dt)
        {
            try
            {
                DataRow[] rows = dt.Select("Clave = '1003'");
                if (rows != null && rows.Length > 0)
                {
                    sTaxCode = rows[0]["Valor"].S();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadDetalleNomina()
        {
            lblTituloInfo.Text = "Información de " + dsDetalleNomina.Tables[0].Rows[0]["Mes"].S() + " " + dsDetalleNomina.Tables[0].Rows[0]["Anio"].S();
            lblFechaCarga.Text = dsDetalleNomina.Tables[0].Rows[0]["FechaCreacion"].S().Dt().ToLongDateString();
            lblUsuarioCarga.Text = dsDetalleNomina.Tables[0].Rows[0]["UsuarioCreacion"].S();
            lblEstatusCarga.Text = dsDetalleNomina.Tables[0].Rows[0]["Procesado"].S();


            gvDetalle.DataSource = dsDetalleNomina.Tables[1];
            gvDetalle.DataBind();
        }

        public void ArmaListadoFacturas(string sEmpresa, List<ConceptosSAP> olst)
        {
            try
            {
                //foreach (DataRow row in dsDetalleNomina.Tables[1].Rows)
                //{
                    PolizaNominaSAP oPol = new PolizaNominaSAP();
                    oPol.sEmpresa = sEmpresa.S();
                    oPol.sSucursal = Helpers.sSucursal;
                    oPol.dtFecha = DateTime.Now; //dsDetalleNomina.Tables[0].Rows[0]["FechaInicio"].S().Dt();
                    string sCardCodeEmpresa = new DBAccesoSAP().DBGetCardCodePorCardName(sEmpresa.S());
                    oPol.sCardCodeEmpresa = sCardCodeEmpresa;
                    oPol.sNoReporte = dsDetalleNomina.Tables[0].Rows[0]["NombreArchivo"].S();
                    oPol.sMoneda = "MXN";
                    oPol.sComentarios = string.Empty;
                    oPol.dDescuento = 0;
                    oPol.sSerie = Helpers.sSerie;

                    //if(sCardCodeEmpresa != string.Empty)
                    //    oLstPoliza.Add(oPol);

                    oPol.oLstConceptos = olst;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaEmpleadosPorEmpresa(DataTable dt)
        {
            try
            {
                gvEmpleadosEmpresa.DataSource = dt;
                gvEmpleadosEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtieneNombreMes(int iMes)
        {
            string smes = string.Empty;

            switch (iMes)
            {
                case 1:
                    smes = "Enero";
                    break;
                case 2:
                    smes = "Febrero";
                    break;
                case 3:
                    smes = "Marzo";
                    break;
                case 4:
                    smes = "Abril";
                    break;
                case 5:
                    smes = "Mayo";
                    break;
                case 6:
                    smes = "Junio";
                    break;
                case 7:
                    smes = "Julio";
                    break;
                case 8:
                    smes = "Agosto";
                    break;
                case 9:
                    smes = "Septiembre";
                    break;
                case 10:
                    smes = "Octubre";
                    break;
                case 11:
                    smes = "Noviembre";
                    break;
                case 12:
                    smes = "Diciembre";
                    break;
            }

            return smes;
        }
        #endregion


        #region VARIABLES Y PROPIEDADES
        private const string sClase = "frmPolizaNomina.aspx.cs";
        private const string sPagina = "frmPolizaNomina.aspx";

        ResultadoNomina_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eUpdApruebaFactura;
        public event EventHandler eSaveXMLFile;
        public event EventHandler eSavePDFFile;

        public event EventHandler eValidaPDFFile;
        public event EventHandler eValidaXMLFile;


        public int iIdFolio
        {
            get { return ViewState["VSIdFolio"].S().I(); }
            set { ViewState["VSIdFolio"] = value; }
        }

        public DataSet dsDetalleNomina
        {
            get { return (DataSet)ViewState["VSDetalleNomina"]; }
            set { ViewState["VSDetalleNomina"] = value; }
        }

        public List<PolizaNominaSAP> oLstPoliza
        {
            set { ViewState["VSPolizaNomina"] = value; }
            get { return (List<PolizaNominaSAP>)ViewState["VSPolizaNomina"]; }
        }

        public string sTaxCode
        {
            get { return ViewState["VSTaxCode"].S(); }
            set { ViewState["VSTaxCode"] = value; }
        }

        public string sEmpresa
        {
            get { return ViewState["VSNombreEmpresa"].S(); }
            set { ViewState["VSNombreEmpresa"] = value; }
        }

        public int iNoFact
        {
            get { return ViewState["VSNoFact"].S().I(); }
            set { ViewState["VSNoFact"] = value; }
        }

        public string sFileName
        {
            get { return ViewState["VSsFileName"].S(); }
            set { ViewState["VSsFileName"] = value; }
        }

        public byte[] oArrFile
        {
            get { return (byte[])ViewState["VSoArrFile"]; }
            set { ViewState["VSoArrFile"] = value; }
        }

        public int iExistePDF
        {
            get { return ViewState["VSExistePDF"].S().I(); }
            set { ViewState["VSExistePDF"] = value; }
        }
        public int iExisteXML
        {
            get { return ViewState["VSExisteXML"].S().I(); }
            set { ViewState["VSExisteXML"] = value; }
        }

        #endregion


    }
}