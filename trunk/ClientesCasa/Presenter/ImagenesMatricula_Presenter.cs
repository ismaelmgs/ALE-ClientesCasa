using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;

namespace ClientesCasa.Presenter
{
    public class ImagenesMatricula_Presenter : BasePresenter<IViewImagenesMatricula>
    {
        private readonly DBImagenesMatricula oIGesCat;

        public ImagenesMatricula_Presenter(IViewImagenesMatricula oView, DBImagenesMatricula oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eSaveImagenesMatricula += eSaveImagenesMatricula_Presenter;
            oIView.eGetImagenesMatricula += eGetImagenesMatricula_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = new DBImagenesMatricula().DBGetObtieneClientesContratos(oIView.oArray);
            oIView.LLenaClientes(oIView.dtClientes);
        }

        protected void eSaveImagenesMatricula_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetInsertaDocumentoAsociadoContrato(oIView.oArrImagenMat))
                oIView.MostrarMensaje("La imágen se agregó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al agregar la imágen.", "Aviso");
        }

        protected void eGetImagenesMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.LLenaImagenesMatricula(oIGesCat.DBGetConsultaImagenesMatricula(oIView.iIdAeronave));
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetEliminaImagenMatricula(oIView.iIdImagen))
                oIView.MostrarMensaje("La imágen se inactivó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al inactivar la imágen.", "Aviso");
        }
    }
}