using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
using System.Text;
using System.Xml;

namespace ClientesCasa.Views.ASC
{
    public partial class frmAprobacionFacturas : System.Web.UI.Page, IViewAprobacionFacturas
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new AprobacionFacturas_Presenter(this, new DBAccesoSAP());
                if (!IsPostBack)
                {
                    if (sFechaDesde == "0001-01-01")
                        sFechaDesde = string.Empty;
                    if (sFechaHasta == "0001-01-01")
                        sFechaHasta = string.Empty;

                    if (eSearchFacturasAprobar != null)
                        eSearchFacturasAprobar(null, null);
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //ViewState["vsDataTable"] = null;
                pnlErrores.Visible = false;

                if (!string.IsNullOrEmpty(txtNoDocumento.Text))
                    sNumDoc = txtNoDocumento.Text;
                else
                    sNumDoc = null;

                if (!string.IsNullOrEmpty(txtDe.Text))
                    sFechaDesde = txtDe.Text.Dt().ToString("dd/MM/yyyy");
                else
                    sFechaDesde = null;

                if (!string.IsNullOrEmpty(txtHasta.Text))
                    sFechaHasta = txtHasta.Text.Dt().ToString("dd/MM/yyyy");
                else
                    sFechaHasta = null;

                if (!string.IsNullOrEmpty(ddlTipoMtto.SelectedItem.Value.S()))
                    sTipoMtto = ddlTipoMtto.SelectedItem.Value;
                else
                    sTipoMtto = null;

                if (!string.IsNullOrEmpty(ddlFlota.SelectedItem.Value.S()))
                    sFlota = ddlFlota.SelectedItem.Value;
                else
                    sFlota = null;

                if (sFechaDesde == "01/01/0001")
                    sFechaDesde = string.Empty;
                if (sFechaHasta == "01/01/0001")
                    sFechaHasta = string.Empty;

                if (eSearchFacturasAprobar != null)
                    eSearchFacturasAprobar(null, null);

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int index = e.Row.RowIndex;
                    sNumDoc = dtResultFac.Rows[index]["DocNum"].S();//gvFacturas.Rows[index].Cells[2].Text;
                    int iEstatus = dtResultFac.Rows[index]["Estatus"].S().I(); //gvFacturas.Rows[index].Cells[9].Text.S().I();

                    //ImageButton imgBtnDet = (ImageButton)gvFacturas.Rows[index].FindControl("imgBtnRef");
                    GridView gvDetalle = (GridView)e.Row.FindControl("gvDetalleFactura");

                    if (iEstatus == 2)
                    {
                        if (eSearchFacturasPart != null)
                            eSearchFacturasPart(null, null);

                        if (dtFacPart != null && dtFacPart.Rows.Count > 0)
                        {
                            gvDetalle.DataSource = dtFacPart;
                            gvDetalle.DataBind();
                        }
                    }


                }
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (ddlEstatus.SelectedItem.Text == ".:Selecciona:.")
                //{
                //    ddlTipoMtto.Enabled = true;
                //    ddlFlota.Enabled = true;
                //    ddlTipoMtto.SelectedItem.Text = ".:Selecciona:.";
                //    ddlFlota.SelectedItem.Text = ".:Selecciona:.";
                //}
                //if (ddlEstatus.SelectedValue == "1")
                //{
                //    ddlTipoMtto.Enabled = false;
                //    ddlFlota.Enabled = false;
                //    ddlTipoMtto.SelectedItem.Text = ".:Selecciona:.";
                //    ddlFlota.SelectedItem.Text = ".:Selecciona:.";
                //}
                //if (ddlEstatus.SelectedValue == "2")
                //{
                //    ddlEstatus.Enabled = true;
                //    ddlTipoMtto.Enabled = true;
                //    ddlFlota.Enabled = true;
                //}
                //if (ddlEstatus.SelectedValue == "3")
                //{
                //    ddlEstatus.Enabled = true;
                //    ddlTipoMtto.Enabled = true;
                //    ddlFlota.Enabled = true;
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        protected void btnAprobarFacturas_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["vsDataTable"] = null;
                pnlErrores.Visible = false;
                bool bProcesado = false;
                bool bValidarChek = false;
                iBanCorrecto = 0;
                iDocEntry = 0;
                dtConsultaFacturas = null;
                DataTable dtDetalle = new DataTable();
                dtDetalle.Columns.Add("DocEntry");
                dtDetalle.Columns.Add("DocNum");
                dtDetalle.Columns.Add("NumAtCard");
                dtDetalle.Columns.Add("DocDate");
                dtDetalle.Columns.Add("DocTotal");
                dtDetalle.Columns.Add("DocTotalFC");
                dtDetalle.Columns.Add("DocCur");
                dtDetalle.Columns.Add("DocRate");
                dtDetalle.Columns.Add("U_Matricula");
                dtDetalle.Columns.Add("Flota");
                dtDetalle.Columns.Add("Usuario");
                dtDetalle.Columns.Add("Estatus");
                dtDetalle.Columns.Add("TipoMtto");
                dtDetalle.Columns.Add("Observaciones");
                dtDetalle.Columns.Add("CardCode");
                dtDetalle.Columns.Add("ItemCode");
                dtDetalle.Columns.Add("ArticuloD");
                dtDetalle.Columns.Add("CentroCostosD");
                dtDetalle.Columns.Add("ImporteD");
                dtDetalle.Columns.Add("TaxCode");
                dtDetalle.Columns.Add("LineNum");
                dtDetalle.Columns.Add("Quantity");
                dtDetalle.Columns.Add("Price");
                dtDetalle.Columns.Add("AcctCode");
                dtDetalle.Columns.Add("FechaImp");
                dtDetalle.Columns.Add("XML");
                dtDetalle.Columns.Add("PDF");

