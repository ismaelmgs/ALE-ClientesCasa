using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewMttoPDF : IBaseView
    {
        string sReferencia { get; set; }
        void Mensaje(string sMensaje);
        void LoadInfoPoliza(DataSet ds);
        void LoadReferencia(bool ban);

        event EventHandler eSearchInfoPoliza;
        event EventHandler eSearchDetailReferencia;

        string sMatriculaRef { get; set; }
        int iMesRef { get; set; }
        int iAnioRef { get; set; }
        string sMonedaRef { set; get; }
    }
}
