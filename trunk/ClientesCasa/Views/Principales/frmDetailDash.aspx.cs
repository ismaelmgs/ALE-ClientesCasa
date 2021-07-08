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
    public partial class frmDetailDash : System.Web.UI.Page, IViewDashboard
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Dashboard_Presenter(this, new DBDashboard());

            string sOpcion = Request.QueryString["Opc"].S();
            CargaPaneles(sOpcion);

            if (!IsPostBack) { }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("frmDashboard.aspx");
            }
            catch (Exception ex)
            {
                Mensaje("Error: " + ex.Message);
            }
        }
        #endregion

        #region METODOS
        protected void CargaPaneles(string sOp)
        {
            try
            {
                #region Aeronaves
                if (sOp == "1")
                {
                    lblTitulo.Text = "Aeronaves";
                    pnlContratos.Visible = false;
                    pnlContratosFinalizados.Visible = false;
                    pnlAeronaves.Visible = false;
                    pnlSegurosVencidos.Visible = false;
                    pnlClientesPendientes.Visible = false;
                    pnlClientesAlDia.Visible = false;
                    pnlAdiestramientos.Visible = false;
                    pnlExamenesMedicos.Visible = false;
                    pnlLicVuelos.Visible = false;
                    pnlVisas.Visible = false;
                    pnlPasaporte.Visible = false;
                    pnlMainPilotos.Visible = false;

                    pnlAeronavesGenerales.Visible = true;
                    pnlEstatusAeronaves.Visible = true;


                    if (eSearchAeronaves != null)
                        eSearchAeronaves(null, null);

                    if (dtProxMtto != null)
                    {
                        if (dtProxMtto.Rows.Count > 0)
                        {
                            gvProximosMttos.DataSource = dtProxMtto;
                            gvProximosMttos.DataBind();
                            //lblPendAdto.Visible = true;
                            //lblPendAdto.Text = dtProxMtto.Rows.Count.S();
                        }
                        else
                            lblPendAdto.Visible = false;
                    }
                    else
                        lblPendAdto.Visible = false;


                    if (dtStatusMtto != null) 
                    {
                        if (dtProxMtto.Rows.Count > 0)
                        {
                            gvEstatusMtto.DataSource = dtProxMtto;
                            gvEstatusMtto.DataBind();
                            //lblPendAdto.Visible = true;
                            //lblPendAdto.Text = dtProxMtto.Rows.Count.S();
                        }
                        else
                            lblPendAdto.Visible = false;
                    }
                    else
                        lblPendAdto.Visible = false;


                    
                }
                #endregion

                #region Pilotos
                if (sOp == "2")
                {
                    lblTitulo.Text = "Pilotos";
                    pnlContratos.Visible = false;
                    pnlContratosFinalizados.Visible = false;
                    pnlAeronaves.Visible = false;
                    pnlSegurosVencidos.Visible = false;
                    pnlClientesPendientes.Visible = false;
                    pnlClientesAlDia.Visible = false;
                    pnlAdiestramientos.Visible = true;
                    pnlExamenesMedicos.Visible = true;
                    pnlLicVuelos.Visible = true;
                    pnlVisas.Visible = true;
                    pnlPasaporte.Visible = true;
                    pnlMainPilotos.Visible = true;

                    pnlAeronavesGenerales.Visible = false;
                    pnlEstatusAeronaves.Visible = false;

                    if (eSearchPilotos != null)
                        eSearchPilotos(null, null);

                    if (dtAdiestramientos != null)
                    {
                        if (dtAdiestramientos.Rows.Count > 0)
                        {
                            gvAdiestramientoXVencer.DataSource = dtAdiestramientos;
                            gvAdiestramientoXVencer.DataBind();
                            lblPendAdto.Visible = true;
                            lblPendAdto.Text = dtAdiestramientos.Rows.Count.S();
                        }
                        else
                            lblPendAdto.Visible = false;
                    }
                    else
                        lblPendAdto.Visible = false;

                    //if (dtAdiestramientosVencidos != null)
                    //{
                    //    if (dtAdiestramientosVencidos.Rows.Count > 0)
                    //    {
                    //        gvAdiestramientosVencidos.DataSource = dtAdiestramientosVencidos;
                    //        gvAdiestramientosVencidos.DataBind();
                    //    }
                    //}

                    if(dtExamenesXVencer.Rows.Count < 0 && dtExamenesVencidos.Rows.Count < 0)
                        pnlExamenesMedicos.Visible = false;

                    if (dtExamenesXVencer != null)
                    {
                        if (dtExamenesXVencer.Rows.Count > 0)
                        {
                            gvExamenesXVencer.DataSource = dtExamenesXVencer;
                            gvExamenesXVencer.DataBind();
                            lblExaMed.Visible = true;
                            lblExaMed.Text = dtExamenesXVencer.Rows.Count.S();
                        }
                        else
                            lblExaMed.Visible = false;
                    }
                    else
                        lblExaMed.Visible = false;

                    //if (dtExamenesVencidos != null)
                    //{
                    //    if (dtExamenesVencidos.Rows.Count > 0)
                    //    {
                    //        gvExamenesVencidos.DataSource = dtExamenesVencidos;
                    //        gvExamenesVencidos.DataBind();
                    //    }
                    //}

                    if (dtLicVuelo != null)
                    {
                        if (dtLicVuelo.Rows.Count > 0)
                        {
                            gvLicVuelos.DataSource = dtLicVuelo;
                            gvLicVuelos.DataBind();
                            lblLicVuelo.Visible = true;
                            lblLicVuelo.Text = dtLicVuelo.Rows.Count.S();
                        }
                        else
                            pnlLicVuelos.Visible = false;
                    }
                    else
                        pnlLicVuelos.Visible = false;

                    if (dtVisaXVencer != null)
                    {
                        if (dtVisaXVencer.Rows.Count > 0)
                        {
                            gvVisasXVencer.DataSource = dtVisaXVencer;
                            gvVisasXVencer.DataBind();
                            lblVisa.Visible = true;
                            lblVisa.Text = dtVisaXVencer.Rows.Count.S();
                        }
                        else
                            lblVisa.Visible = true;
                    }
                    else
                        lblVisa.Visible = true;

                    if (dtVisasVencidas != null)
                    {
                        if (dtVisasVencidas.Rows.Count > 0)
                        {
                            gvVisasVencidas.DataSource = dtVisasVencidas;
                            gvVisasVencidas.DataBind();
                        }
                    }

                    if (dtPasaportesXVencer != null)
                    {
                        if (dtPasaportesXVencer.Rows.Count > 0)
                        {
                            gvPasaportesXVencer.DataSource = dtPasaportesXVencer;
                            gvPasaportesXVencer.DataBind();
                            lblPassport.Visible = true;
                            lblPassport.Text = dtPasaportesXVencer.Rows.Count.S();
                        }
                        else
                            lblPassport.Visible = false;
                    }
                    else
                        lblPassport.Visible = false;

                    if (dtPasaportesVencidos != null)
                    {
                        if (dtPasaportesVencidos.Rows.Count > 0)
                        {
                            gvPasaportesVencidos.DataSource = dtPasaportesVencidos;
                            gvPasaportesVencidos.DataBind();
                        }
                    }
                    

                }
                #endregion

                #region Gastos
                if (sOp == "3")
                {
                    lblTitulo.Text = "Gastos";
                    pnlContratos.Visible = false;
                    pnlContratosFinalizados.Visible = false;
                    pnlAeronaves.Visible = false;
                    pnlSegurosVencidos.Visible = false;
                    pnlClientesPendientes.Visible = true;
                    pnlClientesAlDia.Visible = true;
                    pnlAdiestramientos.Visible = false;
                    pnlExamenesMedicos.Visible = false;
                    pnlLicVuelos.Visible = false;
                    pnlVisas.Visible = false;
                    pnlPasaporte.Visible = false;
                    pnlMainPilotos.Visible = false;

                    pnlAeronavesGenerales.Visible = false;
                    pnlEstatusAeronaves.Visible = false;

                    if (eSearchGastos != null)
                        eSearchGastos(null, null);

                    if (dtClientesPendientes != null)
                    {
                        if (dtClientesPendientes.Rows.Count > 0)
                        {
                            gvClientesPendientes.DataSource = dtClientesPendientes;
                            gvClientesPendientes.DataBind();
                        }
                    }

                    if (dtClientesAlDia != null)
                    {
                        if (dtClientesAlDia.Rows.Count > 0)
                        {
                            gvClientesAlDia.DataSource = dtClientesAlDia;
                            gvClientesAlDia.DataBind();
                        }
                    }
                }
                #endregion

                #region Contratos
                if (sOp == "4")
                {
                    lblTitulo.Text = "Contratos";
                    pnlContratos.Visible = true;
                    pnlContratosFinalizados.Visible = true;
                    pnlAeronaves.Visible = true;
                    pnlSegurosVencidos.Visible = true;
                    pnlClientesPendientes.Visible = false;
                    pnlClientesAlDia.Visible = false;
                    pnlAdiestramientos.Visible = false;
                    pnlExamenesMedicos.Visible = false;
                    pnlLicVuelos.Visible = false;
                    pnlVisas.Visible = false;
                    pnlPasaporte.Visible = false;
                    pnlMainPilotos.Visible = false;

                    pnlAeronavesGenerales.Visible = false;
                    pnlEstatusAeronaves.Visible = false;

                    if (eSearchObj != null)
                        eSearchObj(null, null);

                    if (eSearchCont != null)
                        eSearchCont(null, null);

                    if (dtContratosXVencer != null)
                    {
                        if (dtContratosXVencer.Rows.Count > 0)
                        {
                            gvContratosPorVencer.DataSource = dtContratosXVencer;
                            gvContratosPorVencer.DataBind();
                        }
                    }

                    if (dtContratosFinalizados != null)
                    {
                        if (dtContratosFinalizados.Rows.Count > 0)
                        {
                            gvContratosFinalizados.DataSource = dtContratosFinalizados;
                            gvContratosFinalizados.DataBind();
                        }
                    }

                    if (dtSegurosXVencer != null)
                    {
                        if (dtSegurosXVencer.Rows.Count > 0)
                        {
                            gvProxVencimientos.DataSource = dtSegurosXVencer;
                            gvProxVencimientos.DataBind();
                        }
                    }

                    if (dtSegurosFinalizados != null)
                    {
                        if (dtSegurosFinalizados.Rows.Count > 0)
                        {
                            gvSegurosVencidos.DataSource = dtSegurosFinalizados;
                            gvSegurosVencidos.DataBind();
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadContratos(DataSet ds)
        {
            try
            {
                dtContratosXVencer = null;
                dtContratosFinalizados = null;

                dtContratosXVencer = (DataTable)ds.Tables[0];
                dtContratosFinalizados = (DataTable)ds.Tables[1];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadAeronaves(DataSet ds)
        {
            try
            {
                dtProxMtto = null;
                dtStatusMtto = null;

                dtProxMtto = (DataTable)ds.Tables["ProxMtto"];
                dtStatusMtto = (DataTable)ds.Tables["StatusMtto"];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadSeguros(DataSet ds)
        {
            try
            {
                dtSegurosXVencer = null;
                dtSegurosFinalizados = null;

                dtSegurosXVencer = (DataTable)ds.Tables[0];
                dtSegurosFinalizados = (DataTable)ds.Tables[1];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadGastos(DataSet ds)
        {
            try
            {
                dtClientesPendientes = null;
                dtClientesAlDia = null;

                dtClientesPendientes = (DataTable)ds.Tables["Pendientes"];
                dtClientesAlDia = (DataTable)ds.Tables["ClientesAlDia"];

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadPilotos(DataSet ds)
        {
            try
            {
                dtAdiestramientos = null;
                //dtAdiestramientosVencidos = null;
                dtExamenesXVencer = null;
                //dtExamenesVencidos = null;
                dtLicVuelo = null;
                dtVisaXVencer = null;
                dtVisasVencidas = null;
                dtPasaportesXVencer = null;
                dtPasaportesVencidos = null;

                dtAdiestramientos = (DataTable)ds.Tables[0];
                //dtAdiestramientosVencidos = (DataTable)ds.Tables[1];
                dtExamenesXVencer = (DataTable)ds.Tables[1];
                //dtExamenesVencidos = (DataTable)ds.Tables[3];
                dtLicVuelo = (DataTable)ds.Tables[2];
                dtVisaXVencer = (DataTable)ds.Tables[3];
                dtVisasVencidas = (DataTable)ds.Tables[4];
                dtPasaportesXVencer = (DataTable)ds.Tables[5];
                dtPasaportesVencidos = (DataTable)ds.Tables[6];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadPendientes(DataSet ds){ }

        public void Mensaje(string sMensaje)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmDetailDash.aspx.cs";
        private const string sPagina = "frmDetailDash.aspx";

        Dashboard_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchCont;
        public event EventHandler eSearchGastos;
        public event EventHandler eSearchPilotos;

        public event EventHandler eSearchGral;
        public event EventHandler eSearchAeronaves;

        //Contratos
        public DataTable dtContratosXVencer
        {
            get { return (DataTable)ViewState["VSContratosXVencer"]; }
            set { ViewState["VSContratosXVencer"] = value; }
        }

        public DataTable dtContratosFinalizados
        {
            get { return (DataTable)ViewState["VSContratosFinalizados"]; }
            set { ViewState["VSContratosFinalizados"] = value; }
        }

        public DataTable dtSegurosXVencer
        {
            get { return (DataTable)ViewState["VSSegurosXVencer"]; }
            set { ViewState["VSSegurosXVencer"] = value; }
        }

        public DataTable dtSegurosFinalizados
        {
            get { return (DataTable)ViewState["VSSegurosFinalizados"]; }
            set { ViewState["VSSegurosFinalizados"] = value; }
        }

        //Aeronaves
        public DataTable dtProxMtto
        {
            get { return (DataTable)ViewState["VSProxMtto"]; }
            set { ViewState["VSProxMtto"] = value; }
        }

        public DataTable dtStatusMtto
        {
            get { return (DataTable)ViewState["VSStatusMtto"]; }
            set { ViewState["VSStatusMtto"] = value; }
        }

        //Gastos

        public DataTable dtGastos
        {
            get { return (DataTable)ViewState["VSGastos"]; }
            set { ViewState["VSGastos"] = value; }
        }

        public DataTable dtClientesPendientes
        {
            get { return (DataTable)ViewState["VSClientesPendientes"]; }
            set { ViewState["VSClientesPendientes"] = value; }
        }

        public DataTable dtClientesAlDia
        {
            get { return (DataTable)ViewState["VSClientesAlDia"]; }
            set { ViewState["VSClientesAlDia"] = value; }
        }

        //Pilotos
        public DataTable dtAdiestramientos
        {
            get { return (DataTable)ViewState["VSAdiestramientos"]; }
            set { ViewState["VSAdiestramientos"] = value; }
        }
        //public DataTable dtAdiestramientosVencidos
        //{
        //    get { return (DataTable)ViewState["VSAdiestramientosVencidos"]; }
        //    set { ViewState["VSAdiestramientosVencidos"] = value; }
        //}
        public DataTable dtExamenesXVencer
        {
            get { return (DataTable)ViewState["VSExamenesXVencer"]; }
            set { ViewState["VSExamenesXVencer"] = value; }
        }
        public DataTable dtExamenesVencidos
        {
            get { return (DataTable)ViewState["VSExamenesVencidos"]; }
            set { ViewState["VSExamenesVencidos"] = value; }
        }
        public DataTable dtLicVuelo
        {
            get { return (DataTable)ViewState["VSLicVuelo"]; }
            set { ViewState["VSLicVuelo"] = value; }
        }
        public DataTable dtVisaXVencer
        {
            get { return (DataTable)ViewState["VSVisaXVencer"]; }
            set { ViewState["VSVisaXVencer"] = value; }
        }
        public DataTable dtVisasVencidas
        {
            get { return (DataTable)ViewState["VSVisasVencidas"]; }
            set { ViewState["VSVisasVencidas"] = value; }
        }
        public DataTable dtPasaportesXVencer
        {
            get { return (DataTable)ViewState["VSPasaportesXVencer"]; }
            set { ViewState["VSPasaportesXVencer"] = value; }
        }
        public DataTable dtPasaportesVencidos
        {
            get { return (DataTable)ViewState["VSPasaportesVencidos"]; }
            set { ViewState["VSPasaportesVencidos"] = value; }
        }
        #endregion

    }
}