                foreach (GridViewRow row in gvFacturas.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkFactura");

                    #region RECORRER FACTURAS A APROBAR
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            TextBox txtFechaCont = (TextBox)row.FindControl("txtFechaConta");
                            int iStatus = 0;
                            string strCampo = string.Empty;
                            string strValor = string.Empty;
                            string strExcepcion = string.Empty;
                            int iPDFExists = 0;
                            int iXMLExists = 0;
                            sArchivo = string.Empty;
                            sFechaDoc = string.Empty;

                            string sCad_XML = string.Empty;
                            string sCad_PDF = string.Empty;

                            iDocEntry = gvFacturas.Rows[row.RowIndex].Cells[14].Text.S().I(); // Verificar
                            sNumDoc = gvFacturas.Rows[row.RowIndex].Cells[15].Text;
                            string sFacturaH = gvFacturas.Rows[row.RowIndex].Cells[2].Text;
                            string sFechaH = gvFacturas.Rows[row.RowIndex].Cells[4].Text;
                            string sImporteH = gvFacturas.Rows[row.RowIndex].Cells[5].Text;
                            string sCCH = gvFacturas.Rows[row.RowIndex].Cells[7].Text;
                            string sTipoMttoH = gvFacturas.Rows[row.RowIndex].Cells[8].Text;
                            string sFechaContable = txtFechaCont.Text; //gvFacturas.Rows[row.RowIndex].Cells[9].Text;
                            string sObservacionesH = gvFacturas.Rows[row.RowIndex].Cells[10].Text;
                            int iTipoMtto = 0;
                            string sTipoCambioH = gvFacturas.Rows[row.RowIndex].Cells[11].Text;
                            string sCodigoProveedorH = ConfigurationManager.AppSettings["CardCodeASC"].S(); //gvFacturas.Rows[row.RowIndex].Cells[9].Text;
                            string sMonedaH = gvFacturas.Rows[row.RowIndex].Cells[13].Text;

                            string sFechaFormat = string.Empty;
                            sFechaFormat = GetFormatoFecha(sFechaH);

                            string sFechaCont = string.Empty;
                            sFechaCont = sFechaContable.Dt().ToString("dd/MM/yyyy");

                            sFechaH = string.Empty;
                            sFechaH = sFechaFormat;

                            sFechaDoc = sFechaFormat;
                            sArchivo = sFacturaH;

                            DataTable dtProveedor = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedorH);

                            if (dtProveedor.Rows[0]["U_REQXML"].S() == "SI")
                            {
                                if (eGetValidaXML != null)
                                    eGetValidaXML(null, EventArgs.Empty);

                                sCad_XML = sCadArchivo;
                            }

                            if (dtProveedor.Rows[0]["U_REQPDF"].S() == "SI")
                            {
                                if (eGetValidaPDF != null)
                                    eGetValidaPDF(null, EventArgs.Empty);

                                sCad_PDF = sCadArchivo;
                            }

