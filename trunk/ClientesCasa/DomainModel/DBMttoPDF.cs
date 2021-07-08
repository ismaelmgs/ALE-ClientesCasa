using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ClientesCasa.Clases;
using ClientesCasa.Objetos;
namespace ClientesCasa.DomainModel
{
    public class DBMttoPDF : DBBase
    {
        //Obtiene informaci´`on de poliza
        public DataSet DBGetInfoPoliza(string sReferencia)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaInfoPolizas]", "@Referencia", sReferencia);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetDetalleReferencia(string sReferencia)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ObtieneInformacionReferenciaGasto]", "@Referencia", sReferencia);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}