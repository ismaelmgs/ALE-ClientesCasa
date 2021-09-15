using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBProveedores : DBBase
    {

        public DataTable DBGetObtieneProveedoresExistentes
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaProveedores]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DBGetExisteProveedorenBD(string sDescProv, ref string sMensaje, int iIdProv)
        {
            try
            {
                bool ban = false;
                DataTable dtRes = new DataTable();
                dtRes = oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ValidaExisteProveedor]", "@DescProv", sDescProv,
                                                                                        "@IdProv", iIdProv);
                if (dtRes.Rows.Count > 0)
                {
                    sMensaje = "Ya existe un proveedor con el mismo nombre registrado, favor de verificar";
                    ban = true;
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaProv(object[] oArrProv)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaProveedor]", oArrProv);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaProv(object[] oArrProv)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaProveedor]", oArrProv);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetEliminaProveedor(int iIdProv)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaProveedor]", "@IdProv", iIdProv,
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