using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using ClientesCasa.Presenter;

using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

using NucleoBase.Core;
using System.Globalization;
using ClientesCasa.Clases;
using System.Net.Mail;
using ClosedXML.Excel;
using System.Text;

namespace ClientesCasa.Views.ASC
{
    public partial class frmEnvioFacturas : System.Web.UI.Page, IViewEnvioFacturas
    {
        #region EVENTOS

        protected void Page_PreRenderComplete(object sender, EventArgs e) 
        {
            //hdnArticuloValor.Value = ddlArticulo.SelectedValue;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new EnvioFacturas_Presenter(this, new DBAccesoSAP());
                Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

                //txtDe.Text = DateTime.Now.ToString();

                if (!IsPostBack)
                {
                    sEstatus = "1";
                    ddlEstatus.SelectedIndex = 1;

                    if (sFechaDesde == "0001-01-01")
                        sFechaDesde = string.Empty;
                    if (sFechaHasta == "0001-01-01")
                        sFechaHasta = string.Empty;

                    if (eSearchFacturas != null)
                        eSearchFacturas(null, null);

                    if (eSearchFlotasMXJ != null)
                        eSearchFlotasMXJ(null, null);

                    //Obtener Matriculas Clasificadas
                    if (dtMergeMatriculas == null)
                        dtMergeMatriculas = ObtenerMatchMatriculas();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(ex.Message.S(), sPagina, sClase, "Page_Load", "Error");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                sNumDoc = string.Empty;
                sFechaDesde = string.Empty;
                sFechaHasta = string.Empty;
                sEstatus = string.Empty;
                sTipoMtto = string.Empty;
                sFlota = string.Empty;
                sMatricula = string.Empty;

                sNumDoc = txtNoDocumento.Text;
                sFechaDesde = txtDe.Text.Dt().ToString("yyyy-MM-dd");
                sFechaHasta = txtHasta.Text.Dt().ToString("yyyy-MM-dd");
                sEstatus = ddlEstatus.SelectedItem.Value;
                sTipoMtto = ddlTipoMtto.SelectedItem.Value;
                sFlota = ddlFlota.SelectedItem.Value;
                sMatricula = txtMatriculaCC.Text;

                if (sFechaDesde == "0001-01-01")
                    sFechaDesde = string.Empty;
                if (sFechaHasta == "0001-01-01")
                    sFechaHasta = string.Empty;

                if (eSearchFacturas != null)
                    eSearchFacturas(null, null);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnParticionar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeParticionar.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAceptarParticion_Click(object sender, EventArgs e)
        {
            try
            {
                dtFacParticiones = null;
                int indexG = 0;
                string sNumDoc = string.Empty;
                string sImporteFactura = sImporte;
                int iPart = txtNumParticiones.Text.I();

                if (!string.IsNullOrEmpty(hdnIndexGrid.Value))
                    indexG = hdnIndexGrid.Value.I();
                if (!string.IsNullOrEmpty(hdnNumDoc.Value))
                    sNumDoc = hdnNumDoc.Value;
                if (!string.IsNullOrEmpty(sImporte))
                    sImporteFactura = sImporte;
                if (!string.IsNullOrEmpty(txtNumParticiones.Text) && txtNumParticiones.Text.I() > 0)
                    iPart = txtNumParticiones.Text.I();

                ParticionarFactura(indexG, sNumDoc, sImporteFactura, iPart);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //ddlCentroCostos.Items.Clear();

                //if (e.CommandName == "Particionar")
                //{
                //    txtNumParticiones.Text = string.Empty;
                //    int index = e.CommandArgument.S().I();
                //    string sNumDoc = gvFacturas.Rows[index].Cells[2].Text;

                //    sImporte = gvFacturas.Rows[index].Cells[5].Text;
                //    hdnNumDoc.Value = sNumDoc;
                //    hdnIndexGrid.Value = index.S();
                //    hdnIndexGridCC.Value = string.Empty;
                //    hdnIndexGridCCD.Value = string.Empty;
                //    upaGridFacturas.Update();
                //}

                if (e.CommandName == "AgregarCC") 
                {
                    rdnLstFlota.Items.Clear();
                    //lstCCMultiple.Items.Clear();
                    int index = e.CommandArgument.S().I();
                    string sNumDoc = gvFacturas.Rows[index].Cells[13].Text;
                    Label lblMatDefault = (Label)gvFacturas.Rows[index].FindControl("lblCentroCostos");

                    hdnNumDocCC.Value = sNumDoc;
                    hdnIndexGrid.Value = index.S();
                    hdnIndexGridCC.Value = index.S();
                    hdnIndexGridCCD.Value = string.Empty;
                    hdnOpcion.Value = "1";
                    hdnMatriculaDefault.Value = lblMatDefault.Text;
                    LoadListFlotasModal();

                    if (!string.IsNullOrEmpty(hdnMatriculaDefault.Value))
                        ConsultaMatriculaPrincipal();

                    upaGridFacturas.Update();
                }
                else if (e.CommandName == "ViewReference") 
                {
                    int index = e.CommandArgument.S().I();
                    string sNumFactura = gvFacturas.Rows[index].Cells[2].Text;
                    string sFechaFactura = gvFacturas.Rows[index].Cells[4].Text;
                    string sFile = sNumFactura;
                    sCadArchivo = string.Empty;

                    sArchivo = sFile;
                    sFechaDoc = GetFormatoFecha(sFechaFactura);

                    if (eGetValidaPDF != null)
                        eGetValidaPDF(null, EventArgs.Empty);

                    string sPath = string.Empty;
                    string[] sArrFecha = sFechaDoc.Replace("/","-").Split('-');
                    
                    string sAnio = sArrFecha[2].S();
                    string sMes = string.Empty;

                    if (sArrFecha[1].Length == 1)
                        sMes = "0" + sArrFecha[1].S();
                    else
                        sMes = sArrFecha[1].S();

                    if (!File.Exists(sCadArchivo))
                    {
                        Mensaje("Lo sentimos, el archivo " + sFile + ".pdf no existe");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmVistaPreviaRefDoc.aspx?mes=" + sMes + "&anio=" + sAnio + "&archivo=" + sArchivo + "',this.target, 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (eSearchTipoMtto != null)
                    eSearchTipoMtto(null, null);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int index = e.Row.RowIndex;
                    sNumDoc = dtResultFac.Rows[index]["DocNum"].S();//gvFacturas.Rows[index].Cells[2].Text;
                    int iEstatus = dtResultFac.Rows[index]["Estatus"].S().I(); //gvFacturas.Rows[index].Cells[9].Text.S().I();

                    //ImageButton imgBtnDet = (ImageButton)gvFacturas.Rows[index].FindControl("imgBtnRef");
                    GridView gvDetalle = (GridView)e.Row.FindControl("gvDetalleFactura");
                    DropDownList ddlMtto = (DropDownList)e.Row.FindControl("ddlTipoMtto1");
                    TextBox txtObs = (TextBox)e.Row.FindControl("txtObservaciones");
                    ImageButton btnImg = (ImageButton)e.Row.FindControl("imgBtnAddCentroCostos");
                    CheckBox chkFac = (CheckBox)e.Row.FindControl("chkFactura");

                    if (dtTipoMtto != null && dtTipoMtto.Rows.Count > 0) 
                    {
                        ddlMtto.DataSource = dtTipoMtto;
                        ddlMtto.DataValueField = "Id_TipoMtto";
                        ddlMtto.DataTextField = "TipoMtto";
                        ddlMtto.DataBind();
                        ddlMtto.Items.Insert(0, new ListItem(".:Seleccione:.", ""));
                        ddlMtto.SelectedValue = "";
                    }

                    if (iEstatus == 1)
                    {
                        if (eSearchFacturasPart != null)
                            eSearchFacturasPart(null, null);

                        if (dtFacPart != null && dtFacPart.Rows.Count > 0)
                        {
                            gvDetalle.DataSource = dtFacPart;
                            gvDetalle.DataBind();
                        }
                    }
                    else if (iEstatus == 3) 
                    {
                        if (eSearchFacturasPart != null)
                            eSearchFacturasPart(null, null);

                        if (dtFacPart != null && dtFacPart.Rows.Count > 0)
                        {
                            gvDetalle.DataSource = dtFacPart;
                            gvDetalle.DataBind();
                        }
                        //Desactivar controles
                        int iMtto = dtResultFac.Rows[index]["TipoMtto"].S().I();
                        txtObs.Enabled = false;
                        btnImg.Enabled = false;
                        chkFac.Enabled = false;
                        chkFac.Checked = true;

                        if (iMtto > 0) 
                        {
                            ddlMtto.SelectedValue = iMtto.S();
                            ddlMtto.Enabled = false;
                        }
                    }
                    else if (iEstatus == 2) 
                    {
                        if (eSearchFacturasPart != null)
                            eSearchFacturasPart(null, null);

                        if (dtFacPart != null && dtFacPart.Rows.Count > 0)
                        {
                            gvDetalle.DataSource = dtFacPart;
                            gvDetalle.DataBind();
                        }
                        //Desactivar controles
                        int iMtto = dtResultFac.Rows[index]["TipoMtto"].S().I();
                        txtObs.Enabled = false;
                        btnImg.Enabled = false;
                        chkFac.Enabled = false;
                        chkFac.Checked = true;

                        if (iMtto > 0)
                        {
                            ddlMtto.SelectedValue = iMtto.S();
                            ddlMtto.Enabled = false;
                        }
                    }
                    else if (iEstatus == -1) 
                    {
                        if (eSearchFacturasPart != null)
                            eSearchFacturasPart(null, null);

                        if (dtFacPart != null && dtFacPart.Rows.Count > 0)
                        {
                            gvDetalle.DataSource = dtFacPart;
                            gvDetalle.DataBind();
                        }
                        //Desactivar controles
                        int iMtto = dtResultFac.Rows[index]["TipoMtto"].S().I();
                        txtObs.Enabled = false;
                        btnImg.Enabled = false;
                        chkFac.Enabled = false;
                        chkFac.Checked = true;

                        if (iMtto > 0)
                        {
                            ddlMtto.SelectedValue = iMtto.S();
                            ddlMtto.Enabled = false;
                        }
                        e.Row.BackColor = System.Drawing.Color.FromName("#FA5858");
                        e.Row.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvFacturas_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dataTable = dtResultFac;

                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);

                    if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                    {
                        dataView.Sort = e.SortExpression + " Desc";
                        ViewState["sortdr"] = "Desc";
                    }
                    else
                    {
                        dataView.Sort = e.SortExpression + " Asc";
                        ViewState["sortdr"] = "Asc";
                    }
                    gvFacturas.DataSource = dataView;
                    gvFacturas.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void gvDetalleFactura_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AgregarCCD")
                {
                    string sNumDocD = string.Empty;
                    int index = 0;
                    int indexD = e.CommandArgument.S().I();
                    index = hdnIndexGridCC.Value.S().I();

                    Label lblMatDefault = (Label)gvFacturas.Rows[index].FindControl("lblCentroCostos");
                    UpdatePanel upaDetalle = (UpdatePanel)gvFacturas.Rows[index].FindControl("upaDetFactura");
                    GridView gvDetalle = (GridView)gvFacturas.Rows[index].FindControl("gvDetalleFactura");

                    //sNumDocD = gvDetalle.Rows[indexD].Cells[0].Text;
                    iDocEntry = gvDetalle.DataKeys[indexD]["DocEntry"].S().I();
                    hdnIdDetalleCC.Value = gvDetalle.Rows[indexD].Cells[4].Text;
                    hdnDocEntryCC.Value = iDocEntry.S();
                    hdnNumDocCCD.Value = sNumDocD;
                    hdnIndexGridCC.Value = index.S();
                    hdnIndexGridCCD.Value = indexD.S();
                    hdnOpcion.Value = "2";

                    hdnMatriculaDefault.Value = lblMatDefault.Text;
                    LoadListFlotasModal();

                    if (!string.IsNullOrEmpty(hdnMatriculaDefault.Value))
                        ConsultaMatriculasParticiones();

                    //upaGridFacturas.Update();
                    //upaDetalle.Update();
                }
                else if (e.CommandName == "AgregarArticulo") 
                {
                    //ddlArticulo.Items.Clear();
                    txtImporteArticulo.Text = string.Empty;
                    int indexD = e.CommandArgument.S().I();
                    int index = hdnIndexGridCC.Value.S().I();
                    int iDocEntry = 0;

                    GridView gvDetalle = (GridView)gvFacturas.Rows[index].FindControl("gvDetalleFactura");
                    iDocEntry = gvDetalle.DataKeys[indexD]["DocEntry"].S().I();
                    hdnIdDetalle.Value = gvDetalle.Rows[indexD].Cells[4].Text;
                    hdnDocEntry.Value = iDocEntry.S();
                    hdnArticulo.Value = gvDetalle.Rows[indexD].Cells[0].Text;
                    hdnImporteArticulo.Value = gvDetalle.Rows[indexD].Cells[2].Text.Replace("$","");
                    hdnIndexGridArt.Value = indexD.S();

                    if (eSearchArticulos != null)
                        eSearchArticulos(sender, e);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void gvDetalleFactura_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //GridView gvDetalle = (GridView)e.Row.FindControl("gvDetalleFactura");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void gvMatriculas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgBtnAddCentroCostos_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeAddCC.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgBtnAddCentroCostosD_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeAddCC.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rdnLstFlota_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ddlCentroCostos.Items.Clear();
                //lstCCMultiple.Items.Clear();
                string sFiltro = string.Empty;

                sFiltro = rdnLstFlota.SelectedItem.Value;

                if (dtMergeMatriculas != null && dtMergeMatriculas.Rows.Count > 0)
                {
                    DataRow[] row = dtMergeMatriculas.Select("DesFlota='" + sFiltro + "'");

                    //ddlCentroCostos.Items.Insert(0, new ListItem("Seleccione", ""));
                    int icount = 0;
                    for (int i = 0; i < row.Length; i++)
                    {
                        icount += 1;
                        //lstCCMultiple.Items.Add(new ListItem(row[i]["Matricula"].S(), icount.S()));
                        //ddlCentroCostos.Items.Add(new ListItem(row[i]["Matricula"].S(), icount.S()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnCancelarCC_Click(object sender, EventArgs e)
        {
            //ddlCentroCostos.Items.Clear();
            //lstCCMultiple.Items.Clear();
            rdnLstFlota.Items.Clear();
        }
        public void btnAceptarCC_Click(object sender, EventArgs e)
        {
            try
            {
                string sNumDoc = string.Empty;
                int iIndex = 0;
                int indexG = 0;
                string sOpcion = string.Empty;
                sOpcion = hdnOpcion.Value;

                if (sOpcion == "1")
                {
                    sNumDoc = hdnNumDocCC.Value;
                    iIndex = hdnIndexGridCC.Value.I();
                    string sMatriculas = string.Empty;
                    string stxt = string.Empty;
                    Label lblCC = (Label)gvFacturas.Rows[iIndex].FindControl("lblCentroCostos");
                    lblCC.Text = string.Empty;

                    foreach (GridViewRow row in gvMatriculas.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chkMatricula");

                        if (chk != null)
                        {
                            if (chk.Checked)
                            {
                                int index = row.RowIndex;
                                sMatriculas = gvMatriculas.Rows[index].Cells[1].Text;
                                //stxt = sMatriculas;

                                if (string.IsNullOrEmpty(lblCC.Text))
                                    lblCC.Text = sMatriculas;
                                else
                                    lblCC.Text = lblCC.Text + ",<br/>" + sMatriculas;
                            }
                        }
                    }
                    string sConcatMatriculas = lblCC.Text;
                    rdnLstFlota.Items.Clear();
                    upaGridFacturas.Update();

                    

                    //GridView gvFac = (GridView)gvFacturas.Rows[iIndex].FindControl("gvFacturas");
                    

                    //if (lblCC != null)
                    //{
                    //    //lblCC.Text = ddlCentroCostos.SelectedItem.Text;
                    //    lblCC.Text = string.Empty;
                    //    foreach (ListItem listItem in lstCCMultiple.Items)
                    //    {
                    //        if (listItem.Selected)
                    //        {
                    //            string val = listItem.Value;
                    //            string txt = listItem.Text;

                    //            if (string.IsNullOrEmpty(lblCC.Text))
                    //            {
                    //                lblCC.Text = txt;
                    //            }
                    //            else 
                    //            {
                    //                lblCC.Text = lblCC.Text + ",<br/>" + txt;
                    //            }
                    //        }
                    //    }
                    //    string sConcatMatriculas = lblCC.Text;
                    //}
                    //rdnLstFlota.Items.Clear();
                    //upaGridFacturas.Update();
                }
                else if (sOpcion == "2")
                {
                    //Busqueda de index de gridview Factura
                    //String searchValue = hdnNumDocCC.Value.S();
                    //int rowIndex = -1;
                    //foreach (GridViewRow row in gvFacturas.Rows)
                    //{
                    //    if (row.Cells[2].Text.ToString().Equals(searchValue))
                    //    {
                    //        rowIndex = row.RowIndex;
                    //        break;
                    //    }
                    //}
                    iIdDetalle = hdnIdDetalleCC.Value.S().I();
                    iDocEntry = hdnDocEntryCC.Value.S().I();

                    sNumDoc = hdnNumDocCCD.Value;
                    indexG = hdnIndexGridCC.Value.I(); //rowIndex;
                    iIndex = hdnIndexGridCCD.Value.I();
                    GridView gvFac = (GridView)gvFacturas.Rows[indexG].FindControl("gvDetalleFactura");
                    Label lblCCD = (Label)gvFac.Rows[iIndex].FindControl("lblCentroCostosD");
                    //UpdatePanel upaDet = (UpdatePanel)gvFacturas.Rows[iIndex].FindControl("upaDetFactura");
                    
                    //if (upaDet != null)
                    //    upaDet.Update();


                    if (lblCCD != null)
                    {
                        //lblCCD.Text = ddlCentroCostos.SelectedItem.Text;
                    }
                    //upaDetFactura.Update();
                }

                //upaGridFacturas.Update();
                mpeAddCC.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardarFacturas_Click(object sender, EventArgs e)
        {
            try
            {
                RecorrerFacturas();
                dtFacturas = null;

                sNumDoc = txtNoDocumento.Text;
                sFechaDesde = txtDe.Text.Dt().ToString("yyyy-MM-dd");
                sFechaHasta = txtHasta.Text.Dt().ToString("yyyy-MM-dd");
                //sEstatus = ddlEstatus.SelectedItem.Value;
                sTipoMtto = ddlTipoMtto.SelectedItem.Value;
                sFlota = ddlFlota.SelectedItem.Value;

                sEstatus = "1";
                ddlEstatus.SelectedIndex = 1;

                if (sFechaDesde == "0001-01-01")
                    sFechaDesde = string.Empty;
                if (sFechaHasta == "0001-01-01")
                    sFechaHasta = string.Empty;

                if (eSearchFacturas != null)
                    eSearchFacturas(null, null);
                
                upaGridFacturas.Update();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlEstatus.SelectedItem.Text == ".:Selecciona:.")
                {
                    ddlTipoMtto.Enabled = true;
                    ddlFlota.Enabled = true;
                    ddlTipoMtto.SelectedIndex = -1;
                    ddlFlota.SelectedIndex = -1;
                    btnGuardarFacturas.Enabled = false;
                }
                if (ddlEstatus.SelectedValue == "1")
                {
                    ddlTipoMtto.Enabled = false;
                    ddlFlota.Enabled = false;
                    ddlTipoMtto.SelectedIndex = -1;
                    ddlFlota.SelectedIndex = -1;
                    btnGuardarFacturas.Enabled = true;
                }
                if (ddlEstatus.SelectedValue == "2")
                {
                    ddlEstatus.Enabled = true;
                    ddlTipoMtto.Enabled = true;
                    ddlFlota.Enabled = true;
                    btnGuardarFacturas.Enabled = false;
                }
                if (ddlEstatus.SelectedValue == "3")
                {
                    ddlEstatus.Enabled = true;
                    ddlTipoMtto.Enabled = true;
                    ddlFlota.Enabled = true;
                    btnGuardarFacturas.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void imgBtnRef_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int index = 0;
                index = hdnIndexGridCC.Value.S().I();
                sNumDoc = gvFacturas.Rows[index].Cells[2].Text;
                int iEstatus = gvFacturas.Rows[index].Cells[9].Text.S().I();

                ImageButton imgBtnDet = (ImageButton)gvFacturas.Rows[index].FindControl("imgBtnRef");
                GridView gvDetalle = (GridView)gvFacturas.Rows[index].FindControl("gvDetalleFactura");

                if (iEstatus == 1) 
                {
                    //ImageButton imgBtnDet = (ImageButton)gvFacturas.Rows[index].FindControl("imgBtnRef");
                    //GridView gvDetalle = (GridView)gvFacturas.Rows[index].FindControl("gvDetalleFactura");

                    if (eSearchFacturasPart != null)
                        eSearchFacturasPart(null, null);

                    if (dtFacPart != null && dtFacPart.Rows.Count > 0)
                    {
                        gvDetalle.DataSource = dtFacPart;
                        gvDetalle.DataBind();

                        //for (int i = 0; i < gvDetalle.Rows.Count; i++)
                        //{
                        //    int indexD = gvDetalle.Rows.Count - 1;
                        //    ImageButton btnEliminarCC = (ImageButton)gvDetalle.Rows[indexD].FindControl("btnEliminar");
                        //    ImageButton btnAddCCD = (ImageButton)gvDetalle.Rows[indexD].FindControl("imgBtnAddCentroCostosD");
                        //    string sCCD = gvDetalle.Rows[indexD].Cells[4].Text;

                        //    btnEliminarCC.Enabled = false;
                        //    btnAddCCD.Enabled = false;
                        //}
                    }
                }

                else if (iEstatus == 2 || iEstatus == 3)
                {
                    //ImageButton imgBtnDet = (ImageButton)gvFacturas.Rows[index].FindControl("imgBtnRef");
                    //GridView gvDetalle = (GridView)gvFacturas.Rows[index].FindControl("gvDetalleFactura");

                    if (eSearchFacturasPart != null)
                        eSearchFacturasPart(null, null);

                    if (dtFacPart != null && dtFacPart.Rows.Count > 0)
                    {
                        gvDetalle.DataSource = dtFacPart;
                        gvDetalle.DataBind();

                        for (int i = 0; i < gvDetalle.Rows.Count; i++)
                        {
                            int indexD = gvDetalle.Rows.Count - 1;
                            ImageButton btnEliminarCC = (ImageButton)gvDetalle.Rows[indexD].FindControl("btnEliminar");
                            ImageButton btnAddCCD = (ImageButton)gvDetalle.Rows[indexD].FindControl("imgBtnAddCentroCostosD");
                            string sCCD = gvDetalle.Rows[indexD].Cells[4].Text;

                            btnEliminarCC.Enabled = false;
                            btnAddCCD.Enabled = false;
                        }
                    }
                }
                //upaGridFacturas.Update();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnAgregarArticulo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeAgregarArticulo.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                iBanCorrecto = 0;
                iIdDetalle = hdnIdDetalle.Value.S().I();
                iDocEntry = hdnDocEntry.Value.S().I();
                sItemCode = hdnArticuloValor.Value.S();
                sArticulo = hdnArticuloTexto.Value.S().ToUpper();
                sNvoImporte = txtImporteArticulo.Text;

                if (eProcesarArticulos != null)
                    eProcesarArticulos(sender, e);

                if (iBanCorrecto == 1)
                {
                    Mensaje("Se agrego el articulo correctamente.");
                    mpeAgregarArticulo.Hide();
                }
                else
                {
                    Mensaje("No se pudo registar el articulo, favor de revisar.");
                    mpeAgregarArticulo.Hide();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void gvFacturas_PreRender(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnCancelarArticulo_Click(object sender, EventArgs e)
        {
            mpeAgregarArticulo.Hide();
        }
        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                dtSRCDatosExcel = null;
                dtSRCDatosExcel = ObtenerOrigenDatos();
                ExportGridToExcel(dtSRCDatosExcel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        #endregion

        #region MÉTODOS

        public DataTable ObtenerOrigenDatos() 
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dtResultFac;
                dt.Columns.Remove("DocEntry");
                dt.Columns.Remove("Estatus");
                dt.Columns.Remove("TipoMtto");
                dt.Columns.Remove("TipoMtto2");
                dt.Columns.Remove("Flota1");
                dt.Columns.Remove("TaxCode");
                dt.AcceptChanges();

                dt.Columns["DocNum"].ColumnName = "Documento";
                dt.Columns["NumAtCard"].ColumnName = "Orden de Trabajo";
                dt.Columns["DocDate"].ColumnName = "Fecha Factura";
                dt.Columns["DocCur"].ColumnName = "Moneda";
                dt.Columns["DocRate"].ColumnName = "Tipo de Cambio";
                dt.Columns["ContDate"].ColumnName = "Fecha Contable";
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex)
            {    
                throw ex;
            }
        }

        private void ExportGridToExcel(DataTable dt)
        {
            string sDia = DateTime.Now.Day.S();
            string sMes = DateTime.Now.Month.S();
            string sAnio = DateTime.Now.Year.S();
            string sNameReport = sDia.PadLeft(2, '0') + sMes.PadLeft(2, '0') + sAnio;

            XLWorkbook wb = new XLWorkbook();

            if (pnlFacturasEncontradas.Visible == true)
            {
                DataTable dt1 = new DataTable();
                dt1 = dt;
                wb.Worksheets.Add(dt1, "FacturasGenerales");
            }
            //if (pnlFacturasLicitacion.Visible == true)
            //{
            //    DataTable dt2 = new DataTable();
            //    dt2 = dtFacturasLic;
            //    wb.Worksheets.Add(dt2, "FacturasLicitacion");
            //}

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "UTF-8";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Reporte_" + sNameReport + ".xls");
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }  

        public void LoadTipoMtto(DataTable dt)
        {
            try
            {
                dtTipoMtto = null;
                dtTipoMtto = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadListaArticulos(DataTable dt) 
        {
            try
            {
                dtArticulos = null;
                dtArticulos = dt;

                //if (dtArticulos != null && dtArticulos.Rows.Count > 0)
                //{
                //    ddlArticulo.Items.Insert(0, new ListItem("Seleccione", ""));

                //    for (int i = 0; i < dtArticulos.Rows.Count; i++)
                //    {
                //        ddlArticulo.Items.Add(new ListItem(dtArticulos.Rows[i]["Concepto"].S(), dtArticulos.Rows[i]["CodigoArticulo"].S()));
                //    }
                //}

                //ddlArticulo.DataSource = dtArticulos;
                //ddlArticulo.DataValueField = "CodigoArticulo";
                //ddlArticulo.DataTextField = "Concepto";
                //ddlArticulo.DataBind();
                //ddlArticulo.Items.Insert(0, new ListItem("Seleccione", ""));

                for (int i = 0; i < dtArticulos.Rows.Count; i++)
                {
                    ddlArticulo.Items.Add(new ListItem(dtArticulos.Rows[i]["Concepto"].S(), dtArticulos.Rows[i]["CodigoArticulo"].S()));
                }
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione", ""));
                //ddlArticulo.SelectedItem.Text = dtArticulos.Rows[0][0].S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ConsultaMatriculaPrincipal()
        {
            try
            {
                if (!string.IsNullOrEmpty(hdnMatriculaDefault.Value))
                {
                    #region Llenado Principal de Matriculas
                    string sFiltro = string.Empty;
                    //ddlCentroCostos.Items.Clear();
                    sFiltro = hdnMatriculaDefault.Value;

                    if (dtMergeMatriculas != null && dtMergeMatriculas.Rows.Count > 0)
                    {
                        DataRow[] row = dtMergeMatriculas.Select("Matricula='" + sFiltro + "'");

                        if (row.Length == 1)
                        {
                            string sFlota = string.Empty;

                            if (row[0][1].S() == "ALE")
                            {
                                rdnLstFlota.SelectedValue = "ALE";
                                sFlota = "ALE";
                                rdnLstFlota.Enabled = false;
                            }
                            else if (row[0][1].S() == "MEX" || row[0][1].S() == "OPE")
                            {
                                rdnLstFlota.SelectedValue = row[0][1].S();
                                sFlota = row[0][1].S();
                                rdnLstFlota.Enabled = false;
                            }

                            DataRow[] rowMat;
                            DataTable dtMatriculasSRC = new DataTable();
                            //dtMatriculasSRC = null;
                            dtMatriculasSRC.Columns.Add("Matricula");
                            dtMatriculasSRC.Columns.Add("DesFlota");

                            if (sFlota == "ALE")
                                rowMat = dtMergeMatriculas.Select("DesFlota='" + sFlota + "'");
                            else
                                rowMat = dtMergeMatriculas.Select("DesFlota IN('" + sFlota + "','OPE')");
                                

                            int icount = 0;

                            for (int i = 0; i < rowMat.Length; i++)
                            {
                                icount += 1;
                                DataRow rowCC = dtMatriculasSRC.NewRow();
                                rowCC["Matricula"] = rowMat[i]["Matricula"].S();
                                rowCC["DesFlota"] = rowMat[i]["DesFlota"].S();
                                dtMatriculasSRC.Rows.Add(rowCC);
                            }
                            dtMatriculasSRC.AcceptChanges();
                            gvMatriculas.DataSource = dtMatriculasSRC;
                            gvMatriculas.DataBind();
                        }
                        else 
                        {
                            if (sFiltro == "MEX-JET") 
                            {
                                rdnLstFlota.SelectedValue = "MEX";
                                sFlota = "MEX";
                                rdnLstFlota.Enabled = false;
                                int icount = 0;

                                DataRow[] rowMat;
                                DataTable dtMatriculasSRC = new DataTable();
                                dtMatriculasSRC.Columns.Add("Matricula");
                                dtMatriculasSRC.Columns.Add("DesFlota");
                                //dtMatriculasSRC = null;

                                rowMat = dtMergeMatriculas.Select("DesFlota IN('" + sFlota + "','OPE')");

                                for (int i = 0; i < rowMat.Length; i++)
                                {
                                    icount += 1;
                                    DataRow rowCC = dtMatriculasSRC.NewRow();
                                    rowCC["Matricula"] = rowMat[i]["Matricula"].S();
                                    rowCC["DesFlota"] = rowMat[i]["DesFlota"].S();
                                    dtMatriculasSRC.Rows.Add(rowCC);
                                }
                                dtMatriculasSRC.AcceptChanges();
                                gvMatriculas.DataSource = dtMatriculasSRC;
                                gvMatriculas.DataBind();
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ConsultaMatriculasParticiones()
        {
            try
            {
                #region Llenado Secundario de Matriculas para particiones

                string sFiltro = hdnMatriculaDefault.Value;
                //ddlCentroCostos.Items.Clear();

                if (dtMergeMatriculas != null && dtMergeMatriculas.Rows.Count > 0)
                {
                    DataRow[] row = dtMergeMatriculas.Select("Matricula='" + sFiltro + "'");

                    if (row.Length == 1)
                    {
                        string sFlota = string.Empty;

                        if (row[0][1].S() == "ALE")
                        {
                            rdnLstFlota.SelectedValue = "ALE";
                            sFlota = "ALE";
                        }
                        else if (row[0][1].S() == "MEX" || row[0][1].S() == "OPE")
                        {
                            rdnLstFlota.SelectedValue = row[0][1].S();
                            sFlota = row[0][1].S();
                        }

                        DataRow[] rowMat;

                        if (sFlota == "ALE")
                            rowMat = dtMergeMatriculas.Select("DesFlota='" + sFlota + "'");
                        else
                            rowMat = dtMergeMatriculas.Select("DesFlota IN('" + sFlota + "','OPE')");
                            

                        int icount = 0;

                        for (int i = 0; i < rowMat.Length; i++)
                        {
                            icount += 1;
                            //ddlCentroCostos.Items.Add(new ListItem(rowMat[i]["Matricula"].S(), icount.S()));
                        }
                        //ddlCentroCostos.Items.Insert(0, new ListItem("Seleccione", ""));
                        //ddlCentroCostos.SelectedIndex = 0;

                    }
                }
                //rdnLstFlota_SelectedIndexChanged(null, null);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ParticionarFactura(int indexG, string sNumDoc, string sImporte, int iPart)
        {
            try
            {
                double dbDivide = 0;
                double dbImporte = 0;
                DataTable dtDetalle = new DataTable();
                dtDetalle.Columns.Add("NumDocumento");
                dtDetalle.Columns.Add("Importe");
                dtDetalle.Columns.Add("DocNum");
                dtDetalle.Columns.Add("CentroCostosD");

                if (!string.IsNullOrEmpty(sImporte))
                    dbImporte = sImporte.Replace("$","").Db();

                if (dbImporte > 0)
                    dbDivide = dbImporte / iPart;

                for (int i = 0; i < iPart; i++)
                {
                    DataRow row = dtDetalle.NewRow();
                    row["NumDocumento"] = sNumDoc + "_" + (i + 1);
                    row["Importe"] = String.Format("{0:C}", dbDivide);
                    row["DocNum"] = sNumDoc;
                    row["CentroCostosD"] = string.Empty;
                    dtDetalle.Rows.Add(row);
                }
                dtDetalle.AcceptChanges();

                GridView gvDetalle = (GridView)gvFacturas.Rows[indexG].FindControl("gvDetalleFactura");
                if (gvDetalle != null)
                {
                    gvDetalle.DataSource = dtDetalle;
                    gvDetalle.DataBind();
                }
                //upaGridFacturas.Update();
                mpeParticionar.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadFacturas(DataTable dtFacturas)
        {
            try
            {
                dtResultFac = null;
                dtResultFac = dtFacturas;

                if(dtFacturas != null && dtFacturas.Rows.Count > 0)
                    btnGuardarFacturas.Visible = true;
                else
                    btnGuardarFacturas.Visible = false;

                if (ddlEstatus.SelectedValue == "2" || ddlEstatus.SelectedValue == "3")
                {
                    btnGuardarFacturas.Enabled = false;
                }
                else
                {
                    if (dtFacturas != null && dtFacturas.Rows.Count > 0)
                    {
                        btnGuardarFacturas.Enabled = true;
                        btnGuardarFacturas.Visible = true;
                    }
                }


                gvFacturas.DataSource = dtResultFac;
                gvFacturas.DataBind();
                ControlCheckGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ControlCheckGrid()
        {
            try
            {
                foreach (GridViewRow row in gvFacturas.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkFactura");
                    //RadioButtonList rdnL = (RadioButtonList)row.FindControl("rdnLstTipoMtto");
                    //RadioButtonList rdnL2 = (RadioButtonList)row.FindControl("rdnLstTipoMtto2");
                    DropDownList rdnL = (DropDownList)row.FindControl("ddlTipoMtto1");
                    TextBox txt = (TextBox)row.FindControl("txtObservaciones");
                    //ImageButton imgBtn = (ImageButton)row.FindControl("btnParticionar");
                    ImageButton imgBtn2 = (ImageButton)row.FindControl("imgBtnAddCentroCostos");

                    int iEstatus = gvFacturas.Rows[row.RowIndex].Cells[11].Text.S().I();
                    int iTipoMtto = gvFacturas.Rows[row.RowIndex].Cells[12].Text.S().I();
                    //int iTipoMtto2 = gvFacturas.Rows[row.RowIndex].Cells[13].Text.S().I();
                    //string sObs = gvFacturas.Rows[row.RowIndex].Cells[8].Text.S();

                    if (iEstatus == 2 || iEstatus == 3)
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                        //imgBtn.Enabled = false;
                        imgBtn2.Enabled = false;
                        txt.Enabled = false;
                        rdnL.Enabled = false;

                        if (iTipoMtto == 1)
                        {
                            rdnL.SelectedValue = "1"; //Preventivo
                            rdnL.Enabled = false;
                        }
                        else if (iTipoMtto == 2)
                        {
                            rdnL.SelectedValue = "2"; //Correctivo
                            rdnL.Enabled = false;
                        }

                        if (iTipoMtto == 3)
                        {
                            rdnL.SelectedValue = "3"; //Reserva de Motores
                            rdnL.Enabled = false;
                        }
                        else if (iTipoMtto == 4)
                        {
                            rdnL.SelectedValue = "4"; //Reserva de Interiores
                            rdnL.Enabled = false;
                        }
                    }

                    if (iEstatus == -1)
                    {
 
                    }


                    //txt.Text = sObs;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable ObtenerMatchMatriculas()
        {
            try
            {
                //DataTable dtMatriculasCla = new DataTable();
                //dtMatriculasCla.Columns.Add("Matricula");
                //dtMatriculasCla.Columns.Add("DesFlota");

                if (eSearchMatriculas != null)
                    eSearchMatriculas(null, null);



                //if (eSearchMatriculasSAP != null)
                //    eSearchMatriculasSAP(null, null);

                //if (eSearchMatriculasMXJ != null)
                //    eSearchMatriculasMXJ(null, null);

                //Matriculas 223/224


                //Clasificación de Matriculas
                #region Clasificacion de Matriculas
                //if (dtMatriculasSAP != null && dtMatriculasMXJ != null) 
                //{
                //    if (dtMatriculasSAP.Rows.Count > 0 && dtMatriculasMXJ.Rows.Count > 0) 
                //    {
                //        for (int i = 0; i < dtMatriculasSAP.Rows.Count; i++)
                //        {
                //            DataRow row = dtMatriculasCla.NewRow();
                //            DataRow[] rowMat = dtMatriculasMXJ.Select("Matricula = '" + dtMatriculasSAP.Rows[i]["OcrCode"].S() + "'");

                //            //for (int x = 0; x < dtMatriculasMXJ.Rows.Count; x++){ }

                //            if (rowMat.Length == 1) 
                //            {
                //                row["Matricula"] = rowMat[0]["Matricula"];
                //                row["DesFlota"] = rowMat[0]["DescripcionFlota"];
                //                dtMatriculasCla.Rows.Add(row);
                //            }
                //            else if (rowMat.Length == 0) 
                //            {
                //                row["Matricula"] = dtMatriculasSAP.Rows[i]["OcrCode"].S();
                //                row["DesFlota"] = "Otros";
                //                dtMatriculasCla.Rows.Add(row);
                //            }
                //        }
                //        dtMatriculasCla.AcceptChanges();
                //    }
                //}
                #endregion


                return dtMainMatriculas;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void LoadMatriculas(DataTable dtMatriculas)
        {
            try
            {
                dtMainMatriculas = null;
                dtMainMatriculas = dtMatriculas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadMatriculasSAP(DataTable dtMatriculas) 
        {
            try
            {
                dtMatriculasSAP = null;
                dtMatriculasSAP = dtMatriculas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadMatriculasMXJ(DataTable dtMatriculas) 
        {
            try
            {
                dtMatriculasMXJ = null;
                dtMatriculasMXJ = dtMatriculas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadFlotas(DataTable dtFlotas) 
        {
            try
            {
                dtFlotasMXJ = null;
                dtFlotasMXJ = dtFlotas;

                //if (dtFlotasMXJ != null && dtFlotasMXJ.Rows.Count > 0) 
                //{
                //    int icount = 0;
                //    ddlFlota.Items.Insert(0, new ListItem("Seleccione", ""));

                //    for (int i = 0; i < dtFlotasMXJ.Rows.Count; i++)
                //    {
                //        ddlFlota.Items.Add(new ListItem(dtFlotasMXJ.Rows[i]["DescripcionFlota"].S(), dtFlotasMXJ.Rows[i]["IdFlota"].S()));
                //        icount += 1;
                //    }
                //    ddlFlota.Items.Insert(icount, new ListItem("OTROS", icount.S()));
                //}
                ddlFlota.Items.Clear();
                ddlFlota.Items.Insert(0, new ListItem("Seleccione", ""));
                ddlFlota.Items.Add(new ListItem("CLIENTES CASA", "ALE")); //También abarca OPE las cuales son de clientes casa
                ddlFlota.Items.Add(new ListItem("MEXJET", "MEX"));

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadListFlotasModal() 
        {
            try
            {
                //if (dtFlotasMXJ != null) 
                //{
                //    int icount = 0;

                //    for (int i = 0; i < dtFlotasMXJ.Rows.Count; i++)
                //    {
                //        rdnLstFlota.Items.Add(new ListItem(dtFlotasMXJ.Rows[i]["DescripcionFlota"].S(), dtFlotasMXJ.Rows[i]["IdFlota"].S()));
                //        icount += 1;
                //    }
                //    rdnLstFlota.Items.Insert(icount, new ListItem("OTROS", icount.S()));
                //}
                rdnLstFlota.Items.Clear();
                rdnLstFlota.Items.Add(new ListItem("CLIENTES CASA", "ALE"));
                rdnLstFlota.Items.Add(new ListItem("MEXJET", "MEX"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadFacturasPart(DataTable dtPart)
        {
            try
            {
                dtFacPart = null;
                dtFacPart = dtPart;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void RecorrerFacturas() 
        {
            try
            {
                iBanCorrecto = 0;
                List<FacturaASC> oLsFacturas = new List<FacturaASC>();
                bool banValidar = false;
                bool banValidCC = false;
                int banddl = 0;

                foreach (GridViewRow row in gvFacturas.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkFactura");
                    DropDownList rdnLst = (DropDownList)row.FindControl("ddlTipoMtto1");
                    //RadioButtonList rdnLst2 = (RadioButtonList)row.FindControl("rdnLstTipoMtto2");
                    //TextBox txtFechConta = (TextBox)row.FindControl("txtFechaConta");
                    TextBox txt = (TextBox)row.FindControl("txtObservaciones");
                    Label lbl = (Label)row.FindControl("lblCentroCostos");
                    string sFlota = string.Empty;
                    string sMatriculas = string.Empty;
                    int sArrCount = 0;

                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            if (rdnLst.SelectedIndex > -1)
                            {
                                #region CONTENIDO PRINCIPAL DE ENVIO DE FACTURAS

                                int index = row.RowIndex;
                                string sNumDocumento = string.Empty;
                                string sTaxCode = string.Empty;
                                decimal dResultImporte = 0;
                                decimal dImporteHead = 0;
                                double dPorcentaje = 0;
                                string sImporteTotal = string.Empty;
                                //string sFechaContable = string.Empty;

                                sNumDocumento = gvFacturas.Rows[index].Cells[13].Text;
                                sTaxCode = gvFacturas.Rows[index].Cells[14].Text;
                                dImporteHead = gvFacturas.Rows[index].Cells[5].Text.Replace("$", "").S().D();
                                sImporteTotal = gvFacturas.Rows[index].Cells[5].Text.Replace("$", "").S();
                                //sFechaContable = txtFechConta.Text;

                                if (!string.IsNullOrEmpty(sTaxCode))
                                {
                                    if (sTaxCode == "IVAT16")
                                    {
                                        dPorcentaje = double.Parse(sImporteTotal) / 1.16;
                                        dImporteHead = dPorcentaje.S().D();
                                    }
                                    else if (sTaxCode == "IVAT00")
                                    {
                                        dImporteHead = sImporteTotal.D();
                                    }
                                }

                                FacturaASC oF = new FacturaASC();
                                oF.sNumDoc = sNumDocumento;
                                oF.sNoFactura = gvFacturas.Rows[index].Cells[2].Text;
                                oF.sCentroCostos = lbl.Text.Replace("<br/>", "");
                                //oF.dtFechaDoc = gvFacturas.Rows[index].Cells[4].Text.Dt();
                                oF.dImporte = dImporteHead;

                                if (!string.IsNullOrEmpty(rdnLst.SelectedItem.Value))
                                    oF.iTipoMtto = rdnLst.SelectedItem.Value.I();
                                else
                                    oF.iTipoMtto = 0;

                                oF.iTipoMtto2 = 0;

                                oF.sObservaciones = txt.Text;
                                oF.iStatus = 2; //Actualiza a estatus Enviado
                                oF.sUser = Utils.GetUser;
                                //oF.dtFechaCont = sFechaContable.Dt();

                                //Agregar Detalle de Factura
                                GridView gvDetalle = (GridView)gvFacturas.Rows[index].FindControl("gvDetalleFactura");
                                int iRowCount = 0;

                                if (gvDetalle != null)
                                    iRowCount = gvDetalle.Rows.Count;

                                if (!string.IsNullOrEmpty(lbl.Text))
                                {
                                    String[] sArrMatriculas = lbl.Text.Replace("<br/>", "").Split(',');

                                    if (sArrMatriculas.Length > 0)
                                    {
                                        sArrCount = sArrMatriculas.Length;

                                        if (sArrCount > 0)
                                        {
                                            for (int i = 0; i < sArrCount; i++)
                                            {
                                                string sCentroCosto = sArrMatriculas[i].S();

                                                foreach (GridViewRow rowD in gvDetalle.Rows)
                                                {
                                                    int indexD = rowD.RowIndex;
                                                    int DocEntry = 0;
                                                    string sArticulo = string.Empty;
                                                    int iTipoMttoD = 0;

                                                    string lblCC = sCentroCosto; //(Label)rowD.FindControl("lblCentroCostosD");
                                                    DocEntry = gvDetalle.DataKeys[indexD]["DocEntry"].S().I();
                                                    sArticulo = gvDetalle.Rows[indexD].Cells[0].Text;

                                                    //Importe de prorroteo
                                                    dResultImporte = dImporteHead / sArrCount;

                                                    ParticionesFacturaASC oP = new ParticionesFacturaASC();
                                                    oP.iIDocEntry = DocEntry;
                                                    oP.iDetalle = gvDetalle.Rows[indexD].Cells[3].Text.S().I();
                                                    oP.sNumDoc = sNumDocumento;
                                                    oP.sNumDocD = sArticulo;

                                                    if (rdnLst.SelectedItem.Value == "3")
                                                        oP.sItemCode = "BAL03";
                                                    else if (rdnLst.SelectedItem.Value == "4")
                                                        oP.sItemCode = "BAL04";
                                                    else if (rdnLst.SelectedItem.Value == "1")
                                                    {
                                                        DataTable dtCodArt = new DataTable();
                                                        iTipoMttoD = 0;
                                                        iTipoMttoD = rdnLst.SelectedItem.Value.S().I();
                                                        dtCodArt = new DBEnvioFacturas().DBGetObtieneCodigoArticulo(sArticulo, iTipoMttoD);
                                                        if (dtCodArt != null && dtCodArt.Rows.Count > 0)
                                                            oP.sItemCode = dtCodArt.Rows[0]["CodigoArticulo"].S();
                                                    }
                                                    else if (rdnLst.SelectedItem.Value == "2")
                                                    {
                                                        DataTable dtCodArt = new DataTable();
                                                        iTipoMttoD = 0;
                                                        iTipoMttoD = rdnLst.SelectedItem.Value.S().I();
                                                        dtCodArt = new DBEnvioFacturas().DBGetObtieneCodigoArticulo(sArticulo, iTipoMttoD);
                                                        if (dtCodArt != null && dtCodArt.Rows.Count > 0)
                                                            oP.sItemCode = dtCodArt.Rows[0]["CodigoArticulo"].S();
                                                    }
                                                    else
                                                        oP.sItemCode = "";

                                                    oP.sCentroCostos = lblCC;

                                                    if (sArrMatriculas.Length > 1)
                                                        oP.dImporte = dResultImporte;
                                                    else if (sArrMatriculas.Length == 1)
                                                        oP.dImporte = gvDetalle.Rows[indexD].Cells[4].Text.D();
                                                    
                                                    oP.sObservaciones = "";
                                                    oP.iStatus = 1;
                                                    oP.sUser = Utils.GetUser;

                                                    if (!string.IsNullOrEmpty(lblCC))
                                                    {
                                                        oF.oLstConceptos.Add(oP);
                                                        banValidCC = true;
                                                    }
                                                    else
                                                    {
                                                        Mensaje("*Nota: Debe asignar al menos un centro de costos para cada factura a enviar.");
                                                        banValidCC = false;
                                                        break;
                                                    }
                                                }
                                                
                                            }
                                            oLsFacturas.Add(oF);
                                        }
                                    }
                                }

                                
                                #endregion
                                banValidar = true;
                            }
                            else
                            {
                                banValidar = false;
                                break;
                            }

                            if (rdnLst.SelectedValue == "")
                            {
                                banddl = -1;
                                break;
                            }
                            else
                                banddl = 1;
                        }
                    }
                }

                if (banddl == 1)
                {
                    if (banValidar == true && banValidCC == true)
                    {
                        ListaFacturas = oLsFacturas;

                        if (ListaFacturas.Count > 0)
                        {
                            if (eProcesarFacturas != null)
                                eProcesarFacturas(null, null);

                            if (iBanCorrecto == 1)
                            {
                                Mensaje("Se han enviado las facturas a contabilización");
                            }
                        }
                    }
                }
                else if (banddl == -1)
                {
                    Mensaje("*Debe seleccionar el tipo de matenimiento de cada factura a enviar.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error: " + ex.Message);
                throw ex;
            }
        }

        protected DataTable dtFacturasSRC() 
        {
            try
            {
                DataTable dtSRC = new DataTable();
                dtSRC.Columns.Add("Matricula");
                dtSRC.Columns.Add("DesFlota");
                return dtSRC;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Mensaje(string sMensaje)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }

        public string GetFormatoFecha(string sFecha)
        {
            try
            {
                string sDia = string.Empty;
                string sMes = string.Empty;
                string sAnio = string.Empty;
                DateTime sFec;
                string[] arrFechaDoc = sFecha.Split(' ');
                string sFechaResult = string.Empty;

                sDia = arrFechaDoc[0].S();
                sAnio = arrFechaDoc[2].S();
                sMes = arrFechaDoc[1].S();

                if (sMes == "Ene")
                    sMes = "01";
                if (sMes == "Feb")
                    sMes = "02";
                if (sMes == "Mar")
                    sMes = "03";
                if (sMes == "Abr")
                    sMes = "04";
                if (sMes == "May")
                    sMes = "05";
                if (sMes == "Jun")
                    sMes = "06";
                if (sMes == "Jul")
                    sMes = "07";
                if (sMes == "Ago")
                    sMes = "08";
                if (sMes == "Sep")
                    sMes = "09";
                if (sMes == "Oct")
                    sMes = "10";
                if (sMes == "Nov")
                    sMes = "11";
                if (sMes == "Dic")
                    sMes = "12";

                if (sDia == "1" || sDia == "2" || sDia == "3" || sDia == "4" || sDia == "5" || sDia == "6" || sDia == "7" || sDia == "8" || sDia == "9")
                    sDia = "0" + sDia;

                if (sMes == "1" || sMes == "2" || sMes == "3" || sMes == "4" || sMes == "5" || sMes == "6" || sMes == "7" || sMes == "8" || sMes == "9")
                    sMes = "0" + sMes;

                sFecha = sDia.S() + "/" + sMes.S() + "/" + sAnio.S();
                return sFecha;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmEnvioFacturas.aspx.cs";
        private const string sPagina = "frmEnvioFacturas.aspx";

        EnvioFacturas_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchFacturas;
        public event EventHandler eSearchMatriculasSAP;
        public event EventHandler eSearchMatriculasMXJ;
        public event EventHandler eSearchFlotasMXJ;
        public event EventHandler eSearchMatriculas;
        public event EventHandler eProcesarFacturas;
        public event EventHandler eSearchFacturasPart;
        public event EventHandler eSearchArticulos;
        public event EventHandler eProcesarArticulos;
        public event EventHandler eGetValidaPDF;
        public event EventHandler eSearchTipoMtto;

        public string sNumDoc
        {
            get { return (string)ViewState["VSNumdoc"]; }
            set { ViewState["VSNumdoc"] = value; }
        }
        public string sFechaDesde
        {
            get { return (string)ViewState["VSFechaDe"]; }
            set { ViewState["VSFechaDe"] = value; }
        }
        public string sFechaHasta
        {
            get { return (string)ViewState["VSFechaHasta"]; }
            set { ViewState["VSFechaHasta"] = value; }
        }
        public string sEstatus 
        {
            get { return (string)ViewState["VSEstatus"]; }
            set { ViewState["VSEstatus"] = value; }
        }
        public string sTipoMtto
        {
            get { return (string)ViewState["VSTipoMtto"]; }
            set { ViewState["VSTipoMtto"] = value; }
        }
        public string sFlota
        {
            get { return (string)ViewState["VSFlota"]; }
            set { ViewState["VSFlota"] = value; }
        }
        public string sMatricula
        {
            get { return (string)ViewState["VSMatricula"]; }
            set { ViewState["VSMatricula"] = value; }
        }


        //Variables para Agregar Nuevo articulo
        public int iIdDetalle
        {
            get { return (int)ViewState["VSIdDetalle"]; }
            set { ViewState["VSIdDetalle"] = value; }
        }
        public int iDocEntry
        {
            get { return (int)ViewState["VSDocEntry"]; }
            set { ViewState["VSDocEntry"] = value; }
        }
        public string sItemCode
        {
            get { return (string)ViewState["VSItemCode"]; }
            set { ViewState["VSItemCode"] = value; }
        }
        public string sArticulo
        {
            get { return (string)ViewState["VSArticulo"]; }
            set { ViewState["VSArticulo"] = value; }
        }
        public string sNvoImporte
        {
            get { return (string)ViewState["VSNvoImporte"]; }
            set { ViewState["VSNvoImporte"] = value; }
        }

        //Para armar ruta de documento
        public string sCadArchivo
        {
            get { return (string)ViewState["VSValidaArchivo"]; }
            set { ViewState["VSValidaArchivo"] = value; }
        }
        public string sFechaDoc
        {
            get { return (string)ViewState["VSFechaDoc"]; }
            set { ViewState["VSFechaDoc"] = value; }
        }
        public string sArchivo
        {
            get { return (string)ViewState["VSArchivo"]; }
            set { ViewState["VSArchivo"] = value; }
        }

        public DataTable dtSRCDatosExcel
        {
            get { return (System.Data.DataTable)ViewState["VSSRCDatosExcel"]; }
            set { ViewState["VSSRCDatosExcel"] = value; }
        }

        public DataTable dtResultFac
        {
            get { return (System.Data.DataTable)ViewState["VSResultFac"]; }
            set { ViewState["VSResultFac"] = value; }
        }

        public DataTable dtFacturas
        {
            get { return (DataTable)ViewState["VSFac"]; }
            set { ViewState["VSFac"] = value; }
        }

        public DataTable dtFacParticiones
        {
            get { return (DataTable)ViewState["VSFacPart"]; }
            set { ViewState["VSFacPart"] = value; }
        }

        public string sImporte
        {
            get { return (string)ViewState["VSImporte"]; }
            set { ViewState["VSImporte"] = value; }
        }
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["VSMat"]; }
            set { ViewState["VSMat"] = value; }
        }

        public DataTable dtMatriculasSAP
        {
            get { return (DataTable)ViewState["VSMatSAP"]; }
            set { ViewState["VSMatSAP"] = value; }
        }

        public DataTable dtMatriculasMXJ
        {
            get { return (DataTable)ViewState["VSMatMXJ"]; }
            set { ViewState["VSMatMXJ"] = value; }
        }

        public DataTable dtFlotasMXJ
        {
            get { return (DataTable)ViewState["VSFlota"]; }
            set { ViewState["VSFlota"] = value; }
        }

        public DataTable dtMergeMatriculas
        {
            get { return (DataTable)ViewState["VSMerge"]; }
            set { ViewState["VSMerge"] = value; }
        }

        public DataTable dtMainMatriculas
        {
            get { return (DataTable)ViewState["VSMainMat"]; }
            set { ViewState["VSMainMat"] = value; }
        }

        public DataTable dtFacProcesar
        {
            get { return (System.Data.DataTable)ViewState["VSFacProcesar"]; }
            set { ViewState["VSFacProcesar"] = value; }
        }

        public List<FacturaASC> ListaFacturas
        {
            set { ViewState["VSListaFacturas"] = value; }
            get { return (List<FacturaASC>)ViewState["VSListaFacturas"]; }
        }

        //Bandera para saber si se insertó correctamente
        public int iBanCorrecto
        {
            get { return (int)ViewState["VSCorrecto"]; }
            set { ViewState["VSCorrecto"] = value; }
        }

        public DataTable dtFacPart
        {
            get { return (System.Data.DataTable)ViewState["VSFacP"]; }
            set { ViewState["VSFacP"] = value; }
        }

        public DataTable dtArticulos
        {
            get { return (System.Data.DataTable)ViewState["VSArticulos"]; }
            set { ViewState["VSArticulos"] = value; }
        }

        public DataTable dtColeccionArt
        {
            get { return (System.Data.DataTable)ViewState["VSColeccionArt"]; }
            set { ViewState["VSColeccionArt"] = value; }
        }

        public DataTable dtTipoMtto
        {
            get { return (DataTable)ViewState["VSMtto"]; }
            set { ViewState["VSMtto"] = value; }
        }

        #endregion

    }
}