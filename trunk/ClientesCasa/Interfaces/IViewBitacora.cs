using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClientesCasa.Objetos;

namespace ClientesCasa.Interfaces
{
    public interface IViewBitacora : IBaseView
    {
        object[] oArray { get; }
        //string iIdCliente { set; get; }
        //DataTable dtPaises { set; get; }
        //int iIdPais { set; get; }
        //int iIdPaisDE { set; get; }
        string sMatricula { set; get; }
        string sFolio { set; get; }
        string sIdBitacora { set; get; }
        string sFolioDis { set; get; }
        string sIdBitacoraDis { set; get; }
        string sIdDiscrepancia { set; get; }
        string sIdDiscrepanciaComp { set; get; }
        string sIdComponente { set; get; }
        string sIdSIC { set; get; }
        string sIdPIC { set; get; }
        bool bBandera { set; get; }
        Bitacora oBitacora { set; get; }
        Discrepancia oDiscrepancia { set; get; }
        Componente oComponente { set; get; }
        DataTable dtMatriculas { set; get; }
        //int iIdIntercambio { set; get; }



        void LoadBitacora(DataTable dt);
        void LoadDiscrepancias(DataTable dt);
        void LoadComponentes(DataTable dt);
        void LoadSICPIC(string[] sSICPIC);
        //void CargaEstados(DropDownList ddl, DataTable dt);
        void LlenaIntercambios(DataTable dt);
        void LoadGrupoModelo(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        void CargaMatricula(DataTable dtMatricula);
        void LlenarBitacoraPiernas(DataTable dt);



        event EventHandler eGetMatricula;
        event EventHandler eGetConsultaBitacoraPiernas;
        event EventHandler eGetSicPic;
        event EventHandler eNewObjDiscrepancia;
        event EventHandler eSearchObjDiscrepancia;
        event EventHandler eDeleteObjDiscrepancia;
        event EventHandler eGetDiscrepancias;
        event EventHandler eEditObjDiscrepancia;
        event EventHandler eNewObjComponente;
        event EventHandler eSearchObjComponente;
        event EventHandler eDeleteObjComponente;
        event EventHandler eGetComponentes;
        event EventHandler eEditObjComponente;
    }
}
