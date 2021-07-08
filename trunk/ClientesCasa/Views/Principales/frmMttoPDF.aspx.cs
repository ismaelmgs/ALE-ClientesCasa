using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ClientesCasa.Clases;

using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using ClientesCasa.Presenter;

namespace ClientesCasa.Views.Principales
{
    public partial class frmMttoPDF : System.Web.UI.Page, IViewMttoPDF
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new MttoPDF_Presenter(this, new DBMttoPDF());
            //CargaPDF();
            if (!IsPostBack)
            {
                iAnioRef = 0;

                //LlenaTablaPrueba();
                if (gvFiles.Rows.Count < 1)
                {
                    btnUnir.Visible = false;
                    btnCrearPoliza.Visible = false;
                }
                else
                {
                    btnUnir.Visible = true;
                    btnCrearPoliza.Visible = true;
                }
            }
        }
        protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowIndex == 0)
                    {
                        ImageButton imb = (ImageButton)e.Row.FindControl("imbUp");
                        if (imb != null)
                        {
                            imb.Visible = false;
                        }
                    }

                    if (e.Row.RowIndex == dtArchivos.Rows.Count - 1)
                    {
                        ImageButton imb = (ImageButton)e.Row.FindControl("imbDown");
                        if (imb != null)
                        {
                            imb.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int iIndex = e.CommandArgument.S().I();
                DataTable dtTemp = dtArchivos.Clone();

                if (e.CommandName == "Up" && iIndex > 0)
                {
                    for (int i = 0; i <= iIndex - 2; i++)
                    {
                        dtTemp.ImportRow(dtArchivos.Rows[i]);
                    }

                    dtTemp.ImportRow(dtArchivos.Rows[iIndex]);
                    dtTemp.ImportRow(dtArchivos.Rows[iIndex - 1]);

                    for (int i = iIndex + 1; i < dtArchivos.Rows.Count; i++)
                    {
                        dtTemp.ImportRow(dtArchivos.Rows[i]);
                    }
                }

                if (e.CommandName == "Down" && iIndex < dtArchivos.Rows.Count - 1)
                {
                    for (int i = 0; i < iIndex; i++)
                    {
                        dtTemp.ImportRow(dtArchivos.Rows[i]);
                    }

                    dtTemp.ImportRow(dtArchivos.Rows[iIndex + 1]);
                    dtTemp.ImportRow(dtArchivos.Rows[iIndex]);

                    for (int i = iIndex + 2; i < dtArchivos.Rows.Count; i++)
                    {
                        dtTemp.ImportRow(dtArchivos.Rows[i]);
                    }
                }
                dtArchivos = dtTemp;
                gvFiles.DataSource = dtTemp;
                gvFiles.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void UploadMultipleImages(object sender, EventArgs e)
        {
            try
            {
                if (txtReferencia.Text.S() != string.Empty && iAnioRef > 0)
                {
                    string sRuta = string.Empty;
                    sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                    sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                    sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                    sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));
                    sRuta = sRuta.Replace("[moneda]", sMonedaRef);

                    if (fuDocuments.HasFile)
                    {
                        if (Directory.Exists(sRuta))
                        {
                            foreach (HttpPostedFile postedFile in fuDocuments.PostedFiles)
                            {
                                string fileName = Path.GetFileName(postedFile.FileName);
                                postedFile.SaveAs(sRuta + "\\" + sReferencia + "_" + fileName);
                            }
                            Mensaje("Archivos cargados correctamente:" + fuDocuments.PostedFiles.Count);
                            ObtenerArchivos(sRuta, sReferencia);
                        }
                        else
                        {
                            //Directory.CreateDirectory(sRuta);
                            Mensaje("No existe la ruta: " + sRuta);
                        }
                    }
                }
                else
                    lblReferencia.Text = "Es necesario escribir una referencia.";
            }
            catch (Exception ex)
            {
                Mensaje("Error: " + ex.Message);
                //throw ex;
            }

        }
        protected void btnUnir_Click(object sender, EventArgs e)
        {
            try
            {
                string sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));

                string sRutaDestino = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_D"].S();
                bool bExiste = false;

                for (int i = 0; i < gvFiles.Rows.Count; i++)
                {
                    if (gvFiles.Rows[i].Cells[1].Text.Contains("Poliza"))
                    {
                        bExiste = true;
                        break;
                    }
                }

                if (bExiste == true)
                {
                    string[] files = Directory.GetFiles(sRuta, "*.pdf");
                    List<byte[]> filesByte = new List<byte[]>();

                    foreach (DataRow row in dtArchivos.Rows)
                    {
                        if (row["FileName"].S().Contains(sReferencia))
                            filesByte.Add(File.ReadAllBytes(row["Url"].S()));
                    }

                    File.WriteAllBytes(sRutaDestino + "Completo_" + sReferencia + ".pdf", PdfMerger.MergeFiles(filesByte));
                    Mensaje("El archivo se ha creado, verifique el destino");
                }
                else
                    Mensaje("Lo sentimos, no se pueden unir los documentos por falta de póliza.");
            }
            catch (Exception ex)
            {
                Mensaje("Error: " + ex.Message);
            }
        }
        protected void btnShowPopup_Click(object sender, EventArgs e)
        {
            try
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                //this.lblMessage.Text = "Your Registration is done successfully. Our team will contact you shotly";

                //string sRuta = string.Empty;
                //sRuta = @"C:\\FilePDFTest.pdf"; //hdnPdf.Value;

                //swViewPdf.FilePath = sRuta;

                //btnShowPopup.Attributes.Add("onclick", "javascript:AbrePdf();");
                CargaPDF();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void imgbtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                sReferencia = txtReferencia.Text.S();

                if (eSearchDetailReferencia != null)
                    eSearchDetailReferencia(sender, e);

                CargaArchivos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnCrearPoliza_Click(object sender, EventArgs e)
        {
            try
            {
                bool bExiste = false;
                sNombre = txtReferencia.Text.S();

                if (gvFiles.Rows.Count > 0)
                {
                    //int index = gvFiles.CommandArgument.S().I();

                    for (int i = 0; i < gvFiles.Rows.Count; i++)
                    {
                        if (gvFiles.Rows[i].Cells[1].Text.Contains("Poliza"))
                        {
                            bExiste = true;
                            break;
                        }
                    }

                    if (bExiste == false)
                        ObtenerInfoPoliza();
                    else
                        Mensaje("No se puede crear documento, poliza existente.");

                    CargaArchivos();
                    //tmReload.Enabled = true;

                    Mensaje("La póliza se he creado, pulse buscar nuevamente.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (dtArchivos != null)
                {
                    if (dtArchivos.Rows.Count > 0)
                    {
                        ImageButton btn = (ImageButton)sender;
                        int index = Convert.ToInt32(btn.CommandArgument);
                        dtArchivos.Rows[index].Delete();
                        dtArchivos.AcceptChanges();
                    }
                }
                gvFiles.DataSource = dtArchivos;
                gvFiles.DataBind();
                //ObtenerArchivos(string.Empty, sReferencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                int index = Convert.ToInt32(btn.CommandArgument);
                string sUrl = gvFiles.Rows[index].Cells[3].Text.S().Replace("\\", "\\\\");
                String sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));

                string sMoneda = string.Empty;
                string sMon = sMoneda == "MXN" ? "MN" : "USD";
                sRuta = sRuta.Replace("[moneda]", sMon);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmVistaPrevia.aspx?sRuta=" + sUrl + "',this.target, 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
                ObtenerArchivos(sRuta, sReferencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MÉTODOS
        public void LlenaTablaPrueba()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TipoArchivo", typeof(string));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("Url", typeof(string));

            DataRow dr1 = dt.NewRow();
            dr1["TipoArchivo"] = "Prueba1";
            dr1["FileName"] = "Prueba1";
            dr1["Url"] = "C:\\PDFs\\HOJA1.pdf";

            DataRow dr2 = dt.NewRow();
            dr2["TipoArchivo"] = "Prueba1";
            dr2["FileName"] = "Prueba2";
            dr2["Url"] = "C:\\PDFs\\HOJA2.pdf";

            DataRow dr3 = dt.NewRow();
            dr3["TipoArchivo"] = "Prueba1";
            dr3["FileName"] = "Prueba3";
            dr3["Url"] = "C:\\PDFs\\HOJA3.pdf";

            DataRow dr4 = dt.NewRow();
            dr4["TipoArchivo"] = "Prueba1";
            dr4["FileName"] = "Prueba4";
            dr4["Url"] = "C:\\PDFs\\HOJA4.pdf";

            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            dt.Rows.Add(dr4);

            dtArchivos = dt;
            gvFiles.DataSource = dt;
            gvFiles.DataBind();

            int i = 0;
            foreach (GridViewRow row in gvFiles.Rows)
            {
                if (i == 0)
                {
                    ImageButton imb = (ImageButton)row.FindControl("imbUp");
                    if (imb != null)
                    {
                        imb.Visible = false;
                    }
                }

                if (i == dtArchivos.Rows.Count - 1)
                {
                    ImageButton imb = (ImageButton)row.FindControl("imbDown");
                    if (imb != null)
                    {
                        imb.Visible = false;
                    }
                }

                Button btn = (Button)row.FindControl("btnVista");
                btn.Visible = true;

                i++;
            }

        }
        protected void CargaPDF()
        {
            try
            {
                #region CODIGO PRUEBA
                //string sRuta = string.Empty;
                //sRuta = @"file:///C:/FilePDFTest.pdf"; //hdnPdf.Value;
                //ShowPdf1.FilePath = sRuta;
                //iframe.Attributes.Add("src", sRuta);
                //Response.Write("<script language='JavaScript'>window.open(this.href,this.target,'width=400,height=250');return false;<scri pt>");
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                //this.lblMessage.Text = "Your Registration is done successfully. Our team will contact you shotly";
                #endregion

                string sRuta = @"\\mac\Home\Desktop\ProjectAle\trunk\ClientesCasa\Views\Principales\FilePDFTest.pdf";//System.Web.HttpContext.Current.Server.MapPath("") + "\\" + "FilePDFTest.pdf"; //@"C:\\FilePDFTest.pdf";
                //ShowPdf1.FilePath = sRuta;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool ObtenerInfoPoliza()
        {
            try
            {
                string sCadenaHtml = string.Empty;
                string sNombreArchivo = string.Empty;
                bool Ban = false;

                if (eSearchInfoPoliza != null)
                    eSearchInfoPoliza(null, null);


                if (dtHeadPoliza != null && dtDetailsPoliza != null)
                {
                    if (dtHeadPoliza.Rows.Count > 0 && dtDetailsPoliza.Rows.Count > 0)
                    {
                        sCadenaHtml = CrearHtml();

                        if (!string.IsNullOrEmpty(sCadenaHtml))
                        {
                            string sRuta = string.Empty;
                            sNombreArchivo = "Poliza " + sReferencia + ".pdf";
                            sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S() + sNombreArchivo;
                            sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                            sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                            sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));

                            Document document = new Document();
                            PdfWriter.GetInstance(document, new FileStream(sRuta, FileMode.Create));
                            document.Open();

                            iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
                            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                            hw.Parse(new StringReader(sCadenaHtml));
                            document.Close();
                            ShowPdf(sRuta);

                            Ban = true;
                        }
                    }
                    else
                    {
                        Ban = false;
                    }
                }
                else
                {
                    Ban = false;
                }
                return Ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ShowPdf(string s)
        {
            try
            {
                if (File.Exists(s))
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "inline;filename=" + s);
                    Response.ContentType = "application/pdf";
                    Response.WriteFile(s);
                    Response.Flush();
                    Response.Clear();
                }
                //ObtenerArchivos(string.Empty, sReferencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ObtenerArchivos(string sRuta, string sReferencia)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("TipoArchivo", typeof(string));
                dt.Columns.Add("FileName", typeof(string));
                dt.Columns.Add("Url", typeof(string));

                //sRuta = @"C:\Users\Administrador\Documents\DOCS";

                if (Directory.Exists(sRuta))
                {
                    string[] archivos = System.IO.Directory.GetFiles(sRuta,"*.pdf", SearchOption.AllDirectories); //Diretory.GerFiles("c:\carpeta", "*.txt", SearchOption.AllDirectories)
                    System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo(sRuta);

                    //aqui tambien podemos filtrar y obtener archivos de los subdirectorios
                    //FileInfo[] files = directorio.GetFiles(sRuta, SearchOption.AllDirectories);

                    //foreach (String file in files)
                    //{
                    //    FileInfo info = new FileInfo(file);
                    //    // do something with info.Length
                    //}

                    //recorremos todos los archivos
                    foreach (String file in archivos)
                    {
                        FileInfo a = new FileInfo(file);

                        DataRow dr = dt.NewRow();

                        if (a.Name.Contains(sReferencia.ToLower()) || a.Name.Contains(sReferencia.ToUpper()))
                        {
                            if (a.Extension == ".pdf" || a.Extension == ".PDF")
                            {
                                dr["TipoArchivo"] = a.Extension;
                                dr["FileName"] = a.Name;
                                dr["Url"] = a.FullName; //"C:\\PDFs\\HOJA1.pdf";
                                dt.Rows.Add(dr);
                            }

                            if (a.Extension == ".jpg" || a.Extension == ".JPG" || a.Extension == ".jpeg" || a.Extension == ".JPEG")
                            {
                                dr["TipoArchivo"] = a.Extension;
                                dr["FileName"] = a.Name;
                                dr["Url"] = a.FullName; //"C:\\PDFs\\HOJA1.pdf";
                                dt.Rows.Add(dr);
                            }

                            if (a.Extension == ".png" || a.Extension == ".PNG")
                            {
                                dr["TipoArchivo"] = a.Extension;
                                dr["FileName"] = a.Name;
                                dr["Url"] = a.FullName; //"C:\\PDFs\\HOJA1.pdf";
                                dt.Rows.Add(dr);
                            }

                        }
                    }

                    dtArchivos = dt;
                    gvFiles.DataSource = dt;
                    gvFiles.DataBind();

                    int i = 0;
                    foreach (GridViewRow row in gvFiles.Rows)
                    {
                        if (i == 0)
                        {
                            ImageButton imb = (ImageButton)row.FindControl("imbUp");
                            if (imb != null)
                            {
                                imb.Visible = false;
                            }
                        }

                        if (i == dtArchivos.Rows.Count - 1)
                        {
                            ImageButton imb = (ImageButton)row.FindControl("imbDown");
                            if (imb != null)
                            {
                                imb.Visible = false;
                            }
                        }

                        //Button btn = (Button)row.FindControl("btnVista");
                        //btn.Visible = true;

                        i++;
                    }

                }

                upaFiles.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadInfoPoliza(DataSet ds)
        {
            try
            {
                dtHeadPoliza = null;
                dtDetailsPoliza = null;

                dtHeadPoliza = (DataTable)ds.Tables[0];
                dtDetailsPoliza = (DataTable)ds.Tables[1];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public String CrearHtml()
        {
            try
            {
                string sCadenaHtml = string.Empty;
                //Encabezado
                string sClaveConcur = string.Empty;
                string sMatricula = string.Empty;
                string sMoneda = string.Empty;
                //Totales
                double dSumaImpMxn = 0.0;
                double dSumaPromedio = 0.0;
                double dSumaImp = 0.0;

                //DetalleConcur.InnerHtml = string.Empty;

                if (dtHeadPoliza != null && dtDetailsPoliza != null)
                {
                    if (dtHeadPoliza.Rows.Count > 0 && dtDetailsPoliza.Rows.Count > 0)
                    {
                        sClaveConcur = dtHeadPoliza.Rows[0][0].S();
                        sMatricula = dtHeadPoliza.Rows[0][1].S();
                        sMoneda = dtHeadPoliza.Rows[0][2].S();

                        string cadena = @"<table border='0' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width:90%;'>" +
                            "<tr><td colspan='6' bgcolor='#ffffff' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><font color='#000000'><B>Aerolineas Ejecutivas S.A. de C.V.</B></font></td></tr>" +
                            "<tr><td align='right'>CLAVE DE NOTA:</td><td align='left'>" + sClaveConcur + "</td><td align='right'>MATRÍCULA:</td><td align='left'>" + sMatricula + "</td><td align='right'>MONEDA:</td><td align='left'>" + sMoneda + "</td></tr>" +
                            "<tr><td colspan='6'></td></tr>" +
                            "<tr><td colspan='6'></td></tr>" +
                            "<tr>" +
                            "<td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none; text-align: center;'><B>FECHA DE REGISTRO</B></td>" +
                            "<td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none; text-align: center;'><B>NO. DE CUENTA</B></td>" +
                            "<td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none; text-align: center;'><B>GASTO</B></td>" +
                            "<td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none; text-align: center;'><B>.IMPORTE MXN</B></td>" +
                            "<td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none; text-align: center;'><B>PROMEDIO</B></td>" +
                            "<td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none; text-align: center;'><B>.IMPORTE</B></td>" +
                            "</tr>";

                        int RowMax = dtDetailsPoliza.Rows.Count - 1;

                        //DETALLE
                        for (int i = 0; i < dtDetailsPoliza.Rows.Count - 1; i++)
                        {
                            cadena = cadena + "<tr>";
                            cadena = cadena + "<td align='center'>" + dtDetailsPoliza.Rows[i][0].S() + "</td>";
                            cadena = cadena + "<td align='center'>" + dtDetailsPoliza.Rows[i][1].S() + "</td>";
                            cadena = cadena + "<td align='center'>" + dtDetailsPoliza.Rows[i][2].S() + "</td>";
                            cadena = cadena + "<td align='right'>" + dtDetailsPoliza.Rows[i][3].D().ToString("N2") + "</td>";
                            cadena = cadena + "<td align='right'>" + dtDetailsPoliza.Rows[i][4].D().ToString("0.00") + "</td>";
                            cadena = cadena + "<td align='right'>" + dtDetailsPoliza.Rows[i][5].D().ToString("N2") + "</td>";
                            cadena = cadena + "</tr>";

                            dSumaImpMxn = dSumaImpMxn + dtDetailsPoliza.Rows[i][3].Db();
                            dSumaPromedio = dSumaPromedio + dtDetailsPoliza.Rows[i][4].Db();
                            dSumaPromedio = dSumaPromedio / dtDetailsPoliza.Rows.Count;

                            dSumaImp = dSumaImp + dtDetailsPoliza.Rows[i][5].Db();
                        }

                        //TOTALES
                        cadena = cadena + "<tr>";
                        cadena = cadena + "<td colspan='3'><B>Total General</B></td>";
                        cadena = cadena + "<td align='right'><B>" + dSumaImpMxn.ToString("N2") + "</B></td>";
                        cadena = cadena + "<td align='right'><B>" + dSumaPromedio.ToString("0.00") + "</B></td>";
                        cadena = cadena + "<td align='right'><B>" + dSumaImp.ToString("N2") + "</B></td>";
                        cadena = cadena + "</tr>";
                        cadena = cadena + "</table>";

                        //DetalleConcur.InnerHtml = cadena;

                        sCadenaHtml = cadena;
                    }
                }
                return sCadenaHtml;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public void Mensaje(string sMensaje)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }
        private void CargaArchivos()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReferencia.Text))
                {
                    String sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();

                    sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                    sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                    sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));


                    sReferencia = txtReferencia.Text;
                    ObtenerArchivos(sRuta, txtReferencia.Text);

                    if (gvFiles.Rows.Count > 0)
                    {
                        btnUnir.Visible = true;
                        btnCrearPoliza.Visible = true;
                    }
                    else
                    {
                        btnUnir.Visible = false;
                        btnCrearPoliza.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ObtieneNombreMes(int iMes)
        {
            string sMes = string.Empty;
            switch (iMes)
            {
                case 1:
                    sMes = "01 ENERO";
                    break;
                case 2:
                    sMes = "02 FEBRERO";
                    break;
                case 3:
                    sMes = "03 MARZO";
                    break;
                case 4:
                    sMes = "04 ABRIL";
                    break;
                case 5:
                    sMes = "05 MAYO";
                    break;
                case 6:
                    sMes = "06 JUNIO";
                    break;
                case 7:
                    sMes = "07 JULIO";
                    break;
                case 8:
                    sMes = "08 AGOSTO";
                    break;
                case 9:
                    sMes = "09 SEPTIEMBRE";
                    break;
                case 10:
                    sMes = "10 OCTUBRE";
                    break;
                case 11:
                    sMes = "11 NOVIEMBRE";
                    break;
                case 12:
                    sMes = "12 DICIEMBRE";
                    break;
            }

            return sMes;
        }

        public void LoadReferencia(bool ban)
        {
            if (ban)
            {
                lblReferencia.Text = "Exito!! se encontró la referencia";
                lblReferencia.Visible = true;
                btnUploadImages.Visible = true;
            }
            else
            {
                lblReferencia.Text = "Es necesario escribir la referencia";
                lblReferencia.Visible = true;
                btnUploadImages.Visible = false;
            }
            
        }

        private string ArmaRutaDirectorio()
        {
            try
            {
                string sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                sRuta = sRuta.S().Replace("\\", "\\\\");
                sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));
                string sMon = sMonedaRef == "MXN" ? "MN" : "USD";
                sRuta = sRuta.Replace("[moneda]", sMon);

                return sRuta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmMttoPDF.aspx.cs";
        private const string sPagina = "frmMttoPDF.aspx";

        MttoPDF_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchInfoPoliza;
        public event EventHandler eSearchDetailReferencia;

        public string sReferencia
        {
            get { return (string)ViewState["VSReferencia"]; }
            set { ViewState["VSReferencia"] = value; }
        }

        public DataTable dtArchivos
        {
            get { return (DataTable)ViewState["VSArchivos"]; }
            set { ViewState["VSArchivos"] = value; }
        }

        public DataTable dtHeadPoliza
        {
            get { return (DataTable)ViewState["VSHeadPoliza"]; }
            set { ViewState["VSHeadPoliza"] = value; }
        }

        public DataTable dtDetailsPoliza
        {
            get { return (DataTable)ViewState["VSDetailsPoliza"]; }
            set { ViewState["VSDetailsPoliza"] = value; }
        }
        public string sNombre
        {
            get { return (string)ViewState["vSsNombre"]; }
            set { ViewState["vSsNombre"] = value; }
        }

        public string sMatriculaRef
        {
            get { return (string)ViewState["VSMatriculaRef"]; }
            set { ViewState["VSMatriculaRef"] = value; }
        }

        public int iMesRef
        {
            get { return (int)ViewState["VSMesRef"]; }
            set { ViewState["VSMesRef"] = value; }
        }

        public int iAnioRef
        {
            get { return (int)ViewState["VSAnioRef"]; }
            set { ViewState["VSAnioRef"] = value; }
        }

        public string sMonedaRef
        {
            get { return (string)ViewState["VSTipoMonedaRef"]; }
            set { ViewState["VSTipoMonedaRef"] = value; }
        }
        #endregion



    }
}