using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;

namespace ClientesCasa.Presenter
{
    public class Proveedores_Presenter : BasePresenter<IViewProveedores>
    {
        private readonly DBProveedores oIGesCat;

        public Proveedores_Presenter(IViewProveedores oView, DBProveedores oCI)
            : base(oView)
        {
            oIGesCat = oCI;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadProveedores(oIGesCat.DBGetObtieneProveedoresExistentes);
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            string sMensaje = string.Empty;
            if (!oIGesCat.DBGetExisteProveedorenBD(oIView.sDescProv, ref sMensaje, oIView.iIdProv))
            {
                if (oIGesCat.DBSetInsertaProv(oIView.oArrProv))
                    oIView.MostrarMensaje("El proveedor se insertó de manera correcta.", "Aviso");
                else
                    oIView.MostrarMensaje("Ocurrio un error al insertar el proveedor.", "Aviso");
            }
            else
                oIView.MostrarMensaje(sMensaje, "Aviso");
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            string sMensaje = string.Empty;
            if (!oIGesCat.DBGetExisteProveedorenBD(oIView.sDescProv, ref sMensaje, oIView.iIdProv))
            {
                if (oIGesCat.DBSetActualizaProv(oIView.oArrProv))
                    oIView.MostrarMensaje("El proveedor se actualizó de manera correcta.", "Aviso");
                else
                    oIView.MostrarMensaje("Ocurrio un error al actualizar el proveedor.", "Aviso");
            }
            else
                oIView.MostrarMensaje(sMensaje, "Aviso");
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetEliminaProveedor(oIView.iIdProv))
                oIView.MostrarMensaje("El proveedor se inactivó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al inactivar el proveedor.", "Aviso");
        }
    }
}