using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBNominaFin : DBIntegrator
    {
        public DataSet DBGetFacturasAprobadas(int iIdFolio)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_PN_ObtieneFacturasAprobadas]", "@IdFolio", iIdFolio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetActualizaEstatusFactura(int iIdFolio, string sEmpresa, int iNoFactura, int iEstatus)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_PN_ActualizaEstatusFactura]", "@IdFolio", iIdFolio,
                                                                                                        "@Empresa", sEmpresa,
                                                                                                        "@NoFactura", iNoFactura,
                                                                                                        "@Estatus", iEstatus);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetEliminaFactura(int iIdFolio, string sEmpresa, int iNoFactura)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spS_PN_DeleteFacturasAprobadas]", "@IdFolio", iIdFolio,
                                                                                                        "@Empresa", sEmpresa,
                                                                                                        "@NoFactura", iNoFactura);
                return oRes.S().I();
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public int DBSetActualizaResultadoDocSAP(int iIdFolio, string sEmpresa, int iNoFactura, int iDocSAP, string sMsgSAP)
        {
            try
            {
                try
                {
                    object oRes = oDB_SP.EjecutarValor("[Principales].[spI_PN_ActualizaEstatusDocSAPFactura]", "@IdFolio", iIdFolio,
                                                                                                                "@Empresa", sEmpresa,
                                                                                                                "@NoFactura", iNoFactura,
                                                                                                                "@MsgSAP", sMsgSAP,
                                                                                                                "@idSAP", iDocSAP);

                    return oRes.S().I();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex )
            {
                throw;
            }
        }
    }
}