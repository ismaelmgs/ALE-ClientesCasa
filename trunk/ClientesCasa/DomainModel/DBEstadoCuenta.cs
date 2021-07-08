using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBEstadoCuenta : DBBase
    {
        public DataSet DBGetConsultaEstadoCuenta(int iAnio, int iMes, string sMatricula)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ObtieneEstadoCuentaVersion1]", "@Anio", iAnio,
                                                                                        "@Mes", iMes,
                                                                                        "@Matricula", sMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}