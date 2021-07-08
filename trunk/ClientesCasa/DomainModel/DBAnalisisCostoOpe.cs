using ClientesCasa.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBAnalisisCostoOpe : DBBase
    {

        public DataSet DBGetConsultaRubrosMatriculas(string sMatricula, int iMes, int iAnio)
        {
            try
            {

                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaDatosMantenimiento]", "@Matricula", sMatricula,
                                                                                               "@MONTH", iMes,
                                                                                               "@YEAR", iAnio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetConsultaEstadoCuenta(int iAnio, int iMes, string sMatricula)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ObtieneEstadoCuenta]", "@Anio", iAnio,
                                                                                        "@Mes", iMes,
                                                                                        "@Matricula", sMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetConsultaEstadoCuentaUSD(int iAnio, int iMes, string sMatricula)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ObtieneEstadoCuentaUSD]", "@Anio", iAnio,
                                                                                        "@Mes", iMes,
                                                                                        "@Matricula", sMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaTotalesCostoOperacion(object[] oParams, string sClaveContrato, string sMatricula, string sTipoMoneda, int iMes, int iAnio)
        {
            //decimal serviciosCargo, decimal Iva, decimal SubTotal, decimal GranTotal, decimal EstimadosFacturacion,decimal SaldoAnterior, decimal SaldoActual,
            try
            {
                oDB_SP.EjecutarSP("[ClientesCasa].[spI_CC_InsActTotalesCostoOperacion]", "@ServiciosCargo", oParams[0],
                                                                                        "@Iva", oParams[1],
                                                                                        "@SubTotal", oParams[2],
                                                                                        "@GranTotal", oParams[3],
                                                                                        "@EstimadosFacturacion", oParams[4],
                                                                                        "@SaldoAnterior", oParams[5],
                                                                                        "@SaldoActual", oParams[6],
                                                                                        "@ClaveContrato", sClaveContrato,
                                                                                        "@Matricula", sMatricula,
                                                                                        "@TipoMoneda", sTipoMoneda,
                                                                                        "@Mes", iMes,
                                                                                        "@Anio", iAnio,
                                                                                        "@Usuario", Utils.GetUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetTotalesEstadoCuenta(int iAnio, int iMes, string sMatricula, string sContrato, string sTipoMoneda)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaTotalesCostoOperacion]", "@ClaveContrato", sContrato,
                                                                                                "@Matricula", sMatricula,
                                                                                                "@Mes", iMes,
                                                                                                "@Anio", iAnio,
                                                                                                "@TipoMoneda", sTipoMoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneTotalesEdoCuenta(string sClaveContrato, int iAnio, int iMes)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaTotalesEstadoCuenta]", "@ClaveContrato", sClaveContrato,
                                                                                                "@Mes", iMes,
                                                                                                "@Anio", iAnio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}