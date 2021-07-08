using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;

namespace ClientesCasa.Views.SCAF
{
    public partial class frmConsultaBitacoras : System.Web.UI.Page, IViewBitacora
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Bitacora_Presenter(this, new DBBitacora());

            if (!IsPostBack)
            {
                if (eGetMatricula != null)
                    eGetMatricula(sender, e);
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
       
        protected void gvLegs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv != null)
                {
                    sIdBitacoraDis = gv.DataKeys[e.CommandArgument.S().I()]["IdBitacora"].S();
                    sFolioDis = gv.DataKeys[e.CommandArgument.S().I()]["FolioReal"].S();
                    lblFolioDisRes.Text = sIdBitacoraDis;
                    lblOrigenDisRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[1].Text;
                    lblInDisRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[4].Text;
                    lblCombInDisRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[6].Text;
                    lblLegIdDisRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[0].Text;
                    lblDestinoDisRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[2].Text;
                    lblOutDisRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[3].Text;
                    lblCombOutDisRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[5].Text;
                    pnlAgregarDiscrepancias.Visible = true;
                    pnlAltaLeg.Visible = false;


                    if (eGetDiscrepancias != null)
                        eGetDiscrepancias(sender, e);

                
                }
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmContratos.aspx", "frmContratos.aspx.cs", "gvContratos_RowCommand", "Edición de contratos");
            }
        }
        protected void gvPiernas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string sIdCliente = string.Empty;
                //string sNumCliente = string.Empty;
                //string sNombreCliente = string.Empty;
                //string sRazonSocial = string.Empty;
                //string sRFC = string.Empty;

                //bool ban = false;
                //foreach (GridViewRow row in gvClientes.Rows)
                //{
                //    if (row.RowIndex == gvClientes.SelectedIndex)
                //    {
                //        row.ToolTip = string.Empty;
                //        sIdCliente = gvClientes.DataKeys[row.RowIndex]["IdCliente"].S();
                //        sNombreCliente = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                //        ban = true;
                //    }
                //    else
                //    {
                //        row.ToolTip = "Clic para seleccionar esta fila.";
                //    }
                //}

                //if (ban)
                //{
                //    //iIdCliente = sIdCliente;
                //    //pnlBusqueda.Visible = false;
                //    //pnlAltaClientes.Visible = true;
                //    //LlenaDatosCliente();
                //}
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmContratos.aspx", "frmContratos.aspx.cs", "gvClientes_SelectedIndexChanged", "Busqueda de Clientes");
            }
        }
        protected void gvPiernas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dtMatriculas;
                DataTable dtB = (DataTable)dtBitacora;
                gvPiernas.PageIndex = e.NewPageIndex;
                CargaMatricula(dt);
                LoadBitacora(dtBitacora);
                
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //bDireccionEnvio = false;
                //iIdPais = ddlPais.SelectedValue.S().I();

                //if (eGetEstadosPorPais != null)
                //    eGetEstadosPorPais(ddlEstado, e);
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlPaisDE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //    bDireccionEnvio = true;
                //    iIdPais = ddlPaisDE.SelectedValue.S().I();

                //    if (eGetEstadosPorPais != null)
                //        eGetEstadosPorPais(ddlEstadoDE, e);
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            //LimpiaCamposClientes();

        }
        protected void btnRegresar2_Click(object sender, EventArgs e)
        {
            pnlAltaLeg.Visible = false;
            pnlBusqueda.Visible = true;
        }
        protected void btnRegresar3_Click(object sender, EventArgs e)
        {
            pnlAgregarDiscrepancias.Visible = false;
            pnlAltaLeg.Visible = true;
        }
        protected void btnRegresarComp_Click(object sender, EventArgs e)
        {
            pnlAgregarComponente.Visible = false;
            pnlAgregarDiscrepancias.Visible = true;
        }
        protected void btnAgregarContrato_Click(object sender, EventArgs e)
        {
            try
            {
                //iIdContrato = 0;
                //LimpiaCamposContratos();
                //readClaveCliente.Text = txtClaveCliente.Text;
                //pnlAltaClientes.Visible = false;
                //pnlContratos.Visible = true;

                //gvIntercambio.DataSource = null;
                //gvIntercambio.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvPiernas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv != null)
                {
                    sFolio = gv.DataKeys[e.CommandArgument.S().I()]["FolioReal"].S();
                    sMatricula = gv.DataKeys[e.CommandArgument.S().I()]["AeronaveMatricula"].S();
                    sIdBitacora = gv.DataKeys[e.CommandArgument.S().I()]["IdBitacora"].S();
                    sIdSIC = gv.DataKeys[e.CommandArgument.S().I()]["CopilotoId"].S();
                    sIdPIC = gv.DataKeys[e.CommandArgument.S().I()]["PilotoId"].S();

                    if (e.CommandName == "Editar")
                    {
                        LlenaDatosBitacora();
                        pnlAltaLeg.Visible = true;
                        pnlBusqueda.Visible = false;
                    }

                    //if (e.CommandName == "Eliminar")
                    //{
                    //    if (eSetEliminaContrato != null)
                    //        eSetEliminaContrato(sender, e);

                    //    MostrarMensaje("El contrato se canceló de manera correcta.", "Aviso");

                    //    if (eGetConsultaContratos != null)
                    //        eGetConsultaContratos(sender, e);
                    //}
                }
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmContratos.aspx", "frmContratos.aspx.cs", "gvContratos_RowCommand", "Edición de contratos");
            }
        }
        protected void gvDiscrepancias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv != null)
                {
                    sIdDiscrepancia = gvDiscrepancias.DataKeys[e.CommandArgument.S().I()]["IdDiscrepancia"].S();
                    sIdDiscrepanciaComp = gvDiscrepancias.DataKeys[e.CommandArgument.S().I()]["IdDiscrepancia"].S();
                    sIdBitacoraDis = gvDiscrepancias.DataKeys[e.CommandArgument.S().I()]["IdBitacora"].S();

                    if (e.CommandName == "Editar")
                    {
                        lblTituloDiscrepancia.Text = "Modificar Discrepancia";
                        btnGuardarDiscrepancia.Text = "Modificar";
                        OcultarEtiquetasAviso();
                        mpeDiscrepancia.Show();

                        if (eSearchObjDiscrepancia != null)
                            eSearchObjDiscrepancia(sender, e);

                    }

                    if (e.CommandName == "Eliminar")
                    {
                        if (eDeleteObjDiscrepancia != null)
                            eDeleteObjDiscrepancia(sender, e);
                        if (eGetDiscrepancias != null)
                            eGetDiscrepancias(sender, e);
                        MostrarMensaje("La discrepancia se eliminó de manera correcta.", "Aviso");
                    }

                    if (e.CommandName == "Componente")
                    {
                        lblTipoReporteRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[0].Text;
                        lblCodigoATARes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[4].Text;
                        lblFechaDiscrepanciaRes.Text = gv.Rows[e.CommandArgument.S().I()].Cells[2].Text;
                        pnlAgregarDiscrepancias.Visible = false;
                        pnlAgregarComponente.Visible = true;

                        if (eGetComponentes != null)
                            eGetComponentes(sender, e);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
       
        protected void gvComponentes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (gv != null)
                {
                    sIdComponente = gvComponentes.DataKeys[e.CommandArgument.S().I()]["IdComponente"].S();
                    sIdDiscrepanciaComp = gvComponentes.DataKeys[e.CommandArgument.S().I()]["IdDiscrepancia"].S();

                    if (e.CommandName == "Editar")
                    {
                        lblTituloComponente.Text = "Modificar Componente";
                        btnGuardarComponente.Text = "Modificar";
                        OcultarEtiquetasAvisoComponentes();
                        mpeComponente.Show();

                        if (eSearchObjComponente != null)
                            eSearchObjComponente(sender, e);

                    }

                    if (e.CommandName == "Eliminar")
                    {
                        if (eDeleteObjComponente != null)
                            eDeleteObjComponente(sender, e);
                        if (eGetComponentes != null)
                            eGetComponentes(sender, e);
                        MostrarMensaje("El componente se eliminó de manera correcta.", "Aviso");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAgregaIntercambio_Click(object sender, EventArgs e)
        {
            try
            {
                //iIdIntercambio = 0;
                //LimpiaCamposIntercambio();
                //pnlContratos.Visible = false;
                //pnlAgregaintercambios.Visible = true;

                //if (eGetGrupoModelo != null)
                //    eGetGrupoModelo(sender, e);
            }
            catch (Exception)
            {

            }
        }
        protected void btnCancelarIntercambio_Click(object sender, EventArgs e)
        {
            //    LlenaIntercambios(new DBContratos().DBGetIntercambiosContratos(iIdContrato));
            //    LimpiaCamposIntercambio();
            //    pnlContratos.Visible = true;
            //    pnlAgregaintercambios.Visible = false;
        }
        protected void btnRegresarContratos_Click(object sender, EventArgs e)
        {
            //LimpiaCamposContratos();

            //if (eGetConsultaContratos != null)
            //    eGetConsultaContratos(sender, e);

            //pnlAltaClientes.Visible = true;
            //pnlContratos.Visible = false;
        }

        protected void gvIntercambio_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //        foreach (DataRow row in dtIntercambios.Rows)
            //        {
            //            if (row["IdIntercambio"].S() == iIdIntercambio.S())
            //            {
            //                ClienteContrato oInter = new ClienteContrato();
            //                oInter.iIntercambioId = iIdIntercambio;
            //                oInter.iIntercambioGrupoModeloId = row["IdGrupoModelo"].S().I();
            //                oInter.dContratoIntercambiosFactor = row["Factor"].S().D();
            //                oInter.dContratoIntercambiosEspera = row["Espera"].S().D();
            //                oInter.dContratoIntercambiosPernocta = row["Pernocta"].S().D();
            //                oInter.dContratoIntercambiosValor = row["Valor"].S().D();
            //                oInter.iContratoIntercambiosGalones = row["Galones"].S().I();
            //                oInter.dContratoIntercambiosTarifaInternacional = row["CostoDirectoInt"].S().D();
            //                oInter.dContratoIntercambiosTarifaNacional = row["CostoDirectoNal"].S().D();
            //                oInter.dContratoIntercambiosFerry = row["Ferry"].S().D();
            //                oInter.bContratoIntercambiosAplicaFerry = row["AplicaFerry"].S().B();

            //                pnlContratos.Visible = false;
            //                pnlAgregaintercambios.Visible = true;

            //                if (eGetGrupoModelo != null)
            //                    eGetGrupoModelo(sender, e);

            //                oIntercambio = oInter;
            //            }
            //        }

        }
        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                //iIdCliente = "0";
                //pnlBusqueda.Visible = false;
                //LimpiaCamposClientes();
                //pnlAltaClientes.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                bool ban = true;

                if (txtMotor1.Text.S() == string.Empty)
                {
                    lblReqMotorIAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqMotorIAvi.Visible = false;

                if (txtMotor2.Text.S() == string.Empty)
                {
                    lblReqMotorIIAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqMotorIIAvi.Visible = false;

                if (txtPlaneador.Text.S() == string.Empty)
                {
                    lblReqPlaneadorAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqPlaneadorAvi.Visible = false;

                if (txtCMotor1.Text.S() == string.Empty)
                {
                    lblReqCMotorIAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqCMotorIAvi.Visible = false;

                if (txtCMotor2.Text.S() == string.Empty)
                {
                    lblReqCMotorIIAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqCMotorIIAvi.Visible = false;

                if (txtAterrizajes.Text.S() == string.Empty)
                {
                    lblReqAterrizajesAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqAterrizajesAvi.Visible = false;

                if (txtMecanicoResp.Text.S() == string.Empty)
                {
                    lblReqMecanicoAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqMecanicoAvi.Visible = false;

                if (ban)
                {
                    if (!bBandera)
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
                else
                {
                    MostrarMensaje("Exiten campos obligatorios sin captura", "Aviso");
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAgregarDis_Click(object sender, EventArgs e)
        {
            lblTituloDiscrepancia.Text = "Agregar Discrepancia";
            btnGuardarDiscrepancia.Text = "Guardar";
            LimpiaModalDiscrepancias();
            OcultarEtiquetasAviso();
            mpeDiscrepancia.Show();
        }
        protected void btnAgregarComponente_Click(object sender, EventArgs e)
        {
            lblTituloComponente.Text = "Agregar Componente";
            btnGuardarComponente.Text = "Guardar";
            LimpiaModalComponentes();
            OcultarEtiquetasAvisoComponentes();
            mpeComponente.Show();
        }
        protected void btnGuardarDiscrepancia_Click(object sender, EventArgs e)
        {

            try
            {
                bool ban = true;

                if (ddlOrigen.SelectedValue.S() == "0")
                {
                    lblReqOrigenDisAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqOrigenDisAvi.Visible = false;

                if (ddlTipoRep.SelectedValue.S() == "0")
                {
                    lblReqTipoRepDisAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqTipoRepDisAvi.Visible = false;

                if (txtAccionCorrectiva.Text.S() == string.Empty)
                {
                    lblReqAccionCorrectivaAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqAccionCorrectivaAvi.Visible = false;

                if (txtDescripcion.Text.S() == string.Empty)
                {
                    lblReqDisDescripcionAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqDisDescripcionAvi.Visible = false;

                if (txtBase.Text.S() == string.Empty)
                {
                    lblReqDisBaseAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqDisBaseAvi.Visible = false;

                if (txtCodigoAta.Text.S() == string.Empty)
                {
                    lblReqCodigoAtaAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqCodigoAtaAvi.Visible = false;

                if (txtMecanicoDis.Text.S() == string.Empty)
                {
                    lblReqMecanicoDisAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqMecanicoDisAvi.Visible = false;

                if (txtReferenciaRep.Text.S() == string.Empty)
                {
                    lblReqReferenciaRepAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqReferenciaRepAvi.Visible = false;

                if (txtFechaDis.Text.S() == string.Empty)
                {
                    lblReqFechaDisAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqFechaDisAvi.Visible = false;

                if (txtFechaAtencion.Text.S() == string.Empty)
                {
                    lblReqFechaAtencionAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqFechaAtencionAvi.Visible = false;

                if (ddlDiagnostico.SelectedValue.S() == "0")
                {
                    lblReqDiagnosticoAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqDiagnosticoAvi.Visible = false;

                if (ban)
                {

                    if (btnGuardarDiscrepancia.Text == "Guardar")
                    {
                        if (eNewObjDiscrepancia != null)
                            eNewObjDiscrepancia(sender, e);
                    }
                    else if (btnGuardarDiscrepancia.Text == "Modificar")
                    {
                        if (eEditObjDiscrepancia != null)
                            eEditObjDiscrepancia(sender, e);
                    }
                    if (eGetDiscrepancias != null)
                        eGetDiscrepancias(sender, e);
                }
                else
                {
                    MostrarMensaje("Exiten campos obligatorios sin captura", "Aviso");
                    mpeDiscrepancia.Show();
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void btnGuardarComponente_Click(object sender, EventArgs e)
        {
            try
            {
                bool ban = true;

                if (txtNombreComponente.Text.S() == string.Empty)
                {
                    lblReqNombreComponenteAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqNombreComponenteAvi.Visible = false;


                if (txtNoParteRemovido.Text.S() == string.Empty)
                {
                    lblReqNoParteRemovidoAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqNoParteRemovidoAvi.Visible = false;


                if (txtNoParteInstalado.Text.S() == string.Empty)
                {
                    lblReqNoParteInstaladoAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqNoParteInstaladoAvi.Visible = false;

                if (txtNoSerieRemovido.Text.S() == string.Empty)
                {
                    lblReqNoSerieRemovidoAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqNoSerieRemovidoAvi.Visible = false;

                if (txtNoSerieInstalado.Text.S() == string.Empty)
                {
                    lblReqNoSerieInstaladoAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqNoSerieInstaladoAvi.Visible = false;

                if (txtPosicionComponente.Text.S() == string.Empty)
                {
                    lblReqPosicionComponenteAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqPosicionComponenteAvi.Visible = false;

                if (txtRazonServicio.Text.S() == string.Empty)
                {
                    lblReqRazonServicioAvi.Visible = true;
                    ban = false;
                }
                else
                    lblReqRazonServicioAvi.Visible = false;


                if (ban)
                {

                    if (btnGuardarComponente.Text == "Guardar")
                    {
                        if (eNewObjComponente != null)
                            eNewObjComponente(sender, e);
                    }
                    else if (btnGuardarComponente.Text == "Modificar")
                    {
                        if (eEditObjComponente != null)
                            eEditObjComponente(sender, e);
                    }
                    if (eGetComponentes != null)
                        eGetComponentes(sender, e);
                }
                else
                {
                    MostrarMensaje("Exiten campos obligatorios sin captura", "Aviso");
                    mpeComponente.Show();
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
                //if (iIdIntercambio == 0)
                //{
                //    if (eSaveIntercambio != null)
                //        eSaveIntercambio(sender, e);
                //}
                //else
                //{
                //    if (eUpaIntercambio != null)
                //        eUpaIntercambio(sender, e);
                //}
            }
            catch (Exception ex)
            {

            }
        }
        protected void imbAeronave_Click(object sender, ImageClickEventArgs e)
        {
            //if (eGetMatriculas != null)
            //    eGetMatriculas(sender, e);

            //gvMatriculas.DataSource = dtMatriculas;
            //gvMatriculas.DataBind();

            //mpeMatricula.Show();
        }
        protected void btnAceptarMatriculas_Click(object sender, EventArgs e)
        {
            try
            {
                //    lblErrorMat.Text = string.Empty;
                //    bool ban = false;

                //    foreach (GridViewRow row in gvMatriculas.Rows)
                //    {
                //        RadioButton rb = (RadioButton)row.FindControl("rbSelecciona");
                //        if (rb != null)
                //        {
                //            if (rb.Checked)
                //            {
                //                ban = true;
                //                break;
                //            }
                //        }
                //    }

                //    if (ban)
                //    {
                //        foreach (GridViewRow row in gvMatriculas.Rows)
                //        {
                //            RadioButton rb = (RadioButton)row.FindControl("rbSelecciona");
                //            if (rb != null)
                //            {
                //                if (rb.Checked)
                //                {
                //                    txtAeronaveSerie.Text = gvMatriculas.Rows[row.RowIndex].Cells[1].Text.S();
                //                    ReadMatricula.Text = gvMatriculas.Rows[row.RowIndex].Cells[2].Text.S();

                //                }
                //            }
                //        }

                //        mpeMatricula.Hide();
                //    }
                //    else
                //    {
                //        mpeMatricula.Show();
                //        lblErrorMat.Text = "Debe seleccionar una aeronave para vincular al contrato, favor de verificar";
                //    }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region METODOS
        public void LoadBitacora(DataTable dt)
        {
            dtBitacora = dt;
            gvPiernas.DataSource = dt;
            gvPiernas.DataBind();
        }
        public void LoadDiscrepancias(DataTable dt)
        {
            dtDiscrepancias = dt;
            gvDiscrepancias.DataSource = dt;
            gvDiscrepancias.DataBind();
        }
        public void LoadComponentes(DataTable dt)
        {
            dtComponentes = dt;
            gvComponentes.DataSource = dt;
            gvComponentes.DataBind();
        }
        public void LoadSICPIC(string[] sSICPIC)
        {
            lblPICResp.Text = sSICPIC[0];
            lblSICREsp.Text = sSICPIC[1];
        }
        public void LlenaDatosBitacora()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
                if (eGetConsultaBitacoraPiernas != null)
                    eGetConsultaBitacoraPiernas(null, EventArgs.Empty);
                if (eGetSicPic != null)
                    eGetSicPic(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaPaises(DropDownList ddl)
        {
            //try
            //{
            //    ddl.DataSource = dtPaises;
            //    ddl.DataValueField = "PaisId";
            //    ddl.DataTextField = "PaisNombre";
            //    ddl.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        public void CargaEstados(DropDownList ddl, DataTable dt)
        {
            try
            {
                if (dt != null)
                {
                    //ddl.DataSource = dt;
                    //ddl.DataValueField = "EstadoId";
                    //ddl.DataTextField = "EstadoNombre";
                    //ddl.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaIntercambios(DataTable dt)
        {
            //dtIntercambios = dt.Copy();
            //gvIntercambio.DataSource = dt;
            //gvIntercambio.DataBind();
        }
        private void LimpiaCamposClientes()
        {
            try
            {
                //txtClaveCliente.Text = string.Empty;
                //txtNombreClienteA.Text = string.Empty;
                //txtRazonSocial.Text = string.Empty;
                //rblTieneRFC.SelectedValue = "1";
                //txtRFC.Text = string.Empty;
                //ddlTipoContribuyente.SelectedIndex = -1;
                //chkActivo.Checked = true;
                //txtTelefonoCliente.Text = string.Empty;
                //txtFax.Text = string.Empty;
                //txtCorreoElectronico.Text = string.Empty;
                //ddlSector.SelectedIndex = -1;
                //ddlPais.SelectedIndex = -1;
                //ddlEstado.SelectedIndex = -1;
                //txtDireccion.Text = string.Empty;
                //txtCiudad.Text = string.Empty;
                //txtCP.Text = string.Empty;
                //ddlPaisDE.SelectedIndex = -1;
                //ddlEstadoDE.SelectedIndex = -1;
                //txtDireccionDE.Text = string.Empty;
                //txtCiudadDE.Text = string.Empty;
                //txtCPDE.Text = string.Empty;
                //gvContratos.DataSource = null;
                //gvContratos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiaCamposIntercambio()
        {
            //ddlGrupoModelo.SelectedValue = null;
            //txtIntercambioFactor.Text = string.Empty;
            //txtIntercambioEspera.Text = string.Empty;
            //txtIntercambioPernocta.Text = string.Empty;
            //txtIntercambioValor.Text = string.Empty;
            //txtIntercambioGalones.Text = string.Empty;
            //txtIntercambioTarifaInter.Text = string.Empty;
            //txtIntercambioTarifaNac.Text = string.Empty;
            //txtIntercambioFerry.Text = string.Empty;
            //rblAplicaFerry.SelectedValue = "0";
        }
        private void LimpiaCamposContratos()
        {
            //txtClaveContrato.Text = string.Empty;
            //txtAeronaveSerie.Text = string.Empty;
            //txtPorcentPart.Text = string.Empty;
            //txtHorasContratadas.Text = string.Empty;
            //rblAplcaIntercambios.SelectedValue = "1";
            //rblFactorIntercambio.SelectedValue = "1";
            //ddlEstatusContrato.SelectedIndex = -1;
            //txtFechaContrato.Text = string.Empty;
        }
        public void LoadGrupoModelo(DataTable dt)
        {
            try
            {
                //ddlGrupoModelo.DataSource = dt;
                //ddlGrupoModelo.DataTextField = "Descripcion";
                //ddlGrupoModelo.DataValueField = "GrupoModeloId";
                //ddlGrupoModelo.DataBind();
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
        public void CargaMatricula(DataTable dtMatricula)
        {
            try
            {
                ddlMatricula.DataSource = dtMatricula;
                ddlMatricula.DataValueField = "IdAeroave";
                ddlMatricula.DataTextField = "Matricula";
                ddlMatricula.DataBind();
                ddlMatricula.Items.Insert(0, new ListItem("Todas", "0"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenarBitacoraPiernas(DataTable dt)
        {
            gvLegs.DataSource = dt;
            gvLegs.DataBind();
        }
        private void LimpiaModalDiscrepancias()
        {
            ddlOrigen.SelectedValue = "1";
            ddlTipoRep.SelectedValue = "0";
            txtDescripcion.Text = string.Empty;
            txtAccionCorrectiva.Text = string.Empty;
            txtCodigoAta.Text = string.Empty;
            txtBase.Text = string.Empty;
            txtMecanicoDis.Text = string.Empty;
            txtFechaDis.Text = string.Empty;
            txtFechaAtencion.Text = string.Empty;
            txtReferenciaRep.Text = string.Empty;
            ddlDiagnostico.SelectedValue = "0";
            lblIdDisc.Text = string.Empty;

        }
        private void LimpiaModalComponentes()
        {
            lblDescripcionComp.Text = string.Empty;
            txtNombreComponente.Text = string.Empty;
            txtNoParteRemovido.Text = string.Empty;
            txtNoParteInstalado.Text = string.Empty;
            txtNoSerieRemovido.Text = string.Empty;
            txtNoSerieInstalado.Text = string.Empty;
            txtPosicionComponente.Text = string.Empty;
            txtRazonServicio.Text = string.Empty;
            lblIDCompRes.Text = string.Empty;

        }
        private void OcultarEtiquetasAviso()
        { 
            lblReqOrigenDisAvi.Visible = false;
            lblReqTipoRepDisAvi.Visible = false;
            lblReqAccionCorrectivaAvi.Visible = false;
            lblReqDisDescripcionAvi.Visible = false;
            lblReqDisBaseAvi.Visible = false;
            lblReqCodigoAtaAvi.Visible = false;
            lblReqMecanicoDisAvi.Visible = false;
            lblReqReferenciaRepAvi.Visible = false;
            lblReqFechaDisAvi.Visible = false;
            lblReqFechaAtencionAvi.Visible = false;
            lblReqDiagnosticoAvi.Visible = false;
        }
        private void OcultarEtiquetasAvisoComponentes()
        {
            lblReqNombreComponenteAvi.Visible = false;
            lblReqDescripcionCompAvi.Visible = false;
            lblReqNoParteRemovidoAvi.Visible = false;
            lblReqNoParteInstaladoAvi.Visible = false;
            lblReqNoSerieRemovidoAvi.Visible = false;
            lblReqNoSerieInstaladoAvi.Visible = false;
            lblReqPosicionComponenteAvi.Visible = false;
            lblReqRazonServicioAvi.Visible = false;
        }
        //Agregar el metodo para limpiar componentes asi como su controlador para llenar el grid y las cajas de texto en la edicion
        #endregion

        #region VARIABLES Y PROPIEDADES
        Bitacora_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetMatricula;
        public event EventHandler eGetConsultaBitacoraPiernas;
        public event EventHandler eGetSicPic;
        public event EventHandler eNewObjDiscrepancia;
        public event EventHandler eEditObjDiscrepancia;
        public event EventHandler eSearchObjDiscrepancia;
        public event EventHandler eGetDiscrepancias;
        public event EventHandler eDeleteObjDiscrepancia;
        public event EventHandler eNewObjComponente;
        public event EventHandler eEditObjComponente;
        public event EventHandler eSearchObjComponente;
        public event EventHandler eGetComponentes;
        public event EventHandler eDeleteObjComponente;

        public object[] oArray
        {
            get
            {
                string sFolio = string.Empty;
                string sMatricula = string.Empty;
                string sFechaIni = string.Empty;
                string sFechaFin = string.Empty;


                sFolio = txtFolio.Text;
                if (ddlMatricula.SelectedItem.S() == "Todas")
                {
                    sMatricula = "";
                }
                else
                {
                    sMatricula = ddlMatricula.SelectedItem.S();
                }
                sFechaIni = txtFechaIni.Text;
                sFechaFin = txtFechaFin.Text;

                return new object[]
                {

                    "@Folio", sFolio,
                    "@Matricula", sMatricula,
                    "@FechaIni", sFechaIni,
                    "@FechaFin", sFechaFin
                };
            }
        }
        public DataTable dtBitacora
        {
            get { return (DataTable)ViewState["VBitacora"]; }
            set { ViewState["VBitacora"] = value; }
        }
        public DataTable dtDiscrepancias
        {
            get { return (DataTable)ViewState["VDiscrepancias"]; }
            set { ViewState["VDiscrepancias"] = value; }
        }
        public DataTable dtComponentes
        {
            get { return (DataTable)ViewState["VComponentes"]; }
            set { ViewState["VComponentes"] = value; }
        }
        //public string iIdCliente
        //{
        //    get { return (string)ViewState["VCliente"]; }
        //    set { ViewState["VCliente"] = value; }
        //}
        //public DataTable dtPaises
        //{
        //    get { return (DataTable)ViewState["VSPaises"]; }
        //    set { ViewState["VSPaises"] = value; }
        //}
        //public int iIdPais
        //{
        //    get { return ddlPais.SelectedValue.S().I(); }
        //    set { ViewState["VSIdPais"] = value; }
        //}
        //public int iIdPaisDE
        //{
        //    get { return ddlPaisDE.SelectedValue.S().I(); }
        //    set { ViewState["VSIdPaisDE"] = value; }
        //}
        public string sMatricula
        {
            get { return (string)ViewState["VMatricula"]; }
            set { ViewState["VMatricula"] = value; }
        }
        public string sFolio
        {
            get { return (string)ViewState["VFolio"]; }
            set { ViewState["VFolio"] = value; }
        }
        public string sIdBitacora
        {
            get { return (string)ViewState["VIdBitacora"]; }
            set { ViewState["VIdBitacora"] = value; }
        }
        public string sFolioDis
        {
            get { return (string)ViewState["VFolioDis"]; }
            set { ViewState["VFolioDis"] = value; }
        }
        public string sIdBitacoraDis
        {
            get { return (string)ViewState["VIdBitacoraDis"]; }
            set { ViewState["VIdBitacoraDis"] = value; }
        }
        public string sIdDiscrepancia
        {
            get { return (string)ViewState["VIdDiscrepancia"]; }
            set { ViewState["VIdDiscrepancia"] = value; }
        }
        public string sIdDiscrepanciaComp
        {
            get { return (string)ViewState["VIdDiscrepanciaComp"]; }
            set { ViewState["VIdDiscrepanciaComp"] = value; }
        }
        public string sIdComponente
        {
            get { return (string)ViewState["VIdComponente"]; }
            set { ViewState["VIdComponente"] = value; }
        }
        public string sIdSIC
        {
            get { return (string)ViewState["VIdSIC"]; }
            set { ViewState["VIdSIC"] = value; }
        }
        public string sIdPIC
        {
            get { return (string)ViewState["VIdPIC"]; }
            set { ViewState["VIdPIC"] = value; }
        }
        public bool bBandera
        {
            get { return (bool)ViewState["VbBandera"]; }
            set { ViewState["VbBandera"] = value; }
        }
        public Bitacora oBitacora
        {
            get
            {
                return new Bitacora
                {
                    sFolio = lblFolioResp.Text.S(),
                    sMatricula = lblMatriculaResp.Text.S(),
                    sSerie = lblSerieResp.Text.S(),
                   
                    sMotorI = txtMotor1.Text.S(),
                    sMotorII = txtMotor2.Text.S(),
                    Planeador = txtPlaneador.Text.S(),
                    APU = txtAPU.Text.S(),

                    sCMotorI = txtCMotor1.Text.S(),
                    sCMotorII = txtCMotor2.Text.S(),
                    sAterrizajes = txtAterrizajes.Text.S(),

                    sMecanico = txtMecanicoResp.Text.S(),
                    bBandera = bBandera,
                    

                    sUsuario = Utils.GetUser.S()
                };
            }
            set
            {
                Bitacora oCat = value as Bitacora;
                if (oCat != null)
                {
                    lblFolioResp.Text = oCat.sFolio;
                    lblMatriculaResp.Text = oCat.sMatricula;
                    lblSerieResp.Text = oCat.sSerie;
                    //rblTieneRFC.SelectedValue = oCat.bRFC == true ? "1" : "0";
                    lblPICResp.Text = "" ;
                    lblSICREsp.Text = "";
                    txtMotor1.Text = oCat.sMotorI;
                    txtMotor2.Text = oCat.sMotorII;
                    txtPlaneador.Text  = oCat.Planeador;
                    txtAPU.Text = oCat.APU;

                    txtCMotor1.Text = oCat.sCMotorI;
                    txtCMotor2.Text = oCat.sCMotorII;
                    txtAterrizajes.Text = oCat.sAterrizajes;

                    txtMecanicoResp.Text = oCat.sMecanico;
                    bBandera = oCat.bBandera;

                }
            }
        }
        public Discrepancia oDiscrepancia
        {
            get
            {
                return new Discrepancia
                {
                    sIdDiscrepancia = sIdDiscrepancia,
                    sIdBitacora = sIdBitacoraDis,
                    sOrigen = ddlOrigen.SelectedValue,
                    sTipoReporte = ddlTipoRep.SelectedValue,
                    sDescripcion = txtDescripcion.Text,
                    sAccionesCorrectiva = txtAccionCorrectiva.Text,
                    sCodigoAta = txtCodigoAta.Text,
                    sBase = txtBase.Text,
                    sMecanico = txtMecanicoDis.Text,
                    dtFechaDiscrepancia = txtFechaDis.Text.S() == string.Empty ? null : ((DateTime?)txtFechaDis.Text.S().Dt()),
                    dtFechaAtencion = txtFechaAtencion.Text.S() == string.Empty ? null : ((DateTime?) txtFechaAtencion.Text.S().Dt()),
                    sReferenciaRep = txtReferenciaRep.Text,
                    sDiagnostico = ddlDiagnostico.SelectedValue,
                    sId = lblIdDisc.Text,
                    sComponente = "",
                    sUsuario = Utils.GetUser.S()
                };
            }
            set
            {
                Discrepancia oCat = value as Discrepancia;
                if (oCat != null)
                {
                    sIdBitacoraDis = oCat.sIdBitacora;
                    ddlOrigen.SelectedValue = oCat.sOrigen;
                    ddlTipoRep.SelectedValue = oCat.sTipoReporte;
                    txtDescripcion.Text = oCat.sDescripcion;
                    txtAccionCorrectiva.Text = oCat.sAccionesCorrectiva;
                    txtCodigoAta.Text = oCat.sCodigoAta;
                    txtBase.Text = oCat.sBase;
                    txtMecanicoDis.Text = oCat.sMecanico;
                    txtFechaDis.Text = string.Format("{0:yyyy-MM-dd}", oCat.dtFechaDiscrepancia.S().Dt());
                    txtFechaAtencion.Text = string.Format("{0:yyyy-MM-dd}", oCat.dtFechaAtencion.S().Dt());
                    txtReferenciaRep.Text = oCat.sReferenciaRep;
                    ddlDiagnostico.SelectedValue = oCat.sDiagnostico;
                    lblIdDisc.Text = oCat.sId;
                    btnComponente.Text = oCat.sComponente;

                }
            }
        }
        public Componente oComponente
        {
            get
            {
                return new Componente
                {
                    sIdComponente = sIdComponente,
                    sIdDiscrepancia = sIdDiscrepanciaComp,
                    sNombreComponente = txtNombreComponente.Text,
                    sDescripcion = txtDescripcionComp.Text,
                    sNoParteRemovido = txtNoParteRemovido.Text,
                    sNoSerieRemovido = txtNoSerieRemovido.Text,
                    sNoParteInstalado = txtNoParteInstalado.Text,
                    sNoSerieInstalado = txtNoSerieInstalado.Text,
                    sPocisionComponente = txtPosicionComponente.Text,
                    sID = lblIDCompRes.Text,
                    sRazonServicio = txtRazonServicio.Text,
                    sUsuario = Utils.GetUser.S()
                };
            }
            set
            {
                Componente oCat = value as Componente;
                if (oCat != null)
                {
                    sIdComponente = oCat.sIdComponente;
                    sIdDiscrepanciaComp = oCat.sIdDiscrepancia;
                    txtNombreComponente.Text = oCat.sNombreComponente;
                    txtDescripcionComp.Text = oCat.sDescripcion;
                    txtNoParteRemovido.Text = oCat.sNoParteRemovido;
                    txtNoSerieRemovido.Text = oCat.sNoSerieRemovido;
                    txtNoParteInstalado.Text = oCat.sNoParteInstalado;
                    txtNoSerieInstalado.Text = oCat.sNoSerieInstalado;
                    txtPosicionComponente.Text = oCat.sPocisionComponente;
                    lblIDCompRes.Text = oCat.sID;
                    txtRazonServicio.Text = oCat.sRazonServicio;
                }
            }
        }

        //public ClienteContrato oContrato
        //{
        //    set
        //    {
        //        ClienteContrato oCat = value as ClienteContrato;
        //        if (oCat != null)
        //        {
        //            txtClaveContrato.Text = oCat.sClaveContrato;
        //            txtAeronaveSerie.Text = oCat.sAeronaveSerie;
        //            ReadMatricula.Text = oCat.sAeronaveMatricula.S();
        //            txtPorcentPart.Text = oCat.iPorcentajePart.S();
        //            txtHorasContratadas.Text = oCat.iHorasContratadas.S();
        //            rblAplcaIntercambios.SelectedValue = oCat.bAplicaIntercambios == true ? "1" : "0";
        //            rblFactorIntercambio.SelectedValue = oCat.iFactorIntercambio.S() != "0" ? oCat.iFactorIntercambio.S() : "1";
        //            txtFechaContrato.Text = oCat.dtFechaContrato.S();
        //            ddlEstatusContrato.SelectedValue = oCat.iEstatusContrato.S();


        //            txtFechaFinContrato.Text = oCat.dtFechaFinContrato.S() == "01/01/0001 12:00:00 a. m." ? string.Empty : oCat.dtFechaFinContrato.S();
        //            txtAnticipoContrato.Text = oCat.dAnticipoContrato.S();

        //            if (oCat.iTipoServicioConsultoria > 0)
        //                rblServicioConsultoria.SelectedValue = oCat.iTipoServicioConsultoria.S();

        //            if (oCat.iTipoTarifa > 0)
        //                rblTarifas.SelectedValue = oCat.iTipoTarifa.S();

        //            if (oCat.iDetalleTipoTarifa > 0)
        //                rblDetalleTarifa.SelectedValue = oCat.iDetalleTipoTarifa.S();

        //            txtNoPoliza.Text = oCat.sNoPoliza.S();
        //            txtEmpresaAseguradora.Text = oCat.sEmpresaAseguradora.S();
        //            txtFechaInicioSeguro.Text = oCat.dtFechaInicioSeg.S() == "01/01/0001 12:00:00 a. m." ? string.Empty : oCat.dtFechaInicioSeg.S();
        //            txtFechaFinSeguro.Text = oCat.dtFechaFinSeg.S() == "01/01/0001 12:00:00 a. m." ? string.Empty : oCat.dtFechaFinSeg.S();
        //        }
        //    }
        //}
        //public ClienteContrato oIntercambio
        //{
        //    set
        //    {
        //        ClienteContrato oCat = value as ClienteContrato;
        //        if (oCat != null)
        //        {
        //            ddlGrupoModelo.SelectedValue = oCat.iIntercambioGrupoModeloId.S() != "0" ? oCat.iIntercambioGrupoModeloId.S() : null;
        //            txtIntercambioFactor.Text = oCat.dContratoIntercambiosFactor.S();
        //            txtIntercambioEspera.Text = oCat.dContratoIntercambiosEspera.S();
        //            txtIntercambioPernocta.Text = oCat.dContratoIntercambiosPernocta.S();
        //            txtIntercambioValor.Text = oCat.dContratoIntercambiosValor.S();
        //            txtIntercambioGalones.Text = oCat.iContratoIntercambiosGalones.S();
        //            txtIntercambioTarifaInter.Text = oCat.dContratoIntercambiosTarifaInternacional.S();
        //            txtIntercambioTarifaNac.Text = oCat.dContratoIntercambiosTarifaNacional.S();
        //            txtIntercambioFerry.Text = oCat.dContratoIntercambiosFerry.S();
        //            rblAplicaFerry.SelectedValue = oCat.bContratoIntercambiosAplicaFerry == true ? "1" : "0";
        //        }
        //    }
        //}
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
        #endregion 

        
    }
}