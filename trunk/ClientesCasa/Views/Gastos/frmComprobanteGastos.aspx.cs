using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Drawing;
using System.IO;
using ClientesCasa.Objetos;

namespace ClientesCasa.Views.Gastos
{
    public partial class frmComprobanteGastos : System.Web.UI.Page, IViewComprobantes
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Comprobantes_Presenter(this, new DBComprobantes());

            if (!IsPostBack)
            {

            }
        }
        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void gvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("title", "De clic aqui para selecciona un cliente");

                    e.Row.Attributes.Add("OnMouseOver", "On(this);");
                    e.Row.Attributes.Add("OnMouseOut", "Off(this);");
                    e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gvClientes, "Select$" + e.Row.RowIndex.ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvClientes.PageIndex = e.NewPageIndex;
                LoadClientesMatriculas();
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sClaveCliente = string.Empty;
                string sNombreCliente = string.Empty;
                string sClaveContrato = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#D9E1E4");
                        row.ToolTip = string.Empty;

                        iIdContrato = gvClientes.DataKeys[row.RowIndex]["IdContrato"].S().I();

                        sClaveCliente = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                        sNombreCliente = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
                        sClaveContrato = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        sMatricula = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();

                        ban = true;
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                        row.ToolTip = "Clic para seleccionar esta fila.";
                    }
                }

                if (ban)
                {
                    pnlBusqueda.Visible = false;
                    pnlPeriodo.Visible = true;
                    lblRespCustNum.Text = sClaveCliente;
                    lblRespNombreCliente.Text = sNombreCliente;
                    lblRespClaveContrato.Text = sClaveContrato;
                    lblRespMatricula.Text = sMatricula;
                    txtPeriodo.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPeriodo.Text != string.Empty)
                {
                    string[] sPeriodo = txtPeriodo.Text.S().Split('/');

                    if (sPeriodo.Length == 1)
                        sPeriodo = txtPeriodo.Text.S().Split('-');

                    iMes = sPeriodo[1].S().I();
                    iAnio = sPeriodo[0].S().I();
                }


                if (eObjSelected != null)
                    eObjSelected(sender, e);
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvGastos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string sMoneda = string.Empty;
                    sMoneda = e.Row.Cells[6].Text.S();
                    sReferencia = e.Row.Cells[1].Text.S();

                    String sRuta = ArmaRutaDirectorio(sMoneda);


                    string[] oLstFiles = Directory.GetFiles(sRuta, sReferencia + "*.*");
                    DataTable dtFiles = new DataTable();
                    dtFiles.Columns.Add("Nombre");
                    dtFiles.Columns.Add("Extension");
                    dtFiles.Columns.Add("Ruta");

                    foreach (string sFile in oLstFiles)
                    {
                        DataRow row = dtFiles.NewRow();
                        row["Nombre"] = Path.GetFileName(sFile);
                        row["Extension"] = Path.GetExtension(sFile);
                        row["Ruta"] = sFile;

                        dtFiles.Rows.Add(row);
                    }

                    //iIdGasto = 0;
                    //iIdGasto = gvGastos.DataKeys[e.Row.RowIndex]["IdGasto"].S().I();

                    //if (eGetDocuments != null)
                    //    eGetDocuments(sender, e);

                    if (dtFiles.Rows.Count > 0)
                    {
                        ImageButton imbAgregar = (ImageButton)e.Row.FindControl("imbAgregar");
                        if (imbAgregar != null)
                        {
                            imbAgregar.Visible = false;
                        }
                    }

                    GridView gv = (GridView)e.Row.FindControl("gvArchivos");
                    if (gv != null)
                    {
                        gv.DataSource = dtFiles;
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvGastos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                iIdGasto = gvGastos.DataKeys[e.CommandArgument.S().I()]["IdGasto"].S().I();
                sReferencia = gvGastos.Rows[e.CommandArgument.S().I()].Cells[1].Text.S();
                sDescMoneda = gvGastos.Rows[e.CommandArgument.S().I()].Cells[6].Text.S();

                switch (e.CommandName)
                {
                    case "Agregar":
                        mpeArchivo.Show();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gvArchivos = (GridView)sender;
                iIdImagen = gvArchivos.DataKeys[e.CommandArgument.S().I()]["IdImagen"].S().I();
                string sExtension = gvArchivos.DataKeys[e.CommandArgument.S().I()]["Extension"].S();
                string sNombreArch = gvArchivos.DataKeys[e.CommandArgument.S().I()]["NombreArchivo"].S();
                string sRuta = gvArchivos.Rows[0].Cells[2].Text.S();

                switch (e.CommandName)
                {
                    case "Descargar":

                        if (eGetDocumentId != null)
                            eGetDocumentId(sender, e);

                        if (dtImagen != null && dtImagen.Rows.Count > 0)
                        {
                            byte[] bPDF = (byte[])dtImagen.Rows[0]["Archivo"];
                            if (bPDF != null)
                            {
                                MemoryStream ms = new MemoryStream(bPDF);
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment;filename=" + sNombreArch);
                                Response.ContentType = "application/octet-stream";
                                Response.Buffer = true;
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.BinaryWrite(bPDF);
                                Response.Flush();
                                Response.End();
                            }
                        }
                        break;

                    case "Eliminar":

                        if (eDeleteObj != null)
                            eDeleteObj(sender, e);

                        if (eObjSelected != null)
                            eObjSelected(sender, e);

                        break;

                    case "ViewDoc":
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmVistaPreviaRef.aspx?sRuta=" + sRuta + "',this.target, 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
                        break;

                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                VisualizaArchivo(sender);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAceptarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuArchivo.HasFile)
                {
                    if (ValidaExtension(Path.GetExtension(fuArchivo.FileName)))
                    {
                        sDirectorio = ArmaRutaDirectorio(sDescMoneda);
                        EscribeArchivo(fuArchivo.FileBytes, sDirectorio + "\\" + sReferencia + ".pdf");

                        MostrarMensaje("El archivo se ha subido correctamente.", "Aviso");

                        if (eObjSelected != null)
                            eObjSelected(sender, e);
                    }
                    else
                    {
                        MostrarMensaje("Este tipo de archivo no se puede anexar como comprobante, favor de verificar", "Subir comprobante");
                    }
                }
                else
                    MostrarMensaje("Se debe seleccionar un archivo a subir, favor de verificar", "Subir comprobante");
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvGastosUSD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                iIdGasto = gvGastosUSD.DataKeys[e.CommandArgument.S().I()]["IdGasto"].S().I();
                sReferencia = gvGastosUSD.Rows[e.CommandArgument.S().I()].Cells[1].Text.S();
                sDescMoneda = gvGastosUSD.Rows[e.CommandArgument.S().I()].Cells[6].Text.S();

                switch (e.CommandName)
                {
                    case "Agregar":
                        mpeArchivo.Show();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvGastosUSD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string sMoneda = string.Empty;
                    sMoneda = e.Row.Cells[6].Text.S();
                    sReferencia = e.Row.Cells[1].Text.S();

                    String sRuta = ArmaRutaDirectorio(sMoneda);

                    string[] oLstFiles = Directory.GetFiles(sRuta, sReferencia + "*.*");
                    DataTable dtFiles = new DataTable();
                    dtFiles.Columns.Add("Nombre");
                    dtFiles.Columns.Add("Extension");
                    dtFiles.Columns.Add("Ruta");

                    foreach (string sFile in oLstFiles)
                    {
                        DataRow row = dtFiles.NewRow();
                        row["Nombre"] = Path.GetFileName(sFile);
                        row["Extension"] = Path.GetExtension(sFile);
                        row["Ruta"] = sFile;

                        dtFiles.Rows.Add(row);
                    }


                    if (dtFiles.Rows.Count > 0)
                    {
                        ImageButton imbAgregar = (ImageButton)e.Row.FindControl("imbAgregar");
                        if (imbAgregar != null)
                        {
                            imbAgregar.Visible = false;
                        }
                    }

                    GridView gv = (GridView)e.Row.FindControl("gvArchivosUSD");
                    if (gv != null)
                    {
                        gv.DataSource = dtFiles;
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvArchivosUSD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gvArchivosUSD = (GridView)sender;
                iIdImagen = gvArchivosUSD.DataKeys[e.CommandArgument.S().I()]["IdImagen"].S().I();
                string sExtension = gvArchivosUSD.DataKeys[e.CommandArgument.S().I()]["Extension"].S();
                string sNombreArch = gvArchivosUSD.DataKeys[e.CommandArgument.S().I()]["NombreArchivo"].S();
                string sRuta = gvArchivosUSD.Rows[0].Cells[2].Text.S();

                switch (e.CommandName)
                {
                    case "Descargar":

                        if (eGetDocumentId != null)
                            eGetDocumentId(sender, e);

                        if (dtImagen != null && dtImagen.Rows.Count > 0)
                        {
                            byte[] bPDF = (byte[])dtImagen.Rows[0]["Archivo"];
                            if (bPDF != null)
                            {
                                MemoryStream ms = new MemoryStream(bPDF);
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", "attachment;filename=" + sNombreArch);
                                Response.ContentType = "application/octet-stream";
                                Response.Buffer = true;
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.BinaryWrite(bPDF);
                                Response.Flush();
                                Response.End();
                            }
                        }
                        break;

                    case "Eliminar":

                        if (eDeleteObj != null)
                            eDeleteObj(sender, e);

                        if (eObjSelected != null)
                            eObjSelected(sender, e);

                        break;

                    case "ViewDoc":

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmVistaPreviaRef.aspx?sRuta=" + sRuta + "',this.target, 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
            
                        break;

                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region METODOS
        public void LoadClientesMatriculas()
        {
            try
            {
                gvClientes.DataSource = dtContratos;
                gvClientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadGastos(DataSet dt)
        {
            gvGastos.DataSource = dt.Tables[0];
            gvGastos.DataBind();

            gvGastosUSD.DataSource = dt.Tables[1];
            gvGastosUSD.DataBind();
        }
        private bool ValidaExtension(string sExtension)
        {
            try
            {
                bool ban = false;
                switch (sExtension)
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".bpm":
                    case ".png":
                    case ".gif":
                    case ".pdf":
                        ban = true;
                        break;
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
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
        private void VisualizaArchivo(object sender)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    string sUrl = row.Cells[2].Text;

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmVistaPreviaRef.aspx?sRuta=" + sUrl + "','Visor de documentos', 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ArmaRutaDirectorio(string sMoneda)
        {
            try
            {
                string sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                sRuta = sRuta.S().Replace("\\", "\\\\");
                sRuta = sRuta.Replace("[anio]", iAnio.S());
                sRuta = sRuta.Replace("[matricula]", sMatricula);
                sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMes));
                string sMon = sMoneda == "MXN" ? "MN" : "USD";
                sRuta = sRuta.Replace("[moneda]", sMon);

                return sRuta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EscribeArchivo(byte[] fileBytes, string archivoDestino)
        {
            File.WriteAllBytes(archivoDestino, fileBytes);
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        Comprobantes_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetDocuments;
        public event EventHandler eGetDocumentId;

        public DataTable dtContratos
        {
            get { return (DataTable)ViewState["VSContratos"]; }
            set { ViewState["VSContratos"] = value; }
        }
        public int iIdContrato
        {
            get { return (int)ViewState["VSIdContrato"]; }
            set { ViewState["VSIdContrato"] = value; }
        }
        public object[] oArr
        {
            get
            {
                return new object[] {
                                        "@ClienteNombre", ddlOpcBus.SelectedValue.S().I() == 1 ? txtBusqueda.Text.S() : string.Empty,
                                        "@Contrato", ddlOpcBus.SelectedValue.S().I() == 2 ? txtBusqueda.Text.S() : string.Empty,
                                        "@Matricula", ddlOpcBus.SelectedValue.S().I() == 3 ? txtBusqueda.Text.S() : string.Empty
                                    };
            }
        }
        public string sNumReporte
        {
            get { return (string)ViewState["VSNumReporte"]; }
            set { ViewState["VSNumReporte"] = value; }
        }
        public DataTable dtDocumentos
        {
            get { return (DataTable)ViewState["VSDocumentos"]; }
            set { ViewState["VSDocumentos"] = value; }
        }
        public int iIdGasto
        {
            get { return (int)ViewState["VSIdGasto"]; }
            set { ViewState["VSIdGasto"] = value; }
        }
        public int iIdImagen
        {
            get { return (int)ViewState["VSIdImagen"]; }
            set { ViewState["VSIdImagen"] = value; }
        }
        public DataTable dtImagen
        {
            get { return (DataTable)ViewState["VSdtImagen"]; }
            set { ViewState["VSdtImagen"] = value; }
        }
        public Comprobante oComprobante
        {
            get { return (Comprobante)ViewState["VSComprobante"]; }
            set { ViewState["VSComprobante"] = value; }
        }
        public string sReferencia
        {
            get { return (string)ViewState["VReferencia"]; }
            set { ViewState["VReferencia"] = value; }
        }
        public string sMatricula
        {
            get { return (string)ViewState["VMatricula"]; }
            set { ViewState["VMatricula"] = value; }
        }
        public int iMes
        {
            get { return (int)ViewState["VMes"]; }
            set { ViewState["VMes"] = value; }
        }
        public int iAnio
        {
            get { return (int)ViewState["VAnio"]; }
            set { ViewState["VAnio"] = value; }
        }
        public string sDirectorio
        {
            get { return (string)ViewState["VDirectorio"]; }
            set { ViewState["VDirectorio"] = value; }
        }
        public string sDescMoneda
        {
            get { return (string)ViewState["VDescMoneda"]; }
            set { ViewState["VDescMoneda"] = value; }
        }
        #endregion

        

        


    }
}