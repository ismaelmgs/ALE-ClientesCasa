using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class AEContratos_Presenter : BasePresenter<IViewAEContratos>
    {
        private readonly DBAEContratos oIGesCat;

        public AEContratos_Presenter(IViewAEContratos oView, DBAEContratos oGC)
            : base(oView)
        {
            oIGesCat = oGC;

            oIView.eGetGrupoModelo += eGetGrupoModelo_Presenter;
            oIView.eGetMatriculas += eGetMatriculas_Presenter;
            oIView.eSaveGenerales += eSaveGenerales_Presenter;
            oIView.eSaveTarifas += eSaveTarifas_Presenter;
            oIView.eSaveDescuentos += eSaveDescuentos_Presenter;
            oIView.eGetContratoDetalle += eGetContratoDetalle_Presenter;
            oIView.eSaveIntercambio += eSaveIntercambio_Presenter;
            oIView.eUpdateIntercambio += eUpdateIntercambio_Presenter;
            oIView.eDeleteIntercambio += eDeleteIntercambio_Presenter;
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadClientes(new DBContratos().DBGetObtieneClientesContratos(oIView.oArray));
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.oCliente = new DBContratos().oGetObtieneDatosCliente(oIView.iIdCliente);
        }
        protected void eGetGrupoModelo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGrupoModelo(new DBContratos().DBGetObtieneGruposModelo());
        }
        protected void eGetMatriculas_Presenter(object sender, EventArgs e)
        {
            oIView.dtMatriculas = new DBContratos().DBGetObtieneMatriculas;
        }
        protected void eSaveGenerales_Presenter(object sender, EventArgs e)
        {
            int iRes = 0;
            if (oIView.iIdContrato == 0)
            {
                iRes = oIGesCat.DBSaveGenerales(oIView.oContrato);
            }
            else
            {
                iRes = oIGesCat.DBActualizaContrato(oIView.oContrato);
            }

            if (iRes > 0)
            {
                oIView.RedireccionaAccordion(1);
                oIView.MostrarMensaje("Los datos generales se han guardado correctamente.", "Aviso");
            }
        }
        protected void eSaveTarifas_Presenter(object sender, EventArgs e)
        {
            int iRes = 0;
            DataTable dtInter = oIGesCat.DBGetTarifasContrato(oIView.iIdContrato);
            
            if (dtInter.Rows.Count == 0)
                iRes = oIGesCat.DBSaveTarifa(oIView.objContratosTarifas);
            else
                iRes = oIGesCat.DBUpdateTarifasContrato(oIView.objContratosTarifas);

            oIView.RedireccionaAccordion(2);
            oIView.MostrarMensaje("Las Tarifas se han guardado correctamente.", "Aviso");
        }
        protected void eSaveDescuentos_Presenter(object sender, EventArgs e)
        {
            int iRes = 0;
            DataTable dtCob = oIGesCat.DBGetCobrosDescuentosContrato(oIView.iIdContrato);
            if (dtCob.Rows.Count == 0)
            {
                iRes = oIGesCat.DBSaveCobros(oIView.objCobrosDesc);
            }
            else
                iRes = oIGesCat.DBUpdateCobros(oIView.objCobrosDesc);

            oIView.RedireccionaAccordion(3);
            oIView.MostrarMensaje("Los cobros y descuentos se han guardado correctamente.", "Aviso");
        }
        protected void eGetContratoDetalle_Presenter(object sender, EventArgs e)
        {
            oIView.oContrato = new DBContratos().DBGetObtieneDetalleContrato(oIView.iIdContrato);
            oIView.objContratosTarifas = oIGesCat.DBGetTarifa(oIView.iIdContrato);
            oIView.objCobrosDesc = oIGesCat.DBGetCobros(oIView.iIdContrato);
            oIView.LlenaIntercambios(oIGesCat.DBGetIntercambios(oIView.iIdContrato));
        }
        protected void eSaveIntercambio_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGesCat.DBSaveIntercambio(oIView.oIntercambio);
                oIView.LlenaIntercambios(oIGesCat.DBGetIntercambios(oIView.iIdContrato));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void eUpdateIntercambio_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGesCat.DBUpdateIntercambioContratos(oIView.oIntercambio);
                oIView.LlenaIntercambios(oIGesCat.DBGetIntercambios(oIView.iIdContrato));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void eDeleteIntercambio_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGesCat.DBDeleteIntercambio(oIView.oIntercambio);
                oIView.LlenaIntercambios(oIGesCat.DBGetIntercambios(oIView.iIdContrato));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}