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
using ClientesCasa.Objetos;
using ClientesCasa.Clases;
using System.IO;

namespace ClientesCasa.Views.Catalogos
{
    public partial class frmContratos : System.Web.UI.Page, IViewContratos
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Contratos_Presenter(this, new DBContratos());

            if (!IsPostBack)
            {
                if (eGetPaises != null)
                    eGetPaises(sender, e);

                CargaPaises(ddlPais);
                CargaPaises(ddlPaisDE);

                if (eGetDatosIniciales != null)
                    eGetDatosIniciales(sender, e);
            }

            if (Request[txtFechaContrato.UniqueID] != null)
            {
                if (Request[txtFechaContrato.UniqueID].Length > 0)
                {
                    txtFechaContrato.Text = Request[txtFechaContrato.UniqueID];
                }
            }

            if (Request[txtFechaFinContrato.UniqueID] != null)
            {
                if (Request[txtFechaFinContrato.UniqueID].Length > 0)
                {
                    txtFechaFinContrato.Text = Request[txtFechaFinContrato.UniqueID];
                }
            }


            if (Request[txtFechaInicioSeguro.UniqueID] != null)
            {
                if (Request[txtFechaInicioSeguro.UniqueID].Length > 0)
                {
                    txtFechaInicioSeguro.Text = Request[txtFechaInicioSeguro.UniqueID];
                }
            }

