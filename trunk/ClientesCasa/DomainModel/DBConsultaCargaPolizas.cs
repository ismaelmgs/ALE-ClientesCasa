using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientesCasa.Clases;
using System.Data;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using System.Globalization;
using System.Data.SqlClient;

namespace ClientesCasa.DomainModel
{
    public class DBConsultaCargaPolizas : DBIntegrator
    {
        public DataTable GetQuerySAP(string sQuery)
        {
            try
            {
                return oDB_SP.EjecutarDT_DeQuery(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBObtenerCargasPoliza(string sFechaIni, string sFechaFin)
        {
            try
            {
                DataTable dtResult = new DataTable();

                dtResult = new DBIntegrator().oDB_SP.EjecutarDT("[Principales].[spS_DI_ConsultaCargaPolizasNomina]", "@FechaIni", sFechaIni.Dt(),
                                                                                                                             "@FechaFin", sFechaFin.Dt());
                return dtResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable DBObtenerCargasXFolio(long lgFolio)
        {
            try
            {
                DataTable dtResult = new DataTable();

                dtResult = new DBIntegrator().oDB_SP.EjecutarDT("[Principales].[spS_DI_ConsultaXFolioCarga]", "@Folio", lgFolio);
                return dtResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}