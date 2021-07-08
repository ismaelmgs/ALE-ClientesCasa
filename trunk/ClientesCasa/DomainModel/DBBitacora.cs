using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ClientesCasa.Objetos;
using NucleoBase.Core;
using ClientesCasa.Clases;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBBitacora : DBBase
    {
        public DataTable DBGetObtieneBitacora (object[] oArray)
        {
            try
            {
                return oDB_SP.EjecutarDT("[SCAF].[spS_CC_ConsultaBitacora]", oArray);
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
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMatriculasParaSCAF]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public string[] DBGetObtieneSICPIC(string sIdSIC, string sIdPIC)
        {   //Carga el grid de las discrepancias
            try
            {
                string[] strSICPIC = new string[2];
                wsSiteLine.ws_SyteLine oBD_SP_WS = new wsSiteLine.ws_SyteLine();
                if (sIdPIC != "")
                {
                    DataSet dsPIC = oBD_SP_WS.ObtieneDatosPilotoPorClave(sIdPIC);


                    if (dsPIC != null && dsPIC.Tables.Count > 0)
                    {
                        DataRow row1 = dsPIC.Tables[0].Rows[0];

                        //Campos Clientes
                        strSICPIC[0] = row1["nombre"].S();


                    }
                    else
                    {
                        strSICPIC[0] = "[No Definido]";

                    }
                }
                else
                {
                    strSICPIC[0] = "[No Definido]";
                }

                if (sIdSIC != "")
                {

                    DataSet dsSIC = oBD_SP_WS.ObtieneDatosPilotoPorClave(sIdSIC);
                    if (dsSIC != null && dsSIC.Tables.Count > 0)
                    {
                        DataRow row1 = dsSIC.Tables[0].Rows[0];

                        //Campos Clientes
                        strSICPIC[1] = row1["nombre"].S();


                    }
                    else
                    {
                        strSICPIC[1] = "[No Definido]";

                    }
                }
                else
                {
                    strSICPIC[1] = "[No Definido]";

                }

                return strSICPIC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneDiscrepancias(string sIdBitacora)
        {   //Carga el grid de las discrepancias
            try
            {
                return oDB_SP.EjecutarDT("[SCAF].[spS_CC_ConsultaDiscrepancias]", "@IdBitacora", sIdBitacora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneComponentes(string sIdDiscrepancia)
        {   //Carga el grid de las discrepancias
            try
            {
                return oDB_SP.EjecutarDT("[SCAF].[spS_CC_ConsultaComponentes]", "@IdDiscrepancia", sIdDiscrepancia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Bitacora oGetObtieneDatosBitacora(string sIdBitacora)
        {
            try
            {
                DataSet ds = oDB_SP.EjecutarDS("[SCAF].[spS_CC_ConsultaBitacoraEdicion]", "@IdBitacora", sIdBitacora);

                Bitacora obt = new Bitacora(); // detalla el objeto para empezar a llenar mis propiedades

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow row1 = ds.Tables[0].Rows[0];

                    //Campos Clientes
                    obt.sIdBitacora = row1["IdBitacora"].S();
                    obt.sFolio = row1["FolioReal"].S();
                    obt.sMatricula = row1["AeronaveMatricula"].S();
                    obt.sSerie = row1["AeronaveSerie"].S();
                    obt.sMotorI = row1["MotorI"].S();
                    obt.sMotorII = row1["MotorII"].S();
                    obt.Planeador = row1["Planeador"].S();
                    obt.APU = row1["APU"].S();
                    obt.sCMotorI = row1["CMotorI"].S();
                    obt.sCMotorII = row1["CMotorII"].S();
                    obt.sAterrizajes = row1["Aterrizajes"].S();
                    obt.sMecanico = row1["Mecanico"].S();
                    obt.bBandera = row1["BooBandera"].S().I() > 0 ? true : false;

                }
                else
                {
                    obt.sIdBitacora = string.Empty;
                    obt.sFolio = string.Empty;
                    obt.sMatricula = string.Empty;
                    obt.sSerie = string.Empty;
                    obt.sMotorI = string.Empty;
                    obt.sMotorII = string.Empty;
                    obt.Planeador = string.Empty;
                    obt.APU = string.Empty;
                    obt.sCMotorI = string.Empty;
                    obt.sCMotorII = string.Empty;
                    obt.sAterrizajes = string.Empty;
                    obt.sMecanico = string.Empty;
                    obt.bBandera = false;
                }

                return obt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneBitacoraPiernas(string sFolioReal, string sMatriculaAeronave)
        {
            try
            {
                return oDB_SP.EjecutarDT("[SCAF].[spS_CC_ConsultaBitacoraPiernas]", "@FolioReal", sFolioReal
                                                                                  , "@AeronaveMatricula", sMatriculaAeronave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Discrepancia oGetObtieneDiscrepancia(string sIdDiscrepancia, string sIdBitacora)
        {//carga lo datos en los controles para modificar la discrepancia
            try
            {
                DataSet ds = oDB_SP.EjecutarDS("[SCAF].[spS_CC_ConsultaDiscrepancia]", "@IdDiscrepancia", sIdDiscrepancia
                                                                                      , "@IdBitacora", sIdBitacora);

                Discrepancia obt = new Discrepancia(); // detalla el objeto para empezar a llenar mis propiedades

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow row1 = ds.Tables[0].Rows[0];

                    //Campos Clientes
                    obt.sIdBitacora = row1["IdBitacora"].S();
                    obt.sOrigen = row1["Origen"].S();
                    obt.sTipoReporte = row1["TipoReporte"].S();
                    obt.sDescripcion = row1["Descripcion"].S();
                    obt.sAccionesCorrectiva = row1["AccionCorrectiva"].S();
                    obt.sCodigoAta = row1["ATA"].S();
                    obt.sBase = row1["Base"].S();
                    obt.sMecanico = row1["Mecanico"].S();
                    obt.dtFechaDiscrepancia = row1["FechaDiscrepancia"].S().Dt();
                    obt.dtFechaAtencion = row1["FechaAtencion"].S().Dt();
                    obt.sReferenciaRep = row1["ReferenciaRep"].S();
                    obt.sDiagnostico = row1["Diagnostico"].S();
                    obt.sId = row1["IdDiscrepancia"].S();
                    obt.sComponente = row1["Componente"].S();

                }
                else
                {
                    obt.sIdBitacora = string.Empty;
                    obt.sOrigen = string.Empty;
                    obt.sTipoReporte = string.Empty;
                    obt.sDescripcion = string.Empty;
                    obt.sAccionesCorrectiva = string.Empty;
                    obt.sCodigoAta = string.Empty;
                    obt.sBase = string.Empty;
                    obt.sMecanico = string.Empty;
                    obt.sReferenciaRep = string.Empty;
                    obt.sDiagnostico = string.Empty;
                    obt.sId = string.Empty;
                    obt.sComponente = string.Empty;
                }

                return obt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Componente oGetObtieneComponente(string sIdComponente, string sIdDiscrepancia)
        {//carga lo datos en los controles para modificar la discrepancia
            try
            {
                DataSet ds = oDB_SP.EjecutarDS("[SCAF].[spS_CC_ConsultaComponente]", "@IdComponente", sIdComponente
                                                                                      , "@IdDiscrepancia", sIdDiscrepancia);

                Componente obt = new Componente(); // detalla el objeto para empezar a llenar mis propiedades

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow row1 = ds.Tables[0].Rows[0];

                    //Campos Clientes
                    obt.sIdComponente = row1["IdComponente"].S();
                    obt.sIdDiscrepancia = row1["IdDiscrepancia"].S();
                    obt.sNombreComponente = row1["NombreComponente"].S();
                    obt.sDescripcion = row1["Descripcion"].S();
                    obt.sNoParteRemovido = row1["NoParteRemovido"].S();
                    obt.sNoSerieRemovido = row1["NoSerieRemovido"].S();
                    obt.sNoParteInstalado = row1["NoParteInstalado"].S();
                    obt.sNoSerieInstalado = row1["NoSerieInstalado"].S();
                    obt.sPocisionComponente = row1["PosicionComponente"].S();
                    obt.sID = row1["IdComponente"].S();
                    obt.sRazonServicio = row1["RazonServicio"].S();

                }
                else
                {
                    obt.sIdComponente = string.Empty;
                    obt.sIdDiscrepancia = string.Empty;
                    obt.sNombreComponente = string.Empty;
                    obt.sDescripcion = string.Empty;
                    obt.sNoParteRemovido = string.Empty;
                    obt.sNoSerieRemovido = string.Empty;
                    obt.sNoParteInstalado = string.Empty;
                    obt.sNoSerieInstalado = string.Empty;
                    obt.sPocisionComponente = string.Empty;
                    obt.sID = string.Empty;
                    obt.sRazonServicio = string.Empty;
                }

                return obt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaAdicionalesBitacora(Bitacora oBitacora)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spI_MXJ_InsertaAdicionalesBitacora]", "@FolioReal", oBitacora.sFolio,
                                                                                                  "@AeronaveMatricula", oBitacora.sMatricula,
                                                                                                  "@AeronaveSerie", oBitacora.sSerie,
                                                                                                  "@PIC", oBitacora.PIC,
                                                                                                  "@SIC", oBitacora.SIC,
                                                                                                  "@MotorI", oBitacora.sMotorI,
                                                                                                  "@MotorII", oBitacora.sMotorII,
                                                                                                  "@Planeador", oBitacora.Planeador,
                                                                                                  "@APU", oBitacora.APU,
                                                                                                  "@CMotorI", oBitacora.sCMotorI,
                                                                                                  "@CMotorII", oBitacora.sCMotorII,
                                                                                                  "@Aterrizajes", oBitacora.sAterrizajes,
                                                                                                  "@Mecanico", oBitacora.sMecanico,
                                                                                                  "@UsuarioCreacion", Utils.GetUser);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetInsertaDiscrepancia(Discrepancia oDiscrepancia)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spI_MXJ_InsertaDiscrepancia]", "@IdBitacota", oDiscrepancia.sIdBitacora,
                                                                                            "@Origen", oDiscrepancia.sOrigen,
                                                                                            "@TipoReporte", oDiscrepancia.sTipoReporte,
                                                                                            "@Descripcion", oDiscrepancia.sDescripcion,
                                                                                            "@AccionCorrectiva", oDiscrepancia.sAccionesCorrectiva,
                                                                                            "@ATA", oDiscrepancia.sCodigoAta,
                                                                                            "@Base", oDiscrepancia.sBase,
                                                                                            "@Mecanico", oDiscrepancia.sMecanico,
                                                                                            "@FechaDiscrepancia", oDiscrepancia.dtFechaDiscrepancia,
                                                                                            "@FechaAtencion", oDiscrepancia.dtFechaAtencion,
                                                                                            "@RefereciaRep", oDiscrepancia.sReferenciaRep,
                                                                                            "@Diagnostico", oDiscrepancia.sDiagnostico,
                                                                                            "@Id", oDiscrepancia.sId,
                                                                                            "@Componente", oDiscrepancia.sComponente,
                                                                                            "@UsuarioCreacion", Utils.GetUser);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetInsertaComponente(Componente oComponente)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spI_MXJ_InsertaComponente]", "@IdDiscrepancia", oComponente.sIdDiscrepancia,
                                                                                         "@NombreComponente", oComponente.sNombreComponente,
                                                                                         "@Descripcion", oComponente.sDescripcion,
                                                                                         "@NoParteRemovido", oComponente.sNoParteRemovido,
                                                                                         "@NoSerieRemovido", oComponente.sNoSerieRemovido,
                                                                                         "@NoParteInstalado", oComponente.sNoParteInstalado,
                                                                                         "@NoSerieInstalado", oComponente.sNoSerieInstalado,
                                                                                         "@PosicionComponente", oComponente.sPocisionComponente,
                                                                                         "@Id", oComponente.sID,
                                                                                         "@RazonServicio", oComponente.sRazonServicio,
                                                                                         "@UsuarioCreacion", Utils.GetUser);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetActualizaAdicionalesBitacora(Bitacora oBitacora)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spU_MXJ_ActualizaAdicionalesBitacora]", "@FolioReal", oBitacora.sFolio,
                                                                                                    "@AeronaveMatricula", oBitacora.sMatricula,
                                                                                                    "@MotorI", oBitacora.sMotorI,
                                                                                                    "@MotorII", oBitacora.sMotorII,
                                                                                                    "@Planeador", oBitacora.Planeador,
                                                                                                    "@APU", oBitacora.APU,
                                                                                                    "@CMotorI", oBitacora.sCMotorI,
                                                                                                    "@CMotorII", oBitacora.sCMotorII,
                                                                                                    "@Aterrizajes", oBitacora.sAterrizajes,
                                                                                                    "@Mecanico", oBitacora.sMecanico,
                                                                                                    "@Usuario", Utils.GetUser);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetActualizaDiscrepancia(Discrepancia oDiscrepancia)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spU_MXJ_ActualizaDiscrepancia]", "@IdDiscrepancia", oDiscrepancia.sIdDiscrepancia,
                                                                                            "@IdBitacota", oDiscrepancia.sIdBitacora,
                                                                                            "@Origen", oDiscrepancia.sOrigen,
                                                                                            "@TipoReporte", oDiscrepancia.sTipoReporte,
                                                                                            "@Descripcion", oDiscrepancia.sDescripcion,
                                                                                            "@AccionCorrectiva", oDiscrepancia.sAccionesCorrectiva,
                                                                                            "@ATA", oDiscrepancia.sCodigoAta,
                                                                                            "@Base", oDiscrepancia.sBase,
                                                                                            "@Mecanico", oDiscrepancia.sMecanico,
                                                                                            "@FechaDiscrepancia", oDiscrepancia.dtFechaDiscrepancia,
                                                                                            "@FechaAtencion", oDiscrepancia.dtFechaAtencion,
                                                                                            "@RefereciaRep", oDiscrepancia.sReferenciaRep,
                                                                                            "@Diagnostico", oDiscrepancia.sDiagnostico,
                                                                                            "@Id", oDiscrepancia.sId,
                                                                                            "@Componente", oDiscrepancia.sComponente,
                                                                                            "@UsuarioCreacion", Utils.GetUser);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetActualizaComponente(Componente oComponente)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spU_MXJ_ActualizaComponente]", "@IdComponente", oComponente.sIdComponente,
                                                                                            "@IdDiscrepancia", oComponente.sIdDiscrepancia,
                                                                                            "@NombreComponente", oComponente.sNombreComponente,
                                                                                            "@Descripcion", oComponente.sDescripcion,
                                                                                            "@NoParteRemovido", oComponente.sNoParteRemovido,
                                                                                            "@NoSerieRemovido", oComponente.sNoSerieRemovido,
                                                                                            "@NoParteInstalado", oComponente.sNoParteInstalado,
                                                                                            "@NoSerieInstalado", oComponente.sNoSerieInstalado,
                                                                                            "@PosicionComponente", oComponente.sPocisionComponente,
                                                                                            "@Id", oComponente.sID,
                                                                                            "@RazonServicio", oComponente.sRazonServicio,
                                                                                            "@UsuarioCreacion", Utils.GetUser);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetEliminaDiscrepancia(Discrepancia oDiscrepancia)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spD_MXJ_EliminaDiscrepancia]", "@IdDiscrepancia", oDiscrepancia.sIdDiscrepancia,
                                                                          "@IdBitacota", oDiscrepancia.sIdBitacora,
                                                                          "@UsuarioCreacion", oDiscrepancia.sUsuario);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetEliminaDiscrepancia(Componente oComponente)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[SCAF].[spD_MXJ_EliminaComponente]", "@IdComponente", oComponente.sIdComponente,
                                                                                          "@IdDiscrepancia", oComponente.sIdDiscrepancia,
                                                                                          "@UsuarioCreacion", oComponente.sUsuario);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}