            if (Request[txtFechaFinSeguro.UniqueID] != null)
            {
                if (Request[txtFechaFinSeguro.UniqueID].Length > 0)
                {
                    txtFechaFinSeguro.Text = Request[txtFechaFinSeguro.UniqueID];
                }
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eObjSelected != null)
                    eObjSelected(sender, e);
            }
            catch (Exception ex)
            {
                //Guarda Bitácora
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
                        e.Row.Attributes.Add("title", "De clic aqui para selecciona un cliente");
                        e.Row.Attributes.Add("OnMouseOver", "On(this);");
                        e.Row.Attributes.Add("OnMouseOut", "Off(this);");
                        e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gvClientes, "Select$" + e.Row.RowIndex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmContratos.aspx", "frmContratos.aspx.cs", "gvClientes_RowDataBound", "Busqueda de Clientes");
            }
        }
        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sIdCliente = string.Empty;
                string sNumCliente = string.Empty;
                string sNombreCliente = string.Empty;
                string sRazonSocial = string.Empty;
                string sRFC = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        row.ToolTip = string.Empty;
                        sIdCliente = gvClientes.DataKeys[row.RowIndex]["IdCliente"].S();
                        sNombreCliente = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        ban = true;
                    }
                    else
                    {
                        row.ToolTip = "Clic para seleccionar esta fila.";
                    }
                }

                if (ban)
                {
                    iIdCliente = sIdCliente;
                    pnlBusqueda.Visible = false;
                    pnlAltaClientes.Visible = true;
                    LlenaDatosCliente();

                    iIdContrato = 0;
                    iIdIntercambio = 0;
                }
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmContratos.aspx", "frmContratos.aspx.cs", "gvClientes_SelectedIndexChanged", "Busqueda de Clientes");
            }
        }
        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dtClientes;
                gvClientes.PageIndex = e.NewPageIndex;
                LoadClientes(dt);
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bDireccionEnvio = false;
                iIdPais = ddlPais.SelectedValue.S().I();

                if (eGetEstadosPorPais != null)
                    eGetEstadosPorPais(ddlEstado, e);
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlPaisDE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bDireccionEnvio = true;
                iIdPais = ddlPaisDE.SelectedValue.S().I();

                if (eGetEstadosPorPais != null)
                    eGetEstadosPorPais(ddlEstadoDE, e);
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            LimpiaCamposClientes();
            pnlAltaClientes.Visible = false;
            pnlBusqueda.Visible = true;
        }
        protected void btnAgregarContrato_Click(object sender, EventArgs e)
        {
            try
            {
                iIdContrato = 0;
                LimpiaCamposContratos();
                readClaveCliente.Text = txtClaveCliente.Text;
                pnlAltaClientes.Visible = false;
                pnlContratos.Visible = true;

                gvIntercambio.DataSource = null;
                gvIntercambio.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvContratos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv != null)
                {
                    string sIdContrato = gv.DataKeys[e.CommandArgument.S().I()]["IdContrato"].S();
                    iIdContrato = sIdContrato.S().I();

                    if (e.CommandName == "Editar")
                    {
                        readClaveCliente.Text = oCliente.sNumCliente.S();

                        if (eGetContratoDetalle != null)
                            eGetContratoDetalle(sender, e);

                        readClaveCliente.Text = txtClaveCliente.Text;
                        pnlAltaClientes.Visible = false;
                        pnlContratos.Visible = true;
                    }

                    if (e.CommandName == "Eliminar")
                    {
                        if (eSetEliminaContrato != null)
                            eSetEliminaContrato(sender, e);

                        MostrarMensaje("El contrato se canceló de manera correcta.", "Aviso");

                        if (eGetConsultaContratos != null)
                            eGetConsultaContratos(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmContratos.aspx", "frmContratos.aspx.cs", "gvContratos_RowCommand", "Edición de contratos");
            }
        }
        protected void btnAgregaIntercambio_Click(object sender, EventArgs e)
        {
            try
            {
                iIdIntercambio = 0;
                LimpiaCamposIntercambio();
                pnlContratos.Visible = false;
                pnlAgregaintercambios.Visible = true;

                if (eGetGrupoModelo != null)
                    eGetGrupoModelo(sender, e);
            }
            catch (Exception)
            {

            }
        }
        protected void btnCancelarIntercambio_Click(object sender, EventArgs e)
        {
            LlenaIntercambios(new DBContratos().DBGetIntercambiosContratos(iIdContrato));
            LimpiaCamposIntercambio();
            pnlContratos.Visible = true;
            pnlAgregaintercambios.Visible = false;
        }
        protected void btnRegresarContratos_Click(object sender, EventArgs e)
        {
            LimpiaCamposContratos();

            if (eGetConsultaContratos != null)
                eGetConsultaContratos(sender, e);

            pnlAltaClientes.Visible = true;
            pnlContratos.Visible = false;
        }
        protected void gvIntercambio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv != null)
                {
                    iIdIntercambio = gv.DataKeys[e.CommandArgument.S().I()]["IdIntercambio"].S().I();

                    if (e.CommandName == "Editar")
                    {
                        foreach (DataRow row in dtIntercambios.Rows)
                        {
                            if (row["IdIntercambio"].S() == iIdIntercambio.S())
                            {
                                ClienteContrato oInter = new ClienteContrato();
                                oInter.iIntercambioId = iIdIntercambio;
                                oInter.iIntercambioGrupoModeloId = row["IdGrupoModelo"].S().I();
                                oInter.dContratoIntercambiosFactor = row["Factor"].S().D();
                                oInter.dContratoIntercambiosEspera = row["Espera"].S().D();
                                oInter.dContratoIntercambiosPernocta = row["Pernocta"].S().D();
                                oInter.dContratoIntercambiosValor = row["Valor"].S().D();
                                oInter.iContratoIntercambiosGalones = row["Galones"].S().I();
                                oInter.dContratoIntercambiosTarifaInternacional = row["CostoDirectoInt"].S().D();
                                oInter.dContratoIntercambiosTarifaNacional = row["CostoDirectoNal"].S().D();
                                oInter.dContratoIntercambiosFerry = row["Ferry"].S().D();
                                oInter.bContratoIntercambiosAplicaFerry = row["AplicaFerry"].S().B();

                                pnlContratos.Visible = false;
                                pnlAgregaintercambios.Visible = true;

                                if (eGetGrupoModelo != null)
                                    eGetGrupoModelo(sender, e);

                                oIntercambio = oInter;
                            }
                        }
                    }

                    if (e.CommandName == "Eliminar")
                    {
                        if (eSetEliminaIntercambio != null)
                            eSetEliminaIntercambio(sender, e);

                        MostrarMensaje("El intercambio se eliminó de manera correcta.", "Aviso");

                        LlenaIntercambios(new DBContratos().DBGetIntercambiosContratos(iIdContrato));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                iIdCliente = "0";
                iIdContrato = 0;
                iIdIntercambio = 0;
                pnlBusqueda.Visible = false;
                LimpiaCamposClientes();
                pnlAltaClientes.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(iIdCliente) || iIdCliente.S() == "0")
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnContratoAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdContrato == 0)
                {
                    if (eSaveContrato != null)
                        eSaveContrato(sender, e);
                }
                else
                {
                    if (eUpaContrato != null)
                        eUpaContrato(sender, e);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnIntercambioAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdIntercambio == 0)
                {
                    if (eSaveIntercambio != null)
                        eSaveIntercambio(sender, e);
                }
                else
                {
                    if (eUpaIntercambio != null)
                        eUpaIntercambio(sender, e);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void imbAeronave_Click(object sender, ImageClickEventArgs e)
        {
            if (eGetMatriculas != null)
                eGetMatriculas(sender, e);

            gvMatriculas.DataSource = dtMatriculas;
            gvMatriculas.DataBind();

            mpeMatricula.Show();
        }
        protected void btnAceptarMatriculas_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMat.Text = string.Empty;
                bool ban = false;

                foreach (GridViewRow row in gvMatriculas.Rows)
                {
                    RadioButton rb = (RadioButton)row.FindControl("rbSelecciona");
                    if (rb != null)
                    {
                        if (rb.Checked)
                        {
                            ban = true;
                            break;
                        }
                    }
                }

                if (ban)
                {
                    foreach (GridViewRow row in gvMatriculas.Rows)
                    {
                        RadioButton rb = (RadioButton)row.FindControl("rbSelecciona");
                        if (rb != null)
                        {
                            if (rb.Checked)
                            {
                                txtAeronaveSerie.Text = gvMatriculas.Rows[row.RowIndex].Cells[1].Text.S();
                                ReadMatricula.Text = gvMatriculas.Rows[row.RowIndex].Cells[2].Text.S();

                            }
                        }
                    }

                    mpeMatricula.Hide();
                }
                else
                {
                    mpeMatricula.Show();
                    lblErrorMat.Text = "Debe seleccionar una aeronave para vincular al contrato, favor de verificar";
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                txtDescripcionDoc.Text = string.Empty;
                mpeArchivo.Show();
            }
            catch (Exception ex)
            {

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
                        oComprobante = new Comprobante();
                        oComprobante.sNumeroReporte = txtDescripcionDoc.Text.S();
                        oComprobante.iIdGasto = iIdContrato;
                        oComprobante.sNombreArchivo = fuArchivo.FileName;
                        oComprobante.sExtension = Path.GetExtension(fuArchivo.FileName);
                        oComprobante.bArchivo = fuArchivo.FileBytes;

                        mpeArchivo.Hide();

                        if (eSaveDocumentoContrato != null)
                            eSaveDocumentoContrato(sender, e);

                        if (eGetDocumentosContrato != null)
                            eGetDocumentosContrato(sender, e);
                    }
                    else
                    {
                        MostrarMensaje("Este tipo de archivo no se puede anexar como comprobante, favor de verificar", "Aviso");
                    }
                }
                else
                    MostrarMensaje("Se debe seleccionar un archivo a subir, favor de verificar", "Aviso");
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gvArchivos = (GridView)sender;
                iIdDocumento = gvArchivos.DataKeys[e.CommandArgument.S().I()]["IdDocumento"].S().I();
                string sExtension = string.Empty;
                string sNombreArch = string.Empty;

                switch (e.CommandName)
                {
                    case "Descargar":

                        DataRow[] drs = dtDocumentos.Select("IdDocumento = " + iIdDocumento);

                        if (drs != null && drs.Length > 0)
                        {
                            sNombreArch = drs[0]["NombreArchivo"].S();
                            sExtension = drs[0]["Extension"].S();

                            byte[] bPDF = (byte[])drs[0]["Archivo"];
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

                        if (eSetEliminaDocumento != null)
                            eSetEliminaDocumento(sender, e);

                        if (eGetDocumentosContrato != null)
                            eGetDocumentosContrato(sender, e);

                        break;

                    case "ViewDoc":
                        DataRow[] drFile = dtDocumentos.Select("IdDocumento = " + iIdDocumento);

                        if (drFile != null && drFile.Length > 0)
                        {
                            sNombreArch = drFile[0]["NombreArchivo"].S();
                            sExtension = drFile[0]["Extension"].S();

                            byte[] bPDF = (byte[])drFile[0]["Archivo"];
                            if (bPDF != null)
                            {
                                Session["FileContrato"] = bPDF;
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmVistaPreviaDoc.aspx',this.target, 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region METODOS
        public void LoadClientes(DataTable dt)
        {
            dtClientes = dt;
            gvClientes.DataSource = dt;
            gvClientes.DataBind();
        }
        public void LlenaDatosCliente()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaPaises(DropDownList ddl)
        {
            try
            {
                ddl.DataSource = dtPaises;
                ddl.DataValueField = "PaisId";
                ddl.DataTextField = "PaisNombre";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaEstados(DropDownList ddl, DataTable dt)
        {
            try
            {
                if (dt != null)
                {
                    ddl.DataSource = dt;
                    ddl.DataValueField = "EstadoId";
                    ddl.DataTextField = "EstadoNombre";
                    ddl.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaIntercambios(DataTable dt)
        {
            dtIntercambios = dt.Copy();
            gvIntercambio.DataSource = dt;
            gvIntercambio.DataBind();
        }
        private void LimpiaCamposClientes()
        {
            try
            {
                txtClaveCliente.Text = string.Empty;
                txtNombreClienteA.Text = string.Empty;
                txtRazonSocial.Text = string.Empty;
                rblTieneRFC.SelectedValue = "1";
                txtRFC.Text = string.Empty;
                ddlTipoContribuyente.SelectedIndex = -1;
                chkActivo.Checked = true;
                txtTelefonoCliente.Text = string.Empty;
                txtFax.Text = string.Empty;
                txtCorreoElectronico.Text = string.Empty;
                ddlSector.SelectedIndex = -1;
                ddlPais.SelectedIndex = -1;
                ddlEstado.SelectedIndex = -1;
                txtDireccion.Text = string.Empty;
                txtCiudad.Text = string.Empty;
                txtCP.Text = string.Empty;
                ddlPaisDE.SelectedIndex = -1;
                ddlEstadoDE.SelectedIndex = -1;
                txtDireccionDE.Text = string.Empty;
                txtCiudadDE.Text = string.Empty;
                txtCPDE.Text = string.Empty;
                gvContratos.DataSource = null;
                gvContratos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiaCamposIntercambio()
        {
            ddlGrupoModelo.SelectedValue = null;
            txtIntercambioFactor.Text = string.Empty;
            txtIntercambioEspera.Text = string.Empty;
            txtIntercambioPernocta.Text = string.Empty;
            txtIntercambioValor.Text = string.Empty;
            txtIntercambioGalones.Text = string.Empty;
            txtIntercambioTarifaInter.Text = string.Empty;
            txtIntercambioTarifaNac.Text = string.Empty;
            txtIntercambioFerry.Text = string.Empty;
            rblAplicaFerry.SelectedValue = "0";
        }
        private void LimpiaCamposContratos()
        {
            txtClaveContrato.Text = string.Empty;
            txtAeronaveSerie.Text = string.Empty;
            txtPorcentPart.Text = string.Empty;
            txtHorasContratadas.Text = string.Empty;
            rblAplcaIntercambios.SelectedValue = "1";
            rblFactorIntercambio.SelectedValue = "1";
            ddlEstatusContrato.SelectedIndex = -1;
            txtFechaContrato.Text = string.Empty;
        }
        public void LoadGrupoModelo(DataTable dt)
        {
            try
            {
                ddlGrupoModelo.DataSource = dt;
                ddlGrupoModelo.DataTextField = "Descripcion";
                ddlGrupoModelo.DataValueField = "GrupoModeloId";
                ddlGrupoModelo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            //string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);

            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }
        public void CargaDatosIniciales(DataTable dtSectores, DataTable dtEstatusContrato)
        {
            try
            {
                ddlSector.DataSource = dtSectores;
                ddlSector.DataValueField = "SectorId";
                ddlSector.DataTextField = "SectorDescripcion";
                ddlSector.DataBind();

                ddlEstatusContrato.DataSource = dtEstatusContrato;
                ddlEstatusContrato.DataValueField = "IdEstatus";
                ddlEstatusContrato.DataTextField = "Descripcion";
                ddlEstatusContrato.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaContratos(DataTable dt)
        {
            gvContratos.DataSource = dt;
            gvContratos.DataBind();
        }
        private bool ValidaExtension(string sExtension)
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
        public void LoadDocumentosContratos(DataTable dt)
        {
            try
            {
                gvDocumentos.DataSource = dt;
                gvDocumentos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        Contratos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetPaises;
        public event EventHandler eGetEstadosPorPais;
        public event EventHandler eGetContratoDetalle;
        public event EventHandler eGetGrupoModelo;
        public event EventHandler eSaveContrato;
        public event EventHandler eUpaContrato;
        public event EventHandler eSaveIntercambio;
        public event EventHandler eUpaIntercambio;
        public event EventHandler eGetMatriculas;
        public event EventHandler eGetDatosIniciales;
        public event EventHandler eSetEliminaContrato;
        public event EventHandler eSetEliminaIntercambio;
        public event EventHandler eGetConsultaContratos;

        public event EventHandler eGetDocumentosContrato;
        public event EventHandler eSetEliminaDocumento;
        public event EventHandler eSaveDocumentoContrato;

        public event EventHandler eValidaContratoExistente;

        public object[] oArray
        {
            get
            {
                string sEstatus = string.Empty;
                string sClaveContrato = string.Empty;
                string sClaveCliente = string.Empty;
                string sMatricula = string.Empty;

                switch (ddlOpcion.SelectedValue.S())
                {
                    case "0":
                    case "1":
                        sEstatus = ddlOpcion.SelectedValue.S();
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
        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["VClientes"]; }
            set { ViewState["VClientes"] = value; }
        }
        public string iIdCliente
        {
            get { return (string)ViewState["VCliente"]; }
            set { ViewState["VCliente"] = value; }
        }
        public DataTable dtPaises
        {
            get { return (DataTable)ViewState["VSPaises"]; }
            set { ViewState["VSPaises"] = value; }
        }
        public int iIdPais
        {
            get { return ddlPais.SelectedValue.S().I(); }
            set { ViewState["VSIdPais"] = value; }
        }
        public int iIdPaisDE
        {
            get { return ddlPaisDE.SelectedValue.S().I(); }
            set { ViewState["VSIdPaisDE"] = value; }
        }
        public bool bDireccionEnvio
        {
            get { return (bool)ViewState["VSDireccionEnvio"]; }
            set { ViewState["VSDireccionEnvio"] = value; }
        }
        public int iIdContrato
        {
            get { return (int)ViewState["VSIdContrato"]; }
            set { ViewState["VSIdContrato"] = value; }
        }
        public ClienteContrato oCliente
        {
            get
            {
                return new ClienteContrato
                {
                    iIdCliente = String.IsNullOrEmpty(iIdCliente) ? 0 : iIdCliente.S().I(),
                    sNumCliente = txtClaveCliente.Text.S(),
                    sNombreCliente = txtNombreClienteA.Text.S(),
                    sTipoCliente = "A",
                    sRazonSocial = txtRazonSocial.Text.S(),
                    bRFC = rblTieneRFC.SelectedValue == "1" ? true : false,
                    sRFC = rblTieneRFC.SelectedValue == "1" ? txtRFC.Text.S() : string.Empty,
                    sTipoContribuyente = ddlTipoContribuyente.SelectedValue.S(),
                    iActivo = chkActivo.Checked ? 1 : 0,
                    sTelefono = txtTelefonoCliente.Text.S(),
                    sFax = txtFax.Text.S(),
                    sCorreoEletronico = txtCorreoElectronico.Text.S(),
                    iIdSector = ddlSector.SelectedValue.S().I(),
                    iIdPais = ddlPais.SelectedValue.S().I(),
                    iIdEstado = ddlEstado.SelectedValue.S().I(),
                    sDireccion = txtDireccion.Text.S(),
                    sCiudad = txtCiudad.Text.S(),
                    sCP = txtCP.Text.S(),
                    iIdPaisDE = ddlPaisDE.SelectedValue.S().I(),
                    iIdEstadoDE = ddlEstadoDE.SelectedValue.S().I(),
                    sDireccionDE = txtDireccionDE.Text.S(),
                    sCiudadDE = txtCiudadDE.Text.S(),
                    sCPDE = txtCPDE.Text.S(),


                    iIdContrato = iIdContrato,
                    sIdContrato = iIdContrato.S(),
                    sClaveContrato = txtClaveContrato.Text.S(),
                    sAeronaveMatricula = ReadMatricula.Text.S(),
                    sAeronaveSerie = txtAeronaveSerie.Text.S(),
                    iPorcentajePart = txtPorcentPart.Text.S().I(),
                    iHorasContratadas = txtHorasContratadas.Text.S().I(),
                    bAplicaIntercambios = rblAplcaIntercambios.SelectedValue == "1" ? true : false,
                    iFactorIntercambio = rblFactorIntercambio.SelectedValue.S().I(),

                    dtFechaContrato = txtFechaContrato.Text.S() == string.Empty ? null : (DateTime?)txtFechaContrato.Text.S().Dt(),
                    iEstatusContrato = ddlEstatusContrato.SelectedValue.S().I(),

                    iTipoCosto = rbtnTRipoCosto.SelectedValue == "1" ? 1 : 0,

                    iIntercambioId = ViewState["VSIntercambio"] == null ? 0 : iIdIntercambio,
                    iIntercambioGrupoModeloId = ddlGrupoModelo.SelectedValue.S().I(),
                    dContratoIntercambiosFactor = txtIntercambioFactor.Text.S().D(),
                    dContratoIntercambiosEspera = txtIntercambioEspera.Text.S().D(),
                    dContratoIntercambiosPernocta = txtIntercambioPernocta.Text.S().D(),
                    dContratoIntercambiosValor = txtIntercambioValor.Text.S().D(),
                    iContratoIntercambiosGalones = txtIntercambioGalones.Text.S().I(),
                    dContratoIntercambiosTarifaInternacional = txtIntercambioTarifaInter.Text.S().D(),
                    dContratoIntercambiosTarifaNacional = txtIntercambioTarifaNac.Text.S().D(),
                    dContratoIntercambiosFerry = txtIntercambioFerry.Text.S().D(),
                    bContratoIntercambiosAplicaFerry = rblAplicaFerry.SelectedValue == "1" ? true : false,

                    dtFechaFinContrato = txtFechaFinContrato.Text.S().Dt(),
                    dAnticipoContrato = txtAnticipoContrato.Text.Replace(",", "").S().D(),
                    sMonedaAnticipo = ddlMonedaAnticipo.SelectedValue.S(),
                    iTipoServicioConsultoria = rblServicioConsultoria.SelectedValue.S().I(),
                    iTipoTarifa = rblTarifas.SelectedValue.S().I(),
                    iDetalleTipoTarifa = rblDetalleTarifa.SelectedValue.S().I(),
                    sNoPoliza = txtNoPoliza.Text.S(),
                    sEmpresaAseguradora = txtEmpresaAseguradora.Text.S(),
                    dtFechaInicioSeg = txtFechaInicioSeguro.Text.S().Dt(),
                    dtFechaFinSeg = txtFechaFinSeguro.Text.S().Dt(),

                    sUsuario = Utils.GetUser.S()
                };
            }
            set
            {
                ClienteContrato oCat = value as ClienteContrato;
                if (oCat != null)
                {
                    txtClaveCliente.Text = oCat.sNumCliente;
                    txtNombreClienteA.Text = oCat.sNombreCliente;
                    txtRazonSocial.Text = oCat.sRazonSocial;
                    rblTieneRFC.SelectedValue = oCat.bRFC == true ? "1" : "0";
                    txtRFC.Text = oCat.sRFC;

                    if (oCat.sTipoContribuyente != string.Empty)
                        ddlTipoContribuyente.SelectedValue = oCat.sTipoContribuyente;

                    chkActivo.Checked = true;
                    txtTelefonoCliente.Text = oCat.sTelefono;
                    txtFax.Text = oCat.sFax;
                    txtCorreoElectronico.Text = oCat.sCorreoEletronico;

                    if (oCat.iIdSector != -1)
                        ddlSector.SelectedValue = oCat.iIdSector.S();

                    if (oCat.iIdPais > 0)
                    {
                        bDireccionEnvio = false;

                        ddlPais.SelectedValue = oCat.iIdPais.S();
                        if (eGetEstadosPorPais != null)
                            eGetEstadosPorPais(ddlEstado, EventArgs.Empty);
                    }

                    if (oCat.iIdEstado != 0 && oCat.iIdEstado != -1)
                        ddlEstado.SelectedValue = oCat.iIdEstado.S();

                    txtDireccion.Text = oCat.sDireccion.S();
                    txtCiudad.Text = oCat.sCiudad;
                    txtCP.Text = oCat.sCP;

                    if (oCat.iIdPaisDE > 0)
                    {
                        bDireccionEnvio = true;

                        ddlPaisDE.SelectedValue = oCat.iIdPaisDE.S();
                        if (eGetEstadosPorPais != null)
                            eGetEstadosPorPais(ddlEstadoDE, EventArgs.Empty);
                    }

                    if(oCat.iIdEstadoDE != 0 && oCat.iIdEstadoDE != -1)
                        ddlEstadoDE.SelectedValue = oCat.iIdEstadoDE.S();

                    txtDireccionDE.Text = oCat.sDireccionDE;
                    txtCiudadDE.Text = oCat.sCiudadDE;
                    txtCPDE.Text = oCat.sCPDE;

                    rbtnTRipoCosto.SelectedValue = oCat.iTipoCosto.S();

                    LlenaContratos(oCat.dtContratos);
                }
            }
        }
        public ClienteContrato oContrato
        {
            set
            {
                ClienteContrato oCat = value as ClienteContrato;
                if (oCat != null)
                {
                    txtClaveContrato.Text = oCat.sClaveContrato;
                    txtAeronaveSerie.Text = oCat.sAeronaveSerie;
                    ReadMatricula.Text = oCat.sAeronaveMatricula.S();
                    txtPorcentPart.Text = oCat.iPorcentajePart.S();
                    txtHorasContratadas.Text = oCat.iHorasContratadas.S();
                    rblAplcaIntercambios.SelectedValue = oCat.bAplicaIntercambios == true ? "1" : "0";
                    rblFactorIntercambio.SelectedValue = oCat.iFactorIntercambio.S() != "0" ? oCat.iFactorIntercambio.S() : "1";

                    //txtFechaContrato.Text = oCat.dtFechaContrato.S().Substring(0,10);
                    txtFechaContrato.Text = string.Format("{0:yyyy-MM-dd}", oCat.dtFechaContrato.S().Dt());

                    ddlEstatusContrato.SelectedValue = oCat.iEstatusContrato.S();

                    txtFechaFinContrato.Text = oCat.dtFechaFinContrato.S() == "01/01/0001 12:00:00 a. m." ? string.Empty : string.Format("{0:yyyy-MM-dd}", oCat.dtFechaFinContrato.S().Dt());
                    txtAnticipoContrato.Text = oCat.dAnticipoContrato.ToString("N2");

                    if(oCat.sMonedaAnticipo.S() != string.Empty)
                        ddlMonedaAnticipo.SelectedValue = oCat.sMonedaAnticipo.S();

                    rbtnTRipoCosto.SelectedValue = oCat.iTipoCosto.S();

                    if (oCat.iTipoServicioConsultoria > 0)
                        rblServicioConsultoria.SelectedValue = oCat.iTipoServicioConsultoria.S();

                    if (oCat.iTipoTarifa > 0)
                        rblTarifas.SelectedValue = oCat.iTipoTarifa.S();

                    if (oCat.iDetalleTipoTarifa > 0)
                        rblDetalleTarifa.SelectedValue = oCat.iDetalleTipoTarifa.S();

                    txtNoPoliza.Text = oCat.sNoPoliza.S();
                    txtEmpresaAseguradora.Text = oCat.sEmpresaAseguradora.S();
                    txtFechaInicioSeguro.Text = oCat.dtFechaInicioSeg.S() == "01/01/0001 12:00:00 a. m." ? string.Empty : string.Format("{0:yyyy-MM-dd}", oCat.dtFechaInicioSeg.S().Dt());
                    txtFechaFinSeguro.Text = oCat.dtFechaFinSeg.S() == "01/01/0001 12:00:00 a. m." ? string.Empty : string.Format("{0:yyyy-MM-dd}", oCat.dtFechaFinSeg.S().Dt());

                    dtDocumentos = oCat.dtDocumentos;
                    gvDocumentos.DataSource = oCat.dtDocumentos;
                    gvDocumentos.DataBind();
                }
            }
        }
        public ClienteContrato oIntercambio
        {
            set
            {
                ClienteContrato oCat = value as ClienteContrato;
                if (oCat != null)
                {
                    ddlGrupoModelo.SelectedValue = oCat.iIntercambioGrupoModeloId.S() != "0" ? oCat.iIntercambioGrupoModeloId.S() : null;
                    txtIntercambioFactor.Text = oCat.dContratoIntercambiosFactor.S();
                    txtIntercambioEspera.Text = oCat.dContratoIntercambiosEspera.S();
                    txtIntercambioPernocta.Text = oCat.dContratoIntercambiosPernocta.S();
                    txtIntercambioValor.Text = oCat.dContratoIntercambiosValor.S();
                    txtIntercambioGalones.Text = oCat.iContratoIntercambiosGalones.S();
                    txtIntercambioTarifaInter.Text = oCat.dContratoIntercambiosTarifaInternacional.S();
                    txtIntercambioTarifaNac.Text = oCat.dContratoIntercambiosTarifaNacional.S();
                    txtIntercambioFerry.Text = oCat.dContratoIntercambiosFerry.S();
                    rblAplicaFerry.SelectedValue = oCat.bContratoIntercambiosAplicaFerry == true ? "1" : "0";
                }
            }
        }
        public DataTable dtIntercambios
        {
            get { return (DataTable)ViewState["VSIntercambios"]; }
            set { ViewState["VSIntercambios"] = value; }
        }
        public int iIdIntercambio
        {
            get { return (int)ViewState["VSIntercambio"]; }
            set { ViewState["VSIntercambio"] = value; }
        }
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["VSMatriculas"]; }
            set { ViewState["VSMatriculas"] = value; }
        }
        public DateTime? dtFechaInicoContrato
        {
            get { return ViewState["VSFechaInicioContrato"].S().Dt(); }
            set { ViewState["VSFechaInicioContrato"] = value; }
        }
        public Comprobante oComprobante
        {
            get { return (Comprobante)ViewState["VSComprobante"]; }
            set { ViewState["VSComprobante"] = value; }
        }
        public DataTable dtDocumentos
        {
            get { return (DataTable)ViewState["VSDocumentos"]; }
            set { ViewState["VSDocumentos"] = value; }
        }
        public int iIdDocumento
        {
            get { return (int)ViewState["VSIdDocumento"]; }
            set { ViewState["VSIdDocumento"] = value; }
        }
        public bool bExisteClave
        {
            get { return (bool)ViewState["VSExisteClave"]; }
            set { ViewState["VSExisteClave"] = value; }
        }
        public string sClaveContrato
        {
            get
            {
                return txtClaveContrato.Text.S();
            }
        }
        #endregion

        protected void txtClaveContrato_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (eValidaContratoExistente != null)
                    eValidaContratoExistente(sender, e);

                if (bExisteClave)
                {
                    lblErrorClaveContrato.Visible = true;
                    lblErrorClaveContrato.Text = "La clave del contrato no está disponible.";
                }
                else
                {
                    lblErrorClaveContrato.Visible = false;
                    lblErrorClaveContrato.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rblAplcaIntercambios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAplcaIntercambios.SelectedValue == "0")
            {
                rblFactorIntercambio.Enabled = false;
            }
            else
            {
                rblFactorIntercambio.Enabled = true;
            }
        }
    }
}