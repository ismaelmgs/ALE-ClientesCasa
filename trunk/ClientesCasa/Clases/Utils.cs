using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.IO;
using ClientesCasa.Objetos;
using ClientesCasa.DomainModel;

using ClientesCasa.Clases;
using System.ComponentModel;

namespace ClientesCasa.Clases
{
    public static class Utils
    {
        public static DataTable ConvertListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }
        public static string SaveErrorEnBitacora(string sError, string sPagina, string sClase, string sMetodo, string sCaption)
        {
            try
            {
                object[] obj = new object[]
                                        {
                                            "@Descripcion", sError,
                                            "@Pagina", sPagina,
                                            "@Clase", sClase,
                                            "@Metodo", sMetodo
                                        };

                DataTable dtErrores = new DomainModel.DBUtils().DBSaveBitacoraError(obj);

                if (dtErrores.Rows.Count > 0)
                {
                    return dtErrores.Rows[0][0].S();
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetIPAddress
        {
            get
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }

                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        /// <summary>
        /// Obtiene el nickname del usuario en session
        /// </summary>
        public static string GetUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserIdentity"] == null)
                {
                    UserIdentity oUsuario = new UserIdentity();
                    oUsuario.sUsuario = "system";
                    System.Web.HttpContext.Current.Session["UserIdentity"] = oUsuario;
                }

                return ((UserIdentity)System.Web.HttpContext.Current.Session["UserIdentity"]).sUsuario;
            }
        }
        /// <summary>
        /// Obtiene el nombre del usuario en session
        /// </summary>
        public static string GetUserName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserIdentity"] == null)
                {
                    UserIdentity oUsuario = new UserIdentity();
                    oUsuario.sUsuario = "(usuario)";
                    System.Web.HttpContext.Current.Session["UserIdentity"] = oUsuario;
                }

                return ((UserIdentity)System.Web.HttpContext.Current.Session["UserIdentity"]).sName;
            }
        }

        public static SAPbobsCOM.Company oCompany
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Company"] == null)
                {
                    System.Web.HttpContext.Current.Session["Company"] = new SAPbobsCOM.Company();
                }

                return ((SAPbobsCOM.Company)System.Web.HttpContext.Current.Session["Company"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["Company"] = value;
            }
        }
        public static bool DestroyCOMObject(object oSapObject)
        {
            try
            {
                bool ban = false;

                if (oSapObject != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSapObject);
                    oSapObject = null;
                    GC.Collect();
                }

                ban = true;

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void GuardarBitacora(string sMensaje)
        {
            try
            {
                string lsCarpeta = @"c:/Bitacoras/"; //Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\BitacorasApp\\";
                string lsArchivo = "Bitacora_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";

                //valida si ya existe la carpeta o no
                if (!Directory.Exists(lsCarpeta))
                    Directory.CreateDirectory(lsCarpeta);

                lsArchivo = lsCarpeta + lsArchivo;

                //valida si existe el archivo
                if (!File.Exists(lsArchivo))
                    File.CreateText(lsArchivo).Close();

                StreamWriter loSW = File.AppendText(lsArchivo);

                loSW.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + sMensaje);

                loSW.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ObtieneParametroPorClave(string sClave)
        {
            try
            {
                return new DBParametros().ObtieneParametroPorClave(sClave);
            }
            catch (Exception x) 
            { 
                throw x; 
            }
        }
        public static string ObtieneTotalTiempo(DataTable dtTramos, string sColumna, ref float fTiempoT)
        {
            try
            {
                string sCantidad = string.Empty;
                int iHoras = 0;
                double iMinutos = 0;
                double fTiempo = 0;

                foreach (DataRow row1 in dtTramos.Rows)
                {
                    if (row1[sColumna].S() != string.Empty)
                    {
                        string[] sPartes = row1[sColumna].S().Split(':');

                        iHoras += sPartes[0].S().I();
                        iMinutos += sPartes[1].S().I();
                    }
                }

                iMinutos = Math.Round(iMinutos, 2);


                fTiempo = iHoras + (iMinutos / 60);
                fTiempoT = float.Parse(fTiempo.S());

                sCantidad = ConvierteDecimalATiempo(fTiempo.S().D());

                return sCantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ObtieneTotalTiempoPorMes(DataTable dtTramos, string sColumna, int iMes)
        {
            try
            {
                string sCantidad = string.Empty;
                int iHoras = 0;
                double iMinutos = 0;
                double fTiempo = 0;
                float fTiempoT = 0;

                DataRow[] drMes = dtTramos.Select("Mes = " + iMes.S());
                if (drMes != null && drMes.Length > 0)
                {
                    for (int i = 0; i < drMes.Length; i++)
                    {
                        if (drMes[i][sColumna].S() != string.Empty)
                        {
                            string[] sPartes = drMes[i][sColumna].S().Split(':');

                            iHoras += sPartes[0].S().I();
                            iMinutos += sPartes[1].S().I();
                        }
                    }

                    iMinutos = Math.Round(iMinutos, 2);

                    fTiempo = iHoras + (iMinutos / 60);
                    fTiempoT = float.Parse(fTiempo.S());

                    sCantidad = ConvierteDecimalATiempo(fTiempo.S().D());
                }
                else
                    sCantidad = "00:00";

                return sCantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ConvierteDecimalATiempo(decimal fTiempo)
        {
            try
            {
                if (fTiempo != 0)
                {
                    bool ban = false;

                    if (fTiempo < 0)
                    {
                        ban = true;
                        fTiempo = fTiempo * (-1);
                    }

                    int iHoras = 0;
                    int iMinutos = 0;

                    iHoras = Math.Truncate(fTiempo).S().I();
                    iMinutos = Math.Round(((fTiempo - Math.Truncate(fTiempo)).S().D() * 60)).S().I();

                    if (iMinutos == 60)
                    {
                        iHoras++;
                        iMinutos = 0;
                    }

                    return ban ? "-" + iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0') : iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0');
                }
                else
                    return "00:00";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static float ConvierteTiempoaDecimal(string sTiempo)
        {
            try
            {
                int iHoras = 0;
                double iMinutos = 0;
                float fTiempo = 0;

                if (sTiempo.Length >= 5)
                {
                    string[] sPartes = sTiempo.Split(':');

                    iHoras = sPartes[0].S().I();
                    iMinutos = sPartes[1].S().I();

                    float TotMin = float.Parse((iMinutos / 60).S());

                    return (iHoras + TotMin);

                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}