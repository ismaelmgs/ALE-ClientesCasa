using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ClientesCasa.Interfaces
{
    public interface IViewTipoGasto : IBaseView
    {
        string sDescConcepto { set; get; }
        int iIdConcepto { set; get; }
        object[] oArrTipoG { get; }

        DataTable dtTipoGastos { set; get; }
        void LoadTipoGasto(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
    }
}
