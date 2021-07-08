using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Configuration;
using System.IO;

namespace ClientesCasa.Presenter
{
    public class CargaMasiva_Presenter : BasePresenter<IViewCargaMasiva>
    {
        private readonly DBAccesoSAP oIClientesCat;

        public CargaMasiva_Presenter(IViewCargaMasiva oView, DBAccesoSAP oGC)
            : base(oView)
        {
            oIClientesCat = oGC;

            oIView.eSearchProveedores += SearchProveedores_Presenter;
            oIView.eSearchMoneda += SearchMoneda_Presenter;
            oIView.eSearchAlmacen += SearchAlmacen_Presenter;
            oIView.eSearchMatricula += SearchMatricula_Presenter;
            oIView.eSearchCodigoImpuesto += SearchCodigoImpuesto_Presenter;
            oIView.eSearchLugar += SearchLugar_Presenter;
            oIView.eSetProcesaArchivo += eSetProcesaArchivo_Presenter;
            oIView.eGetValidaPDF += eGetValidaPDF_Presenter;
            oIView.eGetValidaXML += eGetValidaXML_Presenter;
            oIView.eGetCodProveedores += eGetCodProveedores_Presenter;
            oIView.eGetCodArticulo += eGetCodArticulo_Presenter;
            oView.eGetIVAImpuesto += eGetIVAImpuesto_Presenter;
            oView.eSearchAreaDpto += SearchAreaDpto_Presenter;
            oView.eGetDesArticulo += GetDesArticulo_Presenter;
            oView.eGetFacturaExist += GetFacturaExist_Presenter;
            oView.eGetAeropuertoExist += GetAeropuertoExist_Presenter;

            oIView.dtError = new System.Data.DataTable();
        }

        protected void SearchProveedores_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExistencia(new DBAccesoSAP().DBGetObtieneExistenciaCodigoProveedor(oIView.sCodigoProveedor));
        }

