using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class SolicitudAviones_Presenter : BasePresenter<IViewSolicitudAvion>
    {
        private readonly DBSolicitudAviones oIGesCat;
        public SolicitudAviones_Presenter(IViewSolicitudAvion oView, DBSolicitudAviones oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eSearchFlota += eSearchFlota_Presenter;
            oIView.eSearchMats += eSearchMats_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.CargaSolicitudes(oIGesCat.DBGetObtieneUltimasSolicitudes());
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            if(oIGesCat.DBSetInsertaSolicitudAvion(oIView.oSolicitud) > 0)
            {
                oIView.CargaSolicitudes(oIGesCat.DBGetObtieneUltimasSolicitudes());
                oIView.MostrarMensaje("La solicitud de avión se registró correctamente.", "Aviso");
            }
            else
                oIView.MostrarMensaje("Ocurrió un error al registrar la solicitud.", "Aviso");
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaSolicitudVuelo(oIView.oSolicitud, oIView.iIdSolicitud) > 0)
            {
                oIView.CargaSolicitudes(oIGesCat.DBGetObtieneUltimasSolicitudes());
                oIView.MostrarMensaje("La solicitud de avión se actualizó correctamente.", "Aviso");
            }
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar la solicitud.", "Aviso");
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.oSolicitud = oIGesCat.DBGetObtieneSolicitudPorId(oIView.iIdSolicitud);
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetCancelaSolicitud(oIView.iIdSolicitud);
            oIView.CargaSolicitudes(oIGesCat.DBGetObtieneUltimasSolicitudes());
        }

        protected void eSearchFlota_Presenter(object sender, EventArgs e)
        {
            oIView.CargaFlotas(oIGesCat.DBGetObtieneFlotasMJ());
        }

        protected void eSearchMats_Presenter(object sender, EventArgs e)
        {
            oIView.CargaMatriculas(oIGesCat.DBGetObtieneMatriculasPorFlota(oIView.iIdFlota));
        }

    }
}