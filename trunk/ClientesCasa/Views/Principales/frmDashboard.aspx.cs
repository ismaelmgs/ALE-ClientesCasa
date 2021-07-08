using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using ClientesCasa.Presenter;


namespace ClientesCasa.Views.Principales
{
    public partial class frmDashboard : System.Web.UI.Page, IViewDashboardMain
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new DashboardMain_Presenter(this, new DBDashboard());

            if (!IsPostBack)
            {
                ArmaPendientes();
            }
        }
        #endregion

        #region MÉTODOS
        private void ArmaPendientes()
        {
            try
            {
                //Obtiene Pendientes Generales
                if (eSearchGral != null)
                    eSearchGral(null, null);

                if (eSearchPendientesPilotos != null)
                    eSearchPendientesPilotos(null, null);

                if (dtPendientesSeguros != null)
                {
                    if (dtPendientesSeguros.Rows.Count > 0)
                    {
                        lblCountAeronaves.Text = dtPendientesSeguros.Rows[0][0].S();
                    }
                    else
                    {
                        lblCountAeronaves.Text = "0";
                        lblCountAeronaves.Visible = false;
                    }
                }
                else
                {
                    lblCountAeronaves.Text = "0";
                    lblCountAeronaves.Visible = false;
                }

                if (dtPendientesPilotos != null)
                {
                    if (dtPendientesPilotos.Rows.Count > 0)
                    {
                        lblCountPilotos.Text = dtPendientesPilotos.Rows[0][0].S();
                    }
                    else
                    {
                        lblCountPilotos.Text = "0";
                        lblCountPilotos.Visible = false;
                    }
                }
                else
                {
                    lblCountPilotos.Text = "0";
                    lblCountPilotos.Visible = false;
                }


                if (dtPendientesGastos != null)
                {
                    if (dtPendientesGastos.Rows.Count > 0)
                    {
                        lblCountNewGastos.Text = dtPendientesGastos.Rows[0][0].S();
                    }
                    else
                    {
                        lblCountNewGastos.Text = "0";
                        lblCountNewGastos.Visible = false;
                    }
                        
                }
                else
                {
                    lblCountNewGastos.Text = "0";
                    lblCountNewGastos.Visible = false;
                }


                if (dtPendientesContratos != null)
                {
                    if (dtPendientesContratos.Rows.Count > 0)
                    {
                        lblCountVenContratos.Text = dtPendientesContratos.Rows[0][0].S();
                    }
                    else
                    {
                        lblCountVenContratos.Text = "0";
                        lblCountVenContratos.Visible = false;
                    }
                }
                else
                {
                    lblCountVenContratos.Text = "0";
                    lblCountVenContratos.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Mensaje(string sMensaje)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }

        public void LoadPendientes(DataSet ds)
        {
            try
            {
                dtPendientesSeguros = null;
                dtPendientesGastos = null;
                dtPendientesContratos = null;

                dtPendientesSeguros = (DataTable)ds.Tables[0];
                dtPendientesGastos = (DataTable)ds.Tables[1];                
                dtPendientesContratos = (DataTable)ds.Tables[2];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadPendientesPilotos(DataSet ds)
        {
            try
            {
                dtPendientesPilotos = null;
                dtPendientesPilotos = (DataTable)ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmDashboard.aspx.cs";
        private const string sPagina = "frmDashboard.aspx";

        DashboardMain_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchGral;
        public event EventHandler eSearchPendientesPilotos;


        //Pendientes
        public DataTable dtPendientesSeguros
        {
            get { return (DataTable)ViewState["VSPendientesSeguros"]; }
            set { ViewState["VSPendientesSeguros"] = value; }
        }

        public DataTable dtPendientesPilotos
        {
            get { return (DataTable)ViewState["VSPendientesPilotos"]; }
            set { ViewState["VSPendientesPilotos"] = value; }
        }

        public DataTable dtPendientesGastos
        {
            get { return (DataTable)ViewState["VSPendientesGastos"]; }
            set { ViewState["VSPendientesGastos"] = value; }
        }

        public DataTable dtPendientesContratos
        {
            get { return (DataTable)ViewState["VSPendientesContratos"]; }
            set { ViewState["VSPendientesContratos"] = value; }
        }
        
        #endregion

    }
}