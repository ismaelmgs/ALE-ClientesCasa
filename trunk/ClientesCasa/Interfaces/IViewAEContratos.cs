using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewAEContratos : IBaseView
    {
        object[] oArray { get; }
        string iIdCliente { set; get; }
        int iIdContrato { set; get; }
        ClienteContrato oCliente { set; get; }
        DataTable dtMatriculas { set; get; }
        ClienteContrato oContrato { set; get; }
        Contrato_Tarifas objContratosTarifas { set; get; }
        Contrato_CobrosDescuentos objCobrosDesc { set; get; }
        Contrato_Intercambios oIntercambio { set; get; }

        void LoadClientes(DataTable dt);
        void LoadGrupoModelo(DataTable dt);
        void RedireccionaAccordion(int iIndex);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LlenaIntercambios(DataTable dt);


        event EventHandler eGetGrupoModelo;
        event EventHandler eGetMatriculas;
        event EventHandler eSaveGenerales;
        event EventHandler eSaveTarifas;
        event EventHandler eSaveDescuentos;
        event EventHandler eNewIntercambio;
        event EventHandler eGetContratoDetalle;
        event EventHandler eSaveIntercambio;
        event EventHandler eUpdateIntercambio;
        event EventHandler eDeleteIntercambio;
    }
}
