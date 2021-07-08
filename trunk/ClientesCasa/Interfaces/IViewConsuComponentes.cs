using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClientesCasa.Objetos;

namespace ClientesCasa.Interfaces
{
    public interface IViewConsuComponentes : IBaseView
    {
        object[] oArray { get; }

        string sIdDiscrepancia { set; get; }
        string sIdComponente { set; get; }
        Componente oComponente { set; get; }

        void LoadComponentes(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        //void CargaMatricula(DataTable dtMatricula);



        //event EventHandler eGetMatricula;
        event EventHandler eEditObjComponente;
        event EventHandler eSearchObjComponente;
    }
}
