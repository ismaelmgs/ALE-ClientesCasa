using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBImagenesMatricula : DBBase
    {
        public DataTable DBGetObtieneClientesContratos(object[] oArray)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaClientesContratosAeronaveMatriculas]", oArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaDocumentoAsociadoContrato(object[] oArrayImgMat)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaMatImagen]", oArrayImgMat);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaImagenesMatricula(int iIdAeronave)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaImagenesAeronaves]", "@IdAeronave", iIdAeronave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetEliminaImagenMatricula(int iIdImagen)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaImagenMatricula]", "@IdImagen", iIdImagen,
                                                                                            "@UsuarioModificacion", Utils.GetUser);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}