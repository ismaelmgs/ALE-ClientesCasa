using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;

namespace ClientesCasa.Views.Catalogos
{
    public partial class frmFacturantes : System.Web.UI.Page, IViewFacturantes
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Facturantes_Presenter(this, new DBFacturantes());

            if (!IsPostBack)
            {
                
            }
        }
        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (eObjSelected != null)
                    eObjSelected(sender, e);
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sCardCode = string.Empty;
                string sCardName = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        row.ToolTip = string.Empty;
                        sCustNum = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                        sCardCode = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
                        sCardName = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        ban = true;
                    }
                    else
                    {
                        row.ToolTip = "Clic para seleccionar esta fila.";
                    }
                }

                if (ban)
                {
                    pnlFacturantes.Visible = true;
                    lblRespCustNum.Text = sCardCode;
                    lblRespNombreCliente.Text = sCardName;

                    if (eSearchObj != null)
                        eSearchObj(sender, e);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dtClientes;
                gvClientes.PageIndex = e.NewPageIndex;
                LoadClientes(dt);
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes.Add("title", "De clic aqui para selecciona un cliente");

                        e.Row.Attributes.Add("OnMouseOver", "On(this);");
                        e.Row.Attributes.Add("OnMouseOut", "Off(this);");
                        e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gvClientes, "Select$" + e.Row.RowIndex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void gvContratos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                iIdFacturante = gvContratos.DataKeys[e.CommandArgument.S().I()]["IdFacturante"].S().I();

                if (eSetEliminaFacturante != null)
                    eSetEliminaFacturante(sender, e);

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAceptarMatriculas_Click(object sender, EventArgs e)
        {
            try
            {
                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                foreach (GridViewRow row in gvMatriculas.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSelecciona");
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            sClaveContrato = row.Cells[2].Text.S();

                            if (eNewObj != null)
                                eNewObj(sender, e);

                            if (eSearchObj != null)
                                eSearchObj(sender, e);
                        }
                    }
                }

                mpeMatricula.Hide();
            }
            catch (Exception)
            {

            }
        }
        protected void btnAgregarMatricula_Click(object sender, EventArgs e)
        {
            if (eGetMatriculas != null)
                eGetMatriculas(sender, e);
            
            mpeMatricula.Show();
        }
        #endregion

        #region METODOS
        public void LoadClientes(DataTable dt)
        {
            dtClientes = dt;
            gvClientes.DataSource = dt;
            gvClientes.DataBind();
        }
        public void LoadMatriculasAsignadas(DataTable dt)
        {
            try
            {
                dtContratosAsignados = dt;
                gvContratos.DataSource = dt;
                gvContratos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadMatriculas(DataTable dt)
        {
            try
            {
                gvMatriculas.DataSource = dt;
                gvMatriculas.DataBind();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dtContratosAsignados.Rows)
                    {
                        foreach (GridViewRow gr in gvMatriculas.Rows)
                        {
                            if (gr.Cells[1].Text.S() == row["Matricula"].S() && gr.Cells[2].Text.S() == row["ClaveContrato"].S())
                            {
                                CheckBox chk = (CheckBox)gr.Cells[0].FindControl("chkSelecciona");
                                if (chk != null)
                                {
                                    chk.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        Facturantes_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetMatriculas;
        public event EventHandler eSetEliminaFacturante;

        public object[] oArray
        {
            get
            {
                string sClaveCliente = string.Empty;
                string sNombreCliente = string.Empty;

                switch (ddlOpcion.SelectedValue.S())
                {
                    case "1":
                        sClaveCliente = txtBusqueda.Text.S();
                        break;
                    case "2":
                        sNombreCliente = txtBusqueda.Text.S();
                        break;
                }

                return new object[]
                {
                    sClaveCliente, sNombreCliente
                };
            }
        }
        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["VClientes"]; }
            set { ViewState["VClientes"] = value; }
        }
        public string sCustNum
        {
            get { return (string)ViewState["VSCustNum"]; }
            set { ViewState["VSCustNum"] = value; }
        }
        public string sClaveContrato
        {
            get { return (string)ViewState["VSClaveContrato"]; }
            set { ViewState["VSClaveContrato"] = value; }
        }
        public DataTable dtContratosAsignados
        {
            get { return (DataTable)ViewState["VSContratosA"]; }
            set { ViewState["VSContratosA"] = value; }
        }
        public int iIdFacturante
        {
            get { return (int)ViewState["VSIdFacturante"]; }
            set { ViewState["VSIdFacturante"] = value; }
        }
        #endregion


    }
}