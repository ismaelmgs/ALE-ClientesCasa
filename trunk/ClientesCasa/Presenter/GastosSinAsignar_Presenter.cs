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
    public class GastosSinAsignar_Presenter : BasePresenter<IViewGastosSinAsignar>
    {
        private readonly DBGastosSinAsignar oIGesCat;

        public GastosSinAsignar_Presenter(IViewGastosSinAsignar oView, DBGastosSinAsignar oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            //oIView.eSearchTotales += eSearchTotales_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            DataSet ds = oIGesCat.DBGetConsultaGastosSinAsignar(oIView.iAnio, oIView.iMes);

            // PESOS
            string sHTML = string.Empty;
            sHTML = "<fieldset>";
            sHTML += "<legend><H5>Gastos No Asignados en Pesos (MXN)</H5></legend>";
            sHTML += "<table style='width:100%'>";
            //sHTML += "<tr>";
            //sHTML += "<td colspan='8' style='border-bottom:2px solid #000000; border-top:2px solid #000000'><strong>Fecha y detalle de las operaciones</strong></td>";
            //sHTML += "</tr>";

            sHTML += "<tr>";
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                sHTML += "<td style='border-bottom:2px solid #000000; font-weight:bold;'>" + col.ColumnName.S() + "</td>";
            }
            sHTML += "</tr>";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sHTML += "<tr>";
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j == 3 || j == 4)
                        sHTML += "<td style='text-align:right'>" + ds.Tables[0].Rows[i][j].S().D().ToString("c") + "</td>";
                    else
                        sHTML += "<td>" + ds.Tables[0].Rows[i][j].S() + "</td>";
                }
                sHTML += "</tr>";
            }

            decimal dTotalPesos = SumaColumnaTabla(ds.Tables[0], "Importe");
            sHTML += "<tr>";
            sHTML += "<td colspan='9' style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>Total en MXN:</td>";
            sHTML += "<td style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>" + dTotalPesos.ToString("c") + "</td>";
            sHTML += "</tr>";
            sHTML += "<tr>";
            sHTML += "<td colspan='9' style='Height:40px'></td>";
            sHTML += "</tr>";
            sHTML += "</table>";
            sHTML += "</fieldset>";
            //-------- Inicia en dolares
            // DOLARES
            sHTML += "<fieldset>";
            sHTML += "<legend><H5>Gastos No Asignados en Doláres (USD)</H5></legend>";
            sHTML += "<table style='width:100%'>";
            //sHTML += "<tr>";
            //sHTML += "<td colspan='8' style='border-top:2px solid #000000'>Nuevos cargos y abonos en USD para:</td>";
            //sHTML += "</tr>";


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
                    if (j == 3 || j == 4)
                        sHTML += "<td style='text-align:right'>" + ds.Tables[1].Rows[i][j].S().D().ToString("c") + "</td>";
                    else
                        sHTML += "<td>" + ds.Tables[1].Rows[i][j].S() + "</td>";
                }
                sHTML += "</tr>";
            }

            decimal dTotalDlls = SumaColumnaTabla(ds.Tables[1], "Importe");
            sHTML += "<tr>";
            sHTML += "<td colspan='9' style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>Total en USD:</td>";
            sHTML += "<td style='text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000'>" + dTotalDlls.ToString("c") + "</td>";
            sHTML += "</tr>";

            // FINAL
            sHTML += "</table>";
            sHTML += "</fieldset>";
            oIView.LlenaReporte(sHTML);

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