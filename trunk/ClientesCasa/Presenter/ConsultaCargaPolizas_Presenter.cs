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
    public class ConsultaCargaPolizas_Presenter : BasePresenter<IViewConsultaCargaPolizas>
    {
        private readonly DBConsultaCargaPolizas oIClientesCat;

        public ConsultaCargaPolizas_Presenter(IViewConsultaCargaPolizas oView, DBConsultaCargaPolizas oGC)
            : base(oView)
        {
            oIClientesCat = oGC;
            oIView.eSearchXFolio += SearchXFolio_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPolizas(new DBConsultaCargaPolizas().DBObtenerCargasPoliza(oIView.sFechaIni, oIView.sFechaFin));
        }

        protected void SearchXFolio_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPolizaXFolio(new DBConsultaCargaPolizas().DBObtenerCargasXFolio(oIView.lgIdFolio));
        }
    }
}