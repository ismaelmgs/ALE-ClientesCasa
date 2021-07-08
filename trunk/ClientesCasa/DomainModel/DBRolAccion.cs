using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBRolAccion : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRolAccion]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}