using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewCostoHoraVuelo : IBaseView
    {
        DataTable dtTOTALESTOTALES { set; get; }
        DataTable dtTOTALESFijos { set; get; }
        DataTable dtTOTALESVariables { set; get; }
        DataTable dtFijos { set; get; }
        DataTable dtVariables { set; get; }
        DataTable dtClientes { set; get; }
        object[] oArrFiltros { get; }
        DataSet dsGastos { set; get; }
        string sMatricula { set; get; }
        string sClaveContrato { set; get; }
        DataTable dtTotal { set; get; }
        DateTime dtInicio { set; get; }
        DateTime dtFin { set; get; }
        string sMoneda { set; get; }
        string sHTML { set; get; }
        decimal dTipoCambio { set; }
        DataTable dtTotalesTiempo { set; get; }

        decimal dTotalImporteFijo { set; get; }
        decimal dTotalImporteVar { set; get; }


        void LLenaClientes(DataTable dt);


        event EventHandler eSearchTotales;
    }
}
