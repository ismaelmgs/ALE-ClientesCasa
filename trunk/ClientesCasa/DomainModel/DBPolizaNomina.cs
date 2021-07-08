using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using System.Globalization;
using System.Data.SqlClient;

namespace ClientesCasa.DomainModel
{
    public class DBPolizaNomina : DBIntegrator
    {
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

        public int DBInsertaRegistrosHeader(PolizaNominaNew oPolNom) 
        {
            try
            {
                object oRes = new DBIntegrator().oDB_SP.EjecutarValor("[Principales].[spI_PN_InsertaHeaderPolizaNomina]", "@Usuario", oPolNom.sUsuario);
                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneSiPeriodoABACOyaExiste
        {
            get
            {
                try
                {
                    return new DBIntegrator().oDB_SP.EjecutarDT("[Principales].[spS_PN_ConsultaPeriodoExtraidoDeABACO]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DBInsertaPolizaNominaDetalle(List<DetallePolizaNominaNew> oLstDetails, long lgFolio) 
        {
            try
            {
                foreach (DetallePolizaNominaNew oDet in oLstDetails)
                {
                    object oRes = new DBIntegrator().oDB_SP.EjecutarValor("[Principales].[spI_PN_InsertaDetailPolizaNomina]", "@IdFolio", lgFolio,
                                                                                                                                "@IdCounter", oDet.IdCounter,
                                                                                                                                "@EmpresaFacturante", oDet.EmpresaFacturante,
                                                                                                                                "@RFCEmpresaFacturante", oDet.RFCEmpresaFacturante,
                                                                                                                                "@Periodo", oDet.Periodo,
                                                                                                                                "@NombreEmpleado", oDet.NombreEmpleado,
                                                                                                                                "@RFCEmpleado", oDet.RFCEmpleado,
                                                                                                                                "@SalarioMensualBruto", oDet.SalarioMensualBruto,
                                                                                                                                "@Vales", oDet.Vales,
                                                                                                                                "@HorasExtrasDobles", oDet.HorasExtrasDobles,
                                                                                                                                "@HorasExtrasTriples", oDet.HorasExtrasTriples,
                                                                                                                                "@PagoPorIncapacidad", oDet.PagoPorIncapacidad,
                                                                                                                                "@Compensacion", oDet.Compensacion,
                                                                                                                                "@DescuentoPorFaltas", oDet.DescuentoPorFaltas,
                                                                                                                                "@DiasLaborados", oDet.DiasLaborados,
                                                                                                                                "@Retroactivo", oDet.Retroactivo,
                                                                                                                                "@DiasFestivos", oDet.DiasFestivos,
                                                                                                                                "@SalarioDiarioR", oDet.SalarioDiarioR,
                                                                                                                                "@PrimaDominical", oDet.PrimaDominical,
                                                                                                                                "@AguinaldoReal", oDet.AguinaldoReal,
                                                                                                                                "@PrimaVacacional", oDet.PrimaVacacional,
                                                                                                                                "@PrimaAntiguedad", oDet.PrimaAntiguedad,
                                                                                                                                "@Bono", oDet.Bono,
                                                                                                                                "@TotalIngresos", oDet.TotalIngresos,
                                                                                                                                "@SalarioDiarioFiscal", oDet.SalarioDiarioR,
                                                                                                                                "@SalarioIntegrado", oDet.SalarioIntegrado,
                                                                                                                                "@Sueldo", oDet.Sueldo,
                                                                                                                                "@SeptimoDia", oDet.SeptimoDia,
                                                                                                                                "@Horasextras", oDet.HorasExtras,
                                                                                                                                "@Destajos", oDet.Destajos,
                                                                                                                                "@VacacionesATiempo", oDet.VacacionesATiempo,
                                                                                                                                "@PrimaVacacionalReportada", oDet.PrimaVacacionalReportada,
                                                                                                                                "@Aguinaldo", oDet.Aguinaldo,
                                                                                                                                "@ValesGratificacion", oDet.ValesGratificacion,
                                                                                                                                "@MinimoVital", oDet.MinimoVital,
                                                                                                                                "@OtrasPercepciones", oDet.OtrasPercepciones,
                                                                                                                                "@TotalPercepciones", oDet.TotalPercepciones,
                                                                                                                                "@RetiroInvalidezYvida", oDet.RetiroInvalidezYVida,
                                                                                                                                "@RetencionCesantia", oDet.RetencionCesantia,
                                                                                                                                "@RetiroEnfermedadYMaternidad", oDet.RetiroEnfermedadYMaternidad,
                                                                                                                                "@SeguroViviendaInfonavit", oDet.SeguroViviendaInfonavit,
                                                                                                                                "@SubsidioEmpleoAcreditado", oDet.SubsidioEmpleoAcreditado,
                                                                                                                                "@SubsidioEmpleoSP", oDet.SubsidioEmpleoSP,
                                                                                                                                "@ISRAntesDeSubsidioEmpleo", oDet.ISRAntesDeSubsidioEmpleo,
                                                                                                                                "@ISRSP", oDet.ISRSP,
                                                                                                                                "@IMSS", oDet.IMSS,
                                                                                                                                "@PrestamoInfonavit", oDet.PrestamoInfonavit,
                                                                                                                                "@AjusteNeto", oDet.AjusteNeto,
                                                                                                                                "@PensionAlimenticia", oDet.PensionAlimenticia,
                                                                                                                                "@OtrasDeducciones", oDet.OtrasDeducciones,
                                                                                                                                "@TotalDeduccion", oDet.TotalDeducciones,
                                                                                                                                "@Neto", oDet.Neto,
                                                                                                                                "@RecuperacionPrestamo", oDet.RecuperacionPrestamo,
                                                                                                                                "@SaldoRecuperacionPrestamo", oDet.SaldoRecuperacionPrestamo,
                                                                                                                                "@PrestamoEmpresa", oDet.PrestamoEmpresa,
                                                                                                                                "@SaldoPrestamoEmpresa", oDet.SaldoPrestamoEmpresa,
                                                                                                                                "@InvalidezYVida", oDet.InvalidezYVida,
                                                                                                                                "@CesantiaYVejez", oDet.CesantiaYVejez,
                                                                                                                                "@EnfermedadYMaternidadPadron", oDet.EnfermedadYMaternidadPatron,
                                                                                                                                "@GastosPensionados", oDet.GastosPensionados,
                                                                                                                                "@FondoRetiroSAR2", oDet.FondoRetiroSAR2,
                                                                                                                                "@ImpuestoEstatal3", oDet.ImpuestoEstatal3,
                                                                                                                                "@RiesgoDeTrabajo", oDet.RiesgoDeTrabajo,
                                                                                                                                "@IMSSEmpresa", oDet.IMSSEmpresa,
                                                                                                                                "@InfonavitEmpresa", oDet.InfonavitEmpresa,
                                                                                                                                "@GuarderiaIMSS", oDet.GuarderiaIMSS,
                                                                                                                                "@OtrasObligaciones", oDet.OtrasObligaciones,
                                                                                                                                "@TotalObligaciones", oDet.TotalObligaciones,
                                                                                                                                "@SueldoIMSS", oDet.SueldoIMSS,
                                                                                                                                "@MV", oDet.MV,
                                                                                                                                "@CargaPatronal", oDet.CargaPatronal,
                                                                                                                                "@MarkUpPatronal", oDet.MarkUpPatronal,
                                                                                                                                "@MarkUpMV", oDet.MarkUpMV,
                                                                                                                                "@ISN", oDet.ISN,
                                                                                                                                "@SUBSIDIOCHEQUECERTIVA", oDet.SUBSIDIOCHEQUECERTIVA,
                                                                                                                                "@CostoEmpleado", oDet.CostoEmpleado,
                                                                                                                                "@fill", oDet.fill,
                                                                                                                                "@Importe", oDet.Importe,
                                                                                                                                "@IVA", oDet.IVA,
                                                                                                                                "@SubTotalFactura", oDet.SubTotalFactura,
                                                                                                                                "@Retencion", oDet.Retencion,
                                                                                                                                "@TotalFactura", oDet.TotalFactura,
                                                                                                                                "@NumeroFactura", oDet.NumeroFactura
                                                                                                                         );
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable DBGetObtieneExistenciaRFC(string sRFC)
        {
            try
            {
                string sResult = "SELECT COUNT(1) RFC FROM [Aerolineas_Ejecutivas].[dbo].[ALE_RH] WHERE RFC = '" + sRFC + "'";
                return new DBBaseSAP().oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBSetEliminaNominaABACO(int iIdFolio)
        {
            try
            {
                DataTable dtRes = oDB_SP.EjecutarDT("[Principales].[spS_PN_EliminaNominaABACO]", "@IdFolio", iIdFolio);
                return dtRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}