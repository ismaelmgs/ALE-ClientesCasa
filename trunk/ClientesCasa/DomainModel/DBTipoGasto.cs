using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBTipoGasto : DBBase
    {

        public DataTable DBGetObtieneTipoGastoExistentes
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaTipoGasto]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DBGetExisteTipoGenBD(string sDescConcepto, ref string sMensaje, int iIdConcepto)
        {
            try
            {
                bool ban = false;
                DataTable dtRes = new DataTable();
                dtRes = oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ValidaExisteTipoGasto]", "@DescConcepto", sDescConcepto,
                                                                                        "@IdConcepto", iIdConcepto);
                if (dtRes.Rows.Count > 0)
                {
                    sMensaje = "Ya existe un tipo de gasto con el mismo nombre registrado, favor de verificar";
                    ban = true;
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaTipoG(object[] oArrTipoG)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaTipoGasto]", oArrTipoG);

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaTipoGasto(object[] oArrProv)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaTipoGasto]", oArrProv);
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetEliminaTipoGasto(int iIdConcepto)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spD_CC_EliminaTipoGasto]", "@IdConcepto", iIdConcepto,
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