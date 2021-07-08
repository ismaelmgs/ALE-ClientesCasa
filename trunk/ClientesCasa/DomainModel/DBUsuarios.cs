using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBUsuarios : DBBase
    {
        public bool DBEXisteUsuarioActivo(string sUser)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Seguridad].[spS_ConsultaUsuarioActivo]", "@User", sUser);
                return oRes.S() == "1" ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetUsuario(string sUser)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_MXJ_ConsultaUsuarioActivo]", "@Usuario", sUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}