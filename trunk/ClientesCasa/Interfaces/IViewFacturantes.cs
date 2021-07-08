using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewFacturantes : IBaseView
    {
        object[] oArray { get; }
        string sCustNum { set; get; }
        string sClaveContrato { set; get; }
        int iIdFacturante { set; get; }

        void LoadClientes(DataTable dt);
        void LoadMatriculas(DataTable dt);
        void LoadMatriculasAsignadas(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);


        event EventHandler eGetMatriculas;
        event EventHandler eSetEliminaFacturante;
    }
}
