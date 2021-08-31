using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ClientesCasa.Clases;

namespace ClientesCasa.DomainModel
{
    public class DBContratos : DBBase
    {
        public DataTable DBGetObtieneClientesContratos(object[] oArray)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaClientesContratosAeronave]", oArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ClienteContrato oGetObtieneDatosCliente(string sIdCliente)
        {
            try
            {
                DataSet ds = oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaClienteYAdicionales]", "@IdCliente", sIdCliente);

                ClienteContrato ocl = new ClienteContrato();

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow row1 = ds.Tables[0].Rows[0];

                    //Campos Clientes
                    ocl.sNumCliente = row1["CodigoCliente"].S();
                    ocl.sNombreCliente = row1["ClienteNombre"].S();
                    ocl.iActivo = row1["Status"].S().I();

                    if (ds.Tables[1].Rows.Count == 0)
                    {
                        ocl.sRazonSocial = string.Empty;
                        ocl.bRFC = false;
                        ocl.sTipoContribuyente = string.Empty;
                        ocl.sRFC = string.Empty;
                        ocl.sTelefono = string.Empty;
                        ocl.sFax = string.Empty;
                        ocl.iIdSector = -1;
                        ocl.sCorreoEletronico = string.Empty;
                        ocl.iIdPais = -1;
                        ocl.iIdEstado = -1;
                        ocl.sDireccion = string.Empty;
                        ocl.sCiudad = string.Empty;
                        ocl.sCP = string.Empty;
                        ocl.iIdPaisDE = -1;
                        ocl.iIdEstadoDE = -1;
                        ocl.sDireccionDE = string.Empty;
                        ocl.sCiudadDE = string.Empty;
                        ocl.sCPDE = string.Empty;
                    }
                    else
                    {
                        DataRow r = ds.Tables[1].Rows[0];
                        ocl.sRazonSocial = r["RazonSocial"].S();
                        ocl.bRFC = r["RFC"].S() != "" ? true : false;
                        ocl.sTipoContribuyente = r["TipoContribuyente"].S();
                        ocl.sRFC = r["RFC"].S();
                        ocl.sTelefono = r["Telefono"].S();
                        ocl.sFax = r["Fax"].S();
                        ocl.iIdSector = r["IdSector"].S().I();
                        ocl.sCorreoEletronico = r["CorreoElectronico"].S();
                        ocl.iIdPais = r["IdPais"].S().I();
                        ocl.iIdEstado = r["IdEstado"].S().I();
                        ocl.sDireccion = r["Direccion"].S();
                        ocl.sCiudad = r["Ciudad"].S();
                        ocl.sCP = r["CP"].S();
                        ocl.iIdPaisDE = r["IdPaisDE"].S().I();
                        ocl.iIdEstadoDE = r["IdEstadoDE"].S().I();
                        ocl.sDireccionDE = r["DireccionDE"].S();
                        ocl.sCiudadDE = r["CiudadDE"].S();
                        ocl.sCPDE = r["CPDE"].S();
                    }

                    ocl.dtContratos = ds.Tables[2].Copy();
                }

