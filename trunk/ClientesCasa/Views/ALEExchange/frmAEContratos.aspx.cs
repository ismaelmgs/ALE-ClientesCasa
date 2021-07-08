using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientesCasa.Interfaces;
using ClientesCasa.DomainModel;
using ClientesCasa.Presenter;
using System.Data;
using ClientesCasa.Objetos;
using NucleoBase.Core;
using ClientesCasa.Clases;

namespace ClientesCasa.Views.ALEExchange
{
    public partial class frmAEContratos : System.Web.UI.Page, IViewAEContratos
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new AEContratos_Presenter(this, new DBAEContratos());

            if (!IsPostBack)
            {

            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (eObjSelected != null)
                eObjSelected(sender, e);
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
        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {

        }
        protected void btnAgregarContrato_Click(object sender, EventArgs e)
        {
            try
            {
                iIdContrato = 0;
                LimpiaCamposContratos();
                //readClaveCliente.Text = txtClaveCliente.Text;
                pnlAltaClientes.Visible = false;
                pnlContratos.Visible = true;

                //gvIntercambio.DataSource = null;
                //gvIntercambio.DataBind();
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
                        //if (eSetEliminaContrato != null)
                        //    eSetEliminaContrato(sender, e);

                        //MostrarMensaje("El contrato se canceló de manera correcta.", "Aviso");

                        //if (eGetConsultaContratos != null)
                        //    eGetConsultaContratos(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmContratos.aspx", "frmContratos.aspx.cs", "gvContratos_RowCommand", "Edición de contratos");
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

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
        protected void txtClaveContrato_TextChanged(object sender, EventArgs e)
        {

        }
        protected void imbAeronave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (eGetMatriculas != null)
                    eGetMatriculas(sender, e);

                gvMatriculas.DataSource = dtMatriculas;
                gvMatriculas.DataBind();

                mpeMatricula.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rblAplcaIntercambios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnAgregaIntercambio_Click(object sender, EventArgs e)
        {
            try
            {
                iIdIntercambio = 0;
                LimpiaCamposIntercambio();

                if (eGetGrupoModelo != null)
                    eGetGrupoModelo(sender, e);

                lblTituloAltaInter.Text = "Alta de Intercambio";
                mpeAltaIntercambio.Show();
            }
            catch (Exception)
            {

            }
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
                        if (eGetGrupoModelo != null)
                            eGetGrupoModelo(sender, e);

                        foreach (DataRow row in dtIntercambios.Rows)
                        {
                            if (row["IdIntercambio"].S().I() == iIdIntercambio)
                            {
                                Contrato_Intercambios oInt = new Contrato_Intercambios();
                                oInt.iIdGrupoModelo = row["IdGrupoModelo"].S().I();
                                oInt.bAplicaFerry = row["AplicaFerry"].S().B();
                                oInt.dFactor = row["Factor"].S().D();
                                oInt.dTarifaNalDlls = row["TarifaNal"].S().D();
                                oInt.dTarifaIntDlls = row["TarifaInt"].S().D();
                                oInt.dGalones = row["Galones"].S().D();
                                oInt.dCDN = row["CostoDirectoNal"].S().D();
                                oInt.dCDI = row["CostoDirectoInt"].S().D();

                                oIntercambio = oInt;

                                break;
                            }
                        }

                        mpeAltaIntercambio.Show();
                    }

                    if (e.CommandName == "Eliminar")
                    {
                        if (eDeleteIntercambio != null)
                            eDeleteIntercambio(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnRegresarContratos_Click(object sender, EventArgs e)
        {

        }
        protected void btnIntercambioAceptar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelarIntercambio_Click(object sender, EventArgs e)
        {

        }
        protected void rdlListTarifaCoroEspera_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdlListTarifaCoroEspera.SelectedValue == "0")
                {
                    txtTarifaTiempoEsperaNacionaFija.Enabled = false;
                    txtTarifaTiempoEsperaNacionaVariable.Enabled = false;
                    txtTarifaTiempoEsperaInternacinalFija.Enabled = false;
                    txtTarifaTiempoEsperaInternacinalNacional.Enabled = false;
                }
                else
                {
                    txtTarifaTiempoEsperaNacionaFija.Enabled = true;
                    txtTarifaTiempoEsperaNacionaVariable.Enabled = true;
                    txtTarifaTiempoEsperaInternacinalFija.Enabled = true;
                    txtTarifaTiempoEsperaInternacinalNacional.Enabled = true;
                }

                upaTarTiempoEspera.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rdlListTarifaCobro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdlListTarifaCobro.SelectedValue == "0")
                {
                    txtTarifaNacionalFija.Enabled = false;
                    txtTarifaNacionalVarable.Enabled = false;
                    txtTarifaInternacionalFija.Enabled = false;
                    txtTarifaInternacionalVariable.Enabled = false;
                }
                else
                {
                    txtTarifaNacionalFija.Enabled = true;
                    txtTarifaNacionalVarable.Enabled = true;
                    txtTarifaInternacionalFija.Enabled = true;
                    txtTarifaInternacionalVariable.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtTarifaNacionalFija_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaNacionalFija.Text) && txtTarifaNacionalFija.Text.D() > 0)
                {
                    txtTarifaNacionalVarable.ReadOnly = true;
                    txtTarifaNacionalVarable.Text = "0";
                }
                else
                {
                    txtTarifaNacionalFija.Text = "0";
                    txtTarifaNacionalVarable.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void txtTarifaNacionalVarable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaNacionalVarable.Text) && txtTarifaNacionalVarable.Text.D() > 0)
                {
                    txtTarifaNacionalFija.ReadOnly = true;
                    txtTarifaNacionalFija.Text = "0";
                }
                else
                {
                    txtTarifaNacionalVarable.Text = "0";
                    txtTarifaNacionalFija.ReadOnly = false;
                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void txtTarifaInternacionalFija_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaInternacionalFija.Text) && txtTarifaInternacionalFija.Text.D() > 0)
                {
                    txtTarifaInternacionalVariable.ReadOnly = true;
                    txtTarifaInternacionalVariable.Text = "0";
                }
                else
                {
                    txtTarifaInternacionalFija.Text = "0";
                    txtTarifaInternacionalVariable.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void txtTarifaInternacionalVariable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaInternacionalVariable.Text) && txtTarifaInternacionalVariable.Text.D() > 0)
                {
                    txtTarifaInternacionalFija.ReadOnly = true;
                    txtTarifaInternacionalFija.Text = "0";
                }
                else
                {
                    txtTarifaInternacionalVariable.Text = "0";
                    txtTarifaInternacionalFija.ReadOnly = false;
                    txtTarifaInternacionalFija.Text = "0";
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void cboTarifaCalculoPrecioCombustible_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void chkTarifasPrecioEspecial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gvTarifaCombustible.Visible = chkTarifasPrecioEspecial.Checked;
            }
            catch (Exception ex)
            {

            }
        }
        protected void txtTarifaTiempoEsperaNacionaFija_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaNacionaFija.Text) && txtTarifaTiempoEsperaNacionaFija.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = true;
                    txtTarifaTiempoEsperaNacionaVariable.Text = "0";
                }
                else
                {
                    txtTarifaTiempoEsperaNacionaFija.Text = "0";
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = false;
                }

                upaTarTiempoEspera.Update();
            }
            catch (Exception ex)
            {

            }
        }
        protected void txtTarifaTiempoEsperaNacionaVariable_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaNacionaVariable.Text) && txtTarifaTiempoEsperaNacionaVariable.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = true;
                    txtTarifaTiempoEsperaNacionaFija.Text = "0";
                }
                else
                {
                    txtTarifaTiempoEsperaNacionaVariable.Text = "0";
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = false;
                }
                upaTarTiempoEspera.Update();
            }
            catch (Exception ex)
            {

            }
        }
        protected void txtTarifaTiempoEsperaInternacinalFija_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaInternacinalFija.Text) && txtTarifaTiempoEsperaInternacinalFija.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = true;
                }
                else
                {
                    txtTarifaTiempoEsperaInternacinalFija.Text = "0";
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = false;
                }
                upaTarTiempoEspera.Update();
            }
            catch (Exception ex)
            {

            }
        }
        protected void txtTarifaTiempoEsperaInternacinalNacional_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaInternacinalNacional.Text) && txtTarifaTiempoEsperaInternacinalNacional.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = true;
                }
                else
                {
                    txtTarifaTiempoEsperaInternacinalNacional.Text = "0";
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = false;
                }
                upaTarTiempoEspera.Update();
            }
            catch (Exception ex)
            {

            }
        }
        protected void rdlDescuentanPernoctasNacional_CheckedChanged(object sender, EventArgs e)
        {
            //txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasNacional.Checked)
            {
                //txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = true;
                //txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionNacional.ReadOnly = false;
            }
            else
            {
                //txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionNacional.ReadOnly = true;
            }
            txtPernoctaFactorConversionNacional.Text = string.Empty;
        }
        protected void rdlNoDescuentanPernoctasNacional_CheckedChanged(object sender, EventArgs e)
        {
            //txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasNacional.Checked)
            {
                //txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = true;
                //txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionNacional.ReadOnly = false;
            }
            else
            {
                //txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionNacional.ReadOnly = true;
            }
            txtPernoctaFactorConversionNacional.Text = string.Empty;
        }
        protected void rdlDescuentanPernoctasInternacional_CheckedChanged(object sender, EventArgs e)
        {
            //txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasInternacional.Checked)
            {
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = true;
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionInternacional.ReadOnly = false;
            }
            else
            {
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionInternacional.ReadOnly = true;

            }
            txtPernoctaFactorConversionInternacional.Text = string.Empty;
        }
        protected void rdlNoDescuentanPernoctasInternacional_CheckedChanged(object sender, EventArgs e)
        {
            //txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasInternacional.Checked)
            {
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = true;
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionInternacional.ReadOnly = false;
            }
            else
            {
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionInternacional.ReadOnly = true;

            }
            txtPernoctaFactorConversionInternacional.Text = "0";
        }
        protected void rdlDescuentanTiempoEsperaNacional_CheckedChanged(object sender, EventArgs e)
        {
            //txtTiempoEsperaFactorHrsVueloNal.IsValid = true;
            if (rdlDescuentanTiempoEsperaNacional.Checked)
            {

                //txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.IsRequired = true;
                //txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtTiempoEsperaFactorHrsVueloNal.ReadOnly = false;
            }
            else
            {
                //txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.IsRequired = false;
                txtTiempoEsperaFactorHrsVueloNal.ReadOnly = true;

            }
            txtTiempoEsperaFactorHrsVueloNal.Text = string.Empty;
        }
        protected void rdlNoDescuentanTiempoEsperaNacional_CheckedChanged(object sender, EventArgs e)
        {
            //txtTiempoEsperaFactorHrsVueloNal.IsValid = true;
            if (rdlDescuentanTiempoEsperaNacional.Checked)
            {
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = true;
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionInternacional.ReadOnly = false;
            }
            else
            {
                //txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionInternacional.ReadOnly = true;

            }
            txtPernoctaFactorConversionInternacional.Text = string.Empty;
        }
        protected void rdlDescuentanTiempoEsperaInternaacional_CheckedChanged(object sender, EventArgs e)
        {
            if (rdlDescuentanTiempoEsperaInternaacional.Checked)
            {
                //txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = true;
                //txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = false;
            }
            else
            {
                //txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = false;
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = true;

            }
            txtTiempoEsperaFactorHrsVueloInt.Text = string.Empty;
        }
        protected void rdlNoDescuentanTiempoEsperaInternaacional_CheckedChanged(object sender, EventArgs e)
        {
            if (rdlDescuentanTiempoEsperaInternaacional.Checked)
            {
                //txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = true;
                //txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = false;
            }
            else
            {
                //txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = false;
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = true;

            }
            txtTiempoEsperaFactorHrsVueloInt.Text = string.Empty;
        }
        protected void chkCobrosAplicaTramo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gvTramoPactado.Visible = chkCobrosAplicaTramo.Checked;
                gvTramoPactado.Focus();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnGuardarGen_Click(object sender, EventArgs e)
        {
            if (eSaveGenerales != null)
                eSaveGenerales(sender, e);
        }
        protected void btnRegresarTar_Click(object sender, EventArgs e)
        {
            RedireccionaAccordion(0);
        }
        protected void btnGuardarTar_Click(object sender, EventArgs e)
        {
            if (eSaveTarifas != null)
                eSaveTarifas(sender, e);
        }
        protected void btnRegresarDesc_Click(object sender, EventArgs e)
        {
            RedireccionaAccordion(1);
        }
        protected void btnGuardarDesc_Click(object sender, EventArgs e)
        {
            if (eSaveDescuentos != null)
                eSaveDescuentos(sender, e);
        }
        protected void rblTipoIntercambio_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblTipoIntercambio.SelectedValue)
            {
                case "1":
                    pnlFactor.Visible = true;
                    pnlTarifas.Visible = false;
                    pnlCostoDir.Visible = false;
                    break;
                case "2":
                    pnlFactor.Visible = false;
                    pnlTarifas.Visible = true;
                    pnlCostoDir.Visible = false;
                    break;
                case "3":
                    pnlFactor.Visible = false;
                    pnlTarifas.Visible = false;
                    pnlCostoDir.Visible = true;
                    break;
            }
        }
        protected void btnAceptarAltaIntercambio_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdIntercambio > 0)
                {
                    if (eUpdateIntercambio != null)
                        eUpdateIntercambio(sender, e);
                }
                else
                {
                    if (eSaveIntercambio != null)
                        eSaveIntercambio(sender, e);
                }

                mpeAltaIntercambio.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void LimpiaCamposClientes()
        {
            try
            {
                txtClaveCliente.Text = string.Empty;
                txtNombreClienteA.Text = string.Empty;
                txtRazonSocial.Text = string.Empty;
                rblTieneRFC.SelectedValue = "1";
                txtRFC.Text = string.Empty;
                chkActivo.Checked = true;
                txtTelefonoCliente.Text = string.Empty;
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
            chkAplicaFerry.Checked = false;
            txtTarifaNalDlls.Text = string.Empty;
            txtTarifaInterDlls.Text = string.Empty;
            txtGalnesHrVuelo.Text = string.Empty;

            pnlFactor.Visible = false;
            pnlTarifas.Visible = false;
            pnlCostoDir.Visible = false;
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
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }
        public void RedireccionaAccordion(int iIndex)
        {
            Accordion1.SelectedIndex = iIndex;
        }
        public void LlenaContratos(DataTable dt)
        {
            gvContratos.DataSource = dt;
            gvContratos.DataBind();
        }
        public void LlenaIntercambios(DataTable dt)
        {
            dtIntercambios = dt;
            gvIntercambio.DataSource = dt;
            gvIntercambio.DataBind();
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        AEContratos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetGrupoModelo;
        public event EventHandler eGetMatriculas;

        public event EventHandler eSaveGenerales;
        public event EventHandler eSaveTarifas;
        public event EventHandler eSaveDescuentos;
        public event EventHandler eGetContratoDetalle;
        public event EventHandler eNewIntercambio;
        public event EventHandler eSaveIntercambio;
        public event EventHandler eUpdateIntercambio;
        public event EventHandler eDeleteIntercambio;

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
                    iActivo = chkActivo.Checked ? 1 : 0,
                    sTelefono = txtTelefonoCliente.Text.S(),
                    
                    iIdContrato = iIdContrato,
                    sIdContrato = iIdContrato.S(),
                    
                    //sClaveContrato = txtClaveContrato.Text.S(),
                    //sAeronaveMatricula = ReadMatricula.Text.S(),
                    //sAeronaveSerie = txtAeronaveSerie.Text.S(),
                    //iPorcentajePart = txtPorcentPart.Text.S().I(),
                    //iHorasContratadas = txtHorasContratadas.Text.S().I(),
                    //bAplicaIntercambios = rblAplcaIntercambios.SelectedValue == "1" ? true : false,
                    //iFactorIntercambio = rblFactorIntercambio.SelectedValue.S().I(),

                    //dtFechaContrato = txtFechaContrato.Text.S() == string.Empty ? null : (DateTime?)txtFechaContrato.Text.S().Dt(),
                    //iEstatusContrato = ddlEstatusContrato.SelectedValue.S().I(),

                    //iTipoCosto = rbtnTRipoCosto.SelectedValue == "1" ? 1 : 0,

                    //iIntercambioId = ViewState["VSIntercambio"] == null ? 0 : iIdIntercambio,
                    //iIntercambioGrupoModeloId = ddlGrupoModelo.SelectedValue.S().I(),
                    //dContratoIntercambiosFactor = txtIntercambioFactor.Text.S().D(),
                    //dContratoIntercambiosEspera = txtIntercambioEspera.Text.S().D(),
                    //dContratoIntercambiosPernocta = txtIntercambioPernocta.Text.S().D(),
                    //dContratoIntercambiosValor = txtIntercambioValor.Text.S().D(),
                    //iContratoIntercambiosGalones = txtIntercambioGalones.Text.S().I(),
                    //dContratoIntercambiosTarifaInternacional = txtIntercambioTarifaInter.Text.S().D(),
                    //dContratoIntercambiosTarifaNacional = txtIntercambioTarifaNac.Text.S().D(),
                    //dContratoIntercambiosFerry = txtIntercambioFerry.Text.S().D(),
                    //bContratoIntercambiosAplicaFerry = rblAplicaFerry.SelectedValue == "1" ? true : false,

                    //dtFechaFinContrato = txtFechaFinContrato.Text.S().Dt(),
                    //dAnticipoContrato = txtAnticipoContrato.Text.Replace(",", "").S().D(),
                    //sMonedaAnticipo = ddlMonedaAnticipo.SelectedValue.S(),
                    //iTipoServicioConsultoria = rblServicioConsultoria.SelectedValue.S().I(),
                    //iTipoTarifa = rblTarifas.SelectedValue.S().I(),
                    //iDetalleTipoTarifa = rblDetalleTarifa.SelectedValue.S().I(),
                    //sNoPoliza = txtNoPoliza.Text.S(),
                    //sEmpresaAseguradora = txtEmpresaAseguradora.Text.S(),
                    //dtFechaInicioSeg = txtFechaInicioSeguro.Text.S().Dt(),
                    //dtFechaFinSeg = txtFechaFinSeguro.Text.S().Dt(),

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
                    
                    chkActivo.Checked = true;
                    txtTelefonoCliente.Text = oCat.sTelefono;
                    
                    LlenaContratos(oCat.dtContratos);
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
        public bool bActualizaTarifa
        {
            set
            {
                ViewState["bActualizaTarifa"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaTarifa"];
            }
        }
        public bool bActualizaCobro
        {
            set
            {
                ViewState["bActualizaCobro"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaCobro"];
            }
        }
        public DataTable dtServicioConCargo
        {
            get
            {
                return (DataTable)ViewState["ServicioConCargo"];
            }
            set
            {
                ViewState["ServicioConCargo"] = value;
            }
        }
        //public string sClaveContrato
        //{
        //    get
        //    {
        //        return txtClaveContrato.Text.S();
        //    }
        //}
        public ClienteContrato oContrato
        {
            get
            {
                ClienteContrato oCat = new ClienteContrato();
                oCat.sClaveContrato = txtClaveContrato.Text.S();
                oCat.sAeronaveMatricula = ReadMatricula.Text.S();
                oCat.sAeronaveSerie = txtAeronaveSerie.Text.S();
                oCat.iPorcentajePart = txtPorcentPart.Text.S().I();
                oCat.iHorasContratadas = txtHorasContratadas.Text.S().I();
                oCat.bAplicaIntercambios = rblAplcaIntercambios.SelectedValue == "1" ? true : false;
                oCat.iFactorIntercambio = rblFactorIntercambio.SelectedValue.S().I();

                oCat.dtFechaContrato = txtFechaContrato.Text.S() == string.Empty ? null : (DateTime?)txtFechaContrato.Text.S().Dt();
                oCat.iEstatusContrato = ddlEstatusContrato.SelectedValue.S().I();

                oCat.iTipoCosto = rbtnTRipoCosto.SelectedValue == "1" ? 1 : 0;

                oCat.dtFechaFinContrato = txtFechaFinContrato.Text.S().Dt();
                oCat.dAnticipoContrato = txtAnticipoContrato.Text.Replace(",", "").S().D();
                oCat.sMonedaAnticipo = ddlMonedaAnticipo.SelectedValue.S();
                
                oCat.iTipoServicioConsultoria = 0;
                oCat.iTipoTarifa = 1;
                oCat.iDetalleTipoTarifa = 1;

                return oCat;
            }
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

                    txtFechaContrato.Text = string.Format("{0:yyyy-MM-dd}", oCat.dtFechaContrato.S().Dt());

                    ddlEstatusContrato.SelectedValue = oCat.iEstatusContrato.S();

                    txtFechaFinContrato.Text = oCat.dtFechaFinContrato.S() == "01/01/0001 12:00:00 a. m." ? string.Empty : string.Format("{0:yyyy-MM-dd}", oCat.dtFechaFinContrato.S().Dt());
                    txtAnticipoContrato.Text = oCat.dAnticipoContrato.ToString("N2");

                    if (oCat.sMonedaAnticipo.S() != string.Empty)
                        ddlMonedaAnticipo.SelectedValue = oCat.sMonedaAnticipo.S();

                    rbtnTRipoCosto.SelectedValue = oCat.iTipoCosto.S();

                    //if (oCat.iTipoServicioConsultoria > 0)
                    //    rblServicioConsultoria.SelectedValue = oCat.iTipoServicioConsultoria.S();

                    //if (oCat.iTipoTarifa > 0)
                    //    rblTarifas.SelectedValue = oCat.iTipoTarifa.S();

                    //if (oCat.iDetalleTipoTarifa > 0)
                    //    rblDetalleTarifa.SelectedValue = oCat.iDetalleTipoTarifa.S();

                }
            }
        }
        public Contrato_Tarifas objContratosTarifas
        {
            get
            {
                Contrato_Tarifas objTarifa = new Contrato_Tarifas();
                objTarifa.iIdContrato = iIdContrato;
                objTarifa.dCostoDirNal = txtTarifasCostoDirNac.Text.D();
                objTarifa.dCostoDirInt = txtTarifasCostoDirInter.Text.D();
                objTarifa.bCombustible = rblTarifasCombustible.SelectedValue == "1" ? true : false;

                objTarifa.iTipoCalculo = cboTarifaCalculoPrecioCombustible.SelectedValue.S().I();
                objTarifa.dConsumoGalones = txtTarifaConsumo.Text.D();

                objTarifa.dFactorTramosNal = txtFactorTramNal.Text.S().D();
                objTarifa.dFactorTramosInt = txtFactorTramInt.Text.S().D();

                objTarifa.bPrecioInternacionalEspecial = chkTarifasPrecioEspecial.Checked;
                objTarifa.bCobraTiempoEspera = rdlListTarifaCoroEspera.SelectedValue == "1";
                objTarifa.dTiempoEsperaFijaNal = txtTarifaTiempoEsperaNacionaFija.Text.D();
                objTarifa.dTiempoEsperaFijaInt = txtTarifaTiempoEsperaInternacinalFija.Text.D();
                objTarifa.dTiempoEsperaVarNal = txtTarifaTiempoEsperaNacionaVariable.Text.D();
                objTarifa.dTiempoEsperaVarInt = txtTarifaTiempoEsperaInternacinalNacional.Text.D();
                objTarifa.bCobraPernoctas = rdlListTarifaCobro.SelectedValue == "1";
                objTarifa.dPernoctasFijaNal = txtTarifaNacionalFija.Text.D();
                objTarifa.dPernoctasFijaInt = txtTarifaInternacionalFija.Text.D();
                objTarifa.dPernoctasVarNal = txtTarifaNacionalVarable.Text.D();
                objTarifa.dPernoctasVarInt = txtTarifaInternacionalVariable.Text.D();

                return objTarifa;
            }
            set
            {
                Contrato_Tarifas objTarifa = value;
                if (objTarifa.iIdContrato < 0)
                {
                    bActualizaTarifa = false;
                    return;
                }
                txtTarifasCostoDirNac.Text = objTarifa.dCostoDirNal.S();
                txtTarifasCostoDirInter.Text = objTarifa.dCostoDirInt.S();

                if (objTarifa.bCombustible)
                {
                    rdlListTarifaCoroEspera.Items[0].Selected = true;
                }
                else
                {
                    rdlListTarifaCoroEspera.Items[1].Selected = true;
                }
                
                cboTarifaCalculoPrecioCombustible.SelectedValue = objTarifa.iTipoCalculo.S();
                txtTarifaConsumo.Text = objTarifa.dConsumoGalones.S();

                txtFactorTramNal.Text = objTarifa.dFactorTramosNal.S();
                txtFactorTramInt.Text = objTarifa.dFactorTramosInt.S();

                chkTarifasPrecioEspecial.Checked = objTarifa.bPrecioInternacionalEspecial;
                gvTarifaCombustible.Visible = chkTarifasPrecioEspecial.Checked;

                if (objTarifa.bCobraTiempoEspera)
                {
                    rdlListTarifaCoroEspera.Items[0].Selected = true;
                }
                else
                {
                    rdlListTarifaCoroEspera.Items[1].Selected = true;
                }

                txtTarifaTiempoEsperaNacionaFija.Text = objTarifa.dTiempoEsperaFijaNal.S();
                txtTarifaTiempoEsperaInternacinalFija.Text = objTarifa.dTiempoEsperaFijaInt.S();
                txtTarifaTiempoEsperaNacionaVariable.Text = objTarifa.dTiempoEsperaVarNal.S();
                txtTarifaTiempoEsperaInternacinalNacional.Text = objTarifa.dTiempoEsperaVarInt.S();

                if (objTarifa.bCobraPernoctas)
                {
                    rdlListTarifaCobro.Items[0].Selected = true;
                }
                else
                {
                    rdlListTarifaCobro.Items[1].Selected = true;
                }

                txtTarifaNacionalFija.Text = objTarifa.dPernoctasFijaNal.S();
                txtTarifaInternacionalFija.Text = objTarifa.dPernoctasFijaInt.S();
                txtTarifaNacionalVarable.Text = objTarifa.dPernoctasVarNal.S();
                txtTarifaInternacionalVariable.Text = objTarifa.dPernoctasVarInt.S();
            }
        }
        public Contrato_CobrosDescuentos objCobrosDesc
        {
            get
            {
                Contrato_CobrosDescuentos objCobros = new Contrato_CobrosDescuentos();
                objCobros.iIdContrato = iIdContrato;
                
                if (rdlCobroFerrysNinguno.Checked)
                    objCobros.iFerrysConCargo = 0;
                if (rdlCobroFerrysTodos.Checked)
                    objCobros.iFerrysConCargo = 1;
                if (rdlCobroFerrysReposicionamiento.Checked)
                    objCobros.iFerrysConCargo = 2;

                objCobros.bAplicaEsperaLibre = chkCobroAplicaEsperaLibre.Checked;
                objCobros.dHorasVuelo = txtCobroHorasVuelo.Text.D();
                objCobros.dFactorHorasVuelo = txtFactorHrsVuelo.Text.D();
                objCobros.bPernoctaNal = rdlDescuentanPernoctasNacional.Checked;
                objCobros.bPernoctaInt = rdlDescuentanPernoctasInternacional.Checked;
                objCobros.dPernoctaFactorConversionNal = txtPernoctaFactorConversionNacional.Text.D();
                objCobros.dPernoctaFactorConversionInt = txtPernoctaFactorConversionInternacional.Text.D();
                objCobros.dNumeroPernoctasLibreAnual = txtDescuentoPernoctasLibres.Text.D();
                objCobros.bPernoctasDescuento = chkListDescuentosPrecnoctas.Items[0].Selected;
                objCobros.bPernoctasCobro = chkListDescuentosPrecnoctas.Items[1].Selected;
                objCobros.bTiempoEsperaNal = rdlDescuentanTiempoEsperaNacional.Checked;
                objCobros.bTiempoEsperaInt = rdlDescuentanTiempoEsperaInternaacional.Checked;
                objCobros.dTiempoEsperaFactorConversionNal = txtTiempoEsperaFactorHrsVueloNal.Text.D();
                objCobros.dTiempoEsperaFactorConversionInt = txtTiempoEsperaFactorHrsVueloInt.Text.D();
                objCobros.bAplicaTramos = chkCobrosAplicaTramo.Checked;

                List<int> lstIds = new List<int>();
                foreach (GridViewRow row in gvServicioConCargo.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkServCargo");
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            lstIds.Add(dtServicioConCargo.Rows[row.RowIndex]["IdServicioConCargo"].S().I());
                        }
                    }
                }
                objCobros.lstIdServiciosConCargo = lstIds;

                objCobros.iTiempoFatura = rdlTiempoFacturarCalzo.Checked == true ? 0 : 1;
                objCobros.dMinutos = txtTiempoFacturarMasMinutos.Text.D();

                return objCobros;

            }
            set
            {
                Contrato_CobrosDescuentos objCobros = value;
                if (objCobros.iIdContrato < 0)
                {
                    bActualizaCobro = false;
                    return;
                }
                switch (objCobros.iFerrysConCargo)
                {
                    case 0:
                        rdlCobroFerrysNinguno.Checked = true;
                        break;
                    case 1:
                        rdlCobroFerrysTodos.Checked = true;
                        break;
                    case 2:
                        rdlCobroFerrysReposicionamiento.Checked = true;
                        break;
                }
                chkCobroAplicaEsperaLibre.Checked = objCobros.bAplicaEsperaLibre;
                txtCobroHorasVuelo.Text = objCobros.dHorasVuelo.S();
                txtFactorHrsVuelo.Text = objCobros.dFactorHorasVuelo.S();
                rdlDescuentanPernoctasNacional.Checked = objCobros.bPernoctaNal;
                rdlNoDescuentanPernoctasNacional.Checked = !objCobros.bPernoctaNal;

                rdlDescuentanPernoctasInternacional.Checked = objCobros.bPernoctaInt;
                rdlNoDescuentanPernoctasInternacional.Checked = !objCobros.bPernoctaInt;

                txtPernoctaFactorConversionNacional.Text = objCobros.dPernoctaFactorConversionNal.S();
                txtPernoctaFactorConversionInternacional.Text = objCobros.dPernoctaFactorConversionInt.S();
                txtDescuentoPernoctasLibres.Text = objCobros.dNumeroPernoctasLibreAnual.S();
                chkListDescuentosPrecnoctas.Items[0].Selected = objCobros.bPernoctasDescuento;
                chkListDescuentosPrecnoctas.Items[1].Selected = objCobros.bPernoctasCobro;
                rdlDescuentanTiempoEsperaNacional.Checked = objCobros.bTiempoEsperaNal;
                rdlNoDescuentanTiempoEsperaNacional.Checked = !objCobros.bTiempoEsperaNal;
                rdlDescuentanTiempoEsperaInternaacional.Checked = objCobros.bTiempoEsperaInt;
                rdlNoDescuentanTiempoEsperaInternaacional.Checked = !objCobros.bTiempoEsperaInt;
                txtTiempoEsperaFactorHrsVueloNal.Text = objCobros.dTiempoEsperaFactorConversionNal.S();
                txtTiempoEsperaFactorHrsVueloInt.Text = objCobros.dTiempoEsperaFactorConversionInt.S();
                
                foreach (GridViewRow row in gvServicioConCargo.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkServCargo");
                    if (chk != null)
                    {
                        
                    }
                }

                chkCobrosAplicaTramo.Checked = objCobros.bAplicaTramos;
                rdlTiempoFacturarCalzo.Checked = objCobros.iTiempoFatura == 0;
                rdlTiempoFacturarVuelo.Checked = !rdlTiempoFacturarCalzo.Checked;
                txtTiempoFacturarMasMinutos.Text = objCobros.dMinutos.S();
            }
        }
        public Contrato_Intercambios oIntercambio
        {
            set
            {
                Contrato_Intercambios oCat = value as Contrato_Intercambios;
                if (oCat != null)
                {
                    ddlGrupoModelo.SelectedValue = oCat.iIdGrupoModelo.S() != "0" ? oCat.iIdGrupoModelo.S() : null;
                    txtIntercambioFactor.Text = oCat.dFactor.S();
                    chkAplicaFerry.Checked = oCat.bAplicaFerry;
                    txtTarifaNalDlls.Text = oCat.dTarifaNalDlls.S();
                    txtTarifaInterDlls.Text = oCat.dTarifaIntDlls.S();
                    txtGalnesHrVuelo.Text = oCat.dGalones.S();
                    txtCostoDirNalDlls.Text = oCat.dCDN.S();
                    txtCostoDirIntDlls.Text = oCat.dCDI.S();

                    if (oCat.dFactor > 0)
                    {
                        rblTipoIntercambio.SelectedValue = "1";
                        pnlFactor.Visible = true;
                    }
                    if (oCat.dTarifaIntDlls > 0)
                    {
                        pnlTarifas.Visible = true;
                        rblTipoIntercambio.SelectedValue = "2";
                    }
                    if (oCat.dCDN > 0)
                    {
                        pnlCostoDir.Visible = true;
                        rblTipoIntercambio.SelectedValue = "3";
                    }
                }
            }
            get 
            {
                Contrato_Intercambios oCon = new Contrato_Intercambios();
                oCon.iId = iIdIntercambio;
                oCon.iIdContrato = iIdContrato;
                oCon.iIdGrupoModelo = ddlGrupoModelo.SelectedValue.S().I();
                

                if (pnlFactor.Visible)
                {
                    oCon.eTipoIntercambio = TipoIntercambio.Factor;
                    oCon.dFactor = txtIntercambioFactor.Text.S().D();
                    oCon.bAplicaFerry = chkAplicaFerry.Checked;
                    oCon.dTarifaNalDlls = 0;
                    oCon.dTarifaIntDlls = 0;
                    oCon.dGalones = 0;
                    oCon.dCDN = 0;
                    oCon.dCDI = 0;
                }
                if (pnlTarifas.Visible)
                {
                    oCon.eTipoIntercambio = TipoIntercambio.Tarifa;
                    oCon.dFactor = 0;
                    oCon.bAplicaFerry = false;
                    oCon.dTarifaNalDlls = txtTarifaNalDlls.Text.S().D();
                    oCon.dTarifaIntDlls = txtTarifaInterDlls.Text.S().D();
                    oCon.dGalones = 0;
                    oCon.dCDN = 0;
                    oCon.dCDI = 0;
                }
                if (pnlCostoDir.Visible)
                {
                    oCon.eTipoIntercambio = TipoIntercambio.CostoDirecto;
                    oCon.dFactor = 0;
                    oCon.bAplicaFerry = false;
                    oCon.dTarifaNalDlls = 0;
                    oCon.dTarifaIntDlls = 0;
                    oCon.dGalones = txtGalnesHrVuelo.Text.S().D();
                    oCon.dCDN = txtCostoDirNalDlls.Text.S().D();
                    oCon.dCDI = txtCostoDirIntDlls.Text.S().D();
                }

                return oCon;
            }
        }
        #endregion

        

    }
}