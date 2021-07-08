using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Interfaces;
using ClientesCasa.DomainModel;
using System.Data;
using NucleoBase.Core;

namespace ClientesCasa.Presenter
{
    public class Comprobantes_Presenter : BasePresenter<IViewComprobantes>
    {
        private readonly DBComprobantes oIGesCat;
        public Comprobantes_Presenter(IViewComprobantes oView, DBComprobantes oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eGetDocuments += eGetDocuments_Presenter;
            oIView.eGetDocumentId += eGetDocumentId_Presenter;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtContratos = oIGesCat.DBGetObtieneClientesMatriculasFiltros(oIView.oArr);
            oIView.LoadClientesMatriculas();
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGastos(oIGesCat.DBGetObtieneGastosPataComprobantes(oIView.sMatricula, oIView.iMes, oIView.iAnio));
        }
        protected void eGetDocuments_Presenter(object sender, EventArgs e)
        {
            oIView.dtDocumentos = oIGesCat.DBGetObtieneDocumentosPorGasto(oIView.iIdGasto);
        }
        protected void eGetDocumentId_Presenter(object sender, EventArgs e)
        {
            oIView.dtImagen = oIGesCat.DBGetObtieneImagenPorId(oIView.iIdImagen);
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetInsertaComprobante(oIView.oComprobante))
                oIView.MostrarMensaje("Se insertó el comprobante de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al insertar el comprobante.", "Aviso");
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            if(oIGesCat.DBSetEliminaComprobante(oIView.iIdImagen))
                oIView.MostrarMensaje("Se eliminó el comprobante de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al eliminar el comprobante.", "Aviso");
        }
    }
}