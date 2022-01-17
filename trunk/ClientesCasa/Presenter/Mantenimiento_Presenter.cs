using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ClientesCasa.Clases;
using ClientesCasa.Objetos;

namespace ClientesCasa.Presenter
{
    public class Mantenimiento_Presenter : BasePresenter<IViewMantenimiento>
    {
        private readonly DBMantenimiento oIGesCat;

        public Mantenimiento_Presenter(IViewMantenimiento oView, DBMantenimiento oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eUpaGastos += eUpaGastos_Presenter;
            oIView.eInsImpGasto += eInsImpGasto_Presenter;
            oIView.eSearchLegs += eSearchLegs_Presenter;
            oIView.eNewGastoEstimado += eNewGastoEstimado_Presenter;
            oIView.eUpaComprobante += eUpaComprobante_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = new DBContratos().DBGetObtieneClientesContratos(oIView.oArray);
            oIView.LLenaClientes(oIView.dtClientes);
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            object[] oArr = oIView.oArrGastos;
            Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene Rubros ---");
            oIView.dtRubros = oIGesCat.DBGetObtieneRubros;
            Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene Rubros --");
            Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene proveedores ---");
            oIView.dtProveedor = oIGesCat.DBGetObtieneProveedores();
            Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene Proveedores --");
            Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene Tipos de Gasto ---");
            oIView.dtTiposGasto = oIGesCat.DBGetConsultaTiposGasto;
            Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene Tipos de Gasto --");
            Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene Gastos MXN y USD ---");
            DataSet ds = ArmaTablasParaCargarMXNyUSD(oIGesCat.DBGetObtieneGastosMXNUSD(oArr[1].S().I(), oArr[3].S().I(), oArr[5].S()));
            Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene  Gastos MXN y USD --");
            Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO CargaGastos ---");
            oIView.CargaGastosMEXUSA(ds);
            Utils.GuardarBitacora("MANTTO_DATOS  --> FIN CargaGastos --");
        }
        protected void eUpaGastos_Presenter(object sender, EventArgs e)
        {
            object[] oArr = oIView.oArrGastos;
            oArr[7] = ArmaCadenaCuentasGastos();
            DataTable dtGastosSAP = oIGesCat.DBGetObtieneGastosDeSAP(oArr);
            
            foreach (DataRow row in dtGastosSAP.Rows)
            {
                if (!oIGesCat.DBSetInsertaGastosDeSAP(row))
                {
                    break;
                }
            }

            oIView.dtRubros = oIGesCat.DBGetObtieneRubros;
            oIView.dtTiposGasto = oIGesCat.DBGetConsultaTiposGasto;
            DataSet dsCon = ArmaTablasParaCargarMXNyUSD(oIGesCat.DBGetObtieneGastosMXNUSD(oArr[1].S().I(), oArr[3].S().I(), oArr[5].S()));
            
            oIView.CargaGastosMEXUSA(dsCon);
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            foreach (GastoEstimado oGastos in oIView.oLstGastoE)
            {
                oIGesCat.DBSetActualizaGastos(oGastos);
            }
        }
        protected void eInsImpGasto_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetInsertaImportesContratoGastos(oIView.oLstContratosGasto);
        }
        protected void eSearchLegs_Presenter(object sender, EventArgs e)
        {
            oIView.dtLegs = oIGesCat.DBGetConsultaPiernasPorMatriculaFecha(oIView.sMatricula, oIView.dtFechaVlo);

        }
        protected void eNewGastoEstimado_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetInsertaGastoEstimado(oIView.oGastoE);
        }
        private DataSet ArmaTablasParaCargarMXNyUSD(DataSet dsCon)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtMEX = new DataTable("dtMex");
                DataTable dtUSA = new DataTable("dtUSA");

                if (dsCon != null)
                {
                    if (dsCon.Tables.Count > 0)
                    {
                        dtMEX = dsCon.Tables[0].Copy();
                        dtUSA = dsCon.Tables[1].Copy();

                        foreach (DataRow row in dsCon.Tables[2].Rows)
                        {
                            string sName = row["ClaveContrato"].S();
                            dtMEX.Columns.Add("ddl" + sName);
                            dtUSA.Columns.Add("ddl" + sName);
                        }

                        foreach (DataRow row in dsCon.Tables[2].Rows)
                        {
                            string sName = row["ClaveContrato"].S();
                            dtMEX.Columns.Add(sName);
                            dtUSA.Columns.Add(sName);
                        }

                        foreach (DataRow row in dtMEX.Rows)
                        {
                            DataTable dt = oIGesCat.DBGetObtieneImportesPorGastosYContrato(row["IdGasto"].S().L(), Utils.GetUser);

                            for (int i = 0; i < dsCon.Tables[2].Rows.Count; i++)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        if (dsCon.Tables[2].Rows[i]["ClaveContrato"].S() == dt.Rows[j]["ClaveContrato"].S())
                                        {
                                            row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Porcentaje"].S();
                                            row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Importe"].S();
                                        }
                                    }
                                }
                                else
                                {
                                    row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0";
                                    row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0,00";
                                }
                            }
                        }

                        foreach (DataRow row in dtUSA.Rows)
                        {
                            DataTable dt = oIGesCat.DBGetObtieneImportesPorGastosYContrato(row["IdGasto"].S().L(), Utils.GetUser);

                            for (int i = 0; i < dsCon.Tables[2].Rows.Count; i++)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        if (dsCon.Tables[2].Rows[i]["ClaveContrato"].S() == dt.Rows[j]["ClaveContrato"].S())
                                        {
                                            row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Porcentaje"].S();
                                            row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Importe"].S();
                                        }
                                    }
                                }
                                else
                                {
                                    row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0";
                                    row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0,00";
                                }
                            }
                        }

                        ds.Tables.Add(dtMEX);
                        ds.Tables.Add(dtUSA);
                        ds.Tables.Add(dsCon.Tables[2].Copy());
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetEliminaGastoEstimado(oIView.iIdGasto);
        }
        private string ArmaCadenaCuentasGastos()
        {
            try
            {
                DataTable dtC = new DBExtraccion().ObtieneCuentasRubrosActivos;
                string sCadCuentas = string.Empty;
                string sQuery = string.Empty;
                foreach (DataRow row in dtC.Rows)
                {
                    if (sCadCuentas == string.Empty)
                        sCadCuentas = "('" + row["DescripcionCuenta"].S() + "'";
                    else
                        sCadCuentas += "," + "'" + row["DescripcionCuenta"].S() + "'";
                }

                sCadCuentas += ")";

                return sCadCuentas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void eUpaComprobante_Presenter(object sender, EventArgs e)
        {
            oIGesCat.ActualizaBanderaComprobanteGasto(oIView.dtGastosMEX);
            oIGesCat.ActualizaBanderaComprobanteGasto(oIView.dtGastosUSA);

            ObjSelected_Presenter(sender, e);
        }
    }
}