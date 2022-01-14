using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class CorteMensual_Presenter : BasePresenter<IViewCorteMensual>
    {
        private readonly DBCorteMensual oIGesCat;

        public CorteMensual_Presenter(IViewCorteMensual oView, DBCorteMensual oCI)
            : base(oView)
        {
            oIGesCat = oCI;

        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = new DBContratos().DBGetObtieneClientesContratos(oIView.oArray);
            oIView.LLenaClientes(oIView.dtClientes);
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaCalculoCorte(new DBAnalisisCostoOpe().DBGetObtieneTotalesEdoCuenta(oIView.sClaveContrato, oIView.iAnio, oIView.iMes));
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            string sRes = oIGesCat.DBInsertaCorteMensualPorContrato(oIView.sClaveContrato, oIView.iMes, oIView.iAnio);
            if (sRes != string.Empty)
            {
                oIView.InhabilitaBoton();
                oIView.MostrarMensaje("La información de guardo correctamente.", "Aviso");
            }
            else
                oIView.MostrarMensaje("Ocurrió un error al guardar el corte mensual.", "Aviso");


        }
    }
}