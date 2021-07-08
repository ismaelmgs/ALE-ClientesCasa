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
    public class DBMttoPagos : DBBase
    {
        public DataTable DBGetObtienePagosFacturante(string sClaveCliente, int iMes, int iAnio)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ObtienePagosCliente]", "@ClaveCliente", sClaveCliente,
                                                                                            "@Mes", iMes,
                                                                                            "@Anio", iAnio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneFacturantesCliente(string sClaveCliente)
        {
            try
            {
                string sQuery = "SELECT CardCode, CardName, U_CLICORP FROM OCRD(NOLOCK) " +
                                " WHERE U_CLICORP = '" + sClaveCliente + "' ";

                return new DBBaseSAP().oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetActualizaListaPagos(string sClaveCliente, int iAnio, int iMes)
        {
            try
            {
                bool ban = true;

                string sQuery = "DECLARE @Anio	INT, " +
                                        "@Mes   INT, " +
                                        "@Cliente    VARCHAR(20) " +
                                            
                                "SET @Anio = " + iAnio.S() +
                                "SET @Mes = " + iMes.S() +
                                "SET @Cliente = '" + sClaveCliente.S() + "' " +

                                "SELECT " +
                                    "cl.CardCode, " +
                                    "cl.CardName, " +
                                    "p.DocNum, " +
                                    "p.TrsfrDate, " +
                                    "CASE WHEN p.DocCurr = 'MXN' THEN p.DocTotal " +
                                        "ELSE DocTotalFC " +
                                    "END DocTotal," +
                                    "p.DocCurr " +
                                "FROM ORCT p " +
                                "INNER JOIN(SELECT CardCode, CardName FROM OCRD(NOLOCK) " +
                                            "WHERE U_CLICORP = @Cliente) cl ON p.CardCode = cl.CardCode " +
                                    "AND YEAR(DocDate) = @Anio " +
                                    "AND MONTH(DocDate) = @Mes";

                DataTable dtRes = new DBBaseSAP().oDB_SP.EjecutarDT_DeQuery(sQuery);

                foreach (DataRow row in dtRes.Rows)
                {
                    object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaPagosCliente]", "@ClaveCliente", sClaveCliente,
                                                                    "@ClaveContrato", sClaveCliente,
                                                                    "@ClaveFacturante", row.S("CardCode"),
                                                                    "@Facturante", row.S("CardName"),
                                                                    "@ImporteNuevo", row.S("DocTotal").D(),
                                                                    "@ImporteOriginal", row.S("DocTotal").D(),
                                                                    "@IdTipoPago", 1,
                                                                    "@Mes", iMes,
                                                                    "@Anio", iAnio,
                                                                    "@DocNum", row.S("DocNum").I(),
                                                                    "@DocDate", row.S("TrsfrDate").Dt(),
                                                                    "@Moneda", row.S("DocCurr"),
                                                                    "@UsuarioCreacion", Utils.GetUser);
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetFacturantes(string sCliente)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT CardCode cust_num, CardCode, CardName, CardType, GroupCode, CmpPrivate, ";
                sCad += "Address, ZipCode, MailAddres FROM OCRD(NOLOCK) WHERE U_CLICORP = '" + sCliente + "'";

                return new DBBaseSAP().oDB_SP.EjecutarDT_DeQuery(sCad);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetNombreFacturante(string sFacturante)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT CardCode cust_num, CardCode, CardName, CardType, GroupCode, CmpPrivate, ";
                sCad += "Address, ZipCode, MailAddres FROM OCRD(NOLOCK) WHERE CardCode = '" + sFacturante + "'";

                return new DBBaseSAP().oDB_SP.EjecutarDT_DeQuery(sCad);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DBSetEliminaPagoEstimado(int iIdPago)
        {
            try
            {
                oDB_SP.EjecutarSP("[ClientesCasa].[spD_CC_EliminaPagoEstimado]", "@IdPago", iIdPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetActualizaMontosPagos(int iIdPago, decimal dMonto)
        {
            try
            {
                oDB_SP.EjecutarSP("[ClientesCasa].[spU_CC_ActualizaPago]", "@IdPago", iIdPago,
                                                                            "@ImporteNuevo", dMonto,
                                                                            "@Usuario", Utils.GetUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaPagoEstimado(PagoEstimado oPago)
        {
            try
            {
                bool ban = true;

                oPago.sFacturante = DBGetNombreFacturante(oPago.sClaveFacturante).Rows[0]["CardName"].S();

                oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaPagoEstimado]", "@ClaveCliente", oPago.sClaveCliente,
                                                                                    "@ClaveContrato", oPago.sClaveContrato,
                                                                                    "@ClaveFacturante", oPago.sClaveFacturante,
                                                                                    "@Facturante", oPago.sFacturante,
                                                                                    "@ImporteNuevo", oPago.dImporteNvo,
                                                                                    "@Mes", oPago.iMes,
                                                                                    "@Anio", oPago.iAnio,
                                                                                    "@DocNum", oPago.sDocNum,
                                                                                    "@DocDate", oPago.dtDocDate,
                                                                                    "@Moneda", oPago.sMoneda,
                                                                                    "@UsuarioCreacion", oPago.sUsuario);

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}