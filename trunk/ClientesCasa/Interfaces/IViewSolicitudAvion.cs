using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewSolicitudAvion : IBaseView
    {
        int iIdFlota { set; get; }
        int iIdSolicitud { set; get; }
        SolicitudAvion oSolicitud { set; get; }


        void CargaFlotas(DataTable dtFlotas);
        void MostrarMensaje(string sMensaje, string sCaption);
        void CargaSolicitudes(DataTable dtSol);
        void CargaMatriculas(DataTable dtMats);


        event EventHandler eSearchFlota;
        event EventHandler eSearchMats;

    }
}
