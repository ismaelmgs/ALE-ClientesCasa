using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ClientesCasa.Interfaces
{
    public interface IViewImagenesMatricula : IBaseView
    {
        object[] oArray { get; }
        object[] oArrImagenMat { get; }
        DataTable dtClientes { set; get; }
        int iIdAeronave { set; get; }

        int iIdImagen { set; get; }
        void LLenaClientes(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LLenaImagenesMatricula(DataTable dt);

        event EventHandler eSaveImagenesMatricula;
        event EventHandler eGetImagenesMatricula;
    }
}
