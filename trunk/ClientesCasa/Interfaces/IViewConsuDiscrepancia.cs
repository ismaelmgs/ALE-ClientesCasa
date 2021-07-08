using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClientesCasa.Objetos;

namespace ClientesCasa.Interfaces
{
    public interface IViewConsuDiscrepancia: IBaseView
    {
        object[] oArray { get; }

        string sIdBitacoraDis { set; get; }
        string sIdDiscrepancia { set; get; }
        Discrepancia oDiscrepancia { set; get; }

        void LoadDiscrepancias(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        void CargaMatricula(DataTable dtMatricula);



        event EventHandler eGetMatricula;
        event EventHandler eEditObjDiscrepancia;
        event EventHandler eSearchObjDiscrepancia;
    }
}
