using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Configuration;
using System.IO;

namespace ClientesCasa.Presenter
{
    public class Dashboard_Presenter : BasePresenter<IViewDashboard>
    {
        private readonly DBDashboard oIClientesCat;

        public Dashboard_Presenter(IViewDashboard oView, DBDashboard oCI)
            : base(oView)
        {
            oIClientesCat = oCI;
            oIView.eSearchCont += SearchCont_Presenter;
            oIView.eSearchGastos += SearchGastos_Presenter;
            oIView.eSearchPilotos += SearchPilotos_Presenter;
            oIView.eSearchAeronaves += SearchAeronaves_Presenter;
        }

        protected void SearchCont_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContratos(new DBDashboard().DBGetContratos());
        }

        protected void SearchAeronaves_Presenter(object sender, EventArgs e)
        {
            oIView.LoadAeronaves(new DBAccesoFPK().DBGetMttoAeronaves());
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadSeguros(new DBDashboard().DBGetAeronaves());
        }

        protected void SearchGastos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGastos(new DBDashboard().DBGetObtieneGastosSAP());
        }

        protected void SearchPilotos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPilotos(new DBAccesoFPK().DBGetObtieneAdiestramiento());
        }

    }
}