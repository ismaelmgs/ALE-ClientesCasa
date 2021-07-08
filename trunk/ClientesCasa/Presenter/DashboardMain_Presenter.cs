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
    public class DashboardMain_Presenter : BasePresenter<IViewDashboardMain>
    {
        private readonly DBDashboard oIClientesCat;

        public DashboardMain_Presenter(IViewDashboardMain oView, DBDashboard oCI)
            : base(oView)
        {
            oIClientesCat = oCI;
            oIView.eSearchGral += SearchGral_Presenter;
            oIView.eSearchPendientesPilotos += SearchPendientesPilotos_Presenter;
        }

        protected void SearchGral_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPendientes(new DBDashboard().DBGetPendientesGral());
        }

        protected void SearchPendientesPilotos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPendientesPilotos(new DBAccesoFPK().DBGetObtienePendientesPilotos());
        }
    }
}