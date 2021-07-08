using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Data;
using ClientesCasa.Clases;

namespace ClientesCasa.Presenter
{
    public class CostoHoraVuelo_Presenter: BasePresenter<IViewCostoHoraVuelo>
    {
        private readonly DBCostoHoraVuelo oIGesCat;

        public CostoHoraVuelo_Presenter(IViewCostoHoraVuelo oView, DBCostoHoraVuelo oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eSearchTotales += eSearchTotales_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = new DBContratos().DBGetObtieneClientesContratos(oIView.oArrFiltros);
            oIView.LLenaClientes(oIView.dtClientes);
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTC = oIGesCat.DBGetObtieneTipoCambioPeriodo(oIView.dtInicio, oIView.dtFin);
                decimal dTC = 0;
                foreach (DataRow row in dtTC.Rows)
                {
                    dTC += row["Rate"].S().D();
                }

                dTC = Math.Round(dTC / dtTC.Rows.Count, 4);
                oIView.dTipoCambio = dTC;
                DataSet ds = oIGesCat.DBGetObtieneGastosPorHrVuelo(oIView.dtInicio, oIView.dtFin, oIView.sMoneda, oIView.sMatricula, dTC);


                string sHTML = string.Empty;
                sHTML = "<table style='width:100%'>";
                sHTML += "<tr>";
                sHTML += "<td colspan='14' style='background-color:#E8E4E3; border-bottom:2px solid #000000'>FIJOS</td>";
                sHTML += "</tr>";

                DataTable dtRubrosFijos = ObtieneRubrosFijosSinRepetir(ds.Tables[0]);
                foreach (DataRow row in dtRubrosFijos.Rows)
                {
                    string sRubro = row["DescRubro"].S();
                    DataRow[] drRubros = ds.Tables[0].Select("DescripcionRubro = '" + sRubro + "'");
                    if (drRubros != null)
                    {
                        sHTML += "<td style='width:8.82%; text-align: left'>" + "   " + sRubro + "</td>";

                        decimal dImporteFijo = 0;
                        for (int i = 0; i < 12; i++)
                        {
                            try
                            {
                                if (drRubros[i] != null)
                                {
                                    sHTML += "<td style='width:7.00%; text-align: right'>" + drRubros[i]["Importe"].S().D().ToString("c") + "</td>";
                                    dImporteFijo += drRubros[i]["Importe"].S().D();
                                }
                            }
                            catch (Exception)
                            {
                                sHTML += "<td style='width:7.00%; text-align: right'>" + (0).ToString("c") + "</td>";
                            }
                        }

                        sHTML += "<td style='width:7.00%; text-align: right'>" + dImporteFijo.ToString("c") + "</td>";
                    }

                    sHTML += "</tr>";
                }

                decimal dTotalFijo = 0;
                sHTML += "<tr>";
                for (int i = 0; i < 14; i++)
                {
                    if (i == 0)
                    {
                        sHTML += "<td style='width:8.82%; text-align: left; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + "   " + "TOTAL" + "</td>";
                    }
                    else if (i == 13)
                    {
                        sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dTotalFijo.ToString("c") + "</td>";
                    }
                    else
                    {
                        DataRow[] dr = ds.Tables[1].Select("Mes = " + i.ToString());
                        if (dr != null && dr.Length > 0)
                        {
                            dTotalFijo += dr[0]["Importe"].S().D();
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dr[0]["Importe"].S().D().ToString("c") + "</td>";
                        }
                        else
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + (0).ToString("c") + "</td>";
                    }
                }
                sHTML += "</tr>";
                oIView.dTotalImporteFijo = dTotalFijo;




                sHTML += "<tr>";
                sHTML += "<td colspan='14'><br /></td>";
                sHTML += "</tr>";




                sHTML += "<tr>";
                sHTML += "<td colspan='14' style='background-color:#E8E4E3; border-bottom:2px solid #000000'>VARIABLES</td>";
                sHTML += "</tr>";

                DataTable dtRubrosVar = ObtieneRubrosFijosSinRepetir(ds.Tables[2]);
                foreach (DataRow row in dtRubrosVar.Rows)
                {
                    string sRubro = row["DescRubro"].S();
                    DataRow[] drRubros = ds.Tables[2].Select("DescripcionRubro = '" + sRubro + "'");
                    if (drRubros != null)
                    {
                        sHTML += "<td style='width:8.82%; text-align: left'>" + "   " + sRubro + "</td>";

                        decimal dImporteVar = 0;
                        for (int i = 0; i < 12; i++)
                        {
                            try
                            {
                                if (drRubros[i] != null)
                                {
                                    sHTML += "<td style='width:7.00%; text-align: right'>" + drRubros[i]["Importe"].S().D().ToString("c") + "</td>";
                                    dImporteVar += drRubros[i]["Importe"].S().D();
                                }
                            }
                            catch (Exception)
                            {
                                sHTML += "<td style='width:7.00%; text-align: right'>" + (0).ToString("c") + "</td>";
                            }
                        }

                        sHTML += "<td style='width:7.00%; text-align: right'>" + dImporteVar.ToString("c") + "</td>";
                    }

                    sHTML += "</tr>";
                }


                //foreach (DataRow row in ds.Tables[2].Rows)
                //{
                //    sHTML += "<tr>";
                //    string sRubro = row["DescripcionRubro"].S();
                //    decimal dImporteVar = 0;

                //    for (int i = 0; i < 14; i++)
                //    {
                //        if (i == 0)
                //        {
                //            sHTML += "<td style='width:8.82%; text-align: left'>" + "   " + sRubro + "</td>";
                //        }
                //        else if (i == 13)
                //        {
                //            sHTML += "<td style='width:7.00%; text-align: right'>" + dImporteVar.ToString("c") + "</td>";
                //        }
                //        else
                //        {
                //            if (row["Mes"].S().I() == i)
                //            {
                //                dImporteVar += row["Importe"].S().D();
                //                sHTML += "<td style='width:7.00%; text-align: right'>" + row["Importe"].S().D().ToString("c") + "</td>";
                //            }
                //            else
                //            {
                //                sHTML += "<td style='width:7.00%; text-align: right'>" + (0).ToString("c") + "</td>";
                //            }
                //        }
                //    }
                //    sHTML += "</tr>";
                //}

                decimal dTotalVar = 0;
                sHTML += "<tr>";
                for (int i = 0; i < 14; i++)
                {
                    if (i == 0)
                    {
                        sHTML += "<td style='width:8.82%; text-align: left; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + "   " + "TOTAL" + "</td>";
                    }
                    else if (i == 13)
                    {
                        sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dTotalVar.ToString("c") + "</td>";
                    }
                    else
                    {
                        DataRow[] dr = ds.Tables[3].Select("Mes = " + i.ToString());
                        if (dr != null && dr.Length > 0)
                        {
                            dTotalVar += dr[0]["Importe"].S().D();
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dr[0]["Importe"].S().D().ToString("c") + "</td>";
                        }
                        else
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + (0).ToString("c") + "</td>";
                    }
                }
                sHTML += "</tr>";

                sHTML += "<tr><td colspan='14'></td></tr>";
                sHTML += "</table>";
                sHTML += "<br />";
                oIView.dTotalImporteVar = dTotalVar;


                sHTML += "<table style='width:100%'>";
                decimal dTotalFinal = 0;
                sHTML += "<tr>";
                for (int i = 0; i < 14; i++)
                {
                    if (i == 0)
                    {
                        sHTML += "<td style='width:8.82%; text-align: left; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + "   " + "TOTAL" + "</td>";
                    }
                    else if (i == 13)
                    {
                        sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dTotalFinal.ToString("c") + "</td>";
                    }
                    else
                    {
                        DataRow[] dr = ds.Tables[4].Select("Mes = " + i.ToString());
                        if (dr != null && dr.Length > 0)
                        {
                            dTotalFinal += dr[0]["Importe"].S().D();
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dr[0]["Importe"].S().D().ToString("c") + "</td>";
                        }
                        else
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + (0).ToString("c") + "</td>";
                    }
                }
                sHTML += "</tr>";
                sHTML += "</table>";

                float fTotalTiempo = 0;
                DataTable dtMeses = ds.Tables[5].Copy();
                DataTable dtMes = new DataTable();
                dtMes.Columns.Add("Rubro");
                dtMes.Columns.Add("Enero");
                dtMes.Columns.Add("Febrero");
                dtMes.Columns.Add("Marzo");
                dtMes.Columns.Add("Abril");
                dtMes.Columns.Add("Mayo");
                dtMes.Columns.Add("Junio");
                dtMes.Columns.Add("Julio");
                dtMes.Columns.Add("Agosto");
                dtMes.Columns.Add("Septiembre");
                dtMes.Columns.Add("Octubre");
                dtMes.Columns.Add("Noviembre");
                dtMes.Columns.Add("Diciembre");
                dtMes.Columns.Add("Total");

                DataRow rowM = dtMes.NewRow();
                rowM["Rubro"] = "Horas de Vuelo";
                rowM["Enero"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 1);
                rowM["Febrero"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 2);
                rowM["Marzo"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 3);
                rowM["Abril"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 4);
                rowM["Mayo"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 5);
                rowM["Junio"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 6);
                rowM["Julio"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 7);
                rowM["Agosto"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 8);
                rowM["Septiembre"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 9);
                rowM["Octubre"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 10);
                rowM["Noviembre"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 11);
                rowM["Diciembre"] = Utils.ObtieneTotalTiempoPorMes(dtMeses, "Total", 12);
                rowM["Total"] = Utils.ObtieneTotalTiempo(ds.Tables[5], "Total", ref fTotalTiempo);

                dtMes.Rows.Add(rowM);

                oIView.dtTotalesTiempo = dtMes;
                oIView.sHTML = sHTML;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected void eSearchTotales_Presenter(object sender, EventArgs e)
        {
            //oIView.dtTotal = oIGesCat.DBGetObtieneTotalesEdoCuenta(oIView.sClaveContrato, oIView.iAnio, oIView.iMes);
        }

        private DataTable ObtieneRubrosFijosSinRepetir(DataTable dt)
        {
            try
            {
                DataTable dtRubroNvo = new DataTable();
                dtRubroNvo.Columns.Add("DescRubro");

                foreach (DataRow row in dt.Rows)
                {
                    string sRubro = row["DescripcionRubro"].S();
                    bool bExiste = false;

                    foreach (DataRow row2 in dtRubroNvo.Rows)
                    {
                        if (sRubro == row2["DescRubro"].S())
                        {
                            bExiste = true;
                        }
                    }

                    if (!bExiste)
                    {
                        DataRow rowAdd = dtRubroNvo.NewRow();
                        rowAdd["DescRubro"] = sRubro;

                        dtRubroNvo.Rows.Add(rowAdd);
                    }
                }

                return dtRubroNvo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void SumaImporteRubroATabla(string sRubro, string sMes)
        {

        }
    }
}