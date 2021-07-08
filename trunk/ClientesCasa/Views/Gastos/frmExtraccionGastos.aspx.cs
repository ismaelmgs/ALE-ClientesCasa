using ClientesCasa.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Data;
using System.IO;
using System.Text;

namespace ClientesCasa.Views.Gastos
{
    public partial class frmExtraccionGastos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                    LlenaMatriculas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                bool ban = true;

                if (txtPeriodo.Text.S() == string.Empty)
                {
                    lblReqPeriodo.Visible = true;
                    ban = false;
                }
                else
                    lblReqPeriodo.Visible = false;


                if (ddlMatricula.SelectedValue.S() == string.Empty)
                {
                    lblReqMatricula.Visible = true;
                    ban = false;
                }
                else
                    lblReqMatricula.Visible = false;

                if (ban)
                {
                    string[] sPeriodo = txtPeriodo.Text.S().Split('/');
                    if (sPeriodo.Length == 1)
                    {
                        sPeriodo = txtPeriodo.Text.Split('-');
                    }

                    int iMes = sPeriodo[1].S().I();
                    int iAnio = sPeriodo[0].S().I();

                    string sCadMats = string.Empty;
                    foreach (DataRow row in dtMats.Rows)
                    {
                        if (sCadMats == string.Empty)
                            sCadMats = "'" + row["Matricula"].S() + "'";
                        else
                            sCadMats = sCadMats + "," + "'" + row["Matricula"].S() + "'";
                    }

                    DataTable dtGastos = new DBExtraccion().DBGetObtieneGastosPorPeriodo(iMes, iAnio, ddlMatricula.SelectedItem.Text.S(), sCadMats);
                    gvGastos.DataSource = dtGastos;
                    gvGastos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenaMatriculas()
        {
            try
            {
                dtMats = new DBExtraccion().DBGetObtieneMatriculasCC;
                ddlMatricula.DataSource = dtMats;
                ddlMatricula.DataTextField = "Matricula";
                ddlMatricula.DataValueField = "IdAeroave";
                ddlMatricula.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=Extraccion.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvGastos.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        public DataTable dtMats
        {
            set { ViewState["VSMatriculas"] = value; }
            get { return (DataTable)ViewState["VSMatriculas"]; }
        }

    }
}