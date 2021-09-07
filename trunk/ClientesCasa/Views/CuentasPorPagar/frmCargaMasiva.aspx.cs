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

using Microsoft.Office.Interop.Excel;
using NucleoBase.Core;
using System.Globalization;
using ClientesCasa.Clases;
using System.Xml;

namespace ClientesCasa.Views.CuentasPorPagar
{
    public partial class frmCargaMasiva : System.Web.UI.Page, IViewCargaMasiva
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new CargaMasiva_Presenter(this, new DBAccesoSAP());

                //ViewState["vsDataTable"] = null;
                //dtMainErrores = null;
                
                if (!IsPostBack)
                {
                    //DateTime dtNow = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(ex.Message.S(), sPagina, sClase, "Page_Load", "Error");
            }

        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                //Provicional Leer XML
                //ObtenerDatosXML(string.Empty);

                iBanCorrecto = 0;

                #region Layout Genérico
                if (hdnSeleccionFormato.Value == "1")
                {
                    if (ddlBase.SelectedValue != "0")
                    {
                        msgError.Visible = false;
                        lblError.Text = string.Empty;
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        ValidarFileUpload();

                        if (iBanCorrecto == 1 && hdnSeleccionGral.Value == "2")
                        {
                            msgSuccesss.Visible = true;
                            lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                            msgError.Visible = false;
                            lblError.Text = string.Empty;
                            sMsgError = string.Empty;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(sMsgError))
                            {
                                msgSuccesss.Visible = false;
                                lblSuccess.Text = string.Empty;
                                msgError.Visible = true;
                                lblError.Text = sMsgError;
                            }
                        }
                    }
                    else
                    {
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        msgError.Visible = true;
                        lblError.Text = "Se requiere seleccionar el campo BASE para procesar el archivo";
                        //Mensaje("Se requiere seleccionar el campo BASE para procesar el archivo");
                    }
                }
                #endregion

                #region Layout OMA
                if (hdnSeleccionFormato.Value == "2")
                {
                    EliminarProceso();

                    if (ddlBase.SelectedValue != "0")
                    {
                        if (!string.IsNullOrEmpty(txtFechaContable.Text))
                        {
                            if (!string.IsNullOrEmpty(txtIATA.Text))
                            {
                                msgError.Visible = false;
                                lblError.Text = string.Empty;
                                msgSuccesss.Visible = false;
                                lblSuccess.Text = string.Empty;
                                ValidarFileUpload();

                                if (iBanCorrecto == 1)
                                {
                                    msgSuccesss.Visible = true;
                                    lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                                    msgError.Visible = false;
                                    lblError.Text = string.Empty;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(sMsgError))
                                    {
                                        msgSuccesss.Visible = false;
                                        lblSuccess.Text = string.Empty;
                                        msgError.Visible = true;
                                        lblError.Text = sMsgError;
                                    }
                                }
                            }
                            else
                            {
                                msgSuccesss.Visible = false;
                                lblSuccess.Text = string.Empty;
                                msgError.Visible = true;
                                lblError.Text = "Se requiere ingresar las siglas del Aeropuerto IATA.";
                            }
                        }
                        else
                        {
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                            msgError.Visible = true;
                            lblError.Text = "Se requiere ingresar la Fecha de Contabilización para procesar el archivo";
                        }
                    }
                    else
                    {
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        msgError.Visible = true;
                        lblError.Text = "Se requiere seleccionar el campo BASE para procesar el archivo";
                        //Mensaje("Se requiere seleccionar el campo BASE para procesar el archivo");
                    }
                }
                #endregion

                #region Layout AMAIT
                if (hdnSeleccionFormato.Value == "3")
                {
                    if (ddlBase.SelectedValue != "0")
                    {
                        if (!string.IsNullOrEmpty(txtFechaContable.Text))
                        {
                            msgError.Visible = false;
                            lblError.Text = string.Empty;
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                            ValidarFileUpload();

                            if (iBanCorrecto == 1)
                            {
                                msgSuccesss.Visible = true;
                                lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                                msgError.Visible = false;
                                lblError.Text = string.Empty;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sMsgError))
                                {
                                    msgSuccesss.Visible = false;
                                    lblSuccess.Text = string.Empty;
                                    msgError.Visible = true;
                                    lblError.Text = sMsgError;
                                }
                            }
                        }
                        else
                        {
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                            msgError.Visible = true;
                            lblError.Text = "Se requiere ingresar la Fecha de Contabilización para procesar el archivo";
                        }
                    }
                    else
                    {
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        msgError.Visible = true;
                        lblError.Text = "Se requiere seleccionar el campo BASE para procesar el archivo";
                    }
                }
                #endregion

                #region Layout ASA
                if (hdnSeleccionFormato.Value == "4")
                {
                    if (!string.IsNullOrEmpty(txtFechaContable.Text))
                    {
                        msgError.Visible = false;
                        lblError.Text = string.Empty;
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        ValidarFileUpload();
                        if (iBanCorrecto == 1)
                        {
                            msgSuccesss.Visible = true;
                            lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                            msgError.Visible = false;
                            lblError.Text = string.Empty;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(sMsgError))
                            {
                                msgSuccesss.Visible = false;
                                lblSuccess.Text = string.Empty;
                                msgError.Visible = true;
                                lblError.Text = sMsgError;
                                pnlRegASA.Visible = false;
                            }
                            else
                            {
                                //msgSuccesss.Visible = false;
                                //lblSuccess.Text = string.Empty;
                                msgError.Visible = false;
                                lblError.Text = string.Empty;
                                pnlRegASA.Visible = true;
                                pnlSimulacion.Visible = false;
                            }
                        }
                        
                    }
                    else
                    {
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        msgError.Visible = true;
                        lblError.Text = "Se requiere ingresar la Fecha de Contabilización para procesar el archivo";
                    }
                }
                #endregion

                #region Layout ASUR
                if (hdnSeleccionFormato.Value == "5")
                {
                    if (ddlBase.SelectedValue != "0")
                    {
                        if (!string.IsNullOrEmpty(txtFechaContable.Text))
                        {
                            msgError.Visible = false;
                            lblError.Text = string.Empty;
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                            ValidarFileUpload();

                            if (iBanCorrecto == 1)
                            {
                                msgSuccesss.Visible = true;
                                lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                                msgError.Visible = false;
                                lblError.Text = string.Empty;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sMsgError))
                                {
                                    msgSuccesss.Visible = false;
                                    lblSuccess.Text = string.Empty;
                                    msgError.Visible = true;
                                    lblError.Text = sMsgError;
                                    pnlRegASUR.Visible = true;
                                }
                                else
                                {
                                    msgError.Visible = false;
                                    lblError.Text = string.Empty;
                                    pnlRegASUR.Visible = true;
                                    pnlSimulacion.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                            msgError.Visible = true;
                            lblError.Text = "Se requiere ingresar la Fecha de Contabilización para procesar el archivo";
                        }
                    }
                    else
                    {
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                        msgError.Visible = true;
                        lblError.Text = "Se requiere seleccionar el campo BASE para procesar el archivo";
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(ex.Message.S(), sPagina, sClase, "btnProcesar_Click", "Error");
                msgError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        protected void rdnSeleccionGral_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdnSeleccionGral.SelectedValue == "1")
                {
                    hdnSeleccionGral.Value = "1";
                    pnlSeleccionFormato.Visible = true;
                    pnlSeleccionGral.Visible = false;
                    pnlSimulacion.Visible = false;
                    lblProceso.Text = "Proceso de Simulación";
                }
                else if (rdnSeleccionGral.SelectedValue == "2")
                {
                    hdnSeleccionGral.Value = "2";
                    pnlSeleccionFormato.Visible = true;
                    pnlSeleccionGral.Visible = false;
                    pnlSimulacion.Visible = false;
                    lblProceso.Text = "Proceso de Carga Masiva";
                }
                btnRegresar.Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(ex.Message.S(), sPagina, sClase, "rdnSeleccionGral_SelectedIndexChanged", "Error");
            }
        }
        protected void rdnSeleccionFormato_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdnSeleccionFormato.SelectedValue == "1") //Layout Genérico
                {
                    lblTituloProceso.Text = rdnSeleccionFormato.SelectedItem.Text;
                    hdnSeleccionFormato.Value = "1";
                    pnlCargarArchivo.Visible = true;
                    lblBase.Visible = true;
                    ddlBase.Visible = true;
                    hylPlantilla.Visible = true;
                    msgSuccesss.Visible = false;
                    lblSuccess.Text = string.Empty;
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    pnlSimulacion.Visible = false;
                    btnRegresar.Visible = true;
                    lblDescarga.Visible = true;
                    ddlBase.ClearSelection();
                    pnlRegASA.Visible = false;
                    lblFechaContable.Visible = false;
                    txtFechaContable.Visible = false;
                    txtFechaContable.Text = string.Empty;
                    lblComentarios.Visible = true;
                    txtComentarios.Text = string.Empty;
                    txtComentarios.Visible = true;
                    lblSelect.Visible = false;
                    lblAeropuerto.Visible = false;
                    txtIATA.Visible = false;
                    txtIATA.Text = string.Empty;

                    lblArchivoSimulado.Visible = false;
                    lblArchivoSimulado.Text = string.Empty;
                    btnProcesarCargaSimulada.Visible = false;
                    pnlExplorarRepositorioASUR.Visible = false;
                }
                else if (rdnSeleccionFormato.SelectedValue == "2") //OMA
                {
                    lblTituloProceso.Text = rdnSeleccionFormato.SelectedItem.Text;
                    hdnSeleccionFormato.Value = "2";
                    pnlCargarArchivo.Visible = true;
                    lblBase.Visible = true;
                    ddlBase.Visible = true;
                    hylPlantilla.Visible = false;
                    msgSuccesss.Visible = false;
                    lblSuccess.Text = string.Empty;
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    pnlSimulacion.Visible = false;
                    btnRegresar.Visible = true;
                    lblDescarga.Visible = false;
                    ddlBase.ClearSelection();
                    pnlRegASA.Visible = false;
                    lblFechaContable.Visible = true;
                    txtFechaContable.Visible = true;
                    txtFechaContable.Text = string.Empty;
                    lblComentarios.Visible = true;
                    txtComentarios.Text = string.Empty;
                    txtComentarios.Visible = true;
                    lblComentarios.Text = "Referencia:";
                    lblSelect.Visible = false;
                    lblAeropuerto.Visible = true;
                    txtIATA.Visible = true;
                    txtIATA.Text = string.Empty;

                    lblArchivoSimulado.Visible = false;
                    lblArchivoSimulado.Text = string.Empty;
                    btnProcesarCargaSimulada.Visible = false;
                    pnlExplorarRepositorioASUR.Visible = false;
                }
                else if (rdnSeleccionFormato.SelectedValue == "3") //AMAIT
                {
                    lblTituloProceso.Text = rdnSeleccionFormato.SelectedItem.Text;
                    hdnSeleccionFormato.Value = "3";
                    pnlCargarArchivo.Visible = true;
                    lblBase.Visible = true;
                    ddlBase.Visible = true;
                    hylPlantilla.Visible = false;
                    msgSuccesss.Visible = false;
                    lblSuccess.Text = string.Empty;
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    pnlSimulacion.Visible = false;
                    btnRegresar.Visible = true;
                    lblDescarga.Visible = false;
                    ddlBase.ClearSelection();
                    pnlRegASA.Visible = false;
                    lblFechaContable.Visible = true;
                    txtFechaContable.Visible = true;
                    txtFechaContable.Text = string.Empty;
                    lblComentarios.Visible = true;
                    txtComentarios.Text = string.Empty;
                    txtComentarios.Visible = true;
                    lblComentarios.Text = "Referencia:";
                    lblSelect.Visible = false;
                    lblAeropuerto.Visible = false;
                    txtIATA.Visible = false;
                    txtIATA.Text = string.Empty;

                    lblArchivoSimulado.Visible = false;
                    lblArchivoSimulado.Text = string.Empty;
                    btnProcesarCargaSimulada.Visible = false;
                    pnlExplorarRepositorioASUR.Visible = false;
                }
                else if (rdnSeleccionFormato.SelectedValue == "4") //Layout ASA
                {
                    lblTituloProceso.Text = rdnSeleccionFormato.SelectedItem.Text;
                    hdnSeleccionFormato.Value = "4";
                    pnlCargarArchivo.Visible = true;
                    lblBase.Visible = false;
                    ddlBase.Visible = false;
                    hylPlantilla.Visible = false;
                    msgSuccesss.Visible = false;
                    lblSuccess.Text = string.Empty;
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    pnlSimulacion.Visible = false;
                    btnRegresar.Visible = true;
                    lblDescarga.Visible = false;
                    ddlBase.ClearSelection();
                    pnlRegASA.Visible = false;
                    lblFechaContable.Visible = true;
                    txtFechaContable.Visible = true;
                    txtFechaContable.Text = string.Empty;
                    lblComentarios.Visible = true;
                    txtComentarios.Text = string.Empty;
                    txtComentarios.Visible = true;
                    lblSelect.Visible = true;
                    lblSelect.Text = "Seleccionar Remesa:";
                    lblAeropuerto.Visible = false;
                    txtIATA.Visible = false;
                    txtIATA.Text = string.Empty;

                    lblArchivoSimulado.Visible = false;
                    lblArchivoSimulado.Text = string.Empty;
                    btnProcesarCargaSimulada.Visible = false;
                    pnlExplorarRepositorioASUR.Visible = false;
                }

                else if (rdnSeleccionFormato.SelectedValue == "5") //ASUR
                {
                    lblTituloProceso.Text = rdnSeleccionFormato.SelectedItem.Text;
                    hdnSeleccionFormato.Value = "5";
                    pnlCargarArchivo.Visible = false; // Se modifico
                    lblBase.Visible = true;
                    ddlBase.Visible = true;
                    hylPlantilla.Visible = false;
                    msgSuccesss.Visible = false;
                    lblSuccess.Text = string.Empty;
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    pnlSimulacion.Visible = false;
                    btnRegresar.Visible = true;
                    lblDescarga.Visible = false;
                    ddlBase.ClearSelection();
                    pnlRegASA.Visible = false;
                    lblFechaContable.Visible = true;
                    txtFechaContable.Visible = true;
                    txtFechaContable.Text = string.Empty;
                    lblComentarios.Visible = true;
                    txtComentarios.Text = string.Empty;
                    lblComentarios.Text = "Comentarios:";
                    txtComentarios.Visible = true;
                    lblSelect.Visible = false;
                    lblAeropuerto.Visible = false;
                    txtIATA.Visible = false;
                    txtIATA.Text = string.Empty;

                    lblArchivoSimulado.Visible = false;
                    lblArchivoSimulado.Text = string.Empty;
                    btnProcesarCargaSimulada.Visible = false;
                    pnlExplorarRepositorioASUR.Visible = true; // Se modifico y ahora no se debe de mostrar esta parte
                }
                btnRegresar.Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(ex.Message.S(), sPagina, sClase, "rdnSeleccionFormato_SelectedIndexChanged", "Error");
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlSeleccionFormato.Visible = false;
            pnlSeleccionGral.Visible = true;
            btnRegresar.Visible = false;
            lblBase.Visible = false;
            ddlBase.Visible = false;
            hylPlantilla.Visible = false;
            pnlSimulacion.Visible = false;
            lblProceso.Text = string.Empty;
            pnlCargarArchivo.Visible = false;
            rdnSeleccionGral.ClearSelection();
            rdnSeleccionFormato.ClearSelection();
            lblDescarga.Visible = false;
            pnlRegASA.Visible = false;
            lblFechaContable.Visible = false;
            txtFechaContable.Visible = false;
            txtFechaContable.Text = string.Empty;
            lblComentarios.Visible = false;
            txtComentarios.Text = string.Empty;
            txtComentarios.Visible = false;
            lblSelect.Visible = false;
            lblAeropuerto.Visible = false;
            txtIATA.Visible = false;
            txtIATA.Text = string.Empty;
            pnlRegASUR.Visible = false;
            msgSuccessAsur.Visible = false;
            msgErrorAsur.Visible = false;
            msgError.Visible = false;
            msgSuccesss.Visible = false;
            pnlExplorarRepositorioASUR.Visible = false;
        }
        protected void lkbExport_Click(object sender, EventArgs e)
        {
            try
            {
                string sFechaActual = string.Empty;
                string sTime = string.Empty;
                sFechaActual = FormatoFecha(DateTime.Now.ToShortDateString());
                sTime = DateTime.Now.ToShortTimeString().Replace(":", "").Replace(" p. m.", "PM").Replace(" a. m.", "AM");

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=Facturas_NoEncontradas_" + sFechaActual + "_" + sTime + ".xls");
                Response.Charset = "";
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                gvNoProcesados.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(ex.Message.S(), sPagina, sClase, "lkbExport_Click", "Error");
            }
        }

        #endregion

        #region MÉTODOS

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
        protected void ValidarFileUpload()
        {
            try
            {
                if (fluArchivo.HasFile == true)
                {
                    string FileName = Path.GetFileName(fluArchivo.PostedFile.FileName);
                    string Extension = Path.GetExtension(fluArchivo.PostedFile.FileName);
                    string FolderPath = "~/Files/";

                    string FilePath = Server.MapPath(FolderPath + FileName);

                    if (!string.IsNullOrEmpty(FilePath))
                        sArchivoSimulado = FilePath;

                    if (Extension == ".xls" || Extension == ".xlsx" || Extension == ".csv" || Extension == ".CSV" || Extension == ".xml" || Extension == ".XML")
                    {
                        if (File.Exists(FilePath))
                            File.Delete(FilePath);

                        if (!File.Exists(FilePath))
                            fluArchivo.SaveAs(FilePath);

                        Import_To_DataTable(FilePath, Extension);
                        File.Delete(FilePath);
                    }
                    else
                    {
                        msgError.Visible = true;
                        lblError.Text = "El tipo de archivo a procesar no es válido, se recomienda subir archivos Excel.";
                        //Mensaje("El tipo de archivo a procesar no es válido, se recomienda subir archivos Excel.");
                    }
                }
                else
                {
                    msgError.Visible = true;
                    lblError.Text = "No ha seleccionado archivo a procesar, favor de verificar.";
                    //Mensaje("No ha seleccionado archivo a procesar, favor de verificar.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Import_To_DataTable(string FilePath, string Extension)
        {
            try
            {
                bool bCopyFiles = false;
                msgError.Visible = false;
                msgSuccesss.Visible = false;
                lblError.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                pnlRegASA.Visible = false;

                string conStr = "";
                bool bValidExcel = false;
                string SheetName = string.Empty;
                bool bAsur = false;

                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";";
                        break;
                    case ".xlsx": //Excel 07, 2013, etc
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
                        break;
                    case ".XML": //Lectura de XML
                        //case ".CSV": //Excel delimitado por comas ","
                        //conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + "@" + FilePath + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited\";";
                        bAsur = true;
                        break;
                }

                conStr = String.Format(conStr, FilePath);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();

                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.DataTable dtDatosOMA = new System.Data.DataTable();
                System.Data.DataTable dtUnionAMAIT = new System.Data.DataTable();
                System.Data.DataTable dtRemesa = new System.Data.DataTable();
                System.Data.DataTable dtDatosASUR = new System.Data.DataTable();

                //cmdExcel.Connection = connExcel;
                ////Obtiene el nombre de la primera hoja
                //connExcel.Open();
                //System.Data.DataTable dtExcelSchema;
                //dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                if (bAsur != true)
                {
                    cmdExcel.Connection = connExcel;
                    //Obtiene el nombre de la primera hoja
                    connExcel.Open();
                    System.Data.DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();
                }

                #region FORMATO GENÉRICO
                if (hdnSeleccionFormato.Value == "1") // Formato Layout Genérico
                {
                    string sQuery = string.Empty;
                    //sQuery = "SELECT ";
                    //sQuery += "[CODIGO PROVEEDOR],[MONEDA],[CODIGO ARTICULO],[ALMACEN],[LUGAR],[FACTURA],[FECHA DOC (añomesdia)],";
                    //sQuery += "[FECHA CONTA SBO (añomesdia)],[FECHA OPERACIÓN],[MATRICULA/DEPARTAMENTO],";
                    //sQuery += "[SITE],[CANTIDAD],[UNIDAD MEDIDA],[IMPORTE],[CODIGO IMPUESTO],[Tipo],[DOCUMENTO A MODIFICAR],[Comments] ";
                    //sQuery += "FROM [" + SheetName + "]";
                    sQuery = "SELECT * FROM [" + SheetName + "]";
                    cmdExcel.CommandText = sQuery;
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dt);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (string.IsNullOrEmpty(dt.Rows[i][0].S()) && string.IsNullOrEmpty(dt.Rows[i][2].S()) && string.IsNullOrEmpty(dt.Rows[i][5].S()) && string.IsNullOrEmpty(dt.Rows[i][9].S()))
                                {
                                    dt.Rows[i].Delete();
                                }
                            }
                            dt.AcceptChanges();
                        }
                    }

                    if (dt.Columns.IndexOf("PDF") > -1)
                        dt.Columns.Remove("PDF");
                    if (dt.Columns.IndexOf("XML") > -1)
                        dt.Columns.Remove("XML");
                    if (dt.Columns.IndexOf("UUID") > -1)
                        dt.Columns.Remove("UUID");
                    if (dt.Columns.IndexOf("F20") > -1)
                        dt.Columns.Remove("F20");
                    if (dt.Columns.IndexOf("F21") > -1)
                        dt.Columns.Remove("F21");
                    if (dt.Columns.IndexOf("F22") > -1)
                        dt.Columns.Remove("F22");
                    if (dt.Columns.IndexOf("F23") > -1)
                        dt.Columns.Remove("F23");

                    connExcel.Close();
                    dtExcel = dt;
                }
                #endregion

                #region FORMATO OMA
                else if (hdnSeleccionFormato.Value == "2") //Formato OMA
                {
                    string sFecF = string.Empty;
                    dtHeaderRows = null;

                    if (dtHeaderRows == null)
                    {
                        //EliminarProceso();
                        //Finaliza hoja al obtener información de la celda
                        cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "A2:D]";
                        oda.SelectCommand = cmdExcel;
                        oda.Fill(dt);

                        //Construcción de Header
                        string sPrefijo = string.Empty;
                        sPrefijo = txtIATA.Text;

                        sEmpresa = dt.Rows[0][2].S();
                        sFactura = dt.Rows[2][2].S();
                        sFechaFactura = dt.Rows[3][2].S(); //sFecF;
                        dtHeaderRows = dtRowHeader(sEmpresa, sFactura, sFechaFactura);

                        if (dtHeaderRows != null)
                        {
                            if (dtHeaderRows.Rows.Count > 0)
                            {
                                //cmdExcel.CommandText = "SELECT F1 AS FOLIO, SERVICIO, MATRICULA, F5 AS FACTURA, [PERIODO INICIAL], [IMPORTE TOTAL]  FROM [" + SheetName + "A9:X]";
                                cmdExcel.CommandText = "SELECT F1 AS FOLIO, SERVICIO, MATRICULA, [PERIODO INICIAL], [IMPORTE TOTAL]  FROM [" + SheetName + "A9:X]";
                                oda.SelectCommand = cmdExcel;
                                oda.Fill(dtDatosOMA);

                                foreach (DataRow rw in dtDatosOMA.Rows)
                                {
                                    if (string.IsNullOrEmpty(rw["FOLIO"].S()))
                                        rw.Delete();
                                }
                                dtDatosOMA.AcceptChanges();

                                //Lee prefijo de la columa 
                                //if (dtDatosOMA.Columns.Contains("FACTURA"))
                                //{
                                //    if (!string.IsNullOrEmpty(dtDatosOMA.Rows[0]["FACTURA"].S()))
                                //    {
                                //        sPrefijo = dtDatosOMA.Rows[0]["FACTURA"].S().Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").Replace("9", "");
                                //    }
                                //    dtDatosOMA.Columns.Remove("FACTURA");
                                //}
                                //dtDatosOMA.AcceptChanges();
                            }
                        }

                        sFactura = "B" + sPrefijo.ToUpper() + sFactura; //Armado de factura
                    }
                }


                #endregion

                #region FORMATO AMAIT
                else if (hdnSeleccionFormato.Value == "3")
                {
                    System.Data.DataTable dtDatosAMAIT_First = new System.Data.DataTable();
                    dtDatosAMAIT_First.Columns.Add("FECHA", typeof(string));
                    dtDatosAMAIT_First.Columns.Add("FACTURA", typeof(string));
                    dtDatosAMAIT_First.Columns.Add("IMPORTE", typeof(string));
                    dtDatosAMAIT_First.Columns.Add("MATRICULA", typeof(string));

                    System.Data.DataTable dtDatosAMAIT_Second = new System.Data.DataTable();
                    dtDatosAMAIT_Second.Columns.Add("FECHA", typeof(string));
                    dtDatosAMAIT_Second.Columns.Add("FACTURA", typeof(string));
                    dtDatosAMAIT_Second.Columns.Add("IMPORTE", typeof(string));
                    dtDatosAMAIT_Second.Columns.Add("MATRICULA", typeof(string));

                    int iFound = SheetName.IndexOf(" ");

                    if (iFound != -1)
                        SheetName = SheetName.Replace("'", "");

                    cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "B9:E]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dtDatosAMAIT_First);

                    cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "G9:J]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dtDatosAMAIT_Second);

                    connExcel.Close();

                    foreach (DataRow rw in dtDatosAMAIT_First.Rows)
                    {
                        if (string.IsNullOrEmpty(rw["FACTURA"].S()))
                            rw.Delete();
                    }
                    dtDatosAMAIT_First.AcceptChanges();

                    foreach (DataRow rw in dtDatosAMAIT_Second.Rows)
                    {
                        if (string.IsNullOrEmpty(rw["FACTURA"].S()))
                            rw.Delete();
                    }
                    dtDatosAMAIT_Second.AcceptChanges();

                    dtDatosAMAIT_First = ModificarOrigen(dtDatosAMAIT_First);
                    dtDatosAMAIT_Second = ModificarOrigen(dtDatosAMAIT_Second);
                    // Union de la información de AMAIT
                    dtDatosAMAIT_First.Merge(dtDatosAMAIT_Second);
                    dtUnionAMAIT = dtDatosAMAIT_First;
                }
                #endregion

                #region FORMATO ASA
                else if (hdnSeleccionFormato.Value == "4")
                {
                    System.Data.DataTable dtASA = new System.Data.DataTable();
                    //string FolderPath = @"\FACTURAS";
                    string sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASA"].S() + @"Archivos"; //@"C:\Users\Administrador\Documents\"; 

                    bCopyFiles = ProcesoRenombrarArchivos(sRuta);

                    if (bCopyFiles == true)
                    {
                        //Procesar Archivo de remesas
                        cmdExcel.CommandText = "SELECT [Unidad Operativa],[Número OC],[Fecha Transacción],[Fecha Contable],[Tipo Transacción] FROM [" + SheetName + "]";
                        oda.SelectCommand = cmdExcel;
                        oda.Fill(dtRemesa);
                        connExcel.Close();

                        dtRemesa = FormatearRemesas(dtRemesa, sRuta);

                        if (dtRemesa != null)
                        {
                            System.Data.DataTable dtProcesados = new System.Data.DataTable();
                            System.Data.DataTable dtNoProcesados = new System.Data.DataTable();
                            DataRow[] rowProcess;
                            DataRow[] rowNoProcess;

                            dtProcesados.Columns.Add("Unidad_Operativa", typeof(string));
                            dtProcesados.Columns.Add("Factura", typeof(string));
                            dtProcesados.Columns.Add("Fecha_Transaccion", typeof(string));
                            dtProcesados.Columns.Add("Fecha_Contable", typeof(string));
                            dtProcesados.Columns.Add("Base_Site", typeof(string)); //Tipo transacción
                            dtProcesados.Columns.Add("Estatus", typeof(string));

                            dtNoProcesados.Columns.Add("Unidad_Operativa", typeof(string));
                            dtNoProcesados.Columns.Add("Factura", typeof(string));
                            dtNoProcesados.Columns.Add("Fecha_Transaccion", typeof(string));
                            dtNoProcesados.Columns.Add("Fecha_Contable", typeof(string));
                            dtNoProcesados.Columns.Add("Base_Site", typeof(string)); //Tipo transacción
                            dtNoProcesados.Columns.Add("Estatus", typeof(string));

                            rowProcess = dtRemesa.Select("Estatus = 'Encontrados'");
                            rowNoProcess = dtRemesa.Select("Estatus = 'No Encontrados'");

                            foreach (DataRow rowP in rowProcess)
                                dtProcesados.ImportRow(rowP);
                            dtProcesados.AcceptChanges();

                            foreach (DataRow rowNP in rowNoProcess)
                                dtNoProcesados.ImportRow(rowNP);
                            dtNoProcesados.AcceptChanges();

                            gvProcesados.DataSource = dtProcesados;
                            gvProcesados.DataBind();

                            gvNoProcesados.DataSource = dtNoProcesados;
                            gvNoProcesados.DataBind();

                            if (dtProcesados.Rows.Count <= 0)
                                btnProcesarFacturas.Visible = false;
                            else
                                btnProcesarFacturas.Visible = true;


                            if (hdnSeleccionGral.Value == "1" && dtProcesados != null && dtProcesados.Rows.Count > 0)
                            {
                                btnProcesarFacturas.Text = "Validar Facturas";
                                btnProcesarFacturas.ToolTip = "Validar Facturas";
                            }
                            else if (hdnSeleccionGral.Value == "2" && dtProcesados != null && dtProcesados.Rows.Count > 0)
                            {
                                btnProcesarFacturas.Text = "Procesar Facturas";
                                btnProcesarFacturas.ToolTip = "Procesar Facturas";
                            }
                        }
                    }
                }
                #endregion

                #region FORMATO ASUR

                else if (hdnSeleccionFormato.Value == "5")
                {
                    string sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASUR"].S() + @"Archivos";

                    bCopyFiles = ProcesoRenombrarArchivosASUR(sRuta);

                    if (bCopyFiles == true)
                    {
                        //Procesar Archivo de remesas
                        cmdExcel.CommandText = "SELECT [CLAVE AEROPUERTO],[FACTURA] FROM [" + SheetName + "]";
                        oda.SelectCommand = cmdExcel;
                        oda.Fill(dtRemesa);
                        connExcel.Close();

                        dtRemesa = FormatearRemesasASUR(dtRemesa, sRuta);

                        //Envio facturas al método de lectura correspondiente
                        if (dtRemesa != null & dtRemesa.Rows.Count > 0)
                        {
                            System.Data.DataTable dtEncontrados = new System.Data.DataTable();
                            System.Data.DataTable dtNoEncontrados = new System.Data.DataTable();
                            DataRow[] rowEncontrados;
                            DataRow[] rowNoEncontrados;

                            dtEncontrados.Columns.Add("Clave_Aeropuerto", typeof(string));
                            dtEncontrados.Columns.Add("Factura", typeof(string));
                            dtEncontrados.Columns.Add("Archivo_Lectura", typeof(string));
                            dtEncontrados.Columns.Add("Status", typeof(string));

                            dtNoEncontrados.Columns.Add("Clave_Aeropuerto", typeof(string));
                            dtNoEncontrados.Columns.Add("Factura", typeof(string));
                            dtNoEncontrados.Columns.Add("Archivo_Lectura", typeof(string));
                            dtNoEncontrados.Columns.Add("Status", typeof(string));

                            rowEncontrados = dtRemesa.Select("Status = 'Encontrados'");
                            rowNoEncontrados = dtRemesa.Select("Status = 'No Encontrados'");

                            foreach (DataRow rowP in rowEncontrados)
                                dtEncontrados.ImportRow(rowP);
                            dtEncontrados.AcceptChanges();

                            foreach (DataRow rowNP in rowNoEncontrados)
                                dtNoEncontrados.ImportRow(rowNP);
                            dtNoEncontrados.AcceptChanges();

                            gvEncontrados.DataSource = dtEncontrados;
                            gvEncontrados.DataBind();

                            gvNoEncontrados.DataSource = dtNoEncontrados;
                            gvNoEncontrados.DataBind();

                            if (dtEncontrados.Rows.Count <= 0)
                                btnProcesarASUR.Visible = false;
                            else
                                btnProcesarASUR.Visible = true;


                            //if (hdnSeleccionGral.Value == "1" && dtEncontrados != null && dtEncontrados.Rows.Count > 0)
                            //{
                            //    btnProcesarASUR.Text = "Validar Facturas";
                            //    btnProcesarASUR.ToolTip = "Validar Facturas";
                            //}
                            //else if (hdnSeleccionGral.Value == "2" && dtEncontrados != null && dtEncontrados.Rows.Count > 0)
                            //{
                            //    btnProcesarASUR.Text = "Procesar Facturas";
                            //    btnProcesarASUR.ToolTip = "Procesar Facturas";
                            //}

                        }


                    }
                }

                #endregion

                #region Valida columnas y formato de Layout a Cargar
                if (hdnSeleccionFormato.Value == "1")
                    bValidExcel = ValidarArchivo(dtExcel, "1");

                else if (hdnSeleccionFormato.Value == "2")
                    bValidExcel = ValidarArchivo(dtDatosOMA, "2");

                else if (hdnSeleccionFormato.Value == "3")
                    bValidExcel = ValidarArchivo(dtUnionAMAIT, "3");

                else if (hdnSeleccionFormato.Value == "4")
                {
                    if (bCopyFiles == true)
                        bValidExcel = ValidarArchivo(dtRemesa, "4");
                }

                //else if (hdnSeleccionFormato.Value == "5")
                //    bValidExcel = ValidarArchivo(dtDatosASUR, "5"); //bValidExcel = true;
                #endregion

                bool bValidacion = false;

                if (bValidExcel)
                {
                    if (hdnSeleccionFormato.Value == "1") //Validaciones Layout Genérico
                    {
                        if (dt != null)
                            bValidacion = ValidaExcel(dt);
                        else
                            bValidacion = false;
                    }
                    else if (hdnSeleccionFormato.Value == "2") //Validaciones Layout OMA
                    {
                        if (dtHeaderRows != null && dtDatosOMA != null)
                        {
                            bValidacion = ValidaExcelOMA(dtHeaderRows, dtDatosOMA);
                        }
                        else
                            bValidacion = false;
                    }
                    else if (hdnSeleccionFormato.Value == "3") //Validaciones Layout AMAIT
                    {
                        if (dtUnionAMAIT != null)
                            bValidacion = ValidaExcelAMAIT(dtUnionAMAIT);
                        else
                            bValidacion = false;
                    }

                    //else if (hdnSeleccionFormato.Value == "5") //Validaciones Layout ASUR
                    //{
                    //    if (dtDatosASUR != null)
                    //        bValidacion = ValidaASUR(dtDatosASUR);
                    //    else
                    //        bValidacion = false;
                    //}


                    #region Valida simulacion para cargar masivamente
                    if (bValidacion == true && hdnSeleccionGral.Value == "1")
                    {
                        if (gvResultado.Rows.Count < 1)
                        {
                            if (bValidacion == true)
                            {
                                if (ViewState["vsDataTable"] == null)
                                {
                                    lblArchivoSimulado.Visible = true;
                                    lblArchivoSimulado.Text = sArchivoSimulado;
                                    btnProcesarCargaSimulada.Visible = true;
                                }
                            }
                        }

                    }
                    #endregion



                    if (hdnSeleccionGral.Value == "2")
                    {
                        if (hdnSeleccionFormato.Value == "1")
                        {
                            if (bValidacion == true)
                                RecorrerDtExcel(dtExcel);
                        }
                        else if (hdnSeleccionFormato.Value == "2")
                        {
                            if (bValidacion == true)
                                RecorrerDtExcelOMA(dtDatosOMA, dtHeaderRows);
                        }
                        else if (hdnSeleccionFormato.Value == "3")
                        {
                            if (bValidacion == true)
                                RecorrerDtExcelAMAIT(dtUnionAMAIT);
                        }
                        else if (hdnSeleccionFormato.Value == "5")
                        {
                            if (bValidacion == true)
                                RecorrerTXTASUR(dtDatosASUR);
                        }
                    }
                }
                else
                {
                    ErrorRequireColumns();
                    pnlSimulacion.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public System.Data.DataTable dtRowHeader(string sEmpresa, string sFactura, string sFechaFactura)
        {
            try
            {
                System.Data.DataTable dtHeader = new System.Data.DataTable();
                DataColumn column;
                DataRow row;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "EMPRESA";
                dtHeader.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "FACTURA";
                dtHeader.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "FECHA_FACTURA";
                dtHeader.Columns.Add(column);

                row = dtHeader.NewRow();
                row["EMPRESA"] = sEmpresa;
                row["FACTURA"] = sFactura;
                row["FECHA_FACTURA"] = sFechaFactura;
                dtHeader.Rows.Add(row);

                return dtHeader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region VALIDACIONES FORMATO LAYOUT GENÉRICO
        protected bool ValidaExcel(System.Data.DataTable dt)
        {
            try
            {
                //bool ban = false;
                ViewState["vsDataTable"] = null;
                msgSuccesss.Visible = false;
                lblSuccess.Text = string.Empty;
                msgError.Visible = false;
                lblError.Text = string.Empty;
                //pnlSimulacion.Visible = false;

                dtExiste = null;
                System.Data.DataTable data = new System.Data.DataTable();
                data = dt;
                int iFila = 0;
                string strCampo = string.Empty;
                string strValor = string.Empty;
                string strExcepcion = string.Empty;
                int iStatus = 0;
                //Variables para validar formato de Fechas
                string[] format = { "yyyyMMdd" };
                DateTime date;
                string strFecha = string.Empty;
                //Variables para validar cantidad, importe y total
                string strNumeric = string.Empty;
                NumberStyles styles;
                double dResult;

                double dTotal;
                double dImpuesto;
                string sImporte = string.Empty;
                sBase = ddlBase.SelectedItem.Text;

                data.Columns.Add("PDF", typeof(string));
                data.Columns.Add("XML", typeof(string));

                foreach (DataRow dRow in data.Rows)
                {
                    iFila = dt.Rows.IndexOf(dRow) + 2;
                    int iExistePDF = 0;
                    int iExisteXML = 0;
                    string[] sValores = null;
                    bool bEmptyPU = false;
                    bool bEmptyT = false;

                    foreach (DataColumn dColumn in data.Columns)
                    {
                        iStatus = 1;
                        string sCodigoProv = string.Empty;
                        strFecha = string.Empty;
                        strCampo = string.Empty;
                        strValor = string.Empty;
                        strExcepcion = string.Empty;
                        strNumeric = string.Empty;
                        dResult = 0;
                        //sCodigoProveedor = string.Empty;
                        sMoneda = string.Empty;

                        #region VALIDAR CODIGO PROVEEDOR
                        // VALIDAR SI EXISTE EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "CODIGO PROVEEDOR")
                        {
                            sCodigoProveedor = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sCodigoProveedor))
                            {
                                if (eSearchProveedores != null)
                                    eSearchProveedores(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'CODIGO PROVEEDOR' viene vacio, favor de verificar";
                                iStatus = 0;
                            }

                            if (iStatus == 1)
                            {
                                sValoresValidacion = sCodigoProveedor;
                                sCodigoProv = sCodigoProveedor;
                            }
                        }
                        #endregion

                        #region VALIDAR MONEDA
                        //VALIDAR CON SAP
                        if (dRow[dColumn] != null && dColumn.S() == "MONEDA")
                        {
                            sMoneda = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sMoneda))
                            {
                                if (eSearchMoneda != null)
                                    eSearchMoneda(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'MONEDA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR CODIGO ARTICULO
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "CODIGO ARTICULO")
                        {
                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "Campo vacio.";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR ALMACEN
                        if (dRow[dColumn] != null && dColumn.S() == "ALMACEN")
                        {
                            sAlmacen = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sAlmacen))
                            {
                                if (eSearchAlmacen != null)
                                    eSearchAlmacen(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }

                            }
                        }
                        #endregion

                        #region VALIDAR LUGAR

                        //if (dRow[dColumn] != null && dColumn.S() == "LUGAR")
                        //{
                        //    if (string.IsNullOrEmpty(dRow[dColumn].S()))
                        //    {
                        //        strCampo = dColumn.S();
                        //        strValor = dRow[dColumn].S();
                        //        strExcepcion = "El campo 'LUGAR' viene vacio, favor de verificar";
                        //        iStatus = 0;
                        //    }
                        //}

                        #endregion

                        #region VALIDAR FACTURA
                        if (dRow[dColumn] != null && dColumn.S() == "FACTURA")
                        {
                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'FACTURA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }

                            if (iStatus == 1)
                            {
                                sValoresValidacion += "|" + dRow[dColumn].S();

                                if (new DBAccesoSAP().DBGetValidaExisteFactura(sCodigoProveedor, dRow[dColumn].S()))
                                {
                                    dtRow(iFila, "PROVEEDOR/FACTURA", sCodigoProveedor + "/" + dRow[dColumn].S(), 0, "El numero de factura ya existe con este proveedor, favor de verificar");
                                }
                            }
                        }
                        #endregion

                        #region VALIDAR FECHA DOC

                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "FECHA DOC (añomesdia)")
                        {
                            DateTime Fecha = DateTime.ParseExact(dRow[dColumn].ToString(), "yyyyMMdd", new CultureInfo("en-US"));//DateTime.ParseExact(dRow[dColumn].S(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            bool blIsDate = EsFecha(Fecha.ToShortDateString());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo " + dColumn.S() + " se encuenta vacio.";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    strFecha = Fecha.ToShortDateString();
                                    strFecha = FormatoFecha(strFecha);
                                }
                                else
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'FECHA DOC' no contiene información de alguna fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }

                            if (iStatus == 1)
                                sValoresValidacion += "|" + strFecha;
                        }

                        #endregion

                        #region VALIDAR FECHA CONTA SBO

                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "FECHA CONTA SBO (añomesdia)")
                        {
                            DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            bool blIsDate = EsFecha(Fecha.ToShortDateString());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'FECHA CONTA SBO' viene vacio, favor de verificar.";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    strFecha = Fecha.ToShortDateString();
                                    strFecha = FormatoFecha(strFecha);
                                }
                                else
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'FECHA CONTA SBO' no contiene información de alguna fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }

                        }

                        #endregion

                        #region VALIDAR FECHA OPERACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "FECHA OPERACIÓN")
                        {
                            DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            bool blIsDate = EsFecha(Fecha.ToShortDateString());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'FECHA OPERACIÓN' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    strFecha = Fecha.ToShortDateString();
                                    strFecha = FormatoFecha(strFecha);
                                }
                                else
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'FECHA OPERACIÓN' no contiene información de alguna fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }

                        }

                        #endregion

                        #region VALIDAR MATRICULA/DEPARTAMENTO
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "MATRICULA/DEPARTAMENTO")
                        {
                            sMatricula = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sMatricula))
                            {
                                if (eSearchMatricula != null)
                                    eSearchMatricula(null, null);

                                //Validación en SAP En Areas y Matriculas
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    if (eSearchAreaDpto != null)
                                        eSearchAreaDpto(null, null);

                                    if (dtExiste.Rows[0][0].S().I() == 0)
                                    {
                                        strCampo = dColumn.S();
                                        strValor = dRow[dColumn].S();
                                        strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                        iStatus = 0;
                                    }
                                }

                                //Validación en SAP
                                //if (dtExiste.Rows[0][0].S().I() == 0)
                                //{
                                //    strCampo = dColumn.S();
                                //    strValor = dRow[dColumn].S();
                                //    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                //    iStatus = 0;
                                //}
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'MATRICULA/DEPARTAMENTO' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR CANTIDAD
                        if (dRow[dColumn] != null && dColumn.S() == "CANTIDAD")
                        {
                            if (!string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strNumeric = dRow[dColumn].S();
                                styles = NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;

                                if (!Double.TryParse(strNumeric, styles, CultureInfo.InvariantCulture, out dResult))
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'CANTIDAD' no tiene el formato correcto, favor de verificar";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'CANTIDAD' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR TOTAL Y PRECIO UNIDAD

                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "PRECIO UNIDAD")
                        {
                            if (!string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strNumeric = dRow[dColumn].S();
                                sImporte = strNumeric;

                                styles = NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;

                                if (!Double.TryParse(strNumeric, styles, CultureInfo.InvariantCulture, out dResult))
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'PRECIO UNIDAD' no tiene el formato correcto, favor de verificar";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                bEmptyPU = true;
                            }
                        }

                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "TOTAL")
                        {
                            if (!string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strNumeric = dRow[dColumn].S();
                                sImporte = strNumeric;

                                styles = NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;

                                if (!Double.TryParse(strNumeric, styles, CultureInfo.InvariantCulture, out dResult))
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'TOTAL' no tiene el formato correcto, favor de verificar";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                bEmptyT = true;
                            }

                        }

                        if (bEmptyPU == true && bEmptyT == true)
                        {
                            strCampo = dColumn.S();
                            strValor = dRow[dColumn].S();
                            strExcepcion = "El campo 'PRECIO UNIDAD' ó 'TOTAL' viene vacio, favor de verificar que alguno de los dos campos contenga información";
                            iStatus = 0;
                        }
                        #endregion

                        #region VALIDAR CODIGO IMPUESTO
                        //VALIDAR CON SAP
                        if (dRow[dColumn] != null && dColumn.S() == "CODIGO IMPUESTO")
                        {
                            sCodigoImpuesto = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sCodigoImpuesto))
                            {
                                if (eSearchCodigoImpuesto != null)
                                    eSearchCodigoImpuesto(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'CODIGO IMPUESTO' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR Tipo
                        //if (dRow[dColumn] != null && dColumn.S() == "Tipo")
                        //{
                        //    if (string.IsNullOrEmpty(dRow[dColumn].S()))
                        //    {
                        //        strCampo = dColumn.S();
                        //        strValor = dRow[dColumn].S();
                        //        strExcepcion = "El campo 'Tipo' viene vacio, favor de verificar";
                        //        iStatus = 0;
                        //    }
                        //}
                        #endregion

                        #region VALIDAR DOCUMENTO A MODIFICAR
                        //if (dRow[dColumn] != null && dColumn.S() == "DOCUMENTO A MODIFICAR")
                        //{
                        //    if (string.IsNullOrEmpty(dRow[dColumn].S()))
                        //    {
                        //        strCampo = dColumn.S();
                        //        strValor = dRow[dColumn].S();
                        //        strExcepcion = "El campo 'DOCUMENTO A MODIFICAR' viene vacio, favor de verificar";
                        //        iStatus = 0;
                        //    }
                        //}
                        #endregion

                        #region VALIDAR Comments
                        //if (dRow[dColumn] != null && dColumn.S() == "Comments")
                        //{
                        //    if (string.IsNullOrEmpty(dRow[dColumn].S()))
                        //    {
                        //        strCampo = dColumn.S();
                        //        strValor = dRow[dColumn].S();
                        //        strExcepcion = "El campo 'Comments' viene vacio, favor de verificar";
                        //        iStatus = 0;
                        //    }
                        //}
                        #endregion

                        #region VALIDAR SITE Ó BASE
                        //if (dRow[dColumn] != null && dColumn.S() == "SITE")

                        if (!string.IsNullOrEmpty(sValoresValidacion))
                        {
                            sValores = sValoresValidacion.Split('|');

                            if (sValores.Length == 3)
                            {

                                if (!string.IsNullOrEmpty(sBase) && sBase != ".:Seleccione:.")
                                {
                                    if (string.IsNullOrEmpty(sBase))
                                        iStatus = 0;

                                    //if (string.IsNullOrEmpty(sPDF_OMA) && string.IsNullOrEmpty(sXML_OMA))
                                    //{
                                    if (iStatus == 1)
                                    {
                                        if (iExistePDF == 0 && iExisteXML == 0)
                                            sValoresValidacion += ("|" + sBase);

                                        #region VALIDACION DE DOCUMENTOS REQUERIDOS
                                        System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                                        if (dtProv != null && dtProv.Rows.Count > 0)
                                        {
                                            #region VALIDAR PDF

                                            if (iExistePDF == 0)
                                            {
                                                if (iStatus == 1)
                                                {
                                                    if (eGetValidaPDF != null)
                                                        eGetValidaPDF(null, EventArgs.Empty);

                                                    if (dtProv.Rows[0]["U_REQPDF"].S() == "SI")
                                                    {
                                                        if (!File.Exists(sCadArchivo))
                                                        {
                                                            strCampo = "Archivo";
                                                            strValor = sCadArchivo;
                                                            strExcepcion = "El archivo PDF no se encontró en la ubicación adecuada, favor de verificar";
                                                            iStatus = 0;
                                                            iFila = 0;
                                                            iExistePDF = 1;
                                                            //iStatus = 1; //Prueba en servidor
                                                        }
                                                        else
                                                        {
                                                            dRow["PDF"] = sCadArchivo;
                                                            sPDF_OMA = sCadArchivo;
                                                            //iStatus = 1; //Prueba en servidor
                                                        }
                                                        if (iStatus == 0)
                                                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                                    }
                                                    else
                                                    {
                                                        dRow["PDF"] = string.Empty;
                                                        sCadArchivo = string.Empty;
                                                    }
                                                    dRow["PDF"] = sCadArchivo;
                                                    sPDF_OMA = sCadArchivo;
                                                }
                                            }

                                            #endregion

                                            #region VALIDAR XML
                                            if (iExisteXML == 0)
                                            {
                                                //if (iStatus == 1)
                                                //{
                                                if (eGetValidaXML != null)
                                                    eGetValidaXML(null, EventArgs.Empty);

                                                if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                                                {
                                                    if (!File.Exists(sCadArchivo))
                                                    {
                                                        strCampo = "Archivo";
                                                        strValor = sCadArchivo;
                                                        strExcepcion = "El archivo XML no se encontró en la ubicación adecuada, favor de verificar";
                                                        iStatus = 0;
                                                        iFila = 0;
                                                        iExisteXML = 1;
                                                        //iStatus = 1; //Prueba en servidor
                                                    }
                                                    else
                                                    {
                                                        dRow["XML"] = sCadArchivo;
                                                        sXML_OMA = sCadArchivo;
                                                        //iStatus = 1; //Prueba en servidor
                                                    }

                                                    if (iStatus == 0)
                                                        dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                                }
                                                else
                                                {
                                                    dRow["XML"] = string.Empty;
                                                    sCadArchivo = string.Empty;
                                                }
                                                iStatus = 1;
                                                dRow["XML"] = sCadArchivo;
                                                sXML_OMA = sCadArchivo;
                                                //}
                                            }
                                            #endregion
                                        }

                                        #endregion
                                    }
                                    //}
                                }
                                //sValores = null;
                            }

                        }
                        #endregion

                        if (iStatus == 0)
                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                    }
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
                    return true;
                }

                //if (ViewState["vsDataTable"] != null)
                //{
                //    System.Data.DataTable dtSinDupl = new System.Data.DataTable();
                //    dtSinDupl.Columns.Add("Fila", typeof(string));
                //    dtSinDupl.Columns.Add("Campo", typeof(string));
                //    dtSinDupl.Columns.Add("Valor", typeof(string));
                //    dtSinDupl.Columns.Add("Status", typeof(string));
                //    dtSinDupl.Columns.Add("Descripcion", typeof(string));

                //    DataView vista = new DataView((System.Data.DataTable)ViewState["vsDataTable"]);
                //    dtSinDupl = vista.ToTable(true, "Fila", "Campo", "Valor", "Status", "Descripcion");

                //    gvResultado.DataSource = dtSinDupl;
                //    gvResultado.DataBind();

                //    if (gvResultado.Rows.Count > 0)
                //    {
                //        int rowCount = gvResultado.Rows.Count;

                //        if (rowCount > 0)
                //        {
                //            msgError.Visible = true;
                //            lblError.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                //            msgSuccesss.Visible = false;
                //            lblSuccess.Text = string.Empty;
                //        }
                //    }
                //    ban = false;
                //}
                //else
                //{
                //    if (iBanCorrecto == 0 && hdnSeleccionGral.Value == "1")
                //    {
                //        msgSuccesss.Visible = true;
                //        lblSuccess.Text = "El archivo cumple los requerimientos.";
                //        msgError.Visible = false;
                //        lblError.Text = string.Empty;
                //        ban = true;
                //    }
                //}

                //if (ViewState["vsDataTable"] != null)
                //{
                //    System.Data.DataTable dtSinDupl = new System.Data.DataTable();
                //    dtSinDupl.Columns.Add("Fila", typeof(string));
                //    dtSinDupl.Columns.Add("Campo", typeof(string));
                //    dtSinDupl.Columns.Add("Valor", typeof(string));
                //    dtSinDupl.Columns.Add("Status", typeof(string));
                //    dtSinDupl.Columns.Add("Descripcion", typeof(string));

                //    DataView vista = new DataView((System.Data.DataTable)ViewState["vsDataTable"]);
                //    dtSinDupl = vista.ToTable(true, "Fila", "Campo", "Valor", "Status", "Descripcion");

                //    gvResultado.DataSource = dtSinDupl;//(System.Data.DataTable)ViewState["vsDataTable"]; //dtAllError;//dtSinDupl;
                //    gvResultado.DataBind();

                //    if (gvResultado.Rows.Count > 0)
                //    {
                //        msgError.Visible = true;
                //        lblError.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                //        msgSuccesss.Visible = false;
                //        lblSuccess.Text = string.Empty;
                //        pnlSimulacion.Visible = true;
                //        ban = false;
                //    }
                //    else if (dtSinDupl.Rows.Count > 0) 
                //    {
                //        msgError.Visible = true;
                //        lblError.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                //        msgSuccesss.Visible = false;
                //        lblSuccess.Text = string.Empty;
                //        pnlSimulacion.Visible = true;
                //        ban = false;
                //    }

                //}
                //else
                //{
                //    if (iBanCorrecto == 1 && hdnSeleccionGral.Value == "2")
                //    {
                //        msgSuccesss.Visible = true;
                //        lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                //        msgError.Visible = false;
                //        lblError.Text = string.Empty;
                //        pnlSimulacion.Visible = false;
                //    }
                //    else if (iBanCorrecto == 0 && hdnSeleccionGral.Value == "1")
                //    {
                //        msgSuccesss.Visible = true;
                //        lblSuccess.Text = "El archivo cumple los requerimientos.";
                //        msgError.Visible = false;
                //        lblError.Text = string.Empty;
                //        pnlSimulacion.Visible = false;
                //        ban = true;
                //    }
                //    else
                //    {
                //        if (!string.IsNullOrEmpty(sMsgError))
                //        {
                //            msgSuccesss.Visible = false;
                //            lblSuccess.Text = string.Empty;
                //            msgError.Visible = true;
                //            lblError.Text = sMsgError;
                //        }
                //    }



                //}

                //return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecorrerDtExcel(System.Data.DataTable dt)
        {
            try
            {
                sFormato = hdnSeleccionFormato.Value.S();
                new DBUtils().GetLoadInitialValues();
                string sRows = string.Empty;
                string sNoFactura = string.Empty;
                string sFechaOp = string.Empty;
                List<Factura> oLsFacturas = new List<Factura>();
                string sProyecto = string.Empty;
                string sDimension1 = string.Empty;
                string sDimension2 = string.Empty;
                string sDimension3 = string.Empty;
                string sDimension4 = string.Empty;
                string sDimension5 = string.Empty;
                int iHead = 0;
                string sFechaC = string.Empty;
                string sFechaConta = string.Empty;
                string sXMLChar = string.Empty;
                String[] _sXMLChar;

                string sGetXML = string.Empty;
                string sRutaXML = string.Empty;

                System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                System.Data.DataTable distinctDT = SelectDistinct(dtExcel, "FACTURA");
                DataRow[] fRows;

                for (int i = 0; i < distinctDT.Rows.Count; i++)
                {
                    Factura oFactura = new Factura();

                    sNoFactura = distinctDT.Rows[i][0].S();
                    fRows = dtExcel.Select("FACTURA='" + sNoFactura + "'");

                    for (int x = 0; x < fRows.Length; x++)
                    {
                        ConceptosFactura oConcepto = new ConceptosFactura();

                        sMatricula = fRows[x]["MATRICULA/DEPARTAMENTO"].S(); //dtExcel.Rows[i]["MATRICULA/DEPARTAMENTO"].S();

                        sGetXML = sNoFactura + ".xml";
                        sRutaXML = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestUrlXML\OTHER\" + sGetXML;

                        if (eSearchMatricula != null)
                            eSearchMatricula(null, null);

                        //Validación en SAP
                        if (dtExiste.Rows[0][0].S().I() == 0)
                            sDimension1 = sMatricula;
                        else
                        {
                            sDimension1 = string.Empty; //Area ó Departamento
                            sDimension2 = sMatricula;   //Matrícula
                        }


                        if (x == 0)
                        {
                            if (i > 0)
                                iHead = i - i;
                            // Armar Header
                            oFactura.iId = i + 1;
                            oFactura.sEmpresa = "1";
                            oFactura.sSucursal = "1";
                            oFactura.sProveedor = fRows[iHead]["CODIGO PROVEEDOR"].S();
                            oFactura.sNoFactura = fRows[iHead]["FACTURA"].S();
                            oFactura.sTipoFactura = fRows[iHead]["Tipo"].S();
                            oFactura.iTimbrar = 0;
                            oFactura.sMetodoPago = "";
                            oFactura.sFormaPago = "";
                            oFactura.sUsoCFDI = "";

                            DateTime dtFecha = DateTime.Now;

                            sFechaC = FormatoFechaGenerico(fRows[iHead]["FECHA DOC (añomesdia)"].S());
                            sFechaConta = FormatoFechaGenerico(fRows[iHead]["FECHA CONTA SBO (añomesdia)"].S());

                            DateTime dtFechaDoc = DateTime.Parse(sFechaC);
                            DateTime dtFechaConta = DateTime.Parse(sFechaConta);

                            decimal dTipoCambio = ObtieneTipoCambio(fRows[iHead]["MONEDA"].S(), ref dtFecha, sFechaC);
                            oFactura.dtFecha = dtFechaDoc;//dtFecha;
                            oFactura.dtFechaExp = dtFechaDoc;//dtFecha;
                            oFactura.dtFechaImp = dtFechaConta;//DateTime.Now;
                            oFactura.dTipoCambio = dTipoCambio;
                            oFactura.sMsg = "";

                            if (string.IsNullOrEmpty(fRows[iHead]["Comments"].S()))
                                oFactura.sComentarios = txtComentarios.Text;
                            else
                                oFactura.sComentarios = fRows[iHead]["Comments"].S();

                            oFactura.sSerie = "";
                            oFactura.sMoneda = fRows[iHead]["MONEDA"].S();
                            oFactura.dDescuento = 0;

                            //Valida y Obtiene informaxión de XML
                            if (eGetValidaXML != null)
                                eGetValidaXML(null, EventArgs.Empty);

                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                            {
                                //if (File.Exists(sRutaXML))
                                if (File.Exists(fRows[iHead]["XML"].S()))
                                {
                                    sXMLChar = ObtenerDatosXML(fRows[iHead]["XML"].S());
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

                        DateTime FechaOperacion = DateTime.ParseExact(fRows[x]["FECHA OPERACIÓN"].S(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        sFechaOp = FechaOperacion.ToShortDateString();

                        //sTotalLinea = sTotalL(fRows[x]["CANTIDAD"].S(), fRows[x]["IMPORTE"].S());

                        //Agregar a Detalle
                        oConcepto.iId = i + 1;
                        oConcepto.sEmpresa = "1";
                        oConcepto.iLinea = x + 1;
                        oConcepto.sItem = fRows[x]["CODIGO ARTICULO"].S();

                        sCodigoArticulo = fRows[x]["CODIGO ARTICULO"].S();

                        if (!string.IsNullOrEmpty(sCodigoArticulo))
                        {

                            System.Data.DataTable dtDesArticulo = new DBAccesoSAP().DBGetObtieneDescripcionArticulo(sCodigoArticulo);

                            if (dtDesArticulo.Rows.Count > 0)
                                sDescripcionArticulo = dtDesArticulo.Rows[0][0].S();
                            else
                                sDescripcionArticulo = string.Empty;

                            System.Data.DataTable dtCodImpuesto = new DBAccesoSAP().DBGetObtieneCodigoImpuestoArticulo(sCodigoArticulo);

                            if (dtCodImpuesto != null && dtCodImpuesto.Rows.Count > 0)
                                sCodImp = dtCodImpuesto.Rows[0][0].S();
                            else
                                sCodImp = string.Empty;
                        }

                        oConcepto.sDescripcionUsuario = sDescripcionArticulo;
                        oConcepto.sCodBarras = "";

                        oConcepto.dCantidad = fRows[x]["CANTIDAD"].S().D();
                        oConcepto.dPrecio = fRows[x]["PRECIO UNIDAD"].S().D();

                        //if (!string.IsNullOrEmpty(fRows[x]["PRECIO UNIDAD"].S()) && string.IsNullOrEmpty(fRows[x]["TOTAL"].S()))
                        //    oConcepto.dPrecio = fRows[x]["PRECIO UNIDAD"].S().D();

                        //else if (!string.IsNullOrEmpty(fRows[x]["TOTAL"].S()) && string.IsNullOrEmpty(fRows[x]["PRECIO UNIDAD"].S()))
                        //    oConcepto.dTotalLinea = fRows[x]["TOTAL"].S().D();

                        //else if (!string.IsNullOrEmpty(fRows[x]["TOTAL"].S()) && !string.IsNullOrEmpty(fRows[x]["PRECIO UNIDAD"].S()))
                        //{
                        //    oConcepto.dPrecio = fRows[x]["PRECIO UNIDAD"].S().D();
                        //    oConcepto.dTotalLinea = fRows[x]["TOTAL"].S().D();
                        //}


                        if (string.IsNullOrEmpty(fRows[x]["TOTAL"].S()))
                        {
                            oConcepto.dTotalLinea = fRows[x]["PRECIO UNIDAD"].S().D() * fRows[x]["CANTIDAD"].S().D();
                        }
                        else
                        {
                            oConcepto.dTotalLinea = fRows[x]["TOTAL"].S().D();
                        }

                        if (string.IsNullOrEmpty(fRows[x]["CODIGO IMPUESTO"].S()))
                        {
                            oConcepto.sCodigoImpuesto = sCodImp;
                        }
                        else
                        {
                            oConcepto.sCodigoImpuesto = fRows[x]["CODIGO IMPUESTO"].S();
                        }

                        oConcepto.dDescuento = 0;
                        oConcepto.iImpuesto = 0;

                        oConcepto.sCuenta = "";
                        oConcepto.sAlmacen = fRows[x]["ALMACEN"].S();
                        oConcepto.sTipo = fRows[x]["Tipo"].S();

                        if (!string.IsNullOrEmpty(sDimension1))
                        {
                            System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionArea(sMatricula);

                            if (dtDatos.Rows.Count > 0)
                            {
                                sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                                sDimension3 = dtDatos.Rows[0]["U_Site"].S();
                                sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                            }
                            sDimension1 = sMatricula;
                            sDimension2 = string.Empty;
                            sDimension5 = string.Empty;
                        }
                        else
                        {
                            System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionMatricula(sMatricula);

                            if (dtDatos.Rows.Count > 0)
                            {
                                sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                                sDimension3 = dtDatos.Rows[0]["U_Site"].S();
                                sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                            }
                            sDimension1 = string.Empty;
                            sDimension2 = sMatricula;
                            sDimension5 = string.Empty;
                        }

                        oConcepto.sProyecto = sProyecto;
                        oConcepto.sDimension1 = sDimension1;
                        oConcepto.sDimension2 = sDimension2; //&fRows[x]["MATRICULA/DEPARTAMENTO"].S();
                        oConcepto.sDimension3 = sDimension3; //sBase;
                        oConcepto.sDimension4 = sDimension4;
                        oConcepto.sDimension5 = sDimension5;
                        oConcepto.sXML = fRows[x]["XML"].S();
                        oConcepto.sPDF = fRows[x]["PDF"].S();
                        oConcepto.sLugar = fRows[x]["LUGAR"].S();

                        oConcepto.sFechaOperacion = sFechaOp; // aqui va la columna del excel generico
                        oFactura.oLstConceptos.Add(oConcepto);
                    }
                    oLsFacturas.Add(oFactura);
                }
                ListaFacturas = oLsFacturas;

                if (eSetProcesaArchivo != null)
                    eSetProcesaArchivo(null, null);

                if (dtError != null)
                {
                    if (dtError.Rows.Count > 0)
                    {
                        string sCadError = string.Empty;
                        sCadError = "<ul>";

                        for (int i = 0; i < dtError.Rows.Count; i++)
                        {
                            sCadError += "<li>" + dtError.Rows[i]["DescripcionError"].S() + "</li>";
                        }
                        sCadError += "</ul>";
                        msgError.Visible = true;
                        lblError.Text = sCadError;
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                    }
                }
                else
                {
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    msgSuccesss.Visible = true;
                }

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
        public string sTotalL(string sCan, string sImporte)
        {
            try
            {
                double dTotal = 0;
                sCantidad = sCan;
                //Calculo de Total de Linea 'IMPORTE * IVA IMPUESTO'
                //if (eGetIVAImpuesto != null)
                //    eGetIVAImpuesto(null, EventArgs.Empty);

                if (!string.IsNullOrEmpty(sCantidad))
                {
                    //if (sIVA.Db() != 0.0)
                    //    dTotal = sImporte.Db() * sIVA.Db();
                    //else
                    dTotal = sImporte.Db() / sCantidad.Db();
                    sTotalLinea = dTotal.S();
                }
                return sTotalLinea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Fin validaciones Formato Layout Genérico
        #endregion

        #region VALIDACIONES FORMATO LAYOUT OMA
        protected bool ValidaExcelOMA(System.Data.DataTable dtHeader, System.Data.DataTable dtContentOMA)
        {
            try
            {
                ViewState["vsDataTable"] = null;
                msgSuccesss.Visible = false;
                lblSuccess.Text = string.Empty;
                msgError.Visible = false;
                lblError.Text = string.Empty;
                pnlSimulacion.Visible = false;

                //string sFactura = string.Empty;
                string sFechaFactura = string.Empty;
                string sCodPro = string.Empty;
                int iFila = 9;
                bool blBanExist = false;
                string strCampo = string.Empty;
                string strValor = string.Empty;
                string strExcepcion = string.Empty;
                int iStatus = 0;
                //Variables para validar formato de Fechas
                string[] format = { "yyyyMMdd" };
                DateTime date;
                string strFecha = string.Empty;
                //Variables para validar cantidad, importe y total
                string strNumeric = string.Empty;
                NumberStyles styles;
                double dResult;
                bool blBanCodPro = false;
                sLugar = txtIATA.Text;

                dtCodPro = null;
                dtContentOMA.Select("MATRICULA Like 'X%'");

                foreach (DataRow row in dtContentOMA.Rows)
                {
                    string sFormatMat = row["MATRICULA"].S();

                    if (sFormatMat.IndexOf('X') != -1)
                    {
                        string sFirstPartMat = sFormatMat.Substring(0, 2);
                        string sSecondPartMat = sFormatMat.Substring(2, sFormatMat.Length - 2);
                        row["MATRICULA"] = sFirstPartMat + '-' + sSecondPartMat;
                    }
                }
                sEmpresa = dtHeader.Rows[0][0].S().Replace(".", "").Replace(",", "").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");

                if (!string.IsNullOrEmpty(sEmpresa))
                {
                    if (eGetCodProveedores != null)
                        eGetCodProveedores(null, null);
                }
                sCodPro = sCodigoProveedor;
                //sFactura = dtHeader.Rows[0][1].S();
                sFechaFactura = dtHeader.Rows[0][2].S();
                sBase = ddlBase.SelectedItem.Text;
                System.Data.DataTable data = new System.Data.DataTable();
                data = dtContentOMA;

                foreach (DataRow dRow in data.Rows)
                {
                    iFila += 1;
                    int iExistePDF = 0;
                    int iExisteXML = 0;

                    foreach (DataColumn dColumn in data.Columns)
                    {
                        #region VARIABLES
                        iStatus = 1;
                        string sCodigoProv = string.Empty;
                        strFecha = string.Empty;
                        strCampo = string.Empty;
                        strValor = string.Empty;
                        strExcepcion = string.Empty;

                        strNumeric = string.Empty;
                        dResult = 0;
                        #endregion

                        #region VALIDAR CODIGO PROVEEDOR
                        // VALIDAR SI EXISTE EN SAP
                        if (blBanCodPro == false)
                        {
                            if (!string.IsNullOrEmpty(sCodigoProveedor))
                            {
                                if (!string.IsNullOrEmpty(sCodigoProveedor))
                                {
                                    if (eSearchProveedores != null)
                                        eSearchProveedores(null, null);

                                    //Validación en SAP
                                    if (dtExiste.Rows[0][0].S().I() == 0)
                                    {
                                        strCampo = "PROVEEDOR";
                                        strValor = sCodigoProveedor;
                                        strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                        iStatus = 0;
                                    }
                                }
                                else
                                {
                                    strCampo = "PROVEEDOR";
                                    strValor = sCodigoProveedor;
                                    strExcepcion = "El formato OMA no contiene proveedor, favor de verificar";
                                    iStatus = 0;
                                }

                                if (iStatus == 1)
                                {
                                    blBanCodPro = true;
                                    sValoresValidacion = sCodigoProveedor;
                                    sCodigoProv = sCodigoProveedor;
                                }
                            }
                            else
                            {
                                strCampo = "PROVEEDOR";
                                strValor = sCodigoProveedor;
                                strExcepcion = "El se encuentra información del proveedor, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR MONEDA
                        //VALIDAR CON SAP
                        if (dRow[dColumn] != null && dColumn.S() == "MONEDA")
                        {
                            sMoneda = "MXN"; //dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sMoneda))
                            {
                                if (eSearchMoneda != null)
                                    eSearchMoneda(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'MONEDA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR CODIGO ARTICULO POR MEDIO DEL SERVICIO
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "SERVICIO")
                        {
                            sServicio = dRow[dColumn].S();

                            if (string.IsNullOrEmpty(sServicio))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "Campo vacio.";
                                iStatus = 0;
                            }
                            else
                            {

                                if (eGetCodArticulo != null)
                                    eGetCodArticulo(null, null);

                                if (string.IsNullOrEmpty(sCodigoArticulo))
                                {
                                    strCampo = "SERVICIO";
                                    strValor = sCodigoArticulo;
                                    strExcepcion = "No hay información del artículo en la tabla '[ClientesCasa].[tbp_CC_Articulos]', favor de verificar.";
                                    iStatus = 0;
                                }

                            }
                        }
                        #endregion

                        #region VALIDAR FACTURA
                        if (!string.IsNullOrEmpty(sFactura))
                        {
                            if (string.IsNullOrEmpty(sFactura))
                            {
                                strCampo = "FACTURA";
                                strValor = sFactura;
                                strExcepcion = "El campo 'FACTURA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }

                            if (iStatus == 1)
                            {

                                sValoresValidacion += "|" + sFactura;

                                if (iStatus == 1)
                                {
                                    sFechaFactura = FormatoFecha(sFechaFactura.Dt().ToShortDateString());
                                    sValoresValidacion += "|" + sFechaFactura;
                                }

                                if (blBanExist == false)
                                {
                                    if (new DBAccesoSAP().DBGetValidaExisteFactura(sCodigoProveedor, sFactura))
                                    {
                                        blBanExist = true;
                                        dtRow(iFila, "PROVEEDOR/FACTURA", sCodigoProveedor + "/" + sFactura, 0, "El numero de factura ya existe con este proveedor, favor de verificar");
                                    }
                                }
                            }
                        }
                        #endregion

                        #region VALIDAR FECHA OPERACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "PERIODO INICIAL")
                        {
                            bool blIsDate = EsFecha(dRow[dColumn].S());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'FECHA OPERACIÓN' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    strFecha = dRow[dColumn].Dt().ToShortDateString();
                                    strFecha = FormatoFecha(strFecha);
                                }
                                else
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'FECHA OPERACIÓN' no contiene una fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }
                        }

                        #endregion

                        #region VALIDAR MATRICULA
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "MATRICULA")
                        {
                            sMatricula = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sMatricula))
                            {
                                if (eSearchMatricula != null)
                                    eSearchMatricula(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'MATRICULA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR LUGAR
                        //VALIDAR EN SAP
                        if (sLugar != null)
                        {
                            sLugar = txtIATA.Text;

                            if (!string.IsNullOrEmpty(sLugar))
                            {
                                if (eSearchLugar != null)
                                    eSearchLugar(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = "Aeropuerto IATA";
                                    strValor = sLugar;
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = "Aeropuerto IATA";
                                strValor = sLugar;
                                strExcepcion = "El campo 'Aeropuerto IATA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR BASE
                        if (!string.IsNullOrEmpty(sBase) && sBase != ".:Seleccione:.")
                        {
                            if (string.IsNullOrEmpty(sBase))
                                iStatus = 0;

                            if (string.IsNullOrEmpty(sPDF_OMA) && string.IsNullOrEmpty(sXML_OMA))
                            {
                                if (iStatus == 1)
                                {
                                    if (iExistePDF == 0 && iExisteXML == 0)
                                        sValoresValidacion += ("|" + sBase);

                                    #region VALIDACION DE DOCUMENTOS REQUERIDOS
                                    System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                                    if (dtProv != null && dtProv.Rows.Count > 0)
                                    {
                                        #region VALIDAR PDF
                                        if (iExistePDF == 0)
                                        {
                                            if (iStatus == 1)
                                            {
                                                if (eGetValidaPDF != null)
                                                    eGetValidaPDF(null, EventArgs.Empty);

                                                if (dtProv.Rows[0]["U_REQPDF"].S() == "SI")
                                                {
                                                    if (sPDF_OMA == string.Empty)
                                                        sPDF_OMA = sCadArchivo;

                                                    if (!File.Exists(sCadArchivo))
                                                    {
                                                        strCampo = "Documento";
                                                        strValor = sCadArchivo;
                                                        strExcepcion = "El archivo PDF no se encontró en la ubicación adecuada, favor de verificar";
                                                        iStatus = 0;
                                                        iFila = 0;
                                                        iExistePDF = 1;
                                                    }
                                                    else
                                                    {
                                                        sBase = sCadArchivo;
                                                        sPDF_OMA = sCadArchivo;
                                                    }

                                                    if (iStatus == 0)
                                                        dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                                }
                                                sBase = sCadArchivo;
                                                sPDF_OMA = sCadArchivo;
                                            }
                                        }

                                        #endregion

                                        #region VALIDAR XML
                                        if (iExisteXML == 0)
                                        {
                                            //if (iStatus == 1)
                                            //{
                                            if (eGetValidaXML != null)
                                                eGetValidaXML(null, EventArgs.Empty);

                                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                                            {
                                                if (sXML_OMA == string.Empty)
                                                    sXML_OMA = sCadArchivo;

                                                if (!File.Exists(sCadArchivo))
                                                {
                                                    strCampo = "Documento";
                                                    strValor = sCadArchivo;
                                                    strExcepcion = "El archivo XML no se encontró en la ubicación adecuada, favor de verificar";
                                                    iStatus = 0;
                                                    iFila = 0;
                                                    iExisteXML = 1;
                                                }
                                                else
                                                {
                                                    sBase = sCadArchivo;
                                                    sXML_OMA = sCadArchivo;
                                                }

                                                //if (iStatus == 0)
                                                //    dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                            }
                                            //iStatus = 1;
                                            sBase = sCadArchivo;
                                            sXML_OMA = sCadArchivo;
                                            //}
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        if (iStatus == 0)
                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                    }
                }
                gvResultado.DataSource = (System.Data.DataTable)ViewState["vsDataTable"];
                gvResultado.DataBind();

                if (gvResultado.Rows.Count > 0)
                {
                    int rowCount = gvResultado.Rows.Count;

                    if (rowCount > 0)
                    {
                        msgError.Visible = true;
                        lblError.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                    }
                    return false;
                }
                else
                {
                    msgSuccesss.Visible = true;
                    lblSuccess.Text = "El archivo cumple los requerimientos.";
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecorrerDtExcelOMA(System.Data.DataTable dtContent, System.Data.DataTable dtHeader)
        {
            try
            {
                sFormato = hdnSeleccionFormato.Value.S();
                new DBUtils().GetLoadInitialValues();
                string sRows = string.Empty;
                string sNoFactura = string.Empty;
                string sFechaFac = string.Empty;
                string sFechaOperacion = string.Empty;
                string sFechaContable = string.Empty;
                string sFechaExp = string.Empty;
                string sFechaImp = string.Empty;
                string sMes = string.Empty;
                List<Factura> oLsFacturas = new List<Factura>();
                bool bFechaOpe = false;

                string sXMLChar = string.Empty;
                String[] _sXMLChar;

                string sGetXML = string.Empty;
                string sRutaXML = string.Empty;
                string sReferencia = string.Empty;

                sFechaContable = txtFechaContable.Text;
                sReferencia = txtComentarios.Text;
                sLugar = txtIATA.Text;

                System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                for (int i = 0; i < dtHeader.Rows.Count; i++)
                {
                    Factura oFactura = new Factura();
                    sNoFactura = sFactura;
                    sFechaFac = sFechaFactura;
                    sFechaExp = sFechaFac;

                    //sGetXML = sNoFactura + ".XML";
                    //sRutaXML = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestUrlXML\OTHER\" + sGetXML;

                    for (int x = 0; x < dtContent.Rows.Count; x++)
                    {
                        ConceptosFactura oConcepto = new ConceptosFactura();
                        string sProyecto = string.Empty;
                        string sDimension1 = string.Empty;
                        string sDimension3 = string.Empty;
                        string sDimension4 = string.Empty;
                        string sDimension5 = string.Empty;

                        if (x == 0)
                        {
                            //Arma Header
                            oFactura.iId = i + 1;
                            oFactura.sEmpresa = "1";
                            oFactura.sSucursal = "1";
                            oFactura.sProveedor = sCodigoProveedor;
                            oFactura.sNoFactura = sNoFactura;
                            oFactura.sTipoFactura = "";
                            oFactura.iTimbrar = 0;
                            oFactura.sMetodoPago = "";
                            oFactura.sFormaPago = "";
                            oFactura.sUsoCFDI = "";
                            DateTime dtFecha = DateTime.Now;
                            //decimal dTipoCambio = ObtieneTipoCambio(fRows[i]["MONEDA"].S(), ref dtFecha, fRows[i]["FECHA DOC (añomesdia)"].S());
                            decimal dTipoCambio = ObtieneTipoCambio("MXN", ref dtFecha, sFechaFac);
                            oFactura.dtFecha = sFechaFac.Dt();
                            oFactura.dtFechaExp = sFechaExp.Dt();
                            oFactura.dtFechaImp = sFechaContable.Dt();
                            oFactura.dTipoCambio = dTipoCambio;
                            oFactura.sMsg = "";

                            sMes = obtenerNombreMesNumero(dtFecha.Month.I());

                            oFactura.sComentarios = sReferencia; //sMes + " " + dtFecha.Year.S() + " OMA";
                            oFactura.sSerie = "";
                            oFactura.sMoneda = "MXN";
                            oFactura.dDescuento = 0;

                            //Valida y Obtiene informaxión de XML
                            if (eGetValidaXML != null)
                                eGetValidaXML(null, EventArgs.Empty);

                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                            {
                                //if (File.Exists(sCadArchivo))
                                if (File.Exists(sXML_OMA))
                                {
                                    sXMLChar = ObtenerDatosXML(sXML_OMA);
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
                        //Arma Detalle
                        oConcepto.iId = i + 1;
                        oConcepto.sEmpresa = "1";
                        oConcepto.iLinea = x + 1;
                        oConcepto.sItem = sCodigoArticulo;

                        if (!string.IsNullOrEmpty(sCodigoArticulo))
                        {
                            System.Data.DataTable dtDesArticulo = new DBAccesoSAP().DBGetObtieneDescripcionArticulo(sCodigoArticulo);

                            if (dtDesArticulo.Rows.Count > 0)
                                sDescripcionArticulo = dtDesArticulo.Rows[0][0].S();
                            else
                                sDescripcionArticulo = string.Empty;

                            System.Data.DataTable dtCodImpuesto = new DBAccesoSAP().DBGetObtieneCodigoImpuestoArticulo(sCodigoArticulo);

                            if (dtCodImpuesto != null && dtCodImpuesto.Rows.Count > 0)
                                sCodImp = dtCodImpuesto.Rows[0][0].S();
                            else
                                sCodImp = string.Empty;
                        }

                        oConcepto.sDescripcionUsuario = sDescripcionArticulo;
                        oConcepto.sCodBarras = "";
                        oConcepto.dCantidad = 1;
                        oConcepto.dPrecio = dtContent.Rows[x]["IMPORTE TOTAL"].S().D();
                        oConcepto.sCodigoImpuesto = sCodImp;
                        oConcepto.dDescuento = 0;
                        oConcepto.iImpuesto = 0;
                        oConcepto.dTotalLinea = dtContent.Rows[x]["IMPORTE TOTAL"].S().D();
                        oConcepto.sCuenta = "";
                        oConcepto.sAlmacen = "";

                        bFechaOpe = EsFecha(dtContent.Rows[x]["PERIODO INICIAL"].S());

                        if (bFechaOpe == true)
                            sFechaOperacion = dtContent.Rows[x]["PERIODO INICIAL"].Dt().ToShortDateString();
                        else
                            sFechaOperacion = DateTime.Now.ToShortDateString();

                        oConcepto.sFechaOperacion = sFechaOperacion;

                        System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionMatricula(dtContent.Rows[x]["MATRICULA"].S());

                        if (dtDatos.Rows.Count > 0)
                        {
                            sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                            sDimension3 = dtDatos.Rows[0]["U_Site"].S();
                            sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                        }
                        oConcepto.sProyecto = sProyecto;
                        oConcepto.sDimension1 = sDimension1;
                        oConcepto.sDimension2 = dtContent.Rows[x]["MATRICULA"].S();
                        oConcepto.sDimension3 = sDimension3;
                        oConcepto.sDimension4 = sDimension4;
                        oConcepto.sDimension5 = sDimension5;
                        oConcepto.sXML = sXML_OMA;
                        oConcepto.sPDF = sPDF_OMA;
                        oConcepto.sLugar = sLugar; //ddlBase.SelectedItem.Text;
                        oFactura.oLstConceptos.Add(oConcepto);
                    }
                    oLsFacturas.Add(oFactura);
                }
                ListaFacturas = oLsFacturas;

                if (eSetProcesaArchivo != null)
                    eSetProcesaArchivo(null, null);

                if (dtError != null)
                {
                    if (dtError.Rows.Count > 0)
                    {
                        string sCadError = string.Empty;
                        sCadError = "<ul>";

                        for (int i = 0; i < dtError.Rows.Count; i++)
                        {
                            sCadError += "<li>" + dtError.Rows[i]["DescripcionError"].S() + "</li>";
                        }
                        sCadError += "</ul>";
                        msgError.Visible = true;
                        lblError.Text = sCadError;
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                    }
                }
                else
                {
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    msgSuccesss.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VALIDACIONES FORMATO LAYOUT AMAIT
        public System.Data.DataTable ModificarOrigen(System.Data.DataTable dtOrigen)
        {
            try
            {
                //Separa datos de la columna matricula y crea las nuevas columnas
                int col = dtOrigen.Columns.Count;

                dtOrigen.Columns.Add("MATRICULA_", typeof(string));
                dtOrigen.Columns.Add("LUGAR", typeof(string));
                dtOrigen.Columns.Add("FECHA_OPERACION", typeof(string));
                dtOrigen.Columns.Add("AVION", typeof(string));
                dtOrigen.Columns.Add("ARTICULO", typeof(string));

                for (int i = 0; i < dtOrigen.Rows.Count; i++)
                {
                    //string[] arrFechaOp;
                    string[] arrCadena;
                    //string sDia = string.Empty;
                    //string sMes = string.Empty;
                    //string sAnio = string.Empty;

                    int iPos = 0;
                    string sTab = dtOrigen.Rows[i][3].S().Replace(", ", ",").Replace("; ", ";").Replace("-", "").Replace(": ", ";");

                    if (sTab.IndexOf(" A") > 0)
                        iPos = sTab.IndexOf(" A");
                    if (sTab.IndexOf(" C") > 0)
                        iPos = sTab.IndexOf(" C");
                    else if (sTab.IndexOf(" D") > 0)
                        iPos = sTab.IndexOf(" D");
                    else if (sTab.IndexOf(" K") > 0)
                        iPos = sTab.IndexOf(" K");
                    else if (sTab.IndexOf(" M") > 0)
                        iPos = sTab.IndexOf(" M");
                    if (sTab.IndexOf(" S") > 0)
                        iPos = sTab.IndexOf(" S");
                    if (sTab.IndexOf(" Q") > 0)
                        iPos = sTab.IndexOf(" Q");

                    if (iPos > 0)
                    {
                        sTab = sTab.Remove(iPos, 1).Insert(iPos, ";");
                        arrCadena = sTab.Split(';', ',', ':');
                    }
                    else
                        arrCadena = dtOrigen.Rows[i][3].S().Split(';', ',', ':');

                    dtOrigen.Rows[i][4] = arrCadena[0].TrimStart(' ').Replace("-", ""); // Quita el guión (-) de Matricula si es que lo trae por default para dar formato


                    if (arrCadena[1].TrimStart(' ').Length <= 4) // Por si base no contiene información
                        dtOrigen.Rows[i][5] = arrCadena[1].TrimStart(' ');
                    else
                        dtOrigen.Rows[i][5] = string.Empty;

                    if (arrCadena[1].TrimStart(' ').Length == 10) // Por si fecha viene en el lugar de base
                    {
                        dtOrigen.Rows[i][6] = arrCadena[1].TrimStart(' ');
                        dtOrigen.Rows[i][7] = arrCadena[2].TrimStart(' ');
                        dtOrigen.Rows[i][8] = arrCadena[3].TrimStart(' ');
                    }
                    else
                    {
                        dtOrigen.Rows[i][6] = arrCadena[2].TrimStart(' ');
                        dtOrigen.Rows[i][7] = arrCadena[3].TrimStart(' ');
                        dtOrigen.Rows[i][8] = arrCadena[4].TrimStart(' ');
                    }



                }
                // Eliminar columna original de Matricula
                dtOrigen.Columns.Remove(dtOrigen.Columns["MATRICULA"]);
                //Modifica las matriculas que inician con "X" de nuestro origen de datos
                dtOrigen.Select("MATRICULA_ Like 'X%'");

                foreach (DataRow row in dtOrigen.Rows)
                {
                    string sFormatMat = row["MATRICULA_"].S();
                    string sFormatFac = row["FACTURA"].S();

                    if (sFormatMat.IndexOf('X') != -1)
                    {
                        string sFirstPartMat = sFormatMat.Substring(0, 2);
                        string sSecondPartMat = sFormatMat.Substring(2, sFormatMat.Length - 2);
                        row["MATRICULA_"] = sFirstPartMat + '-' + sSecondPartMat;
                    }
                    row["FACTURA"] = "00" + sFormatFac;
                }
                dtOrigen.Columns["MATRICULA_"].ColumnName = "MATRICULA";
                return dtOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool ValidaExcelAMAIT(System.Data.DataTable dtUnionAMAIT)
        {
            try
            {
                ViewState["vsDataTable"] = null;
                msgSuccesss.Visible = false;
                lblSuccess.Text = string.Empty;
                msgError.Visible = false;
                lblError.Text = string.Empty;
                pnlSimulacion.Visible = false;

                string sFactura = string.Empty;
                string sFechaFactura = string.Empty;
                string sCodPro = string.Empty;
                int iFila = 9;
                bool blBanExist = false;
                string strCampo = string.Empty;
                string strValor = string.Empty;
                string strExcepcion = string.Empty;
                int iStatus = 0;
                //Variables para validar formato de Fechas
                string[] format = { "yyyyMMdd" };
                DateTime date;
                string strFecha = string.Empty;
                //Variables para validar cantidad, importe y total
                string strNumeric = string.Empty;
                NumberStyles styles;
                double dResult;
                bool blBanCodPro = false;

                dtUnionAMAIT.Columns.Add("PDF", typeof(string));
                dtUnionAMAIT.Columns.Add("XML", typeof(string));

                sEmpresa = "AMAIT";

                if (!string.IsNullOrEmpty(sEmpresa))
                {
                    if (eGetCodProveedores != null)
                        eGetCodProveedores(null, null);
                }
                sBase = ddlBase.SelectedItem.Text;

                foreach (DataRow dRow in dtUnionAMAIT.Rows)
                {
                    iFila += 1;
                    sValoresValidacion = string.Empty;
                    int iExistePDF = 0;
                    int iExisteXML = 0;
                    bool blIsDate = false;

                    if (!string.IsNullOrEmpty(sCodigoProveedor))
                        sCodPro = sCodigoProveedor;

                    foreach (DataColumn dColumn in dtUnionAMAIT.Columns)
                    {
                        #region VARIABLES
                        string sCodigoProv = string.Empty;
                        strFecha = string.Empty;
                        strCampo = string.Empty;
                        strValor = string.Empty;
                        strExcepcion = string.Empty;
                        strNumeric = string.Empty;
                        dResult = 0;
                        iStatus = 1;
                        #endregion

                        #region VALIDAR CODIGO PROVEEDOR
                        // VALIDAR SI EXISTE EN SAP
                        //if (blBanCodPro == false)
                        //{
                        if (!string.IsNullOrEmpty(sCodPro))
                        {
                            if (!string.IsNullOrEmpty(sCodPro))
                            {
                                if (eSearchProveedores != null)
                                    eSearchProveedores(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = "CODIGO PROVEEDOR";
                                    strValor = sCodPro;
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = "CODIGO PROVEEDOR";
                                strValor = sCodPro;
                                strExcepcion = "El campo 'CODIGO PROVEEDOR' viene vacio, favor de verificar";
                                iStatus = 0;
                            }

                            if (iStatus == 1)
                            {
                                blBanCodPro = true;
                                sValoresValidacion = sCodPro;
                                sCodigoProv = sCodPro;
                            }
                        }
                        //}
                        #endregion

                        #region VALIDAR MONEDA
                        //VALIDAR CON SAP
                        //if (dRow[dColumn] != null && dColumn.S() == "MONEDA")
                        //{
                        //    sMoneda = "MXN"; //dRow[dColumn].S();

                        //    if (!string.IsNullOrEmpty(sMoneda))
                        //    {
                        //        if (eSearchMoneda != null)
                        //            eSearchMoneda(null, null);

                        //        //Validación en SAP
                        //        if (dtExiste.Rows[0][0].S().I() == 0)
                        //        {
                        //            strCampo = dColumn.S();
                        //            strValor = dRow[dColumn].S();
                        //            strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                        //            iStatus = 0;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        strCampo = dColumn.S();
                        //        strValor = dRow[dColumn].S();
                        //        strExcepcion = "El campo 'MONEDA' viene vacio, favor de verificar";
                        //        iStatus = 0;
                        //    }
                        //}
                        #endregion

                        #region VALIDAR FECHA FACTURACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "FECHA")
                        {
                            //DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            blIsDate = EsFechaAMAIT(dRow[dColumn].S());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo " + dColumn.S() + " se encuentra vacio.";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    strFecha = dRow[dColumn].S().Dt().ToShortDateString();
                                    sFechaFactura = FormatoFecha(strFecha);
                                }
                                else
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'FECHA' no contiene una fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }

                            //if (iStatus == 1)
                            //    sValoresValidacion += "|" + sFechaFactura;
                        }

                        #endregion

                        #region VALIDAR FACTURA
                        if (dRow[dColumn] != null && dColumn.S() == "FACTURA")
                        {
                            sFactura = dRow[dColumn].S();

                            if (string.IsNullOrEmpty(sFactura))
                            {
                                strCampo = "FACTURA";
                                strValor = sFactura;
                                strExcepcion = "El campo 'FACTURA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }

                            if (iStatus == 1)
                            {
                                sValoresValidacion += "|" + sFactura;

                                if (iStatus == 1)
                                {
                                    //sFechaFactura = FormatoFecha(sFechaFactura.Dt().ToShortDateString());
                                    if (!string.IsNullOrEmpty(sFechaFactura))
                                        sValoresValidacion += "|" + sFechaFactura;
                                }

                                if (blBanExist == false)
                                {
                                    if (new DBAccesoSAP().DBGetValidaExisteFactura(sCodigoProveedor, sFactura))
                                    {
                                        blBanExist = true;
                                        dtRow(iFila, "PROVEEDOR/FACTURA", sCodigoProveedor + "/" + sFactura, 0, "El numero de factura ya existe con este proveedor, favor de verificar");
                                    }
                                }
                            }


                        }
                        #endregion

                        #region VALIDAR CODIGO ARTICULO POR MEDIO DE LA DESCRIPCIÓN DEL ARTÍCULO
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "ARTICULO")
                        {
                            sDesArticulo = dRow[dColumn].S();

                            if (string.IsNullOrEmpty(sDesArticulo))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "Campo vacio.";
                                iStatus = 0;
                            }
                            else
                            {
                                if (eGetCodArticulo != null)
                                    eGetCodArticulo(null, null);

                                if (string.IsNullOrEmpty(sCodigoArticulo))
                                {
                                    strCampo = "CODIGO ARTICULO";
                                    strValor = sCodigoArticulo;
                                    strExcepcion = "El campo 'CODIGO ARTICULO' viene vacio, favor de verificar";
                                    iStatus = 0;
                                }

                            }
                        }
                        #endregion

                        #region VALIDAR IMPORTE
                        if (dRow[dColumn] != null && dColumn.S() == "IMPORTE")
                        {
                            if (!string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strNumeric = dRow[dColumn].S().Replace("$", "").Db().S();
                                styles = NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;

                                if (!Double.TryParse(strNumeric, styles, CultureInfo.InvariantCulture, out dResult))
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'IMPORTE' no tiene el formato correcto, favor de verificar";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'IMPORTE' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR MATRICULA
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "MATRICULA")
                        {
                            sMatricula = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sMatricula))
                            {
                                if (eSearchMatricula != null)
                                    eSearchMatricula(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    //strCampo = dColumn.S();
                                    //strValor = dRow[dColumn].S();
                                    //strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    //iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'MATRICULA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR BASE
                        string[] sValores = sValoresValidacion.Split('|');

                        if (sValores.Length == 3)
                        {
                            if (!string.IsNullOrEmpty(sBase) && sBase != ".:Seleccione:.")
                            {
                                if (string.IsNullOrEmpty(sBase))
                                    iStatus = 0;

                                //if (string.IsNullOrEmpty(sPDF_OMA) && string.IsNullOrEmpty(sXML_OMA))
                                //{
                                if (iStatus == 1)
                                {
                                    if (iExistePDF == 0 && iExisteXML == 0)
                                        sValoresValidacion += ("|" + sBase);

                                    #region VALIDACION DE DOCUMENTOS REQUERIDOS
                                    System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                                    if (dtProv != null && dtProv.Rows.Count > 0)
                                    {
                                        #region VALIDAR PDF
                                        if (iExistePDF == 0)
                                        {
                                            if (iStatus == 1)
                                            {
                                                if (eGetValidaPDF != null)
                                                    eGetValidaPDF(null, EventArgs.Empty);

                                                if (dtProv.Rows[0]["U_REQPDF"].S() == "SI")
                                                {
                                                    if (sPDF_OMA == string.Empty)
                                                        sPDF_OMA = sCadArchivo;

                                                    if (!File.Exists(sCadArchivo))
                                                    {
                                                        strCampo = "Base";
                                                        strValor = sCadArchivo;
                                                        strExcepcion = "El archivo PDF no se encontró en la ubicación adecuada, favor de verificar";
                                                        iStatus = 0;
                                                        iFila = 0;
                                                        iExistePDF = 1;
                                                    }
                                                    else
                                                    {
                                                        dRow["PDF"] = sCadArchivo;
                                                        //sBase = sCadArchivo;
                                                        sPDF_OMA = sCadArchivo;
                                                    }

                                                    if (iStatus == 0)
                                                        dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                                }
                                                dRow["PDF"] = sCadArchivo;
                                                //sBase = sCadArchivo;
                                                sPDF_OMA = sCadArchivo;
                                            }
                                        }

                                        #endregion

                                        #region VALIDAR XML
                                        if (iExisteXML == 0)
                                        {
                                            //if (iStatus == 1)
                                            //{
                                            if (eGetValidaXML != null)
                                                eGetValidaXML(null, EventArgs.Empty);

                                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                                            {
                                                if (sXML_OMA == string.Empty)
                                                    sXML_OMA = sCadArchivo;

                                                if (!File.Exists(sCadArchivo))
                                                {
                                                    strCampo = "Base";
                                                    strValor = sCadArchivo;
                                                    strExcepcion = "El archivo XML no se encontró en la ubicación adecuada, favor de verificar";
                                                    iStatus = 0;
                                                    iFila = 0;
                                                    iExisteXML = 1;
                                                }
                                                else
                                                {
                                                    //sBase = sCadArchivo;
                                                    dRow["XML"] = sCadArchivo;
                                                    sXML_OMA = sCadArchivo;
                                                }

                                                //if (iStatus == 0)
                                                //    dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                            }
                                            //iStatus = 1;
                                            //sBase = sCadArchivo;
                                            dRow["XML"] = sCadArchivo;
                                            sXML_OMA = sCadArchivo;
                                            //}
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                //}
                            }
                        }
                        #endregion

                        #region VALIDAR FECHA OPERACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "FECHA_OPERACION")
                        {
                            //DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            blIsDate = EsFechaAMAIT(dRow[dColumn].S());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = "MATRICULA"; //dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'MATRICULA' no contiene fecha de operación, favor de verificar";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    strFecha = dRow[dColumn].S().Dt().ToShortDateString();
                                    strFecha = FormatoFecha(strFecha);
                                }
                                else
                                {
                                    strCampo = "MATRICULA"; //dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'MATRICULA' no contiene una fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }
                        }

                        #endregion

                        if (iStatus == 0)
                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                    }

                }
                gvResultado.DataSource = (System.Data.DataTable)ViewState["vsDataTable"];
                gvResultado.DataBind();

                if (gvResultado.Rows.Count > 0)
                {
                    int rowCount = gvResultado.Rows.Count;

                    if (rowCount > 0)
                    {
                        msgError.Visible = true;
                        lblError.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                    }
                    return false;
                }
                else
                {
                    msgSuccesss.Visible = true;
                    lblSuccess.Text = "El archivo cumple los requerimientos.";
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecorrerDtExcelAMAIT(System.Data.DataTable dtContent)
        {
            try
            {
                sFormato = hdnSeleccionFormato.Value.S();
                new DBUtils().GetLoadInitialValues();
                string sRows = string.Empty;
                string sNoFactura = string.Empty;
                string sFechaOp = string.Empty;
                string sProyecto = string.Empty;
                string sDimension1 = string.Empty;
                string sDimension2 = string.Empty;
                string sDimension3 = string.Empty;
                string sDimension4 = string.Empty;
                string sDimension5 = string.Empty;
                List<Factura> oLsFacturas = new List<Factura>();
                string sMes = string.Empty;
                int iHead = 0;
                bool bFechaOpe = false;
                string sFechaOperacion = string.Empty;

                //System.Data.DataTable distinctDT = SelectDistinct(dtContent, "FACTURA");
                DataRow[] fRows;

                string sXMLChar = string.Empty;
                String[] _sXMLChar;

                string sGetXML = string.Empty;
                string sRutaXML = string.Empty;
                string sFechaContable = string.Empty;
                string sReferencia = string.Empty;

                sFechaContable = txtFechaContable.Text;
                sReferencia = txtComentarios.Text;

                System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                for (int i = 0; i < dtContent.Rows.Count; i++)
                {
                    Factura oFactura = new Factura();

                    sNoFactura = dtContent.Rows[i][1].S();
                    fRows = dtContent.Select("FACTURA='" + sNoFactura + "'");

                    sMatricula = dtContent.Rows[i]["MATRICULA"].S();

                    sGetXML = sNoFactura + ".XML";
                    sRutaXML = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestUrlXML\OTHER\" + sGetXML;

                    if (eSearchMatricula != null)
                        eSearchMatricula(null, null);

                    //Validación en SAP
                    if (dtExiste.Rows[0][0].S().I() == 0)
                        sDimension1 = "S19";
                    else
                        sDimension1 = string.Empty;

                    for (int x = 0; x < fRows.Length; x++)
                    {
                        ConceptosFactura oConcepto = new ConceptosFactura();

                        if (x == 0)
                        {
                            // Armar Header
                            oFactura.iId = i + 1;
                            oFactura.sEmpresa = "1";
                            oFactura.sSucursal = "1";
                            oFactura.sProveedor = sCodigoProveedor;//fRows[i]["CODIGO PROVEEDOR"].S();
                            oFactura.sNoFactura = fRows[x]["FACTURA"].S();
                            oFactura.sTipoFactura = "";
                            oFactura.iTimbrar = 0;
                            oFactura.sMetodoPago = "";
                            oFactura.sFormaPago = "";
                            oFactura.sUsoCFDI = "";
                            DateTime dtFecha = DateTime.Now;
                            decimal dTipoCambio = ObtieneTipoCambio("MXN", ref dtFecha, fRows[x]["FECHA"].S());
                            oFactura.dtFecha = fRows[x]["FECHA"].S().Dt();
                            oFactura.dtFechaExp = fRows[x]["FECHA"].S().Dt();
                            oFactura.dtFechaImp = sFechaContable.Dt();
                            oFactura.dTipoCambio = dTipoCambio;
                            oFactura.sMsg = "";

                            sMes = obtenerNombreMesNumero(dtFecha.Month.I());

                            oFactura.sComentarios = sReferencia; //sMes + " " + dtFecha.Year.S() + " AMAIT";
                            oFactura.sSerie = "";
                            oFactura.sMoneda = "MXN";
                            oFactura.dDescuento = 0;

                            //Valida y Obtiene informaxión de XML
                            if (eGetValidaXML != null)
                                eGetValidaXML(null, EventArgs.Empty);

                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                            {
                                //if (File.Exists(sCadArchivo))
                                if (File.Exists(fRows[x]["XML"].S()))
                                {
                                    sXMLChar = ObtenerDatosXML(fRows[x]["XML"].S());
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

                        bFechaOpe = EsFecha(fRows[x]["FECHA_OPERACION"].S());

                        if (bFechaOpe == true)
                            sFechaOperacion = fRows[x]["FECHA_OPERACION"].Dt().ToShortDateString();
                        else
                            sFechaOperacion = DateTime.Now.ToShortDateString();

                        //DateTime FechaOperacion = DateTime.ParseExact(fRows[x]["FECHA_OPERACION"].S(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        //sFechaOp = FechaOperacion.ToShortDateString();

                        //Agregar a Detalle
                        oConcepto.iId = i + 1;
                        oConcepto.sEmpresa = "1";
                        oConcepto.iLinea = x + 1;
                        oConcepto.sItem = sCodigoArticulo; //fRows[x]["CODIGO ARTICULO"].S();

                        if (!string.IsNullOrEmpty(sCodigoArticulo))
                        {
                            System.Data.DataTable dtDesArticulo = new DBAccesoSAP().DBGetObtieneDescripcionArticulo(sCodigoArticulo);

                            if (dtDesArticulo.Rows.Count > 0)
                                sDescripcionArticulo = dtDesArticulo.Rows[0][0].S();
                            else
                                sDescripcionArticulo = string.Empty;

                            System.Data.DataTable dtCodImpuesto = new DBAccesoSAP().DBGetObtieneCodigoImpuestoArticulo(sCodigoArticulo);

                            if (dtCodImpuesto != null && dtCodImpuesto.Rows.Count > 0)
                                sCodImp = dtCodImpuesto.Rows[0][0].S();
                            else
                                sCodImp = string.Empty;
                        }

                        double dImporte = fRows[x]["IMPORTE"].S().Replace("$", "").Db();
                        double dIvaDiv = 1.16;
                        double dtotal = dImporte / dIvaDiv;

                        oConcepto.sDescripcionUsuario = sDescripcionArticulo;
                        oConcepto.sCodBarras = "";
                        oConcepto.dCantidad = 1;
                        oConcepto.dPrecio = decimal.Parse(dtotal.S());
                        oConcepto.sCodigoImpuesto = sCodImp;
                        oConcepto.dDescuento = 0;
                        oConcepto.iImpuesto = 0;
                        oConcepto.dTotalLinea = decimal.Parse(dtotal.S());
                        oConcepto.sCuenta = "";
                        oConcepto.sAlmacen = "";

                        if (sDimension1 == "S19")
                        {
                            sProyecto = "ALE";
                            sDimension1 = "S19";
                            sDimension2 = string.Empty;
                            sDimension3 = "TLC";
                            sDimension4 = "VLO";
                            sDimension5 = string.Empty;
                        }
                        else
                        {
                            System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionMatricula(sMatricula);

                            if (dtDatos.Rows.Count > 0)
                            {
                                sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                                sDimension3 = dtDatos.Rows[0]["U_Site"].S();
                                sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                            }
                            sDimension1 = string.Empty;
                            sDimension2 = fRows[x]["MATRICULA"].S();
                            sDimension5 = string.Empty;
                        }

                        oConcepto.sProyecto = sProyecto;
                        oConcepto.sDimension1 = sDimension1;
                        oConcepto.sDimension2 = sDimension2;
                        oConcepto.sDimension3 = sDimension3;
                        oConcepto.sDimension4 = sDimension4;
                        oConcepto.sDimension5 = sDimension5;
                        oConcepto.sXML = fRows[x]["XML"].S(); //sXML_OMA;
                        oConcepto.sPDF = fRows[x]["PDF"].S(); //sPDF_OMA;
                        oConcepto.sLugar = "TLC"; //ddlBase.SelectedItem.Text;
                        oConcepto.sFechaOperacion = sFechaOperacion;
                        oFactura.oLstConceptos.Add(oConcepto);
                    }
                    oLsFacturas.Add(oFactura);
                }
                ListaFacturas = oLsFacturas;

                if (eSetProcesaArchivo != null)
                    eSetProcesaArchivo(null, null);


                if (!string.IsNullOrEmpty(sMsgError))
                {
                    msgError.Visible = true;
                    lblError.Text = sMsgError;
                    msgSuccesss.Visible = false;
                    lblSuccess.Text = string.Empty;
                }
                else
                {
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    msgSuccesss.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VALIDACIONES FORMATO ASA
        protected void btnProcesarFacturas_Click(object sender, EventArgs e)
        {
            try
            {
                //Procesar facturas seleccionadas ASA
                if (hdnSeleccionFormato.Value == "4")
                {
                    //String sRutaArchivo = @"C:\Users\Administrador\Documents" + @"\FACTURAS";
                    String sRutaArchivo = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASA"].S() + @"\ArchivosProcesados";
                    string sArchivo = string.Empty;

                    if (Directory.Exists(sRutaArchivo))
                    {
                        foreach (GridViewRow row in gvProcesados.Rows)
                        {
                            bool bValidacion = false;

                            sArchivo = gvProcesados.Rows[row.RowIndex].Cells[1].Text; //Factura ó nombre de Archivo

                            if (!string.IsNullOrEmpty(sArchivo))
                            {
                                dtDatosFac = LeerArchivo(sRutaArchivo, sArchivo);

                                if (dtDatosFac != null)
                                {
                                    if (dtDatosFac.Rows.Count > 0)
                                    {
                                        bValidacion = ValidaArchivoTXTASA(dtDatosFac, sArchivo);

                                        if (hdnSeleccionGral.Value == "2")
                                        {
                                            if (bValidacion == true)
                                                RecorrerDtTXTASA(dtDatosFac, sArchivo);
                                        }

                                    }
                                }
                            }

                        }
                    }

                    //Llenado de errores
                    #region LLENADO DE ERRORES
                    if (dtAllError != null)
                    {
                        if (dtAllError.Rows.Count > 0)
                        {
                            System.Data.DataTable dtSinDupl = new System.Data.DataTable();
                            dtSinDupl.Columns.Add("Fila", typeof(string));
                            dtSinDupl.Columns.Add("Campo", typeof(string));
                            dtSinDupl.Columns.Add("Valor", typeof(string));
                            dtSinDupl.Columns.Add("Status", typeof(string));
                            dtSinDupl.Columns.Add("Descripcion", typeof(string));

                            DataView vista = new DataView(dtAllError);
                            dtSinDupl = vista.ToTable(true, "Fila", "Campo", "Valor", "Status", "Descripcion");

                            gvResultado.DataSource = dtSinDupl;//(System.Data.DataTable)ViewState["vsDataTable"]; //dtAllError;//dtSinDupl;
                            gvResultado.DataBind();


                            if (gvResultado.Rows.Count > 0)
                            {
                                int rowCount = gvResultado.Rows.Count;

                                if (rowCount > 0)
                                {
                                    msgError.Visible = true;
                                    lblError.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                                    msgSuccesss.Visible = false;
                                    lblSuccess.Text = string.Empty;
                                    pnlRegASA.Visible = false;
                                    pnlSimulacion.Visible = true;
                                }
                            }

                        }
                    }
                    else
                    {
                        if (iBanCorrecto == 1 && hdnSeleccionGral.Value == "2")
                        {
                            msgSuccesss.Visible = true;
                            lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                            msgError.Visible = false;
                            lblError.Text = string.Empty;
                            pnlSimulacion.Visible = false;
                            pnlRegASA.Visible = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(sMsgError))
                            {
                                msgSuccesss.Visible = false;
                                lblSuccess.Text = string.Empty;
                                msgError.Visible = true;
                                lblError.Text = sMsgError;
                            }
                        }

                        if (iBanCorrecto == 0 && hdnSeleccionGral.Value == "1")
                        {
                            msgSuccesss.Visible = true;
                            lblSuccess.Text = "El archivo cumple los requerimientos.";
                            msgError.Visible = false;
                            lblError.Text = string.Empty;
                            pnlSimulacion.Visible = false;
                            pnlRegASA.Visible = true;
                        }



                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                msgError.Visible = true;
                lblError.Text = ex.Message;
                //throw ex;
            }
        }
        protected System.Data.DataTable LeerArchivo(string sRutaArchivo, string sArchivo)
        {
            try
            {
                string sTXTFind = sRutaArchivo + @"\" + sArchivo + ".txt";
                string sNoFactura = sArchivo;

                if (File.Exists(sTXTFind))
                {
                    System.Data.DataTable dtFilasTexto = new System.Data.DataTable();
                    dtDatosFac = new System.Data.DataTable();
                    dtDatosFac.Columns.Add("Aeropuerto", typeof(string));
                    dtDatosFac.Columns.Add("Factura", typeof(string));
                    dtDatosFac.Columns.Add("Fecha Factura", typeof(string));
                    dtDatosFac.Columns.Add("Producto", typeof(string));
                    dtDatosFac.Columns.Add("Matricula", typeof(string));
                    dtDatosFac.Columns.Add("Fecha/Hora ini", typeof(string));
                    dtDatosFac.Columns.Add("Importe", typeof(string));
                    dtDatosFac.Columns.Add("Lugar", typeof(string));

                    dtDatosFac.Columns.Add("M3", typeof(string)); //Cantidad

                    dtFilasTexto.Columns.Add("LineasArchivo", typeof(string));

                    using (StreamReader lector = new StreamReader(sTXTFind))
                    {
                        string sLine = string.Empty;
                        string[] sArrFac;
                        string slinea = string.Empty;
                        int iCount;

                        while (lector.Peek() > -1)
                        {
                            slinea = lector.ReadLine();
                            iCount = slinea.IndexOf("|", 0);

                            if (iCount > -1)
                            {
                                DataRow drow = dtFilasTexto.NewRow();
                                drow["LineasArchivo"] = slinea;
                                dtFilasTexto.Rows.Add(drow);
                            }
                        }

                        if (dtFilasTexto.Rows.Count > 0)
                        {
                            int[] arrInt = new int[7];

                            for (int i = 0; i < 1; i++)
                            {
                                sArrFac = dtFilasTexto.Rows[i][0].S().Split('|');

                                string[] arrCols = { "Aeropuerto", "Fecha Factura", "Concepto", "Matricula", "Fecha/Hora ini", "Importe", "M3" };

                                for (int x = 0; x < sArrFac.Length; x++)
                                {
                                    if (sArrFac[x].IndexOf("Aeropuerto") != -1)
                                        if (sArrFac[x] == arrCols[0])
                                            arrInt[0] = x;

                                    if (sArrFac[x].IndexOf("Fecha Factura") != -1)
                                        if (sArrFac[x] == arrCols[1])
                                            arrInt[1] = x;

                                    if (sArrFac[x].IndexOf("Concepto") != -1 || sArrFac[x].IndexOf("Producto") != -1)
                                        if (sArrFac[x] == arrCols[2] || sArrFac[x] == "Producto")
                                            arrInt[2] = x;


                                    if (sArrFac[x].IndexOf("Matricula") != -1)
                                        if (sArrFac[x] == arrCols[3])
                                            arrInt[3] = x;

                                    if (sArrFac[x].IndexOf("Fecha/Hora ini") != -1)
                                        arrInt[4] = x;

                                    if (sArrFac[x].IndexOf("Importe") != -1 || sArrFac[x].IndexOf("I") != -1)
                                        if (sArrFac[x] == arrCols[5] || sArrFac[x] == "I")
                                            arrInt[5] = x;
                                    //Cantidad
                                    if (sArrFac[x].IndexOf("M3") != -1)
                                        if (sArrFac[x] == arrCols[6])
                                            arrInt[6] = x;
                                }
                            }


                            for (int i = 1; i < dtFilasTexto.Rows.Count; i++)
                            {
                                string[] arrLugar;
                                sArrFac = dtFilasTexto.Rows[i][0].S().Split('|');

                                DataRow dRowText = dtDatosFac.NewRow();

                                dRowText["Aeropuerto"] = sArrFac[arrInt[0]].S();
                                dRowText["Factura"] = sNoFactura;
                                dRowText["Fecha Factura"] = sArrFac[arrInt[1]].S();
                                dRowText["Producto"] = sArrFac[arrInt[2]].S();
                                dRowText["Matricula"] = sArrFac[arrInt[3]].S();
                                dRowText["Fecha/Hora ini"] = sArrFac[arrInt[4]].S();
                                dRowText["Importe"] = sArrFac[arrInt[5]].S();

                                arrLugar = sArrFac[0].S().Split(',');
                                dRowText["Lugar"] = arrLugar[1].S().Replace(" ", "").Replace(".", "");
                                dRowText["M3"] = sArrFac[arrInt[6]].S();
                                dtDatosFac.Rows.Add(dRowText);
                            }
                            dtDatosFac.AcceptChanges();
                        }


                    }
                }
                return dtDatosFac;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool ValidaArchivoTXTASA(System.Data.DataTable dtDatosFac, string sArchivo)
        {
            try
            {
                #region VARIABLES LOCALES
                bool ban = false;
                //ViewState["vsDataTable"] = null;
                msgSuccesss.Visible = false;
                lblSuccess.Text = string.Empty;
                msgError.Visible = false;
                lblError.Text = string.Empty;
                pnlSimulacion.Visible = false;

                string sFactura = sArchivo;
                string sFechaFactura = string.Empty;
                string sCodPro = string.Empty;
                int iFila = 0;
                bool blBanExist = false;
                string strCampo = string.Empty;
                string strValor = string.Empty;
                string strExcepcion = string.Empty;
                int iStatus = 0;
                //Variables para validar formato de Fechas
                string[] format = { "yyyyMMdd" };
                DateTime date;
                string strFecha = string.Empty;
                //Variables para validar cantidad, importe y total
                string strNumeric = string.Empty;
                NumberStyles styles;
                double dResult;
                bool blBanCodPro = false;

                dtDatosFac.Select("Matricula Like 'X%'");

                foreach (DataRow row in dtDatosFac.Rows)
                {
                    string sFormatMat = row["Matricula"].S();

                    if (sFormatMat.IndexOf('X') != -1)
                    {
                        string sFirstPartMat = sFormatMat.Substring(0, 2);
                        string sSecondPartMat = sFormatMat.Substring(2, sFormatMat.Length - 2);
                        row["Matricula"] = sFirstPartMat + '-' + sSecondPartMat;
                    }
                }

                dtDatosFac.Columns.Add("PDF", typeof(string));
                dtDatosFac.Columns.Add("XML", typeof(string));

                sEmpresa = "ASA";

                if (!string.IsNullOrEmpty(sEmpresa))
                {
                    if (eGetCodProveedores != null)
                        eGetCodProveedores(null, null);
                }
                sBase = "TLC";
                #endregion

                #region VALIDACIONES
                foreach (DataRow dRow in dtDatosFac.Rows)
                {
                    iFila += 1;
                    sValoresValidacion = string.Empty;
                    int iExistePDF = 0;
                    int iExisteXML = 0;
                    bool blIsDate = false;
                    string[] sValores;

                    if (!string.IsNullOrEmpty(sCodigoProveedor))
                        sCodPro = sCodigoProveedor;

                    foreach (DataColumn dColumn in dtDatosFac.Columns)
                    {
                        #region VARIABLES
                        string sCodigoProv = string.Empty;
                        strFecha = string.Empty;
                        strCampo = string.Empty;
                        strValor = string.Empty;
                        strExcepcion = string.Empty;
                        strNumeric = string.Empty;
                        dResult = 0;
                        iStatus = 1;

                        #endregion

                        #region VALIDAR CODIGO PROVEEDOR
                        // VALIDAR SI EXISTE EN SAP
                        //if (blBanCodPro == false)
                        //{
                        if (!string.IsNullOrEmpty(sCodPro))
                        {
                            if (!string.IsNullOrEmpty(sCodPro))
                            {
                                if (eSearchProveedores != null)
                                    eSearchProveedores(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = "Archivo " + sArchivo + ".txt - " + "CODIGO PROVEEDOR";
                                    strValor = sCodPro;
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = "Archivo " + sArchivo + ".txt - " + "CODIGO PROVEEDOR";
                                strValor = sCodPro;
                                strExcepcion = "El campo 'CODIGO PROVEEDOR' viene vacio, favor de verificar";
                                iStatus = 0;
                            }

                            if (iStatus == 1)
                            {
                                blBanCodPro = true;
                                if (string.IsNullOrEmpty(sValoresValidacion))
                                    sValoresValidacion = sCodPro;

                                sCodigoProv = sCodPro;
                            }
                        }
                        //}
                        #endregion

                        #region VALIDAR FACTURA *Verificar
                        //Verificar para validar solo una vez
                        if (!string.IsNullOrEmpty(sFactura))
                        {
                            sValores = sValoresValidacion.Split('|');

                            if (sValores.Length == 1)
                            {
                                if (iStatus == 1)
                                {
                                    sValoresValidacion += "|" + sFactura;

                                    if (blBanExist == false)
                                    {
                                        if (new DBAccesoSAP().DBGetValidaExisteFactura(sCodigoProveedor, sFactura))
                                        {
                                            blBanExist = true;
                                            dtRow(iFila, "PROVEEDOR/FACTURA", sCodigoProveedor + "/" + sFactura, 0, "El numero de factura ya existe con este proveedor, favor de verificar");
                                        }
                                    }
                                }
                            }


                        }
                        else
                        {
                            strCampo = "Archivo " + sArchivo + ".txt - " + "FACTURA";
                            strValor = sFactura;
                            strExcepcion = "El campo 'FACTURA' viene vacio, favor de verificar";
                            iStatus = 0;
                        }
                        #endregion

                        #region VALIDAR FECHA FACTURACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "Fecha Factura")
                        {
                            sValores = sValoresValidacion.Split('|');

                            if (sValores.Length == 2)
                            {
                                //DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                                blIsDate = EsFecha(dRow[dColumn].S());

                                if (string.IsNullOrEmpty(dRow[dColumn].S()))
                                {
                                    strCampo = "Archivo " + sArchivo + ".txt - " + dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo " + dColumn.S() + " se encuentra vacio.";
                                    iStatus = 0;
                                }
                                else
                                {
                                    if (blIsDate == true)
                                    {
                                        strFecha = dRow[dColumn].S().Dt().ToShortDateString();
                                        sFechaFactura = FormatoFecha(strFecha);

                                        if (iStatus == 1)
                                        {
                                            //sFechaFactura = FormatoFecha(sFechaFactura.Dt().ToShortDateString());
                                            if (!string.IsNullOrEmpty(sFechaFactura))
                                                sValoresValidacion += "|" + sFechaFactura;
                                        }
                                    }
                                    else
                                    {
                                        strCampo = "Archivo " + sArchivo + ".txt - " + dColumn.S();
                                        strValor = dRow[dColumn].S();
                                        strExcepcion = "El campo 'FECHA' no contiene una fecha, favor de verificar.";
                                        iStatus = 0;
                                    }
                                }
                            }

                            //if (iStatus == 1)
                            //    sValoresValidacion += "|" + sFechaFactura;
                        }

                        #endregion

                        #region VALIDAR CODIGO ARTICULO POR MEDIO DE LA DESCRIPCIÓN DEL ARTÍCULO ¿PRODUCTO? * Verificar
                        //VALIDAR EN SAP -----Verificar
                        if (dRow[dColumn] != null && dColumn.S() == "Producto" || dColumn.S() == "Concepto")
                        {
                            sServicio = dRow[dColumn].S();

                            if (string.IsNullOrEmpty(sServicio))
                            {
                                strCampo = "Archivo " + sArchivo + ".txt - " + dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "Campo vacio.";
                                iStatus = 0;
                            }
                            else
                            {
                                if (eGetCodArticulo != null)
                                    eGetCodArticulo(null, null);

                                if (string.IsNullOrEmpty(sCodigoArticulo))
                                {
                                    strCampo = "Archivo " + sArchivo + ".txt - " + "CODIGO ARTICULO";
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El 'ARTICULO' no se encuentra en base de datos, favor de verificar";
                                    iStatus = 0;
                                }

                            }
                        }
                        #endregion

                        #region VALIDAR IMPORTE
                        if (dRow[dColumn] != null && dColumn.S() == "Importe")
                        {
                            if (!string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strNumeric = dRow[dColumn].S().Replace("$", "").Db().S();
                                styles = NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;

                                if (!Double.TryParse(strNumeric, styles, CultureInfo.InvariantCulture, out dResult))
                                {
                                    strCampo = "Archivo " + sArchivo + ".txt - " + dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'IMPORTE' no tiene el formato correcto, favor de verificar";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = "Archivo " + sArchivo + ".txt - " + dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'IMPORTE' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR MATRICULA
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "Matricula")
                        {
                            sMatricula = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sMatricula))
                            {
                                if (eSearchMatricula != null)
                                    eSearchMatricula(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = "Archivo " + sArchivo + ".txt - " + dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = "Archivo " + sArchivo + ".txt - " + dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'MATRICULA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR BASE
                        sValores = sValoresValidacion.Split('|');

                        if (sValores.Length == 3)
                        {
                            if (!string.IsNullOrEmpty(sBase))
                            {
                                if (string.IsNullOrEmpty(sBase))
                                    iStatus = 0;

                                //if (string.IsNullOrEmpty(sPDF_OMA) && string.IsNullOrEmpty(sXML_OMA))
                                //{
                                if (iStatus == 1)
                                {
                                    if (iExistePDF == 0 && iExisteXML == 0)
                                        sValoresValidacion += ("|" + sBase);

                                    #region VALIDACION DE DOCUMENTOS REQUERIDOS
                                    System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                                    if (dtProv != null && dtProv.Rows.Count > 0)
                                    {
                                        #region VALIDAR PDF
                                        if (iExistePDF == 0)
                                        {
                                            if (iStatus == 1)
                                            {
                                                if (eGetValidaPDF != null)
                                                    eGetValidaPDF(null, EventArgs.Empty);

                                                if (dtProv.Rows[0]["U_REQPDF"].S() == "SI")
                                                {
                                                    if (sPDF_OMA == string.Empty)
                                                        sPDF_OMA = sCadArchivo;

                                                    if (!File.Exists(sCadArchivo))
                                                    {
                                                        strCampo = "Archivo";
                                                        strValor = sCadArchivo;
                                                        strExcepcion = "El archivo PDF no se encontró en la ubicación adecuada, favor de verificar";
                                                        iStatus = 0;
                                                        iFila = 0;
                                                        iExistePDF = 1;
                                                        //iStatus = 1;//Prueba
                                                    }
                                                    else
                                                    {
                                                        dRow["PDF"] = sCadArchivo;
                                                        //sBase = sCadArchivo;
                                                        sPDF_OMA = sCadArchivo;
                                                    }

                                                    if (iStatus == 0)
                                                        dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                                }
                                                dRow["PDF"] = sCadArchivo;
                                                //sBase = sCadArchivo;
                                                sPDF_OMA = sCadArchivo;
                                            }
                                        }

                                        #endregion

                                        #region VALIDAR XML
                                        if (iExisteXML == 0)
                                        {
                                            //if (iStatus == 1)
                                            //{
                                            if (eGetValidaXML != null)
                                                eGetValidaXML(null, EventArgs.Empty);

                                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                                            {
                                                if (sXML_OMA == string.Empty)
                                                    sXML_OMA = sCadArchivo;

                                                if (!File.Exists(sCadArchivo))
                                                {
                                                    strCampo = "Archivo";
                                                    strValor = sCadArchivo;
                                                    strExcepcion = "El archivo XML no se encontró en la ubicación adecuada, favor de verificar";
                                                    iStatus = 0;
                                                    iFila = 0;
                                                    iExisteXML = 1;
                                                    //iStatus = 1;//Prueba
                                                }
                                                else
                                                {
                                                    //sBase = sCadArchivo;
                                                    dRow["XML"] = sCadArchivo;
                                                    sXML_OMA = sCadArchivo;
                                                }

                                                //if (iStatus == 0)
                                                //    dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                            }
                                            //iStatus = 1;
                                            //sBase = sCadArchivo;
                                            dRow["XML"] = sCadArchivo;
                                            sXML_OMA = sCadArchivo;
                                            //}
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                //}
                            }
                        }
                        #endregion

                        #region VALIDAR FECHA OPERACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "Fecha/Hora ini")
                        {
                            //DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            blIsDate = EsFecha(dRow[dColumn].S());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'Fecha/Hora ini' no contiene fecha de operación, favor de verificar";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    strFecha = dRow[dColumn].S().Dt().ToShortDateString();
                                    strFecha = FormatoFecha(strFecha);
                                }
                                else
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'Fecha/Hora ini' no contiene una fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }
                        }

                        #endregion

                        if (iStatus == 0)
                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                    }

                }
                #endregion

                //Llenado de errores
                #region LLENADO DE ERRORES
                if (ViewState["vsDataTable"] != null)
                {
                    System.Data.DataTable dtSinDupl = new System.Data.DataTable();
                    dtSinDupl.Columns.Add("Fila", typeof(string));
                    dtSinDupl.Columns.Add("Campo", typeof(string));
                    dtSinDupl.Columns.Add("Valor", typeof(string));
                    dtSinDupl.Columns.Add("Status", typeof(string));
                    dtSinDupl.Columns.Add("Descripcion", typeof(string));
                    dtSinDupl = (System.Data.DataTable)ViewState["vsDataTable"];

                    if (dtSinDupl != null)
                    {
                        if (dtAllError == null)
                            dtAllError = dtSinDupl.Copy();
                        else
                            dtAllError = dtSinDupl;

                        dtAllError.AcceptChanges();
                    }
                    ban = false;
                }
                else
                {
                    ban = true;
                }
                #endregion

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecorrerDtTXTASA(System.Data.DataTable dtContent, string sFactura)
        {
            try
            {
                sFormato = hdnSeleccionFormato.Value.S();
                new DBUtils().GetLoadInitialValues();
                string sRows = string.Empty;
                string sNoFactura = string.Empty;
                string sFechaOp = string.Empty;
                string sProyecto = string.Empty;
                string sDimension1 = string.Empty;
                string sDimension2 = string.Empty;
                string sDimension3 = string.Empty;
                string sDimension4 = string.Empty;
                string sDimension5 = string.Empty;
                List<Factura> oLsFacturas = new List<Factura>();
                string sMes = string.Empty;
                int iHead = 0;
                string sFechaContable = string.Empty;

                string sXMLChar = string.Empty;
                String[] _sXMLChar;

                string sGetXML = string.Empty;
                string sRutaXML = string.Empty;
                string sTotalCant = string.Empty;
                double dTotalCantidad = 0;
                string sAlmacen = string.Empty;
                string sComentarios = string.Empty;
                sComentarios = txtComentarios.Text;

                System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                sFechaContable = txtFechaContable.Text;

                System.Data.DataTable distinctDT = SelectDistinct(dtContent, "Factura");
                DataRow[] fRows;

                for (int i = 0; i < distinctDT.Rows.Count; i++)
                {
                    Factura oFactura = new Factura();

                    sNoFactura = sFactura;
                    fRows = dtContent.Select("Factura='" + sNoFactura + "'");

                    sGetXML = sNoFactura + ".XML";
                    sRutaXML = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestUrlXML\OTHER\" + sGetXML;

                    //sMatricula = dtContent.Rows[i]["MATRICULA"].S();

                    //if (eSearchMatricula != null)
                    //    eSearchMatricula(null, null);

                    //Validación en SAP
                    //if (dtExiste.Rows[0][0].S().I() == 0)
                    //    sDimension1 = "S19";
                    //else
                    //    sDimension1 = string.Empty;

                    for (int x = 0; x < fRows.Length; x++)
                    {
                        ConceptosFactura oConcepto = new ConceptosFactura();

                        if (x == 0)
                        {
                            // Armar Header
                            oFactura.iId = i + 1;
                            oFactura.sEmpresa = "1";
                            oFactura.sSucursal = "1";
                            oFactura.sProveedor = sCodigoProveedor;
                            oFactura.sNoFactura = sFactura;
                            oFactura.sTipoFactura = "";
                            oFactura.iTimbrar = 0;
                            oFactura.sMetodoPago = "";
                            oFactura.sFormaPago = "";
                            oFactura.sUsoCFDI = "";
                            DateTime dtFecha = DateTime.Now;
                            decimal dTipoCambio = ObtieneTipoCambio("MXN", ref dtFecha, fRows[x]["Fecha Factura"].S());
                            oFactura.dtFecha = fRows[x]["Fecha Factura"].S().Dt();
                            oFactura.dtFechaExp = fRows[x]["Fecha Factura"].S().Dt();
                            oFactura.dtFechaImp = sFechaContable.Dt(); //dtFecha;
                            oFactura.dTipoCambio = dTipoCambio;
                            oFactura.sMsg = "";

                            sMes = obtenerNombreMesNumero(dtFecha.Month.I());

                            oFactura.sComentarios = sComentarios; //sMes + " " + dtFecha.Year.S() + " ASA";
                            oFactura.sSerie = "";
                            oFactura.sMoneda = "MXN";
                            oFactura.dDescuento = 0;

                            ////Valida y Obtiene informaxión de XML
                            if (eGetValidaXML != null)
                                eGetValidaXML(null, EventArgs.Empty);

                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                            {
                                //if (File.Exists(sRutaXML))
                                if (File.Exists(fRows[x]["XML"].S()))
                                {
                                    sXMLChar = ObtenerDatosXML(fRows[x]["XML"].S());
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
                            ////Fin de validación XML
                        }

                        bool bFechaOp = EsFecha(fRows[x]["Fecha/Hora ini"].S());

                        if (bFechaOp == true)
                        {
                            //DateTime FechaOperacion = DateTime.ParseExact(fRows[x]["Fecha/Hora ini"].Dt().ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            sFechaOp = fRows[x]["Fecha/Hora ini"].Dt().ToShortDateString();
                        }
                        else
                            sFechaOp = DateTime.Now.ToShortDateString();

                        //Agregar a Detalle
                        oConcepto.iId = i + 1;
                        oConcepto.sEmpresa = "1";
                        oConcepto.iLinea = x + 1;

                        if (fRows[x]["Matricula"].S() == "KT32078" || fRows[x]["Matricula"].S() == "LA69879" || fRows[x]["Matricula"].S() == "LE16884")
                        {
                            oConcepto.sItem = "GTO20"; //fRows[x]["CODIGO ARTICULO"].S();

                            System.Data.DataTable dtDesArticulo = new DBAccesoSAP().DBGetObtieneDescripcionArticulo("GTO20");

                            if (dtDesArticulo.Rows.Count > 0)
                                sDescripcionArticulo = dtDesArticulo.Rows[0][0].S();
                            else
                                sDescripcionArticulo = string.Empty;

                            System.Data.DataTable dtCodImpuesto = new DBAccesoSAP().DBGetObtieneCodigoImpuestoArticulo("GTO20");

                            if (dtCodImpuesto != null && dtCodImpuesto.Rows.Count > 0)
                                sCodImp = dtCodImpuesto.Rows[0][0].S();
                            else
                                sCodImp = string.Empty;
                        }
                        else
                        {
                            oConcepto.sItem = sCodigoArticulo;

                            if (!string.IsNullOrEmpty(sCodigoArticulo))
                            {
                                System.Data.DataTable dtDesArticulo = new DBAccesoSAP().DBGetObtieneDescripcionArticulo(sCodigoArticulo);

                                if (dtDesArticulo.Rows.Count > 0)
                                    sDescripcionArticulo = dtDesArticulo.Rows[0][0].S();
                                else
                                    sDescripcionArticulo = string.Empty;

                                System.Data.DataTable dtCodImpuesto = new DBAccesoSAP().DBGetObtieneCodigoImpuestoArticulo(sCodigoArticulo);

                                if (dtCodImpuesto != null && dtCodImpuesto.Rows.Count > 0)
                                    sCodImp = dtCodImpuesto.Rows[0][0].S();
                                else
                                    sCodImp = string.Empty;
                            }
                        }

                        if (fRows[x][3].S() == "TURBOSINA" || fRows[x][3].S() == "turbosina")
                        {
                            dTotalCantidad = fRows[x]["M3"].S().Db() * 1000;
                        }
                        else
                            dTotalCantidad = 1;

                        oConcepto.sDescripcionUsuario = sDescripcionArticulo;
                        oConcepto.sCodBarras = "";
                        oConcepto.dCantidad = decimal.Parse(dTotalCantidad.ToString());
                        oConcepto.dPrecio = 0; //fRows[x]["Importe"].S().Replace("$", "").D();
                        oConcepto.sCodigoImpuesto = sCodImp;
                        oConcepto.dDescuento = 0;
                        oConcepto.iImpuesto = 0;
                        oConcepto.dTotalLinea = fRows[x]["Importe"].S().Replace("$", "").D();
                        oConcepto.sCuenta = "";

                        if (fRows[x]["Matricula"].S() == "KT32078")
                            oConcepto.sAlmacen = "P0001";
                        else if (fRows[x]["Matricula"].S() == "LA69879")
                            oConcepto.sAlmacen = "P0002";
                        else if (fRows[x]["Matricula"].S() == "LE16884")
                            oConcepto.sAlmacen = "P0001";
                        else
                        {
                            System.Data.DataTable dtAlmacen = new DBAccesoSAP().DBGetObtieneAlmacen(fRows[x]["Matricula"].S());

                            if (dtAlmacen.Rows.Count > 0)
                                oConcepto.sAlmacen = dtAlmacen.Rows[0]["CCTypeCode"].S();
                            else
                                oConcepto.sAlmacen = "";
                        }

                        System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionMatricula(fRows[x]["Matricula"].S());

                        if (dtDatos.Rows.Count > 0)
                        {
                            sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                            sDimension3 = "TLC"; //dtDatos.Rows[0]["U_Site"].S();
                            sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                        }
                        sDimension1 = string.Empty;
                        sDimension2 = fRows[x]["Matricula"].S();
                        sDimension5 = string.Empty;

                        oConcepto.sProyecto = sProyecto;
                        oConcepto.sDimension1 = sDimension1;
                        oConcepto.sDimension2 = sDimension2;
                        oConcepto.sDimension3 = sDimension3;
                        oConcepto.sDimension4 = sDimension4;
                        oConcepto.sDimension5 = sDimension5;
                        oConcepto.sXML = fRows[x]["XML"].S(); //sXML_OMA;
                        oConcepto.sPDF = fRows[x]["PDF"].S(); //sPDF_OMA;
                        oConcepto.sLugar = fRows[x]["Lugar"].S(); //"TLC";
                        oConcepto.sFechaOperacion = sFechaOp;
                        oFactura.oLstConceptos.Add(oConcepto);
                    }
                    oLsFacturas.Add(oFactura);
                }
                ListaFacturas = oLsFacturas;

                if (eSetProcesaArchivo != null)
                    eSetProcesaArchivo(null, null);


                if (!string.IsNullOrEmpty(sMsgError))
                {
                    msgError.Visible = true;
                    lblError.Text = sMsgError;
                    msgSuccesss.Visible = false;
                    lblSuccess.Text = string.Empty;
                }
                else
                {
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    msgSuccesss.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool ProcesoRenombrarArchivos(string sRutaOrigen)
        {
            try
            {
                string nombreXML = string.Empty;
                string NombrePDF = string.Empty;
                string NombreListado = string.Empty;
                string serie = string.Empty;
                string folio = string.Empty;
                String sRutaDestino = string.Empty;
                bool banPdf = false;
                bool banXml = false;
                bool banTxt = false;

                sRutaDestino = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASA"].S() + @"ArchivosProcesados\";

                if (Directory.Exists(sRutaDestino))
                {
                    foreach (string archivos in Directory.GetFiles(sRutaOrigen))
                    {
                        if (archivos.Substring(archivos.Length - 3).ToUpper() == "XML")
                        {
                            nombreXML = leerXML(archivos);
                            NombrePDF = nombreXML.Substring(0, nombreXML.Length - 3) + "pdf";
                            NombreListado = nombreXML.Substring(0, nombreXML.Length - 3) + "txt";

                            serie = sSerie;
                            folio = sFolio;
                            //*******************************************************************Copia el XML
                            File.Copy(archivos, sRutaDestino + "\\" + nombreXML, true);
                            banXml = true;

                            //*******************************************************************Copia el PDF
                            foreach (string archivoPDF in Directory.GetFiles(sRutaOrigen))
                            {
                                if (archivoPDF.Substring(archivoPDF.Length - 3).ToUpper() == "PDF")
                                {
                                    //string a = archivoPDF.Substring(archivoPDF.Length - (serie.Length + folio.Length + 5)).ToUpper();
                                    //string b = serie.ToUpper() + "_" + folio.ToUpper() + ".PDF";
                                    if (archivoPDF.Substring(archivoPDF.Length - (serie.Length + folio.Length + 5)).ToUpper() == serie.ToUpper() + "_" + folio.ToUpper() + ".PDF")
                                    {
                                        File.Copy(archivoPDF, sRutaDestino + "\\" + NombrePDF, true);
                                        banPdf = true;
                                    }
                                }
                            }
                            //*******************************************************************Copia el Listado Archivos texto
                            foreach (string archivoListado in Directory.GetFiles(sRutaOrigen))
                            {
                                if (archivoListado.Substring(archivoListado.Length - 3).ToUpper() == "TXT")
                                {
                                    string a = archivoListado.Substring(archivoListado.Length - (folio.Length + 4)).ToUpper();
                                    string b = folio.ToUpper() + ".TXT";
                                    if (a == b)
                                    {
                                        File.Copy(archivoListado, sRutaDestino + "\\" + NombreListado, true);
                                        banTxt = true;
                                    }
                                }
                            }

                        }
                    }

                    if (banPdf == true && banXml == true && banTxt == true)
                        return true;
                    else
                        return false;

                    //sMsgError = string.Empty;
                }
                else
                {
                    //msgError.Visible = true;
                    //lblError.Text = "Error: No se encuentra el directorio '" + sRutaDestino + "', favor de verificar.";
                    sMsgError = "No se encuentra el directorio '" + sRutaDestino + "', favor de verificar.";
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
        }
        private string leerXML(string ruta)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(ruta);
            XmlNodeList cfdi = xDoc.GetElementsByTagName("cfdi:Comprobante");
            foreach (XmlElement comprobante in cfdi)
            {
                sSerie = comprobante.GetAttribute("Serie");
                sFolio = comprobante.GetAttribute("Folio");
            }
            return sSerie + sFolio + ".xml";
        }
        public System.Data.DataTable FormatearRemesas(System.Data.DataTable dtOrigen, string sRuta)
        {
            try
            {
                System.Data.DataTable dtDestino = new System.Data.DataTable();
                int col = dtOrigen.Columns.Count;
                sExist = string.Empty;
                string sPath = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASA"].S();

                dtOrigen.Columns.Add("Estatus", typeof(string));

                dtOrigen.Columns[0].ColumnName = "Unidad_Operativa";
                dtOrigen.Columns[1].ColumnName = "Factura";
                dtOrigen.Columns[2].ColumnName = "Fecha_Transaccion";
                dtOrigen.Columns[3].ColumnName = "Fecha_Contable";
                dtOrigen.Columns[4].ColumnName = "Base_Site";

                for (int i = 0; i < dtOrigen.Rows.Count; i++)
                {
                    string sChar = dtOrigen.Rows[i][1].S().Replace("-", "");
                    //string[] sArrBase = dtOrigen.Rows[i][4].S().Split('_');

                    sFacExists = sChar;

                    //if (eGetFacturaExist != null)
                    //    eGetFacturaExist(null, null);

                    //if (sExist == "0")
                    //{
                    string sPDFFind = sPath + @"\ArchivosProcesados" + @"\" + sChar + ".pdf";
                    string sXMLFind = sPath + @"\ArchivosProcesados" + @"\" + sChar + ".xml";
                    string sTXTFind = sPath + @"\ArchivosProcesados" + @"\" + sChar + ".txt";

                    dtOrigen.Rows[i][1] = sChar;
                    dtOrigen.Rows[i][4] = dtOrigen.Rows[i][4].S(); //sArrBase[0].S();

                    if (File.Exists(sPDFFind) && File.Exists(sXMLFind) && File.Exists(sTXTFind))
                    {
                        dtOrigen.Rows[i][5] = "Encontrados";
                    }
                    else
                    {
                        dtOrigen.Rows[i][5] = "No Encontrados";
                    }
                    //}
                    //else
                    //{
                    //    dtOrigen.Rows[i].Delete();
                    //    dtOrigen.AcceptChanges();
                    //}

                }
                dtOrigen.AcceptChanges();

                return dtOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VALIDACIONES FORMATO ASUR

        protected bool ValidacionASUR(System.Data.DataTable dt)
        {
            try
            {
                #region VARIABLES LOCALES
                //List<ErroresValidacion> oLstErrores = new List<ErroresValidacion>();
                //bool ban = false;
                ViewState["vsDataTable"] = null;
                //System.Web.HttpContext.Current.Session["SErrores"] = null;
                msgSuccessAsur.Visible = false;
                lblSuccessAsur.Text = string.Empty;
                msgErrorAsur.Visible = false;
                lblErrorAsur.Text = string.Empty;
                pnlSimulacion.Visible = false;

                //string sFactura = sArchivo;
                string sFechaFactura = string.Empty;
                string sCodPro = string.Empty;
                int iFila = 0;
                //bool blBanExist = false;
                string strCampo = string.Empty;
                string strValor = string.Empty;
                string strExcepcion = string.Empty;
                int iStatus = 0;
                //Variables para validar formato de Fechas
                string[] format = { "yyyyMMdd" };
                //DateTime date;
                string strFecha = string.Empty;
                //Variables para validar cantidad, importe y total
                string strNumeric = string.Empty;
                NumberStyles styles;
                double dResult;
                bool blBanCodPro = false;
                string sDesFactura = string.Empty;

                //sDesFactura = sFactura.Substring(0, 6);

                dt.Select("Matricula Like 'X%'");

                foreach (DataRow row in dt.Rows)
                {
                    string sFormatMat = row["Matricula"].S();

                    if (sFormatMat.IndexOf('X') != -1)
                    {
                        string sFirstPartMat = sFormatMat.Substring(0, 2);
                        string sSecondPartMat = sFormatMat.Substring(2, sFormatMat.Length - 2);
                        row["Matricula"] = sFirstPartMat + '-' + sSecondPartMat;
                    }
                }
                dt.Columns.Add("PDF", typeof(string));
                dt.Columns.Add("XML", typeof(string));

                sBase = ddlBase.SelectedItem.Text;
                #endregion

                foreach (DataRow dRow in dt.Rows)
                {
                    #region VARIABLES LOCALES
                    sEmpresa = string.Empty;
                    sCodigoProveedor = string.Empty;
                    sEmpresa = dRow["Factura"].ToString().Substring(3, 3);
                    string sFac = dRow["Factura"].ToString();

                    if (!string.IsNullOrEmpty(sEmpresa))
                    {
                        if (eGetCodProveedores != null)
                            eGetCodProveedores(null, null);
                    }

                    iFila += 1;
                    sValoresValidacion = string.Empty;
                    int iExistePDF = 0;
                    int iExisteXML = 0;
                    bool blIsDate = false;
                    string[] sValores;

                    if (!string.IsNullOrEmpty(sCodigoProveedor))
                        sCodPro = sCodigoProveedor;
                    #endregion

                    foreach (DataColumn dColumn in dt.Columns)
                    {
                        iStatus = 1;
                        string sCodigoProv = string.Empty;
                        strFecha = string.Empty;
                        strCampo = string.Empty;
                        strValor = string.Empty;
                        strExcepcion = string.Empty;
                        strNumeric = string.Empty;
                        dResult = 0;
                        sMoneda = string.Empty;

                        #region VALIDAR CODIGO PROVEEDOR
                        // VALIDAR SI EXISTE EN SAP
                        //if (blBanCodPro == false)
                        //{
                        if (!string.IsNullOrEmpty(sCodPro))
                        {
                            if (!string.IsNullOrEmpty(sCodPro))
                            {
                                if (eSearchProveedores != null)
                                    eSearchProveedores(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = "CODIGO PROVEEDOR";
                                    strValor = sCodPro;
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = "CODIGO PROVEEDOR";
                                strValor = sCodPro;
                                strExcepcion = "El 'CODIGO PROVEEDOR' está vacio, favor de verificar";
                                iStatus = 0;
                            }

                            if (iStatus == 1)
                            {
                                blBanCodPro = true;
                                if (string.IsNullOrEmpty(sValoresValidacion))
                                    sValoresValidacion = sCodPro;

                                sCodigoProv = sCodPro;
                            }
                        }
                        //}
                        #endregion

                        #region VALIDAR FACTURA
                        if (dRow[dColumn] != null && dColumn.S() == "Factura")
                        {
                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'FACTURA' viene vacio, favor de verificar";
                                iStatus = 0;
                            }

                            sValores = sValoresValidacion.Split('|');

                            if (sValores.Length == 1)
                            {
                                if (iStatus == 1)
                                {
                                    sValoresValidacion += "|" + dRow[dColumn].S();

                                    if (new DBAccesoSAP().DBGetValidaExisteFactura(sCodigoProveedor, dRow[dColumn].S()))
                                    {
                                        dtRow(0, "PROVEEDOR/FACTURA", sCodigoProveedor + "/" + dRow[dColumn].S(), 0, "El numero de factura ya existe con este proveedor, favor de verificar");
                                    }
                                }
                            }
                        }
                        #endregion

                        #region VALIDAR FECHA FACTURACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "Fecha Factura")
                        {
                            sValores = sValoresValidacion.Split('|');

                            if (sValores.Length == 2)
                            {
                                CultureInfo culture = new CultureInfo("en-US");
                                DateTime tempDate = Convert.ToDateTime(dRow[dColumn].S(), culture);
                                //DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                                blIsDate = EsFecha(tempDate.ToShortDateString());
                                //Mensaje("blIsDate:" + blIsDate + "dRow[dColumn].S()=" + dRow[dColumn].S());

                                if (string.IsNullOrEmpty(dRow[dColumn].S()))
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo " + dColumn.S() + " se encuentra vacio.";
                                    iStatus = 0;
                                }
                                else
                                {
                                    if (blIsDate == true)
                                    {
                                        string[] sarrFecha = tempDate.ToShortDateString().Split('/');
                                        string sFechaFac = sarrFecha[2].S() + sarrFecha[1].S() + sarrFecha[0].S();
                                        sFechaFactura = sFechaFac;

                                        //Mensaje("sFechaFactura:" + sFechaFac);

                                        if (iStatus == 1)
                                        {
                                            //sFechaFactura = FormatoFecha(sFechaFactura.Dt().ToShortDateString());
                                            if (!string.IsNullOrEmpty(sFechaFactura))
                                                sValoresValidacion += "|" + sFechaFactura;
                                        }
                                    }
                                    else
                                    {
                                        strCampo = dColumn.S();
                                        strValor = dRow[dColumn].S();
                                        strExcepcion = "El campo 'Fecha Factura' no contiene una fecha, favor de verificar.";
                                        iStatus = 0;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region VALIDAR CODIGO ARTICULO POR MEDIO DE LA DESCRIPCIÓN DEL ARTÍCULO ¿PRODUCTO? * Verificar
                        //VALIDAR EN SAP -----Verificar
                        if (dRow[dColumn] != null && dColumn.S() == "Producto" || dColumn.S() == "Concepto")
                        {
                            sServicio = dRow[dColumn].S();

                            if (string.IsNullOrEmpty(sServicio))
                            {
                                strCampo = "Factura " + sFac + " - " + "CODIGO ARTICULO";
                                strValor = dRow[dColumn].S();
                                strExcepcion = "Campo vacio.";
                                iStatus = 0;
                            }
                            else
                            {
                                if (eGetCodArticulo != null)
                                    eGetCodArticulo(null, null);

                                if (string.IsNullOrEmpty(sCodigoArticulo))
                                {
                                    //strCampo = "CODIGO ARTICULO";
                                    //strValor = sCodigoArticulo;
                                    //strExcepcion = "El dato obtenido 'CODIGO ARTICULO' está vacio, favor de verificar";
                                    //iStatus = 0;

                                    strCampo = "Factura " + sFac + " - " + "CODIGO ARTICULO";
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El 'ARTICULO' no se encuentra en base de datos, favor de verificar";
                                    iStatus = 0;
                                }

                            }
                        }
                        #endregion

                        #region VALIDAR MATRICULA
                        //VALIDAR EN SAP
                        if (dRow[dColumn] != null && dColumn.S() == "Matricula")
                        {
                            sMatricula = dRow[dColumn].S();

                            if (!string.IsNullOrEmpty(sMatricula))
                            {
                                if (eSearchMatricula != null)
                                    eSearchMatricula(null, null);

                                //Validación en SAP
                                if (dtExiste.Rows[0][0].S().I() == 0)
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El Item '" + strValor + "' no existe en SAP.";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'Matricula' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR IMPORTE
                        if (dRow[dColumn] != null && dColumn.S() == "Importe")
                        {
                            if (!string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strNumeric = dRow[dColumn].S().Replace("$", "").Db().S();
                                styles = NumberStyles.Integer | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;

                                if (!Double.TryParse(strNumeric, styles, CultureInfo.InvariantCulture, out dResult))
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "El campo 'Importe' no tiene el formato correcto, favor de verificar";
                                    iStatus = 0;
                                }
                            }
                            else
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "El campo 'Importe' viene vacio, favor de verificar";
                                iStatus = 0;
                            }
                        }
                        #endregion

                        #region VALIDAR FECHA OPERACIÓN
                        if (!string.IsNullOrEmpty(dRow[dColumn].S()) && dColumn.S() == "Llegada")
                        {
                            CultureInfo culture = new CultureInfo("en-US");
                            DateTime tempDate = dRow[dColumn].Dt();
                            
                            //DateTime Fecha = DateTime.ParseExact(dRow[dColumn].S(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            //tempDate = dRow[dColumn].ToString().Dt();

                            //CultureInfo provider = CultureInfo.InvariantCulture;
                            //DateTime tempDate = DateTime.ParseExact(dRow[dColumn].ToString(), "mm/dd/yyyy", provider);

                            blIsDate = EsFecha(tempDate.ToShortDateString());
                            //Mensaje("blIsDate:" + blIsDate + "dRow[dColumn].S()=" + dRow[dColumn].S());

                            if (string.IsNullOrEmpty(dRow[dColumn].S()))
                            {
                                strCampo = dColumn.S();
                                strValor = dRow[dColumn].S();
                                strExcepcion = "La 'Fecha de Operación' no es correcta, favor de verificar";
                                iStatus = 0;
                            }
                            else
                            {
                                if (blIsDate == true)
                                {
                                    //CultureInfo culture = new CultureInfo("en-US");
                                    //DateTime tempDate = Convert.ToDateTime(dRow[dColumn].S(), culture);
                                    string[] sarrFecha = tempDate.ToShortDateString().Split('/');
                                    string sFechaFac = sarrFecha[2].S() + sarrFecha[1].S() + sarrFecha[0].S();
                                    strFecha = sFechaFac;

                                    //Mensaje("strFecha:" + strFecha);
                                }
                                else
                                {
                                    strCampo = dColumn.S();
                                    strValor = dRow[dColumn].S();
                                    strExcepcion = "La 'Fecha de Operación' no contiene una fecha, favor de verificar.";
                                    iStatus = 0;
                                }
                            }
                        }

                        #endregion

                        #region VALIDAR SITE Ó BASE

                        if (!string.IsNullOrEmpty(sValoresValidacion))
                        {
                            sValores = sValoresValidacion.Split('|');

                            if (sValores.Length == 3)
                            {

                                if (!string.IsNullOrEmpty(sBase) && sBase != ".:Seleccione:.")
                                {
                                    if (string.IsNullOrEmpty(sBase))
                                        iStatus = 0;

                                    //if (string.IsNullOrEmpty(sPDF_OMA) && string.IsNullOrEmpty(sXML_OMA))
                                    //{
                                    if (iStatus == 1)
                                    {
                                        if (iExistePDF == 0 && iExisteXML == 0)
                                            sValoresValidacion += ("|" + sBase);

                                        #region VALIDACION DE DOCUMENTOS REQUERIDOS
                                        System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                                        if (dtProv != null && dtProv.Rows.Count > 0)
                                        {
                                            #region VALIDAR PDF

                                            if (iExistePDF == 0)
                                            {
                                                if (iStatus == 1)
                                                {
                                                    if (eGetValidaPDF != null)
                                                        eGetValidaPDF(null, EventArgs.Empty);

                                                    if (dtProv.Rows[0]["U_REQPDF"].S() == "SI")
                                                    {
                                                        if (!File.Exists(sCadArchivo))
                                                        {
                                                            strCampo = "Archivo";
                                                            strValor = sCadArchivo;
                                                            strExcepcion = "El archivo PDF no se encontró en la ubicación adecuada, favor de verificar";
                                                            iStatus = 0;
                                                            iFila = 0;
                                                            iExistePDF = 1;
                                                            //iStatus = 1; //Prueba en servidor
                                                        }
                                                        else
                                                        {
                                                            dRow["PDF"] = sCadArchivo;
                                                            sPDF_OMA = sCadArchivo;
                                                            //iStatus = 1; //Prueba en servidor
                                                        }
                                                        if (iStatus == 0)
                                                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                                    }
                                                    else
                                                    {
                                                        dRow["PDF"] = string.Empty;
                                                        sCadArchivo = string.Empty;
                                                    }
                                                    dRow["PDF"] = sCadArchivo;
                                                    sPDF_OMA = sCadArchivo;
                                                }
                                            }

                                            #endregion

                                            #region VALIDAR XML
                                            if (iExisteXML == 0)
                                            {
                                                //if (iStatus == 1)
                                                //{
                                                if (eGetValidaXML != null)
                                                    eGetValidaXML(null, EventArgs.Empty);

                                                if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                                                {
                                                    if (!File.Exists(sCadArchivo))
                                                    {
                                                        strCampo = "Archivo";
                                                        strValor = sCadArchivo;
                                                        strExcepcion = "El archivo XML no se encontró en la ubicación adecuada, favor de verificar";
                                                        iStatus = 0;
                                                        iFila = 0;
                                                        iExisteXML = 1;
                                                        //iStatus = 1; //Prueba en servidor
                                                    }
                                                    else
                                                    {
                                                        dRow["XML"] = sCadArchivo;
                                                        sXML_OMA = sCadArchivo;
                                                        //iStatus = 1; //Prueba en servidor
                                                    }

                                                    if (iStatus == 0)
                                                        dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                                                }
                                                else
                                                {
                                                    dRow["XML"] = string.Empty;
                                                    sCadArchivo = string.Empty;
                                                }
                                                iStatus = 1;
                                                dRow["XML"] = sCadArchivo;
                                                sXML_OMA = sCadArchivo;
                                                //}
                                            }
                                            #endregion
                                        }

                                        #endregion
                                    }
                                    //}
                                }
                                //sValores = null;
                            }

                        }
                        #endregion

                        if (iStatus == 0)
                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);
                    }
                }

                if ((System.Data.DataTable)ViewState["vsDataTable"] != null)
                    return false;
                else
                    return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsDate(object _value)
        {
            try
            {
                Convert.ToDateTime(_value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnProcesarASUR_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnSeleccionFormato.Value == "5")
                {
                    ProcesarASUR();
                }
            }
            catch (Exception ex)
            {
                //Mensaje("Error: " + ex.Message);
                msgErrorAsur.Visible = true;
                lblErrorAsur.Text = ex.Message;
            }
        }

        protected void ProcesarASUR() 
        {
            try
            {
                if (hdnSeleccionFormato.Value == "5")
                {
                    String sRutaEncontrados = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASUR"].S() + @"ArchivosProcesados";
                    string sArchivo = string.Empty;

                    #region LECTURA DE INFORMACIÓN DEPENDE ARCHIVO DE LECTURA
                    if (Directory.Exists(sRutaEncontrados))
                    {
                        bool bValidacion = false;
                        //ViewState["vsDataTable"] = null;
                        System.Data.DataTable dtDatosASUR = new System.Data.DataTable();
                        System.Data.DataTable dtDataXML = new System.Data.DataTable();
                        System.Data.DataTable dtDataCSV = new System.Data.DataTable();

                        foreach (GridViewRow row in gvEncontrados.Rows)
                        {
                            string sDocTotal = string.Empty;
                            string sFile = gvEncontrados.Rows[row.RowIndex].Cells[2].Text; //Nombre de Archivo
                            string sFactura = gvEncontrados.Rows[row.RowIndex].Cells[1].Text; //Factura
                            string sUrlFileFound = sRutaEncontrados + @"\" + sFile; //Ruta de archivo a leer
                            string sExt = string.Empty;
                            sExt = sFile.Substring(sFile.Length - 4);

                            if (sExt == ".CSV" || sExt == ".csv")
                            {
                                //Obtener Total de Documento
                                sDocTotal = ObtenerTotalesXML(sUrlFileFound.Replace("CSV", "XML").Replace("csv", "xml"));
                                //Lectura de CSV
                                dtDataCSV = ObtenerDatosCSV(sUrlFileFound, sFactura, sDocTotal);
                                dtDatosASUR.Merge(dtDataCSV);

                            }
                            else if (sExt == ".XML" || sExt == ".xml")
                            {
                                //Lectura de XML
                                dtDataXML = GetDataAsurXML(sUrlFileFound);
                                dtDatosASUR.Merge(dtDataXML);
                            }
                        }
                        dtDatosASUR.AcceptChanges();

                        //Validaciones campos de dtDatosASUR
                        if (dtDatosASUR != null)
                        {
                            if (dtDatosASUR.Rows.Count > 0)
                            {
                                bValidacion = CargaErrores(dtDatosASUR);

                                if (hdnSeleccionGral.Value == "2")
                                {
                                    //Recorrido de facturas para insertar a SAP
                                    if (bValidacion == true)
                                    {
                                        RecorrerTXTASUR(dtDatosASUR);

                                        if (iBanCorrecto == 1 && hdnSeleccionGral.Value == "2")
                                        {
                                            msgSuccessAsur.Visible = true;
                                            lblSuccessAsur.Text = "Se ha cargado correctamente el archivo.";
                                            msgErrorAsur.Visible = false;
                                            lblErrorAsur.Text = string.Empty;
                                            pnlSimulacion.Visible = false;
                                            pnlRegASUR.Visible = false;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(sMsgError))
                                            {
                                                msgSuccessAsur.Visible = false;
                                                lblSuccessAsur.Text = string.Empty;
                                                msgErrorAsur.Visible = true;
                                                lblErrorAsur.Text = sMsgError;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool CargaErrores(System.Data.DataTable dtDatosASUR) 
        {
            try
            {
                sMsgError = string.Empty;
                bool valido = false;
                ViewState["vsDataTable"] = null;
                iBanCorrecto = 0;

                valido = ValidacionASUR(dtDatosASUR);

                if (valido == false)
                {
                    if (ViewState["vsDataTable"] != null)
                    {
                        System.Data.DataTable dtSinDupl = new System.Data.DataTable();
                        dtSinDupl.Columns.Add("Fila", typeof(string));
                        dtSinDupl.Columns.Add("Campo", typeof(string));
                        dtSinDupl.Columns.Add("Valor", typeof(string));
                        dtSinDupl.Columns.Add("Status", typeof(string));
                        dtSinDupl.Columns.Add("Descripcion", typeof(string));
                        dtSinDupl = (System.Data.DataTable)ViewState["vsDataTable"];

                        System.Data.DataTable dtContenedor = new System.Data.DataTable();

                        DataView vista = new DataView(dtSinDupl);
                        dtContenedor = vista.ToTable(true, "Fila", "Campo", "Valor", "Status", "Descripcion");

                        gvResultado.DataSource = dtContenedor;
                        gvResultado.DataBind();

                    }

                    if (gvResultado.Rows.Count > 0)
                    {
                        int rowCount = gvResultado.Rows.Count;

                        if (rowCount > 0)
                        {
                            msgErrorAsur.Visible = true;
                            lblErrorAsur.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                            msgSuccessAsur.Visible = false;
                            lblSuccessAsur.Text = string.Empty;
                            pnlRegASUR.Visible = false;
                            pnlSimulacion.Visible = true;
                        }
                    }
                }
                else
                {
                    if (iBanCorrecto == 0 && hdnSeleccionGral.Value == "1")
                    {
                        msgSuccessAsur.Visible = true;
                        lblSuccessAsur.Text = "El archivo cumple los requerimientos.";
                        msgErrorAsur.Visible = false;
                        lblErrorAsur.Text = string.Empty;
                        pnlSimulacion.Visible = false;
                        pnlRegASUR.Visible = true;
                    }
                    valido = true;
                }

                return valido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Metodo leer csv
        protected System.Data.DataTable ObtenerDatosCSV(string sRuta, string sFactura, string sDocTotal)
        {
            try
            {
                int iRows = 0;
                string csvData = File.ReadAllText(sRuta);
                iRows = csvData.Split('\n').S().I();
                String[] sArr;
                String[] sRowCol;
                String[] sArrI;
                String[] sArrI2;
                string sArrIM = string.Empty;
                string sArrIM2 = string.Empty;
                System.Data.DataTable dtSrc = new System.Data.DataTable();
                decimal dDocTotal = 0;

                if (!string.IsNullOrEmpty(sDocTotal))
                    dDocTotal = sDocTotal.D();

                sRowCol = csvData.Split('\r');
                sArr = sRowCol[0].Split(',');

                for (int i = 0; i < sArr.Length; i++)
                {
                    if (dtSrc.Columns.Contains(sArr[i].S()))
                        dtSrc.Columns.Add(sArr[i].S() + "_", typeof(string));
                    else
                        dtSrc.Columns.Add(sArr[i].S(), typeof(string));
                }
                dtSrc.Columns.Add("DocTotal", typeof(string));

                for (int i = 1; i < sRowCol.Length; i++)
                {
                    sArrIM = sRowCol[i].Replace("\n", "").Replace(", S.A.", " S.A.").Replace(",,,", ",-,-,").Replace(" ", "").Replace("  ", "").Replace("   ", "").Replace("    ", "").Replace("     ", "").Replace(@"""/", @"""""""").Replace(@"""", @"""""""");
                    sArrIM2 = sRowCol[i].Replace("\n", "").Replace(", S.A.", " S.A.").Replace(",,,", ",-,-,").Replace("  ", "").Replace("   ", "").Replace("    ", "").Replace("     ", "").Replace(@"""/", @"""""""").Replace(@"""", @""""""""); 

                    for (int n = 0; n < 9; n++)
                    {
                        string sOri = string.Empty;
                        string sRep = string.Empty;
                        sOri = @"""""""" + n.S() + ","; // n.S() + ",";
                        sRep = n.S();

                        sArrIM = sArrIM.Replace(sOri, sRep);
                        sArrIM2 = sArrIM2.Replace(sOri, sRep);
                    }

                    sArrI = sArrIM.Split(',');
                    sArrI2 = sArrIM2.Split(',');

                    if (!string.IsNullOrEmpty(sArrIM) && !string.IsNullOrEmpty(sArrIM2))
                    {
                        int row = i - 1;
                        dtSrc.Rows.Add();
                        dtSrc.Rows[row]["Numero de Cliente"] = sArrI[0].S().Trim();
                        dtSrc.Rows[row]["Cliente"] = sArrI2[1].S().Trim().Replace("\"", ""); //sArrI[1].S().Trim().Replace("\"", "");
                        dtSrc.Rows[row]["Concepto"] = sArrI2[2].S().Trim().Replace("\"", "");
                        dtSrc.Rows[row]["Factura"] = sFactura; //sArrI[3].S().Trim();//.Replace("00","").Replace("000","").Replace("0000","").Replace("00000","").Replace("000000","");
                        dtSrc.Rows[row]["Fecha Factura"] = sArrI[4].S().Trim();
                        dtSrc.Rows[row]["Folio"] = sArrI[5].S().Trim();
                        dtSrc.Rows[row]["Matricula"] = sArrI[6].S().Trim();
                        dtSrc.Rows[row]["Avion"] = sArrI[7].S().Trim();
                        dtSrc.Rows[row]["Tipo Vuelo"] = sArrI[8].S().Trim();
                        dtSrc.Rows[row]["Tarifa"] = sArrI[9].S().Trim().Replace("\"", "");
                        dtSrc.Rows[row]["Cantidad"] = "1"; //sArrI[10].S().Trim();
                        dtSrc.Rows[row]["Unidad"] = sArrI[11].S().Trim();
                        dtSrc.Rows[row]["Vuelo Llegada"] = sArrI[12].S().Trim();
                        dtSrc.Rows[row]["Origen"] = sArrI[13].S().Trim();
                        dtSrc.Rows[row]["Llegada"] = sArrI[14].S().Trim();
                        dtSrc.Rows[row]["Vuelo Salida"] = sArrI[15].S().Trim();
                        dtSrc.Rows[row]["Destino"] = sArrI[16].S().Trim();
                        dtSrc.Rows[row]["Salida"] = sArrI[17].S().Trim();
                        dtSrc.Rows[row]["Tarifa_"] = ""; //sArrI[18].S().Trim();
                        dtSrc.Rows[row]["Horas Serv."] = sArrI[19].S().Trim();
                        dtSrc.Rows[row]["Horas Cob."] = sArrI[20].S().Trim();
                        dtSrc.Rows[row]["Importe"] = sArrI[21].S().Trim().Replace("\"", ""); //Verificar
                        dtSrc.Rows[row]["Descuento"] = "0"; //sArrI[22].S().Trim();
                        dtSrc.Rows[row]["Impuesto"] = sArrI[23].S().Trim().Replace("\"", "");
                        dtSrc.Rows[row]["Total"] = sArrI[24].S().Trim().Replace("\"", "");
                        dtSrc.Rows[row]["Horario"] = sArrI[25].S().Trim();
                        dtSrc.Rows[row]["DocTotal"] = dDocTotal.S();
                    }
                }
                dtSrc.AcceptChanges();
                return dtSrc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool ValidaASUR(System.Data.DataTable dtDatosASUR)
        {
            try
            {
                #region VARIABLES LOCALES
                List<ErroresValidacion> oLstErrores = new List<ErroresValidacion>();
                bool ban = false;
                //ViewState["vsDataTable"] = null;
                System.Web.HttpContext.Current.Session["SErrores"] = null;
                msgSuccessAsur.Visible = false;
                lblSuccessAsur.Text = string.Empty;
                msgErrorAsur.Visible = false;
                lblErrorAsur.Text = string.Empty;
                pnlSimulacion.Visible = false;

                //string sFactura = sArchivo;
                string sFechaFactura = string.Empty;
                string sCodPro = string.Empty;
                int iFila = 0;
                bool blBanExist = false;
                string strCampo = string.Empty;
                string strValor = string.Empty;
                string strExcepcion = string.Empty;
                int iStatus = 0;
                //Variables para validar formato de Fechas
                string[] format = { "yyyyMMdd" };
                DateTime date;
                string strFecha = string.Empty;
                //Variables para validar cantidad, importe y total
                string strNumeric = string.Empty;
                NumberStyles styles;
                double dResult;
                bool blBanCodPro = false;
                string sDesFactura = string.Empty;

                //sDesFactura = sFactura.Substring(0, 6);

                dtDatosASUR.Select("Matricula Like 'X%'");

                foreach (DataRow row in dtDatosASUR.Rows)
                {
                    string sFormatMat = row["Matricula"].S();

                    if (sFormatMat.IndexOf('X') != -1)
                    {
                        string sFirstPartMat = sFormatMat.Substring(0, 2);
                        string sSecondPartMat = sFormatMat.Substring(2, sFormatMat.Length - 2);
                        row["Matricula"] = sFirstPartMat + '-' + sSecondPartMat;
                    }
                }
                dtDatosASUR.Columns.Add("PDF", typeof(string));
                dtDatosASUR.Columns.Add("XML", typeof(string));

                sBase = ddlBase.SelectedItem.Text;
                #endregion

                #region VALIDACIONES
                foreach (DataRow dRow in dtDatosASUR.Rows)
                {
                    #region VARIABLES LOCALES
                    sEmpresa = string.Empty;
                    sCodigoProveedor = string.Empty;
                    sEmpresa = dRow["Factura"].ToString().Substring(3, 3);
                    string sFac = dRow["Factura"].ToString();

                    if (!string.IsNullOrEmpty(sEmpresa))
                    {
                        if (eGetCodProveedores != null)
                            eGetCodProveedores(null, null);
                    }

                    iFila += 1;
                    sValoresValidacion = string.Empty;
                    int iExistePDF = 0;
                    int iExisteXML = 0;
                    bool blIsDate = false;
                    string[] sValores;

                    if (!string.IsNullOrEmpty(sCodigoProveedor))
                        sCodPro = sCodigoProveedor;
                    #endregion

                    foreach (DataColumn dColumn in dtDatosASUR.Columns)
                    {
                        #region VARIABLES
                        string sCodigoProv = string.Empty;
                        strFecha = string.Empty;
                        strCampo = string.Empty;
                        strValor = string.Empty;
                        strExcepcion = string.Empty;
                        strNumeric = string.Empty;
                        dResult = 0;
                        iStatus = 1;

                        #endregion

                        

                        

                        if (iStatus == 0)
                        {
                            dtRow(iFila, strCampo, strValor, iStatus, strExcepcion);

                            //ErroresValidacion oErrores = new ErroresValidacion();
                            //oErrores.sFila = iFila.S();
                            //oErrores.sCampo = strCampo;
                            //oErrores.sValor = strValor;
                            //if (iStatus == 0)
                            //    oErrores.sStatus = "Error";
                            //else
                            //    oErrores.sStatus = "Válido";
                            //oErrores.sDescripcion = strExcepcion;
                            //oLstErrores.Add(oErrores);
                            //Response.Write("<script>alert('Campos:" + oErrores.sFila + "," + oErrores.sCampo + "," + oErrores.sValor + "');</script>");
                        }
                            
                    }

                }
                //ListaErrores = oLstErrores;
                #endregion

                #region LLENADO DE ERRORES

                //gvResultado.DataSource = (System.Data.DataTable)ViewState["vsDataTable"];
                //gvResultado.DataBind();

                //if (gvResultado.Rows.Count > 0)
                //{
                //    int rowCount = gvResultado.Rows.Count;

                //    if (rowCount > 0)
                //    {
                //        msgErrorAsur.Visible = true;
                //        lblErrorAsur.Text = "El archivo contiene algunos errores que impiden su procesamiento.";
                //        msgSuccessAsur.Visible = false;
                //        lblSuccessAsur.Text = string.Empty;
                //        pnlRegASUR.Visible = false;
                //        pnlSimulacion.Visible = true;
                //    }
                //}

                //System.Data.DataTable dt = ConvertToDatatable(ListaErrores);

                //if (!string.IsNullOrEmpty(Convert.ToString(ViewState["vsDataTable"])))
                ////if (dt != null && dt.Rows.Count > 0)
                //{
                //    Response.Write("<script>alert('Paso despues de comprobar dt');</script>");
                //    System.Data.DataTable dtSinDupl = new System.Data.DataTable();
                //    dtSinDupl.Columns.Add("Fila", typeof(string));
                //    dtSinDupl.Columns.Add("Campo", typeof(string));
                //    dtSinDupl.Columns.Add("Valor", typeof(string));
                //    dtSinDupl.Columns.Add("Status", typeof(string));
                //    dtSinDupl.Columns.Add("Descripcion", typeof(string));

                //    Response.Write("<script>alert('Asignando dt a dtSinDupl');</script>");

                //    dtSinDupl = (System.Data.DataTable)ViewState["vsDataTable"];
                //    //dtSinDupl = dt;

                //    Response.Write("<script>alert('Count=" + dtSinDupl.Rows.Count.S() + "');</script>");


                //    if (dtSinDupl != null)
                //    {
                //        if (dtAllError == null)
                //            dtAllError = dtSinDupl.Copy();
                //        else
                //            dtAllError = dtSinDupl;

                //        dtAllError.AcceptChanges();
                //    }
                //    ban = false;
                //}
                //else
                //{
                //    Response.Write("<script>alert('Lista vacia');</script>");
                //    ban = true;
                //}
                #endregion

                //if (ViewState["vsDataTable"] != null || !string.IsNullOrEmpty(Convert.ToString(ViewState["vsDataTable"])))
                //    ban = false;
                if (System.Web.HttpContext.Current.Session["SErrores"] != null)
                    ban = false;
                else
                    ban = true;

                return ban;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable ConvertToDatatable(List<ErroresValidacion> lstErrores) 
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Fila", typeof(string));
                dt.Columns.Add("Campo", typeof(string));
                dt.Columns.Add("Valor", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Descripcion", typeof(string));

                for (int i = 0; i < lstErrores.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Fila"] = lstErrores[i].sFila;
                    dr["Campo"] = lstErrores[i].sCampo;
                    dr["Valor"] = lstErrores[i].sValor;
                    dr["Status"] = lstErrores[i].sStatus;
                    dr["Descripcion"] = lstErrores[i].sDescripcion;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();

                return dt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        protected void RecorrerTXTASUR(System.Data.DataTable dtContent)
        {
            try
            {
                sFormato = hdnSeleccionFormato.Value.S();
                new DBUtils().GetLoadInitialValues();
                string sRows = string.Empty;
                string sNoFactura = string.Empty;
                string sFechaOp = string.Empty;
                string sFechaContable = string.Empty;
                string sLugar = string.Empty;

                List<Factura> oLsFacturas = new List<Factura>();
                string sProyecto = string.Empty;
                string sDimension1 = string.Empty;
                string sDimension2 = string.Empty;
                string sDimension3 = string.Empty;
                string sDimension4 = string.Empty;
                string sDimension5 = string.Empty;
                string sMes = string.Empty;
                int iHead = 0;

                string sXMLChar = string.Empty;
                String[] _sXMLChar;

                string sGetXML = string.Empty;
                string sRutaXML = string.Empty;

                sBase = ddlBase.SelectedItem.Text;
                sFechaContable = txtFechaContable.Text;
                //string sFechaC = string.Empty;

                bool bFechaOpe = false;
                string sFechaOperacion = string.Empty;
                string sComentarios = string.Empty;

                CultureInfo culture = new CultureInfo("en-US");

                System.Data.DataTable distinctDT = SelectDistinct(dtContent, "Factura");
                DataRow[] fRows;

                for (int i = 0; i < distinctDT.Rows.Count; i++)
                {
                    Factura oFactura = new Factura();
                    sEmpresa = string.Empty;
                    sCodigoProveedor = string.Empty;
                    sCodigoArticulo = string.Empty;
                    sMatricula = string.Empty;
                    sNoFactura = distinctDT.Rows[i][0].S();
                    fRows = dtContent.Select("Factura='" + sNoFactura + "'");

                    sGetXML = sNoFactura + ".XML";
                    sRutaXML = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestUrlXML\ASUR\" + sGetXML;

                    sMatricula = dtContent.Rows[i]["Matricula"].S();
                    sLugar = sNoFactura.Substring(0, 6).Substring(3, 3);

                    sEmpresa = sNoFactura.ToString().Substring(3, 3);
                    if (!string.IsNullOrEmpty(sEmpresa))
                    {
                        if (eGetCodProveedores != null)
                            eGetCodProveedores(null, null);
                    }

                    if (eSearchMatricula != null)
                        eSearchMatricula(null, null);

                    //Validación en SAP
                    if (dtContent.Rows[0][0].S().I() == 0)
                        sDimension1 = sMatricula;
                    else
                    {
                        sDimension1 = string.Empty; //Area ó Departamento
                        sDimension2 = sMatricula;   //Matrícula
                    }

                    for (int x = 0; x < fRows.Length; x++)
                    {
                        ConceptosFactura oConcepto = new ConceptosFactura();
                        sServicio = string.Empty;

                        if (x == 0)
                        {
                            //if (i > 0)
                            //    iHead = i - 1;
                            // Armar Header
                            oFactura.iId = i + 1;
                            oFactura.sEmpresa = "1";
                            oFactura.sSucursal = "1";
                            oFactura.sProveedor = sCodigoProveedor; //fRows[iHead]["CODIGO PROVEEDOR"].S();
                            oFactura.sNoFactura = fRows[x]["Factura"].S();
                            oFactura.sTipoFactura = "";
                            oFactura.iTimbrar = 0;
                            oFactura.sMetodoPago = "";
                            oFactura.sFormaPago = "";
                            oFactura.sUsoCFDI = "";

                            DateTime dtFecha = DateTime.Now;
                            DateTime tempDateFactura = fRows[x]["Fecha Factura"].Dt();
                            DateTime tempDateContable = sFechaContable.Dt();

                            //DateTime tmpFechaFactura = Convert.ToDateTime(fRows[x]["Fecha Factura"].S(), culture);
                            //DateTime tmpFechaContable = Convert.ToDateTime(sFechaContable, culture);

                            decimal dTipoCambio = ObtieneTipoCambio("MXN", ref dtFecha, tempDateFactura.ToString("dd/MM/yyyy"));
                            oFactura.dtFecha = tempDateFactura;
                            oFactura.dtFechaExp = tempDateFactura;
                            oFactura.dtFechaImp = tempDateContable; //sFechaContable.Dt();
                            oFactura.dTipoCambio = dTipoCambio;
                            oFactura.sMsg = "";

                            if (string.IsNullOrEmpty(txtComentarios.Text))
                            {
                                sMes = obtenerNombreMesNumero(dtFecha.Month.I());
                                sComentarios = sMes + " " + dtFecha.Year.S() + " ASUR";
                            }
                            else
                                sComentarios = txtComentarios.Text;

                            oFactura.sComentarios = sComentarios;
                            oFactura.sSerie = "";
                            oFactura.sMoneda = "MXN";

                            decimal dbDocTotal = 0;
                            if (!string.IsNullOrEmpty(fRows[x]["DocTotal"].S()))
                            {
                                dbDocTotal = fRows[x]["DocTotal"].S().D();
                            }
                            oFactura.dDescuento = dbDocTotal;

                            ////Valida y Obtiene informaxión de XML
                            if (eGetValidaXML != null)
                                eGetValidaXML(null, EventArgs.Empty);

                            System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCodigoProveedor);

                            if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                            {
                                //if (File.Exists(sCadArchivo))
                                if (File.Exists(fRows[x]["XML"].S()))
                                {
                                    sXMLChar = ObtenerDatosXML(fRows[x]["XML"].S());
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
                            ////Fin de validación XML
                        }

                        //Agregado 03 de Mayo 2019
                        CultureInfo cultureOther = new CultureInfo("en-US");
                        //DateTime tempDateOpe = fRows[x]["Llegada"].Dt();

                        string[] sArrFec;
                        string sFechaFormat = string.Empty;

                        if (fRows[x]["Llegada"].ToString().Contains("-") || fRows[x]["Llegada"].ToString().Contains("/"))
                        {
                            sArrFec = fRows[x]["Llegada"].ToString().Split('-');

                            if (sArrFec.Length > 2)
                            {
                                sFechaFormat = sArrFec[0] + "-" + sArrFec[1] + "-" + sArrFec[2];
                            }
                            else
                            {
                                sFechaFormat = sArrFec[0].Replace("/","-");
                            }
                        }

                        DateTime tempDateOpe = sFechaFormat.Dt(); //fRows[x]["Llegada"].ToString().Dt();
                        //string[] sarrFechaOpe = tempDateOpe.ToShortDateString().Split('/');
                        //string sAFechaOpe = sarrFechaOpe[2].S() + sarrFechaOpe[1].S() + sarrFechaOpe[0].S();

                        //DateTime tmpFechaOperacion = Convert.ToDateTime(fRows[x]["Llegada"].S(), culture);

                        bFechaOpe = EsFecha(tempDateOpe.ToShortDateString()); //Verificar fecha de operación

                        if (bFechaOpe == true)
                            sFechaOperacion = tempDateOpe.ToShortDateString(); //tmpFechaOperacion.ToShortDateString(); //Verificar fecha de operación
                        else
                            sFechaOperacion = DateTime.Now.ToShortDateString();

                        //sTotalLinea = sTotalL(fRows[x]["Cantidad"].S(), fRows[x]["Importe"].S().Replace("$", ""));
                        sTotalLinea = fRows[x]["Importe"].S().Replace("$", "");

                        //Agregar a Detalle
                        oConcepto.iId = i + 1;
                        oConcepto.sEmpresa = "1";
                        oConcepto.iLinea = x + 1;

                        //Obtener codigo articulo
                        sServicio = fRows[x]["Concepto"].S();

                        if (eGetCodArticulo != null)
                            eGetCodArticulo(null, null);

                        if (!string.IsNullOrEmpty(sCodigoArticulo))
                            oConcepto.sItem = sCodigoArticulo; //fRows[x]["CODIGO ARTICULO"].S();
                        else
                            oConcepto.sItem = string.Empty;



                        if (!string.IsNullOrEmpty(sCodigoArticulo))
                        {
                            System.Data.DataTable dtDesArticulo = new DBAccesoSAP().DBGetObtieneDescripcionArticulo(sCodigoArticulo);

                            if (dtDesArticulo.Rows.Count > 0)
                                sDescripcionArticulo = dtDesArticulo.Rows[0][0].S();
                            else
                                sDescripcionArticulo = string.Empty;

                            System.Data.DataTable dtCodImpuesto = new DBAccesoSAP().DBGetObtieneCodigoImpuestoArticulo(sCodigoArticulo);

                            if (dtCodImpuesto != null && dtCodImpuesto.Rows.Count > 0)
                                sCodImp = dtCodImpuesto.Rows[0][0].S();
                            else
                                sCodImp = string.Empty;

                        }

                        //double dImporte = fRows[x]["Importe"].S().Replace("$", "").Db();
                        //double dIvaDiv = fRows[x]["Impuesto"].S().Replace("$", "").Db();
                        //double dtotal = dImporte / dIvaDiv;

                        oConcepto.sDescripcionUsuario = sDescripcionArticulo;
                        oConcepto.sCodBarras = "";
                        oConcepto.dCantidad = fRows[x]["Cantidad"].S().D();

                        if (!string.IsNullOrEmpty(fRows[x]["Tarifa_"].S()))
                            oConcepto.dPrecio = fRows[x]["Tarifa_"].S().Replace("$", "").D();
                        else
                            oConcepto.dPrecio = 0;

                        oConcepto.sCodigoImpuesto = sCodImp;
                        oConcepto.dDescuento = 0;

                        //if (!string.IsNullOrEmpty(fRows[x]["Impuesto"].S()))
                        //    oConcepto.iImpuesto = fRows[x]["Impuesto"].S().I();
                        //else
                        oConcepto.iImpuesto = 0;


                        oConcepto.dTotalLinea = sTotalLinea.D(); //fRows[x]["Importe"].S().D();
                        oConcepto.sCuenta = "";
                        oConcepto.sAlmacen = "";

                        System.Data.DataTable dtDatos = new DBAccesoSAP().DBGetObtieneInformacionMatricula(fRows[x]["Matricula"].S());

                        if (dtDatos.Rows.Count > 0)
                        {
                            sProyecto = string.Empty;
                            sDimension3 = string.Empty;
                            sDimension4 = string.Empty;

                            sProyecto = dtDatos.Rows[0]["U_Unidad_Negocio"].S();
                            sDimension3 = dtDatos.Rows[0]["U_Site"].S();
                            sDimension4 = dtDatos.Rows[0]["U_CodFin"].S();
                        }
                        sDimension1 = string.Empty;
                        sDimension2 = fRows[x]["Matricula"].S();
                        sDimension5 = string.Empty;

                        oConcepto.sProyecto = sProyecto;
                        oConcepto.sDimension1 = sDimension1;
                        oConcepto.sDimension2 = sDimension2;
                        oConcepto.sDimension3 = sDimension3;
                        oConcepto.sDimension4 = sDimension4;
                        oConcepto.sDimension5 = sDimension5;
                        oConcepto.sXML = fRows[x]["XML"].S(); //sXML_OMA;
                        oConcepto.sPDF = fRows[x]["PDF"].S(); //sPDF_OMA;
                        oConcepto.sLugar = sLugar;
                        oConcepto.sFechaOperacion = sFechaOperacion;
                        oFactura.oLstConceptos.Add(oConcepto);
                    }
                    oLsFacturas.Add(oFactura);
                }
                ListaFacturas = oLsFacturas;

                if (eSetProcesaArchivo != null)
                    eSetProcesaArchivo(null, null);

                if (dtError != null)
                {
                    if (dtError.Rows.Count > 0)
                    {
                        string sCadError = string.Empty;
                        sCadError = "<ul>";

                        for (int i = 0; i < dtError.Rows.Count; i++)
                        {
                            sCadError += "<li>" + dtError.Rows[i]["DescripcionError"].S() + "</li>";
                        }
                        sCadError += "</ul>";
                        msgError.Visible = true;
                        lblError.Text = sCadError;
                        msgSuccesss.Visible = false;
                        lblSuccess.Text = string.Empty;
                    }
                }
                else
                {
                    msgError.Visible = false;
                    lblError.Text = string.Empty;
                    msgSuccesss.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected System.Data.DataTable GetDataAsurXML(string sArchivoXML)
        {
            try
            {
                //System.Data.DataTable dtDatosASUR = new System.Data.DataTable();
                System.Data.DataTable dtDataXML = new System.Data.DataTable();

                if (hdnSeleccionFormato.Value == "5")
                {
                    string sRutaXML = string.Empty;
                    //String sRepositorioFacturas = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASUR"].S() + @"FACTURAS";
                    sRutaXML = sArchivoXML;

                    if (File.Exists(sRutaXML))
                    {
                        if (sRutaXML.Substring(sRutaXML.Length - 3).ToUpper() == "XML")
                        {
                            #region Crea DataTable para almacenar facturas ASUR
                            dtDataXML = leerXMLASUR(sRutaXML);
                            #endregion
                        }
                        dtDataXML.AcceptChanges();
                    }

                }
                return dtDataXML;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private System.Data.DataTable leerXMLASUR(string ruta)
        {

            System.Data.DataTable dtSrc = new System.Data.DataTable();
            dtSrc.Columns.Add("Numero de Cliente", typeof(string));
            dtSrc.Columns.Add("Cliente", typeof(string));
            dtSrc.Columns.Add("Concepto", typeof(string));
            dtSrc.Columns.Add("Factura", typeof(string));
            dtSrc.Columns.Add("Fecha Factura", typeof(string));
            dtSrc.Columns.Add("Folio", typeof(string));
            dtSrc.Columns.Add("Matricula", typeof(string));
            dtSrc.Columns.Add("Avion", typeof(string));
            dtSrc.Columns.Add("Tipo Vuelo", typeof(string));
            dtSrc.Columns.Add("Tarifa", typeof(string));
            dtSrc.Columns.Add("Cantidad", typeof(string));
            dtSrc.Columns.Add("Unidad", typeof(string));
            dtSrc.Columns.Add("Vuelo Llegada", typeof(string));
            dtSrc.Columns.Add("Origen", typeof(string));
            dtSrc.Columns.Add("Llegada", typeof(string));
            dtSrc.Columns.Add("Vuelo Salida", typeof(string));
            dtSrc.Columns.Add("Destino", typeof(string));
            dtSrc.Columns.Add("Salida", typeof(string));
            dtSrc.Columns.Add("Tarifa_", typeof(string));
            dtSrc.Columns.Add("Horas Serv.", typeof(string));
            dtSrc.Columns.Add("Horas Cob.", typeof(string));
            dtSrc.Columns.Add("Importe", typeof(string));
            dtSrc.Columns.Add("Descuento", typeof(string));
            dtSrc.Columns.Add("Impuesto", typeof(string));
            dtSrc.Columns.Add("Total", typeof(string));
            dtSrc.Columns.Add("Horario", typeof(string));
            dtSrc.Columns.Add("DocTotal", typeof(string));

            XmlDocument xDoc = new XmlDocument();
            DataSet ds = new DataSet();

            xDoc.Load(ruta);
            ds.ReadXml(ruta);

            for (int i = 0; i < ds.Tables["Concepto"].Rows.Count; i++)
            {
                dtSrc.Rows.Add();
                dtSrc.Rows[i]["Numero de Cliente"] = ds.Tables["DatosGenerales"].Rows[0]["numeroCliente"].S();
                dtSrc.Rows[i]["Cliente"] = ds.Tables["Emisor"].Rows[0]["Nombre"].S().Replace(", S.A.", " S.A.").Replace("\"", "").Replace(",,,", ",-,-,").Replace(@"""", @"");
                dtSrc.Rows[i]["Concepto"] = ds.Tables["Concepto"].Rows[i]["Descripcion"].S();
                dtSrc.Rows[i]["Factura"] = ds.Tables["Comprobante"].Rows[0]["Serie"].S() + ds.Tables["Comprobante"].Rows[0]["Folio"].S();
                dtSrc.Rows[i]["Fecha Factura"] = ds.Tables["Comprobante"].Rows[0]["Fecha"].S().Substring(0, 10);

                string sMatricula = string.Empty;
                string[] sCadenaObs = null;
                string sFechaOperacion = string.Empty;
                string sConcatFechaO = string.Empty;
                string sAvion = string.Empty;
                string sFolio = string.Empty;
                string sOrigen = string.Empty;

                sCadenaObs = ds.Tables["DatosGenerales"].Rows[0]["observacionSecundaria"].S().Split(' ');

                if (!string.IsNullOrEmpty(ds.Tables["AddendaDomicilioReceptor"].Rows[0]["localidad"].S()))
                {
                    if (ds.Tables["AddendaDomicilioReceptor"].Rows[0]["localidad"].S().Contains("TOLUCA"))
                        sOrigen = "TLC";
                    else if (ds.Tables["AddendaDomicilioReceptor"].Rows[0]["localidad"].S().Contains("MONTERREY"))
                        sOrigen = "MTY";
                }

                if (sCadenaObs.Length == 21)
                {
                    if (sCadenaObs[0].Contains("Matricula:"))
                        sMatricula = sCadenaObs[1].S();

                    if (sCadenaObs[3].Contains("Aeronave"))
                        sAvion = sCadenaObs[4].S();

                    sConcatFechaO = sCadenaObs[9].S() + " " + sCadenaObs[10].S() + " " + sCadenaObs[11].S() + " " + sCadenaObs[12].S();

                    if (sConcatFechaO.Contains("Fecha - Hora Operacion:"))
                        sFechaOperacion = sCadenaObs[13].S();

                    if (sCadenaObs[19].Contains("Folio"))
                        sFolio = sCadenaObs[20].S().Replace(".", "");
                }


                dtSrc.Rows[i]["Folio"] = sFolio;//Verificar
                dtSrc.Rows[i]["Matricula"] = sMatricula;
                dtSrc.Rows[i]["Avion"] = sAvion;
                dtSrc.Rows[i]["Tipo Vuelo"] = "-";
                dtSrc.Rows[i]["Tarifa"] = "-";
                dtSrc.Rows[i]["Cantidad"] = ds.Tables["Concepto"].Rows[i]["Cantidad"].S(); //Verificar
                dtSrc.Rows[i]["Unidad"] = ds.Tables["Concepto"].Rows[i]["Unidad"].S(); //Verificar
                dtSrc.Rows[i]["Vuelo Llegada"] = "-";
                dtSrc.Rows[i]["Origen"] = sOrigen;
                dtSrc.Rows[i]["Llegada"] = sFechaOperacion;
                dtSrc.Rows[i]["Vuelo Salida"] = "-";
                dtSrc.Rows[i]["Destino"] = "-";
                dtSrc.Rows[i]["Salida"] = "-";
                dtSrc.Rows[i]["Tarifa_"] = "0";//Verificar
                dtSrc.Rows[i]["Horas Serv."] = "-";
                dtSrc.Rows[i]["Horas Cob."] = "-";
                dtSrc.Rows[i]["Importe"] = ds.Tables["Concepto"].Rows[i]["Importe"].S();

                if (ds.Tables["Comprobante"].Columns.Contains("Total"))
                    dtSrc.Rows[i]["DocTotal"] = ds.Tables["Comprobante"].Rows[0]["Total"].Db().S();
                else
                    dtSrc.Rows[i]["DocTotal"] = "0";

                dtSrc.Rows[i]["Descuento"] = 0;
                dtSrc.Rows[i]["Impuesto"] = ds.Tables["Traslado"].Rows[i]["TasaOCuota"].S().Db() * ds.Tables["Concepto"].Rows[i]["Importe"].S().Db();
                dtSrc.Rows[i]["Total"] = "-";
                dtSrc.Rows[i]["Horario"] = "-";
            }
            dtSrc.AcceptChanges();

            return dtSrc;
        }

        protected string ObtenerTotalesXML(string sPath) 
        {
            try
            {
                string sDocTotal = string.Empty;

                XmlDocument xDoc = new XmlDocument();
                DataSet ds = new DataSet();

                xDoc.Load(sPath);
                ds.ReadXml(sPath);

                if (ds.Tables["Comprobante"].Columns.Contains("Total"))
                    sDocTotal = ds.Tables["Comprobante"].Rows[0]["Total"].Db().S();
                else
                    sDocTotal = "0";

                return sDocTotal;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        protected void btnExplorarRepo_Click(object sender, EventArgs e)
        {
            try
            {
                #region Layout ASUR
                if (hdnSeleccionFormato.Value == "5")
                {
                    if (ddlBase.SelectedValue != "0")
                    {
                        if (!string.IsNullOrEmpty(txtFechaContable.Text))
                        {
                            pnlSimulacion.Visible = false;
                            msgErrorAsur.Visible = false;
                            lblErrorAsur.Text = string.Empty;
                            msgSuccessAsur.Visible = false;
                            lblSuccessAsur.Text = string.Empty;

                            ExplorarRepositorio();

                            pnlRegASUR.Visible = true;

                            if (hdnSeleccionGral.Value == "1" && hdnSeleccionFormato.Value == "5")
                            {
                                lblTituloRegAsur.Text = "Simulación de ASUR";
                            }
                            else if (hdnSeleccionGral.Value == "2" && hdnSeleccionFormato.Value == "5")
                            {
                                lblTituloRegAsur.Text = "Carga Masiva de ASUR";
                            }


                            //dtFacturas = ObtenerFacturasRepo();

                            //if (dtFacturas != null && dtFacturas.Rows.Count > 0)
                            //{
                            //    pnlRegASUR.Visible = true;
                            //    gvFacturasASUR.DataSource = dtFacturas;
                            //    gvFacturasASUR.DataBind();

                            //}

                            //Validación una vez se haga la insercin a SAP
                            //if (iBanCorrecto == 1)
                            //{
                            //    msgSuccessAsur.Visible = true;
                            //    lblSuccessAsur.Text = "Se ha cargado correctamente el archivo.";
                            //    msgErrorAsur.Visible = false;
                            //    lblErrorAsur.Text = string.Empty;
                            //}
                            //else
                            //{
                            //    if (!string.IsNullOrEmpty(sMsgError))
                            //    {
                            //        msgSuccessAsur.Visible = false;
                            //        lblSuccessAsur.Text = string.Empty;
                            //        msgErrorAsur.Visible = true;
                            //        lblErrorAsur.Text = sMsgError;
                            //    }
                            //}
                        }
                        else
                        {
                            msgSuccessAsur.Visible = false;
                            lblSuccessAsur.Text = string.Empty;
                            msgErrorAsur.Visible = true;
                            lblErrorAsur.Text = "Se requiere ingresar la Fecha de Contabilización para procesar el archivo";
                        }
                    }
                    else
                    {
                        msgSuccessAsur.Visible = false;
                        lblSuccessAsur.Text = string.Empty;
                        msgErrorAsur.Visible = true;
                        lblErrorAsur.Text = "Se requiere seleccionar el campo BASE para procesar el archivo";
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                lblErrorAsur.Text = ex.Message;
                msgErrorAsur.Visible = true;
            }
        }
        protected void ExplorarRepositorio()
        {
            try
            {
                System.Data.DataTable dtRemesa = new System.Data.DataTable();
                System.Data.DataTable dtExplorerData = new System.Data.DataTable();
                bool bCopyFiles = false;
                string sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASUR"].S() + @"Archivos";

                dtExplorerData = ObtenerFacturasRepo(sRuta);

                bCopyFiles = ProcesoRenombrarArchivosASUR(sRuta);

                if (bCopyFiles == true)
                {
                    //Procesar Archivo de remesas
                    //cmdExcel.CommandText = "SELECT [CLAVE AEROPUERTO],[FACTURA] FROM [" + SheetName + "]";
                    //oda.SelectCommand = cmdExcel;
                    //oda.Fill(dtRemesa);
                    //connExcel.Close();

                    dtRemesa = FormatearRemesasASUR(dtExplorerData, sRuta);

                    //Envio facturas al método de lectura correspondiente
                    if (dtRemesa != null & dtRemesa.Rows.Count > 0)
                    {
                        System.Data.DataTable dtEncontrados = new System.Data.DataTable();
                        System.Data.DataTable dtNoEncontrados = new System.Data.DataTable();
                        DataRow[] rowEncontrados;
                        DataRow[] rowNoEncontrados;

                        dtEncontrados.Columns.Add("Clave_Aeropuerto", typeof(string));
                        dtEncontrados.Columns.Add("Factura", typeof(string));
                        dtEncontrados.Columns.Add("Archivo_Lectura", typeof(string));
                        dtEncontrados.Columns.Add("Status", typeof(string));

                        dtNoEncontrados.Columns.Add("Clave_Aeropuerto", typeof(string));
                        dtNoEncontrados.Columns.Add("Factura", typeof(string));
                        dtNoEncontrados.Columns.Add("Archivo_Lectura", typeof(string));
                        dtNoEncontrados.Columns.Add("Status", typeof(string));

                        rowEncontrados = dtRemesa.Select("Status = 'Encontrados'");
                        rowNoEncontrados = dtRemesa.Select("Status = 'No Encontrados'");

                        foreach (DataRow rowP in rowEncontrados)
                            dtEncontrados.ImportRow(rowP);
                        dtEncontrados.AcceptChanges();

                        foreach (DataRow rowNP in rowNoEncontrados)
                            dtNoEncontrados.ImportRow(rowNP);
                        dtNoEncontrados.AcceptChanges();

                        gvEncontrados.DataSource = dtEncontrados;
                        gvEncontrados.DataBind();

                        gvNoEncontrados.DataSource = dtNoEncontrados;
                        gvNoEncontrados.DataBind();

                        if (dtEncontrados.Rows.Count <= 0)
                            btnProcesarASUR.Visible = false;
                        else
                            btnProcesarASUR.Visible = true;


                        if (hdnSeleccionGral.Value == "1" && dtEncontrados != null && dtEncontrados.Rows.Count > 0)
                        {
                            btnProcesarASUR.Text = "Validar Facturas";
                            btnProcesarASUR.ToolTip = "Validar Facturas";
                        }
                        else if (hdnSeleccionGral.Value == "2" && dtEncontrados != null && dtEncontrados.Rows.Count > 0)
                        {
                            btnProcesarASUR.Text = "Procesar Facturas";
                            btnProcesarASUR.ToolTip = "Procesar Facturas";
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //La utilizamos para extraer las facturas en el repositorio y mostrarlas
        protected System.Data.DataTable ObtenerFacturasRepo(string sURLRepositorio)
        {
            try
            {
                #region GenerarRemsesa

                System.Data.DataTable dtRemesas = new System.Data.DataTable();
                dtRemesas.Columns.Add("CLAVE_AEROPUERTO", typeof(string));
                dtRemesas.Columns.Add("FACTURA", typeof(string));

                if (Directory.Exists(sURLRepositorio))
                {
                    int i = 0, x = 0;

                    foreach (string archivos in Directory.GetFiles(sURLRepositorio))
                    {
                        if (archivos.Substring(archivos.Length - 3).ToUpper() == "XML")
                        {
                            XmlDocument xDoc = new XmlDocument();
                            DataSet ds = new DataSet();
                            FileInfo info = new FileInfo(archivos);
                            string sNameFile = info.Name;

                            xDoc.Load(archivos);
                            ds.ReadXml(archivos);

                            if (i == 0)
                            {
                                dtRemesas.Rows.Add();
                                dtRemesas.Rows[i]["CLAVE_AEROPUERTO"] = ds.Tables["Comprobante"].Rows[0]["Serie"].S().Substring(ds.Tables["Comprobante"].Rows[0]["Serie"].S().Length - 3);
                                dtRemesas.Rows[i]["FACTURA"] = ds.Tables["Comprobante"].Rows[0]["Serie"].S() + ds.Tables["Comprobante"].Rows[0]["Folio"].S();
                                x += 1;
                                i += 1;
                            }
                            else if (x > 0)
                            {
                                dtRemesas.Rows.Add();
                                dtRemesas.Rows[x]["CLAVE_AEROPUERTO"] = ds.Tables["Comprobante"].Rows[0]["Serie"].S().Substring(ds.Tables["Comprobante"].Rows[0]["Serie"].S().Length - 3);
                                dtRemesas.Rows[x]["FACTURA"] = ds.Tables["Comprobante"].Rows[0]["Serie"].S() + ds.Tables["Comprobante"].Rows[0]["Folio"].S();
                                x += 1;
                            }
                        }
                    }
                }
                #endregion

                #region Obtiene facturas

                //if (hdnSeleccionFormato.Value == "5")
                //{
                //    System.Data.DataTable dtFacturas = new System.Data.DataTable();
                //    string sPathRepositorio = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASUR"].S() + @"Archivos";
                //    dtFacturas.Columns.Add("NoFactura", typeof(string));
                //    dtFacturas.Columns.Add("NombreCliente", typeof(string));
                //    dtFacturas.Columns.Add("FechaFactura", typeof(string));
                //    dtFacturas.Columns.Add("Archivo", typeof(string));

                //    if (Directory.Exists(sPathRepositorio))
                //    {
                //        int i = 0, x = 0;

                //        foreach (string archivos in Directory.GetFiles(sPathRepositorio))
                //        {
                //            if (archivos.Substring(archivos.Length - 3).ToUpper() == "XML")
                //            {
                //                XmlDocument xDoc = new XmlDocument();
                //                DataSet ds = new DataSet();
                //                FileInfo info = new FileInfo(archivos);
                //                string sNameFile = info.Name;

                //                xDoc.Load(archivos);
                //                ds.ReadXml(archivos);

                //                if (i == 0) 
                //                {
                //                    dtFacturas.Rows.Add();
                //                    dtFacturas.Rows[i]["NoFactura"] = ds.Tables["Comprobante"].Rows[0]["Serie"].S() + ds.Tables["Comprobante"].Rows[0]["Folio"].S();
                //                    dtFacturas.Rows[i]["NombreCliente"] = ds.Tables["Emisor"].Rows[0]["Nombre"].S();
                //                    dtFacturas.Rows[i]["FechaFactura"] = ds.Tables["Comprobante"].Rows[0]["Fecha"].S().Substring(0, 10);
                //                    dtFacturas.Rows[i]["Archivo"] = sNameFile;
                //                    x += 1;
                //                    i += 1;
                //                }
                //                else if (x > 0)
                //                {
                //                    dtFacturas.Rows.Add();
                //                    dtFacturas.Rows[x]["NoFactura"] = ds.Tables["Comprobante"].Rows[0]["Serie"].S() + ds.Tables["Comprobante"].Rows[0]["Folio"].S();
                //                    dtFacturas.Rows[x]["NombreCliente"] = ds.Tables["Emisor"].Rows[0]["Nombre"].S();
                //                    dtFacturas.Rows[x]["FechaFactura"] = ds.Tables["Comprobante"].Rows[0]["Fecha"].S().Substring(0, 10);
                //                    dtFacturas.Rows[x]["Archivo"] = sNameFile;
                //                    x += 1;
                //                }
                //            }
                //        }
                //        dtFacturas.AcceptChanges();
                //    }
                //}
                #endregion

                return dtRemesas;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Restructuración de lectura ASUR
        protected bool ProcesoRenombrarArchivosASUR(string sRutaOrigen)
        {
            try
            {
                string nombreXML = string.Empty;
                string NombrePDF = string.Empty;
                string NombreListado = string.Empty;
                string serie = string.Empty;
                string folio = string.Empty;
                String sRutaDestino = string.Empty;
                bool banPdf = false;
                bool banXml = false;
                bool banCsv = false;

                sRutaDestino = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASUR"].S() + @"ArchivosProcesados\";

                if (Directory.Exists(sRutaDestino))
                {
                    foreach (string archivos in Directory.GetFiles(sRutaOrigen))
                    {
                        if (archivos.Substring(archivos.Length - 3).ToUpper() == "XML")
                        {
                            nombreXML = leerXML(archivos);
                            NombrePDF = nombreXML.Substring(0, nombreXML.Length - 3) + "pdf";
                            NombreListado = nombreXML.Substring(0, nombreXML.Length - 3) + "csv";

                            serie = sSerie;
                            folio = sFolio;
                            //*******************************************************************Copia el XML
                            File.Copy(archivos, sRutaDestino + "\\" + nombreXML, true);
                            banXml = true;

                            //*******************************************************************Copia el PDF
                            foreach (string archivoPDF in Directory.GetFiles(sRutaOrigen))
                            {
                                if (archivoPDF.Substring(archivoPDF.Length - 3).ToUpper() == "PDF")
                                {
                                    //string a = archivoPDF.Substring(archivoPDF.Length - (serie.Length + folio.Length + 5)).ToUpper();
                                    //string b = serie.ToUpper() + "_" + folio.ToUpper() + ".PDF";
                                    if (archivoPDF.Substring(archivoPDF.Length - (serie.Length + folio.Length + 5)).ToUpper().Replace("_", "") == serie.ToUpper() + "_" + folio.ToUpper() + ".PDF")
                                    {
                                        File.Copy(archivoPDF, sRutaDestino + "\\" + NombrePDF, true);
                                        banPdf = true;
                                    }
                                    else if (archivoPDF.Substring(archivoPDF.Length - (serie.Length + folio.Length + 4)).ToUpper().Replace("_", "") == serie.ToUpper() + folio.ToUpper() + ".PDF")
                                    {
                                        File.Copy(archivoPDF, sRutaDestino + "\\" + NombrePDF, true);
                                        banPdf = true;
                                    }
                                    else if (archivoPDF.Substring(archivoPDF.Length - (serie.Length + folio.Length + 3)).ToUpper().Replace("_", "") == serie.ToUpper() + folio.ToUpper() + ".PDF")
                                    {
                                        File.Copy(archivoPDF, sRutaDestino + "\\" + NombrePDF, true);
                                        banPdf = true;
                                    }

                                }
                            }
                            //*******************************************************************Copia el Listado Archivos texto
                            foreach (string archivoListado in Directory.GetFiles(sRutaOrigen))
                            {
                                if (archivoListado.Substring(archivoListado.Length - 3).ToUpper() == "CSV")
                                {
                                    string a = archivoListado.Substring(archivoListado.Length - (folio.Length + 4)).ToUpper();
                                    string b = folio.ToUpper() + ".CSV";
                                    if (a == b)
                                    {
                                        File.Copy(archivoListado, sRutaDestino + "\\" + NombreListado, true);
                                        banCsv = true;
                                    }
                                    else if (archivoListado.Substring(archivoListado.Length - (folio.Length + 3)).ToUpper() == folio.ToUpper() + ".CSV")
                                    {
                                        File.Copy(archivoListado, sRutaDestino + "\\" + NombreListado, true);
                                        banCsv = true;
                                    }
                                }
                            }

                        }
                    }

                    if (banPdf == true && banXml == true && banCsv == true)
                        return true;
                    else
                        return false;

                    //sMsgError = string.Empty;
                }
                else
                {
                    //msgError.Visible = true;
                    //lblError.Text = "Error: No se encuentra el directorio '" + sRutaDestino + "', favor de verificar.";
                    sMsgError = "No se encuentra el directorio '" + sRutaDestino + "', favor de verificar.";
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
        }
        public System.Data.DataTable FormatearRemesasASUR(System.Data.DataTable dtOrigen, string sRuta)
        {
            try
            {
                System.Data.DataTable dtDestino = new System.Data.DataTable();
                int col = dtOrigen.Columns.Count;
                sExist = string.Empty;
                string sPath = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_ASUR"].S();

                dtOrigen.Columns.Add("Archivo_Lectura", typeof(string));
                dtOrigen.Columns.Add("Status", typeof(string));

                dtOrigen.Columns[0].ColumnName = "Clave_Aeropuerto";
                dtOrigen.Columns[1].ColumnName = "Factura";

                for (int i = 0; i < dtOrigen.Rows.Count; i++)
                {
                    string sNFactura = dtOrigen.Rows[i][1].S().Replace("-", "");
                    //sFacExists = sChar;
                    sAeroExists = dtOrigen.Rows[i][0].S();

                    if (!string.IsNullOrEmpty(sAeroExists))
                    {
                        if (eGetAeropuertoExist != null)
                            eGetAeropuertoExist(null, null);

                        if (sExist == "1")
                            dtOrigen.Rows[i][2] = sNFactura + ".XML";
                        else if (sExist == "0")
                            dtOrigen.Rows[i][2] = sNFactura + ".CSV";
                    }

                    string sPDFFind = sPath + @"\ArchivosProcesados" + @"\" + sNFactura + ".pdf";
                    string sXMLFind = sPath + @"\ArchivosProcesados" + @"\" + sNFactura + ".xml";
                    string sCSVFind = sPath + @"\ArchivosProcesados" + @"\" + sNFactura + ".csv";

                    if (File.Exists(sPDFFind) && File.Exists(sXMLFind) && File.Exists(sCSVFind))
                    {
                        dtOrigen.Rows[i][3] = "Encontrados";
                    }
                    else
                    {
                        dtOrigen.Rows[i][3] = "No Encontrados";
                    }

                }
                dtOrigen.AcceptChanges();

                return dtOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

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
        public void LoadExistencia(System.Data.DataTable dtRes)
        {
            try
            {
                dtExiste = null;
                dtExiste = dtRes;
            }
            catch (Exception)
            {
                throw;
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
        private bool ColumnEqual(object A, object B)
        {
            // Compares two values to see if they are equal. Also compares DBNULL.Value.           
            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is BNull.Value
                return false;
            return (A.Equals(B));  // value type standard comparison
        }
        public System.Data.DataTable SelectDistinct(System.Data.DataTable SourceTable, string FieldName)
        {
            // Create a Datatable â€“ datatype same as FieldName
            System.Data.DataTable dt = new System.Data.DataTable(SourceTable.TableName);
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
        public void Mensaje(string sMensaje)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
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
        public bool ValidarArchivo(System.Data.DataTable dsFileExcel, string sLayout)
        {
            try
            {
                int iValidColumn;
                bool bValidExcel = false;
                string[] arrColumn1 = { "CODIGO PROVEEDOR", "MONEDA", "CODIGO ARTICULO", "FACTURA", "FECHA DOC (añomesdia)", "FECHA CONTA SBO (añomesdia)", "FECHA OPERACIÓN", "MATRICULA/DEPARTAMENTO", "CANTIDAD", "PRECIO UNIDAD", "TOTAL", "CODIGO IMPUESTO" };
                string[] arrColumn2 = { "FOLIO", "SERVICIO", "MATRICULA", "PERIODO INICIAL", "IMPORTE TOTAL" };
                string[] arrColumn3 = { "FECHA", "FACTURA", "IMPORTE", "MATRICULA", "LUGAR", "FECHA_OPERACION", "ARTICULO" };
                string[] arrColumn4 = { "Factura" };
                string[] arrColumn5 = { "Concepto", "Factura", "Fecha Factura", "Matricula", "Llegada", "Importe" };

                switch (sLayout)
                {
                    // Layout genérico
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
                    // OMA
                    case "2":
                        ViewState["VSLayout"] = arrColumn2;
                        for (int i = 0; i < arrColumn2.Length; i++)
                        {
                            iValidColumn = dsFileExcel.Columns.IndexOf(arrColumn2[i]);

                            if (iValidColumn == -1)
                            {
                                bValidExcel = false;
                                break;
                            }
                            else
                                bValidExcel = true;
                        }
                        break;
                    // AMAIT
                    case "3":
                        ViewState["VSLayout"] = arrColumn3;
                        for (int i = 0; i < arrColumn3.Length; i++)
                        {
                            if (dsFileExcel.Rows.Count > 0)
                            {
                                iValidColumn = dsFileExcel.Columns.IndexOf(arrColumn3[i]);

                                if (iValidColumn == -1)
                                {
                                    bValidExcel = false;
                                    break;
                                }
                                else
                                    bValidExcel = true;
                            }
                            else
                            {
                                bValidExcel = false;
                                break;
                            }
                        }
                        break;
                    // ASA
                    case "4":
                        ViewState["VSLayout"] = arrColumn4;
                        for (int i = 0; i < arrColumn4.Length; i++)
                        {
                            iValidColumn = dsFileExcel.Columns.IndexOf(arrColumn4[i]);

                            if (iValidColumn == -1)
                            {
                                bValidExcel = false;
                                break;
                            }
                            else
                                bValidExcel = true;
                        }
                        break;
                    // ASUR
                    case "5":
                        ViewState["VSLayout"] = arrColumn5;
                        for (int i = 0; i < arrColumn5.Length; i++)
                        {
                            iValidColumn = dsFileExcel.Columns.IndexOf(arrColumn5[i]);

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
        public void LoadCodigoProveedor(System.Data.DataTable dtRes)
        {
            try
            {
                dtCodPro = null;
                dtCodPro = dtRes;

                if (dtCodPro != null)
                    if (dtCodPro.Rows.Count > 0)
                        sCodigoProveedor = dtCodPro.Rows[0][0].S();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadCodigoArticulo(System.Data.DataTable dtRes)
        {
            try
            {
                dtCodArt = null;
                dtCodArt = dtRes;

                if (dtCodArt != null)
                    if (dtCodArt.Rows.Count > 0)
                        sCodigoArticulo = dtCodArt.Rows[0][0].S();
                    else
                        sCodigoArticulo = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadIVA(System.Data.DataTable dtRes)
        {
            try
            {
                dtIVA = null;
                dtIVA = dtRes;

                if (dtIVA != null)
                    if (dtIVA.Rows.Count > 0)
                        sIVA = dtIVA.Rows[0][0].S();
                    else
                        sIVA = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadDescripcionArticulo(System.Data.DataTable dtRes)
        {
            try
            {
                dtDesArticulo = null;
                dtDesArticulo = dtRes;

                if (dtDesArticulo != null)
                    if (dtDesArticulo.Rows.Count > 0)
                        sDescripcionArticulo = dtDesArticulo.Rows[0][0].S();
                    else
                        sDescripcionArticulo = string.Empty;
            }
            catch (Exception)
            {
                throw;
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
        public bool EsFechaAMAIT(string sFecha)
        {
            try
            {
                DateTime sFec = DateTime.ParseExact(sFecha, "dd.MM.yyyy", new CultureInfo("en-US"));
                bool iBan = false;

                if (DateTime.TryParseExact(sFecha, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                    iBan = true;
                else if (DateTime.TryParseExact(sFecha, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out sFec))
                    iBan = true;
                else if (DateTime.TryParseExact(sFecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                    iBan = true;
                else if (DateTime.TryParseExact(sFecha, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                    iBan = true;
                else if (DateTime.TryParseExact(sFecha, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                    iBan = true;
                else if (DateTime.TryParseExact(sFecha, "MMddyyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                    iBan = true;
                else if (DateTime.TryParseExact(sFecha, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out sFec))
                    iBan = true;

                return iBan;
            }
            catch
            {
                return false;
            }
        }
        public static Boolean EsFecha(String sFecha)
        {
            try
            {
                DateTime.Parse(sFecha);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string obtenerNombreMesNumero(int numeroMes)
        {
            try
            {
                DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                string nombreMes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formatoFecha.GetMonthName(numeroMes));
                return nombreMes;
            }
            catch
            {
                return "Desconocido";
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        #endregion

        #region PROPIEDADES Y VARIABLES

        //string sXML_OMA = string.Empty;
        //string sPDF_OMA = string.Empty;

        private const string sClase = "frmCargaMasiva.aspx.cs";
        private const string sPagina = "frmCargaMasiva.aspx";

        CargaMasiva_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchProveedores;
        public event EventHandler eSearchMoneda;
        public event EventHandler eSearchAlmacen;
        public event EventHandler eSearchMatricula;
        public event EventHandler eSearchCodigoImpuesto;
        public event EventHandler eSearchLugar;
        public event EventHandler eSetProcesaArchivo;
        public event EventHandler eGetValidaXML;
        public event EventHandler eGetValidaPDF;
        public event EventHandler eGetCodProveedores;
        public event EventHandler eGetCodArticulo;
        public event EventHandler eGetIVAImpuesto;
        public event EventHandler eSearchAreaDpto;
        public event EventHandler eGetDesArticulo;

        public event EventHandler eGetFacturaExist;
        public event EventHandler eGetAeropuertoExist;

        public string sFormato
        {
            get { return (string)ViewState["VSFormato"]; }
            set { ViewState["VSFormato"] = value; }
        }
        public string sCodigoProveedor
        {
            get { return (string)ViewState["VSCodigoProveedor"]; }
            set { ViewState["VSCodigoProveedor"] = value; }
        }

        public string sMoneda
        {
            get { return (string)ViewState["VSMoneda"]; }
            set { ViewState["VSMoneda"] = value; }
        }

        public string sAlmacen
        {
            get { return (string)ViewState["VSAlmacen"]; }
            set { ViewState["VSAlmacen"] = value; }
        }

        public string sMatricula
        {
            get { return (string)ViewState["VSMatricula"]; }
            set { ViewState["VSMatricula"] = value; }
        }

        public string sCodigoImpuesto
        {
            get { return (string)ViewState["VSCodigoImpuesto"]; }
            set { ViewState["VSCodigoImpuesto"] = value; }
        }

        public string sLugar
        {
            get { return (string)ViewState["VSLugar"]; }
            set { ViewState["VSLugar"] = value; }
        }

        public string sIVA
        {
            get { return (string)ViewState["VSsIVA"]; }
            set { ViewState["VSsIVA"] = value; }
        }

        public string sCantidad
        {
            get { return (string)ViewState["VSCantidad"]; }
            set { ViewState["VSCantidad"] = value; }
        }

        public string sTotalLinea
        {
            get { return (string)ViewState["VSTotalLinea"]; }
            set { ViewState["VSTotalLinea"] = value; }
        }

        public System.Data.DataTable dtExiste
        {
            get { return (System.Data.DataTable)ViewState["VSdtExiste"]; }
            set { ViewState["VSdtExiste"] = value; }
        }

        public System.Data.DataTable dtExcel
        {
            get { return (System.Data.DataTable)ViewState["VSdtExcel"]; }
            set { ViewState["VSdtExcel"] = value; }
        }

        public System.Data.DataTable dtIVA
        {
            get { return (System.Data.DataTable)ViewState["VSIVA"]; }
            set { ViewState["VSIVA"] = value; }
        }

        public List<Factura> ListaFacturas
        {
            set { ViewState["VSListaFacturas"] = value; }
            get { return (List<Factura>)ViewState["VSListaFacturas"]; }
        }

        //Lista de errores
        public List<ErroresValidacion> ListaErrores
        {
            set { ViewState["VSListaErrores"] = value; }
            get { return (List<ErroresValidacion>)ViewState["VSListaErrores"]; }
        }

        public string sValoresValidacion
        {
            get { return (string)ViewState["VSValoresValidacion"]; }
            set { ViewState["VSValoresValidacion"] = value; }
        }
        public string sCadArchivo
        {
            get { return (string)ViewState["VSValidaArchivo"]; }
            set { ViewState["VSValidaArchivo"] = value; }
        }

        public System.Data.DataTable dtDesArticulo
        {
            get { return (System.Data.DataTable)ViewState["VSDesArticulo"]; }
            set { ViewState["VSDesArticulo"] = value; }
        }
        public string sDescripcionArticulo
        {
            get { return (string)ViewState["VSDesArticulo"]; }
            set { ViewState["VSDesArticulo"] = value; }
        }

        public string sCodImp
        {
            get { return (string)ViewState["VSCodImp"]; }
            set { ViewState["VSCodImp"] = value; }
        }

        //Formato OMA
        public System.Data.DataTable dtHeaderRows
        {
            get { return (System.Data.DataTable)ViewState["VSHeaderRows"]; }
            set { ViewState["VSHeaderRows"] = value; }
        }
        public System.Data.DataTable dtDatosOMA
        {
            get { return (System.Data.DataTable)ViewState["VSDatosOMA"]; }
            set { ViewState["VSDatosOMA"] = value; }
        }

        public System.Data.DataTable dtCodPro
        {
            get { return (System.Data.DataTable)ViewState["VSdtCodPro"]; }
            set { ViewState["VSdtCodPro"] = value; }
        }

        public string sEmpresa
        {
            get { return (string)ViewState["VSEmpresa"]; }
            set { ViewState["VSEmpresa"] = value; }
        }


        public string sFactura
        {
            get { return (string)ViewState["VSFactura"]; }
            set { ViewState["VSFactura"] = value; }
        }

        public string sFechaFactura
        {
            get { return (string)ViewState["VSFechaFactura"]; }
            set { ViewState["VSFechaFactura"] = value; }
        }

        public string sCodigoArticulo
        {
            get { return (string)ViewState["VSCodigoArticulo"]; }
            set { ViewState["VSCodigoArticulo"] = value; }
        }

        public System.Data.DataTable dtCodArt
        {
            get { return (System.Data.DataTable)ViewState["VSCodArt"]; }
            set { ViewState["VSCodArt"] = value; }
        }

        public string sServicio
        {
            get { return (string)ViewState["VSServicio"]; }
            set { ViewState["VSServicio"] = value; }
        }

        public string sBase
        {
            get { return (string)ViewState["VSBase"]; }
            set { ViewState["VSBase"] = value; }
        }

        public string sXML_OMA
        {
            get { return (string)ViewState["VSXML_OMA"]; }
            set { ViewState["VSXML_OMA"] = value; }
        }

        public string sPDF_OMA
        {
            get { return (string)ViewState["VSPDF_OMA"]; }
            set { ViewState["VSPDF_OMA"] = value; }
        }

        public string sMsgError
        {
            get { return (string)ViewState["VSMsgError"]; }
            set { ViewState["VSMsgError"] = value; }
        }
        public System.Data.DataTable dtError
        {
            get { return (System.Data.DataTable)ViewState["VSdtError"]; }
            set { ViewState["VSdtError"] = value; }
        }

        //Bandera para saber si se insertó correctamente
        public int iBanCorrecto
        {
            get { return (int)ViewState["VSCorrecto"]; }
            set { ViewState["VSCorrecto"] = value; }
        }

        // Formato AMAIT
        public string sDesArticulo
        {
            get { return (string)ViewState["VSDesArticulo"]; }
            set { ViewState["VSDesArticulo"] = value; }
        }
        public System.Data.DataTable dtUnionAMAIT
        {
            get { return (System.Data.DataTable)ViewState["VSUnionAMAIT"]; }
            set { ViewState["VSUnionAMAIT"] = value; }
        }

        //Formato ASA
        public System.Data.DataTable dtDatosFac
        {
            get { return (System.Data.DataTable)ViewState["VSDatosFac"]; }
            set { ViewState["VSDatosFac"] = value; }
        }
        public System.Data.DataTable dtErroresValidacion
        {
            get { return (System.Data.DataTable)ViewState["VSErroresValidacion"]; }
            set { ViewState["VSErroresValidacion"] = value; }
        }
        public System.Data.DataTable dtAllError
        {
            get { return (System.Data.DataTable)ViewState["VSAllError"]; }
            set { ViewState["VSAllError"] = value; }
        }
        public string sSerie
        {
            get { return (string)ViewState["VSSerie"]; }
            set { ViewState["VSSerie"] = value; }
        }
        public string sFolio
        {
            get { return (string)ViewState["VSFolio"]; }
            set { ViewState["VSFolio"] = value; }
        }
        public string sExist
        {
            get { return (string)ViewState["VSExiste"]; }
            set { ViewState["VSExiste"] = value; }
        }

        public string sFacExists
        {
            get { return (string)ViewState["VSFacExiste"]; }
            set { ViewState["VSFacExiste"] = value; }
        }
        //Formato ASUR
        public System.Data.DataTable dtDatosASUR
        {
            get { return (System.Data.DataTable)ViewState["VSDatosASUR"]; }
            set { ViewState["VSDatosASUR"] = value; }
        }

        public System.Data.DataTable dtDataXML
        {
            get { return (System.Data.DataTable)ViewState["VSDataXML"]; }
            set { ViewState["VSDataXML"] = value; }
        }

        public System.Data.DataTable dtFacturas
        {
            get { return (System.Data.DataTable)ViewState["VSFacturas"]; }
            set { ViewState["VSFacturas"] = value; }
        }
        public System.Data.DataTable dtContentFacturas
        {
            get { return (System.Data.DataTable)ViewState["VSContentFacturas"]; }
            set { ViewState["VSContentFacturas"] = value; }
        }
        public System.Data.DataTable dtMainErrores
        {
            get { return (System.Data.DataTable)ViewState["VSMainErrores"]; }
            set { ViewState["VSMainErrores"] = value; }
        }

        public string sFila
        {
            get { return (string)ViewState["VSFila"]; }
            set { ViewState["VSFila"] = value; }
        }
        public string sCampo
        {
            get { return (string)ViewState["VSCampo"]; }
            set { ViewState["VSCampo"] = value; }
        }
        public string sValor
        {
            get { return (string)ViewState["VSValor"]; }
            set { ViewState["VSValor"] = value; }
        }
        public string sStatus
        {
            get { return (string)ViewState["VSStatus"]; }
            set { ViewState["VSStatus"] = value; }
        }
        public string sDescripcion
        {
            get { return (string)ViewState["VSDescripcion"]; }
            set { ViewState["VSDescripcion"] = value; }
        }

        //Validacion de simulacion a carga masiva
        public string sArchivoSimulado
        {
            get { return (string)ViewState["VSArchivoSimulado"]; }
            set { ViewState["VSArchivoSimulado"] = value; }
        }

        public bool bValidacion
        {
            get { return false; }
            set { ViewState["VSValidacion"] = value; }
        }

        public string sAeroExists
        {
            get { return (string)ViewState["VSAeroExists"]; }
            set { ViewState["VSAeroExists"] = value; }
        }

        #endregion

        protected void btnProcesarCargaSimulada_Click(object sender, EventArgs e)
        {
            try
            {
                iBanCorrecto = 0;

                if (!string.IsNullOrEmpty(sArchivoSimulado))
                {
                    if (dtExcel != null)
                    {
                        RecorrerDtExcel(dtExcel);
                    }

                    //Procesar OMA
                    if (hdnSeleccionFormato.Value == "2")
                    {
                        if (dtDatosOMA != null && dtHeaderRows != null)
                        {
                            RecorrerDtExcelOMA(dtDatosOMA, dtHeaderRows);
                        }
                    }

                    //Procesar AMAIT
                    if (hdnSeleccionFormato.Value == "3")
                    {
                        if (dtUnionAMAIT != null)
                            RecorrerDtExcelAMAIT(dtUnionAMAIT);
                    }

                    //Procesar ASUR
                    if (hdnSeleccionFormato.Value == "5")
                    {
                        if (dtDatosASUR != null)
                            RecorrerTXTASUR(dtDatosASUR);
                    }



                    if (iBanCorrecto == 1)
                    {
                        msgSuccesss.Visible = true;
                        lblSuccess.Text = "Se ha cargado correctamente el archivo.";
                        msgError.Visible = false;
                        lblError.Text = string.Empty;

                        lblArchivoSimulado.Visible = false;
                        lblArchivoSimulado.Text = string.Empty;
                        btnProcesarCargaSimulada.Visible = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sMsgError))
                        {
                            msgSuccesss.Visible = false;
                            lblSuccess.Text = string.Empty;
                            msgError.Visible = true;
                            lblError.Text = sMsgError;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
                msgError.Visible = true;
                Utils.SaveErrorEnBitacora(ex.Message.S(), sPagina, sClase, "btnProcesarCargaSimulada_Click", "Error");
            }
        }

        protected void gvFacturasASUR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[3].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}