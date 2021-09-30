using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Clases;
using System.Data;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System.IO;


namespace ClientesCasa.Views.Catalogos
{
    public partial class frmImagenesMatriculas : System.Web.UI.Page, IViewImagenesMatricula
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ImagenesMatricula_Presenter(this, new DBImagenesMatricula());

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

            }
        }

        protected void gvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //e.Row.ToolTip = "De clic aqui para selecciona un cliente";
                        e.Row.Attributes.Add("title", "De clic aqui para selecciona un cliente");
                        e.Row.Attributes.Add("OnMouseOver", "On(this);");
                        e.Row.Attributes.Add("OnMouseOut", "Off(this);");
                        e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gvClientes, "Select$" + e.Row.RowIndex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvImagenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gvArchivos = (GridView)sender;
                iIdImagen = gvArchivos.DataKeys[e.CommandArgument.S().I()]["IdImagen"].S().I();
                string sExtension = string.Empty;
                string sNombreArch = string.Empty;

                switch (e.CommandName)
                {
                    case "Descargar":

                        DataRow[] drs = dtImagenes.Tables[0].Select("IdImagen = " + iIdImagen);

                        if (drs != null && drs.Length > 0)
                        {
                            sNombreArch = drs[0]["NombreImg"].S();
                            sExtension = drs[0]["Extension"].S();

                            byte[] bPDF = (byte[])drs[0]["Imagen"];
                            if (bPDF != null)
                            {
                                MemoryStream ms = new MemoryStream(bPDF);
                                Response.ContentType = "image/jpeg";
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
                }
            }
            catch(Exception ex)
            { 

            }
        }

        protected void gvImagenesPDF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gvArchivos = (GridView)sender;
                iIdImagen = gvArchivos.DataKeys[e.CommandArgument.S().I()]["IdImagen"].S().I();
                string sExtension = string.Empty;
                string sNombreArch = string.Empty;

                switch (e.CommandName)
                {
                    case "Descargar":

                        DataRow[] drs = dtImagenes.Tables[1].Select("IdImagen = " + iIdImagen);

                        if (drs != null && drs.Length > 0)
                        {
                            sNombreArch = drs[0]["NombreImg"].S();
                            sExtension = drs[0]["Extension"].S();

                            byte[] bPDF = (byte[])drs[0]["Imagen"];
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
                DataTable dt = dtClientes;
                gvClientes.PageIndex = e.NewPageIndex;
                LLenaClientes(dt);
            }
            catch (Exception ex)
            {

            }
        }

        protected void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        protected void imbEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int iIndex = ((ImageButton)sender).CommandArgument.S().I();
                iIdImagen = gvImagenes.DataKeys[iIndex]["IdImagen"].S().I();

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                if (eGetImagenesMatricula != null)
                    eGetImagenesMatricula(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imbEliminarPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int iIndex = ((ImageButton)sender).CommandArgument.S().I();
                iIdImagen = gvImagenesPDF.DataKeys[iIndex]["IdImagen"].S().I();

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                if (eGetImagenesMatricula != null)
                    eGetImagenesMatricula(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ////dtGastosMex = null;
                //lstCliente = new List<string>();
                string sMatriculag = string.Empty;
                //string sContratog = string.Empty;
                string sClaveCliente = string.Empty;
                string sNombreCliente = string.Empty;
                //string sRazonSocial = string.Empty;
                //string sRFC = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        //        row.BackColor = ColorTranslator.FromHtml("#D9E1E4");
                        //        row.ToolTip = string.Empty;

                        sMatriculag = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();
                        //        sContratog = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        sClaveCliente = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                        Label lblAeronaveId = (Label)gvClientes.Rows[row.RowIndex].FindControl("lblIdAeronave");
                        iIdAeronave = lblAeronaveId.Text.S().I();
                        //        sRazonSocial = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        ban = true;
                    }
                    //    else
                    //    {
                    //        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    //    }

                    if (ban)
                    {
                        sNombreCliente = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
                        //    txtPeriodo.Text = string.Empty;
                        //    sMatricula = sMatriculag;
                        //    row.ToolTip = "Clic para seleccionar esta fila.";


                        //    lstCliente.Add(sClaveCliente);
                        //lstCliente.Add(sNombreCliente);
                        //lstCliente.Add(sMatriculag);

                        lblClaveCliente.Text = "Clave cliente: " + sClaveCliente;
                        lblNombreCliente.Text = "Nombre cliente: " + sNombreCliente;
                        lblMatricula.Text = "Matrícula: " + sMatriculag;

                        //    sContrato = sContratog;
                        //    mpePeriodo.Show();
                        pnlRubros.Visible = true;

                        if (eGetImagenesMatricula != null)
                            eGetImagenesMatricula(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            mpeArchivo.Show();
        }

        protected void btnAgregarPDF_Click(object sender, EventArgs e)
        {
            mpeArchivoPDF.Show();
        }

        protected void btnAceptarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuArchivo.HasFile)
                {
                    if (ValidaExtension(Path.GetExtension(fuArchivo.FileName)))
                    {
                        sTituloImagen = txtDescripcionDoc.Text;
                        sNombreArchivo = fuArchivo.FileName;
                        sExtensionImagen = Path.GetExtension(fuArchivo.FileName);
                        arImagen = fuArchivo.FileBytes;

                        mpeArchivo.Hide();

                        if (eSaveImagenesMatricula != null)
                            eSaveImagenesMatricula(sender, e);

                        if (eGetImagenesMatricula != null)
                            eGetImagenesMatricula(sender, e);
                    }
                    else
                    {
                        MostrarMensaje("Este tipo de archivo no se puede anexar como comprobante, favor de verificar", "Aviso");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAceptarArchivoPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuArchivoPDF.HasFile)
                {
                    if (ValidaExtensionPDF(Path.GetExtension(fuArchivoPDF.FileName)))
                    {
                        sTituloImagen = txtDescPDF.Text;
                        sNombreArchivo = fuArchivoPDF.FileName;
                        sExtensionImagen = Path.GetExtension(fuArchivoPDF.FileName);
                        arImagen = fuArchivoPDF.FileBytes;

                        mpeArchivo.Hide();

                        if (eSaveImagenesMatricula != null)
                            eSaveImagenesMatricula(sender, e);

                        if (eGetImagenesMatricula != null)
                            eGetImagenesMatricula(sender, e);
                    }
                    else
                    {
                        MostrarMensaje("Este tipo de archivo no se puede anexar como comprobante, favor de verificar", "Aviso");
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region METODOS

        private bool ValidaExtension(string sExtension)
        {
            try
            {
                bool ban = false;
                switch (sExtension)
                {
                    case ".jpg":
                        ban = true;
                        break;
                    case ".jpeg":
                        ban = true;
                        break;
                    case ".gif":
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

        private bool ValidaExtensionPDF(string sExtension)
        {
            try
            {
                bool ban = false;
                switch (sExtension)
                {
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

        public void LLenaClientes(DataTable dt)
        {
            try
            {
                gvClientes.DataSource = dt;
                gvClientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LLenaImagenesMatricula(DataSet ds)
        {
            try
            {
                dtImagenes = ds;

                gvImagenes.DataSource = ds.Tables[0];
                gvImagenes.DataBind();

                gvImagenesPDF.DataSource = ds.Tables[1];
                gvImagenesPDF.DataBind();
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
        #endregion

        #region VARIABLES Y PROPIEDADES

        ImagenesMatricula_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public event EventHandler eSaveImagenesMatricula;
        public event EventHandler eGetImagenesMatricula;

        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["VClientes"]; }
            set { ViewState["VClientes"] = value; }
        }

        public DataSet dtImagenes
        {
            get { return (DataSet)ViewState["VImagenes"]; }
            set { ViewState["VImagenes"] = value; }
        }

        public string sMatricula
        {
            get { return (string)ViewState["VMatricula"]; }
            set { ViewState["VMatricula"] = value; }
        }

        public string sNombreArchivo
        {
            get { return (string)ViewState["VNombreArchivo"]; }
            set { ViewState["VNombreArchivo"] = value; }
        }

        public string sTituloImagen
        {
            get { return (string)ViewState["VsTituloImagen"]; }
            set { ViewState["VsTituloImagen"] = value; }
        }

        public string sExtensionImagen
        {
            get { return (string)ViewState["VSExtension"]; }
            set { ViewState["VSExtension"] = value; }
        }

        public int iIdAeronave
        {
            get { return (int)ViewState["VSIdAeronave"]; }
            set { ViewState["VSIdAeronave"] = value; }
        }

        public int iIdImagen
        {
            get { return (int)ViewState["VSIdImagen"]; }
            set { ViewState["VSIdImagen"] = value; }
        }

        public byte[] arImagen
        {
            get { return (byte[])ViewState["VSImagen"]; }
            set { ViewState["VSImagen"] = value; }
        }

        //private byte[] _bArchivo = new byte[1];

        public object[] oArrImagenMat
        {
            get
            {
                return new object[] {
                                        "@IdAeronave", iIdAeronave,
                                        "@TituloImg", sTituloImagen, 
                                        "@NombreImagen", sNombreArchivo,
                                        "@Extension", sExtensionImagen,
                                        "@Imagen", arImagen,
                                        "@UsuarioCreacion", Utils.GetUser
                                    };
            }
        }

        public object[] oArray
        {
            get
            {
                string sEstatus = string.Empty;
                string sClaveContrato = string.Empty;
                string sClaveCliente = string.Empty;
                string sMatricula = string.Empty;

                switch (ddlOpcBus.SelectedValue.S())
                {
                    case "0":
                    case "1":
                        sEstatus = ddlOpcBus.SelectedValue.S();
                        break;
                    case "2":
                        sEstatus = "2";
                        sClaveCliente = txtBusqueda.Text.S();
                        break;
                    case "3":
                        sEstatus = "2";
                        sClaveContrato = txtBusqueda.Text.S();
                        break;
                    case "4":
                        sEstatus = "2";
                        sMatricula = txtBusqueda.Text.S();
                        break;
                }

                return new object[]
                {

                    "@ClaveCliente", sClaveCliente,
                    "@ClaveContrato", sClaveContrato,
                    "@Estatus", sEstatus,
                    "@Matricula", sMatricula
                };
            }
        }

        #endregion
    }
}