        protected void SearchMoneda_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExistencia(new DBAccesoSAP().DBGetObtieneExistenciaMoneda(oIView.sMoneda));
        }

        protected void SearchAlmacen_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExistencia(new DBAccesoSAP().DBGetObtieneExistenciaAlmacen(oIView.sAlmacen));
        }

        protected void SearchMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExistencia(new DBAccesoSAP().DBGetObtieneExistenciaMatricula(oIView.sMatricula));
        }

        protected void SearchAreaDpto_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExistencia(new DBAccesoSAP().DBGetObtieneExistenciaAreaDpto(oIView.sMatricula));
        }

        protected void SearchCodigoImpuesto_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExistencia(new DBAccesoSAP().DBGetObtieneExistenciaCodigoImpuesto(oIView.sCodigoImpuesto));
        }

        protected void SearchLugar_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExistencia(new DBAccesoSAP().DBGetObtieneExistenciaLugar(oIView.sLugar));
        }

        protected void GetDesArticulo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDescripcionArticulo(new DBAccesoSAP().DBGetObtieneDescripcionArticulo(oIView.sCodigoArticulo));
        }
        
        protected void eSetProcesaArchivo_Presenter(object sender, EventArgs e)
        {
            foreach (Factura oF in oIView.ListaFacturas)
            {
                if (CreateSapDoc(oF))
                {
                    if (new DBAccesoSAP().DBSetInsertaRegistrosHeader(oF, 1))
                    {
                        new DBAccesoSAP().DBSetInsertaRegistrosDetalle(oF.oLstConceptos);
                        oIView.iBanCorrecto = 1;

                        //new DBAccesoSAP().DBSetInsertaDatosXMLSAP(oF);
                    }
                }
                else
                {
                    if (new DBAccesoSAP().DBSetInsertaRegistrosHeader(oF, 0))
                    {
                        new DBAccesoSAP().DBSetInsertaRegistrosDetalle(oF.oLstConceptos);
                        oIView.iBanCorrecto = 0;
                    }
                }
            }
        }
        
        public bool CreateSapDoc(Factura oF)
        {
            //oPurchaseInvoices
            bool ban = false;
            int iCont = 0;
            int iContNC = 0;
            bool bCM = false;
            bool bNC = false;
            SAPbobsCOM.Documents oSapDoc = null;
            SAPbobsCOM.Documents oSapDocNC = null;

            //Facturas --------------  13
            //Nota de credito -------  14
            //socios de negocio -----  2
            //Factura de proveedores-  18

            try
            {
                MyGlobals.sStepLog = "CreateSapDoc: Empresa[" + oF.sEmpresa.S() + "] - ID[" + oF.iId.S() + "]";

                foreach (ConceptosFactura oCF in oF.oLstConceptos)
                {
                    string sTaxCode = string.Empty;
                    
                    //oSapDoc = null;

                    #region FACTURA
                    if (string.IsNullOrEmpty(oCF.sTipo) || oCF.sTipo == "FAC")
                    {
                        iCont += 1;

                        if (iCont == 1)
                        {
                            oSapDoc = Utils.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);

                            //MyGlobals.sStepLog = "CreateSapDoc: Empresa[" + oF.sEmpresa.S() + "] - ID[" + oF.iId.S() + "]";

                            oSapDoc.CardCode = oF.sProveedor;
                            oSapDoc.DocDate = oF.dtFechaImp; //oF.dtFecha;
                            //oSapDoc.DocDueDate =  oF.dtFecha.AddMonths(1);
                            oSapDoc.TaxDate = oF.dtFecha;
                            oSapDoc.NumAtCard = oF.sNoFactura;
                            oSapDoc.DocCurrency = oF.sMoneda;
                            oSapDoc.Comments = oF.sComentarios;
                            //oSapDoc.Series = ConfigurationManager.AppSettings["SerieFactura"].S().I();
                            //oSapDoc.DiscountPercent = double.Parse(oF.dDescuento.S());

                            if (oIView.sFormato == "5")
                                oSapDoc.DocTotal = double.Parse(oF.dDescuento.S());
                            else
                                oSapDoc.DiscountPercent = double.Parse(oF.dDescuento.S());

                            oSapDoc.DocRate = double.Parse(oF.dTipoCambio.S());
                            oSapDoc.UserFields.Fields.Item("U_PDF").Value = oF.oLstConceptos[0].sPDF;
                            oSapDoc.UserFields.Fields.Item("U_XML").Value = oF.oLstConceptos[0].sXML;
                        }

                        sTaxCode = oCF.sCodigoImpuesto;

                        // FACTURA DE ARTICULOS
                        oSapDoc.Lines.ItemCode = oCF.sItem;
                        oSapDoc.Lines.ItemDescription = oCF.sDescripcionUsuario;
                        //oSapDoc.Lines.Text = "";
                        oSapDoc.Lines.Quantity = double.Parse(oCF.dCantidad.ToString());
                        oSapDoc.Lines.UnitPrice = oCF.dPrecio.S().Db();
                        oSapDoc.Lines.LineTotal = oCF.dTotalLinea.S().Db();
                        oSapDoc.Lines.DiscountPercent = oCF.dDescuento.S().Db();

                        oSapDoc.Lines.TaxCode = sTaxCode;
                        if (!String.IsNullOrEmpty(oCF.sAlmacen))
                        {
                            oSapDoc.Lines.WarehouseCode = oCF.sAlmacen;
                        }

                        //oSapDoc.Lines.AccountCode = oCF.sCuenta;
                        oSapDoc.Lines.ProjectCode = oCF.sProyecto;
                        oSapDoc.Lines.CostingCode = oCF.sDimension1;
                        oSapDoc.Lines.CostingCode2 = oCF.sDimension2;       // MATRICULA
                        oSapDoc.Lines.CostingCode3 = oCF.sDimension3;       // BASE                 U_Site
                        oSapDoc.Lines.CostingCode4 = oCF.sDimension4;       // CODIGO FINANCIERO    U_CodFin
                        oSapDoc.Lines.CostingCode5 = oCF.sDimension5;       // PROYECTO             U_Unidad_Negocio
                        oSapDoc.Lines.UserFields.Fields.Item("U_UBICACION").Value = oCF.sLugar;
                        oSapDoc.Lines.UserFields.Fields.Item("U_ALE_FechaOperacion").Value = DateTime.Parse(oCF.sFechaOperacion); //DateTime.Parse(oCF.sFechaOperacion).ToString("MM/dd/yyyy"); // Ya existe en el servidor 224 Tabla PCH1

                        oSapDoc.Lines.Add();
                        bCM = true;
                    }
                    #endregion

                    #region NOTA DE CRÉDITO
                    else if (oCF.sTipo == "NC")
                    {
                        iContNC += 1;

                        if (iContNC == 1)
                        {
                            oSapDocNC = Utils.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseCreditNotes);
                        }

                        //MyGlobals.sStepLog = "CreateSapDoc: Empresa[" + oF.sEmpresa.S() + "] - ID[" + oF.iId.S() + "]";

                        oSapDocNC.CardCode = oF.sProveedor;
                        oSapDocNC.DocDate = oF.dtFechaImp; //oF.dtFecha;
                        //oSapDoc.DocDueDate =  oF.dtFecha.AddMonths(1);
                        oSapDocNC.TaxDate = oF.dtFecha;
                        oSapDocNC.NumAtCard = oF.sNoFactura;
                        oSapDocNC.DocCurrency = oF.sMoneda;
                        oSapDocNC.Comments = oF.sComentarios;
                        //oSapDoc.Series = ConfigurationManager.AppSettings["SerieFactura"].S().I();
                        oSapDocNC.DiscountPercent = double.Parse(oF.dDescuento.S());
                        //string sTaxCode = string.Empty;
                        oSapDocNC.DocRate = double.Parse(oF.dTipoCambio.S());
                        oSapDocNC.UserFields.Fields.Item("U_PDF").Value = oF.oLstConceptos[0].sPDF;
                        oSapDocNC.UserFields.Fields.Item("U_XML").Value = oF.oLstConceptos[0].sXML;

                        sTaxCode = oCF.sCodigoImpuesto;

                        oSapDocNC.DocTotal = oCF.dTotalLinea.Db();

                        // FACTURA DE ARTICULOS
                        oSapDocNC.Lines.ItemCode = oCF.sItem;
                        oSapDocNC.Lines.ItemDescription = oCF.sDescripcionUsuario;
                        //oSapDoc.Lines.Text = "";
                        oSapDocNC.Lines.Quantity = oCF.dCantidad.S().Db();
                        oSapDocNC.Lines.UnitPrice = oCF.dPrecio.S().Db();
                        oSapDocNC.Lines.DiscountPercent = oCF.dDescuento.S().Db();
                        oSapDocNC.Lines.LineTotal = oCF.dTotalLinea.Db();

                        oSapDocNC.Lines.TaxCode = sTaxCode;
                        if (!String.IsNullOrEmpty(oCF.sAlmacen))
                        {
                            oSapDocNC.Lines.WarehouseCode = oCF.sAlmacen;
                        }

                        //oSapDoc.Lines.AccountCode = oCF.sCuenta;
                        oSapDocNC.Lines.ProjectCode = oCF.sProyecto;
                        oSapDocNC.Lines.CostingCode = oCF.sDimension1;
                        oSapDocNC.Lines.CostingCode2 = oCF.sDimension2;       // MATRICULA
                        oSapDocNC.Lines.CostingCode3 = oCF.sDimension3;       // BASE                 U_Site
                        oSapDocNC.Lines.CostingCode4 = oCF.sDimension4;       // CODIGO FINANCIERO    U_CodFin
                        oSapDocNC.Lines.CostingCode5 = oCF.sDimension5;       // PROYECTO             U_Unidad_Negocio
                        oSapDocNC.Lines.UserFields.Fields.Item("U_UBICACION").Value = oCF.sLugar;
                        oSapDocNC.Lines.UserFields.Fields.Item("U_ALE_FechaOperacion").Value = DateTime.Parse(oCF.sFechaOperacion); //DateTime.Parse(oCF.sFechaOperacion).ToString("MM/dd/yyyy"); // Ya existe en el servidor 224 Tabla PCH1

                        oSapDocNC.Lines.Add();
                        bNC = true;
                    }
                    #endregion
                    
                }

                string sMensaje = string.Empty;

                if (bCM == true)
                {

                    if (oSapDoc.Add() != 0)
                    {
                        //oIView.dtError = new System.Data.DataTable();

                        if (oIView.dtError.Columns.IndexOf("DescripcionError") == -1)
                            oIView.dtError.Columns.Add("DescripcionError", typeof(string));

                        oF.oErr.bExisteError = true;
                        sMensaje = "Error al guardar el documento en SAP.  [" + Utils.oCompany.GetLastErrorCode().S() + "] - " + Utils.oCompany.GetLastErrorDescription();
                        oF.oErr.sMsjError = sMensaje;

                        if (Utils.oCompany.GetLastErrorCode().S() == "-4013")
                        {
                            oIView.sMsgError = "La fecha de la factura " + oF.dtFecha + " esta fuera del período activo, favor de verificar.";
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = " Código Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + oIView.sMsgError;
                            oIView.dtError.Rows.Add(row);
                        }

                        if (Utils.oCompany.GetLastErrorCode().S() == "-10")
                        {
                            //oIView.sMsgError = "El tipo de cambio de " + oF.sMoneda + " no se encuentra registrado en SAP con la fecha " + DateTime.ParseExact(oF.dtFecha.ToShortDateString(), "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/mm/yyyy") + ", favor de verificar.";
                            oIView.sMsgError = "El tipo de cambio de " + oF.sMoneda + " no se encuentra registrado en SAP con la fecha " + oF.dtFecha.ToShortDateString() + ", favor de verificar.";
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = "Código de Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + oIView.sMsgError;
                            oIView.dtError.Rows.Add(row);
                        }

                        if (Utils.oCompany.GetLastErrorCode().S() == "-1116")
                        {
                            oIView.sMsgError = sMensaje;
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = " Código Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + sMensaje;
                            oIView.dtError.Rows.Add(row);
                        }

                        if (Utils.oCompany.GetLastErrorCode().S() == "-5002")
                        {
                            oIView.sMsgError = sMensaje;
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = " Código Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + sMensaje;
                            oIView.dtError.Rows.Add(row);
                        }
                    }
                    else
                    {
                        // TODO OK
                        ban = true;
                        oF.oErr.bExisteError = false;
                        oF.iDocSAP = Utils.oCompany.GetNewObjectKey().S().I();

                        if (oF.iDocSAP < 1)
                            oF.iDocSAP = new DBAccesoSAP().GetValueByQuery("SELECT MAX(DocEntry) FROM OINV WHERE DataSource='O' AND UserSign=" + Utils.oCompany.UserSignature.S()).S().I();
                        else
                        {
                            oF.oErr.sMsjError = "Se creo una factura de proveedor en SAP. DB[" + Utils.oCompany.CompanyDB + "] - DocEntry[" + oF.iDocSAP.S() + "] - ID_Tabla[" + oF.iId + "]";
                            oIView.sMsgError = oF.oErr.sMsjError;
                        }
                    }
                }


                if (bNC == true)
                {
                    if (oSapDocNC.Add() != 0)
                    {
                        //oIView.dtError = new System.Data.DataTable();

                        if (oIView.dtError.Columns.IndexOf("DescripcionError") == -1)
                            oIView.dtError.Columns.Add("DescripcionError", typeof(string));

                        oF.oErr.bExisteError = true;
                        sMensaje = "Error al guardar el documento en SAP.  [" + Utils.oCompany.GetLastErrorCode().S() + "] - " + Utils.oCompany.GetLastErrorDescription();
                        oF.oErr.sMsjError = sMensaje;

                        if (Utils.oCompany.GetLastErrorCode().S() == "-4013")
                        {
                            oIView.sMsgError = "La fecha de la factura " + oF.dtFecha + " esta fuera del período activo, favor de verificar.";
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = " Código Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + oIView.sMsgError;
                            oIView.dtError.Rows.Add(row);
                        }

                        if (Utils.oCompany.GetLastErrorCode().S() == "-10")
                        {
                            //oIView.sMsgError = "El tipo de cambio de " + oF.sMoneda + " no se encuentra registrado en SAP con la fecha " + DateTime.ParseExact(oF.dtFecha.ToShortDateString(), "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/mm/yyyy") + ", favor de verificar.";
                            oIView.sMsgError = "El tipo de cambio de " + oF.sMoneda + " no se encuentra registrado en SAP con la fecha " + oF.dtFecha.ToShortDateString() + ", favor de verificar.";
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = "Código de Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + oIView.sMsgError;
                            oIView.dtError.Rows.Add(row);
                        }

                        if (Utils.oCompany.GetLastErrorCode().S() == "-1116")
                        {
                            oIView.sMsgError = sMensaje;
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = " Código Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + sMensaje;
                            oIView.dtError.Rows.Add(row);
                        }

                        if (Utils.oCompany.GetLastErrorCode().S() == "-5002")
                        {
                            oIView.sMsgError = sMensaje;
                            System.Data.DataRow row = oIView.dtError.NewRow();

                            row["DescripcionError"] = " Código Proveedor/Factura(" + oF.sProveedor + "/" + oF.sNoFactura + "). " + sMensaje;
                            oIView.dtError.Rows.Add(row);
                        }
                    }
                    else
                    {
                        // TODO OK
                        ban = true;
                        oF.oErr.bExisteError = false;
                        oF.iDocSAP = Utils.oCompany.GetNewObjectKey().S().I();

                        if (oF.iDocSAP < 1)
                            oF.iDocSAP = new DBAccesoSAP().GetValueByQuery("SELECT MAX(DocEntry) FROM OINV WHERE DataSource='O' AND UserSign=" + Utils.oCompany.UserSignature.S()).S().I();
                        else
                        {
                            oF.oErr.sMsjError = "Se creo una factura de proveedor en SAP. DB[" + Utils.oCompany.CompanyDB + "] - DocEntry[" + oF.iDocSAP.S() + "] - ID_Tabla[" + oF.iId + "]";
                            oIView.sMsgError = oF.oErr.sMsjError;
                        }
                    }
                }
                
                return ban;
            }
            catch (Exception ex)
            {
                string sMsg = string.Empty;
                sMsg = "Error al importar registro de DB Intermedia. Tabla[tbp_CC_Facturas] - " + "ID[" + oF.iId.S() + "] Mensaje de Error: " + ex.Message;
                oF.oErr.bExisteError = true;
                oF.oErr.sMsjError = sMsg.Replace("'", "");

                return false;
            }
            finally
            {
                Utils.DestroyCOMObject(oSapDoc);
                Utils.DestroyCOMObject(oSapDocNC);
            }
        }
        
        protected bool InsertaHeaderCC(Factura oFac, int iEstatus) 
        {
            try
            {
                return new DBAccesoSAP().DBSetInsertaRegistrosHeader(oFac, iEstatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected bool InsertaDetalleCC(List<ConceptosFactura> oLs)
        {
            try
            {
                new DBAccesoSAP().DBSetInsertaRegistrosDetalle(oLs);
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected void eGetValidaPDF_Presenter(object sender, EventArgs e)
        {
            oIView.sCadArchivo = ArmaRuta(2);
        }
        
        protected void eGetValidaXML_Presenter(object sender, EventArgs e)
        {
            oIView.sCadArchivo = ArmaRuta(1);
        }

        private string ArmaRuta(int iOpcion)
        {
            string sTipoDoc = string.Empty;
            if (iOpcion == 1)
                sTipoDoc = ".xml";
            else
                sTipoDoc = ".pdf";

            string sCadFinal = string.Empty;

            if (oIView.sValoresValidacion != string.Empty)
            {
                string[] sValores = oIView.sValoresValidacion.Split('|');

                if (sValores.Length == 4)
                {
                    string sRuta = System.Configuration.ConfigurationManager.AppSettings["RutaArchivos"].S();

                    string sGroupCode = new DBAccesoSAP().DBGetObtieneGruopCodeProveedor(sValores[0]).Rows[0][0].S();
                    string sCardName = new DBAccesoSAP().DBGetObtieneGruopCodeProveedor(sValores[0]).Rows[0][1].S();
                    string sFolderNalExt = string.Empty;
                    string sFolder2 = string.Empty;
                    switch(sGroupCode)
                    {
                        // Nacionales
                        case "102":
                            sFolder2 = "nacionales";
                            sFolderNalExt = System.Configuration.ConfigurationManager.AppSettings["FolderNacional"].S();
                            break;
                        // Extranjeros
                        case "103":
                            sFolder2 = "extranjeros";
                            sFolderNalExt = System.Configuration.ConfigurationManager.AppSettings["FolderExtranjero"].S();
                            break;
                    }

                    
                    string sAnio = sValores[2].Substring(0,4).S();
                    string sMes = sValores[2].Substring(4, 2).S();



                    sCadFinal = sRuta + sFolderNalExt + @"\" + sAnio + @"\Proveedores " + sFolder2 +" " + 
                        sValores[3] + @"\" + sCardName + @"\" + sMes + @"\" + sValores[1] + sTipoDoc;
                }
                
            }

            return sCadFinal;
        }

        protected void eGetCodProveedores_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCodigoProveedor(new DBCarga().DBGetCodigoProveedor(oIView.sEmpresa));
        }

        protected void eGetCodArticulo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCodigoArticulo(new DBCarga().DBGetCodigoArticulo(oIView.sServicio));
        }

        protected void eGetIVAImpuesto_Presenter(object sender, EventArgs e)
        {
            oIView.LoadIVA(new DBAccesoSAP().DBGetObtieneIVAImpuesto(oIView.sCodigoImpuesto));
        }

        protected void GetFacturaExist_Presenter(object sender, EventArgs e)
        {
            oIView.sExist = new DBAccesoSAP().DBGetExisteFactura(oIView.sFacExists);
        }

        protected void GetAeropuertoExist_Presenter(object sender, EventArgs e)
        {
            oIView.sExist = new DBAccesoSAP().DBGetExisteAeropuerto(oIView.sAeroExists);
        }
        
    }
}