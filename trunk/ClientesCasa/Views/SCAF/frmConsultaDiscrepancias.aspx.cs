using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;

namespace ClientesCasa.Views.SCAF
{
    public partial class frmConsultaDiscrepancias : System.Web.UI.Page, IViewConsuDiscrepancia
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsuDiscrepancia_Presenter(this, new DBConsuDiscrepancia());

            if (!IsPostBack)
            {
                if (eGetMatricula != null)
                    eGetMatricula(sender, e);
            }
        }
        #region EVENTOS
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
        protected void gvConsultaDiscrepancias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv != null)
            {
                if (e.CommandName == "Editar")
                {
                    sIdDiscrepancia = gvConsultaDiscrepancias.DataKeys[e.CommandArgument.S().I()]["IdDiscrepancia"].S();
                    sIdBitacoraDis = gvConsultaDiscrepancias.DataKeys[e.CommandArgument.S().I()]["IdBitacora"].S();
                    lblTituloDiscrepancia.Text = "Modificar Discrepancia";
                    btnGuardarDiscrepancia.Text = "Modificar";
                    mpeDiscrepancia.Show();
                    OcultarEtiquetasAviso();
                    if (eSearchObjDiscrepancia != null)
                        eSearchObjDiscrepancia(sender, e);

                }

               
            }
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

                    if (btnGuardarDiscrepancia.Text == "Modificar")
                    {
                        if (eEditObjDiscrepancia != null)
                            eEditObjDiscrepancia(sender, e);
                    }
                    if (eObjSelected != null)
                        eObjSelected(sender, e);
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
        protected void gvConsultaDiscrepancias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dtMatriculas;
                DataTable dtD = (DataTable)dtDiscrepancias;
                gvConsultaDiscrepancias.PageIndex = e.NewPageIndex;
                CargaMatricula(dt);
                LoadDiscrepancias(dtD);

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtD = (DataTable)dtDiscrepancias;
                gvConsultaDiscrepancias.AllowPaging = false;
                gvConsultaDiscrepancias.DataSource = dtD;
                gvConsultaDiscrepancias.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=ReporteDiscrepancias.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                this.EnableViewState = false;

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                pnlReporteDiscrepancias.RenderControl(htmlWrite);
                //gvConsultaDiscrepancias.RenderControl(htmlWrite);
                //Response.Write(stringWrite.ToString());
                //Response.End();
                Response.Output.Write(stringWrite.ToString());
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {

            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion
        #region Metodos
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
        public void LoadDiscrepancias(DataTable dt)
        {
            dtDiscrepancias = dt;
            gvConsultaDiscrepancias.DataSource = dt;
            gvConsultaDiscrepancias.DataBind();
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
        #endregion
        #region VARIABLES Y PROPIEDADES
        ConsuDiscrepancia_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetMatricula;
        public event EventHandler eEditObjDiscrepancia;
        public event EventHandler eSearchObjDiscrepancia;
        

        public object[] oArray
        {
            get
            {
                string sFolio = string.Empty;
                string sMatricula = string.Empty;
                string sFechaIni = string.Empty;
                string sFechaFin = string.Empty;


                sFolio = txtFolioDiscrepancia.Text;
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

        public DataTable dtDiscrepancias
        {
            get { return (DataTable)ViewState["CDiscrepancias"]; }
            set { ViewState["CDiscrepancias"] = value; }
        }
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["CDMatriculas"]; }
            set { ViewState["CDMatriculas"] = value; }
        }
        public string sIdDiscrepancia
        {
            get { return (string)ViewState["VIdDiscrepancia"]; }
            set { ViewState["VIdDiscrepancia"] = value; }
        }
        public string sIdBitacoraDis
        {
            get { return (string)ViewState["VIdBitacoraDis"]; }
            set { ViewState["VIdBitacoraDis"] = value; }
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
                    dtFechaAtencion = txtFechaAtencion.Text.S() == string.Empty ? null : ((DateTime?)txtFechaAtencion.Text.S().Dt()),
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
        #endregion

        

        
    }
}