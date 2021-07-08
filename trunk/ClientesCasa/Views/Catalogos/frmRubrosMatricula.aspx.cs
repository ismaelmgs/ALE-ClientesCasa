using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Presenter;
using ClientesCasa.Objetos;

namespace ClientesCasa.Views.Catalogos
{
    public partial class frmRubrosMatricula : System.Web.UI.Page, IViewRubrosMatricula
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new RubrosMatricula_Presenter(this, new DBRubrosMatricula());
            if (!IsPostBack)
            {

            }
        }
        protected void btnBuscarMatricula_Click(object sender, EventArgs e)
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
        protected void gvMatricula_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sMatricula = string.Empty;
                string sSerie = string.Empty;
                string sNomCliente = string.Empty;
                bool ban = false;
                foreach (GridViewRow row in gvMatricula.Rows)
                {
                    if (row.RowIndex == gvMatricula.SelectedIndex)
                    {
                        //row.BackColor = ColorTranslator.FromHtml("#D9E1E4");
                        row.ToolTip = string.Empty;
                        iIdAeronave = gvMatricula.DataKeys[row.RowIndex]["IdAeronave"].S().I();
                        sMatricula = gvMatricula.Rows[row.RowIndex].Cells[1].Text.S();
                        sSerie = gvMatricula.Rows[row.RowIndex].Cells[0].Text.S();
                        sNomCliente = gvMatricula.Rows[row.RowIndex].Cells[4].Text.S();

                        ban = true;
                    }
                    else
                    {
                        //row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                        row.ToolTip = "Clic para seleccionar esta fila.";
                    }
                }

                if (ban)
                {
                    pnlPrincipalRubros.Visible = true;
                    readSerie.Text = sSerie;
                    readMatricula.Text = sMatricula;
                    readCliente.Text = sNomCliente;

                    if (eObjSelected != null)
                        eObjSelected(sender, e);
                }
            }
            catch (Exception ex)
            {
                //Utils.GuardaErrorEnBitacora(mpeMensaje, ex.Message, "frmConfMatriculas.aspx", "frmConfMatriculas.aspx.cs", "gvMatricula_SelectedIndexChanged", "Selecciona Matriculas");
            }
        }
        protected void gvMatricula_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMatricula.PageIndex = e.NewPageIndex;
                LoadMatriculas(dtMatriculas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvMatricula_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gvMatricula, "Select$" + e.Row.RowIndex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                oLstRubros = new List<RubrosMatricula>();
                int iParticipacion = 0;
                foreach (GridViewRow row in gvRubros.Rows)
                {
                    // 1 = total, 
                    // 2 % participacion

                    RadioButtonList rbl = (RadioButtonList)row.FindControl("rblParticipacion");
                    iParticipacion = rbl.SelectedValue == "1" ? 1 : 2;

                    RubrosMatricula oRubro = new RubrosMatricula();
                    oRubro.iIdRubro = gvRubros.DataKeys[row.RowIndex]["IdRubro"].S().I();
                    oRubro.iParticipacion = iParticipacion;
                    oRubro.iIdAeronave = iIdAeronave;

                    oLstRubros.Add(oRubro);
                }

                if (eNewObj != null)
                    eNewObj(sender, e);
            }
            catch (Exception ex)
            {
                
            }
        }
        #region METODOS
        public void LoadMatriculas(DataTable dt)
        {
            try
            {
                dtMatriculas = dt.Copy();
                gvMatricula.DataSource = dt;
                gvMatricula.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadRubros(DataSet ds)
        {
            try
            {
                DataTable dtR = ds.Tables[0].Copy();
                DataTable dt = ds.Tables[1].Copy();

                gvRubros.DataSource = dt;
                gvRubros.DataBind();

                foreach (DataRow row in dtR.Rows)
                {
                    foreach (GridViewRow gr in gvRubros.Rows)
                    {
                        if (row["IdRubro"].S() == gvRubros.DataKeys[gr.RowIndex]["IdRubro"].S())
                        {
                            RadioButtonList rbl = (RadioButtonList)gr.FindControl("rblParticipacion");
                            rbl.SelectedValue = row["Participacion"].S();
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
        RubrosMatricula_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public List<RubrosMatricula> oLstRubros
        {
            get { return (List<RubrosMatricula>)ViewState["VSRubrosMatricula"]; }
            set { ViewState["VSRubrosMatricula"] = value; }
        }
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["VSMatriculas"]; }
            set { ViewState["VSMatriculas"] = value; }
        }
        public int iIdAeronave
        {
            get { return (int)ViewState["VSIdAeronave"]; }
            set { ViewState["VSIdAeronave"] = value; }
        }
        public object[] oArr
        {
            get
            {
                int iEstatus = 2;
                string sSerie = string.Empty;
                string sMatricula = string.Empty;
                string sClaveContrato = string.Empty;
                string sClaveCliente = string.Empty;

                switch (ddlOpcBus.SelectedValue)
                {
                    case "0":
                    case "1":
                        iEstatus = ddlOpcBus.SelectedValue.S().I();
                        break;
                    case "2":
                        sMatricula = txtBusqueda.Text.S();
                        break;
                    case "3":
                        sSerie = txtBusqueda.Text.S();
                        break;
                    case "4":
                        sClaveCliente = txtBusqueda.Text.S();
                        break;
                    case "5":
                        sClaveContrato = txtBusqueda.Text.S();
                        break;
                }
                
                return new object[] {
                                        "@Serie", sSerie,
                                        "@Matricula", sMatricula,
                                        "@ClaveContrato", sClaveContrato,
                                        "@ClaveCliente", sClaveCliente,
                                        "@Status", iEstatus
                                    };
            }
        }
        #endregion
        
    }

}