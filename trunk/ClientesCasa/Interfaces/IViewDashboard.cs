using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewDashboard : IBaseView
    {
        void Mensaje(string sMensaje);
        void LoadContratos(DataSet ds);
        void LoadAeronaves(DataSet ds);
        void LoadSeguros(DataSet ds);
        void LoadGastos(DataSet ds);
        void LoadPilotos(DataSet ds);
        

        event EventHandler eSearchCont;
        event EventHandler eSearchGastos;
        event EventHandler eSearchPilotos;
        event EventHandler eSearchAeronaves;
    }
}
