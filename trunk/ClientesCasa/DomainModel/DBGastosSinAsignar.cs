using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBGastosSinAsignar : DBBase
    {
        public DataSet DBGetConsultaGastosSinAsignar(int iAnio, int iMes)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ObtieneFastosSinAsignar]", "@Anio", iAnio,
                                                                                            "@Mes", iMes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}