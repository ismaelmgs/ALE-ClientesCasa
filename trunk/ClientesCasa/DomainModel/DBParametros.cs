using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using NucleoBase.Core;
using System.Data;
using ClientesCasa.Clases;
using ClientesCasa.Objetos;

namespace ClientesCasa.DomainModel
{
    public class DBParametros : DBBase
    {
        public string ObtieneParametroPorClave(string sClave)
        {
            try
            {
                string sResult = string.Empty;
                DataTable dt = new DataTable();

                dt = oDB_SP.EjecutarDT_DeQuery("[ClientesCasa].[spS_CC_ConsultaCodigoProveedores]", "@Empresa", sClave);

                if (dt != null)
                    if (dt.Rows.Count > 0)
                        sResult = dt.Rows[0][0].S();
                return sResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}