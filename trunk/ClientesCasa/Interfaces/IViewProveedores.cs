using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ClientesCasa.Interfaces
{
    public interface IViewProveedores : IBaseView
    {
        string sDescProv { set; get; }
        int iIdProv { set; get; }
        object[] oArrProv { get; }

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadProveedores(DataTable dt);
    }
}
