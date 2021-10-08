using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Clases;
using System.Data;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System.IO;


namespace ClientesCasa.Views.Catalogos
{
    public partial class frmCorteGastos : System.Web.UI.Page, IViewCorteMensual
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new CorteMensual_Presenter(this, new DBCorteMensual());

            if (!IsPostBack)
            {

            }
        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(sender, e);
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

        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = dtClientes;
                gvClientes.PageIndex = e.NewPageIndex;
                LLenaClientes(dt);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ////dtGastosMex = null;
                //lstCliente = new List<string>();
                string sMatriculag = string.Empty;
                string sContratog = string.Empty;
                string sClaveCliente = string.Empty;
                string sNombreCliente = string.Empty;
                //string sRazonSocial = string.Empty;
                //string sRFC = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        sClaveCliente = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                        sNombreCliente = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
                        sContratog = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        sMatriculag = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();
                        
                        Label lblAeronaveId = (Label)gvClientes.Rows[row.RowIndex].FindControl("lblIdAeronave");
                        ban = true;
                    }

                    if (ban)
                    {
                        lblNombreCliente.Text = sNombreCliente;
                        lblMatricula.Text = sMatriculag;
                        lblClaveCliente.Text = sClaveCliente;
                        sClaveContrato = sContratog;

                        mpePeriodo.Show();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAceptarPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("VPeriodo");
                if (Page.IsValid)
                {
                    string[] sPeriodo = txtPeriodo.Text.S().Split('/');

                    if (sPeriodo.Length == 1)
                        sPeriodo = txtPeriodo.Text.S().Split('-');

                    iMes = sPeriodo[1].S().I();
                    iAnio = sPeriodo[0].S().I();


                    lblReqMes.Text = ObtieneNombreMes(iMes);
                    lblReqAnio.Text = iAnio.S();
                    
                    if (eObjSelected != null)
                        eObjSelected(sender, e);

                    upaPrincipal.Update();
                    pnlCorte.Visible = true;

                    mpePeriodo.Hide();
                }
                else
                    mpePeriodo.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCorte_Click(object sender, EventArgs e)
        {
            lblCaption.Text = "Confirmación";
            lblMessageConfirm.Text = "¿Realmente deseas guardar esta información?";
            mpeConfirm.Show();
        }

        protected void btnAceptConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelConfirm_Click(object sender, EventArgs e)
        {
            mpeConfirm.Hide();
        }
        #endregion

        #region METODOS
        public void LLenaClientes(DataTable dt)
        {
            try
            {
                gvClientes.DataSource = dt;
                gvClientes.DataBind();
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

        private string ObtieneNombreMes(int iMes)
        {
            string sMes = string.Empty;
            switch (iMes)
            {
                case 1:
                    sMes = "ENERO";
                    break;
                case 2:
                    sMes = "FEBRERO";
                    break;
                case 3:
                    sMes = "MARZO";
                    break;
                case 4:
                    sMes = "ABRIL";
                    break;
                case 5:
                    sMes = "MAYO";
                    break;
                case 6:
                    sMes = "JUNIO";
                    break;
                case 7:
                    sMes = "JULIO";
                    break;
                case 8:
                    sMes = "AGOSTO";
                    break;
                case 9:
                    sMes = "SEPTIEMBRE";
                    break;
                case 10:
                    sMes = "OCTUBRE";
                    break;
                case 11:
                    sMes = "NOVIEMBRE";
                    break;
                case 12:
                    sMes = "DICIEMBRE";
                    break;
            }

            return sMes;
        }

        public void LlenaCalculoCorte(DataTable dt)
        {
            gvCorteMensual.DataSource = dt;
            gvCorteMensual.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ExisteMesActual"].S() == "1")
                {
                    lblValidacion.Text = "El mes actual ya se ha cerrado, favor de verificar";
                    btnCorte.Enabled = false;
                }
                else if (dt.Rows[0]["ExisteMesAnterior"].S() == "0")
                {
                    lblValidacion.Text = "El mes anterior no se ha cerrado, favor de cerrarlo primero.";
                    btnCorte.Enabled = false;
                }
                else
                    lblValidacion.Text = string.Empty;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        CorteMensual_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        
        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["VClientes"]; }
            set { ViewState["VClientes"] = value; }
        }
        
        public string sMatricula
        {
            get { return (string)ViewState["VMatricula"]; }
            set { ViewState["VMatricula"] = value; }
        }

        public object[] oArray
        {
            get
            {
                string sEstatus = string.Empty;
                string sClaveContrato = string.Empty;
                string sClaveCliente = string.Empty;
                string sMatricula = string.Empty;

                switch (ddlOpcBus.SelectedValue.S())
                {
                    case "0":
                    case "1":
                        sEstatus = ddlOpcBus.SelectedValue.S();
                        break;
                    case "2":
                        sEstatus = "2";
                        sClaveCliente = txtBusqueda.Text.S();
                        break;
                    case "3":
                        sEstatus = "2";
                        sClaveContrato = txtBusqueda.Text.S();
                        break;
                    case "4":
                        sEstatus = "2";
                        sMatricula = txtBusqueda.Text.S();
                        break;
                }

                return new object[]
                {

                    "@ClaveCliente", sClaveCliente,
                    "@ClaveContrato", sClaveContrato,
                    "@Estatus", sEstatus,
                    "@Matricula", sMatricula
                };
            }
        }

        public string sClaveContrato
        {
            get { return ViewState["VClaveContrato"].S(); }
            set { ViewState["VClaveContrato"] = value; }
        }
        
        public int iMes
        {
            get { return (int)ViewState["VMes"]; }
            set { ViewState["VMes"] = value; }
        }

        public int iAnio
        {
            get { return (int)ViewState["VAnio"]; }
            set { ViewState["VAnio"] = value; }
        }
        
        #endregion
        
    }
}