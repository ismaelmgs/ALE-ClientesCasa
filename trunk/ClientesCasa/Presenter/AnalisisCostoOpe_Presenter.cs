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
    public class AnalisisCostoOpe_Presenter : BasePresenter<IViewAnalisisCostoOpe>
    {
        private readonly DBAnalisisCostoOpe oIGesCat;

        public AnalisisCostoOpe_Presenter(IViewAnalisisCostoOpe oView, DBAnalisisCostoOpe oCI)
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
                DataSet ds = oIGesCat.DBGetConsultaEstadoCuenta(oIView.iAnio, oIView.iMes, oIView.sMatricula);
                string sHTML = string.Empty;
                decimal dTotalPesos = 0;
                foreach (DataTable table in ds.Tables)
                {
                    decimal dTotalRubro = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        dTotalRubro += row["Importe"].S().D();
                    }

                    dTotalPesos += dTotalRubro;

                    sHTML += "<table style='width:100%'> " +
                                "<tr style='border-bottom:1px solid #000000'> " +
                                    "<td colspan='4' style='width:85%; font-weight:bold;'> " + table.Rows[0]["Rubro"].S() + " - " + table.Rows[0]["TipoGasto"].S() +
                                    "</td> " +
                                    "<td style='width:85%; font-weight:bold;'> " +
                                    dTotalRubro.ToString("c") +
                                    "</td> " +
                                "</tr> ";

                    foreach (DataRow row in table.Rows)
                    {
                        sHTML += "<tr> " +
                                        "<td style='width:15%'>" + row["Fecha"].S() + "</td> " +
                                        "<td style='width:15%'>" + row["Referencia"].S() + "</td> " +
                                        "<td style='width:20%'>" + row["Detalle"].S() + "</td> " +
                                        "<td style='width:35%'>" + row["Proveedor"].S() + "</td> " +
                                        "<td style='width:15%'>" + row["Importe"].S().D().ToString("c") + "</td> " +
                                    "</tr> ";               
                    }

                    sHTML += "</table>";
                    sHTML += "</br>";
                }


                DataSet dsUSD = oIGesCat.DBGetConsultaEstadoCuentaUSD(oIView.iAnio, oIView.iMes, oIView.sMatricula);
                string sHTML_USD = string.Empty;
                decimal dTotalUSD = 0;
                foreach (DataTable table in dsUSD.Tables)
                {
                    decimal dTotalRubro = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        dTotalRubro += row["Importe"].S().D();
                    }

                    dTotalUSD += dTotalRubro;

                    sHTML_USD += "<table style='width:100%'> " +
                                "<tr style='border-bottom:1px solid #000000'> " +
                                    "<td colspan='4' style='width:85%; font-weight:bold;'> " + table.Rows[0]["Rubro"].S() + " - " + table.Rows[0]["TipoGasto"].S() +
                                    "</td> " +
                                    "<td style='width:85%; font-weight:bold;'> " +
                                    dTotalRubro.ToString("c") +
                                    "</td> " +
                                "</tr> ";

                    foreach (DataRow row in table.Rows)
                    {
                        sHTML_USD += "<tr> " +
                                        "<td style='width:15%'>" + row["Fecha"].S() + "</td> " +
                                        "<td style='width:15%'>" + row["Referencia"].S() + "</td> " +
                                        "<td style='width:20%'>" + row["Detalle"].S() + "</td> " +
                                        "<td style='width:35%'>" + row["Proveedor"].S() + "</td> " +
                                        "<td style='width:15%'>" + row["Importe"].S().D().ToString("c") + "</td> " +
                                    "</tr> ";
                    }

                    sHTML_USD += "</table>";
                    sHTML_USD += "</br>";
                }

                oIView.dTotalPesos = dTotalPesos;
                oIView.dTotalUSD = dTotalUSD;
                oIView.sHTML = sHTML;
                oIView.sHTML_USD = sHTML_USD;

                oIView.dsGastos = ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            //oIGesCat.DBSetInsertaTotalesCostoOperacion(oIView.oParams, oIView.sClaveContrato, oIView.sMatricula, oIView.sTipoMoneda, oIView.iMes, oIView.iAnio);
        }

        protected void eSearchTotales_Presenter(object sender, EventArgs e)
        {
            oIView.dtTotal = oIGesCat.DBGetObtieneTotalesEdoCuenta(oIView.sClaveContrato, oIView.iAnio, oIView.iMes);
        }
    }
}