                            if (iDocEntry != 0)
                            {
                                if (eSearchDetalleFac != null)
                                    eSearchDetalleFac(sender, e);

                                if (dtConsultaFacturas != null && dtConsultaFacturas.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtConsultaFacturas.Rows.Count; i++)
                                    {
                                        DataRow rowD = dtDetalle.NewRow();
                                        rowD["DocEntry"] = iDocEntry.S();
                                        rowD["DocNum"] = sNumDoc; //dtConsultaFacturas.Rows[i]["NumDocumento"].S();
                                        rowD["NumAtCard"] = sFacturaH;
                                        rowD["DocDate"] = dtConsultaFacturas.Rows[i]["DocDate"].S();
                                        rowD["DocTotal"] = sImporteH.Replace("$", "").S().D();
                                        rowD["DocTotalFC"] = "";
                                        rowD["DocCur"] = dtConsultaFacturas.Rows[i]["Currency"].S();
                                        rowD["DocRate"] = sTipoCambioH;
                                        rowD["U_Matricula"] = sCCH;
                                        rowD["Flota"] = dtConsultaFacturas.Rows[i]["Flota"].S();
                                        rowD["Usuario"] = sUsuario;
                                        rowD["Estatus"] = "";

                                        if (sTipoMttoH == "Preventivo")
                                            iTipoMtto = 1;
                                        else if (sTipoMttoH == "Correctivo")
                                            iTipoMtto = 2;
                                        else if (sTipoMttoH == "Reserva Motores")
                                            iTipoMtto = 3;
                                        else if (sTipoMttoH == "Reserva Interiores")
                                            iTipoMtto = 4;

                                        rowD["TipoMtto"] = iTipoMtto.S();
                                        rowD["Observaciones"] = sObservacionesH;
                                        rowD["CardCode"] = sCodigoProveedorH;
                                        rowD["ItemCode"] = dtConsultaFacturas.Rows[i]["ItemCode"].S();
                                        rowD["ArticuloD"] = dtConsultaFacturas.Rows[i]["Dscription"].S();
                                        rowD["CentroCostosD"] = dtConsultaFacturas.Rows[i]["CentroCostosD"].S();
                                        rowD["ImporteD"] = dtConsultaFacturas.Rows[i]["LineTotal"].S();
                                        rowD["TaxCode"] = dtConsultaFacturas.Rows[i]["Taxcode"].S();
                                        rowD["LineNum"] = dtConsultaFacturas.Rows[i]["LineNum"].S();
                                        rowD["Quantity"] = dtConsultaFacturas.Rows[i]["Quantity"].S();
                                        rowD["Price"] = dtConsultaFacturas.Rows[i]["LineTotal"].S();//dtConsultaFacturas.Rows[i]["Price"].S();
                                        rowD["AcctCode"] = dtConsultaFacturas.Rows[i]["AcctCode"].S();
                                        rowD["FechaImp"] = sFechaCont;
                                        rowD["XML"] = sCad_XML;
                                        rowD["PDF"] = sCad_PDF;
                                        dtDetalle.Rows.Add(rowD);

                                    }

                                }

                                //Validar documentos PDF y XML

                                sArchivo = sFacturaH;
                                sFechaDoc = sFechaH;

                                #region VALIDAR PDF
                                if (eGetValidaPDF != null)
                                    eGetValidaPDF(null, EventArgs.Empty);

                                if (!File.Exists(sCadArchivo))
                                {
                                    Session["CadArchivo"] = sCadArchivo;
                                    strCampo = sArchivo + ".pdf";
                                    strValor = sCadArchivo;
                                    strExcepcion = "El archivo PDF no se encontró en la ubicación adecuada, favor de verificar";
                                    iStatus = 1; //Prueba para pasar 1
                                    iPDFExists = 1; //Prueba para pasar 1

                                    if (iStatus == 0 && iPDFExists == 0)
                                        dtRowError(strCampo, strValor, iStatus, strExcepcion);
                                }
                                else
                                {
                                    iStatus = 1;
                                    iPDFExists = 1;
                                }

                                
                                #endregion

                                #region VALIDAR XML

                                if (eGetValidaXML != null)
                                    eGetValidaXML(null, EventArgs.Empty);

                                if (!File.Exists(sCadArchivo))
                                {
                                    strCampo = sArchivo + ".xml";
                                    strValor = sCadArchivo;
                                    strExcepcion = "El archivo XML no se encontró en la ubicación adecuada, favor de verificar";
                                    iStatus = 1; //Prueba para pasar 1
                                    iXMLExists = 1; //Prueba para pasar 1

                                    if (iStatus == 0 && iXMLExists == 0)
                                        dtRowError(strCampo, strValor, iStatus, strExcepcion);
                                }
                                else
                                {
                                    iStatus = 1;
                                    iXMLExists = 1;
                                }

                                #endregion

                                if (iPDFExists == 1 && iXMLExists == 1)
                                {
                                    //if (ViewState["vsDataTable"] == null || gvResultado.Rows.Count == 0)
                                    //{
                                        //Mensaje("Actualiza:" + ViewState["vsDataTable"].S());
                                        //if (eUpdateFacturas != null)
                                        //    eUpdateFacturas(sender, e);
                                    //}
                                }

