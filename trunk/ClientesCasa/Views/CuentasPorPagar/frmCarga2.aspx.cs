using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

using Microsoft.Office.Interop.Excel;
using NucleoBase.Core;

public partial class Views_CuentasPorPagar_frmCarga : System.Web.UI.Page
{

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { }
    }
    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        try
        {
            if (fluArchivo.HasFile)
            {
                string FileName = Path.GetFileName(fluArchivo.PostedFile.FileName);
                string Extension = Path.GetExtension(fluArchivo.PostedFile.FileName);
                string FolderPath = "~/Files/";

                string FilePath = Server.MapPath(FolderPath + FileName);
                fluArchivo.SaveAs(FilePath);
                Import_To_DataTable(FilePath, Extension);
            }
        }
        catch (Exception ex) {
            throw ex;
        }

    }

    #endregion

    #region MÉTODOS
    private void Import_To_DataTable(string FilePath, string Extension)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=YES\";";
                break;
            case ".xlsx": //Excel 07, 2013, etc
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=YES\";";
                break;
        }
        conStr = String.Format(conStr, FilePath);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;

        //Obtiene el nombre de la primera hoja
        connExcel.Open();
        System.Data.DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Lee los datos de la primera hoja
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        //Bind Data to GridView
        //GridView1.Caption = Path.GetFileName(FilePath);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
    }
    #endregion

    #region PROPIEDADES Y VARIABLES
    #endregion

}