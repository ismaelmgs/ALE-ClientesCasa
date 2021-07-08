using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class Facturantes_Presenter : BasePresenter<IViewFacturantes>
    {
        private readonly DBFacturantes oIGesCat;

        public Facturantes_Presenter(IViewFacturantes oView, DBFacturantes oCI) : base(oView)
        {
            oIGesCat = oCI;

            oIView.eGetMatriculas += eGetMatriculas_Presenter;
            oIView.eSetEliminaFacturante += eSetEliminaFacturante_Presenter;
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGesCat.DBGetObtieneFacturantesPorcliente(oIView.oArray));
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMatriculasAsignadas(oIGesCat.ObtieneContratosPorCusNum(oIView.sCustNum));
        }
        protected void eGetMatriculas_Presenter(object sender, EventArgs e)
        {
            object[] oFiltro = new object[8] { "@ClaveCliente", "", "@ClaveContrato", "", "@Estatus",  2, "@Matricula", "" };
            oIView.LoadMatriculas(new DBContratos().DBGetObtieneClientesContratos(oFiltro));
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            oIGesCat.EliminaContratosDelFacturante(oIView.sCustNum);
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.InsertaContratoAFacturante(oIView.sCustNum, oIView.sClaveContrato))
                oIView.MostrarMensaje("Los contratos se asociaron de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al asociar los contratos", "Aviso");
        }
        protected void eSetEliminaFacturante_Presenter(object sender, EventArgs e)
        {
            oIGesCat.EliminaUnContratoAsociadoPorID(oIView.iIdFacturante);
        }
    }
}