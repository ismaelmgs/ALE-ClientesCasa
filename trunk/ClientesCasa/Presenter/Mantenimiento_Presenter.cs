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
            oIView.eUpaComprobanteMXN += eUpaComprobanteMXN_Presenter;
            oIView.eUpaComprobanteUSD += eUpaComprobanteUSD_Presenter;
            oIView.eGetCargaInicial += eGetCargaInicial_Presenter;

            oIView.eObjSelectedUSD += ObjSelectedUSD_Presenter;
            oIView.eUpaGastosUSD += eUpaGastosUSD_Presenter;
        }

        protected void eGetCargaInicial_Presenter(object sender, EventArgs e)
        {
            oIView.dtRubros = oIGesCat.DBGetObtieneRubros;
            oIView.dtProveedor = oIGesCat.DBGetObtieneProveedores();
            oIView.dtTiposGasto = oIGesCat.DBGetConsultaTiposGasto;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = new DBContratos().DBGetObtieneClientesContratos(oIView.oArray);
            oIView.LLenaClientes(oIView.dtClientes);
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO CargaGastos ---");
            object[] oArr = oIView.oArrGastos;
            DataSet ds = ArmaTablasParaCargarMXN(oIGesCat.DBGetObtieneGastosMXNUSD(oArr[1].S().I(), oArr[3].S().I(), oArr[5].S()));
            
            
            oIView.CargaGastosMEXUSA(ds);
            Utils.GuardarBitacora("MANTTO_DATOS  --> FIN CargaGastos --");
        }

        protected void ObjSelectedUSD_Presenter(object sender, EventArgs e)
        {
            Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO CargaGastos ---");
            object[] oArr = oIView.oArrGastos;
            DataSet ds = ArmaTablasParaCargarUSD(oIGesCat.DBGetObtieneGastosMXNUSD(oArr[1].S().I(), oArr[3].S().I(), oArr[5].S()));


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

            //Omar Validar que no haya problemas
            //oIView.dtRubros = oIGesCat.DBGetObtieneRubros;
            //oIView.dtTiposGasto = oIGesCat.DBGetConsultaTiposGasto;
            DataSet dsCon = ArmaTablasParaCargarMXN(oIGesCat.DBGetObtieneGastosMXNUSD(oArr[1].S().I(), oArr[3].S().I(), oArr[5].S()));
            
            oIView.CargaGastosMEXUSA(dsCon);
        }

        protected void eUpaGastosUSD_Presenter(object sender, EventArgs e)
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

            //Omar Validar que no haya problemas
            //oIView.dtRubros = oIGesCat.DBGetObtieneRubros;
            //oIView.dtTiposGasto = oIGesCat.DBGetConsultaTiposGasto;
            DataSet dsCon = ArmaTablasParaCargarUSD(oIGesCat.DBGetObtieneGastosMXNUSD(oArr[1].S().I(), oArr[3].S().I(), oArr[5].S()));

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
            List<MantenimientoGastosType> olst = new List<MantenimientoGastosType>();
            foreach (MantenimientoGastos oMtto in oIView.oLstContratosGasto)
            {
                MantenimientoGastosType oM = new MantenimientoGastosType();
                oM.iIdGasto = oMtto.iIdGasto;
                oM.dImporte = oMtto.dImporte;
                oM.sContrato = oMtto.sContrato;
                oM.iPorcentaje = oMtto.iPorcentaje;
                oM.sUsuario = oMtto.sUsuario;

                olst.Add(oM);
            }

            oIGesCat.DBSetInsertaImportesContratoGastos(olst.ConvertListToDataTable());
        }
        protected void eSearchLegs_Presenter(object sender, EventArgs e)
        {
            oIView.dtLegs = oIGesCat.DBGetConsultaPiernasPorMatriculaFecha(oIView.sMatricula, oIView.dtFechaVlo);

        }
        protected void eNewGastoEstimado_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetInsertaGastoEstimado(oIView.oGastoE);
        }
        
        private DataSet ArmaTablasParaCargarMXN(DataSet dsCon)
        {
            try
            {
                //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Arma Gastor por contrato (porcentaje) --");

                DataSet ds = new DataSet();
                DataTable dtMEX = new DataTable("dtMex");
                //DataTable dtUSA = new DataTable("dtUSA");

                if (dsCon != null)
                {
                    if (dsCon.Tables.Count > 0)
                    {
                        dtMEX = dsCon.Tables[0].Copy();
                        //dtUSA = dsCon.Tables[1].Copy();

                        foreach (DataRow row in dsCon.Tables[2].Rows)
                        {
                            string sName = row["ClaveContrato"].S();
                            dtMEX.Columns.Add("ddl" + sName);
                            //dtUSA.Columns.Add("ddl" + sName);

                            dtMEX.Columns.Add(sName);
                            //dtUSA.Columns.Add(sName);
                        }

                        DataTable dtT = oIGesCat.DBGetObtieneImportesTodos_Gastos(oIView.sMatricula , oIView.iAnio, oIView.iMes);

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene porcentaje para MXN --");
                        foreach (DataRow row in dtMEX.Rows)
                        {
                            
                            //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene Importes Por Gastos y Contrato MXN --");
                            string strIdGasto = row["IdGasto"].S();
                            DataRow[] dr = dtT.Select("IdGasto=" + strIdGasto);
                            DataTable dt = dtT.Clone();
                            foreach (DataRow d in dr)
                                dt.ImportRow(d);


                            //dt = oIGesCat.DBGetObtieneImportesPorGastosYContrato(row["IdGasto"].S().L(), Utils.GetUser);
                            //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene Importes Por Gastos y Contrato MXN --");
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

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene porcentaje para MXN --");

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene porcentaje para USD --");
                        //foreach (DataRow row in dtUSA.Rows)
                        //{
                        //    string strIdGastoUSA = row["IdGasto"].S();
                        //    DataRow[] drUSA = dtT.Select("IdGasto=" + strIdGastoUSA);
                        //    DataTable dt = dtT.Clone();
                        //    foreach (DataRow d in drUSA)
                        //        dt.ImportRow(d);

                        //    //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene Importes Por Gastos y Contrato USD --");
                        //    //DataTable dt = oIGesCat.DBGetObtieneImportesPorGastosYContrato(row["IdGasto"].S().L(), Utils.GetUser);
                        //    //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene Importes Por Gastos y Contrato USD --");
                        //    for (int i = 0; i < dsCon.Tables[2].Rows.Count; i++)
                        //    {
                        //        if (dt.Rows.Count > 0)
                        //        {
                        //            for (int j = 0; j < dt.Rows.Count; j++)
                        //            {
                        //                if (dsCon.Tables[2].Rows[i]["ClaveContrato"].S() == dt.Rows[j]["ClaveContrato"].S())
                        //                {
                        //                    row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Porcentaje"].S();
                        //                    row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Importe"].S();
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0";
                        //            row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0,00";
                        //        }
                        //    }
                        //}

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene porcentaje para USD --");

                        ds.Tables.Add(dtMEX);
                        //ds.Tables.Add(dtUSA);
                        ds.Tables.Add(dsCon.Tables[2].Copy());
                    }
                }

                //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Arma Gastor por contrato (porcentaje) --");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet ArmaTablasParaCargarUSD(DataSet dsCon)
        {
            try
            {
                //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Arma Gastor por contrato (porcentaje) --");

                DataSet ds = new DataSet();
                //DataTable dtMEX = new DataTable("dtMex");
                DataTable dtUSA = new DataTable("dtUSA");

                if (dsCon != null)
                {
                    if (dsCon.Tables.Count > 0)
                    {
                        //dtMEX = dsCon.Tables[0].Copy();
                        dtUSA = dsCon.Tables[1].Copy();

                        foreach (DataRow row in dsCon.Tables[2].Rows)
                        {
                            string sName = row["ClaveContrato"].S();
                            //dtMEX.Columns.Add("ddl" + sName);
                            dtUSA.Columns.Add("ddl" + sName);

                            //dtMEX.Columns.Add(sName);
                            dtUSA.Columns.Add(sName);
                        }

                        DataTable dtT = oIGesCat.DBGetObtieneImportesTodos_Gastos(oIView.sMatricula, oIView.iAnio, oIView.iMes);

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene porcentaje para MXN --");
                        //foreach (DataRow row in dtMEX.Rows)
                        //{

                        //    //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene Importes Por Gastos y Contrato MXN --");
                        //    string strIdGasto = row["IdGasto"].S();
                        //    DataRow[] dr = dtT.Select("IdGasto=" + strIdGasto);
                        //    DataTable dt = dtT.Clone();
                        //    foreach (DataRow d in dr)
                        //        dt.ImportRow(d);


                        //    //dt = oIGesCat.DBGetObtieneImportesPorGastosYContrato(row["IdGasto"].S().L(), Utils.GetUser);
                        //    //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene Importes Por Gastos y Contrato MXN --");
                        //    for (int i = 0; i < dsCon.Tables[2].Rows.Count; i++)
                        //    {
                        //        if (dt.Rows.Count > 0)
                        //        {
                        //            for (int j = 0; j < dt.Rows.Count; j++)
                        //            {
                        //                if (dsCon.Tables[2].Rows[i]["ClaveContrato"].S() == dt.Rows[j]["ClaveContrato"].S())
                        //                {
                        //                    row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Porcentaje"].S();
                        //                    row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = dt.Rows[j]["Importe"].S();
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            row["ddl" + dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0";
                        //            row[dsCon.Tables[2].Rows[i]["ClaveContrato"].S()] = "0,00";
                        //        }
                        //    }
                        //}

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene porcentaje para MXN --");

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene porcentaje para USD --");
                        foreach (DataRow row in dtUSA.Rows)
                        {
                            string strIdGastoUSA = row["IdGasto"].S();
                            DataRow[] drUSA = dtT.Select("IdGasto=" + strIdGastoUSA);
                            DataTable dt = dtT.Clone();
                            foreach (DataRow d in drUSA)
                                dt.ImportRow(d);

                            //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO Obtiene Importes Por Gastos y Contrato USD --");
                            //DataTable dt = oIGesCat.DBGetObtieneImportesPorGastosYContrato(row["IdGasto"].S().L(), Utils.GetUser);
                            //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene Importes Por Gastos y Contrato USD --");
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

                        //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Obtiene porcentaje para USD --");

                        //ds.Tables.Add(dtMEX);
                        ds.Tables.Add(dtUSA);
                        ds.Tables.Add(dsCon.Tables[2].Copy());
                    }
                }

                //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Arma Gastor por contrato (porcentaje) --");
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
        protected void eUpaComprobanteMXN_Presenter(object sender, EventArgs e)
        {
            oIGesCat.ActualizaBanderaComprobanteGasto(oIView.dtGastosMEX);
            //oIGesCat.ActualizaBanderaComprobanteGasto(oIView.dtGastosUSA);

            ObjSelected_Presenter(sender, e);
        }

        protected void eUpaComprobanteUSD_Presenter(object sender, EventArgs e)
        {
            //oIGesCat.ActualizaBanderaComprobanteGasto(oIView.dtGastosMEX);
            oIGesCat.ActualizaBanderaComprobanteGasto(oIView.dtGastosUSA);

            ObjSelected_Presenter(sender, e);
        }
    }
}