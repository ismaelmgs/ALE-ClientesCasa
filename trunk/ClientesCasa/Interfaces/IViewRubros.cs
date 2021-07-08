using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewRubros : IBaseView
    {
        int iIdRubro { set; get; }
        string sDescRubro { get; }
        object[] oArrRubroI { get; }
        object[] oArrRubroU { get; }
        string sDescCuenta { get; }
        DataTable dtCuentasRubro { set; get; }
        string sFiltroCuentas { set; get; }
        int iIdCuenta { set; get; }
        string sNombreCuenta { get; }


        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadRubros(DataTable dt);
        void LLenaComboCuentas(DataTable dt);


        event EventHandler eNewCuenta;
        event EventHandler eSearchCuentas;
        event EventHandler eGetCuentas;
        event EventHandler eSetEliminaCuenta;
    }
}
