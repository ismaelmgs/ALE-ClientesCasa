using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientesCasa.Objetos;

namespace ClientesCasa.Interfaces
{
    public interface IViewMantenimiento : IBaseView
    {
        object[] oArray { get; }
        object[] oArrGastos { get; }
        DataTable dtClientes { set; get; }
        DataTable dtRubros { set; get; }
        DataTable dtTiposGasto { set; get; }
        List<GastoEstimado> oLstGastoE { set; get; }
        List<MantenimientoGastos> oLstContratosGasto { set; get; }
        DataTable dtLegs { set; get; }
        int iTrip { set; get; }
        long iIdGasto { set; get; }
        GastoEstimado oGastoE { set; get; }
        string sMatricula { set; get; }
        DateTime dtFechaVlo { set; get; }

        DataTable dtGastosMEX { set; get; }
        DataTable dtGastosUSA { set; get; }

        void LLenaClientes(DataTable dt);
        void CargaGastosMEXUSA(DataSet ds);


        event EventHandler eUpaGastos;
        event EventHandler eInsImpGasto;
        event EventHandler eSearchLegs;
        event EventHandler eNewGastoEstimado;
        event EventHandler eUpaComprobante;
    }
}
