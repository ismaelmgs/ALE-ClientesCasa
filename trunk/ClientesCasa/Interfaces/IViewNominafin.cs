using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewNominafin : IBaseView
    {
        int iIdFolio { set; get; }
        DataSet dsDetalleNomina { set; get; }
        DataTable dtExiste { set; get; }
        string sTaxCode { set; get; }
        string sEmpresa { set; get; }
        int iNoFact { set; get; }
        int iEstatus { set; get; }
        //int iIdFactura { set; get; }
        HeaderFactura oHeader { get; }


        void LoadParametros(DataTable dt);
        void LoadDetalleNomina();
        void MostrarMensaje(string sMensaje, string sCaption);


        event EventHandler eUpdEstatusFact;
    }
}
