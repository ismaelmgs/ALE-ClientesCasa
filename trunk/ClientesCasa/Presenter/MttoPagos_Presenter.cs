using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class MttoPagos_Presenter :BasePresenter<IViewMttoPagos>
    {
        private readonly DBMttoPagos oIGesCat;

        public MttoPagos_Presenter(IViewMttoPagos oView, DBMttoPagos oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eSearchPagos += eSearchPagos_Presenter;
            oIView.eUpaListaPagos += eUpaListaPagos_Presenter;
            oIView.eUpaMontoPago += eUpaMontoPago_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = new DBContratos().DBGetObtieneClientesContratos(oIView.oArray);
            oIView.LLenaClientes(oIView.dtClientes);
        }

        protected void eSearchPagos_Presenter(object sender, EventArgs e)
        {
            DataSet dsPagos = new DataSet();
            dsPagos.Tables.Add(oIGesCat.DBGetObtienePagosFacturante(oIView.sClaveCliente, oIView.iMes, oIView.iAnio));
            dsPagos.Tables.Add(oIGesCat.DBGetObtieneFacturantesCliente(oIView.sClaveCliente));
            oIView.dsPagos = dsPagos;
        }
        protected void eUpaListaPagos_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetActualizaListaPagos(oIView.sClaveCliente, oIView.iAnio, oIView.iMes);
        }
        protected void eUpaMontoPago_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetActualizaMontosPagos(oIView.iIdPago, oIView.dMonto);
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetInsertaPagoEstimado(oIView.oPagoEstimado);
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetEliminaPagoEstimado(oIView.iIdPago);
        }
    }
}