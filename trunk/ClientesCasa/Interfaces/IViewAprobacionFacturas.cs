using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewAprobacionFacturas : IBaseView
    {
        int iBanCorrecto { get; set; }
        string sNumDoc { get; }
        string sFechaDesde { get; }
        string sFechaHasta { get; }
        string sTipoMtto { get; }
        string sFlota { get; }
        string sUsuario { get; }
        int iDocEntry { get; set; }
        string sCadArchivo { set; get; }
        string sFechaDoc { get; }
        string sArchivo { get; }
        void LoadDetalleFacturasASC(DataTable dt);
        void LoadFacturasPart(DataTable dt);
        void LoadFacturasASC(DataTable dt);
        List<FacturaAprobacionASC> ListaFacturas { set; get; }

        event EventHandler eSearchFacturasAprobar;
        event EventHandler eSearchDetalleFac;
        event EventHandler eUpdateFacturas;
        event EventHandler eSetProcesarAprobacion;
        event EventHandler eGetValidaXML;
        event EventHandler eGetValidaPDF;
        event EventHandler eSearchFacturasPart;
    }
}
