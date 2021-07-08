using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using NucleoBase.Core;
using System.Data;
using ClientesCasa.Clases;

namespace ClientesCasa.DomainModel
{
    public class DBUtils : DBBase
    {
        public DataTable DBSaveBitacoraError(object[] oArr)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spI_MXJ_InsertaLogError]", oArr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetLoadInitialValues()
        {
            try
            {
                DataTable dt = oDB_SP.EjecutarDT("[Configuracion].[spS_DI_ConsultaAccesosSBO]");
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    //// PRODUCCION
                    //MyGlobals.oCompany.Server = "SAP";//row["Servidor"].S(); //"SAP";//row["Servidor"].S();  // "SAPPILOTO";
                    //MyGlobals.oCompany.CompanyDB = "Aerolineas_Ejecutivas";//"Aerolineas_Simulacro";//row["DBCompania"].S(); // "SBODemoMX";
                    //MyGlobals.oCompany.UserName = "manager";//"manager";//row["SBOUserName"].S(); // "manager";
                    //MyGlobals.oCompany.Password = "sapaero";//"sapaero";//row["SBOPassword"].S(); // "12345";
                    //MyGlobals.oCompany.LicenseServer = "SAP:30000";//"SAP:30000";//row["Licencia"].S(); // "SAPPILOTO";
                    //MyGlobals.oCompany.DbUserName = "sa";//"sa"; //row["DBUsuario"].S(); // "sa";
                    //MyGlobals.oCompany.DbPassword = "P@$w0rd2017";//"Pr0c3s0.12";//row["DBPassword"].S(); // "Pr0c3s0.12";


                    Utils.oCompany = new SAPbobsCOM.Company();
                    //PILOTO
                    
                    Utils.oCompany.Server = row["Servidor"].S();        //"SAPPILOTO";            
                    Utils.oCompany.CompanyDB = row["DBCompania"].S();      //"Aerolineas_Pruebas";
                    Utils.oCompany.UserName = row["SBOUserName"].S();     //"manager";              
                    Utils.oCompany.Password = row["SBOPassword"].S();     //"sapaero";              
                    Utils.oCompany.LicenseServer = row["Licencia"].S();        //"SAPPILOTO";            
                    Utils.oCompany.DbUserName = row["DBUsuario"].S();       //"sa";                   
                    Utils.oCompany.DbPassword = row["DBPassword"].S();      //"Pr0c3s0.12";

                    switch (row["TipoServidor"].S())
                    {
                        case "1":
                            Utils.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL;
                            break;
                        case "4":
                            Utils.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
                            break;
                        case "6":
                            Utils.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                            break;
                        case "7":
                            Utils.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                            break;
                        case "8":
                            Utils.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                            break;
                    }

                    //Utils.GuardarBitacora("Intenta conectar a SAP");

                    int iError = Utils.oCompany.Connect();
                    if (iError != 0)
                    {
                        string sError = string.Empty;
                        Utils.oCompany.GetLastError(out iError, out sError);
                        //MyGlobals.sStepLog = "Conectar: " + iError.S() + " Mensaje: " + sError;
                        throw new Exception(MyGlobals.sStepLog);
                    }
                    //else
                    //    Utils.GuardarBitacora("Conectado a SAP con exito!!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}