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
    public class ConsuDiscrepancia_Presenter : BasePresenter<IViewConsuDiscrepancia>
    {
        private readonly DBConsuDiscrepancia oIGesCat;
        public ConsuDiscrepancia_Presenter(IViewConsuDiscrepancia oView, DBConsuDiscrepancia oCI)
            : base(oView)
        {
            oIGesCat = oCI;
            oIView.eGetMatricula += eGetMatricula_Presenter;
            oIView.eEditObjDiscrepancia += EditObjDiscrepancia_Presenter;
            oIView.eSearchObjDiscrepancia += SearchObj_PresenterDiscrepancia;
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDiscrepancias(oIGesCat.DBGetObtieneDiscrepancias(oIView.oArray));
        }
        protected void eGetMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.CargaMatricula(oIGesCat.DBGetObtieneMatriculas);
        }
        protected void EditObjDiscrepancia_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaDiscrepancia(oIView.oDiscrepancia))
                oIView.MostrarMensaje("La Discrepancia se actualizó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar la Discrepancia.", "Aviso");
        }
        protected void SearchObj_PresenterDiscrepancia(object sender, EventArgs e)
        {
            oIView.oDiscrepancia = oIGesCat.oGetObtieneDiscrepancia(oIView.sIdDiscrepancia, oIView.sIdBitacoraDis);
        }
    }
}