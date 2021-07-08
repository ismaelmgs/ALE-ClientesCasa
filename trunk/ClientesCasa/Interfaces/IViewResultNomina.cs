using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewResultNomina : IBaseView
    {
        int iIdFolio { set; get; }
        DataSet dsDetalleNomina { set; get; }
        List<PolizaNominaSAP> oLstPoliza { set; get; }
        string sTaxCode { set; get; }
        string sEmpresa { set; get; }
        int iNoFact { set; get; }
        string sFileName { set; get; }
        byte[] oArrFile { set; get; }

        int iExistePDF { set; get; }
        int iExisteXML { set; get; }

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadParametros(DataTable dt);
        void LoadDetalleNomina();
        void LlenaEmpleadosPorEmpresa(DataTable dt);
        void ArmaListadoFacturas(string sEmpresa, List<ConceptosSAP> olst);

        event EventHandler eUpdApruebaFactura;
        event EventHandler eSaveXMLFile;
        event EventHandler eSavePDFFile;

        event EventHandler eValidaPDFFile;
        event EventHandler eValidaXMLFile;
    }
}
