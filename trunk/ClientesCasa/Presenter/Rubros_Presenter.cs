using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class Rubros_Presenter : BasePresenter<IViewRubros>
    {
        private readonly DBRubros oIGesCat;

        public Rubros_Presenter(IViewRubros oView, DBRubros oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eNewCuenta += eNewCuenta_Presenter;
            oIView.eSearchCuentas += eSearchCuentas_Presenter;
            oIView.eGetCuentas += eGetCuentas_Presenter;
            oIView.eSetEliminaCuenta += eSetEliminaCuenta_Presenter;
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            string sMensaje = string.Empty;
            if (!oIGesCat.DBGetExisteRubroenBD(oIView.sDescRubro, ref sMensaje, oIView.iIdRubro))
            {
                if (oIGesCat.DBSetInsertaRubro(oIView.oArrRubroI))
                    oIView.MostrarMensaje("El rubro se insertó de manera correcta.", "Aviso");
                else
                    oIView.MostrarMensaje("Ocurrio un error al insertar el rubro.", "Aviso");
            }
            else
                oIView.MostrarMensaje(sMensaje, "Aviso");
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            string sMensaje = string.Empty;
            if (!oIGesCat.DBGetExisteRubroenBD(oIView.sDescRubro.ToUpper(), ref sMensaje, oIView.iIdRubro))
            {
                if (oIGesCat.DBSetActualizaRubro(oIView.oArrRubroU))
                    oIView.MostrarMensaje("El rubro se actualizó de manera correcta.", "Aviso");
                else
                    oIView.MostrarMensaje("Ocurrio un error al actualizar el rubro.", "Aviso");
            }
            else
                oIView.MostrarMensaje(sMensaje, "Aviso");
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadRubros(oIGesCat.DBGetObtieneRubrosExistentes);
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetEliminaRubro(oIView.iIdRubro))
                oIView.MostrarMensaje("El rubro se inactivó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al inactivar el rubro.", "Aviso");
        }

        protected void eNewCuenta_Presenter(object sender, EventArgs e)
        {
            string sMensaje = string.Empty;
            if (!oIGesCat.DBGetExisteCuentaEnOtroRubro(oIView.sDescCuenta, ref sMensaje))
            {
                if (oIGesCat.DBSetInsertaCuentaAsociadaRubro(oIView.iIdRubro, oIView.sDescCuenta, oIView.sNombreCuenta))
                    oIView.MostrarMensaje("La cuenta se registró de manera correcta.", "Aviso");
                else
                    oIView.MostrarMensaje("Ocurrió un error al registrar la cuenta.", "Aviso");
            }
            else
                oIView.MostrarMensaje(sMensaje, "Aviso");
        }
        protected void eSearchCuentas_Presenter(object sender, EventArgs e)
        {
            oIView.dtCuentasRubro = oIGesCat.DBGetObtieneCuentasPorRubro(oIView.iIdRubro);
        }
        protected void eGetCuentas_Presenter(object sender, EventArgs e)
        {
            oIView.LLenaComboCuentas(new DBAccesoSAP().DBGetObtieneCuentasFiltradas(oIView.sFiltroCuentas));
        }
        protected void eSetEliminaCuenta_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetEliminaCuentaRubro(oIView.iIdCuenta))
                oIView.MostrarMensaje("La cuenta se eliminó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al eliminar la cuenta.", "Aviso");
        }
    }
}