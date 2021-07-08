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
    public class DBDashboard : DBBase
    {
        //Obtiene contratos por vencer y contratos finalizados
        public DataSet DBGetContratos()
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaContratosPorVencer]");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Obtiene seguros de aeronaves
        public DataSet DBGetAeronaves()
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaSegurosAeronaves]");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Obtiene cantidad de gastos por matriculas
        public DataSet DBGetObtieneGastosSAP()
        {
            try
            {
                //Conjunto de tablas de Gastos SAP y Clientes Casa
                DataSet dsResult = new DataSet();
                //Gastos Clientes Casa
                DataSet dsCC = DBGetObtieneGastosMatriculas;
                DataTable dtExtraccion = new DataTable();
                DataTable dtGastosPendientes = new DataTable();
                DataTable dtCC = new DataTable();
                string sQuery = string.Empty;
                
                int iAnio = DateTime.Now.Year.S().I();
                int iMes = DateTime.Now.Month.S().I();

                dtGastosPendientes.Columns.Add("Matricula", typeof(string));
                dtGastosPendientes.Columns.Add("Pendientes", typeof(string));

                dsCC.Tables[0].Columns.Add("GastosSAP", typeof(string));

                //Extracción SAP
                sQuery = "SELECT ACD.OcrCode2 Matricula,COUNT(*) Gastos FROM OJDT AC " +
                                    "INNER JOIN JDT1 ACD ON AC.TransId = ACD.TransId " +
                                    "INNER JOIN OACT OAC ON ACD.Account = OAC.AcctCode " +
                                    "WHERE YEAR(AC.RefDate) = " + iAnio.ToString() +
                                    " AND MONTH(AC.RefDate) = 4" + //iMes.ToString() +
                                    " group by ACD.OcrCode2";
                //"ACD.OcrCode2 = '" + sMatricula + "'" +

                dtExtraccion = new DBBaseSAP().oDB_SP.EjecutarDT_DeQuery(sQuery);

                if (dtExtraccion != null)
                {
                    if (dtExtraccion.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExtraccion.Rows.Count; i++)
                        {
                            DataRow[] fRows;
                            string sMatricula = string.Empty;
                            int iGastosSAP = 0;
                            int iPendientes = 0;
                            int iGastosCC = 0;
                            bool bExiste = false; //Cuando la matricula no existe en Clientes Casa

                            sMatricula = dtExtraccion.Rows[i]["Matricula"].S();
                            iGastosSAP = dtExtraccion.Rows[i]["Gastos"].S().I();

                            fRows = dsCC.Tables[0].Select("Matricula='" + sMatricula + "'");

                            if(!string.IsNullOrEmpty(sMatricula))
                            {
                                DataRow row;

                                if (fRows.Length > 1)
                                {
                                    for (int x = 0; x < fRows.Length - 1; x++)
                                    {
                                        if (fRows[x]["Matricula"].S() == sMatricula)
                                        {
                                            //Agrega GastoSap a dsCC Clientes Casa
                                            fRows[x]["GastosSAP"] = iGastosSAP.S();
                                            iGastosCC = fRows[x]["GastosCC"].S().I();
                                            if (x == 1)
                                                fRows[x]["GastosCC"] = string.Empty;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int x = 0; x < fRows.Length; x++)
                                    {
                                        if (fRows[x]["Matricula"].S() == sMatricula)
                                        {
                                            //Agrega GastoSap a dsCC Clientes Casa
                                            fRows[x]["GastosSAP"] = iGastosSAP.S();
                                        }
                                    }
                                }

                                //Recorrido de Clientes Casa Matriculas
                                for (int z = 0; z <= dsCC.Tables[1].Rows.Count - 1; z++)
                                {
                                    if (dsCC.Tables[1].Rows[z]["Matricula"].S().Contains(sMatricula))
                                    {
                                        row = dtGastosPendientes.NewRow();

                                        if (iGastosCC == 0)
                                            iGastosCC = dsCC.Tables[1].Rows[z]["GastosCC"].S().I();

                                        iPendientes = iGastosSAP - iGastosCC;

                                        row["Matricula"] = sMatricula;
                                        row["Pendientes"] = iPendientes.S();
                                        dtGastosPendientes.Rows.Add(row);
                                        bExiste = true;
                                    }
                                }

                                if (bExiste == false)
                                {
                                    row = dtGastosPendientes.NewRow();
                                    row["Matricula"] = sMatricula;
                                    row["Pendientes"] = iGastosSAP.S();
                                    dtGastosPendientes.Rows.Add(row);
                                }
                            }
                        }
                        dtGastosPendientes.AcceptChanges();
                    }
                }
                dsCC.Tables[0].TableName = "ClientesAlDia";
                dsCC.Tables.Add(dtGastosPendientes);
                dsCC.Tables[2].TableName = "Pendientes";
                dsResult = dsCC.Copy();
                return dsResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet DBGetObtieneGastosMatriculas
        {
            get
            {
                try
                {
                    return new DBBase().oDB_SP.EjecutarDS_DeQuery("[ClientesCasa].[spS_CC_ConsultaGastosMatriculas]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //Obtiene Pendientes Generales Dashboard
        public DataSet DBGetPendientesGral()
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaPendientesDashboard]");
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////Obtiene matriculas activas
        //public DataTable DBGetObtieneMatriculasCC
        //{
        //    get
        //    {
        //        try
        //        {
        //            return new DBBase().oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMatriculasCCActivas]");
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

    }
}