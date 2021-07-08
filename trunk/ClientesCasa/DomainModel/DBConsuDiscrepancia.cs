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
    public class DBConsuDiscrepancia : DBBase
    {
        public DataTable DBGetObtieneDiscrepancias(object[] oArray)
        {
            try
            {
                return oDB_SP.EjecutarDT("[SCAF].[spS_CC_ConsultaDiscrepanciasII]", oArray);
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
           
    }
}