using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Drawing;
using ClientesCasa.Clases;

namespace ClientesCasa.Views.Reportes
{
    public partial class frmGastosSinAsignar : System.Web.UI.Page, IViewGastosSinAsignar
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new GastosSinAsignar_Presenter(this, new DBGastosSinAsignar());
            if (!IsPostBack) 
            {
                LlenarListaAnio();
            }
        }

        protected void btnBuscarGastos_Click(object sender, EventArgs e)
        {
            try
            {
                iMes = 0;
                iAnio = 0;

                iMes = ddlMes.SelectedItem.Value.S().I();
                iAnio = ddlAnio.SelectedItem.Value.S().I();

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvGastosPesos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvGastosPesos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvGastosPesos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvGastosDolares_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvGastosDolares_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvGastosDolares_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            string sMes = ddlMes.SelectedItem.Text;
            string sAnio = ddlAnio.SelectedItem.Value.S();
            string sNameReport = sMes + " " + sAnio;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=GastosSinAsignar_" + sNameReport + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            pnlReporte.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        #endregion

        #region MÉTODOS

        public void LLenaGastos(DataSet ds)
        {
            try
            {
                //gvClientes.DataSource = dt;
                //gvClientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaReporte(string sHTML)
        {
            try
            {
                pnlGastos.InnerHtml = sHTML;
                btnGenerar.Visible = true;
                pnlReporte.Visible = true;
                lblTituloMes.Text = "Mes: " + ddlMes.SelectedItem.Text;
                lblTituloAnio.Text = "Año: " + ddlAnio.SelectedItem.Value.S();
                lblTituloElaboro.Text = "Elaboró: " + Utils.GetUserName;
                lblTituloFecha.Text = "Fecha: " + DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarListaAnio() 
        {
            try
            {
                for (int i = DateTime.Now.Year; i >= 2000; i--)
                {
                    ListItem lst = new ListItem();
                    lst.Value = i.ToString();
                    lst.Text = i.ToString();
                    ddlAnio.Items.Add(lst);
                }
                ddlAnio.Items.Insert(0, new ListItem(".:Seleccione:.", ""));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region VARIABLES Y PROPIEDADES
        GastosSinAsignar_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public DataSet dsGastos
        {
            get { return (DataSet)ViewState["VSGastos"]; }
            set { ViewState["VSGastos"] = value; }
        }

        public int iMes
        {
            get { return (int)ViewState["VSMes"]; }
            set { ViewState["VSMes"] = value; }
        }
        public int iAnio
        {
            get { return (int)ViewState["VSAnio"]; }
            set { ViewState["VSAnio"] = value; }
        }

        #endregion
    }
}