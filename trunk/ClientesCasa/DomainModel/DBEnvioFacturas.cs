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
    public class DBEnvioFacturas : DBBase
    {
        //Obtiene Flotas de Mexjet
        public DataTable DBGetObtieneFlotas()
        {
            try
            {
                string sResult = "SELECT IdFlota, DescripcionFlota FROM [Catalogos].[tbc_MXJ_Flota] WHERE Status = 1";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetObtieneMatriculasMXJ()
        {
            try
            {
                string sResult = "SELECT MA.Matricula, MF.DescripcionFlota FROM [Catalogos].[tbc_MXJ_Aeronave] MA ";
                sResult += "INNER JOIN [Catalogos].[tbc_MXJ_Flota] MF ON MA.IdFlota = MF.IdFlota ";
                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DBSetActualizaFacturasHeader(FacturaASC oFac)
        {
            try
            {
                object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_ASC_ActualizacionHeaderFactura]", "@NumDoc", oFac.sNumDoc,
                                                                                                                          "@NumFac", oFac.sNoFactura,
                                                                                                                          "@Matricula", oFac.sCentroCostos,
                                                                                                                          "@TipoMtto", oFac.iTipoMtto,
                                                                                                                          "@TipoMtto2", oFac.iTipoMtto2,
                                                                                                                          "@Status", oFac.iStatus,
                                                                                                                          "@Observaciones", oFac.sObservaciones,
                                                                                                                          "@User", oFac.sUser);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtieneFacturasASC(string sNumDoc, string sFechaDesde, string sFechaHasta, string sTipoMtto, string sEstatus, string sFlota, string sMatricula) 
        {
            try
            {

                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ASC_ConsultaFacturas]", "@NumDoc", sNumDoc,
                                                                                         "@FechaDesde", sFechaDesde,
                                                                                         "@FechaHasta", sFechaHasta,
                                                                                         "@TipoMtto", sTipoMtto,
                                                                                         "@Estatus", sEstatus,
                                                                                         "@Flota", sFlota,
                                                                                         "@Matricula", sMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtieneFacturasParticionesASC(string sNumDoc)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ASC_ConsultaFacturaDetalle]", "@NumDoc", sNumDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtieneArticulos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ASC_ConsultaArticulos]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Consulta para aprobación
        public DataTable ObtieneFacturasAprobacionASC(string sNumDoc, string sFechaDesde, string sFechaHasta, string sTipoMtto, string sFlota)
        {
            try
            {

                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ASC_ConsultaFacturasAprobacion]", "@NumDoc", sNumDoc,
                                                                                                "@FechaDesde", sFechaDesde,
                                                                                                "@FechaHasta", sFechaHasta,
                                                                                                "@TipoMtto", sTipoMtto,
                                                                                                "@Flota", sFlota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaFacturasDetalle(List<ParticionesFacturaASC> oLstCon) 
        {
            try
            {
                int iLineNum = 0;
                foreach (ParticionesFacturaASC oCon in oLstCon)
                {
                    object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_ASC_InsertaDetalleFactura]", "@IdDetalle", oCon.iDetalle,
                                                                                                                         "@DocEntry", oCon.iIDocEntry,
                                                                                                                         "@CentroCD", oCon.sCentroCostos,
                                                                                                                         "@StatusD", oCon.iStatus,
                                                                                                                         "@LineNum", iLineNum,
                                                                                                                         "@Importe", oCon.dImporte,
                                                                                                                         "@ItemCode", oCon.sItemCode);
                    iLineNum += 1;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Consulta Detalle de Facturas(Particiones)
        public DataTable ObtieneFacturasDetalleASC(string sDocEntry)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ASC_ConsultaDetalleFacturas]", "@DocEntry", sDocEntry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Actualiza Facturas de Aprobacion
        public bool DBActualizaFacturasAprobacion(int iDocEntry, string sUser)
        {
            try
            {
                object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ASC_ActualizaFacturas]", "@DocEntry", iDocEntry,
                                                                                                                 "@User", sUser);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBInsertaFacturasHeader(FacturaAprobacionASC oFac, int iEstatus) 
        {
            try
            {
                bool ban = false;
                int iIdentity = 0;
                
                object oRes = new DBIntegrator().oDB_SP.EjecutarValor("[Principales].[spI_DI_InsertaDatosHeaderFacturaASC]", "@Empresa", oFac.sEmpresa,
                                                                                                                             "@Sucursal", oFac.sSucursal,
                                                                                                                             "@Fecha", oFac.dtFecha,
                                                                                                                             "@Empleado", oFac.sEmpleado,
                                                                                                                             "@Referencia", oFac.sNoFactura,
                                                                                                                             "@TipoCambio", oFac.dTipoCambio,
                                                                                                                             "@TipoMtto", oFac.iTipoMtto,
                                                                                                                             "@Comentarios", oFac.sComentarios,
                                                                                                                             "@Serie", oFac.sSerie,
                                                                                                                             "@Moneda", oFac.sMoneda,
                                                                                                                             "@Descuento", oFac.dDescuento,
                                                                                                                             "@Estatus", iEstatus,
                                                                                                                             "@FormaPago", oFac.sFormaPago,
                                                                                                                             "@FechaExp", oFac.dtFechaExp,
                                                                                                                             "@FechaImp", oFac.dtFechaImp,
                                                                                                                             "@Msg", oFac.sMsg,
                                                                                                                             "@SapDoc", oFac.iDocSAP);

                //if (oFac.iDocSAP != 0)
                //{
                //    string sCardCode = "P00064";
                //    System.Data.DataTable dtProv = new DBAccesoSAP().DBGetObtieneDocRequeridosProveedor(sCardCode);

                //    if (dtProv.Rows[0]["U_REQXML"].S() == "SI")
                //    {
                //        //Inserción de datos XML a SAP
                //        string sCode = string.Empty;
                //        string sName = string.Empty;
                //        string sAccount = string.Empty;
                //        string sJrnalline = string.Empty;
                //        string sRemarks = string.Empty;
                //        string sUID = string.Empty;

                //        if (!string.IsNullOrEmpty(oFac.sUID) && !string.IsNullOrEmpty(oFac.iDocSAP.S()) && !string.IsNullOrEmpty(oFac.sRFC) && !string.IsNullOrEmpty(oFac.sMonto))
                //        {
                //            String[] sArrUID = oFac.sUID.S().Split('-');
                //            sUID = sArrUID[0].S() + "-" + sArrUID[1].S() + "-" + sArrUID[2].S() + "-" + sArrUID[3].S() + "-";

                //            sCode = oFac.iDocSAP + "_" + sUID;
                //            sName = oFac.iDocSAP + "_" + sUID;
                //            sAccount = "";
                //            sJrnalline = "0";
                //            sRemarks = "Factura de:" + dtProv.Rows[0]["CardName"].S();

                //            string sResult = "INSERT INTO Aerolineas_Ejecutivas.[dbo].[@BXP_ACTPCH](Code, Name, U_BXP_DOCENTRY, U_BXP_UUID,U_BXP_RFC, U_BXP_AMOUNT, U_BXP_ACCOUNT, U_BXP_JRNALLINE, U_BXP_REMARKS) " +
                //                             "VALUES('" + sCode + "','" + sName + "','" + oFac.iDocSAP + "','" + oFac.sUID + "', '" + oFac.sRFC + "'," + oFac.sMonto.D() + ",'" + sAccount + "','" + sJrnalline + "','" + sRemarks + "')";

                //            new DBBaseSAP().oDB_SP.EjecutarQuery(sResult);
                //        }
                //        //Fin de Inserción de datos XML a SAP
                //    }
                //}

                if (oRes != null) 
                {
                    iIdentity = oRes.S().I();
                }

                //return oRes != null ? true : false;
                return iIdentity;
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        public bool DBSetInsertaFacturaDetalleASC(List<ConceptosFacturaAprobASC> oLstCn, int iIdentity)
        {
            try
            {
                foreach (ConceptosFacturaAprobASC oCon in oLstCn)
                {
                    object oRes = new DBIntegrator().oDB_SP.EjecutarValor("[Principales].[spI_DI_InsertaDatosDetalleFacturaASC]", "@IdFactura", iIdentity,
                                                                                                                                  "@Empresa", oCon.sEmpresa,
                                                                                                                                  "@Linea", oCon.iLinea,
                                                                                                                                  "@Item", oCon.sItem,
                                                                                                                                  "@DescripcionUsuario", oCon.sDescripcionUsuario,
                                                                                                                                  "@Cantidad", oCon.dCantidad,
                                                                                                                                  "@Precio", oCon.dPrecio,
                                                                                                                                  "@Descuento", oCon.dDescuento,
                                                                                                                                  "@Impuesto", oCon.iImpuesto,
                                                                                                                                  "@CodigoImpuesto", oCon.sCodigoImpuesto,
                                                                                                                                  "@TotalLinea", oCon.dTotalLinea,
                                                                                                                                  "@Cuenta", oCon.sCuenta,
                                                                                                                                  "@Almacen", oCon.sAlmacen,
                                                                                                                                  "@Proyecto", oCon.sProyecto,
                                                                                                                                  "@Dimension1", oCon.sDimension1,
                                                                                                                                  "@Dimension2", oCon.sDimension2,
                                                                                                                                  "@Dimension3", oCon.sDimension3,
                                                                                                                                  "@Dimension4", oCon.sDimension4,
                                                                                                                                  "@Dimension5", oCon.sDimension5,
                                                                                                                                  "@XML", oCon.sXML,
                                                                                                                                  "@PDF", oCon.sPDF);
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Agregar Articulo y actualiza articulo del cual tomamos el importe original
        public bool DBInsertaArticulo(int idDetalle, int iDocEntry, string sItemCode, string sDesArticulo, string sImporte)
        {
            try
            {
                object oRes = new DBBase().oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_ASC_InsertaArticuloDetalle]", "@IdParticion", idDetalle,
                                                                                                                      "@DocEntry", iDocEntry,
                                                                                                                      "@ItemCode", sItemCode,
                                                                                                                      "@DesArticulo", sDesArticulo,
                                                                                                                      "@Importe", sImporte.D());
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable DBGetObtieneCodigoArticulo(string sArticulo, int iTipoMtto)
        {
            try
            {
                string sResult = "SELECT TOP 1 CodigoArticulo FROM [ClientesCasa].[tbp_CC_Articulos] WHERE Concepto LIKE '%" + sArticulo + "%' AND TipoMtto = " + iTipoMtto + "";
                return new DBBase().oDB_SP.EjecutarDT_DeQuery(sResult);
                //return oDB_SP.EjecutarDT_DeQuery(sResult);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}