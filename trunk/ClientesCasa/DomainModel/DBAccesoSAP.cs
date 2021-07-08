using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using System.Globalization;

namespace ClientesCasa.DomainModel
{
    public class DBAccesoSAP : DBBaseSAP
    {

        public object GetValueByQuery(string sQ)
        {
            SAPbobsCOM.Recordset oRS = Utils.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            object sRes = null;

            try
            {
                oRS.DoQuery(sQ);
                if (oRS.RecordCount > 0)
                    sRes = oRS.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oRS != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRS);
                    oRS = null;
                    GC.Collect();
                }
            }

            return sRes;
        }
        public DataTable GetQuerySAP(string sQuery)
        {
            try
            {
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneExistenciaCodigoProveedor(string sCodigoProveedor)
        {
           try
            {
                string sResult = "SELECT COUNT(1) CodigoProveedor FROM [Aerolineas_Ejecutivas].[dbo].[OCRD] WHERE CardCode = '" + sCodigoProveedor + "'" + " AND CardType = 'S'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneExistenciaMoneda(string sMoneda)
        {
            try
            {
                string sResult = "SELECT COUNT(1) Moneda FROM [Aerolineas_Ejecutivas].[dbo].[OCRN] WHERE CurrCode LIKE '%" + sMoneda + "%'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneExistenciaAlmacen(string sAlmacen)
        {
            try
            {
                string sResult = "SELECT COUNT(1) Almacen FROM [Aerolineas_Ejecutivas].[dbo].[OWHS] WHERE WhsCode = '" + sAlmacen + "'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneExistenciaMatricula(string sMatricula)
        {
            try
            {
                string sResult = "SELECT COUNT(1) Matricula FROM [Aerolineas_Ejecutivas].[dbo].[OPRC] WHERE PrcCode LIKE '%" + sMatricula + "%' AND DimCode=2 AND Active='Y'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneExistenciaAreaDpto(string sArea)
        {
            try
            {
                string sResult = "SELECT COUNT(1) Area FROM [Aerolineas_Ejecutivas].[dbo].[OPRC] WHERE PrcCode LIKE '%" + sArea + "%' AND DimCode=1 AND Active='Y'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneExistenciaCodigoImpuesto(string sCodigoImpuesto)
        {
            try
            {
                string sResult = "SELECT COUNT(1) CodigoImpuesto FROM [Aerolineas_Ejecutivas].[dbo].[OSTC] WHERE CODE LIKE '%" + sCodigoImpuesto + "%' AND ValidForAP='Y'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneExistenciaLugar(string sLugar)
        {
            try
            {
                string sResult = "SELECT COUNT(1) Lugar FROM [Aerolineas_Ejecutivas].[dbo].[@UBICACIONES] WHERE Code LIKE '%" + sLugar + "%'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DBSetInsertaRegistrosHeader(Factura oFac, int iEstatus)
        {
            try
            {
                // hacer metodos para insertar header y detalle en las tablas para que quede la evidencia.
                object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaDatosHeaderFactura]", "@Empresa", oFac.sEmpresa,
                                                                                                                    "@ID", oFac.iId,
                                                                                                                    "@Sucursal", oFac.sSucursal,
                                                                                                                    "@Fecha", oFac.dtFecha,
                                                                                                                    "@Cliente", oFac.sProveedor,
                                                                                                                    "@Referencia", oFac.sNoFactura,
                                                                                                                    "@TipoFactura", oFac.sTipoFactura,
                                                                                                                    "@Comentarios", oFac.sComentarios,
                                                                                                                    "@Serie", oFac.sSerie,
                                                                                                                    "@Moneda", oFac.sMoneda,
                                                                                                                    "@Timbrar",oFac.iTimbrar,
                                                                                                                    "@UUID", oFac.sUID,
                                                                                                                    "@Descuento", oFac.dDescuento,
                                                                                                                    "@Estatus", iEstatus,
                                                                                                                    "@MetodoPago", oFac.sMetodoPago,
                                                                                                                    "@FormaPago", oFac.sFormaPago,
                                                                                                                    "@UsoCFDI", oFac.sUsoCFDI,
                                                                                                                    "@FechaExp", oFac.dtFechaExp,
                                                                                                                    "@FechaImp", oFac.dtFechaImp,
                                                                                                                    "@Msg", oFac.sMsg,
                                                                                                                    "@SapDoc", oFac.iDocSAP);

                if (oFac.iDocSAP != 0)
                {
                    System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(oFac.sProveedor);

                    if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                    {
                        //Inserción de datos XML a SAP
                        string sCode = string.Empty;
                        string sName = string.Empty;
                        string sAccount = string.Empty;
                        string sJrnalline = string.Empty;
                        string sRemarks = string.Empty;
                        string sUID = string.Empty;

                        if (!string.IsNullOrEmpty(oFac.sUID) && !string.IsNullOrEmpty(oFac.iDocSAP.S()) && !string.IsNullOrEmpty(oFac.sRFC) && !string.IsNullOrEmpty(oFac.sMonto))
                        {
                            String[] sArrUID = oFac.sUID.S().Split('-');
                            sUID = sArrUID[0].S() + "-" + sArrUID[1].S() + "-" + sArrUID[2].S() + "-" + sArrUID[3].S() + "-";

                            sCode = oFac.iDocSAP + "_" + sUID;
                            sName = oFac.iDocSAP + "_" + sUID;
                            sAccount = "2101-0100";
                            sJrnalline = "0";
                            sRemarks = "Factura de:";

                            string sResult = "INSERT INTO [Aerolineas_Ejecutivas].[dbo].[@BXP_ACTPCH](Code, Name, U_BXP_DOCENTRY, U_BXP_UUID,U_BXP_RFC, U_BXP_AMOUNT, U_BXP_ACCOUNT, U_BXP_JRNALLINE, U_BXP_REMARKS) " +
                                             "VALUES('" + sCode + "','" + sName + "','" + oFac.iDocSAP + "','" + oFac.sUID + "', '" + oFac.sRFC + "'," + oFac.sMonto.D() + ",'" + sAccount + "','" + sJrnalline + "','" + sRemarks + "')";

                            oDB_SP.EjecutarDT_DeQuery(sResult);
                        }
                        //Fin de Inserción de datos XML a SAP
                    }
                }


                return oRes != null ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaRegistrosDetalle(List<ConceptosFactura> oLstCn)
        {
            try
            {
                foreach (ConceptosFactura oCon in oLstCn)
                {
                    object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaDatosDetalleFactura]", "@Empresa", oCon.sEmpresa,
                                                                                                                        "@ID", oCon.iId,
                                                                                                                        "@Linea", oCon.iLinea,
                                                                                                                        "@Item", oCon.sItem,
                                                                                                                    "@DescripcionUsuario",oCon.sDescripcionUsuario,
                                                                                                                        "@CodBarras", oCon.sCodBarras,
                                                                                                                        "@Cantidad", oCon.dCantidad,
                                                                                                                        "@Precio", oCon.dPrecio,
                                                                                                                        "@Descuento", oCon.dDescuento,
                                                                                                                        "@Impuesto", oCon.iImpuesto,
                                                                                                                        "@CodigoImpuesto", oCon.sCodigoImpuesto,
                                                                                                                        "@TotalLinea", oCon.dTotalLinea,
                                                                                                                        "@Almacen", oCon.sAlmacen,
                                                                                                                        "@Proyecto", oCon.sProyecto,
                                                                                                                        "@Dimension1", oCon.sDimension1,
                                                                                                                        "@Dimension2", oCon.sDimension2,
                                                                                                                        "@Dimension3", oCon.sDimension3,
                                                                                                                        "@Dimension4", oCon.sDimension4,
                                                                                                                        "@Dimension5", oCon.sDimension5,
                                                                                                                        "@XML", oCon.sXML,
                                                                                                                        "@PDF", oCon.sPDF,
                                                                                                                        "@FechaOperacion", oCon.sFechaOperacion.Dt());
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneGruopCodeProveedor(string sClaveProveedor)
        {
            try
            {
                string sQuery = "SELECT top 1 GroupCode, CardName FROM OCRD WHERE CardCode= '" + sClaveProveedor + "'";
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal DBGetObtieneTipoCambioDia(DateTime dtFechaFact)
        {
            try
            {
                string sQuery = "SELECT TOP 1 Rate FROM ORTT (NOLOCK) WHERE Currency = 'USD' AND YEAR(RateDate) = " + dtFechaFact.Year.S() + " AND MONTH(RateDate) = " + dtFechaFact.Month.S() + " AND DAY(RateDate) = " + dtFechaFact.Day.S();
                return oDB_SP.EjecutarValor_DeQuery(sQuery).S().D();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneInformacionMatricula(string sMatricula)
        {
            try
            {
                string sQuery = "SELECT Code, Name, U_Site, U_Unidad_Negocio, U_CodFin, U_Serie FROM [@MATRICULAS] (NOLOCK) WHERE Code = '" + sMatricula + "'";
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneInformacionArea(string sArea)
        {
            try
            {
                string sQuery = "SELECT Code, Name, U_Site, U_Unidad_Negocio, U_CodFin FROM [@AREA] (NOLOCK) WHERE Code = '" + sArea + "'";
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBGetValidaExisteFactura(string sCodProv, string sFolioFact)
        {
            try
            {
                string sQuery = "SELECT TOP 1 1 FROM OPCH (NOLOCK) WHERE CardCode = '" + sCodProv + "' AND NumAtCard = '" + sFolioFact + "'";
                DataTable dtRes = oDB_SP.EjecutarDT_DeQuery(sQuery);
                if (dtRes != null && dtRes.Rows.Count > 0)
                {
                    return dtRes.Rows[0][0].S().I() > 0 ? true : false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneDocRequeridosProveedor(string sCardCode)
        {
            try
            {
                string sQuery = "SELECT TOP 1 CardCode, CardName, U_REQPDF, U_REQXML FROM OCRD (NOLOCK) WHERE CardCode = '" + sCardCode + "'";
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneCuentasFiltradas(string sFiltro)
        {
            try
            {
                string sQuery = "SELECT AcctCode, ISNULL(AcctCode,'') + '-' + ISNULL(AcctName,'') AcctName FROM OACT (NOLOCK) WHERE AcctCode LIKE '%" + sFiltro + "%'";
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneIVAImpuesto(string sCodigoImpuesto)
        {
            try
            {
                string sResult = "SELECT CASE WHEN Rate > 0 THEN (Rate/100) + 1 ELSE Rate END AS Rate FROM [Aerolineas_Ejecutivas].[dbo].[OSTC] WHERE Code = '" + sCodigoImpuesto + "'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetObtieneDescripcionArticulo(string sCodigoArticulo)
        {
            try
            {
                string sResult = "SELECT Top 1 ItemName FROM [Aerolineas_Ejecutivas].[dbo].[OITM] WHERE ItemCode = '" + sCodigoArticulo + "'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetObtieneCodigoImpuestoArticulo(string sCodigoArticulo)
        {
            try
            {
                string sResult = "SELECT TOP 1 CodigoImpuesto FROM [ClientesCasa].[tbp_CC_Articulos] WHERE CodigoArticulo = '" + sCodigoArticulo + "'";
                return new DBBase().oDB_SP.EjecutarDT_DeQuery(sResult);
                //return oDB_SP.EjecutarDT_DeQuery(sResult);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DBGetExisteFactura(string sFactura)
        {
            try
            {
                object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spS_CC_ConsultaExistenciaFactura]", "@Factura", sFactura);
                return oRes.S();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetObtieneAlmacen(string sMatricula)
        {
            try
            {
                string sResult = "SELECT Top 1 CCTypeCode FROM [Aerolineas_Ejecutivas].[dbo].[OPRC] WHERE PrcCode = '" + sMatricula + "'";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DBGetExisteAeropuerto(string sCveAero)
        {
            try
            {
                object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spS_CC_ConsultaExistenciaAeropuerto]", "@CveAero", sCveAero);
                return oRes.S();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region MODULO DE ENVIO DE FACTURAS

        public DataTable DBGetObtieneFacturas(string sNumDoc)
        {
            try
            {
                string sResult = "SELECT OP.DocEntry, OP.NumAtCard, OP.DocDate, OP.CardCode, OP.CardName, OP.DocTotal ";
                sResult += "FROM [Aerolineas_Ejecutivas].[dbo].[OPCH] OP ";
                sResult += "WHERE OP.DocEntry = '" + sNumDoc + "' ";
                sResult += "ORDER BY OP.DocDate DESC";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Consulta de Matriculas SAP
        public DataTable DBGetObtieneMatriculasSAP()
        {
            try
            {
                string sResult = "SELECT OcrCode, OcrName FROM OOCR WHERE DimCode = 2 AND Active = 'Y' ";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetObtieneMatriculas()
        {
            try
            {
                string sResult = "SELECT Code AS Matricula,U_Unidad_Negocio AS DesFlota,Name,U_Site,U_CodFin FROM [@MATRICULAS] ";
                sResult += "WHERE U_Unidad_Negocio IN ('OPE','ALE','MEX')";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetObtieneTiposMtto()
        {
            try
            {
                string sResult = "SELECT Id_TipoMtto, TipoMtto FROM [ClientesCasa].[tbc_CC_ASC_TipoMtto]";
                return new DBBase().oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DBGetCardCodePorCardName(string sEmpresa)
        {
            try
            {
                string sResult = "SELECT CardCode FROM OCRD WITH(NOLOCK) WHERE CardName = '" + sEmpresa + "'";
                DataTable dt = oDB_SP.EjecutarDT_DeQuery(sResult);

                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].S();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


    }
}