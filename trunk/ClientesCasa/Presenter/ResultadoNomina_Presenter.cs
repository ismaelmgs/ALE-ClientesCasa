using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Data;

namespace ClientesCasa.Presenter
{
    public class ResultadoNomina_Presenter : BasePresenter<IViewResultNomina>
    {
        private readonly DBResultadoNomina oIClientesCat;

        public ResultadoNomina_Presenter(IViewResultNomina oView, DBResultadoNomina oGC) : base(oView)
        {
            oIClientesCat = oGC;

            oIView.eUpdApruebaFactura += eUpdApruebaFactura_Presenter;
            oIView.eSaveXMLFile += eSaveXMLFile_Presenter;
            oIView.eSavePDFFile += eSavePDFFile_Presenter;

            oIView.eValidaPDFFile += eValidaPDFFile_Presenter;
            oIView.eValidaXMLFile += eValidaXMLFile_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadParametros(oIClientesCat.DBGetObtieneParametrosPolizaNomina());

            DataSet dsDetalle = oIClientesCat.DBGetObtieneDetalleNomina(oIView.iIdFolio);
            dsDetalle.Tables[1].Columns.Add("CardCode");

            foreach (DataRow row in dsDetalle.Tables[1].Rows)
            {
                string sCardCodeEmpresa = new DBAccesoSAP().DBGetCardCodePorCardName(row["Empresa"].S());
                row["CardCode"] = sCardCodeEmpresa;
            }

            dsDetalle.Tables[1].AcceptChanges();

            oIView.dsDetalleNomina = dsDetalle.Copy();
            oIView.LoadDetalleNomina();
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                foreach (PolizaNominaSAP oPol in oIView.oLstPoliza)
                {
                    DataTable dt = oIClientesCat.DBGetObtieneConceptosDetalleNomina(oIView.iIdFolio, oPol.sEmpresa, 0);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ConceptosSAP oCon = new ConceptosSAP();
                            oCon.sCuenta = row["Cuenta"].S();
                            oCon.sItem = row["Rfc"].S();
                            oCon.dPrecio = row["Debe"].S().D();
                            oCon.sProyecto = row["UNeg"].S();
                            oCon.sDimension1 = row["Area"].S();
                            oCon.sDimension2 = row["Matricula"].S();
                            oCon.sDimension3 = row["Base"].S();
                            oCon.sDimension4 = row["CodFin"].S();
                            oCon.sCodigoImpuesto = oIView.sTaxCode;

                            oPol.oLstConceptos.Add(oCon);
                        }
                    }

                    oIClientesCat.GetLoadInitialValues();
                    CreateSapDoc(oPol);

                    if (MyGlobals.oCompany.Connected)
                        MyGlobals.oCompany.Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaEmpleadosPorEmpresa(oIClientesCat.DBGetObtieneEmpleadoPorEmpresaYFactura(oIView.iIdFolio, oIView.sEmpresa, oIView.iNoFact));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            int iFolio = oIView.iIdFolio;
            string sEmpresa = oIView.sEmpresa;
            int iNoFact = oIView.iNoFact;

            oIClientesCat.DBSetEliminaPolizaPorEmpresaYFolio(iFolio, sEmpresa, iNoFact);
            DataTable dt = oIClientesCat.DBGetObtieneEmpleadoPorEmpresaYFactura(iFolio, sEmpresa, iNoFact);
            foreach (DataRow row in dt.Rows)
            {
                oIClientesCat.DBSetInsertaResultadoPolizaPorEmpleado(iFolio, row["RFCEmpleado"].S(), sEmpresa, iNoFact);
            }

            DataTable dtConcept = oIClientesCat.DBGetObtieneConceptosDetalleNomina(iFolio, sEmpresa, iNoFact);
            List<ConceptosSAP> olst = new List<ConceptosSAP>();
            foreach (DataRow row in dtConcept.Rows)
            {
                ConceptosSAP oCF = new ConceptosSAP();
                oCF.sCuenta = row["Cuenta"].S();                // Comision u Outsourcing
                oCF.sItem = row["Rfc"].S();                     // RFC del empleado
                oCF.dPrecio = row["Debe"].S().D();              // Debe
                oIView.sTaxCode = "IVAA16";                     // IVAA16
                oCF.sProyecto = row["UNeg"].S();                // UNeg
                oCF.sDimension1 = row["Area"].S();              // Area
                oCF.sDimension2 = row["Matricula"].S();         // Matricula
                oCF.sDimension3 = row["Base"].S();              // Base
                oCF.sDimension4 = row["CodFin"].S();            // CodFin

                olst.Add(oCF);
            }

            //oIView.ArmaListadoFacturas(sEmpresa, olst);