                                //Fin de Validación

                            }

                            

                        }
                    }

                    #endregion
                }
                dtDetalle.AcceptChanges();


                if (dtDetalle.Rows.Count > 0)
                {
                    if (dtDetalle != null && dtDetalle.Rows.Count > 0)
                    {
                        if (ViewState["vsDataTable"] == null)
                        {
                            bProcesado = ProcesarFacturas(dtDetalle);
                           
                            if (bProcesado == true)
                            {
                                //CargaFacturas();

                                Mensaje("Las facturas han sido contabilizadas correctamente.");

                                sNumDoc = txtNoDocumento.Text;
                                sFechaDesde = txtDe.Text.Dt().ToString("dd/MM/yyyy");
                                sFechaHasta = txtHasta.Text.Dt().ToString("dd/MM/yyyy");
                                sTipoMtto = ddlTipoMtto.SelectedItem.Value;
                                sFlota = ddlFlota.SelectedItem.Value;

                                if (sFechaDesde == "01/01/0001")
                                    sFechaDesde = string.Empty;
                                if (sFechaHasta == "01/01/0001")
                                    sFechaHasta = string.Empty;

                                if (eSearchFacturasAprobar != null)
                                    eSearchFacturasAprobar(null, null);
                            }
                            else
                            {
                                Mensaje("Verificar, no se pueden insertar las facturas seleccionadas.");
                            }
                        }
                        else
                        {
                            pnlErrores.Visible = true;
                            gvResultado.DataSource = (DataTable)ViewState["vsDataTable"];
                            gvResultado.DataBind();
                        }
                    }
                }
                else
                {
                    Mensaje("Verificar, debe de seleccionar al menos una factura para aprobar.");
                }
                upaGridFacturas.Update();
            }
            catch (Exception ex)
            {
                Mensaje("Error:" + ex.Message);
                //Mensaje("Fecha Actual Server:" + DateTime.Now.ToShortDateString() + ", error: " + ex.Message);
            }
        }

        #endregion

        #region MÉTODOS

        protected void CargaFacturas() 
        {
            try
            {
                if (sFechaDesde == "0001-01-01")
                    sFechaDesde = string.Empty;
                if (sFechaHasta == "0001-01-01")
                    sFechaHasta = string.Empty;

                if (eSearchFacturasAprobar != null)
                    eSearchFacturasAprobar(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetFormatoFecha(string sFecha)
        {
            try
            {
                string sDia = string.Empty;
                string sMes = string.Empty;
                string sAnio = string.Empty;
                DateTime sFec;
                string[] arrFechaDoc = sFecha.Split(' ');
                string sFechaResult = string.Empty;

                sDia = arrFechaDoc[0].S();
                sAnio = arrFechaDoc[2].S();
                sMes = arrFechaDoc[1].S();

                if (sMes == "Ene")
                    sMes = "01";
                if (sMes == "Feb")
                    sMes = "02";
                if (sMes == "Mar")
                    sMes = "03";
                if (sMes == "Abr")
                    sMes = "04";
                if (sMes == "May")
                    sMes = "05";
                if (sMes == "Jun")
                    sMes = "06";
                if (sMes == "Jul")
                    sMes = "07";
                if (sMes == "Ago")
                    sMes = "08";
                if (sMes == "Sep")
                    sMes = "09";
                if (sMes == "Oct")
                    sMes = "10";
                if (sMes == "Nov")
                    sMes = "11";
                if (sMes == "Dic")
                    sMes = "12";

                if (sDia == "1" || sDia == "2" || sDia == "3" || sDia == "4" || sDia == "5" || sDia == "6" || sDia == "7" || sDia == "8" || sDia == "9")
                    sDia = "0" + sDia;

                if (sMes == "1" || sMes == "2" || sMes == "3" || sMes == "4" || sMes == "5" || sMes == "6" || sMes == "7" || sMes == "8" || sMes == "9")
                    sMes = "0" + sMes;

                sFecha = sDia.S() + "/" + sMes.S() + "/" + sAnio.S(); 
                return sFecha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadFacturasPart(DataTable dtPart)
        {
            try
            {
                dtFacPart = null;
                dtFacPart = dtPart;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void dtRowError(string strCampo, string strValor, int iStatus, string strExcepcion)
        {
            try
            {
                bool bBandera = false;
                System.Data.DataTable dtNew = new System.Data.DataTable();
                System.Data.DataTable dt;

                //Declaramos variables DataColumn y DataRow.
                DataColumn column;
                DataRow row;
                DataView view;

                if (ViewState["vsDataTable"] != null)
                    bBandera = true;

                // Verificamos si nuestro DataTable en la variable de estado contiene datos
                if (bBandera == false)
                {
                    // Creación de nueva DataColumn, typo de dato y Nombre de la columna.

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Campo";
                    dtNew.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Valor";
                    dtNew.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = Type.GetType("System.String");
                    column.ColumnName = "Status";
                    dtNew.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = Type.GetType("System.String");
                    column.ColumnName = "Descripcion";
                    dtNew.Columns.Add(column);

                    // Crea un nuevo objeto DataRow y lo agrega al DataTable. 
                    row = dtNew.NewRow();
                    row["Campo"] = strCampo;
                    row["Valor"] = strValor;

                    if (iStatus == 0)
                        row["Status"] = "Error";
                    else
                        row["Status"] = "Válido";

                    row["Descripcion"] = strExcepcion;
                    dtNew.Rows.Add(row);

                    ViewState["vsDataTable"] = dtNew;
                    view = new DataView(dtNew);
                    //pnlSimulacion.Visible = true;
                }
                else
                {
                    dt = (System.Data.DataTable)ViewState["vsDataTable"];
                    row = dt.NewRow();
                    row["Campo"] = strCampo;
                    row["Valor"] = strValor;

                    if (iStatus == 0)
                        row["Status"] = "Error";
                    else
                        row["Status"] = "Válido";

                    row["Descripcion"] = strExcepcion;
                    dt.Rows.Add(row);
                    ViewState["vsDataTable"] = dt;
                    view = new DataView(dt);
                    //pnlSimulacion.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool ProcesarFacturas(DataTable dtContenido) 
        {
            try
            {
                bool bBan = false;
                int iHead = 0;
                string sNumDoc = string.Empty;
                string sNoFactura = string.Empty;
                string sFechaC = string.Empty;
                string sProyecto = string.Empty;
                string sDimension1 = string.Empty;
                string sDimension2 = string.Empty;
                string sDimension3 = string.Empty;
                string sDimension4 = string.Empty;
                string sDimension5 = string.Empty;
                string sCentroCostoD = string.Empty;
                string sTpoCambio = string.Empty;
                string sCodigoProveedor = string.Empty;
                string sMoneda = string.Empty;

                string sFechaFac = string.Empty;
                string sFechaExp = string.Empty;
                string sFechaImp = string.Empty;
                //string sTipoMtto = string.Empty;

                string sXMLChar = string.Empty;
                String[] _sXMLChar;
                string sRutaXML = string.Empty;

                List<FacturaAprobacionASC> oLsFacturas = new List<FacturaAprobacionASC>();

                DataTable distinctDT = new DataTable();
                DataRow[] fRows;
                distinctDT = SelectDistinct(dtContenido, "NumAtCard");

                sCodigoProveedor = "P00064";
                System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                for (int i = 0; i < distinctDT.Rows.Count; i++)
                {
                    FacturaAprobacionASC oFactura = new FacturaAprobacionASC();
                    sNoFactura = distinctDT.Rows[i][0].S();
                    fRows = dtContenido.Select("NumAtCard='" + sNoFactura + "'");

                    string sGetXML = sNoFactura + ".XML";
                    sRutaXML = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestUrlXML\OTHER\" + sGetXML;

                    for (int x = 0; x < fRows.Length; x++)
                    {
                        ConceptosFacturaAprobASC oConcepto = new ConceptosFacturaAprobASC();
                        sCentroCostoD = fRows[x]["CentroCostosD"].S();

                        //if (eSearchMatricula != null)
                        //    eSearchMatricula(null, null);

                        iDocEntry = fRows[x]["DocEntry"].S().I();

                        if (x == 0)
                        {
                            if (i > 0)
                                iHead = i - i;
                            //Armando Header
                            oFactura.iId = i + 1;
                            oFactura.sEmpresa = "1";
                            oFactura.sSucursal = "1";
                            oFactura.sEmpleado = fRows[iHead]["CardCode"].S();
                            oFactura.sNoFactura = fRows[iHead]["NumAtCard"].S();
                            oFactura.sFormaPago = "";

                            DateTime dtFecha = DateTime.Now;
                            sFechaImp = fRows[iHead]["FechaImp"].ToString(); // Fecha Contable
                            sFechaC = fRows[iHead]["DocDate"].ToString(); // Fecha Factura

                            sFechaFac = sFechaC; //Fecha Factura
                            sFechaExp = sFechaImp; //Fecha Contable

                            string dtActual = DateTime.Now.ToString("yyyyMMdd");
                            string dtFechaDoc = sFechaExp.Substring(0, 10).Replace(" ", ""); // Fecha Contable
                            string dtFechaDocFac = sFechaFac.Substring(0, 10).Replace(" ", ""); // Fecha Factura
                            decimal dTipoCambio = fRows[iHead]["DocRate"].ToString().Replace(".", ",").D();                            

                            string sDocDate = string.Empty;
                            string sDocDateFac = string.Empty;
                            string[] sArrFechaDoc = dtFechaDoc.Split('/');
                            string[] sArrFechaDocFac = dtFechaDocFac.Split('/');

                            string sDay = string.Empty;
                            string sMonth = string.Empty;
                            string sYear = string.Empty;

                            string sDayF = string.Empty;
                            string sMonthF = string.Empty;
                            string sYearF = string.Empty;

                            sDay = sArrFechaDoc[0];
                            sMonth = sArrFechaDoc[1];
                            sYear = sArrFechaDoc[2];

                            sDayF = sArrFechaDocFac[0];
                            sMonthF = sArrFechaDocFac[1];
                            sYearF = sArrFechaDocFac[2];

                            if (sDay.Length == 1)
                                sDay = sDay.PadLeft(2, '0');
                            if (sMonth.Length == 1)
                                sMonth = sMonth.PadLeft(2,'0');
                            if (sYear.Length > 4)
                                sYear = sYear.Substring(0, 4);

                            if (sDayF.Length == 1)
                                sDayF = sDayF.PadLeft(2, '0');
                            if (sMonthF.Length == 1)
                                sMonthF = sMonthF.PadLeft(2, '0');
                            if (sYearF.Length > 4)
                                sYearF = sYearF.Substring(0, 4);

                            sDocDate = sYear + sMonth + sDay;
                            sDocDateFac = sYearF + sMonthF + sDayF;

                            oFactura.dtFecha = sDocDateFac; //Fecha Factura    //sFechaExp.Dt(); //fRows[iHead]["DocDate"].S().Dt(); //dtFechaActual; //sFechaC.Dt();//dtFechaDoc;
                            oFactura.dtFechaExp = sDocDate; //Fecha Contable   //sFechaImp.Dt(); //fRows[iHead]["DocDate"].S().Dt(); //dtFechaActual; //sFechaC.Dt();//dtFechaDoc;
                            oFactura.dtFechaImp = dtActual; //dtFecha.ToShortDateString(); //dtFechaConta;
                            oFactura.dTipoCambio = dTipoCambio;
                            oFactura.sMsg = "";

                            string sComment = HttpUtility.HtmlDecode(fRows[iHead]["Observaciones"].S());
                            oFactura.sComentarios = sComment;

                            oFactura.sSerie = "";
                            oFactura.iTipoMtto = fRows[iHead]["TipoMtto"].S().I();

                            if (!string.IsNullOrEmpty(fRows[iHead]["DocCur"].S()))
                            {
                                if (fRows[iHead]["DocCur"].S() == "MXP")
                                    sMoneda = "MXN";
                                else
                                    sMoneda = fRows[iHead]["DocCur"].S();
                            }
                            else
                                sMoneda = string.Empty;

                            oFactura.sMoneda = sMoneda;
                            oFactura.dDescuento = 0;
                            oFactura.iDocSAP = fRows[iHead]["DocNum"].S().I();

                            //Valida y Obtiene informaxión de XML
                            if (eGetValidaXML != null)
                                eGetValidaXML(null, EventArgs.Empty);

                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                            {
                                //if (File.Exists(sRutaXML))
                                if (File.Exists(sCadArchivo))
                                {
                                    sXMLChar = ObtenerDatosXML(sCadArchivo);
                                    //sXMLChar = ObtenerDatosXML(sRutaXML);

                                    if (!string.IsNullOrEmpty(sXMLChar))
                                    {
                                        _sXMLChar = sXMLChar.Split('|');
                                        oFactura.sUID = _sXMLChar[0].S();
                                        oFactura.sRFC = _sXMLChar[1].S();
                                        oFactura.sMonto = _sXMLChar[2].S();
                                    }
                                }
                                else
                                {
                                    oFactura.sUID = "";
                                    oFactura.sRFC = "";
                                    oFactura.sMonto = "";
                                }
                            }
                            else
                            {
                                oFactura.sUID = string.Empty;
                                oFactura.sRFC = string.Empty;
                                oFactura.sMonto = string.Empty;
                            }
                            //Fin de validación XML

                        }

                        //Agregar a Detalle
                        oConcepto.iId = i + 1;
                        oConcepto.sEmpresa = "1";
                        oConcepto.iLinea = x + 1;
                        oConcepto.sItem = fRows[x]["ItemCode"].S();
                        oConcepto.sDescripcionUsuario = fRows[x]["ArticuloD"].S(); //sDescripcionArticulo;
                        oConcepto.dCantidad = fRows[x]["Quantity"].ToString().Replace(".",",").D();
                        oConcepto.dPrecio = fRows[x]["Price"].ToString().Replace(".", ",").D();
                        oConcepto.dTotalLinea = fRows[x]["ImporteD"].ToString().Replace(".", ",").D();
                        oConcepto.sCodigoImpuesto = fRows[x]["TaxCode"].S();
                        oConcepto.dDescuento = 0;
                        oConcepto.iImpuesto = 0;
                        oConcepto.sCuenta = fRows[x]["AcctCode"].S();
                        oConcepto.sAlmacen = "";

                        if (!string.IsNullOrEmpty(sDimension1))
                        {
                            System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionArea(sCentroCostoD);

                            if (dtDatos.Rows.Count > 0)
                            {
                                sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                                sDimension3 = dtDatos.Rows[0]["U_Site"].S();
                                sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                            }
                            sDimension1 = sCentroCostoD;
                            sDimension2 = string.Empty;
                            sDimension5 = string.Empty;
                        }
                        else
                        {
                            System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionMatricula(sCentroCostoD);

                            if (dtDatos.Rows.Count > 0)
                            {
                                sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                                sDimension3 = dtDatos.Rows[0]["U_Site"].S();
                                sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                            }
                            sDimension1 = string.Empty;
                            sDimension2 = sCentroCostoD;
                            sDimension5 = string.Empty;
                        }

                        oConcepto.sProyecto = sProyecto;
                        oConcepto.sDimension1 = sDimension1;
                        oConcepto.sDimension2 = sDimension2;
                        oConcepto.sDimension3 = sDimension3;
                        oConcepto.sDimension4 = sDimension4;
                        oConcepto.sDimension5 = sDimension5;
                        oConcepto.sXML = fRows[x]["XML"].S();
                        oConcepto.sPDF = fRows[x]["PDF"].S();

                        oFactura.oLstConceptos.Add(oConcepto);

                        //Actualiza Estatus de Factura
                        if (eUpdateFacturas != null)
                            eUpdateFacturas(null, null);

                    }
                    oLsFacturas.Add(oFactura);
                }
                ListaFacturas = oLsFacturas;

                if (eSetProcesarAprobacion != null)
                    eSetProcesarAprobacion(null, null);

                if (iBanCorrecto == 1) 
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerDatosXML(string sRutaXML)
        {
            try
            {
                string sUUID = string.Empty;
                string sRFC = string.Empty;
                string sMonto = string.Empty;
                XmlDocument xDoc = new XmlDocument();

                xDoc.Load(sRutaXML);

                XmlNodeList cfdi = xDoc.GetElementsByTagName("cfdi:Comprobante");
                XmlNodeList cfdi2 = xDoc.GetElementsByTagName("cfdi:Emisor");
                XmlNodeList tfd = xDoc.GetElementsByTagName("tfd:TimbreFiscalDigital");

                foreach (XmlElement comprobante in cfdi)
                {
                    sMonto = comprobante.GetAttribute("Total");
                }

                foreach (XmlElement comprobante in cfdi2)
                {
                    sRFC = comprobante.GetAttribute("Rfc");
                }

                foreach (XmlElement comprobante in tfd)
                {
                    sUUID = comprobante.GetAttribute("UUID");
                }
                return sUUID + "|" + sRFC + "|" + sMonto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FormatoFechaGenerico(string sFecha)
        {
            try
            {
                string sDia = string.Empty;
                string sMes = string.Empty;
                string sAnio = string.Empty;
                DateTime sFec;

                if (DateTime.TryParseExact(sFecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                {
                    string[] arrFecha = sFecha.Split('/');
                    sMes = arrFecha[1].S();
                    sDia = arrFecha[0].S();
                    sAnio = arrFecha[2].S();
                }
                else
                {
                    if (DateTime.TryParseExact(sFecha, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out sFec))
                    {
                        sAnio = sFecha.Substring(0, 4).S();
                        sMes = sFecha.Substring(4, 2).S();
                        sDia = sFecha.Substring(6, 2).S();
                    }

                    //string[] arrFecha = sFecha.Split('/');
                    //sDia = arrFecha[1].S();
                    //sMes = arrFecha[0].S();
                    //sAnio = arrFecha[2].S();
                }

                if (sDia == "1" || sDia == "2" || sDia == "3" || sDia == "4" || sDia == "5" || sDia == "6" || sDia == "7" || sDia == "8" || sDia == "9")
                    sDia = "0" + sDia;

                if (sMes == "1" || sMes == "2" || sMes == "3" || sMes == "4" || sMes == "5" || sMes == "6" || sMes == "7" || sMes == "8" || sMes == "9")
                    sMes = "0" + sMes;

                sFecha = sDia + "/" + sMes + "/" + sAnio;

                return sFecha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal ObtieneTipoCambio(string sMoneda, ref DateTime dtFechaFact, string sFecha)
        {
            try
            {
                if (sMoneda != "MXN")
                {
                    sFecha = FormatoFecha(sFecha);
                    dtFechaFact = new DateTime(sFecha.Substring(0, 4).S().I(), sFecha.Substring(4, 2).S().I(), sFecha.Substring(6, 2).S().I());
                    return new DBAccesoSAP().DBGetObtieneTipoCambioDia(dtFechaFact);
                }
                else
                    return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FormatoFecha(string sFecha)
        {
            try
            {
                string sDia = string.Empty;
                string sMes = string.Empty;
                string sAnio = string.Empty;
                DateTime sFec;

                if (DateTime.TryParseExact(sFecha, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out sFec))
                {
                    sAnio = sFecha.Substring(0, 4).S();
                    sMes = sFecha.Substring(4, 2).S();
                    sDia = sFecha.Substring(6, 2).S();
                }
                else
                {
                    if (DateTime.TryParseExact(sFecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                    {
                        string[] arrFecha = sFecha.Split('/');
                        sMes = arrFecha[1].S();
                        sDia = arrFecha[0].S();
                        sAnio = arrFecha[2].S();
                    }
                    //else
                    //{
                    //    string[] arrFecha = sFecha.Split('/');
                    //    sDia = arrFecha[1].S();
                    //    sMes = arrFecha[0].S();
                    //    sAnio = arrFecha[2].S();
                    //}
                }



                if (sDia == "1" || sDia == "2" || sDia == "3" || sDia == "4" || sDia == "5" || sDia == "6" || sDia == "7" || sDia == "8" || sDia == "9")
                    sDia = "0" + sDia;

                if (sMes == "1" || sMes == "2" || sMes == "3" || sMes == "4" || sMes == "5" || sMes == "6" || sMes == "7" || sMes == "8" || sMes == "9")
                    sMes = "0" + sMes;

                sFecha = sAnio.S() + sMes.S() + sDia.S();
                return sFecha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
        {
            // Create a Datatable â€“ datatype same as FieldName
            DataTable dt = new DataTable(SourceTable.TableName);
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);
            // Loop each row & compare each value with one another
            // Add it to datatable if the values are mismatch
            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
            return dt;
        }

        private bool ColumnEqual(object A, object B)
        {
            // Compares two values to see if they are equal. Also compares DBNULL.Value.           
            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is BNull.Value
                return false;
            return (A.Equals(B));  // value type standard comparison
        }

        public void Mensaje(string sMensaje)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }

        public void LoadFacturasASC(DataTable dtFacturas)
        {
            try
            {
                //DataTable dtResultFac = new DataTable();
                dtResultFac = dtFacturas;

                if (dtResultFac != null && dtResultFac.Rows.Count > 0)
                    btnAprobarFacturas.Visible = true;
                else
                    btnAprobarFacturas.Visible = false;

                gvFacturas.DataSource = dtResultFac;
                gvFacturas.DataBind();
                //ControlCheckGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadDetalleFacturasASC(DataTable dtDetalleFa) 
        {
            try
            {
                if (dtDetalleFa != null && dtDetalleFa.Rows.Count > 0) 
                {
                    dtConsultaFacturas = null;
                    dtConsultaFacturas = dtDetalleFa;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmAprobacionFacturas.aspx.cs";
        private const string sPagina = "frmAprobacionFacturas.aspx";

        AprobacionFacturas_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchFacturasAprobar;
        public event EventHandler eSearchDetalleFac;
        public event EventHandler eUpdateFacturas;
        public event EventHandler eSetProcesarAprobacion;
        public event EventHandler eGetValidaXML;
        public event EventHandler eGetValidaPDF;
        public event EventHandler eSearchFacturasPart;

        public int iDocEntry
        {
            get { return (int)ViewState["VSDocEntry"]; }
            set { ViewState["VSDocEntry"] = value; }
        }
        public string sNumDoc
        {
            get { return (string)ViewState["VSNumdoc"]; }
            set { ViewState["VSNumdoc"] = value; }
        }
        public string sFechaDesde
        {
            get { return (string)ViewState["VSFechaDe"]; }
            set { ViewState["VSFechaDe"] = value; }
        }
        public string sFechaHasta
        {
            get { return (string)ViewState["VSFechaHasta"]; }
            set { ViewState["VSFechaHasta"] = value; }
        }
        public string sEstatus
        {
            get { return (string)ViewState["VSEstatus"]; }
            set { ViewState["VSEstatus"] = value; }
        }
        public string sTipoMtto
        {
            get { return (string)ViewState["VSTipoMtto"]; }
            set { ViewState["VSTipoMtto"] = value; }
        }
        public string sFlota
        {
            get { return (string)ViewState["VSFlota"]; }
            set { ViewState["VSFlota"] = value; }
        }
        public string sUsuario 
        {
            get { return (string)ViewState["VSUsuario"]; }
            set { ViewState["VSUsuario"] = Utils.GetUser; }
        }
        public string sCadArchivo
        {
            get { return (string)ViewState["VSValidaArchivo"]; }
            set { ViewState["VSValidaArchivo"] = value; }
        }
        public DataTable dtFacturas
        {
            get { return (DataTable)ViewState["VSFac"]; }
            set { ViewState["VSFac"] = value; }
        }
        public string sFechaDoc 
        {
            get { return (string)ViewState["VSFechaDoc"]; }
            set { ViewState["VSFechaDoc"] = value; }
        }
        public string sArchivo
        {
            get { return (string)ViewState["VSArchivo"]; }
            set { ViewState["VSArchivo"] = value; }
        }
        public string sXML
        {
            get { return (string)ViewState["VSXML"]; }
            set { ViewState["VSXML"] = value; }
        }

        //Bandera para saber si se insertó correctamente
        public int iBanCorrecto
        {
            get { return (int)ViewState["VSCorrecto"]; }
            set { ViewState["VSCorrecto"] = value; }
        }

        public DataTable dtConsultaFacturas
        {
            get { return (DataTable)ViewState["VSGetFac"]; }
            set { ViewState["VSGetFac"] = value; }
        }

        public List<FacturaAprobacionASC> ListaFacturas
        {
            set { ViewState["VSListaFacturasASC"] = value; }
            get { return (List<FacturaAprobacionASC>)ViewState["VSListaFacturasASC"]; }
        }

        public DataTable dtResultFac
        {
            get { return (System.Data.DataTable)ViewState["VSResultFac"]; }
            set { ViewState["VSResultFac"] = value; }
        }

        public DataTable dtFacPart
        {
            get { return (System.Data.DataTable)ViewState["VSFacP"]; }
            set { ViewState["VSFacP"] = value; }
        }
        #endregion

        

        
    }
}