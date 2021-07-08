using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewDashboardMain : IBaseView
    {
        void Mensaje(string sMensaje);
        void LoadPendientes(DataSet ds);
        void LoadPendientesPilotos(DataSet ds);

        event EventHandler eSearchGral;
        event EventHandler eSearchPendientesPilotos;
    }
}
