using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Data;
using ClientesCasa.Clases;

namespace ClientesCasa.DomainModel
{
    public class DBSolicitudAviones : DBBase
    {
        public DataTable DBGetObtieneFlotasMJ()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaFlota]", "@IdFlota", 0,
                                                                                "@Descripcion", "",
                                                                                "@estatus", 1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBGetObtieneMatriculasPorFlota(int iIdFlota)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ALEExchange].[spS_AE_ConsultaAeronavesPorFlota]", "@IdFlota", iIdFlota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudAvion DBGetObtieneSolicitudPorId(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ALEExchange].[spS_AE_ObtieneSolicitudAvionPorId]", "@IdSolicitud", iIdSolicitud).AsEnumerable().Select(r => new SolicitudAvion
                {
                    iIdFlota = r["IdFlota"].S().I(),
                    iIdMatricula = r["IdMatricula"].S().I(),
                    sPersona = r["Persona"].S(),
                    sFechaInicio = r["FechaInicio"].S(),
                    sFechaFin = r["FechaFin"].S(),
                    iAcepta = r["Acepta"].S().I(),
                    sComentarios = r["Comentarios"].S()
                }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaSolicitudAvion(SolicitudAvion oSol)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ALEExchange].[spI_AE_InsertaSolicitudAvion]", "@IdMatricula", oSol.iIdMatricula,
                                                                                                    "@Persona", oSol.sPersona,
                                                                                                    "@FechaInicio", oSol.sFechaInicio,
                                                                                                    "@FechaFin", oSol.sFechaFin,
                                                                                                    "@Acepta", oSol.iAcepta,
                                                                                                    "@Comentarios", oSol.sComentarios,
                                                                                                    "@UsuarioCreacion", Utils.GetUser);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneUltimasSolicitudes()
        {
            try
            {
                return oDB_SP.EjecutarDT("[ALEExchange].[spS_AE_ObtieneUltimaSolicitudes]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetActualizaSolicitudVuelo(SolicitudAvion oSol, int iIdSolicitud)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ALEExchange].[spU_AE_ActualizaSolicitudAvion]", "@IdSolicitud", iIdSolicitud,
                                                                                                    "@IdMatricula", oSol.iIdMatricula,
                                                                                                    "@Persona", oSol.sPersona,
                                                                                                    "@FechaInicio", oSol.sFechaInicio,
                                                                                                    "@FechaFin", oSol.sFechaFin,
                                                                                                    "@Acepta", oSol.iAcepta,
                                                                                                    "@Comentarios", oSol.sComentarios,
                                                                                                    "@UsuarioModificacion", Utils.GetUser);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetCancelaSolicitud(int iIdSolicitud)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ALEExchange].[spD_AE_CancelaSolicitudAvion]", "@IdSolicitud", iIdSolicitud);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}