using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Data;

namespace ClientesCasa.Presenter
{
    public class Contratos_Presenter : BasePresenter<IViewContratos>
    {
        private readonly DBContratos oIGesCat;

        public Contratos_Presenter(IViewContratos oView, DBContratos oCI)
            : base(oView)
        {
            oIGesCat = oCI;

            oIView.eGetPaises += eGetPaises_Presenter;
            oIView.eGetEstadosPorPais += eGetEstadosPorPais_Presenter;
            oIView.eGetContratoDetalle += eGetContratoDetalle_Presenter;
            oIView.eGetGrupoModelo += eGetGrupoModelo_Presenter;
            oIView.eGetMatriculas += eGetMatriculas_Presenter;
            oIView.eGetDatosIniciales += eGetDatosIniciales_Presenter;
            oIView.eSaveContrato += eSaveContrato_Presenter;
            oIView.eUpaContrato += eUpaContrato_Presenter;
            oIView.eSaveIntercambio += eSaveIntercambio_Presenter;
            oIView.eUpaIntercambio += eUpaIntercambio_Presenter;
            oIView.eSetEliminaContrato += eSetEliminaContrato_Presenter;
            oIView.eSetEliminaIntercambio += eSetEliminaIntercambio_Presenter;
            oIView.eGetConsultaContratos += eGetConsultaContratos_Presenter;

            oIView.eSaveDocumentoContrato += eSaveDocumentoContrato_Presenter;
            oIView.eGetDocumentosContrato += eGetDocumentosContrato_Presenter;
            oIView.eSetEliminaDocumento += eSetEliminaDocumento_Presenter;

            oIView.eValidaContratoExistente += eValidaContratoExistente_Presenter;
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGesCat.DBGetObtieneClientesContratos(oIView.oArray));
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.oCliente = oIGesCat.oGetObtieneDatosCliente(oIView.iIdCliente);
        }
        protected void eGetPaises_Presenter(object sender, EventArgs e)
        {
            oIView.dtPaises = oIGesCat.DBGetObtienePaises;
        }
        protected void eGetEstadosPorPais_Presenter(object sender, EventArgs e)
        {
            if (oIView.bDireccionEnvio)
                oIView.CargaEstados((DropDownList)sender, oIGesCat.DBGetObtieneEstadosPorPais(oIView.iIdPaisDE));
            else
                oIView.CargaEstados((DropDownList)sender, oIGesCat.DBGetObtieneEstadosPorPais(oIView.iIdPais));
        }
        protected void eGetContratoDetalle_Presenter(object sender, EventArgs e)
        {
            DataTable dtListRep = new DataTable();
            dtListRep = oIGesCat.DBGetObtieneListadoRepEdoCuenta();
            oIView.CargaLista_Rep_Edo_Cuenta(dtListRep);
            oIView.oContrato = oIGesCat.DBGetObtieneDetalleContrato(oIView.iIdContrato);
            oIView.LlenaIntercambios(oIGesCat.DBGetIntercambiosContratos(oIView.iIdContrato));
        }
        protected void eGetGrupoModelo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGrupoModelo(oIGesCat.DBGetObtieneGruposModelo());
        }
        protected void eGetMatriculas_Presenter(object sender, EventArgs e)
        {
            oIView.dtMatriculas = oIGesCat.DBGetObtieneMatriculas;
        }
        protected void eGetDatosIniciales_Presenter(object sender, EventArgs e)
        {
            oIView.CargaDatosIniciales(oIGesCat.DBGetObtieneSectoresCliente, oIGesCat.DBGetObtieneEstatusContratos);
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            int iIntCli = oIGesCat.DBSetInsertaCliente(oIView.oCliente);
            if (iIntCli > 0)
            {
                oIView.iIdCliente = iIntCli.S();
                oIView.MostrarMensaje("El cliente se insertó de manera correcta.", "Aviso");
            }
            else
                oIView.MostrarMensaje("Ocurrió un error al insertar al cliente.", "Aviso");
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaCliente(oIView.oCliente))
                oIView.MostrarMensaje("El cliente se actualizó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar al cliente.", "Aviso");
        }
        protected void eSaveContrato_Presenter(object sender, EventArgs e)
        {
            if (!oIGesCat.DBGetValidaExisteContrato(oIView.sClaveContrato))
            {
                int iIdCont = oIGesCat.DBSetInsertaContrato(oIView.oCliente);
                if (iIdCont > 0)
                {
                    oIView.iIdContrato = iIdCont;
                    oIView.MostrarMensaje("El contrato se insertó de manera correcta.", "Aviso");
                }
                else
                    oIView.MostrarMensaje("Ocurrió un error al insertar el contrato.", "Aviso");
            }
            else
                oIView.MostrarMensaje("El contrato escrito ya existe, favor de verificar.", "Aviso");
        }
        protected void eUpaContrato_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaContrato(oIView.oCliente))
                oIView.MostrarMensaje("El contrato se actualízó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar el contrato.", "Aviso");
        }
        protected void eSaveIntercambio_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetInsertaIntercambioContrato(oIView.oCliente))
                oIView.MostrarMensaje("El intercambio se insertó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al insertar el intercambio.", "Aviso");
        }
        protected void eUpaIntercambio_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetActualizaIntercambioContrato(oIView.oCliente))
                oIView.MostrarMensaje("El intercambio se actualízó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrió un error al actualizar el intercambio.", "Aviso");
        }
        protected void eSetEliminaContrato_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetCancelaContratoCC(oIView.iIdContrato);
        }
        protected void eSetEliminaIntercambio_Presenter(object sender, EventArgs e)
        {
            oIGesCat.DBSetEliminaUnIntercambio(oIView.iIdIntercambio, oIView.iIdContrato);
        }
        protected void eGetConsultaContratos_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaContratos(oIGesCat.DBGetObtieneContratosPorCliente(oIView.iIdCliente.S().I()));
        }
        protected void eSaveDocumentoContrato_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.DBSetInsertaDocumentoAsociadoContrato(oIView.oComprobante))
                oIView.MostrarMensaje("El documento se agregó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al agregar el documento.", "Aviso");
        }
        protected void eGetDocumentosContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDocumentosContratos(oIGesCat.DBGetConsultaDocumentosAsociadosContrato(oIView.iIdContrato));
        }
        protected void eSetEliminaDocumento_Presenter(object sender, EventArgs e)   
        {
            if(oIGesCat.DBSetEliminaDocumentoAsociadoContrato(oIView.iIdDocumento))
                oIView.MostrarMensaje("El documento se eliminó de manera correcta.", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al eliminar el documento.", "Aviso");
        }

        protected void eValidaContratoExistente_Presenter(object sender, EventArgs e)
        {
            oIView.bExisteClave = oIGesCat.DBGetExisteClaveContrato(oIView.sClaveContrato);
        }
    }
}