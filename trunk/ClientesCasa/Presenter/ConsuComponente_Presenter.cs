using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using NucleoBase.Core;
using System.Data;

namespace ClientesCasa.Presenter
{
    public class ConsuComponente_Presenter : BasePresenter<IViewConsuComponentes>
    {
        private readonly DBConsuComponente oIGesCat;
        public ConsuComponente_Presenter(IViewConsuComponentes oView, DBConsuComponente oCI)
            : base(oView)
        {
            oIGesCat = oCI;
            oIView.eEditObjComponente += EditObjComponente_Presenter;
            oIView.eSearchObjComponente += SearchObj_PresenterComponente;
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadComponentes(oIGesCat.DBGetObtieneComponentes(oIView.oArray));
        }
        protected void EditObjComponente_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaComponente(oIView.oComponente))
                oIView.MostrarMensaje("El componente se actualizó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar el componente.", "Aviso");
        }
        protected void SearchObj_PresenterComponente(object sender, EventArgs e)
        {
            oIView.oComponente = oIGesCat.oGetObtieneComponente(oIView.sIdComponente, oIView.sIdDiscrepancia);
        }
    }
}