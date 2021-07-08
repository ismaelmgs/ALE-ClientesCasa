using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBExtraccion : DBBaseSAP
    {
        public DataTable DBGetObtieneGastosPorPeriodo(int iMes, int iAnio, string sMatricula, string sMats)
        {
            try
            {
                DataTable dtC = ObtieneCuentasRubrosActivos;
                string sCadCuentas = string.Empty;
                string sQuery = string.Empty;
                foreach (DataRow row in dtC.Rows)
                {
                    if(sCadCuentas == string.Empty)
                        sCadCuentas = "'" + row["DescripcionCuenta"].S() + "'";
                    else
                        sCadCuentas += "," + "'" + row["DescripcionCuenta"].S() + "'";
                }

                sQuery = "SELECT " +
                            "AC.TransId,  " +
                            "CONVERT(VARCHAR(10),AC.RefDate,103)                                                                          'Fecha Contabilizacion',  " +
                            "CONVERT(VARCHAR(10),AC.DueDate,103)                                                                          'Fecha de Vencimiento',  " +
                            "CONVERT(VARCHAR(10),AC.CreateDate,103)                                                                       'Fecha de Documento',  " +
                            "(SELECT TOP 1 SeriesName FROM NNM1 WHERE ObjectCode = CONVERT(VARCHAR, AC.Series))	 'Serie',  " +
                            "AC.Ref2                                                                             'Referencia 2',  " +
                            "ACD.Account                                                                         'Cuenta de Mayor',  " +
                            "OAC.AcctName                                                                        'Nombre Cuenta de Mayor',  " +
                            "AC.Memo                                                                             'Comentarios',  " +
                            "ACD.ContraAct                                                                       'Cuenta contrapartida',  " +
                            "(SELECT CardName FROM OCRD(NOLOCK) WHERE CardCode = ACD.ContraAct)					 'Nombre Cuenta contrapartida',  " +
                            "ACD.Debit                                                                           'Cargo (ML)', " +
                            "'MXN'                                                                               'Cargo (ML)(moneda)', " +
                            "ACD.Credit                                                                          'Abono (ML)', " +
                            "'MXN'                                                                               'Abono (ML)(moneda)', " +
                            "CONVERT(DECIMAL(12, 2), (ACD.Debit - ACD.Credit))                                   'Saldo acumulado (ML)', " +
                            "'MXN'                                                                               'Saldo acumulado (ML)(moneda)', " +
                            "ACD.FCDebit                                                                         'Debito (ME)', " +
                            "'USD'                                                                               'Debito (ME)(moneda)', " +
                            "ACD.FCCredit                                                                        'Credito (ME)', " +
                            "'USD'                                                                               'Credito (ME)(moneda)', " +
                            "CONVERT(DECIMAL(12, 2), (ACD.FCDebit - ACD.FCCredit))                               'Saldo acumulado (ME)', " +
                            "'USD'                                                                               'Saldo acumulado (ME)(moneda)', " +
                            "CASE WHEN ISNULL(AC.TransCurr, '') = '' THEN CONVERT(DECIMAL(12,2),(ACD.Debit - ACD.Credit)) " +
                                "ELSE CONVERT(DECIMAL(12,2),(ACD.FCDebit - ACD.FCCredit)) " +
                            "END                                                                                 'IMPORTE', " +
                            "CASE WHEN ISNULL(AC.TransCurr, '') = '' THEN 1 " +
		                        "ELSE CONVERT(DECIMAL(12,2),(ACD.Debit - ACD.Credit) / CONVERT(DECIMAL(12, 2), CASE WHEN (ACD.FCDebit - ACD.FCCredit) = 0 THEN 1 " +
																							                        "ELSE (ACD.FCDebit - ACD.FCCredit) " +
																						                        "END)) " +
	                        "END                                                                                 'T/C',  " +
                            "CASE WHEN ISNULL(AC.TransCurr, '') = '' THEN 'MXN' " +
                                "ELSE 'USD' " +
                            "END                                                                                 'MONEDA', " +
                            "AC.Ref1                                                                             'Referencia 1', " +
                            "AC.Ref2                                                                             'Referencia 2', " +
                            "AC.Ref3                                                                             'Referencia 3', " +
                            "REPLICATE('0', 1) + CONVERT(VARCHAR, " + iMes.S() + ") + " +
                                "CONVERT(VARCHAR, " + iAnio.S() + ") + CONVERT(VARCHAR, AC.TransId)              'Reporte', " +
                            "CASE WHEN AC.Ref3 IN('EFECTIVO', 'AMEX') THEN AC.Ref2 " +
                                "ELSE '' " +
                            "END                                                                                 'CONCUR', " +
                            "ACD.OcrCode2                                                                        'Matricula' " +
                        "FROM OJDT AC " +
                        "INNER JOIN JDT1 ACD ON AC.TransId = ACD.TransId " +
                        "INNER JOIN OACT OAC ON ACD.Account = OAC.AcctCode " +
                        "WHERE YEAR(AC.RefDate) = " + iAnio.S() +
                            " AND MONTH(AC.RefDate) = " + iMes.S() +
                            " AND OAC.AcctCode in (" + sCadCuentas + ")";

                if (sMatricula != string.Empty && sMatricula != "(TODAS)")
                    sQuery += " AND ACD.OcrCode2 = '" + sMatricula + "'";
                else
                    sQuery += " AND ACD.OcrCode2 IN (" + sMats + ")";

                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ObtieneCuentasRubrosActivos
        {
            get
            {
                try
                {
                    return new DBBase().oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaCuentasDeGastos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBGetObtieneMatriculasCC
        {
            get
            {
                try
                {
                    return new DBBase().oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMatriculasCCActivas]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}