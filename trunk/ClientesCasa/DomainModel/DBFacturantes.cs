using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ClientesCasa.Clases;

namespace ClientesCasa.DomainModel
{
    public class DBFacturantes : DBBase
    {
        public DataTable DBGetObtieneFacturantesPorcliente(object[] oArray)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "DECLARE @ClaveCliente VARCHAR(50), @NombreCliente VARCHAR(50) ";
                sCad += "SET @ClaveCliente = '" + oArray[0].S() + "' ";
                sCad += "SET @NombreCliente = '" + oArray[1].S() + "' ";
                sCad += "IF(@ClaveCliente = '') SET @ClaveCliente = NULL IF(@NombreCliente = '') SET @NombreCliente = NULL ";
                sCad += "SELECT CardCode cust_num, CardCode, CardName, CardType, GroupCode, CmpPrivate, Address, ZipCode, MailAddres, U_CLICORP ";
                sCad += "FROM OCRD(NOLOCK) WHERE 1 = 1 AND GroupCode = 110 AND (@ClaveCliente IS NULL OR  U_CLICORP LIKE '%' + @ClaveCliente + '%') AND (@NombreCliente IS NULL OR CardName like '%'+ @NombreCliente + '%')";

                return new DBAccesoSAP().oDB_SP.EjecutarDT_DeQuery(sCad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtieneContratosPorCusNum(string sCustNum)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaMatriculasPorCustNum]", "@Cust_num", sCustNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertaContratoAFacturante(string sCustNum, string sClaveContrato)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaContratoAFacturante]", "@cust_num", sCustNum,
                                                                                                        "@ClaveContrato", sClaveContrato,
                                                                                                        "@UsuarioCreacion", Utils.GetUser);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminaContratosDelFacturante(string sCustNum)
        {
            try
            {
                oDB_SP.EjecutarSP("[ClientesCasa].[spD_CC_EliminaContratosDeFacturante]", "@Cust_num", sCustNum);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminaUnContratoAsociadoPorID(int iIdFacturante)
        {
            try
            {
                oDB_SP.EjecutarSP("[ClientesCasa].[spD_CC_EliminaContratosDeFacturantePorID]", "@IdFacturante", iIdFacturante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}