            PolizaNominaSAP oPol = new PolizaNominaSAP();
            oPol.sEmpresa = sEmpresa.S();
            oPol.sSucursal = Helpers.sSucursal;
            oPol.dtFecha = DateTime.Now; //dsDetalleNomina.Tables[0].Rows[0]["FechaInicio"].S().Dt();
            string sCardCodeEmpresa = new DBAccesoSAP().DBGetCardCodePorCardName(sEmpresa.S());
            oPol.sCardCodeEmpresa = sCardCodeEmpresa;
            oPol.sNoReporte = "1"; //dsDetalleNomina.Tables[0].Rows[0]["NombreArchivo"].S();
            oPol.sMoneda = "MXN";
            oPol.sComentarios = string.Empty;
            oPol.dDescuento = 0;
            oPol.sSerie = Helpers.sSerie;

            oPol.oLstConceptos = olst;


            oIClientesCat.GetLoadInitialValues();
            CreateSapDoc(oPol);

            if (MyGlobals.oCompany.Connected)
                MyGlobals.oCompany.Disconnect();
        }

        protected void eUpdApruebaFactura_Presenter(object sender, EventArgs e)
        {
            int iRes = oIClientesCat.DBSetInsertaFacturaAprobadaRH(oIView.iIdFolio, oIView.sEmpresa, oIView.iNoFact);

            if (iRes > 0)
                oIView.MostrarMensaje("La factura se aprobó correctamente.", "Aviso");
        }

        public bool CreateSapDoc(PolizaNominaSAP oF)
        {
            bool ban = false;
            SAPbobsCOM.Documents oSapDoc = MyGlobals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
            oSapDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;
            oSapDoc.DocObjectCodeEx = "18";

            //Facturas --------------  13
            //Nota de credito -------  14
            //socios de negocio -----  2
            //Factura de proveedores-  18

            try
            {
                MyGlobals.sStepLog = "CreateSapDoc: Empresa[" + oF.sEmpresa.S() + "] - ID[" + oF.iId.S() + "]";

                oSapDoc.CardCode = oF.sCardCodeEmpresa;                                     //Empresa CardCode (P00450) OCRD
                oSapDoc.DocDate = oF.dtFecha;                                               //Fecha contabilizacion
                oSapDoc.DocDueDate = oF.dtFecha.AddMonths(1);                               //Fecha contabilizacion + 1 mes
                oSapDoc.NumAtCard = oF.sNoReporte;                                          //Nombre del archivo
                oSapDoc.DocCurrency = oF.sMoneda;                                           //MXN
                oSapDoc.Comments = oF.sComentarios;                                         //string.empty
                oSapDoc.Series = Helpers.sSerie.S().I();                                    //10
                oSapDoc.DiscountPercent = double.Parse(oF.dDescuento.S());                  //0
                //string sTaxCode = string.Empty;                                             //IVAA16
                //sTaxCode = oIView.sTaxCode;
                //oSapDoc.DocRate = double.Parse(oF.dTipoCambio.S());                       //
                //oSapDoc.UserFields.Fields.Item("U_TIPOCOMP").Value = oF.sFormaPago;       //
                //oSapDoc.ControlAccount = ConfigurationManager.AppSettings["CuentaEmp"].S(); //string.empty

                int Cont = 0;
                foreach (ConceptosSAP oCF in oF.oLstConceptos)
                {
                    //if (new DBResultadoNomina().GetValueByQuery("SELECT COUNT(1) FROM OITM WHERE ITEMCODE='" + oCF.sItem + "' ").S().I() == 0)
                    //{
                    //    throw new Exception("No existe el item " + oCF.sItem + " en SAP. DB[" + MyGlobals.oCompany.CompanyDB + "] ");
                    //}

                    //sTaxCode = oCF.sCodigoImpuesto;

                    //// FACTURA DE ARTICULOS
                    //oSapDoc.Lines.ItemCode = oCF.sItem;
                    //oSapDoc.Lines.ItemDescription = oCF.sDescripcionUsuario;
                    ////oSapDoc.Lines.Text = "";
                    //oSapDoc.Lines.Quantity = oCF.iCantidad.S().Db();
                    //oSapDoc.Lines.UnitPrice = oCF.dPrecio.S().Db();
                    //oSapDoc.Lines.DiscountPercent = oCF.dDescuento.S().Db();

                    //oSapDoc.Lines.TaxCode = sTaxCode;
                    //if (!String.IsNullOrEmpty(oCF.sAlmacen))
                    //{
                    //    oSapDoc.Lines.WarehouseCode = oCF.sAlmacen;
                    //}

                    ////oSapDoc.Lines.AccountCode = oCF.sCuenta;
                    //oSapDoc.Lines.ProjectCode = oCF.sProyecto;

                    //oSapDoc.Lines.CostingCode = oCF.sDimension1;
                    //oSapDoc.Lines.CostingCode2 = oCF.sDimension2;
                    //oSapDoc.Lines.CostingCode3 = oCF.sDimension3;
                    //oSapDoc.Lines.CostingCode4 = oCF.sDimension4;
                    //oSapDoc.Lines.CostingCode5 = oCF.sDimension5;
                    //oSapDoc.Lines.UserFields.Fields.Item("U_OBS").Value = oCF.sDescripcionAmpliada;
                    //oSapDoc.Lines.Add();


                    // FACTURA DE SERVICIOS
                    oSapDoc.Lines.AccountCode = oCF.sCuenta;                    // Comision u Outsourcing
                    oSapDoc.Lines.ItemDescription = oCF.sItem;    // RFC del empleado
                    oSapDoc.Lines.UnitPrice = oCF.dPrecio.S().Db();             // Debe
                    oSapDoc.Lines.TaxCode = oIView.sTaxCode;                    // IVAA16
                    oSapDoc.Lines.ProjectCode = oCF.sProyecto;                  // UNeg
                    oSapDoc.Lines.CostingCode = oCF.sDimension1;                // Area
                    oSapDoc.Lines.CostingCode2 = oCF.sDimension2;               // Matricula
                    oSapDoc.Lines.CostingCode3 = oCF.sDimension3;               // Base
                    oSapDoc.Lines.CostingCode4 = oCF.sDimension4;               // CodFin
                    oSapDoc.Lines.Add();

                    Cont++;
                }

                string sMensaje = string.Empty;
                if (oSapDoc.Add() != 0)
                {
                    oF.oEstatus.iEstatus = 0;
                    sMensaje = "Error al guardar el documento en SAP.  [" + MyGlobals.oCompany.GetLastErrorCode().S() + "] - " + MyGlobals.oCompany.GetLastErrorDescription();
                    oF.oEstatus.sMensaje = sMensaje;
                }
                else
                {
                    ban = true;
                    oF.oEstatus.iEstatus = 1;
                    oF.oEstatus.iSapDoc = MyGlobals.oCompany.GetNewObjectKey().S().I();

                    if (oF.oEstatus.iSapDoc < 1)
                        oF.oEstatus.iSapDoc = new DBResultadoNomina().GetValueByQuery("SELECT MAX(DocNum) FROM OINV WHERE DataSource='O' AND UserSign=" + MyGlobals.oCompany.UserSignature.S()).S().I();
                    else
                        oF.oEstatus.sMensaje = "Se creo una factura de proveedor en SAP. DB[" + MyGlobals.oCompany.CompanyDB + "] - DocEntry[" + oF.oEstatus.iSapDoc.S() + "] - ID_Tabla[" + oF.oEstatus.iID + "]";
                }

                return ban;
            }
            catch (Exception ex)
            {
                string sMsg = string.Empty;
                sMsg = "Error al importar registro de DB Intermedia. Tabla[" + oF.oEstatus.sTabla + "] - " + "ID[" + oF.oEstatus.iID.S() + "] Mensaje de Error: " + ex.Message;
                oF.oEstatus.iEstatus = 0;
                oF.oEstatus.sMensaje = sMsg.Replace("'", "");
                //throw new Exception(oF.oEstatus.sMensaje);

                return false;
            }
            finally
            {
                Utils.DestroyCOMObject(oSapDoc);
            }
        }

