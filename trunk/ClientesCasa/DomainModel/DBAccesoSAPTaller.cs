using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using System.Globalization;
namespace ClientesCasa.DomainModel
{
    public class DBAccesoSAPTaller : DBBaseSAPTaller
    {
        public object GetValueByQuery(string sQ)
        {
            SAPbobsCOM.Recordset oRS = Utils.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            object sRes = null;

            try
            {
                oRS.DoQuery(sQ);
                if (oRS.RecordCount > 0)
                    sRes = oRS.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oRS != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRS);
                    oRS = null;
                    GC.Collect();
                }
            }

            return sRes;
        }
        public DataTable GetQuerySAP(string sQuery)
        {
            try
            {
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region MODULO DE ENVIO DE FACTURAS
        public DataTable DBGetObtieneFacturas(string sNumDoc, string sFechaDesde, string sFechaHasta)
        {
            try
            {
                //string sResult = "SELECT OP.DocEntry, OP.NumAtCard, OP.DocDate, OP.CardCode, OP.CardName, OP.DocTotal ";
                //sResult += "FROM [SBO_ASC].[dbo].[OPCH] OP ";

                //if (!string.IsNullOrEmpty(sNumDoc) || (!string.IsNullOrEmpty(sFechaDesde) && !string.IsNullOrEmpty(sFechaHasta)))
                //{
                //    sResult += "WHERE 1=1 ";
                //    sResult += "AND OP.DocEntry = '" + sNumDoc + "' ";
                //    sResult += "OR OP.DocDate BETWEEN '" + sFechaDesde + "' AND '" + sFechaHasta + "' ";
                //}
                //else
                //{
                //    sResult += "WHERE 1=1 ";
                //}
                //sResult += "ORDER BY OP.DocDate DESC";

                string sResult = "SELECT DocEntry,C.DocNum,C.NumAtCard,C.DocDate,C.DocTotal AS [ImporteMXN],C.DocTotalFC AS [ImporteUSD],C.DocCur ";
                sResult += ",C.DocRate,C.U_Matricula AS [Matricula],'' AS [Flota] ";
                sResult += ",(SELECT U_NAME FROM [SBO_ASC].[dbo].[OUSR] AS U WHERE C.UserSign=U.USERID) AS [Usuario] ";
                sResult += "FROM [SBO_ASC].[dbo].[OINV] AS C ";

                if (!string.IsNullOrEmpty(sNumDoc) || (!string.IsNullOrEmpty(sFechaDesde) && !string.IsNullOrEmpty(sFechaHasta)))
                {
                    sResult += "WHERE 1=1 AND C.CardCode LIKE 'CN0016' ";
                    sResult += "AND C.CANCELED='N' ";
                    sResult += "AND C.DocType='I' ";
                    sResult += "AND C.DocEntry = '" + sNumDoc + "' ";
                    sResult += "OR C.DocDate BETWEEN '" + sFechaDesde + "' AND '" + sFechaHasta + "' ";
                }
                else
                {
                    sResult += "WHERE 1=1 AND C.CardCode LIKE 'CN0016' ";
                    sResult += "AND C.CANCELED='N' ";
                    sResult += "AND C.DocType='I' ";
                }
                sResult += "ORDER BY C.DocDate DESC";

                return oDB_SP.EjecutarDT_DeQuery(sResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}