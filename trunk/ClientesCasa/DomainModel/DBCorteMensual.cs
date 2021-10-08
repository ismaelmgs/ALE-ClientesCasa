using ClientesCasa.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBCorteMensual : DBBase
    {
        public string DBInsertaCorteMensualPorContrato(string sClaveContrato, int iMes, int iAnio)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_CC_GuardaCorteMensual]", "@ClaveContrato", sClaveContrato,
                                                                                                "@Mes", iMes,
                                                                                                "@Anio", iAnio,
                                                                                                "@Usuario", Utils.GetUser);

                return oRes.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}