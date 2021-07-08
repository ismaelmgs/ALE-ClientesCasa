using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewRubrosMatricula : IBaseView
    {
        int iIdAeronave { set; get; }
        object[] oArr { get; }
        List<RubrosMatricula> oLstRubros { set; get; }

        void LoadMatriculas(DataTable dt);
        void LoadRubros(DataSet ds);
        void MostrarMensaje(string sMensaje, string sCaption);
    }
}
