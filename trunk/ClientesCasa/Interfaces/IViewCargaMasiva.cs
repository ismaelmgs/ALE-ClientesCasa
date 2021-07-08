using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewCargaMasiva : IBaseView
    {
        string sFormato { get; }
        string sCodigoProveedor { get; }
        string sMoneda { get; }
        string sAlmacen { get; }
        string sMatricula { get; }
        string sCodigoImpuesto { get; }
        string sLugar { get; }

        string sFila { get; }
        string sCampo { get; }
        string sValor { get; }
        string sStatus { get; }
        string sDescripcion { get; }

        List<Factura> ListaFacturas { set; get; }
        List<ErroresValidacion> ListaErrores { set; get; }
        string sCadArchivo { set; get; }
        
        string sValoresValidacion { set; get; }

        void Mensaje(string sMensaje);
        void LoadExistencia(DataTable dt);

        void LoadCodigoProveedor(DataTable dt);
        void LoadCodigoArticulo(DataTable dt);
        void LoadIVA(DataTable dt);
        void LoadDescripcionArticulo(DataTable dt);
        string sEmpresa { get; }
        string sFactura { get; }
        string sFechaFactura { get; }
        string sServicio { get; }
        string sCodigoArticulo { get; }
        string sIVA { get; }
        string sCantidad { get; }
        string sXML_OMA { get; set; }
        string sPDF_OMA { get; set; }
        string sTotalLinea { get; set; }

        string sMsgError { get; set; }
        DataTable dtError { set; get; }
        string sDescripcionArticulo { get; }
        string sCodImp { get; }

        //Formato AMAIT
        string sDesArticulo { get; }

        //Bandera para saber si se insertó correctamente
        int iBanCorrecto { get; set; }

        string sFacExists { get; }
        string sExist { get; set; }
        string sAeroExists { get; }

        event EventHandler eSearchProveedores;
        event EventHandler eSearchMoneda;
        event EventHandler eSearchAlmacen;
        event EventHandler eSearchMatricula;
        event EventHandler eSearchCodigoImpuesto;
        event EventHandler eSearchLugar;
        event EventHandler eSetProcesaArchivo;
        event EventHandler eGetValidaXML;
        event EventHandler eGetValidaPDF;
        event EventHandler eGetCodProveedores;
        event EventHandler eGetCodArticulo;
        event EventHandler eGetIVAImpuesto;
        event EventHandler eSearchAreaDpto;
        event EventHandler eGetDesArticulo;
        event EventHandler eGetFacturaExist;
        event EventHandler eGetAeropuertoExist;
    }
}
