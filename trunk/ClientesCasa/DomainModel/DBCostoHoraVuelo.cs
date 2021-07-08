using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBCostoHoraVuelo : DBBase
    {
        public DataSet DBGetObtieneGastosPorHrVuelo(DateTime dtInicio, DateTime dtFin, string sMoneda, string sMatricula, decimal dTipoCambio)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ObtieneCostoPorHoraVuelo]", "@FechaInicio", dtInicio,
                                                                                            "@FechaFin", dtFin,
                                                                                            "@Moneda", sMoneda,
                                                                                            "@Matricula", sMatricula,
                                                                                            "@TipoCambio", dTipoCambio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneTipoCambioPeriodo(DateTime dtInicio, DateTime dtFin)
        {
            try
            {
                string sFechaInicio = dtInicio.ToString("yyyy-MM-dd");
                string sFechaFin = dtFin.AddDays(1).ToString("yyyy-MM-dd");

                string sCad = "SELECT Rate FROM ORTT (NOLOCK) WHERE RateDate >= '" + sFechaInicio + " 00:00:00.000' AND RateDate < '" + sFechaFin + " 00:00:00.000' AND Currency = 'USD'";

                return new DBBaseSAP().oDB_SP.EjecutarDT_DeQuery(sCad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}