using ClientesCasa.Clases;
using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBComprobantes : DBBase
    {
        public DataTable DBGetObtieneClientesMatriculasFiltros(object[] oArr)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaClientesContratosOpciones]", oArr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DBGetObtieneGastosPataComprobantes(string sMatricula, int iMes, int iAnio)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ObtieneGastosParaComprobantes]", "@Matricula", sMatricula,
                                                                                                    "@Mes", iMes,
                                                                                                    "@Anio", iAnio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneDocumentosPorGasto(int iIdGasto)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaImagenesAsociadasGasto]", "@IdGasto", iIdGasto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneImagenPorId(int iIdImagen)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaImagenPorId]", "@IdImagen", iIdImagen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaComprobante(Comprobante oC)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaComprobante]", "@NumReporte", oC.sNumeroReporte,
                                                                                                "@IdGasto", oC.iIdGasto,
                                                                                                "@Archivo", oC.bArchivo,
                                                                                                "@NombreArchivo", oC.sNombreArchivo,
                                                                                                "@Extension", oC.sExtension,
                                                                                                "@UsuarioCreacion", Utils.GetUser);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetEliminaComprobante(int iIdImagen)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaArchivoComprobantePorId]", "@IdImagen", iIdImagen);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}