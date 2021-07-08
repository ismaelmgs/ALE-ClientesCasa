using ClientesCasa.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBRubros : DBBase
    {
        public bool DBSetInsertaRubro(object[] oArrRubro)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaRubro]", oArrRubro);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaRubro(object[] oArrRubro)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaRubro]", oArrRubro);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneRubrosExistentes
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaRubros]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool DBSetEliminaRubro(int iIdRubro)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaRubro]", "@IdRubro", iIdRubro,
                                                                                            "@UsuarioModificacion", Utils.GetUser);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaCuentaAsociadaRubro(int iIdRubro, string sDesc, string sNombreCuenta)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaCuentaRubro]", "@IdRubro", iIdRubro,
                                                                                                "@DescripcionCuenta", sDesc,
                                                                                                "@NombreCuenta", sNombreCuenta,
                                                                                                "@UsuarioCreacion", Utils.GetUser);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneCuentasPorRubro(int iIdRubro)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaCuentasPorRubro]", "@IdRubro", iIdRubro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetEliminaCuentaRubro(int iIdCuenta)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaCuentaDeRubro]", "@IdCuenta", iIdCuenta);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBGetExisteCuentaEnOtroRubro(string sDescCuenta, ref string sMensaje)
        {
            try
            {
                bool ban = false;
                DataTable dtRes = new DataTable();
                dtRes = oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ValidaExisteCuentaOtroRubro]", "@DescCuenta", sDescCuenta);
                if (dtRes.Rows.Count > 0)
                {
                    sMensaje = "La cuenta se encuentra registrada en el rubro: " + dtRes.Rows[0]["DescripcionRubro"].S();
                    ban = true;
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBGetExisteRubroenBD(string sDescRubro, ref string sMensaje, int iIdRubro)
        {
            try
            {
                bool ban = false;
                DataTable dtRes = new DataTable();
                dtRes = oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ValidaExisteRubro]", "@DescRubro", sDescRubro,
                                                                                        "@IdRubro", iIdRubro);
                if (dtRes.Rows.Count > 0)
                {
                    sMensaje = "Ya existe un rubro con el mismo nombre registrado, favor de verificar";
                    ban = true;
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetRubrosPorArea(int iArea)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaRubrosPorArea]", "@AreaRubro", iArea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}