using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewEnvioFacturas : IBaseView
    {
        int iBanCorrecto { get; set; }
        string sNumDoc { get; }
        string sFechaDesde { get; }
        string sFechaHasta { get; }
        string sEstatus { get; }
        string sTipoMtto { get; }
        string sFlota { get; }
        string sMatricula { get; }

        int iIdDetalle { get; }
        int iDocEntry { get; }
        string sItemCode { get; }
        string sArticulo { get; }
        string sNvoImporte { get; }

        string sCadArchivo { set; get; }
        string sFechaDoc { get; }
        string sArchivo { get; }

        void LoadFacturas(DataTable dt);
        void LoadMatriculasSAP(DataTable dt);
        void LoadMatriculasMXJ(DataTable dt);
        void LoadFlotas(DataTable dt);
        void LoadMatriculas(DataTable dt);
        void LoadFacturasPart(DataTable dt);
        void LoadListaArticulos(DataTable dt);
        List<FacturaASC> ListaFacturas { set; get; }
        void LoadTipoMtto(DataTable dt);

        event EventHandler eSearchFacturas;
        event EventHandler eSearchMatriculasSAP;
        event EventHandler eSearchMatriculasMXJ;
        event EventHandler eSearchFlotasMXJ;
        event EventHandler eSearchMatriculas;
        event EventHandler eProcesarFacturas;
        event EventHandler eSearchFacturasPart;
        event EventHandler eSearchArticulos;
        event EventHandler eProcesarArticulos;
        event EventHandler eGetValidaPDF;
        event EventHandler eSearchTipoMtto;
    }
}
