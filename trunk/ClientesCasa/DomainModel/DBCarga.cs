using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ClientesCasa.Clases;

namespace ClientesCasa.DomainModel
{
    public class DBCarga : DBBase
    {
        public DataTable DBGetCodigoProveedor(string sEmpresa)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaCodigoProveedores]", "@Empresa", sEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetCodigoArticulo(string sServicio)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaCodigoArticulos]", "@Servicio", sServicio);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}