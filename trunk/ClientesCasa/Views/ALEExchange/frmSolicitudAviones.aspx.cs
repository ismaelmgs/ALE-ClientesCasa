using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using System.Data;

namespace ClientesCasa.Views.ALEExchange
{
    public partial class frmSolicitudAviones : System.Web.UI.Page, IViewSolicitudAvion
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new SolicitudAviones_Presenter(this, new DBSolicitudAviones());

            if (!IsPostBack)
            {
                iIdSolicitud = 0;

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                if (eSearchFlota != null)
                    eSearchFlota(sender, e);
            }
        }
        protected void btnAgregarSolicitud_Click(object sender, EventArgs e)
        {
            iIdSolicitud = 0;
            mpeAltaSolicitud.Show();
        }
        protected void btnGuardarSol_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdSolicitud > 0)
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }
                else
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                mpeAltaSolicitud.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlFlota_SelectedIndexChanged(object sender, EventArgs e)
        {
            iIdFlota = ddlFlota.SelectedValue.S().I();

            if (eSearchMats != null)
                eSearchMats(sender, e);
        }
        protected void gvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                iIdSolicitud = gvSolicitudes.DataKeys[e.CommandArgument.S().I()]["IdSolicitud"].S().I();

                switch (e.CommandName)
                {
                    case "Editar":
                        if (eObjSelected != null)
                            eObjSelected(sender, e);

                        mpeAltaSolicitud.Show();
                        break;
                    case "Cancelar":
                        if (eDeleteObj != null)
                            eDeleteObj(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region METODOS
        public void CargaFlotas(DataTable dtFlotas)
        {
            try
            {
                ddlFlota.DataSource = dtFlotas;
                ddlFlota.DataTextField = "DescripcionFlota";
                ddlFlota.DataValueField = "IdFlota";
                ddlFlota.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CargaMatriculas(DataTable dtMats)
        {
            try
            {
                ddlMatricula.DataSource = dtMats;
                ddlMatricula.DataTextField = "Matricula";
                ddlMatricula.DataValueField = "IdAeroave";
                ddlMatricula.DataBind();
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

        public void CargaSolicitudes(DataTable dtSol)
        {
            try
            {
                dtSolicitudes = dtSol;
                gvSolicitudes.DataSource = dtSolicitudes;
                gvSolicitudes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        SolicitudAviones_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchFlota;
        public event EventHandler eSearchMats;

        public int iIdFlota
        {
            get { return (int)ViewState["VSIdFlota"]; }
            set { ViewState["VSIdFlota"] = value; }
        }
        public int iIdSolicitud
        {
            get { return (int)ViewState["VSIdSolicitud"]; }
            set { ViewState["VSIdSolicitud"] = value; }
        }
        public DataTable dtSolicitudes
        {
            set { ViewState["VSSolicitudes"] = value; }
            get { return (DataTable)ViewState["VSSolicitudes"]; }
        }
        public SolicitudAvion oSolicitud
        {
            get
            {
                return new SolicitudAvion
                {
                    iIdFlota = ddlFlota.SelectedValue.S().I(),
                    iIdMatricula = ddlMatricula.SelectedValue.S().I(),
                    sPersona = txtPersona.Text.S(),
                    sFechaInicio = txtFechaInicio.Text.S(),
                    sFechaFin = txtFechaFin.Text.S(),
                    iAcepta = ddlPrestamo.SelectedValue.S().I(),
                    sComentarios = txtComentarios.Text
                };
            }
            set
            {
                SolicitudAvion oCat = value as SolicitudAvion;
                if (oCat != null)
                {
                    ddlFlota.SelectedValue = oCat.iIdFlota.S();
                    iIdFlota = oCat.iIdFlota;

                    if(eSearchMats != null)
                        eSearchMats(null, EventArgs.Empty);

                    ddlMatricula.SelectedValue = oCat.iIdMatricula.S();
                    txtPersona.Text = oCat.sPersona.S();
                    txtFechaInicio.Text = string.Format("{0:yyyy-MM-dd}", oCat.sFechaInicio.S().Dt());
                    txtFechaFin.Text = string.Format("{0:yyyy-MM-dd}", oCat.sFechaFin.S().Dt());
                    ddlPrestamo.SelectedValue = oCat.iAcepta.S();
                    txtComentarios.Text = oCat.sComentarios;
                }   
            }
        }
        #endregion

        
    }
}