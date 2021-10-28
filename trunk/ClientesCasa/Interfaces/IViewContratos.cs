using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ClientesCasa.Interfaces
{
    public interface IViewContratos : IBaseView
    {
        object[] oArray { get; }
        string iIdCliente { set; get; }
        DataTable dtPaises { set; get; }
        int iIdPais { set; get; }
        int iIdPaisDE { set; get; }
        int iIdContrato { set; get; }
        bool bDireccionEnvio { set; get; }
        ClienteContrato oCliente { set; get; }
        ClienteContrato oContrato { set; }
        DataTable dtMatriculas { set; get; }
        int iIdIntercambio { set; get; }
        int iIdDocumento { set; get; }
        Comprobante oComprobante { get; set; }
        DataTable dtDocumentos { get; set; }
        bool bExisteClave { get; set; }
        string sClaveContrato { get; }


        void LoadClientes(DataTable dt);
        void CargaEstados(DropDownList ddl, DataTable dt);
        void LlenaIntercambios(DataTable dt);
        void CargaLista_Rep_Edo_Cuenta(DataTable dt);
        void LoadGrupoModelo(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        void CargaDatosIniciales(DataTable dtSectores, DataTable dtEstatusContrato);
        void LlenaContratos(DataTable dt);
        void LoadDocumentosContratos(DataTable dt);



        event EventHandler eGetPaises;
        event EventHandler eGetEstadosPorPais;
        event EventHandler eGetContratoDetalle;
        event EventHandler eGetGrupoModelo;
        event EventHandler eGetMatriculas;
        event EventHandler eGetDatosIniciales;
        event EventHandler eSaveContrato;
        event EventHandler eUpaContrato;
        event EventHandler eSaveIntercambio;
        event EventHandler eUpaIntercambio;
        event EventHandler eSetEliminaContrato;
        event EventHandler eSetEliminaIntercambio;
        event EventHandler eGetConsultaContratos;

        event EventHandler eGetDocumentosContrato;
        event EventHandler eSetEliminaDocumento;
        event EventHandler eSaveDocumentoContrato;

        event EventHandler eValidaContratoExistente;
    }
}
