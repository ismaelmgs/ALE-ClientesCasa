using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.Presenter
{
    public class EstadoCuenta_Presenter : BasePresenter<IVIewEstadoCuenta>
    {
        private readonly DBEstadoCuenta oIGesCat;

        public EstadoCuenta_Presenter(IVIewEstadoCuenta oView, DBEstadoCuenta oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eSearchTotales += eSearchTotales_Presenter;
            oIView.eSearchEdoCuenta += eSearchEdoCuenta_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = new DBContratos().DBGetObtieneClientesContratos(oIView.oArrFiltros);
            oIView.LLenaClientes(oIView.dtClientes);
        }

        protected void eSearchEdoCuenta_Presenter(object sender, EventArgs e)
        {
            string sMatricula = oIView.sMatricula;
            DataSet ds = oIGesCat.DBGetConsultaEstadoCuenta(oIView.iAnio, oIView.iMes, sMatricula);

            oIView.dsEdoCuenta = ds;
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            try
            {
                string sMatricula = oIView.sMatricula;
                DataSet ds = oIGesCat.DBGetConsultaEstadoCuenta(oIView.iAnio, oIView.iMes, sMatricula);


                // PESOS
                string sHTML = string.Empty;
                sHTML = "<table style='width:100%'>";
                sHTML += "<tr>";
                sHTML += "<td colspan='8' style='border-bottom:2px solid #000000; border-top:2px solid #000000'><strong>Fecha y detalle de las operaciones</strong></td>";
                sHTML += "</tr>";

                sHTML += "<tr>";
                sHTML += "<td colspan='8'>Nuevos cargos y abonos en $ para: " + sMatricula + "</td>";
                sHTML += "</tr>";


                sHTML += "<tr>";
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    sHTML += "<td style='border-bottom:2px solid #000000'>" + col.ColumnName.S() + "</td>";
                }
                sHTML += "</tr>";

                for(int i=0 ; i < ds.Tables[0].Rows.Count; i++)
                {
                    sHTML += "<tr>";
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if(j == 7)
                            sHTML += "<td style='text-align:right'>" + ds.Tables[0].Rows[i][j].S().D().ToString("c") + "</td>";
                        else
                            sHTML += "<td>" + ds.Tables[0].Rows[i][j].S() + "</td>";
                    }
                    sHTML += "</tr>";
                }
                decimal dTotalPesos = 0;
                decimal dIVA = 0; 
                decimal dTotal = 0;

                if (ds.Tables.Count > 2)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        if (ds.Tables[2].Rows[0][0].S() == "1")
                        {
                            dTotalPesos = SumaColumnaTabla(ds.Tables[0], "Importe");
                            dIVA = dTotalPesos * .16m;
                            dTotal = dTotalPesos + dIVA;

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right;  border-top:2px solid #000000'>Total de cargos en $ para:</td>";
                            sHTML += "<td style='text-align: right;  border-top:2px solid #000000'>" + dTotalPesos.ToString("c") + "</td>";
                            sHTML += "</tr>";

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; '>IVA:</td>";
                            sHTML += "<td style='text-align: right; '>" + dIVA.ToString("c") + "</td>";
                            sHTML += "</tr>";

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; '>Total:</td>";
                            sHTML += "<td style='text-align: right; '>" + dTotal.ToString("c") + "</td>";
                            sHTML += "</tr>";
                        }
                        else if (ds.Tables[2].Rows[0][0].S() == "0" || string.IsNullOrEmpty(ds.Tables[2].Rows[0][0].S()))
                        {
                            dTotalPesos = SumaColumnaTabla(ds.Tables[0], "Importe");
                            //dIVA = dTotalPesos * .16m;
                            dTotal = dTotalPesos;

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right;  border-top:2px solid #000000'>Total de cargos en $ para:</td>";
                            sHTML += "<td style='text-align: right;  border-top:2px solid #000000'>" + dTotalPesos.ToString("c") + "</td>";
                            sHTML += "</tr>";

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; '>Total:</td>";
                            sHTML += "<td style='text-align: right; '>" + dTotal.ToString("c") + "</td>";
                            sHTML += "</tr>";
                        }
                    }
                }
                else
                {
                    dTotalPesos = SumaColumnaTabla(ds.Tables[0], "Importe");
                    dIVA = dTotalPesos * .16m;
                    dTotal = dTotalPesos + dIVA;

                    sHTML += "<tr>";
                    sHTML += "<td colspan='7' style='text-align: right;  border-top:2px solid #000000'>Total de cargos en $ para:</td>";
                    sHTML += "<td style='text-align: right;  border-top:2px solid #000000'>" + dTotalPesos.ToString("c") + "</td>";
                    sHTML += "</tr>";

                    sHTML += "<tr>";
                    sHTML += "<td colspan='7' style='text-align: right; '>IVA:</td>";
                    sHTML += "<td style='text-align: right; '>" + dIVA.ToString("c") + "</td>";
                    sHTML += "</tr>";

                    sHTML += "<tr>";
                    sHTML += "<td colspan='7' style='text-align: right; '>Total:</td>";
                    sHTML += "<td style='text-align: right; '>" + dTotal.ToString("c") + "</td>";
                    sHTML += "</tr>";
                }

                

                

                

                //sHTML += "<tr>";
                //sHTML += "<td colspan='7' style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>Total de cargos en $ para:</td>";
                //sHTML += "<td style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>" + dTotalPesos.ToString("c") + "</td>";
                //sHTML += "</tr>";

                //sHTML += "<tr>";
                //sHTML += "<td colspan='7' style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>Total de cargos en $ para:</td>";
                //sHTML += "<td style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>" + dTotalPesos.ToString("c") + "</td>";
                //sHTML += "</tr>";


                sHTML += "<tr>";
                sHTML += "<td colspan='8' style='Height:40px'></td>";
                sHTML += "</tr>";

                // DOLARES
                sHTML += "<tr>";
                sHTML += "<td colspan='8' style='border-top:2px solid #000000'>Nuevos cargos y abonos en USD para: " + sMatricula + "</td>";
                sHTML += "</tr>";


                sHTML += "<tr>";
                foreach (DataColumn col in ds.Tables[1].Columns)
                {
                    sHTML += "<td style='border-bottom:2px solid #000000'>" + col.ColumnName.S() + "</td>";
                }
                sHTML += "</tr>";

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    sHTML += "<tr>";
                    for (int j = 0; j < ds.Tables[1].Columns.Count; j++)
                    {
                        if (j == 7)
                            sHTML += "<td style='text-align:right'>" + ds.Tables[1].Rows[i][j].S().D().ToString("c") + "</td>";
                        else
                            sHTML += "<td>" + ds.Tables[1].Rows[i][j].S() + "</td>";
                    }
                    sHTML += "</tr>";
                }


                decimal dTotalDlls = 0;
                decimal dIVADlls = 0;
                decimal dTotalDls = 0;

                if (ds.Tables.Count > 2)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        if (ds.Tables[2].Rows[0][0].S() == "1")
                        {
                            dTotalDlls = SumaColumnaTabla(ds.Tables[1], "Importe");
                            dIVADlls = dTotalDlls * .16m;
                            dTotalDls = dTotalDlls + dIVADlls;

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; border-top:2px solid #000000'>Total de cargos en USD:</td>";
                            sHTML += "<td style='text-align: right; border-top:2px solid #000000'>" + dTotalDlls.ToString("c") + "</td>";
                            sHTML += "</tr>";

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; '>IVA:</td>";
                            sHTML += "<td style='text-align: right; '>" + dIVADlls.ToString("c") + "</td>";
                            sHTML += "</tr>";

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; '>Total en USD:</td>";
                            sHTML += "<td style='text-align: right; '>" + dTotalDls.ToString("c") + "</td>";
                            sHTML += "</tr>";
                        }
                        else if (ds.Tables[2].Rows[0][0].S() == "0" || string.IsNullOrEmpty(ds.Tables[2].Rows[0][0].S()))
                        {
                            dTotalDlls = SumaColumnaTabla(ds.Tables[1], "Importe");
                            //dIVADlls = dTotalDlls * .16m;
                            dTotalDls = dTotalDlls + dIVADlls;

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; border-top:2px solid #000000'>Total de cargos en USD:</td>";
                            sHTML += "<td style='text-align: right; border-top:2px solid #000000'>" + dTotalDlls.ToString("c") + "</td>";
                            sHTML += "</tr>";

                            sHTML += "<tr>";
                            sHTML += "<td colspan='7' style='text-align: right; '>Total en USD:</td>";
                            sHTML += "<td style='text-align: right; '>" + dTotalDls.ToString("c") + "</td>";
                            sHTML += "</tr>";
                        }
                    }
                }
                else
                {
                    dTotalDlls = SumaColumnaTabla(ds.Tables[1], "Importe");
                    dIVADlls = dTotalDlls * .16m;
                    dTotalDls = dTotalDlls + dIVADlls;

                    sHTML += "<tr>";
                    sHTML += "<td colspan='7' style='text-align: right; border-top:2px solid #000000'>Total de cargos en USD:</td>";
                    sHTML += "<td style='text-align: right; border-top:2px solid #000000'>" + dTotalDlls.ToString("c") + "</td>";
                    sHTML += "</tr>";

                    sHTML += "<tr>";
                    sHTML += "<td colspan='7' style='text-align: right; '>IVA:</td>";
                    sHTML += "<td style='text-align: right; '>" + dIVADlls.ToString("c") + "</td>";
                    sHTML += "</tr>";

                    sHTML += "<tr>";
                    sHTML += "<td colspan='7' style='text-align: right; '>Total en USD:</td>";
                    sHTML += "<td style='text-align: right; '>" + dTotalDls.ToString("c") + "</td>";
                    sHTML += "</tr>";
                }




                // FINAL
                sHTML += "</table>";
                oIView.LlenaReporte(sHTML);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eSearchTotales_Presenter(object sender, EventArgs e)
        {
            oIView.dtTotal = new DBAnalisisCostoOpe().DBGetObtieneTotalesEdoCuenta(oIView.sClaveContrato, oIView.iAnio, oIView.iMes);
        }

        private decimal SumaColumnaTabla(DataTable dt, string sColumna)
        {
            try
            {
                decimal dSuma = 0;
                foreach (DataRow row in dt.Rows)
                {
                    dSuma += row[sColumna].S().D();
                }

                return dSuma;
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }
    }
}