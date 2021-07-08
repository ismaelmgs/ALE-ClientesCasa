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
    public partial class frmConsultaComponentes : System.Web.UI.Page, IViewConsuComponentes
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsuComponente_Presenter(this, new DBConsuComponente());
        }
        #region EVENTOS
        protected void btnAceptar_Click(object sender, EventArgs e)
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
        protected void gvConsultaComponentes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv != null)
            {
                sIdDiscrepancia = gvConsultaComponentes.DataKeys[e.CommandArgument.S().I()]["IdDiscrepancia"].S();
                sIdComponente = gvConsultaComponentes.DataKeys[e.CommandArgument.S().I()]["IdComponente"].S();

                if (e.CommandName == "Editar")
                {
                    lblTituloComponente.Text = "Modificar Componente";
                    btnGuardarComponente.Text = "Modificar";
                    mpeComponente.Show();

                    if (eSearchObjComponente != null)
                        eSearchObjComponente(sender, e);

                }


            }
        }

        protected void gvConsultaComponentes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtD = (DataTable)dtComponentes;
                gvConsultaComponentes.PageIndex = e.NewPageIndex;
                LoadComponentes(dtD);

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtD = (DataTable)dtComponentes;
                gvConsultaComponentes.AllowPaging = false;
                gvConsultaComponentes.DataSource = dtD;
                gvConsultaComponentes.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=ReporteComponentes.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                this.EnableViewState = false;

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                pnlReporteComponentes.RenderControl(htmlWrite);
                //gvConsultaComponentes.RenderControl(htmlWrite);
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
                    if (btnGuardarComponente.Text == "Modificar")
                    {
                        if (eEditObjComponente != null)
                            eEditObjComponente(sender, e);
                    }
                    if (eObjSelected != null)
                        eObjSelected(sender, e);
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
        public void LoadComponentes(DataTable dt)
        {
            dtComponentes = dt;
            gvConsultaComponentes.DataSource = dt;
            gvConsultaComponentes.DataBind();
        }
        #endregion
        #region VARIABLES Y PROPIEDADES
        ConsuComponente_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eEditObjComponente;
        public event EventHandler eSearchObjComponente;


        public object[] oArray
        {
            get
            {
                string sFolio = string.Empty;
                string sDiscrepancia = string.Empty;
                string sFechaIni = string.Empty;
                string sFechaFin = string.Empty;


                sFolio = txtFolioComponente.Text;
                sDiscrepancia = txtFolioDiscrepancia.Text;
                sFechaIni = txtFechaIni.Text;
                sFechaFin = txtFechaFin.Text;

                return new object[]
                {

                    "@Folio", sFolio,
                    "@FolioDiscrepancia", sDiscrepancia,
                    "@FechaIni", sFechaIni,
                    "@FechaFin", sFechaFin
                };
            }
        }

        public DataTable dtComponentes
        {
            get { return (DataTable)ViewState["CComponentes"]; }
            set { ViewState["CComponentes"] = value; }
        }
        public string sIdDiscrepancia
        {
            get { return (string)ViewState["VIdDiscrepancia"]; }
            set { ViewState["VIdDiscrepancia"] = value; }
        }
        public string sIdComponente
        {
            get { return (string)ViewState["VIdComponente"]; }
            set { ViewState["VIdComponente"] = value; }
        }
        public Componente oComponente
        {
            get
            {
                return new Componente
                {
                    sIdComponente = sIdComponente,
                    sIdDiscrepancia = sIdDiscrepancia,
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
                    sIdDiscrepancia = oCat.sIdDiscrepancia;
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
        #endregion

       

        
    }
}