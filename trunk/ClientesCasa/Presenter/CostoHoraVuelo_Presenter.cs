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
                DataSet dsReporte;
                DataTable dtFijos = new DataTable();
                DataTable dtVariables = new DataTable();
                DataColumn column;
                DataRow rowFijos;
                DataRow rowVariables;
                string strImporte = string.Empty;
                string strImporteFijo = string.Empty;
                string strImporteVar = string.Empty;

                DataTable dtTotalesF = new DataTable();
                DataColumn columnTf;
                DataRow rowTotalf;
                string strImporteTotalesF = string.Empty;

                DataTable dtTotalesV = new DataTable();
                DataColumn columnTv;
                DataRow rowTotalv;
                string strImporteTotalesV = string.Empty;


                DataTable dtTotalesT = new DataTable();
                DataColumn columnTT;
                DataRow rowTotalT;
                string strImporteTotalesT = string.Empty;

                decimal dTC = 0;
                foreach (DataRow row in dtTC.Rows)
                {
                    dTC += row["Rate"].S().D();
                }

                dTC = Math.Round(dTC / dtTC.Rows.Count, 4);
                oIView.dTipoCambio = dTC;
                DataSet ds = oIGesCat.DBGetObtieneGastosPorHrVuelo(oIView.dtInicio, oIView.dtFin, oIView.sMoneda, oIView.sMatricula, dTC);

                #region                Tabla de FIJOS 
                column = new DataColumn();
                column.ColumnName = "Concepto";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Enero";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Febrero";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Marzo";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Abril";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Mayo";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Junio";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Julio";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Agosto";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Septiembre";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Octubre";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Noviembre";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Diciembre";
                dtFijos.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Total";
                dtFijos.Columns.Add(column);

                dtVariables = dtFijos.Clone();

                #endregion

                #region Tabla de Totales Fijos
                columnTf = new DataColumn();
                columnTf.ColumnName = "Tconcepto";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tenero";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tfebrero";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tmarzo";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tabril";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tmayo";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tjunio";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tjulio";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tagosto";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tseptiembre";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Toctubre";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tnoviembre";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Tdiciembre";
                dtTotalesF.Columns.Add(columnTf);

                columnTf = new DataColumn();
                columnTf.ColumnName = "Ttotal";
                dtTotalesF.Columns.Add(columnTf);

                #endregion

                #region Tabla de Totales VARIABLES
                columnTv = new DataColumn();
                columnTv.ColumnName = "Tconcepto";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tenero";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tfebrero";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tmarzo";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tabril";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tmayo";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tjunio";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tjulio";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tagosto";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tseptiembre";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Toctubre";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tnoviembre";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Tdiciembre";
                dtTotalesV.Columns.Add(columnTv);

                columnTv = new DataColumn();
                columnTv.ColumnName = "Ttotal";
                dtTotalesV.Columns.Add(columnTv);

                #endregion

                #region Tabla de Totales TOTALES
                columnTT = new DataColumn();
                columnTT.ColumnName = "Tconcepto";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tenero";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tfebrero";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tmarzo";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tabril";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tmayo";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tjunio";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tjulio";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tagosto";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tseptiembre";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Toctubre";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tnoviembre";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Tdiciembre";
                dtTotalesT.Columns.Add(columnTT);

                columnTT = new DataColumn();
                columnTT.ColumnName = "Ttotal";
                dtTotalesT.Columns.Add(columnTT);

                #endregion

                string sHTML = string.Empty;
                sHTML = "<table style='width:100%'>";
                sHTML += "<tr>";
                sHTML += "<td colspan='14' style='background-color:#E8E4E3; border-bottom:2px solid #000000'>FIJOS</td>";
                sHTML += "</tr>";

                DataTable dtRubrosFijos = ObtieneRubrosFijosSinRepetir(ds.Tables[0]);
                foreach (DataRow row in dtRubrosFijos.Rows)
                {
                    rowFijos = dtFijos.NewRow();
                    string sRubro = row["DescRubro"].S();
                    DataRow[] drRubros = ds.Tables[0].Select("DescripcionRubro = '" + sRubro + "'");
                    rowFijos["Concepto"] = sRubro;
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
                                    strImporte = drRubros[i]["Importe"].S().D().ToString("c");
                                    sHTML += "<td style='width:7.00%; text-align: right'>" + drRubros[i]["Importe"].S().D().ToString("c") + "</td>";
                                    dImporteFijo += drRubros[i]["Importe"].S().D();
                                    #region Carga en tabla los fijos pra el RPT
                                    if (i == 0)
                                        rowFijos["Enero"] = strImporte;
                                    if (i == 1)
                                        rowFijos["Febrero"] = strImporte;
                                    if (i == 2)
                                        rowFijos["Marzo"] = strImporte;
                                    if (i == 3)
                                        rowFijos["Abril"] = strImporte;
                                    if (i == 4)
                                        rowFijos["Mayo"] = strImporte;
                                    if (i == 5)
                                        rowFijos["Junio"] = strImporte;
                                    if (i == 6)
                                        rowFijos["Julio"] = strImporte;
                                    if (i == 7)
                                        rowFijos["Agosto"] = strImporte;
                                    if (i == 8)
                                        rowFijos["Septiembre"] = strImporte;
                                    if (i == 9)
                                        rowFijos["Octubre"] = strImporte;
                                    if (i == 10)
                                        rowFijos["Noviembre"] = strImporte;
                                    if (i == 11)
                                        rowFijos["Diciembre"] = strImporte;
                                    #endregion
                                }
                            }
                            catch (Exception)
                            {
                                sHTML += "<td style='width:7.00%; text-align: right'>" + (0).ToString("c") + "</td>";
                                #region Carga en tabla los fijos pra el RPT
                                strImporte = (0).ToString("c");
                                if (i == 0)
                                    rowFijos["Enero"] = strImporte;
                                if (i == 1)
                                    rowFijos["Febrero"] = strImporte;
                                if (i == 2)
                                    rowFijos["Marzo"] = strImporte;
                                if (i == 3)
                                    rowFijos["Abril"] = strImporte;
                                if (i == 4)
                                    rowFijos["Mayo"] = strImporte;
                                if (i == 5)
                                    rowFijos["Junio"] = strImporte;
                                if (i == 6)
                                    rowFijos["Julio"] = strImporte;
                                if (i == 7)
                                    rowFijos["Agosto"] = strImporte;
                                if (i == 8)
                                    rowFijos["Septiembre"] = strImporte;
                                if (i == 9)
                                    rowFijos["Octubre"] = strImporte;
                                if (i == 10)
                                    rowFijos["Noviembre"] = strImporte;
                                if (i == 11)
                                    rowFijos["Diciembre"] = strImporte;
                                #endregion
                            }
                        }
                        strImporteFijo = dImporteFijo.ToString("c");
                        rowFijos["Total"] = strImporteFijo;
                        sHTML += "<td style='width:7.00%; text-align: right'>" + dImporteFijo.ToString("c") + "</td>";
                    }
                    dtFijos.Rows.Add(rowFijos);
                    sHTML += "</tr>";
                }

                decimal dTotalFijo = 0;
                sHTML += "<tr>";
                rowTotalf = dtTotalesF.NewRow();  //Inicio del Row de totales Fijos para el RPT
                for (int i = 0; i < 14; i++)
                {
                    if (i == 0)
                    {
                        rowTotalf["Tconcepto"] = "TOTAL";
                        sHTML += "<td style='width:8.82%; text-align: left; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + "   " + "TOTAL" + "</td>";
                    }
                    else if (i == 13)
                    {
                        rowTotalf["Ttotal"] = dTotalFijo.ToString("c");
                        sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dTotalFijo.ToString("c") + "</td>";
                    }
                    else
                    {
                        DataRow[] dr = ds.Tables[1].Select("Mes = " + i.ToString());
                        if (dr != null && dr.Length > 0)
                        {
                            dTotalFijo += dr[0]["Importe"].S().D();
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dr[0]["Importe"].S().D().ToString("c") + "</td>";
                            #region  Carga en tabla totales Fijos pra RPT
                            strImporteTotalesF = dr[0]["Importe"].S().D().ToString("c");
                            if (i == 1)
                                rowTotalf["Tenero"] = strImporteTotalesF;
                            if (i == 2)
                                rowTotalf["Tfebrero"] = strImporteTotalesF;
                            if (i == 3)
                                rowTotalf["Tmarzo"] = strImporteTotalesF;
                            if (i == 4)
                                rowTotalf["Tabril"] = strImporteTotalesF;
                            if (i == 5)
                                rowTotalf["Tmayo"] = strImporteTotalesF;
                            if (i == 6)
                                rowTotalf["Tjunio"] = strImporteTotalesF;
                            if (i == 7)
                                rowTotalf["Tjulio"] = strImporteTotalesF;
                            if (i == 8)
                                rowTotalf["Tagosto"] = strImporteTotalesF;
                            if (i == 9)
                                rowTotalf["Tseptiembre"] = strImporteTotalesF;
                            if (i == 10)
                                rowTotalf["Toctubre"] = strImporteTotalesF;
                            if (i == 11)
                                rowTotalf["Tnoviembre"] = strImporteTotalesF;
                            if (i == 12)
                                rowTotalf["Tdiciembre"] = strImporteTotalesF;
                            #endregion
                        }
                        else
                        {
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + (0).ToString("c") + "</td>";
                            #region  Carga en tabla totales Fijos pra RPT
                            strImporteTotalesF = (0).ToString("c");
                            if (i == 1)
                                rowTotalf["Tenero"] = strImporteTotalesF;
                            if (i == 2)
                                rowTotalf["Tfebrero"] = strImporteTotalesF;
                            if (i == 3)
                                rowTotalf["Tmarzo"] = strImporteTotalesF;
                            if (i == 4)
                                rowTotalf["Tabril"] = strImporteTotalesF;
                            if (i == 5)
                                rowTotalf["Tmayo"] = strImporteTotalesF;
                            if (i == 6)
                                rowTotalf["Tjunio"] = strImporteTotalesF;
                            if (i == 7)
                                rowTotalf["Tjulio"] = strImporteTotalesF;
                            if (i == 8)
                                rowTotalf["Tagosto"] = strImporteTotalesF;
                            if (i == 9)
                                rowTotalf["Tseptiembre"] = strImporteTotalesF;
                            if (i == 10)
                                rowTotalf["Toctubre"] = strImporteTotalesF;
                            if (i == 11)
                                rowTotalf["Tnoviembre"] = strImporteTotalesF;
                            if (i == 12)
                                rowTotalf["Tdiciembre"] = strImporteTotalesF;
                            #endregion
                        }
                    }
                }
                dtTotalesF.Rows.Add(rowTotalf);
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
                    rowVariables = dtVariables.NewRow();
                    string sRubro = row["DescRubro"].S();
                    DataRow[] drRubros = ds.Tables[2].Select("DescripcionRubro = '" + sRubro + "'");
                    rowVariables["Concepto"] = sRubro;
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
                                    strImporte = drRubros[i]["Importe"].S().D().ToString("c");
                                    sHTML += "<td style='width:7.00%; text-align: right'>" + drRubros[i]["Importe"].S().D().ToString("c") + "</td>";
                                    dImporteVar += drRubros[i]["Importe"].S().D();
                                    #region Carga en tabla los VARIABLES pra el RPT
                                    if (i == 0)
                                        rowVariables["Enero"] = strImporte;
                                    if (i == 1)
                                        rowVariables["Febrero"] = strImporte;
                                    if (i == 2)
                                        rowVariables["Marzo"] = strImporte;
                                    if (i == 3)
                                        rowVariables["Abril"] = strImporte;
                                    if (i == 4)
                                        rowVariables["Mayo"] = strImporte;
                                    if (i == 5)
                                        rowVariables["Junio"] = strImporte;
                                    if (i == 6)
                                        rowVariables["Julio"] = strImporte;
                                    if (i == 7)
                                        rowVariables["Agosto"] = strImporte;
                                    if (i == 8)
                                        rowVariables["Septiembre"] = strImporte;
                                    if (i == 9)
                                        rowVariables["Octubre"] = strImporte;
                                    if (i == 10)
                                        rowVariables["Noviembre"] = strImporte;
                                    if (i == 11)
                                        rowVariables["Diciembre"] = strImporte;
                                    #endregion
                                }
                            }
                            catch (Exception)
                            {
                                sHTML += "<td style='width:7.00%; text-align: right'>" + (0).ToString("c") + "</td>";
                                #region Carga en tabla los VARIABLES pra el RPT
                                strImporte = (0).ToString("c");
                                if (i == 0)
                                    rowVariables["Enero"] = strImporte;
                                if (i == 1)
                                    rowVariables["Febrero"] = strImporte;
                                if (i == 2)
                                    rowVariables["Marzo"] = strImporte;
                                if (i == 3)
                                    rowVariables["Abril"] = strImporte;
                                if (i == 4)
                                    rowVariables["Mayo"] = strImporte;
                                if (i == 5)
                                    rowVariables["Junio"] = strImporte;
                                if (i == 6)
                                    rowVariables["Julio"] = strImporte;
                                if (i == 7)
                                    rowVariables["Agosto"] = strImporte;
                                if (i == 8)
                                    rowVariables["Septiembre"] = strImporte;
                                if (i == 9)
                                    rowVariables["Octubre"] = strImporte;
                                if (i == 10)
                                    rowVariables["Noviembre"] = strImporte;
                                if (i == 11)
                                    rowVariables["Diciembre"] = strImporte;
                                #endregion
                            }
                        }
                        strImporteVar = dImporteVar.ToString("c");
                        rowVariables["Total"] = strImporteVar;
                        sHTML += "<td style='width:7.00%; text-align: right'>" + dImporteVar.ToString("c") + "</td>";
                    }
                    dtVariables.Rows.Add(rowVariables);
                    sHTML += "</tr>";
                }

                #region Codigo comentado
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
                #endregion

                decimal dTotalVar = 0;
                sHTML += "<tr>";
                rowTotalv = dtTotalesV.NewRow();  //Inicio del Row de totales VARIABLES para el RPT
                for (int i = 0; i < 14; i++)
                {
                    if (i == 0)
                    {
                        rowTotalv["Tconcepto"] = "TOTAL";
                        sHTML += "<td style='width:8.82%; text-align: left; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + "   " + "TOTAL" + "</td>";
                    }
                    else if (i == 13)
                    {
                        rowTotalv["Ttotal"] = dTotalVar.ToString("c");
                        sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dTotalVar.ToString("c") + "</td>";
                    }
                    else
                    {
                        DataRow[] dr = ds.Tables[3].Select("Mes = " + i.ToString());
                        if (dr != null && dr.Length > 0)
                        {
                            dTotalVar += dr[0]["Importe"].S().D();
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dr[0]["Importe"].S().D().ToString("c") + "</td>";
                            #region  Carga en tabla totales VARIABLES pra RPT
                            strImporteTotalesV = dr[0]["Importe"].S().D().ToString("c");
                            if (i == 1)
                                rowTotalv["Tenero"] = strImporteTotalesV;
                            if (i == 2)
                                rowTotalv["Tfebrero"] = strImporteTotalesV;
                            if (i == 3)
                                rowTotalv["Tmarzo"] = strImporteTotalesV;
                            if (i == 4)
                                rowTotalv["Tabril"] = strImporteTotalesV;
                            if (i == 5)
                                rowTotalv["Tmayo"] = strImporteTotalesV;
                            if (i == 6)
                                rowTotalv["Tjunio"] = strImporteTotalesV;
                            if (i == 7)
                                rowTotalv["Tjulio"] = strImporteTotalesV;
                            if (i == 8)
                                rowTotalv["Tagosto"] = strImporteTotalesV;
                            if (i == 9)
                                rowTotalv["Tseptiembre"] = strImporteTotalesV;
                            if (i == 10)
                                rowTotalv["Toctubre"] = strImporteTotalesV;
                            if (i == 11)
                                rowTotalv["Tnoviembre"] = strImporteTotalesV;
                            if (i == 12)
                                rowTotalv["Tdiciembre"] = strImporteTotalesV;
                            #endregion
                        }
                        else
                        {
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + (0).ToString("c") + "</td>";
                            #region  Carga en tabla totales VARIABLES pra RPT
                            strImporteTotalesV = (0).ToString("c");
                            if (i == 1)
                                rowTotalv["Tenero"] = strImporteTotalesV;
                            if (i == 2)
                                rowTotalv["Tfebrero"] = strImporteTotalesV;
                            if (i == 3)
                                rowTotalv["Tmarzo"] = strImporteTotalesV;
                            if (i == 4)
                                rowTotalv["Tabril"] = strImporteTotalesV;
                            if (i == 5)
                                rowTotalv["Tmayo"] = strImporteTotalesV;
                            if (i == 6)
                                rowTotalv["Tjunio"] = strImporteTotalesV;
                            if (i == 7)
                                rowTotalv["Tjulio"] = strImporteTotalesV;
                            if (i == 8)
                                rowTotalv["Tagosto"] = strImporteTotalesV;
                            if (i == 9)
                                rowTotalv["Tseptiembre"] = strImporteTotalesV;
                            if (i == 10)
                                rowTotalv["Toctubre"] = strImporteTotalesV;
                            if (i == 11)
                                rowTotalv["Tnoviembre"] = strImporteTotalesV;
                            if (i == 12)
                                rowTotalv["Tdiciembre"] = strImporteTotalesV;
                            #endregion
                        }
                    }
                }
                dtTotalesV.Rows.Add(rowTotalv);
                sHTML += "</tr>";

                sHTML += "<tr><td colspan='14'></td></tr>";
                sHTML += "</table>";
                sHTML += "<br />";
                oIView.dTotalImporteVar = dTotalVar;


                sHTML += "<table style='width:100%'>";
                decimal dTotalFinal = 0;
                sHTML += "<tr>";
                rowTotalT = dtTotalesT.NewRow();//Inicio del Row de totales TOTALES para el RPT
                for (int i = 0; i < 14; i++)
                {
                    if (i == 0)
                    {
                        rowTotalT["Tconcepto"] = "TOTAL";
                        sHTML += "<td style='width:8.82%; text-align: left; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + "   " + "TOTAL" + "</td>";
                    }
                    else if (i == 13)
                    {
                        rowTotalT["Ttotal"] = dTotalFinal.ToString("c");
                        sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dTotalFinal.ToString("c") + "</td>";
                    }
                    else
                    {
                        DataRow[] dr = ds.Tables[4].Select("Mes = " + i.ToString());
                        if (dr != null && dr.Length > 0)
                        {
                            dTotalFinal += dr[0]["Importe"].S().D();
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + dr[0]["Importe"].S().D().ToString("c") + "</td>";
                            #region  Carga en tabla totales VARIABLES pra RPT
                            strImporteTotalesT = dr[0]["Importe"].S().D().ToString("c");
                            if (i == 1)
                                rowTotalT["Tenero"] = strImporteTotalesT;
                            if (i == 2)
                                rowTotalT["Tfebrero"] = strImporteTotalesT;
                            if (i == 3)
                                rowTotalT["Tmarzo"] = strImporteTotalesT;
                            if (i == 4)
                                rowTotalT["Tabril"] = strImporteTotalesT;
                            if (i == 5)
                                rowTotalT["Tmayo"] = strImporteTotalesT;
                            if (i == 6)
                                rowTotalT["Tjunio"] = strImporteTotalesT;
                            if (i == 7)
                                rowTotalT["Tjulio"] = strImporteTotalesT;
                            if (i == 8)
                                rowTotalT["Tagosto"] = strImporteTotalesT;
                            if (i == 9)
                                rowTotalT["Tseptiembre"] = strImporteTotalesT;
                            if (i == 10)
                                rowTotalT["Toctubre"] = strImporteTotalesT;
                            if (i == 11)
                                rowTotalT["Tnoviembre"] = strImporteTotalesT;
                            if (i == 12)
                                rowTotalT["Tdiciembre"] = strImporteTotalesT;
                            #endregion
                        }
                        else
                        {
                            sHTML += "<td style='width:7.00%; text-align: right; border-bottom:2px solid #000000; border-top:2px solid #000000; font-weight:bold;'>" + (0).ToString("c") + "</td>";
                            #region  Carga en tabla totales VARIABLES pra RPT
                            strImporteTotalesT = (0).ToString("c");
                            if (i == 1)
                                rowTotalT["Tenero"] = strImporteTotalesT;
                            if (i == 2)
                                rowTotalT["Tfebrero"] = strImporteTotalesT;
                            if (i == 3)
                                rowTotalT["Tmarzo"] = strImporteTotalesT;
                            if (i == 4)
                                rowTotalT["Tabril"] = strImporteTotalesT;
                            if (i == 5)
                                rowTotalT["Tmayo"] = strImporteTotalesT;
                            if (i == 6)
                                rowTotalT["Tjunio"] = strImporteTotalesT;
                            if (i == 7)
                                rowTotalT["Tjulio"] = strImporteTotalesT;
                            if (i == 8)
                                rowTotalT["Tagosto"] = strImporteTotalesT;
                            if (i == 9)
                                rowTotalT["Tseptiembre"] = strImporteTotalesT;
                            if (i == 10)
                                rowTotalT["Toctubre"] = strImporteTotalesT;
                            if (i == 11)
                                rowTotalT["Tnoviembre"] = strImporteTotalesT;
                            if (i == 12)
                                rowTotalT["Tdiciembre"] = strImporteTotalesT;
                            #endregion
                        }
                    }
                }
                dtTotalesT.Rows.Add(rowTotalT);
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

                oIView.dtFijos = dtFijos;
                oIView.dtVariables = dtVariables;
                oIView.dtTOTALESFijos = dtTotalesF;
                oIView.dtTOTALESVariables = dtTotalesV;
                oIView.dtTOTALESTOTALES = dtTotalesT;

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