using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewMttoPagos : IBaseView
    {
        object[] oArray { get; }
        int iAnio { set; get; }
        int iMes { set; get; }
        string sClaveCliente { set; get; }
        string sClaveContrato { set; get; }
        DataTable dtClientes { set; get; }
        DataSet dsPagos { set; get; }
        int iIdPago { set; get; }
        decimal dMonto { set; get; }
        PagoEstimado oPagoEstimado { get; }
        

        void LLenaClientes(DataTable dt);


        event EventHandler eSearchPagos;
        event EventHandler eUpaListaPagos;
        event EventHandler eUpaMontoPago;
    }
}
