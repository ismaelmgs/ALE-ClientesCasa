using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewComprobantes : IBaseView
    {
        object[] oArr { get; }
        DataTable dtContratos { set; get; }
        DataTable dtDocumentos { set; get; }
        int iIdGasto { set; get; }
        int iIdImagen { set; get; }
        DataTable dtImagen { set; get; }
        Comprobante oComprobante { set; get; }
        string sMatricula { set; get; }
        int iMes { set; get; }
        int iAnio { set; get; }

        void LoadClientesMatriculas();
        void LoadGastos(DataSet dt);
        void MostrarMensaje(string sMensaje, string sCaption);


        event EventHandler eGetDocuments;
        event EventHandler eGetDocumentId;
    }
}
