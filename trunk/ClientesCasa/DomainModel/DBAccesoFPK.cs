using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using System.Data.SqlClient;

namespace ClientesCasa.DomainModel
{
    public class DBAccesoFPK : DBFlightPak
    {

        public DataSet DBGetObtieneAdiestramiento()
        {
            try
            {
                string sNombre = "FPK";//Utils.ObtieneParametroPorClave("101");
                DataSet dsResultado = new DataSet();
                //SqlDataAdapter da = new SqlDataAdapter();

                //using (SqlConnection con = new SqlConnection(Globales.GetConfigConnection("SqlDefaultFPK")))
                //{
                //    using (SqlCommand cmd = new SqlCommand("dbo.[spS_CC_ConsultaAdiestramientos]", con))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.CommandTimeout = 10;
                //        con.Open();
                //        cmd.ExecuteNonQuery();
                //        da.SelectCommand = cmd;
                //        da.Fill(dsResultado);
                //    }
                //}
                
                return oDB_SP.EjecutarDS_DeQuery("dbo.[spS_CC_ConsultaAdiestramientos]");

                //object[] oArrFil = { "@iTrip", iTrip, };
                //return oDB_SP.EjecutarDS_DeQuery(sQry);
                //return dsResultado;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtienePendientesPilotos()
        {
            try
            {
                //string sNombre = "FPK";//Utils.ObtieneParametroPorClave("101");
                DataSet dsResultado = new DataSet();
                return oDB_SP.EjecutarDS_DeQuery("[dbo].[spS_CC_ConsultaPilotosPendientes]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetMttoAeronaves()
        {
            try
            {
                string sProxMtto = "select *, CASE WHEN TBL.FECHA_INICIO > CONVERT(VARCHAR(10),GETDATE(), 103) THEN 'Próximo Mtto' END AS status " +
                               "from openquery(FPK_Live_1, " +
                                    "'SELECT " +
                                        "TL.orig_nmbr, " +
                                        "TM.tail_nmbr MATRICULA, " +
                                        "TM.desc DESCRIPCION_GRAL, " +
                                        "TM.rec_type, " +
                                        "TL.depicao_id LUGAR_ICAO, " +
                                        "TL.homdep FECHA_INICIO, " +
                                        "TL.homarr FECHA_FIN, " +
                                        "CD.desc TIPO_MTTO " +
                                    "FROM TRIPLEG TL " +
                                    "INNER JOIN TRIPMAIN TM ON (TL.orig_nmbr = TM.orig_nmbr) " +
                                    "INNER JOIN [acftduty] CD ON (TL.duty_type = CD.code) " +
                                    "WHERE TM.rec_type=''M'' ') TBL " +
                "WHERE 1 = 1 AND TBL.FECHA_INICIO > CONVERT(VARCHAR(10),GETDATE(), 103)";
                DataTable dt1 = oDB_SP.EjecutarDT_DeQuery(sProxMtto);

                string sStatusMtto = "select *, CASE " +
                               "WHEN CONVERT(VARCHAR(10),GETDATE(), 103) = CONVERT(VARCHAR(10),TBL.FECHA_INICIO, 103) THEN 'Hoy Inicia Mtto' " +
                               "WHEN CONVERT(VARCHAR(10),GETDATE(), 103) = CONVERT(VARCHAR(10),TBL.FECHA_FIN, 103) THEN 'Hoy Finaliza Mtto' " +
                               "WHEN CONVERT(VARCHAR(10),GETDATE(), 103) < CONVERT(VARCHAR(10),TBL.FECHA_FIN, 103) THEN 'En Mantenimiento' " +
                               "END AS estatus " +
                               "from openquery(FPK_Live_1, " +
                                    "'SELECT " +
                                        "TL.orig_nmbr, " +
                                        "TM.tail_nmbr MATRICULA, " +
                                        "TM.desc DESCRIPCION_GRAL, " +
                                        "TM.rec_type, " +
                                        "TL.depicao_id LUGAR_ICAO, " +
                                        "TL.homdep FECHA_INICIO, " +
                                        "TL.homarr FECHA_FIN, " +
                                        "CD.desc TIPO_MTTO " +
                                    "FROM TRIPLEG TL " +
                                    "INNER JOIN TRIPMAIN TM ON (TL.orig_nmbr = TM.orig_nmbr) " +
                                    "INNER JOIN [acftduty] CD ON (TL.duty_type = CD.code) " +
                                    "WHERE TM.rec_type=''M'' ') TBL " +
                "WHERE 1 = 1 AND CONVERT(VARCHAR(10),GETDATE(), 103) BETWEEN TBL.FECHA_INICIO AND TBL.FECHA_FIN";
                DataTable dt2 = oDB_SP.EjecutarDT_DeQuery(sStatusMtto);


                DataSet ds = new DataSet();

                dt1.TableName = "ProxMtto";
                ds.Tables.Add(dt1);

                dt2.TableName = "StatusMtto";
                ds.Tables.Add(dt2);

                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}