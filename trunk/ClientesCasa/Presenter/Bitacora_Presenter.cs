using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using NucleoBase.Core;
using System.Data;

namespace ClientesCasa.Presenter
{
    public class Bitacora_Presenter : BasePresenter<IViewBitacora>
    {
        private readonly DBBitacora oIGesCat;
        public Bitacora_Presenter(IViewBitacora oView, DBBitacora oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eGetMatricula += eGetMatricula_Presenter;
            oIView.eGetConsultaBitacoraPiernas += eGetConsultaBitacoraPiernas_Presenter;
            oIView.eNewObjDiscrepancia += NewObjDiscrepancia_Presenter;
            oIView.eSearchObjDiscrepancia += SearchObj_PresenterDiscrepancia;
            oIView.eGetDiscrepancias += eGetDiscrepancias_Presenter;
            oIView.eEditObjDiscrepancia += EditObjDiscrepancia_Presenter;
            oIView.eDeleteObjDiscrepancia += eDeleteObjDiscrepancia_Presenter;
            oIView.eNewObjComponente += NewObjComponente_Presenter;
            oIView.eSearchObjComponente += SearchObj_PresenterComponente;
            oIView.eGetComponentes += eGetComponentes_Presenter;
            oIView.eEditObjComponente += EditObjComponente_Presenter;
            oIView.eDeleteObjComponente += eDeleteObjComponente_Presenter;
            oIView.eGetSicPic += eGetSICPIC_Presenter;
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitacora(oIGesCat.DBGetObtieneBitacora(oIView.oArray));
        }
        protected void eGetMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.CargaMatricula(oIGesCat.DBGetObtieneMatriculas);
        }
        protected void eGetSICPIC_Presenter(object sender, EventArgs e)
        {
            oIView.LoadSICPIC(oIGesCat.DBGetObtieneSICPIC(oIView.sIdSIC, oIView.sIdPIC));
        }
        protected void eGetDiscrepancias_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDiscrepancias(oIGesCat.DBGetObtieneDiscrepancias(oIView.sIdBitacoraDis));
        }
        protected void eGetComponentes_Presenter(object sender, EventArgs e)
        {
            oIView.LoadComponentes(oIGesCat.DBGetObtieneComponentes(oIView.sIdDiscrepanciaComp));
        }
        protected void eGetConsultaBitacoraPiernas_Presenter(object sender, EventArgs e)
        {
            oIView.LlenarBitacoraPiernas(oIGesCat.DBGetObtieneBitacoraPiernas(oIView.sFolio.S(), oIView.sMatricula.S()));
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.oBitacora = oIGesCat.oGetObtieneDatosBitacora(oIView.sIdBitacora);
        }
        protected void SearchObj_PresenterDiscrepancia(object sender, EventArgs e)
        {
            oIView.oDiscrepancia = oIGesCat.oGetObtieneDiscrepancia(oIView.sIdDiscrepancia, oIView.sIdBitacoraDis);
        }
        protected void SearchObj_PresenterComponente(object sender, EventArgs e)
        {
            oIView.oComponente = oIGesCat.oGetObtieneComponente(oIView.sIdComponente, oIView.sIdDiscrepanciaComp);
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetInsertaAdicionalesBitacora(oIView.oBitacora))
                oIView.MostrarMensaje("Los adicionales se insertaron de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al insertar los Adicionales.", "Aviso");
        }
        protected void NewObjDiscrepancia_Presenter(object sender, EventArgs e)
        {
            int iRes = oIGesCat.DBSetInsertaDiscrepancia(oIView.oDiscrepancia);
            if (iRes > 0)
                oIView.MostrarMensaje("La discrepancia se insertó de manera correcta, Folio: " + iRes.S(), "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al insertar la discrepancia.", "Aviso");
        }
        protected void NewObjComponente_Presenter(object sender, EventArgs e)
        {
            int iRes = oIGesCat.DBSetInsertaComponente(oIView.oComponente);
            if (iRes > 0)
                oIView.MostrarMensaje("El componente se insertó de manera correcta, Folio: " + iRes.S(), "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al insertar el componente.", "Aviso");
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaAdicionalesBitacora(oIView.oBitacora))
                oIView.MostrarMensaje("Los adicionales se actualizaron de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar los Adicionales.", "Aviso");
        }
        protected void EditObjDiscrepancia_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaDiscrepancia(oIView.oDiscrepancia))
                oIView.MostrarMensaje("La Discrepancia se actualizó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar la Discrepancia.", "Aviso");
        }
        protected void EditObjComponente_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaComponente(oIView.oComponente))
                oIView.MostrarMensaje("El componente se actualizó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar el componente.", "Aviso");
        }
        protected void eDeleteObjDiscrepancia_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetEliminaDiscrepancia(oIView.oDiscrepancia))
                oIView.MostrarMensaje("La discrepancia se eliminó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al eliminar la discrepancia.", "Aviso");
        }
        protected void eDeleteObjComponente_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetEliminaDiscrepancia(oIView.oComponente))
                oIView.MostrarMensaje("El Componente se eliminó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al eliminar el componente.", "Aviso");
        }
    }
}