                return ocl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtienePaises
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaPaises]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBGetObtieneEstadosPorPais(int iIdPais)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaEstadosPorPais]", "@PaisId", iIdPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ClienteContrato DBGetObtieneDetalleContrato(int iIdContrato)
        {
            try
            {

                DataSet dsRes = oDB_SP.EjecutarDS("[ClientesCasa].[spS_CC_ConsultaDetalleContrato]", "@IdContrato", iIdContrato);
                return dsRes.Tables[0].AsEnumerable().Select(r => new ClienteContrato()
                {
                    sIdContrato = r["IdContrato"].S(),
                    sClaveContrato = r["ClaveContrato"].S(),
                    sAeronaveSerie = r["Serie"].S(),
                    sAeronaveMatricula = r["Matricula"].S(),
                    iPorcentajePart = r["PorcParticipacion"].S().D(),
                    iHorasContratadas = r["HorasContratadasTotal"].S().I(),
                    bAplicaIntercambios = r["AplicaIntercambio"].S() == "1" ? true : false,
                    iFactorIntercambio = r["FactorIntercambio"].S().I(),
                    dtFechaContrato = r["FechaContrato"].S().Dt(),
                    iEstatusContrato = r["Status"].S().I(),
                    iTipoCosto = r["TiempoFacturar"].S().I(),
                    dtFechaFinContrato = r["FechaFinContrato"].S().Dt(),
                    dAnticipoContrato = r["AnticipoContrato"].S().D(),
                    iTipoServicioConsultoria = r["TipoServicioConsultoria"].S().I(),
                    iTipoTarifa = r["TipoTarifa"].S().I(),
                    iDetalleTipoTarifa = r["DetalleTipoTarifa"].S().I(),
                    sNoPoliza = r["NoPolizaSeguro"].S(),
                    sEmpresaAseguradora = r["EmpresaAseguradora"].S(),
                    dtFechaInicioSeg = r["FechaInicioSeg"].S().Dt(),
                    dtFechaFinSeg = r["FechaFinSeg"].S().Dt(),
                    vbPolizaSeguro = r["PolizaSeguri"].S() != string.Empty ? (byte[])r["PolizaSeguri"] : new byte[1],
                    sMonedaAnticipo = r["MonedaAnticipo"].S(),
                    dtDocumentos = dsRes.Tables[1]
                }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetIntercambiosContratos(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaIntercambioContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneGruposModelo()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaGrupoModelo]", "@GrupoModeloId", 0,
                                                                                        "@Descripcion", "%%",
                                                                                        "@ConsumoGalones", 0,
                                                                                        "@Tarifa", 0,
                                                                                        "@estatus", -1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetInsertaCliente(ClienteContrato oCliente)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaCliente]", "@CodigoCliente", oCliente.sNumCliente,
                                                                                            "@Nombre", oCliente.sNombreCliente,
                                                                                            "@RazonSocial", oCliente.sRazonSocial,
                                                                                            "@RFC", oCliente.sRFC,
                                                                                            "@TipoContribuyente", oCliente.sTipoContribuyente,
                                                                                            "@Telefono", oCliente.sTelefono,
                                                                                            "@Fax", oCliente.sFax,
                                                                                            "@CorreoElectronico", oCliente.sCorreoEletronico,
                                                                                            "@IdSector", oCliente.iIdSector,
                                                                                            "@IdPais", oCliente.iIdPais,
                                                                                            "@IdEstado", oCliente.iIdEstado,
                                                                                            "@Direccion", oCliente.sDireccion,
                                                                                            "@Ciudad", oCliente.sCiudad,
                                                                                            "@CP", oCliente.sCP,
                                                                                            "@IdPaisDE", oCliente.iIdPaisDE,
                                                                                            "@IdEstadoDE", oCliente.iIdEstadoDE,
                                                                                            "@DireccionDE", oCliente.sDireccionDE,
                                                                                            "@CiudadDE", oCliente.sCiudadDE,
                                                                                            "@CPDE", oCliente.sCPDE,
                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetActualizaCliente(ClienteContrato oCliente)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaCliente]", "@IdCliente", oCliente.iIdCliente,
                                                                                                "@CodigoCliente", oCliente.sNumCliente,
                                                                                                "@Nombre", oCliente.sNombreCliente,
                                                                                                "@RazonSocial", oCliente.sRazonSocial,
                                                                                                "@RFC", oCliente.sRFC,
                                                                                                "@TipoContribuyente", oCliente.sTipoContribuyente,
                                                                                                "@Telefono", oCliente.sTelefono,
                                                                                                "@Fax", oCliente.sFax,
                                                                                                "@CorreoElectronico", oCliente.sCorreoEletronico,
                                                                                                "@IdSector", oCliente.iIdSector,
                                                                                                "@IdPais", oCliente.iIdPais,
                                                                                                "@IdEstado", oCliente.iIdEstado,
                                                                                                "@Direccion", oCliente.sDireccion,
                                                                                                "@Ciudad", oCliente.sCiudad,
                                                                                                "@CP", oCliente.sCP,
                                                                                                "@IdPaisDE", oCliente.iIdPaisDE,
                                                                                                "@IdEstadoDE", oCliente.iIdEstadoDE,
                                                                                                "@DireccionDE", oCliente.sDireccionDE,
                                                                                                "@CiudadDE", oCliente.sCiudadDE,
                                                                                                "@CPDE", oCliente.sCPDE,
                                                                                                "@UsuarioModificacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetInsertaContrato(ClienteContrato oContrato)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaContratoCC]", "@IdCliente", oContrato.iIdCliente,
                                                                                                "@ClaveContrato", oContrato.sClaveContrato,
                                                                                                "@FechaContrato", oContrato.dtFechaContrato,
                                                                                                "@HorasContratadasTotal", oContrato.iHorasContratadas,
                                                                                                "@Matricula", oContrato.sAeronaveMatricula,
                                                                                                "@PorcParticipacion", oContrato.iPorcentajePart,
                                                                                                "@AplicaIntercambio", oContrato.bAplicaIntercambios,
                                                                                                "@FactorIntercambio", oContrato.iFactorIntercambio,
                                                                                                "@TiempoFacturar", oContrato.iTipoCosto,
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress);
                int iRes = 0;
                if (oRes.S().I() > 0)
                {
                    oContrato.iIdContrato = oRes.S().I();
                    iRes = DBSetInsertaAdicionalesContratoCC(oContrato);
                }

                return iRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetActualizaContrato(ClienteContrato oContrato)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaContrato]", "@IdContrato", oContrato.sIdContrato,
                                                                                                "@ClaveContrato", oContrato.sClaveContrato,
                                                                                                "@FechaContrato", oContrato.dtFechaContrato,
                                                                                                "@HorasContratadasTotal", oContrato.iHorasContratadas,
                                                                                                "@Matricula", oContrato.sAeronaveMatricula,
                                                                                                "@PorcParticipacion", oContrato.iPorcentajePart,
                                                                                                "@AplicaIntercambio", oContrato.bAplicaIntercambios,
                                                                                                "@FactorIntercambio", oContrato.iFactorIntercambio,
                                                                                                "@TiempoFacturar", oContrato.iTipoCosto,
                                                                                                "@UsuarioModificacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress);

                int iRes = DBSetActualizaAdicionalesContratoCC(oContrato);

                return iRes > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaIntercambioContrato(ClienteContrato oIntercambio)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaIntercambioContratoCC]", "@IdContrato", oIntercambio.sIdContrato.S().I(),
                                                                                                            "@IdGrupoModelo", oIntercambio.iIntercambioGrupoModeloId,
                                                                                                            "@AplicaFerry", oIntercambio.bContratoIntercambiosAplicaFerry,
                                                                                                            "@Factor", oIntercambio.dContratoIntercambiosFactor,
                                                                                                            "@TarifaNal", oIntercambio.dContratoIntercambiosTarifaNacional,
                                                                                                            "@TarifaInt", oIntercambio.dContratoIntercambiosTarifaInternacional,
                                                                                                            "@Galones", oIntercambio.iContratoIntercambiosGalones,
                                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                                            "@IP", Utils.GetIPAddress,
                                                                                                            "@Espera", oIntercambio.dContratoIntercambiosEspera,
                                                                                                            "@Pernocta", oIntercambio.dContratoIntercambiosPernocta,
                                                                                                            "@Ferry", oIntercambio.dContratoIntercambiosFerry,
                                                                                                            "@Valor", oIntercambio.dContratoIntercambiosValor);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetActualizaIntercambioContrato(ClienteContrato oIntercambio)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_MXJ_ActualizaIntercambioContratoCC]", "@IdIntercambio", oIntercambio.iIntercambioId,
                                                                                                            "@IdContrato", oIntercambio.sIdContrato.S().I(),
                                                                                                            "@IdGrupoModelo", oIntercambio.iIntercambioGrupoModeloId,
                                                                                                            "@AplicaFerry", oIntercambio.bContratoIntercambiosAplicaFerry,
                                                                                                            "@Factor", oIntercambio.dContratoIntercambiosFactor,
                                                                                                            "@TarifaNal", oIntercambio.dContratoIntercambiosTarifaNacional,
                                                                                                            "@TarifaInt", oIntercambio.dContratoIntercambiosTarifaInternacional,
                                                                                                            "@Galones", oIntercambio.iContratoIntercambiosGalones,
                                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                                            "@IP", Utils.GetIPAddress,
                                                                                                            "@Espera", oIntercambio.dContratoIntercambiosEspera,
                                                                                                            "@Pernocta", oIntercambio.dContratoIntercambiosPernocta,
                                                                                                            "@Ferry", oIntercambio.dContratoIntercambiosFerry,
                                                                                                            "@Valor", oIntercambio.dContratoIntercambiosValor);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneMatriculas
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaMatriculaAeronave]", "@Serie", string.Empty,
                                                                                                "@Matricula", string.Empty,
                                                                                                "@Estatus", 1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBGetObtieneEstatusContratos
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoEstatusContrato]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBGetObtieneSectoresCliente
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[ALE_SP_SEL_ConsultaSectores]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void DBSetEliminaUnIntercambio(int iIdIntercambio, int iIdContrato)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spD_MXJ_EliminaIntercambioContrato]", "@IdIntercambio", iIdIntercambio,
                                                                                        "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetCancelaContratoCC(int iIdContrato)
        {
            try
            {
                oDB_SP.EjecutarSP("[ClientesCasa].[spD_CC_EliminaContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneContratosPorCliente(int iIdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaContratosPorCliente]", "@IdCliente", iIdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetInsertaAdicionalesContratoCC(ClienteContrato oContrato)
        {
            try
            {
                DateTime? dtFechaFinContrato = oContrato.dtFechaFinContrato.S() == "01/01/0001 12:00:00 a. m." ? null : ((DateTime?)oContrato.dtFechaFinContrato.S().Dt());
                DateTime? dtFechaInicioSeg = oContrato.dtFechaInicioSeg.S() == "01/01/0001 12:00:00 a. m." ? null : ((DateTime?)oContrato.dtFechaInicioSeg.S().Dt());
                DateTime? dtFechaFinSeg = oContrato.dtFechaFinSeg.S() == "01/01/0001 12:00:00 a. m." ? null : ((DateTime?)oContrato.dtFechaFinSeg.S().Dt());

                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaAdicionalesContratoCC]", "@IdContrato", oContrato.iIdContrato,
                                                                                                            "@FechaFinContrato", dtFechaFinContrato,
                                                                                                            "@AnticipoContrato", oContrato.dAnticipoContrato.S().D(),
                                                                                                            "@TipoServicioConsultoria", oContrato.iTipoServicioConsultoria,
                                                                                                            "@TipoTarifa", oContrato.iTipoTarifa,
                                                                                                            "@DetalleTipoTarifa", oContrato.iDetalleTipoTarifa,
                                                                                                            "@NoPolizaSeguro", oContrato.sNoPoliza,
                                                                                                            "@EmpresaAseguradora", oContrato.sEmpresaAseguradora,
                                                                                                            "@FechaInicioSeg", dtFechaInicioSeg,
                                                                                                            "@FechaFinSeg", dtFechaFinSeg,
                                                                                                            "@PolizaSeguro", oContrato.vbPolizaSeguro,
                                                                                                            "@MonedaAnticipo", oContrato.sMonedaAnticipo);
                
                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetActualizaAdicionalesContratoCC(ClienteContrato oContrato)
        {
            try
            {
                return oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaContratosCC]", "@IdContrato", oContrato.iIdContrato,
                                                                                            "@FechaFinContrato", oContrato.dtFechaFinContrato,
                                                                                            "@AnticipoContrato", oContrato.dAnticipoContrato.S().D(),
                                                                                            "@TipoServicioConsultoria", oContrato.iTipoServicioConsultoria,
                                                                                            "@TipoTarifa", oContrato.iTipoTarifa,
                                                                                            "@DetalleTipoTarifa", oContrato.iDetalleTipoTarifa,
                                                                                            "@NoPolizaSeguro", oContrato.sNoPoliza,
                                                                                            "@EmpresaAseguradora", oContrato.sEmpresaAseguradora,
                                                                                            "@FechaInicioSeg", oContrato.dtFechaInicioSeg,
                                                                                            "@FechaFinSeg", oContrato.dtFechaFinSeg,
                                                                                            "@PolizaSeguro", oContrato.vbPolizaSeguro,
                                                                                            "@MonedaAnticipo", oContrato.sMonedaAnticipo).S().I();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool DBSetInsertaDocumentoAsociadoContrato(Comprobante oC)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaDocumentosContrato]", "@IdContrato", oC.iIdGasto,
                                                                                                        "@DescArchivo", oC.sNumeroReporte,
                                                                                                        "@NombreArchivo", oC.sNombreArchivo,
                                                                                                        "@Extension", oC.sExtension,
                                                                                                        "@Archivo", oC.bArchivo,
                                                                                                        "@UsuarioCreacion", Utils.GetUser);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetConsultaDocumentosAsociadosContrato(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaDocumentosPorContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetEliminaDocumentoAsociadoContrato(int iIdDocumento)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaDocumentoAsociadoContrato]", "@IdDocumento", iIdDocumento);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBGetExisteClaveContrato(string sClaveContrato)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spS_CC_ConsultaSiContratoExiste]", "@ClaveContrato", sClaveContrato);

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBGetValidaExisteContrato(string sClaveContrato)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spS_CC_ValidaExisteContrato]", "@ClaveContrato", sClaveContrato);

                return oRes.S().I() > 0  ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}