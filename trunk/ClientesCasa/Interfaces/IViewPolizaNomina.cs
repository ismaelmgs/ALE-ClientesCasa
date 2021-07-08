using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewPolizaNomina : IBaseView
    {
        List<PolizaNominaNew> ListaPolizas { set; get; }
        //Bandera para saber si se insertó correctamente
        int iSuccess { get; set; }
        string sRFC { get; set; }
        int iIdFolio { set; get; }
        DataTable dtExiste { set; get; }


        void LoadRFC(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eNewProcesaArchivo;
        event EventHandler eSearchRFC;
    }
}
