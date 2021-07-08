using ClientesCasa.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBResultadoNomina : DBIntegrator
    {
        public DataSet DBGetObtieneDetalleNomina(int iIdFolio)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_PN_ObtieneDetalleNomina]", "@IdFolio", iIdFolio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetValueByQuery(string sQ)
        {
            SAPbobsCOM.Recordset oRS = MyGlobals.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            object sRes = null;

            try
            {
                oRS.DoQuery(sQ);
                if (oRS.RecordCount > 0)
                    sRes = oRS.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oRS != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRS);
                    oRS = null;
                    GC.Collect();
                }
            }

            return sRes;
        }

        public DataTable DBGetObtieneConceptosDetalleNomina(int iIdFolio, string sEmpresa, int iNoFact)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_PN_ObtieneConceptosEmpresa]", "@IdFolio", iIdFolio,
                                                                                            "@Empresa", sEmpresa,
                                                                                            "@NoFact", iNoFact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneParametrosPolizaNomina()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_PN_ObtieneParametros]");
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
                    MyGlobals.oCompany = new SAPbobsCOM.Company();

                    ////PRODUCCION
                    //MyGlobals.oCompany.Server = "SAP";//row["Servidor"].S(); //"SAP";//row["Servidor"].S();  // "SAPPILOTO";
                    //MyGlobals.oCompany.CompanyDB = "Aerolineas_Ejecutivas";//"Aerolineas_Simulacro";//row["DBCompania"].S(); // "SBODemoMX";
                    //MyGlobals.oCompany.UserName = "manager";//"manager";//row["SBOUserName"].S(); // "manager";
                    //MyGlobals.oCompany.Password = "sapaero";//"sapaero";//row["SBOPassword"].S(); // "12345";
                    //MyGlobals.oCompany.LicenseServer = "SAP:30000";//"SAP:30000";//row["Licencia"].S(); // "SAPPILOTO";
                    //MyGlobals.oCompany.DbUserName = "sa";//"sa"; //row["DBUsuario"].S(); // "sa";
                    //MyGlobals.oCompany.DbPassword = "P@$w0rd2017";//"Pr0c3s0.12";//row["DBPassword"].S(); // "Pr0c3s0.12";

                    //PILOTO
                    MyGlobals.oCompany.Server = "SAPPILOTO";          //row["Servidor"].S();        
                    MyGlobals.oCompany.CompanyDB = "Aerolineas_Ejecutivas";  //row["DBCompania"].S();      
                    MyGlobals.oCompany.UserName = "manager";                //row["SBOUserName"].S();     
                    MyGlobals.oCompany.Password = "sapaero";                //row["SBOPassword"].S();     
                    MyGlobals.oCompany.LicenseServer = "SAPPILOTO";        //row["Licencia"].S();        
                    MyGlobals.oCompany.DbUserName = "sa";                     //row["DBUsuario"].S();       
                    MyGlobals.oCompany.DbPassword = "Pr0c3s0.12";             //row["DBPassword"].S();      

                    switch (row["TipoServidor"].S())
                    {
                        case "1":
                            MyGlobals.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL;
                            break;
                        case "4":
                            MyGlobals.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
                            break;
                        case "6":
                            MyGlobals.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                            break;
                        case "7":
                            MyGlobals.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                            break;
                        case "8":
                            MyGlobals.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                            break;
                    }

                    int iError = MyGlobals.oCompany.Connect();
                    if (iError != 0)
                    {
                        string sError = string.Empty;
                        MyGlobals.oCompany.GetLastError(out iError, out sError);
                        MyGlobals.sStepLog = "Conectar: " + iError.S() + " Mensaje: " + sError;
                        throw new Exception(MyGlobals.sStepLog);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneEmpleadoPorEmpresa(int iIdFolio, string sEmpresa)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_PN_ObtieneEmpleadoPorEmpresa]", "@IdFolio", iIdFolio,
                                                                                            "@Empresa", sEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneEmpleadoPorEmpresaYFactura(int iIdFolio, string sEmpresa, int iNoFact)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_PN_ObtieneEmpleadoPorEmpresaNoFactura]", "@IdFolio", iIdFolio,
                                                                                                        "@Empresa", sEmpresa,
                                                                                                        "@NumeroFact", iNoFact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtieneCalculoPolizaNomina(int iIdFolio, string sRFCEmp, string sEmpresaFact, int iNoFact)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_PN_ConsultaDetallePolizaNomina]", "@IdFolio", iIdFolio,
                                                                                                "@RFC_EMP", sRFCEmp,
                                                                                                "@EmpresaFact", sEmpresaFact,
                                                                                                "@NoFactura", iNoFact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetEliminaPolizaPorEmpresaYFolio(int iIdFolio, string sEmpresa, int iNoFact)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spS_PN_EliminaPolizaPorEmpresaYFolio]", "@IdFolio", iIdFolio,
                                                                                        "@Empresa", sEmpresa,
                                                                                        "@NoFactura", iNoFact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaResultadoPolizaPorEmpleado(int iIdFolio, string RFCEmp, string EmpresaFact, int iNoFact)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spS_PN_GeneraCalculoPolizaPorEmpresa]", "@IdFolio", iIdFolio,
                                                                                            "@RFC_EMP", RFCEmp,
                                                                                            "@EmpresaFact", EmpresaFact,
                                                                                            "@NoFactura", iNoFact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int DBSetInsertaFacturaAprobadaRH(int iIdFolio, string sEmpresa, int iNoFact)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_PN_InsertaFacturaAprobadaPorRH]", "@IdFolio", iIdFolio,
                                                                                                        "@Empresa", sEmpresa,
                                                                                                        "@NoFactura", iNoFact,
                                                                                                        "@UsuarioCreacion", Utils.GetUserName);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaArchivoXML(int iIdFolio, string sEmpresa, int iNoFact, byte[] oArr, string sFileName)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_PN_InsertaXMLFacturas]", "@IdFolio", iIdFolio,
                                                                                                "@Empresa", sEmpresa,
                                                                                                "@NoFactura", iNoFact,
                                                                                                "@XmlDoc", oArr,
                                                                                                "@UsuarioXML", Utils.GetUserName,
                                                                                                "@NombreXML", sFileName);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaArchivoPDF(int iIdFolio, string sEmpresa, int iNoFact, byte[] oArr, string sFileName)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_PN_InsertaPdfFacturas]", "@IdFolio", iIdFolio,
                                                                                                "@Empresa", sEmpresa,
                                                                                                "@NoFactura", iNoFact,
                                                                                                "@XmlPdf", oArr,
                                                                                                "@UsuarioPDF", Utils.GetUserName,
                                                                                                "@NombrePDF", sFileName);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetValidaPDF_Factura(int iIdFolio, string sEmpresaFact, int iNoFact)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_PN_ValidaPdfFactura]", "@IdFolio", iIdFolio,
                                                                                  "@Empresa", sEmpresaFact,
                                                                                  "@NoFactura", iNoFact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetValidaXML_Factura(int iIdFolio, string sEmpresaFact, int iNoFact)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_PN_ValidaXMLFactura]", "@IdFolio", iIdFolio,
                                                                                  "@Empresa", sEmpresaFact,
                                                                                  "@NoFactura", iNoFact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}