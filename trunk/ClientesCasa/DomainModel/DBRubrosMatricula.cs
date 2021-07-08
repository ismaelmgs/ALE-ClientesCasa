using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBRubrosMatricula : DBBase
    {
        public DataTable DBGetObtieneMatriculasPorFiltros(object[] oArr)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ClientesCasa].[spS_CC_ConsultaMatriculasDifFiltros]", oArr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtieneRubrosPorMatricula(int iIdAeronave)
        {
            try
            {
                return oDB_SP.EjecutarDS("[ClientesCasa].[sps_CC_ConsultaRubrosClientesMatriculas]", "@IdAeronave", iIdAeronave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetEliminaRubrosDeMatricula(int iIdAeronave)
        {
            try
            {
                oDB_SP.EjecutarSP("[ClientesCasa].[spD_CC_EliminaRubrosAsociadosMatricula]", "@IdAeronave", iIdAeronave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetInsertaRubrosAMatricula(List<RubrosMatricula> oLst)
        {
            try
            {
                bool ban = true;
                DBSetEliminaRubrosDeMatricula(oLst[0].iIdAeronave);
                foreach (RubrosMatricula oRM in oLst)
                {
                    object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaRubroAsociadoMatricula]", "@IdAeronave", oRM.iIdAeronave,
                                                                                                                "@IdRubro", oRM.iIdRubro,
                                                                                                                "@Participacion", oRM.iParticipacion);
                }

                return ban;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}