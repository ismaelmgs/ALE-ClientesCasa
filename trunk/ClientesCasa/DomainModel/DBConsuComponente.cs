using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ClientesCasa.Objetos;
using NucleoBase.Core;
using ClientesCasa.Clases;

namespace ClientesCasa.DomainModel
{
    public class DBConsuComponente : DBBase
    {
        public DataTable DBGetObtieneComponentes(object[] oArray)
        {
            try
            {
                return oDB_SP.EjecutarDT("[SCAF].[spS_CC_ConsultaComponentesII]", oArray);
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
           
    }
}