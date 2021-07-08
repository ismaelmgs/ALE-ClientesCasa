using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class RubrosMatricula_Presenter : BasePresenter<IViewRubrosMatricula>
    {
        private readonly DBRubrosMatricula oIGesCat;

        public RubrosMatricula_Presenter(IViewRubrosMatricula oView, DBRubrosMatricula oCI)
            : base(oView)
        {
            oIGesCat = oCI;


        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMatriculas(oIGesCat.DBGetObtieneMatriculasPorFiltros(oIView.oArr));
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadRubros(oIGesCat.DBGetObtieneRubrosPorMatricula(oIView.iIdAeronave));
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetInsertaRubrosAMatricula(oIView.oLstRubros))
                oIView.MostrarMensaje("Los rubros se asociaron de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al asociar los rubros.", "Aviso");
        }
    }
}