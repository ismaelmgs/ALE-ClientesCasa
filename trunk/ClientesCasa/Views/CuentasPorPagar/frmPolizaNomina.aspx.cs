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

//using Microsoft.Office.Interop.Excel;
using NucleoBase.Core;
using System.Globalization;
using ClientesCasa.Clases;
using System.Xml;
using System.Text.RegularExpressions;

namespace ClientesCasa.Views.ASC
{
    public partial class frmPolizaNomina : System.Web.UI.Page, IViewPolizaNomina
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new PolizaNomina_Presenter(this, new DBPolizaNomina());

            if (!IsPostBack)
            {
                if (eObjSelected != null)
                    eObjSelected(sender, e);

                CargaDatosIniciales();
            }
        }

        protected void btnLeer_Click(object sender, EventArgs e)
        {
            try
            {
                //wsNomina.ws_catalogosTokenSoapClient oclient = new wsNomina.ws_catalogosTokenSoapClient();
                //wsNomina.SoapHeaderAccess oheader = new wsNomina.SoapHeaderAccess();
                //oheader.Usuario = "ISIWS_Test";
                //oheader.Password = "ISIWS_Test";
                //wsNomina.TokenAcceso oToken = oclient.GetTokenAcceso(oheader);
                //wsNomina.SoapHeaderToken oHT = new wsNomina.SoapHeaderToken();
                //oHT.TokenAcceso = oToken.Token.S();

                //DataSet ds = oclient.GetCatalog(oHT, "nom_arkan_mex", "enero21");

                wsNomina.ws_catalogosToken oclient = new wsNomina.ws_catalogosToken();
                wsNomina.SoapHeaderAccess oheader = new wsNomina.SoapHeaderAccess();
                oheader.Usuario = "ISIWS_Test";
                oheader.Password = "ISIWS_Test";
                oclient.SoapHeaderAccessValue = oheader;
                wsNomina.TokenAcceso oToken = oclient.GetTokenAcceso();
                wsNomina.SoapHeaderToken oHT = new wsNomina.SoapHeaderToken();
                oHT.TokenAcceso = oToken.Token.S();
                oclient.SoapHeaderTokenValue = oHT;
                string sMes = string.Empty;
                string sMesPeriodo = string.Empty;
                string sAnioPeriodo = string.Empty;


                sAnioPeriodo = dtExiste.Rows[0]["Anio"].S();
                sMes = ObtieneNombreMesReporte(DateTime.Now.AddMonths(-2).Month);
                sMesPeriodo = sMes + sAnioPeriodo.Substring(2);
                DataSet ds = oclient.GetCatalog("nom_arkan_mex", "marzo21");
                //DataSet ds = oclient.GetCatalog("nom_arkan_mex", sMesPeriodo);


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    bool bValidaCampos = true; // ValidaDatosDataTable(ds.Tables[0]);

                    if (bValidaCampos)
                    {
                        pnlBotonesProcesar.Visible = true;
                        hdnValidacion.Value = "1";
                        btnInsertar.Enabled = true;

                        if (iFolioExistente > 0)
                            btnSobreescribir.Visible = true;

                        dtData = ds.Tables[0].Copy();
                        //gvDatos.DataSource = dtExcel;
                        //gvDatos.DataBind();
                    }
                    else
                    {
                        pnlBotonesProcesar.Visible = false;
                        hdnValidacion.Value = "0";
                        btnInsertar.Enabled = false;
                    }
                }
                else
                {
                    msgError.Visible = true;
                    lblError.Text = "No se encontró información del periodo, favor de verificar";
                }


                #region COMENTADO
                //Page.Validate("groupFechas");

                //if (Page.IsValid)
                //{
                //    if ((!string.IsNullOrEmpty(txtFechaInicio.Text) && !string.IsNullOrEmpty(txtFechaFinal.Text)))
                //    {
                //        if (txtFechaInicio.Text.Dt() < txtFechaFinal.Text.Dt())
                //        {
                //            msgError.Visible = false;
                //            lblError.Text = string.Empty;
                //            msgSuccesss.Visible = false;
                //            lblSuccess.Text = string.Empty;

                //            ValidarFileUpload();
                //        }
                //        else
                //        {
                //            //MostrarMensaje("Ingrese un período válido.", "Lo sentimos. Intente nuevamente.");
                //            msgError.Visible = true;
                //            lblError.Text = "Ingrese un período válido.";
                //            msgSuccesss.Visible = false;
                //            lblSuccess.Text = string.Empty;
                //        }
                //    }
                //    else
                //    {
                //        //MostrarMensaje("Ingrese una fecha en los campos solicitados.", "Lo sentimos. Intente nuevamente.");
                //        msgError.Visible = true;
                //        lblError.Text = "Ingrese una fecha en los campos solicitados.";
                //        msgSuccesss.Visible = false;
                //        lblSuccess.Text = string.Empty;
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: ", ex.Message.ToString());
            }
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnValidacion.Value == "1")
                {
                    InsertarDatos(dtData);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void tmRedi_Tick(object sender, EventArgs e)
        {
            Response.Redirect("frmPolizaResult.aspx?Fol=" + iIdFolio.S());
        }

        protected void btnRedirigir_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmPolizaResult.aspx?IdFolio=" + iFolioExistente.S());
        }

        protected void btnSobreescribir_Click(object sender, EventArgs e)
        {
            if (eDeleteObj != null)
                eDeleteObj(sender, e);

            btnLeer_Click(sender, e);

            btnInsertar_Click(sender, e);

            msgSuccesss.Visible = true;
            lblSuccess.Text = "Se reemplazó con exito el periodo.";
            //MostrarMensaje("Se reemplazó la carga con Éxito","Aviso");
        }
        #endregion

        #region MÉTODOS
        //protected void ValidarFileUpload()
        //{
        //    try
        //    {
        //        if (fluArchivo.HasFile == true)
        //        {
        //            string FileName = Path.GetFileName(fluArchivo.PostedFile.FileName);
        //            string Extension = Path.GetExtension(fluArchivo.PostedFile.FileName);
        //            string FolderPath = "~/Files/";
        //            string FilePath = Server.MapPath(FolderPath + FileName);
        //            sArchivoExcel = FileName;

        //            if (!string.IsNullOrEmpty(FilePath))
        //                sArchivoSimulado = FilePath;

        //            if (Extension == ".xls" || Extension == ".xlsx" || Extension == ".XLS" || Extension == ".XLSX")
        //            {
        //                if (File.Exists(FilePath))
        //                    File.Delete(FilePath);

        //                if (!File.Exists(FilePath))
        //                    fluArchivo.SaveAs(FilePath);

        //                ImportToDataTable(FilePath, Extension);
        //                File.Delete(FilePath);
        //            }
        //            else
        //            {
        //                msgError.Visible = true;
        //                lblError.Text = "El tipo de archivo a procesar no es válido, se recomienda subir archivos Excel.";
        //            }
        //        }
        //        else
        //        {
        //            msgError.Visible = true;
        //            lblError.Text = "No ha seleccionado archivo a procesar, favor de verificar.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void ImportToDataTable(string FilePath, string Extension)
        //{
        //    try
        //    {
        //        msgError.Visible = false;
        //        msgSuccesss.Visible = false;
        //        lblError.Text = string.Empty;
        //        lblSuccess.Text = string.Empty;
        //        string conStr = "";
        //        string SheetName = string.Empty;
        //        bool bValidExcel = false;
        //        bool bValidacion = false;

        //        switch (Extension)
        //        {
        //            case ".xls": //Excel 97-03
        //                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";";
        //                break;
        //            case ".xlsx": //Excel 07, 2013, etc
        //                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
        //                break;
        //            case ".XLS": //Excel 97-03
        //                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";";
        //                break;
        //            case ".XLSX": //Excel 07, 2013, etc
        //                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
        //                break;
        //        }

        //        if (!string.IsNullOrEmpty(conStr))
        //        {
        //            conStr = String.Format(conStr, FilePath);
        //            OleDbConnection connExcel = new OleDbConnection(conStr);
        //            OleDbCommand cmdExcel = new OleDbCommand();
        //            OleDbDataAdapter oda = new OleDbDataAdapter();

        //            System.Data.DataTable dt = new System.Data.DataTable();
        //            cmdExcel.Connection = connExcel;
        //            //Obtiene el nombre de la primera hoja
        //            connExcel.Open();
        //            System.Data.DataTable dtExcelSchema;
        //            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //            connExcel.Close();

        //            string sQuery = string.Empty;
        //            sQuery = "SELECT * FROM [" + SheetName + "]";
        //            cmdExcel.CommandText = sQuery;
        //            oda.SelectCommand = cmdExcel;
        //            oda.Fill(dt);
        //            //dtExcel = dt;

        //            #region FORMATEAR DATATABLE

        //            //Elimina las columnas en blanco despues de la columna TOTAL
        //            int desiredSize = 116;

        //            while (dt.Columns.Count > desiredSize)
        //            {
        //                dt.Columns.RemoveAt(desiredSize);
        //            }

        //            dtExcel = FormatterData(dt);

        //            #endregion

        //            if (dtExcel.Columns.Count == 116)
        //            {
        //                #region Valida columnas y formato de Layout a Cargar
        //                bValidExcel = ValidarArchivo(dtExcel, "1");
        //                #endregion
        //                bool bValidaCampos = false;

        //                if (bValidExcel)
        //                {
        //                    bValidaCampos = ValidaDatosDataTable(dtExcel);

        //                    if (bValidaCampos)
        //                    {
        //                        pnlBotonesProcesar.Visible = true;
        //                        hdnValidacion.Value = "1";
        //                        btnInsertar.Enabled = true;
        //                        //gvDatos.DataSource = dtExcel;
        //                        //gvDatos.DataBind();
        //                    }
        //                    else
        //                    {
        //                        pnlBotonesProcesar.Visible = false;
        //                        hdnValidacion.Value = "0";
        //                        btnInsertar.Enabled = false;
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                msgSuccesss.Visible = false;
        //                lblSuccess.Visible = false;
        //                msgError.Visible = true;
        //                lblError.Visible = true;
        //                lblError.Text = "No se puede leer el archivo, verifique las columnas.";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected DataTable FormatterData(DataTable dt)
        //{
        //    try
        //    {
        //        dt.Columns[0].ColumnName = "Empresa";
        //        dt.Columns[1].ColumnName = "Periodo";
        //        dt.Columns[2].ColumnName = "Fecha";
        //        dt.Columns[3].ColumnName = "TipoMovimiento";
        //        dt.Columns[4].ColumnName = "Factura";
        //        dt.Columns[5].ColumnName = "TipoNomina";
        //        dt.Columns[6].ColumnName = "Nombre";
        //        dt.Columns[7].ColumnName = "RFC";
        //        dt.Columns[8].ColumnName = "SalarioMensual";
        //        dt.Columns[9].ColumnName = "Vales";
        //        dt.Columns[10].ColumnName = "HorasExtras";
        //        dt.Columns[11].ColumnName = "HorasExtrasTLC";
        //        dt.Columns[12].ColumnName = "HorasExtrasADN";
        //        dt.Columns[13].ColumnName = "HorasExtrasPend";
        //        dt.Columns[14].ColumnName = "IncidenciasNoRep";
        //        dt.Columns[15].ColumnName = "Faltas";
        //        dt.Columns[16].ColumnName = "Devolucion"; //Agregada
        //        dt.Columns[17].ColumnName = "DevolucionCredInfo";
        //        dt.Columns[18].ColumnName = "IncidenciasQ3MexJet"; //Agregada
        //        dt.Columns[19].ColumnName = "PagoPorIncapacidad";
        //        dt.Columns[20].ColumnName = "Compensacion";
        //        dt.Columns[21].ColumnName = "CompensacionPorRetro";
        //        dt.Columns[22].ColumnName = "CompensacionFijaP114";
        //        dt.Columns[23].ColumnName = "DevolucionPorFaltas"; //Agregado
        //        dt.Columns[24].ColumnName = "DevolucionPorPrestamo"; //Agregado
        //        dt.Columns[25].ColumnName = "Aguinaldo"; //Agregado
        //        dt.Columns[26].ColumnName = "DiasLaborados"; //Modificado Posicion
        //        dt.Columns[27].ColumnName = "SD"; //Agregado
        //        dt.Columns[28].ColumnName = "PagoPropina"; //Modificado Posicion
        //        dt.Columns[29].ColumnName = "Retroactivo";
        //        dt.Columns[30].ColumnName = "HorasDiasFestivos";
        //        dt.Columns[31].ColumnName = "HorasDiasFestivosTLC";
        //        dt.Columns[32].ColumnName = "HorasDiasFestivosADN";
        //        dt.Columns[33].ColumnName = "PrimaDominical";
        //        dt.Columns[34].ColumnName = "PrimaDominicalTLC";
        //        dt.Columns[35].ColumnName = "PrimaDominicalADN";
        //        dt.Columns[36].ColumnName = "PrimaVacacionalTLC";
        //        dt.Columns[37].ColumnName = "PrimaVacacionalADN"; //Agregado
        //        dt.Columns[38].ColumnName = "PrimaVacacional";
        //        dt.Columns[39].ColumnName = "INCXAntiguedad"; //Agregado
        //        dt.Columns[40].ColumnName = "PrimaAntiguedad";
        //        dt.Columns[41].ColumnName = "Bono";
        //        dt.Columns[42].ColumnName = "TotalIngresos";
        //        dt.Columns[43].ColumnName = "SalarioDiario";
        //        dt.Columns[44].ColumnName = "SalarioIntegrado";
        //        dt.Columns[45].ColumnName = "Sueldo";
        //        dt.Columns[46].ColumnName = "SeptimoDia";
        //        dt.Columns[47].ColumnName = "HorasExtras2";
        //        dt.Columns[48].ColumnName = "Destajos";
        //        dt.Columns[49].ColumnName = "PremioEficiencia";
        //        dt.Columns[50].ColumnName = "Vacaciones";
        //        dt.Columns[51].ColumnName = "PrimaVacacional2";
        //        dt.Columns[52].ColumnName = "Aguinaldo2";
        //        dt.Columns[53].ColumnName = "OtrasPercepciones";
        //        dt.Columns[54].ColumnName = "TotalPercepciones";
        //        dt.Columns[55].ColumnName = "RetInvVida";
        //        dt.Columns[56].ColumnName = "RetCesantia";
        //        dt.Columns[57].ColumnName = "RetEnfMatObrero";
        //        dt.Columns[58].ColumnName = "SeguroViviendaInfonavit";
        //        dt.Columns[59].ColumnName = "SubsEmpleoAcreditado";
        //        dt.Columns[60].ColumnName = "SubsidioEmpleo";
        //        dt.Columns[61].ColumnName = "ISRAntesSubsEmpleo";
        //        dt.Columns[62].ColumnName = "ISR_Art142"; //Agregado
        //        dt.Columns[63].ColumnName = "ISR_SP";
        //        dt.Columns[64].ColumnName = "IMSS";
        //        dt.Columns[65].ColumnName = "PrestamoInfonavit";
        //        dt.Columns[66].ColumnName = "PrestamoEmpresa"; //Modificado Posicion
        //        dt.Columns[67].ColumnName = "AjusteNeto";
        //        dt.Columns[68].ColumnName = "PensionAlimenticia";
        //        dt.Columns[69].ColumnName = "OtrasDeducciones";
        //        dt.Columns[70].ColumnName = "TotalDeducciones";
        //        dt.Columns[71].ColumnName = "Neto";
        //        dt.Columns[72].ColumnName = "NetoIMSSReal"; //Agregado
        //        dt.Columns[73].ColumnName = "ValesGratificacion";
        //        dt.Columns[74].ColumnName = "InvalidezVida";
        //        dt.Columns[75].ColumnName = "CesantiaVejez";
        //        dt.Columns[76].ColumnName = "EnfMatPatron";
        //        dt.Columns[77].ColumnName = "FondoRetiroSAR2PorCiento";
        //        dt.Columns[78].ColumnName = "ImpuestoEstatal3PorCiento";
        //        dt.Columns[79].ColumnName = "RiesgoTrabajo";
        //        dt.Columns[80].ColumnName = "IMSSEmpresa";
        //        dt.Columns[81].ColumnName = "InfonavitEmpresa";
        //        dt.Columns[82].ColumnName = "GuarderiaIMSS";
        //        dt.Columns[83].ColumnName = "OtrasObligaciones";
        //        dt.Columns[84].ColumnName = "TotalObligaciones";
        //        dt.Columns[85].ColumnName = "EmpresaAsimilados"; //Nombre Modificado
        //        dt.Columns[86].ColumnName = "Asimilados";
        //        dt.Columns[87].ColumnName = "ISR";
        //        dt.Columns[88].ColumnName = "TotalPagarAsimiladosSalario";
        //        dt.Columns[89].ColumnName = "PrestamoCompania";
        //        dt.Columns[90].ColumnName = "InteresesPrestamo";
        //        dt.Columns[91].ColumnName = "RecuperacionAsesores";
        //        dt.Columns[92].ColumnName = "GastosNoComprobados"; //Agregado
        //        dt.Columns[93].ColumnName = "DescuentoGMM";
        //        dt.Columns[94].ColumnName = "CursoIngles"; //Agregado
        //        dt.Columns[95].ColumnName = "DescuentoSportium";
        //        dt.Columns[96].ColumnName = "DescuentoPorMerma";
        //        dt.Columns[97].ColumnName = "DescuentoPorPagoDeMas"; //Agregado
        //        dt.Columns[98].ColumnName = "DescuentoPorFaltas"; //Modificado Posicion
        //        dt.Columns[99].ColumnName = "GastosNoComprobados2";
        //        dt.Columns[100].ColumnName = "Ayudante";
        //        dt.Columns[101].ColumnName = "DescuentoClienteOtros";
        //        dt.Columns[102].ColumnName = "Otros";
        //        dt.Columns[103].ColumnName = "PensionAlimencia2";
        //        dt.Columns[104].ColumnName = "NetoPagarAsimilados";
        //        dt.Columns[105].ColumnName = "TarjetaEmpresarialISR"; //Agregado
        //        dt.Columns[106].ColumnName = "SueldoIMSS";
        //        dt.Columns[107].ColumnName = "AsimiladosSalarios";
        //        dt.Columns[108].ColumnName = "TarjetaEmpresarialISR2"; //Agregado
        //        dt.Columns[109].ColumnName = "CargaPatronal";
        //        dt.Columns[110].ColumnName = "MARKUP7_5Porciento";
        //        dt.Columns[111].ColumnName = "FactorChequeCertSub";
        //        dt.Columns[112].ColumnName = "CostoEmpleado";
        //        dt.Columns[113].ColumnName = "Importe";
        //        dt.Columns[114].ColumnName = "IVA";
        //        dt.Columns[115].ColumnName = "TotalFactura";
        //        dt.AcceptChanges();
        //        return dt;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        protected bool ValidaDatosDataTable(DataTable dt)
        {
            try
            {
                ViewState["vsDataTable"] = null;
                msgSuccesss.Visible = false;
                lblSuccess.Text = string.Empty;
                msgError.Visible = false;
                lblError.Text = string.Empty;

                int iFila = 0;
                string strCampo = string.Empty;
                string strValor = string.Empty;
                string strExcepcion = string.Empty;
                int iStatus = 0;

                foreach (DataRow dRow in dt.Rows)
                {
                    iStatus = 1;
                    iFila = dt.Rows.IndexOf(dRow) + 2;

                    if (!string.IsNullOrEmpty(dRow["RFCEmpleado"].S()))
                    {
                        sRFC = dRow["RFCEmpleado"].S();

                        if (eSearchRFC != null)
                            eSearchRFC(null, null);

                        if (dtExistsRFC.Rows[0][0].S().I() == 0)
                        {
                            strCampo = "RFCEmpleado";
                            strValor = dRow["RFCEmpleado"].S();
                            strExcepcion = "El RFCEmpleado '" + strValor + "' no existe en la tabla [ALE_RH].";
                            iStatus = 0;
                        }
                    }
                    else
                    {
                        strCampo = "RFCEmpleado";
                        strValor = dRow["RFCEmpleado"].S();
                        strExcepcion = "El campo 'RFCEmpleado' viene vacio, favor de verificar";
                        iStatus = 0;
                    }

                    if (iStatus == 0)
                        dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                }

                gvResultado.DataSource = (System.Data.DataTable)ViewState["vsDataTable"];
                gvResultado.DataBind();

                if (gvResultado.Rows.Count > 0 && (System.Data.DataTable)ViewState["vsDataTable"] != null)
                {
                    int rowCount = gvResultado.Rows.Count;

                    if (rowCount > 0)
                    {
                        msgError.Visible = true;
                        lblError.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        btnInsertar.Enabled = false;
                        return false;
                    }
                    return false;
                }
                else
                {
                    msgSuccesss.Visible = true;
                    lblSuccess.Text = "El archivo cumple los requerimientos.";
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    btnInsertar.Enabled = true;
                    return true;
                }

                //return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void InsertarDatos(DataTable dt)
        {
            try
            {
                //string sNombreArchivo = string.Empty;
                //string sFechaIni = string.Empty;
                //string sFechaFin = string.Empty;
                string sUsuario = string.Empty;

                ////Datos de Header
                //sNombreArchivo = "PRUEBAS ABRIL";
                //sFechaIni = ("2020-04-01 00:00:00").S().Dt().ToShortDateString(); //txtFechaInicio.Text.Dt().ToShortDateString();
                //sFechaFin = ("2020-04-30 00:00:00").S().Dt().ToShortDateString(); //txtFechaFinal.Text.Dt().ToShortDateString();
                sUsuario = Utils.GetUserName;
                List<PolizaNominaNew> oLsPoliza = new List<PolizaNominaNew>();

                //Datos de Detalle
                if (dt != null && dt.Rows.Count > 0)
                {
                    //Llenado de Header
                    PolizaNominaNew oPoliza = new PolizaNominaNew();
                    //oPoliza.sArchivo = sNombreArchivo;
                    //oPoliza.dtFechaIni = sFechaIni.Dt();
                    //oPoliza.dtFechaFin = sFechaFin.Dt();
                    oPoliza.sUsuario = sUsuario;

                    //Llenado de Detalle
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DetallePolizaNominaNew oDetalle = new DetallePolizaNominaNew();
                        oDetalle.IdCounter = dt.Rows[i]["IdCounter"].S().I();
                        oDetalle.EmpresaFacturante = dt.Rows[i]["EmpresaFacturante"].S();
                        oDetalle.RFCEmpresaFacturante = dt.Rows[i]["RFCEmpresaFacturante"].S();
                        oDetalle.Periodo = dt.Rows[i]["Periodo"].S();
                        oDetalle.NombreEmpleado = dt.Rows[i]["NombreEmpleado"].S();
                        oDetalle.RFCEmpleado = dt.Rows[i]["RFCEmpleado"].S();
                        oDetalle.SalarioMensualBruto = dt.Rows[i]["SalarioMensualBruto"].S().D();
                        oDetalle.Vales = dt.Rows[i]["Vales"].S().D();
                        oDetalle.HorasExtrasDobles = dt.Rows[i]["HorasExtrasDobles"].S().D();
                        oDetalle.HorasExtrasTriples = dt.Rows[i]["HorasExtrasTriples"].S().D();
                        oDetalle.PagoPorIncapacidad = dt.Rows[i]["PagoPorIncapacidad"].S().D();
                        oDetalle.Compensacion = dt.Rows[i]["Compensacion"].S().D();
                        oDetalle.DescuentoPorFaltas = dt.Rows[i]["DescuentoPorFaltas"].S().D();
                        oDetalle.DiasLaborados = dt.Rows[i]["DiasLaborados"].S().D();
                        oDetalle.Retroactivo = dt.Rows[i]["Retroactivo"].S().D();
                        oDetalle.DiasFestivos = dt.Rows[i]["DiasFestivos"].S().D();
                        oDetalle.SalarioDiarioR = dt.Rows[i]["SalarioDiarioReal"].S().D();
                        oDetalle.PrimaDominical = dt.Rows[i]["PrimaDominical"].S().D();
                        oDetalle.AguinaldoReal = dt.Rows[i]["AguinaldoReal"].S().D();
                        oDetalle.PrimaVacacional = dt.Rows[i]["PrimaVacacional"].S().D();
                        oDetalle.PrimaAntiguedad = dt.Rows[i]["PrimaAntiguedad"].S().D();
                        oDetalle.Bono = dt.Rows[i]["Bono"].S().D();
                        oDetalle.TotalIngresos = dt.Rows[i]["TotalIngresos"].S().D();
                        oDetalle.SalarioDiarioFiscal = dt.Rows[i]["SalarioDiarioFiscal"].S().D();
                        oDetalle.SalarioIntegrado = dt.Rows[i]["SalarioIntegrado"].S().D();
                        oDetalle.Sueldo = dt.Rows[i]["Sueldo"].S().D();
                        oDetalle.SeptimoDia = dt.Rows[i]["SeptimoDia"].S().D();
                        oDetalle.HorasExtras = dt.Rows[i]["HorasExtras"].S().D();
                        oDetalle.Destajos = dt.Rows[i]["Destajos"].S().D();
                        oDetalle.VacacionesATiempo = dt.Rows[i]["VacacionesATiempo"].S().D();
                        oDetalle.PrimaVacacionalReportada = dt.Rows[i]["PrimaVacacionalReportada"].S().D();
                        oDetalle.Aguinaldo = dt.Rows[i]["Aguinaldo"].S().D();
                        oDetalle.ValesGratificacion = dt.Rows[i]["ValesGratificacion"].S().D();
                        oDetalle.OtrasPercepciones = dt.Rows[i]["OtrasPercepciones"].S().D();
                        oDetalle.TotalPercepciones = dt.Rows[i]["TotalPercepciones"].S().D();
                        oDetalle.RetiroInvalidezYVida = dt.Rows[i]["RetiroInvalidezYVida"].S().D();
                        oDetalle.RetencionCesantia = dt.Rows[i]["RetencionCesantia"].S().D();
                        oDetalle.RetiroEnfermedadYMaternidad = dt.Rows[i]["RetiroEnfermedadYMaternidad"].S().D();
                        oDetalle.SeguroViviendaInfonavit = dt.Rows[i]["SeguroViviendaInfonavit"].S().D();
                        oDetalle.SubsidioEmpleoAcreditado = dt.Rows[i]["SubsidioEmpleoAcreditado"].S().D();
                        oDetalle.SubsidioEmpleoSP = dt.Rows[i]["SubsidioEmpleoSP"].S().D();
                        oDetalle.ISRAntesDeSubsidioEmpleo = dt.Rows[i]["ISRAntesDeSubsidioEmpleo"].S().D();
                        oDetalle.ISRSP = dt.Rows[i]["ISRSP"].S().D();
                        oDetalle.IMSS = dt.Rows[i]["IMSS"].S().D();
                        oDetalle.PrestamoInfonavit = dt.Rows[i]["PrestamoInfonavit"].S().D();
                        oDetalle.AjusteNeto = dt.Rows[i]["AjusteNeto"].S().D();
                        oDetalle.PensionAlimenticia = dt.Rows[i]["PensionAlimenticia"].S().D();
                        oDetalle.OtrasDeducciones = dt.Rows[i]["OtrasDeducciones"].S().D();
                        oDetalle.TotalDeducciones = dt.Rows[i]["TotalDeducciones"].S().D();
                        oDetalle.Neto = dt.Rows[i]["Neto"].S().D();
                        oDetalle.RecuperacionPrestamo = dt.Rows[i]["RecuperacionPrestamo"].S().D();
                        oDetalle.SaldoRecuperacionPrestamo = dt.Rows[i]["SaldoRecuperacionPrestamo"].S().D();
                        oDetalle.PrestamoEmpresa = dt.Rows[i]["PrestamoEmpresa"].S().D();
                        oDetalle.SaldoPrestamoEmpresa = dt.Rows[i]["SaldoPrestamoEmpresa"].S().D();
                        oDetalle.InvalidezYVida = dt.Rows[i]["InvalidezYVida"].S().D();
                        oDetalle.CesantiaYVejez = dt.Rows[i]["CesantiaYVejez"].S().D();
                        oDetalle.EnfermedadYMaternidadPatron = dt.Rows[i]["EnfermedadYMaternidadPatron"].S().D();
                        oDetalle.GastosPensionados = dt.Rows[i]["GastosPensionados"].S().D();
                        oDetalle.FondoRetiroSAR2 = dt.Rows[i]["FondoRetiroSAR2"].S().D();
                        oDetalle.ImpuestoEstatal3 = dt.Rows[i]["ImpuestoEstatal3"].S().D();
                        oDetalle.RiesgoDeTrabajo = dt.Rows[i]["RiesgoDeTrabajo"].S().D();
                        oDetalle.IMSSEmpresa = dt.Rows[i]["IMSSEmpresa"].S().D();
                        oDetalle.InfonavitEmpresa = dt.Rows[i]["InfonavitEmpresa"].S().D();
                        oDetalle.GuarderiaIMSS = dt.Rows[i]["GuarderiaIMSS"].S().D();
                        oDetalle.OtrasObligaciones = dt.Rows[i]["OtrasObligaciones"].S().D();
                        oDetalle.TotalObligaciones = dt.Rows[i]["TotalObligaciones"].S().D();
                        oDetalle.SueldoIMSS = dt.Rows[i]["SueldoIMSS"].S().D();
                        oDetalle.MV = dt.Rows[i]["MV"].S().D();
                        oDetalle.CargaPatronal = dt.Rows[i]["CargaPatronal"].S().D();
                        oDetalle.MarkUpPatronal = dt.Rows[i]["MarkUpPatronal"].S().D();
                        oDetalle.MarkUpMV = dt.Rows[i]["MarkUpMV"].S().D();
                        oDetalle.ISN = dt.Rows[i]["ISN"].S().D();
                        oDetalle.SUBSIDIOCHEQUECERTIVA = dt.Rows[i]["SUBSIDIOCHEQUECERTIVA"].S().D();
                        oDetalle.CostoEmpleado = dt.Rows[i]["CostoEmpleado"].S().D();
                        oDetalle.fill = dt.Rows[i]["fill"].S().D();
                        oDetalle.Importe = dt.Rows[i]["Importe"].S().D();
                        oDetalle.IVA = dt.Rows[i]["IVA"].S().D();
                        oDetalle.SubTotalFactura = dt.Rows[i]["SubTotalFactura"].S().D();
                        oDetalle.Retencion = dt.Rows[i]["Retencion"].S().D();
                        oDetalle.TotalFactura = dt.Rows[i]["TotalFactura"].S().D();
                        oDetalle.NumeroFactura = dt.Rows[i]["NumeroFactura"].S().D();

                        oPoliza.oLstDetalle.Add(oDetalle);
                    }

                    oLsPoliza.Add(oPoliza);
                    ListaPolizas = oLsPoliza;

                    if (eNewProcesaArchivo != null)
                        eNewProcesaArchivo(null, null);


                    tmRedi.Enabled = true;


                    //if (iSuccess == 1)
                    //{
                    //    Response.Redirect("frmPolizaResult.aspx?Fol=" + iIdFolio.S());
                    //    //msgError.Visible = false;
                    //    //lblError.Visible = false;
                    //    //msgSuccesss.Visible = true;
                    //    //lblSuccess.Visible = true;
                    //    //lblSuccess.Text = "Se cargo correctamente el archivo.";
                    //}
                    //else
                    //{
                    //    msgSuccesss.Visible = false;
                    //    lblSuccess.Visible = false;
                    //    msgError.Visible = true;
                    //    lblError.Visible = true;
                    //    lblError.Text = "Error, no se pudo cargar el archivo.";
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidarArchivo(System.Data.DataTable dsFileExcel, string sLayout)
        {
            try
            {
                int iValidColumn;
                bool bValidExcel = false;
                string[] arrColumn1 = { "Empresa", "Periodo", "TipoMovimiento" };

                switch (sLayout)
                {
                    // Layout Poliza Nomina
                    case "1":
                        ViewState["VSLayout"] = arrColumn1;
                        for (int i = 0; i < arrColumn1.Length; i++)
                        {
                            iValidColumn = dsFileExcel.Columns.IndexOf(arrColumn1[i]);

                            if (iValidColumn == -1)
                            {
                                bValidExcel = false;
                                break;
                            }
                            else
                                bValidExcel = true;
                        }
                        break;
                    default:
                        break;
                }

                return bValidExcel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadRFC(DataTable dt)
        {
            try
            {
                dtExistsRFC = null;
                dtExistsRFC = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }

        public void ErrorRequireColumns()
        {
            if (ViewState["VSLayout"] != null)
            {
                string[] arrCamposReq = (string[])ViewState["VSLayout"];

                string sMsgRequeridos = string.Empty;

                sMsgRequeridos = @"<ul>";

                for (int i = 0; i < arrCamposReq.Length; i++)
                {
                    sMsgRequeridos += @"<li>" + arrCamposReq[i] + @"</li>";
                }
                sMsgRequeridos += @"</ul>";

                msgError.Visible = true;
                lblError.Text = @"El archivo seleccionado no contiene las columnas requeridas para el proceso, favor de verificar que contenga los siguientes campos:" + sMsgRequeridos;
                //Mensaje(" \n");
            }
            else
            {
                msgError.Visible = false;
                lblError.Text = string.Empty;
            }
        }

        public void dtRow(int iFila, string strCampo, string strValor, int iStatus, string strExcepcion)
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
                //else if (System.Web.HttpContext.Current.Session["SErrores"] != null)
                //    bBandera = true;

                // Verificamos si nuestro DataTable en la variable de estado contiene datos
                if (bBandera == false)
                {
                    // Creación de nueva DataColumn, typo de dato y Nombre de la columna.
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "Fila";
                    dtNew.Columns.Add(column);

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

                    row["Fila"] = iFila;
                    row["Campo"] = strCampo;
                    row["Valor"] = strValor;

                    if (iStatus == 0)
                        row["Status"] = "Error";
                    else
                        row["Status"] = "Válido";

                    row["Descripcion"] = strExcepcion;
                    dtNew.Rows.Add(row);

                    ViewState["vsDataTable"] = dtNew;
                    //System.Web.HttpContext.Current.Session["SErrores"] = dtNew;
                    view = new DataView(dtNew);
                    pnlSimulacion.Visible = true;
                }
                else
                {
                    dt = (System.Data.DataTable)ViewState["vsDataTable"];
                    //dt = (System.Data.DataTable)System.Web.HttpContext.Current.Session["SErrores"];
                    row = dt.NewRow();

                    row["Fila"] = iFila;
                    row["Campo"] = strCampo;
                    row["Valor"] = strValor;

                    if (iStatus == 0)
                        row["Status"] = "Error";
                    else
                        row["Status"] = "Válido";

                    row["Descripcion"] = strExcepcion;
                    dt.Rows.Add(row);
                    ViewState["vsDataTable"] = dt;
                    //System.Web.HttpContext.Current.Session["SErrores"] = dt;
                    view = new DataView(dt);
                    pnlSimulacion.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        public void EliminarProceso()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName) && p.StartTime.AddSeconds(+120) > DateTime.Now)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        private void CargaDatosIniciales()
        {
            try
            {
                if (dtExiste.Rows.Count > 0)
                {
                    string sMes = string.Empty;
                    if (dtExiste.Rows[0]["Resultado"].S() == "1")
                    {
                        btnLeer.Visible = false;
                        sMes = ObtieneNombreMes(dtExiste.Rows[0]["Mes"].S().I());
                        iFolioExistente = dtExiste.Rows[0]["IdFolio"].S().I();
                        iIdFolio = iFolioExistente;
                        lblPeriodoConsultar.Text = "Información de " + sMes + " " + dtExiste.Rows[0]["Anio"].S() + " ";
                        iMes = dtExiste.Rows[0]["Mes"].S().I();
                        btnSobreescribir.Visible = true;
                        pnlSobreescribir.Visible = true;
                    }
                    else
                    {
                        btnLeer.Visible = true;
                        sMes = ObtieneNombreMes(DateTime.Now.AddMonths(-2).Month);
                        btnRedirigir.Visible = false;
                        lblPeriodoConsultar.Text = " " + sMes + " " + dtExiste.Rows[0]["Anio"].S();
                        iFolioExistente = 0;
                        iMes = dtExiste.Rows[0]["Mes"].S().I();
                    }
                }
                else
                {
                    MostrarMensaje("Error al obtener el periodo extraido de ABACO", "Aviso");
                }
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

        private string ObtieneNombreMesReporte(int iMes)
        {
            string smes = string.Empty;

            switch (iMes)
            {
                case 1:
                    smes = "enero";
                    break;
                case 2:
                    smes = "febrero";
                    break;
                case 3:
                    smes = "marzo";
                    break;
                case 4:
                    smes = "abril";
                    break;
                case 5:
                    smes = "mayo";
                    break;
                case 6:
                    smes = "junio";
                    break;
                case 7:
                    smes = "julio";
                    break;
                case 8:
                    smes = "agosto";
                    break;
                case 9:
                    smes = "septiembre";
                    break;
                case 10:
                    smes = "octubre";
                    break;
                case 11:
                    smes = "noviembre";
                    break;
                case 12:
                    smes = "diciembre";
                    break;
            }

            return smes;
        }

       
        #endregion

        #region VARIABLES Y PROPIEDADES
        private const string sClase = "frmPolizaNomina.aspx.cs";
        private const string sPagina = "frmPolizaNomina.aspx";

        PolizaNomina_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eNewProcesaArchivo;
        public event EventHandler eSearchRFC;
        //public event EventHandler eEliminaNominaABACO;

        public string sArchivoSimulado
        {
            get { return (string)ViewState["VSArchivoSimulado"]; }
            set { ViewState["VSArchivoSimulado"] = value; }
        }
        public string sArchivoExcel
        {
            get { return (string)ViewState["VSArchivoExcel"]; }
            set { ViewState["VSArchivoExcel"] = value; }
        }
        public string sRFC
        {
            get { return (string)ViewState["VSRFC"]; }
            set { ViewState["VSRFC"] = value; }
        }
        public int iSuccess
        {
            get { return (int)ViewState["VSSuccess"]; }
            set { ViewState["VSSuccess"] = value; }
        }
        public DataTable dtExcel
        {
            get { return (DataTable)ViewState["VSdtExcel"]; }
            set { ViewState["VSdtExcel"] = value; }
        }
        public DataTable dtData
        {
            get { return (DataTable)ViewState["VSdtData"]; }
            set { ViewState["VSdtData"] = value; }
        }
        public DataTable dtExistsRFC
        {
            get { return (DataTable)ViewState["VSExistsRFC"]; }
            set { ViewState["VSExistsRFC"] = value; }
        }
        public int iIdFolio
        {
            get { return (int)Session["VSIdFolio"]; }
            set { Session["VSIdFolio"] = value; }
        }
        public List<PolizaNominaNew> ListaPolizas
        {
            set { ViewState["VSListaPolizas"] = value; }
            get { return (List<PolizaNominaNew>)ViewState["VSListaPolizas"]; }
        }
        public DataTable dtExiste
        {
            set { ViewState["VSdtExiste"] = value; }
            get { return (DataTable)ViewState["VSdtExiste"]; }
        }
        public int iFolioExistente
        {
            get { return (int)ViewState["VSFolioExistente"]; }
            set { ViewState["VSFolioExistente"] = value; }
        }
        public int iMes
        {
            get { return (int)ViewState["VSiMes"]; }
            set { ViewState["VSiMes"] = value; }
        }
        #endregion
    }
}