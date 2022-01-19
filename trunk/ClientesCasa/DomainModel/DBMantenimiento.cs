using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ClientesCasa.Clases;
using ClientesCasa.Objetos;
using System.IO;
using System.Threading.Tasks;

namespace ClientesCasa.DomainModel
{
    public class DBMantenimiento : DBBase
    {
        public DataTable DBGetObtieneGastosDeSAP(object[] oArrGastos)
        {
            try
            {
                string sMatricula = oArrGastos[5].S();
                string sCuentas = oArrGastos[7].S();
                return new DBIntegrator().oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaGastosPorMatricula]", "@MES", oArrGastos[1].S().I(),
                                                                                                                    "@ANIO", oArrGastos[3].S().I(),
                                                                                                                    "@MAT", sMatricula,
                                                                                                                    "@Cuentas", sCuentas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaGastosDeSAP(DataRow row)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaGastosDesdeSAP]", "@Referencia", row["Referencia"].S(),
                                                                                                    "@Matricula", row["Matricula"].S(),
                                                                                                    "@Importe", row["Importe"].S().D(),
                                                                                                    "@ImporteOriginal", row["ImporteModificado"].S().D(),
                                                                                                    "@Moneda", row["Moneda"].S(),
                                                                                                    "@IdRubro", row["IdRubro"].S().I(),
                                                                                                    "@IdTipoGasto", row["IdTipoGasto"].S().I(),
                                                                                                    "@Mes", row["Mes"].S().I(),
                                                                                                    "@Anio", row["Anio"].S().I(),
                                                                                                    "@Cuenta", row["Cuenta"].S(),
                                                                                                    "@NumeroTrip", row["NumeroTrip"].S().I(),
                                                                                                    "@NumeroPierna", row["NumeroPierna"].S().I(),
                                                                                                    "@TipodeGasto", row["TipoGasto"].S(),
                                                                                                    "@AmpliadoGasto", row["AmpliadoGasto"].S(),
                                                                                                    "@Concur", row["Concur"].S().I(),
                                                                                                    "@BaseRef", row["BaseRef"].S().I(),
                                                                                                    "@FechaVueloOpe", row["FechaOpe"].S(),
                                                                                                    "@Proveedor", row["Proveedor"].S(),
                                                                                                    "@UsuarioCreacion", Utils.GetUser);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneRubros
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaRubros]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBGetConsultaTiposGasto
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaTiposGasto]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataSet DBGetObtieneGastosMXNUSD(int iMes, int iAnio, string sMatricula)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaGastosRubroMatricula]", "@Matricula", sMatricula,
                                                                                                    "@Mes", iMes,
                                                                                                    "@Anio", iAnio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneImportesPorGastosYContrato(long iIdGasto, string sUsuario)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaImportesPorGasto]",    "@IdGasto", iIdGasto,
                                                                                                "@Usuario", sUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneImportesTodos_Gastos(string sMatricula, int iAnio, int iMes)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaImportesTODOSGastos]", "@Matricula", sMatricula,
                                                                                                "@Anio", iAnio,
                                                                                                "@Mes", iMes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaGastos(GastoEstimado oGasto)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaMontosGastos]", "@IdGasto", oGasto.iIdGasto,
                                                                                                  "@Importe", oGasto.dImporte,
                                                                                                  "@NumeroTrip", oGasto.iNumeroTrip,
                                                                                                  "@UsuarioModificacion", oGasto.sUsuario,
                                                                                                  "@NumeroPierna", oGasto.iNumeroPierna,
                                                                                                  "@IdRubro", oGasto.iIdRubro,
                                                                                                  "@TipodeGasto", oGasto.sTipoGasto,
                                                                                                  "@AmpliadoGasto1", oGasto.sAmpliadoGasto,
                                                                                                  "@FechaVueloOpe", oGasto.sFechaVueloOpe,
                                                                                                  "@IdTipoRubro", oGasto.iIdTipoRubro,
                                                                                                  "@Comentarios", oGasto.sComentarios,
                                                                                                  "@Proveedor", oGasto.sProveedor);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DBSetInsertaGastos
        public void DBSetInsertaImportesContratoGastos(List<MantenimientoGastos> oGastos)
        {
            try
            {
                
                foreach (MantenimientoGastos oGasto in oGastos)
                {
                    oDB_SP.EjecutarSP("[ClientesCasa].[spI_CC_InsertaImporteContratosGasto]", "@IdGasto", oGasto.iIdGasto,
                                                                                                "@Importe", oGasto.dImporte,
                                                                                                "@ClaveContrato", oGasto.sContrato,
                                                                                                "@Porcentaje", oGasto.iPorcentaje,
                                                                                                "@UsuarioModificacion", oGasto.sUsuario);
                    
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaAcumuladosGastos(string sDescripcionG)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaAcumuladosGastos]", "@DescripcionConcepto", sDescripcionG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaPiernasPorMatriculaFecha(string sMatricula, DateTime dtFecha)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaPiernasPorMatriculaFecha]", "@Matricula", sMatricula,
                                                                                                    "@Fecha", dtFecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaGastoEstimado(GastoEstimado oGasto)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaGastoEstimado]", "@Referencia", oGasto.sNoReferencia,
                                                                                                  "@Matricula", oGasto.sMatricula,
                                                                                                  "@CentroCostos", oGasto.sCentroCostos,
                                                                                                  "@Importe", oGasto.dImporte,
                                                                                                  "@TipoMoneda", oGasto.sTipoMoneda,
                                                                                                  "@IdRubro", oGasto.iIdRubro,
                                                                                                  "@Proveedor", oGasto.sProveedor,
                                                                                                  "@Mes", oGasto.iMes,
                                                                                                  "@Anio", oGasto.iAnio,
                                                                                                  "@NumeroTrip", oGasto.iNumeroTrip,
                                                                                                  "@UsuarioCreacion", oGasto.sUsuario);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetEliminaGastoEstimado(long iIdGasto)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaGastosEstimados]", "@IdGasto", iIdGasto);

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaBanderaComprobanteGasto(DataTable dt)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    string sRuta = ArmaRutaComprobante(row["Mes"].S(), row["Anio"].S(), row["Matricula"].S(), row["Moneda"].S());
                    string sRutaFinal = sRuta.Replace("\\", @"\");
                    if (File.Exists(sRutaFinal + row["Referencia"].S() + ".pdf"))
                        DBSetActualizaBanderaComprobanteGasto(row["IdGasto"].S().I(),1);
                    else
                        DBSetActualizaBanderaComprobanteGasto(row["IdGasto"].S().I(), 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DBSetActualizaBanderaComprobanteGasto(int iIdGasto, int iComprobante)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaBanderaComprobante]", "@IdGasto", iIdGasto,
                                                                                                            "@Comprobante", iComprobante);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ArmaRutaComprobante(string sMes, string sAnio, string sMatriculaRef, string sMoneda)
        {
            try
            {
                string sRuta = string.Empty;
                sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                
                sRuta = sRuta.S().Replace("\\", "\\\\");
                sRuta = sRuta.Replace("[anio]", sAnio.S());
                sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(sMes.S().I()));
                string sMon = sMoneda == "MXN" ? "MN" : "USD";
                sRuta = sRuta.Replace("[moneda]", sMon);
            
                return sRuta;
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
                    sMes = "01 ENERO";
                    break;
                case 2:
                    sMes = "02 FEBRERO";
                    break;
                case 3:
                    sMes = "03 MARZO";
                    break;
                case 4:
                    sMes = "04 ABRIL";
                    break;
                case 5:
                    sMes = "05 MAYO";
                    break;
                case 6:
                    sMes = "06 JUNIO";
                    break;
                case 7:
                    sMes = "07 JULIO";
                    break;
                case 8:
                    sMes = "08 AGOSTO";
                    break;
                case 9:
                    sMes = "09 SEPTIEMBRE";
                    break;
                case 10:
                    sMes = "10 OCTUBRE";
                    break;
                case 11:
                    sMes = "11 NOVIEMBRE";
                    break;
                case 12:
                    sMes = "12 DICIEMBRE";
                    break;
            }

            return sMes;
        }

        public DataTable DBGetObtieneProveedores()
        {
            try
            {
                DataTable dtProvSAP = new DBBaseSAP().oDB_SP.EjecutarDT("[dbo].[SP_GET_OBTIENE_SOCIOSNEGOCIO]");
                DataTable dtProved = oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaProveedor]");

                dtProvSAP.Merge(dtProved);

                DataView dtV = dtProvSAP.DefaultView;
                dtV.Sort = "Descripcion ASC";

                return dtV.ToTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}