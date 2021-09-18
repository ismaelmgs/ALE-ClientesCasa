using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;

namespace ClientesCasa.Presenter
{
    public class TipoGasto_Presenter : BasePresenter<IViewTipoGasto>
    {
        private readonly DBTipoGasto oIGesCat;

        public TipoGasto_Presenter(IViewTipoGasto oView, DBTipoGasto oCI)
            : base(oView)
        {
            oIGesCat = oCI;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTipoGasto(oIGesCat.DBGetObtieneTipoGastoExistentes);
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            string sMensaje = string.Empty;
            if (!oIGesCat.DBGetExisteTipoGenBD(oIView.sDescConcepto, ref sMensaje, oIView.iIdConcepto))
            {
                if (oIGesCat.DBSetInsertaTipoG(oIView.oArrTipoG))
                    oIView.MostrarMensaje("El tipo de gasto se insertó de manera correcta.", "Aviso");
                else
                    oIView.MostrarMensaje("Ocurrio un error al insertar el tipo de gasto.", "Aviso");
            }
            else
                oIView.MostrarMensaje(sMensaje, "Aviso");
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            string sMensaje = string.Empty;
            if (!oIGesCat.DBGetExisteTipoGenBD(oIView.sDescConcepto, ref sMensaje, oIView.iIdConcepto))
            {
                if (oIGesCat.DBSetActualizaTipoGasto(oIView.oArrTipoG))
                    oIView.MostrarMensaje("El tipo de gasto se actualizó de manera correcta.", "Aviso");
                else
                    oIView.MostrarMensaje("Ocurrio un error al actualizar el tipo de gasto.", "Aviso");
            }
            else
                oIView.MostrarMensaje(sMensaje, "Aviso");
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetEliminaTipoGasto(oIView.iIdConcepto))
                oIView.MostrarMensaje("El tipo de gasto se inactivó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al inactivar el tipo de gasto.", "Aviso");
        }
    }
}