        private void InsertaProcesadoPolizaNomina()
        {

        }

        protected void eSaveXMLFile_Presenter(object sender, EventArgs e)
        {
            int iRes = oIClientesCat.DBSetInsertaArchivoXML(oIView.iIdFolio, oIView.sEmpresa, oIView.iNoFact, oIView.oArrFile, oIView.sFileName);
            if (iRes > 0)
                oIView.MostrarMensaje("El xml se adjunto correctamente.", "Aviso");
        }

        protected void eSavePDFFile_Presenter(object sender, EventArgs e)
        {
            int iRes = oIClientesCat.DBSetInsertaArchivoPDF(oIView.iIdFolio, oIView.sEmpresa, oIView.iNoFact, oIView.oArrFile, oIView.sFileName);
            if (iRes > 0)
                oIView.MostrarMensaje("El PDF se adjunto correctamente.", "Aviso");
        }

        protected void eValidaPDFFile_Presenter(object sender, EventArgs e)
        {
            DataSet ds = oIClientesCat.DBGetValidaPDF_Factura(oIView.iIdFolio, oIView.sEmpresa, oIView.iNoFact);
            oIView.iExistePDF = 0;

            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string str = ds.Tables[0].Rows[0]["Res"].ToString();
                    if (str.I() == 1)
                        oIView.iExistePDF = 1;
                }
                        
        }
        protected void eValidaXMLFile_Presenter(object sender, EventArgs e)
        {
            string strREs = string.Empty;
            DataSet ds = oIClientesCat.DBGetValidaXML_Factura(oIView.iIdFolio, oIView.sEmpresa, oIView.iNoFact);
            oIView.iExisteXML = 0;

            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string str = ds.Tables[0].Rows[0]["Res"].ToString();
                    if (str.I() == 1)
                        oIView.iExisteXML = 1;
                }
                        
                
        }
    }
    }