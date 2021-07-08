using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IVIewEstadoCuenta : IBaseView
    {
        DataTable dtClientes { set; get; }
        object[] oArrFiltros { get; }
        int iMes { get; set; }
        int iAnio { get; set; }
        DataSet dsGastos { set; get; }
        string sMatricula { set; get; }
        string sClaveContrato { set; get; }
        DataTable dtTotal { set; get; }
        DataSet dsEdoCuenta { set; get; }


        void LLenaClientes(DataTable dt);
        void LlenaReporte(string sHTML);


        event EventHandler eSearchTotales;
        event EventHandler eSearchEdoCuenta;
    }
}
