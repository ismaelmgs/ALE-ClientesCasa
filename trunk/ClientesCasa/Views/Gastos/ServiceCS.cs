using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;

/// <summary>
/// Summary description for ServiceCS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class ServiceCS : System.Web.Services.WebService
{
    [WebMethod]
    public CascadingDropDownNameValue[] GetTipoGasto(string knownCategoryValues)
    {
        string query = "SELECT DISTINCT Concepto AS Descripcion FROM [ClientesCasa].[tbc_CC_TiposGastosConcur]";
        List<CascadingDropDownNameValue> countries = GetData(query);
        return countries.ToArray();
    }

    [WebMethod]
    public CascadingDropDownNameValue[] GetAmpliado(string knownCategoryValues)
    {
        string country = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["TipoGasto"];
        string query = string.Format("SELECT DISTINCT ISNULL(SubConcepto,'') Descripcion FROM [ClientesCasa].[tbc_CC_TiposGastosConcur] WHERE Concepto = '{0}' GROUP BY SubConcepto HAVING ( COUNT(SubConcepto) > 0)", country);
        List<CascadingDropDownNameValue> cities = GetData(query);
        return cities.ToArray();
    }

    private List<CascadingDropDownNameValue> GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();
            cmd.Connection = con;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    values.Add(new CascadingDropDownNameValue
                    {
                        name = reader[0].ToString(),
                        value = reader[0].ToString()
                    });
                }
                reader.Close();
                con.Close();
                return values;
            }
        }
    }
}
