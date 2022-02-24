using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using AjaxControlToolkit;
using ClientesCasa.Clases;
using ClientesCasa.Objetos;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ClientesCasa.Views.Gastos
{
    public partial class frmMantenimientoUsd : System.Web.UI.Page, IViewMantenimiento
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            //Utils.GuardarBitacora("MANTTO_DATOS  --> ** INICIO Load   -------------------------------------------------");
            //se inicia el presentador
            oPresenter = new Mantenimiento_Presenter(this, new DBMantenimiento());

            if (!IsPostBack)
            {
                //txtTrioUSA.Attributes["onfocus"] = "javascript:this.select();";
                //txtTripPiernas.Attributes["onfocus"] = "javascript:this.select();";
                iBanPre = 0;

                if (eGetCargaInicial != null)
                    eGetCargaInicial(sender, e);
            }

            //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN Load   -------------------------------------------------");
        }
        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                //Utils.GuardarBitacora("MANTTO_DATOS  --> ** INICIO busqueda de clientes");
                if (eSearchObj != null)
                    eSearchObj(sender, e);
                //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN busqueda de clientes");
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
                        //e.Row.ToolTip = "De clic aqui para selecciona un cliente";
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
                //Utils.GuardarBitacora("MANTTO_DATOS  --> Selecciona la matricula ************");
                //dtGastosMex = null;
                lstCliente = new List<string>();
                string sMatriculag = string.Empty;
                string sContratog = string.Empty;
                string sClaveCliente = string.Empty;
                string sNombreCliente = string.Empty;
                string sRazonSocial = string.Empty;
                string sRFC = string.Empty;

                iBanPre = 1;
                int index = gvClientes.SelectedIndex.I();

                bool ban = false;

                if (index == gvClientes.SelectedIndex)
                {
                    //gvClientes.Rows[index].BackColor = ColorTranslator.FromHtml("#D9E1E4");
                    //gvClientes.Rows[index].ToolTip = string.Empty;

                    sMatriculag = gvClientes.Rows[index].Cells[3].Text.S();
                    sContratog = gvClientes.Rows[index].Cells[2].Text.S();
                    sClaveCliente = gvClientes.Rows[index].Cells[0].Text.S();
                    sRazonSocial = gvClientes.Rows[index].Cells[2].Text.S();
                    ban = true;
                }

                //foreach (GridViewRow row in gvClientes.Rows)
                //{
                //    if (row.RowIndex == gvClientes.SelectedIndex)
                //    {
                //        row.BackColor = ColorTranslator.FromHtml("#D9E1E4");
                //        row.ToolTip = string.Empty;

                //        sMatriculag = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();
                //        sContratog = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                //        sClaveCliente = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                //        sRazonSocial = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                //        ban = true;
                //    }
                //    else
                //    {
                //        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                //    }

                    if (ban)
                    {
                        sNombreCliente = gvClientes.Rows[index].Cells[1].Text.S();
                        txtPeriodo.Text = string.Empty;
                        sMatricula = sMatriculag;
                        gvClientes.ToolTip = "Clic para seleccionar esta fila.";


                        lstCliente.Add(sClaveCliente);
                        lstCliente.Add(sNombreCliente);
                        lstCliente.Add(sMatriculag);

                        sContrato = sContratog;
                        mpePeriodo.Show();
                        GC.Collect();
                        //Utils.GuardarBitacora("MANTTO_DATOS  --> Muestra el Calendario");
                    }
                //}
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAceptarPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                //Utils.GuardarBitacora("MANTTO_DATOS  --> ** INICIO Selecciona del periodo a consultar ************");
                Page.Validate("VPeriodo");
                if (Page.IsValid)
                {
                    iBanPre = 2; //Bandera para recargar gridview de gastos
                    string[] sPeriodo = txtPeriodo.Text.S().Split('/');

                    if (sPeriodo.Length == 1)
                        sPeriodo = txtPeriodo.Text.S().Split('-');

                    iMes = sPeriodo[1].S().I();
                    iAnio = sPeriodo[0].S().I();

                    lblClaveClienteUSD.Text = "Clave cliente: " + lstCliente[0];
                    lblNombreClienteUSD.Text = "Nombre cliente: " + lstCliente[1];
                    lblMatriculaUSD.Text = "Matrícula: " + lstCliente[2];

                    lblReqMes.Text = NombreMes;
                    lblReqAnio.Text = iAnio.S();

                    pnlActualizar.Visible = true;
                    pnlRubrosUSA.Visible = true;

                    if (eObjSelectedUSD != null)
                        eObjSelectedUSD(sender, e);

                    //lblCentroCostosMEX.Text = "Centro de costos: " + sCentroCostos;

                    upaPrincipal.Update();
                    upaGastosDolares.Update();
                    mpePeriodo.Hide();
                }
                else
                    mpePeriodo.Show();
                //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN muestra resultados en pantalla");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eUpaGastosUSD != null)
                    eUpaGastosUSD(sender, e);

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        decimal dSumaImporteUSA = 0;
        decimal dSumaImporteOUSA = 0;
        protected void gvMantenimientoUSA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string sRubroSelect = string.Empty;
            string sProvGSelect = string.Empty;
            //Utils.GuardarBitacora("MANTTO_DATOS  --> INICIO RowDataBound USD ---");
            try
            {
                if (!IsPostBack)
                {
                    if (iBanPre == 2)
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            DataTable dt = dtGastosUSA;
                            TextBox txtImp = (TextBox)e.Row.FindControl("txtImporte");
                            TextBox txtImp2;
                            DropDownList ddlPorc;

                            if (dtContratos.Rows.Count == 1)
                            {
                                txtImp2 = (TextBox)e.Row.FindControl("txtImporte_2");
                                ddlPorc = (DropDownList)e.Row.FindControl("ddlPorcentaje");

                                if (txtImp2 != null)
                                    txtImp2.Text = dt.Rows[e.Row.RowIndex]["ImporteContGasto"].S();

                                if (dtPorcentaje != null)
                                {
                                    ddlPorc.DataSource = dtPorcentaje;
                                    ddlPorc.DataTextField = "Valor";
                                    ddlPorc.DataValueField = "Id";
                                    ddlPorc.DataBind();

                                    if (dt.Rows[e.Row.RowIndex]["PorcParticipacion"].S() != "")
                                        ddlPorc.SelectedValue = dt.Rows[e.Row.RowIndex]["PorcParticipacion"].S();
                                }
                            }

                            if (txtImp != null)
                            {
                                if (dt != null)
                                {
                                    txtImp.Text = dt.Rows[e.Row.RowIndex]["ImporteModificado"].S();
                                    txtImp.Attributes["onfocus"] = "javascript:this.select();";
                                }
                            }

                            dSumaImporteUSA += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"));
                            dSumaImporteOUSA += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ImporteModificado"));

                            DropDownList ddlTipoGasto = (DropDownList)e.Row.FindControl("ddlTipoGasto");

                            if (ddlTipoGasto != null)
                            {
                                ddlTipoGasto.DataSource = dtTiposGasto;
                                ddlTipoGasto.DataTextField = "Descripcion";
                                ddlTipoGasto.DataValueField = "Valor";
                                ddlTipoGasto.DataBind();

                                if (dt.Rows[e.Row.RowIndex]["TipoGasto"].S() != "")
                                {
                                    ddlTipoGasto.SelectedValue = dt.Rows[e.Row.RowIndex]["TipoGasto"].S();
                                }
                            }

                            DropDownList ddlFijoVar = (DropDownList)e.Row.FindControl("ddlFijoVar");
                            if (ddlFijoVar != null)
                            {
                                ddlFijoVar.SelectedValue = dt.Rows[e.Row.RowIndex]["TipoRubro"].S();
                            }

                            TextBox txtComentarios = (TextBox)e.Row.FindControl("txtComentarios");
                            if (txtComentarios != null)
                            {
                                txtComentarios.Text = dt.Rows[e.Row.RowIndex]["Comentarios"].S();
                            }

                            DropDownList ddlRubro = (DropDownList)e.Row.FindControl("ddlRubro");
                            if (ddlRubro != null)
                            {
                                ddlRubro.DataSource = dtRubros;
                                ddlRubro.DataTextField = "DescripcionRubro";
                                ddlRubro.DataValueField = "IdRubro";
                                ddlRubro.DataBind();

                                sRubroSelect = dt.Rows[e.Row.RowIndex]["IdRubro"].S();
                                ddlRubro.SelectedValue = sRubroSelect;
                            }

                            DropDownList ddlProvGUS = (DropDownList)e.Row.FindControl("ddlProvGUS");
                            if (ddlProvGUS != null)
                            {
                                ddlProvGUS.DataSource = dtProveedor;
                                ddlProvGUS.DataTextField = "Descripcion";
                                ddlProvGUS.DataValueField = "IdProveedor";
                                ddlProvGUS.DataBind();

                                //sProvGSelect = dt.Rows[e.Row.RowIndex]["lblProv"].S();
                                Label lblProvUS = (Label)e.Row.FindControl("lblProvUS");
                                ddlProvGUS.SelectedItem.Text = lblProvUS.Text;
                            }

                            if (dtContratos != null)
                            {
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        if (dtContratos.Rows.Count > 1)
                                        {
                                            foreach (DataRow row in dtContratos.Rows)
                                            {
                                                DropDownList ddl = (DropDownList)e.Row.FindControl("ddl" + row.S("ClaveContrato") + "|" + e.Row.RowIndex.S());
                                                if (ddl != null)
                                                {
                                                    ddl.SelectedValue = dt.Rows[e.Row.RowIndex]["ddl" + row["ClaveContrato"]].S();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            Label lblNoPierna = (Label)e.Row.FindControl("lblNoPierna");
                            if (lblNoPierna != null)
                            {
                                lblNoPierna.Text = dt.Rows[e.Row.RowIndex]["NumeroPierna"].S();
                            }

                            if (e.Row.RowType == DataControlRowType.Footer)
                            {
                                e.Row.Cells[5].Text = dSumaImporteOUSA.ToString("c");
                                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                                e.Row.Cells[5].Font.Bold = true;
                                e.Row.Cells[6].Text = dSumaImporteUSA.ToString("c");
                                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                                e.Row.Cells[6].Font.Bold = true;
                                readTotal.Text = dSumaImporteUSA.ToString("c");
                            }

                            GC.Collect();

                        }
                        
                    }
                }


                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    DataTable dt = dtGastosUSA;

                //    // MOGI 21/01/2022
                //    //GridView gvDetalle = (GridView)e.Row.FindControl("gvDetalleGastoUSD");
                //    //if (gvDetalle != null)
                //    //{
                //    //    DataTable dtD = dt.Clone();
                //    //    dtD.ImportRow(dt.Rows[e.Row.RowIndex]);

                //    //    gvDetalle.DataSource = dtD;
                //    //    gvDetalle.DataBind();
                //    //}

                //    ImageButton btnGastoE = (ImageButton)e.Row.FindControl("btnEliminarUSA");
                //    if (btnGastoE != null)
                //    {
                //        btnGastoE.Visible = dt.Rows[e.Row.RowIndex]["IdTipoGasto"].S() == "2" ? true : false;
                //    }

                //    if (dtContratos != null)
                //    {
                //        TextBox txtNoPierna = (TextBox)e.Row.FindControl("txtNoPierna");
                //        TextBox txtTrip = (TextBox)e.Row.FindControl("txtNoTripUSA");
                //        TextBox txtImp = (TextBox)e.Row.FindControl("txtImporte");
                //        if //(txtNoPierna != null && txtTrip != null && 
                //            (txtImp != null)
                //        {
                //            if (dt != null)
                //            {
                //                //txtNoPierna.Text = dt.Rows[e.Row.RowIndex]["NumeroPierna"].S();
                //                //txtTrip.Text = dt.Rows[e.Row.RowIndex]["NumeroTrip"].S();
                //                txtImp.Text = dt.Rows[e.Row.RowIndex]["ImporteModificado"].S();

                //                //txtNoPierna.Attributes["onfocus"] = "javascript:this.select();";
                //                //txtTrip.Attributes["onfocus"] = "javascript:this.select();";
                //                txtImp.Attributes["onfocus"] = "javascript:this.select();";
                //            }
                //        }
                //    }

                //    dSumaImporteUSA += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"));
                //    dSumaImporteOUSA += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ImporteModificado"));

                //    DropDownList ddlTipoGasto = (DropDownList)e.Row.FindControl("ddlTipoGasto");
                //    //DropDownList ddlAcumulado1 = (DropDownList)e.Row.FindControl("ddlAcumulado1");

                //    //if (ddlTipoGasto != null && ddlAcumulado1 != null)
                //    if (ddlTipoGasto != null)
                //    {
                //        ddlTipoGasto.DataSource = dtTiposGasto;
                //        ddlTipoGasto.DataTextField = "Descripcion";
                //        ddlTipoGasto.DataValueField = "Valor";
                //        ddlTipoGasto.DataBind();

                //        if (dt.Rows[e.Row.RowIndex]["TipoGasto"].S() != "")
                //        {
                //            ddlTipoGasto.SelectedValue = dt.Rows[e.Row.RowIndex]["TipoGasto"].S();
                //            //CargaComboAcumuladoGasto(ddlAcumulado1, ObtieneAumuladosGasto1(dt.Rows[e.Row.RowIndex]["TipoGasto"].S()));
                //            //ddlAcumulado1.SelectedValue = dt.Rows[e.Row.RowIndex]["AmpliadoGasto"].S();
                //        }
                //    }

                //    DropDownList ddlFijoVar = (DropDownList)e.Row.FindControl("ddlFijoVar");
                //    if (ddlFijoVar != null)
                //    {
                //        ddlFijoVar.SelectedValue = dt.Rows[e.Row.RowIndex]["TipoRubro"].S();
                //    }

                //    TextBox txtComentarios = (TextBox)e.Row.FindControl("txtComentarios");
                //    if (txtComentarios != null)
                //    {
                //        txtComentarios.Text = dt.Rows[e.Row.RowIndex]["Comentarios"].S();
                //    }

                //    DropDownList ddlRubro = (DropDownList)e.Row.FindControl("ddlRubro");
                //    if (ddlRubro != null)
                //    {
                //        ddlRubro.DataSource = dtRubros;
                //        ddlRubro.DataTextField = "DescripcionRubro";
                //        ddlRubro.DataValueField = "IdRubro";
                //        ddlRubro.DataBind();

                //        ddlRubro.SelectedValue = dt.Rows[e.Row.RowIndex]["IdRubro"].S();
                //    }

                //    DropDownList ddlProvGUS = (DropDownList)e.Row.FindControl("ddlProvGUS");
                //    if (ddlProvGUS != null)
                //    {
                //        ddlProvGUS.DataSource = dtProveedor;
                //        ddlProvGUS.DataTextField = "Descripcion";
                //        ddlProvGUS.DataValueField = "IdProveedor";
                //        ddlProvGUS.DataBind();

                //        //sProvGSelect = dt.Rows[e.Row.RowIndex]["lblProv"].S();
                //        Label lblProvUS = (Label)e.Row.FindControl("lblProvUS");
                //        ddlProvGUS.SelectedItem.Text = lblProvUS.Text;
                //    }

                //    if (dtContratos != null)
                //    {
                //        if (dt != null)
                //        {
                //            if (dt.Rows.Count > 0)
                //            {
                //                foreach (DataRow row in dtContratos.Rows)
                //                {
                //                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddl" + row.S("ClaveContrato"));
                //                    if (ddl != null)
                //                    {
                //                        ddl.SelectedValue = dt.Rows[e.Row.RowIndex]["ddl" + row["ClaveContrato"]].S();
                //                    }
                //                }
                //            }
                //        }
                //    }

                //    ImageButton imbReferencia = (ImageButton)e.Row.FindControl("imbReferenciaDlls");
                //    if (imbReferencia != null)
                //    {
                //        if (dt.Rows[e.Row.RowIndex]["Comprobante"].S().I() == 1)
                //            imbReferencia.Visible = true;
                //    }

                //    Label lblNoPierna = (Label)e.Row.FindControl("lblNoPierna");
                //    if (lblNoPierna != null)
                //    {
                //        lblNoPierna.Text = dt.Rows[e.Row.RowIndex]["NumeroPierna"].S();
                //    }
                //}

                //if (e.Row.RowType == DataControlRowType.Footer)
                //{
                //    e.Row.Cells[5].Text = dSumaImporteOUSA.ToString("c");
                //    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                //    e.Row.Cells[5].Font.Bold = true;
                //    e.Row.Cells[6].Text = dSumaImporteUSA.ToString("c");
                //    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                //    e.Row.Cells[6].Font.Bold = true;
                //}

                //GC.Collect();
            }
            catch (Exception ex)
            {

            }
            //Utils.GuardarBitacora("MANTTO_DATOS  --> FIN RowDataBound USD --");
        }
        protected void gvMantenimientoUSA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Eliminar":
                        iIdGasto = gvMantenimientoUSA.DataKeys[e.CommandArgument.S().I()]["IdGasto"].S().L();

                        if (iIdGasto != 0)
                        {

                            if (eDeleteObj != null)
                                eDeleteObj(sender, e);

                            if (eObjSelectedUSD != null)
                                eObjSelectedUSD(sender, e);

                            upaGastosDolares.Update();
                        }
                        break;

                    case "ViewReference":
                        string sReferencia = e.CommandArgument.S();
                        string sUrl = sReferencia + ".pdf";
                        string strAnio = string.Empty;
                        string strMes = string.Empty;
                        strAnio = iAnio.S();
                        strMes = iMes.S();

                        DataTable dt = new DBMttoPDF().DBGetDetalleReferencia(sReferencia, sMatricula, iAnio.S(), iMes.S());
                        if (dt.Rows.Count > 0)
                        {
                            string sRuta = ArmaRutaComprobante(sReferencia);

                            if (File.Exists(sRuta + sUrl))
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../Gastos/frmVistaPreviaRef.aspx?sRuta=" + sRuta + sUrl + "',this.target, 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
                            else
                                MostrarMensaje("No se encontró el archivo, favor de verificar", "Aviso");
                        }
                        break;
                }
                GC.Collect();
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvMantenimientoUSA_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    if(iBanPre == 2)
                    {
                        DataTable dt = dtGastosUSA;

                        if (e.Row.RowType == DataControlRowType.Header)
                        {
                            if (dtContratos.Rows.Count > 1)
                            {
                                for (int i = 0; i < dtContratos.Rows.Count; i++)
                                {
                                    TableCell tc = new TableCell();
                                    tc.Text = dtContratos.Rows[i]["ClaveContrato"].S();
                                    tc.Style.Add("font-weight", "bold");
                                    e.Row.Cells.Add(tc);
                                }
                            }
                        }

                        if (dtContratos.Rows.Count > 1)
                        {
                            if (e.Row.RowType == DataControlRowType.DataRow)
                            {
                                for (int i = 0; i < dtContratos.Rows.Count; i++) //dt.Columns.Count; i++)
                                {
                                    DropDownList ddlPor = new DropDownList();
                                    ddlPor.ID = "ddl" + dtContratos.Rows[i]["ClaveContrato"].S().Replace("-", "") + "|" + e.Row.RowIndex.S();
                                    //ddlPor.CssClass = "slcList";
                                    ddlPor.Width = 65;
                                    ddlPor.DataSource = dtPorcentaje;
                                    ddlPor.DataTextField = "Valor";
                                    ddlPor.DataValueField = "Id";
                                    ddlPor.DataBind();

                                    ddlPor.SelectedValue = dt.Rows[e.Row.RowIndex]["ddl" + dtContratos.Rows[i]["ClaveContrato"].S().Replace("-", "")].S();

                                    TextBox t1 = new TextBox();
                                    t1.ID = "txt" + dtContratos.Rows[i]["ClaveContrato"].S().Replace("-", "");
                                    t1.Visible = true;
                                    t1.CssClass = "AlineadoDerecha";
                                    t1.Width = 65;
                                    t1.Text = dt.Rows[e.Row.RowIndex][dtContratos.Rows[i]["ClaveContrato"].S().Replace("-", "")].S();
                                    t1.Attributes["onfocus"] = "javascript:this.select();";

                                    TableCell tc = new TableCell();
                                    tc.Width = 300;
                                    tc.Controls.Add(ddlPor);
                                    tc.Controls.Add(t1);
                                    e.Row.Cells.Add(tc);
                                }

                                System.Web.UI.WebControls.Image imgError = new System.Web.UI.WebControls.Image();
                                imgError.ID = "imgError";
                                imgError.ImageUrl = "~/Images/icons/error.png";
                                imgError.ToolTip = "La distribución de porcentajes es incorrecta.";
                                imgError.Visible = false;
                                imgError.Height = 16;
                                imgError.Width = 16;

                                System.Web.UI.WebControls.Image imgWarning = new System.Web.UI.WebControls.Image();
                                imgWarning.ID = "imgWarning";
                                imgWarning.ImageUrl = "~/Images/icons/warning.png";
                                imgWarning.ToolTip = "No se ha realizado la distribución del gasto.";
                                imgWarning.Visible = false;
                                imgWarning.Height = 16;
                                imgWarning.Width = 16;

                                TableCell tcError = new TableCell();
                                tcError.Controls.Add(imgError);
                                tcError.Controls.Add(imgWarning);

                                e.Row.Cells.Add(tcError);
                            }
                        }

                        if (dtContratos.Rows.Count > 1)
                        {
                            if (e.Row.RowType == DataControlRowType.Footer)
                            {
                                for (int i = 0; i < dtContratos.Rows.Count; i++)
                                {
                                    TableCell tc = new TableCell();
                                    tc.Text = dtContratos.Rows[i]["ClaveContrato"].S();
                                    tc.Style.Add("font-weight", "bold");
                                    e.Row.Cells.Add(tc);
                                }

                            }
                        }
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvMantenimientoUSA_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (iBanPre == 2)
                    {
                        DataTable dt = dtGastosUSA;

                        for (int i = 0; i < gvMantenimientoUSA.Rows.Count; i++)
                        {
                            DropDownList ddlTipo = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlTipoGasto");
                            //CascadingDropDown cddTipo = (CascadingDropDown)gvMantenimientoUSA.Rows[i].FindControl("cdlTipoGasto");
                            DropDownList ddlAcu = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlAcumulado1");
                            CascadingDropDown cddAcu = (CascadingDropDown)gvMantenimientoUSA.Rows[i].FindControl("cdlAmpliado");

                            if (dt.Rows[i]["TipoGasto"].S() != "")
                            {
                                //cddTipo.SelectedValue = dt.Rows[i]["TipoGasto"].S();
                                ddlTipo.SelectedValue = dt.Rows[i]["TipoGasto"].S();

                                //cddAcu.SelectedValue = dt.Rows[i]["AmpliadoGasto"].S();

                                //if (dt.Rows[i]["AmpliadoGasto"].S() != "0")
                                //    ddlAcu.SelectedValue = dt.Rows[i]["AmpliadoGasto"].S();
                            }
                            else
                            {
                                ddlTipo.SelectedIndex = 0;
                            }
                        }

                        if (dtContratos.Rows.Count > 1)
                        {
                            DataTable dtTotalCon = new DataTable();
                            DataTable dtIndex = new DataTable();
                            for (int i = 0; i < dtContratos.Rows.Count; i++)
                            {
                                dtTotalCon.Columns.Add(dtContratos.Rows[i]["ClaveContrato"].S(), typeof(decimal));
                            }

                            dtIndex = dtTotalCon.Clone();
                            DataRow drImpCon = dtTotalCon.NewRow();

                            for (int i = 0; i < dtContratos.Rows.Count; i++)
                            {
                                decimal dImp = 0;
                                foreach (DataRow drImp in dt.Rows)
                                {
                                    dImp += drImp[dtContratos.Rows[i]["ClaveContrato"].S()].S().D();
                                }

                                drImpCon[dtContratos.Rows[i]["ClaveContrato"].S()] = dImp;
                            }

                            dtTotalCon.Rows.Add(drImpCon);


                            DataRow rowInd = dtIndex.NewRow();
                            for (int i = 0; i < gvMantenimientoUSA.HeaderRow.Cells.Count; i++)
                            {
                                foreach (DataRow dr in dtContratos.Rows)
                                {
                                    if (gvMantenimientoUSA.HeaderRow.Cells[i].Text.S() == dr["ClaveContrato"].S())
                                    {
                                        rowInd[dr["ClaveContrato"].S()] = i.S();
                                    }
                                }
                            }

                            dtIndex.Rows.Add(rowInd);

                            foreach (DataRow row in dtContratos.Rows)
                            {
                                gvMantenimientoUSA.FooterRow.Cells[dtIndex.Rows[0][row["ClaveContrato"].S()].S().I()].Text = dtTotalCon.Rows[0][row["ClaveContrato"].S()].S().D().ToString("c");
                                gvMantenimientoUSA.FooterRow.Cells[dtIndex.Rows[0][row["ClaveContrato"].S()].S().I()].HorizontalAlign = HorizontalAlign.Center;
                            }
                        }
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gvMantenimientoUSA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMantenimientoUSA.PageIndex = e.NewPageIndex;
                if (dsGastosUSD != null)
                {
                    gvMantenimientoUSA.DataSource = dtGastosUSA;
                    gvMantenimientoUSA.DataBind();
                    ControlLlenadoDatos(gvMantenimientoUSA);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscarPierna_Click1(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                if (row != null)
                {
                    iIdFila = row.RowIndex;
                    gvPiernasUSA.DataSource = null;
                    gvPiernasUSA.DataBind();
                    eMoneda = MonedaGasto.Dolares;
                    mpePiernasUSA.Show();

                    if (row.Cells[3].Text.S() != string.Empty && row.Cells[3].Text.S() != "&nbsp;")
                    {
                        txtTrioUSA.Text = string.Format("{0:yyyy-MM-dd}", row.Cells[3].Text.S().Dt());
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAgregarEstimadoUSA_Click(object sender, EventArgs e)
        {
            try
            {
                mpeGastosEstimados.Show();
                sTipoMonedaG = "USD";
                //sCentroCostos = string.Empty;

                //if (gvMantenimientoUSA.Rows.Count > 0)
                //    sCentroCostos = gvMantenimientoUSA.Rows[0].Cells[7].Text.S();

                CargaRubros();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAceptarEstimado_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("VGastoEstimado");
                if (Page.IsValid)
                {
                    oGastoE = new GastoEstimado();
                    oGastoE.sNoReferencia = txtNoReferencia.Text.S();
                    oGastoE.sMatricula = sMatricula;
                    oGastoE.dImporte = txtImporte.Text.S().D();
                    oGastoE.sTipoMoneda = sTipoMonedaG;
                    oGastoE.iIdRubro = ddlRubro.SelectedValue.S().I();
                    oGastoE.sProveedor = ddlProveedor.SelectedItem.Text;
                    oGastoE.iMes = iMes;
                    oGastoE.iAnio = iAnio;
                    oGastoE.iNumeroTrip = 0;
                    oGastoE.sUsuario = Utils.GetUser;

                    if (eNewGastoEstimado != null)
                        eNewGastoEstimado(sender, e);

                    if (eObjSelectedUSD != null)
                        eObjSelectedUSD(sender, e);

                    //upaGridGastosMXN.Update();
                    upaGastosDolares.Update();

                    LimpiaCamposGastoEstimado();

                    mpeGastosEstimados.Hide();
                }
                else
                    mpeGastosEstimados.Show();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnModificar1_Click(object sender, EventArgs e)
        {
            try
            {
                CargaImportePorcentajes(gvMantenimientoUSA);
                RecuperaGridDolares();

                if (ValidaPorcentajes(gvMantenimientoUSA) != 0)
                {
                    eMoneda = MonedaGasto.Dolares;
                    upaDolares.Update();
                    lblCaption.Text = "Gastos en Dolares";
                    lblMessageConfirm.Text = "Existen gastos sin distribuir o existe alguno incorrecto, ¿Desea continuar guardando la información?";
                    mpeConfirm.Show();
                    upaGastosDolares.Update();
                    return;
                }
                string sRes = ActualizaGridDolares();
                GC.Collect();
                MostrarMensaje(sRes, "Aviso");
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnAceptConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = string.Empty;
                switch (eMoneda)
                {
                    case MonedaGasto.Pesos:
                        //sResultado = ActualizaGridPesos();
                        break;
                    case MonedaGasto.Dolares:
                        sResultado = ActualizaGridDolares();
                        break;
                }

                mpeConfirm.Hide();
                MostrarMensaje(sResultado, "Modificación de importe");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "Error al guardar informacion");
            }
        }
        protected void btnCancelConfirm_Click(object sender, EventArgs e)
        {
            mpeConfirm.Hide();
        }
        protected void btnBuscarPiernasUSA_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTrioUSA.Text.S() != string.Empty)
                {
                    lblValFechaVloU.Visible = false;

                    dtFechaVlo = txtTrioUSA.Text.S().Dt();

                    if (eSearchLegs != null)
                        eSearchLegs(sender, e);

                    gvPiernasUSA.DataSource = dtLegs;
                    gvPiernasUSA.DataBind();
                }
                else
                {
                    lblValFechaVloU.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAceptarPiernaUSA_Click(object sender, EventArgs e)
        {
            try
            {
                if (rblTipoFechaUSA.SelectedValue == "1")
                {
                    bool ban = false;
                    UpdatePanel upa = new UpdatePanel();
                    for (int i = 0; i < gvPiernasUSA.Rows.Count; i++)
                    {
                        upa = (UpdatePanel)gvPiernasUSA.Rows[i].FindControl("upaDetGastosUSD");
                        RadioButton rb = (RadioButton)gvPiernasUSA.Rows[i].FindControl("rbSeleccionaUSA");
                        if (rb != null)
                        {
                            if (rb.Checked)
                            {
                                ban = true;
                                break;
                            }
                        }
                    }

                    int iIdPierna = 0;

                    if (!ban)
                    {
                        lblMensajePiernasUSA.Text = "Debe seleccionar una pierna, favor de verificar";
                        mpePiernasUSA.Show();
                    }
                    else
                    {
                        for (int i = 0; i < gvPiernasUSA.Rows.Count; i++)
                        {
                            RadioButton rb = (RadioButton)gvPiernasUSA.Rows[i].FindControl("rbSeleccionaUSA");
                            if (rb != null)
                            {
                                if (rb.Checked)
                                {
                                    iIdPierna = gvPiernasUSA.DataKeys[i]["LegId"].S().I();
                                }
                            }
                        }

                        DataRow[] row = dtLegs.Select("LegId = " + iIdPierna.S());
                        if (row != null)
                        {
                            
                            dtGastosUSA.Rows[iIdFila]["FechaVuelo"] = row[0]["FechaVuelo"].S();
                            dtGastosUSA.Rows[iIdFila]["LegId"] = iIdPierna;
                            dtGastosUSA.Rows[iIdFila]["Ruta"] = row[0]["Ruta"].S();
                            dtGastosUSA.Rows[iIdFila]["TiempoCalzo"] = row[0]["TiempoCalzo"].S();

                            // MOGI 21/01/2022
                            //LlenaGridDetalle(gvMantenimientoUSA, iIdFila, dtGastosUSA, "gvDetalleGastoUSD", upa);

                            UpdatePanel upaFechaMXN = (UpdatePanel)gvMantenimientoUSA.Rows[iIdFila].FindControl("upaFechaUSD");
                            if (upaFechaMXN != null)
                            {
                                Label lblFechaMXN = (Label)gvMantenimientoUSA.Rows[iIdFila].FindControl("lblFechaUSD");
                                if (lblFechaMXN != null)
                                    lblFechaMXN.Text = row[0]["FechaVuelo"].S().Dt().ToString("dd/MM/yyyy");

                                Label lblNoPierna = (Label)gvMantenimientoUSA.Rows[iIdFila].FindControl("lblNoPierna");
                                if (lblNoPierna != null)
                                    lblNoPierna.Text = iIdPierna.S();

                                upaFechaMXN.Update();
                            }

                            UpdatePanel upaGridDetalle = (UpdatePanel)gvMantenimientoUSA.Rows[iIdFila].FindControl("upaDetGastosUSD");
                            if (upaGridDetalle != null)
                                upaGridDetalle.Update();

                        }
                    }
                }
                else
                {
                    DataTable dtVacio = new DataTable();
                    dtVacio.Columns.Add("FechaVuelo");
                    dtVacio.Columns.Add("LegId");
                    dtVacio.Columns.Add("Ruta");
                    dtVacio.Columns.Add("TiempoCalzo");

                    DataRow row = dtVacio.NewRow();
                    row["FechaVuelo"] = txtFechaOperacionUSA.Text.S().Dt().ToString("dd/MM/yyyy");
                    row["LegId"] = "0";
                    row["Ruta"] = string.Empty;
                    row["TiempoCalzo"] = string.Empty;

                    dtVacio.Rows.Add(row);

                    // MOGI 21/01/2022
                    //GridView gvDetalle = (GridView)gvMantenimientoUSA.Rows[iIdFila].FindControl("gvDetalleGastoUSD");
                    //if (gvDetalle != null)
                    //{
                    //    gvDetalle.DataSource = dtVacio;
                    //    gvDetalle.DataBind();

                    //}
                    ////

                    ///


                    //gvMantenimientoUSA.Rows[iIdFila].Cells[3].Text = txtFechaOperacionUSA.Text.S().Dt().ToString("dd/MM/yyyy");

                    UpdatePanel upaFechaUSD = (UpdatePanel)gvMantenimientoUSA.Rows[iIdFila].FindControl("upaFechaUSD");
                    if (upaFechaUSD != null)
                    {

                        Label lblFechaUSD = (Label)gvMantenimientoUSA.Rows[iIdFila].FindControl("lblFechaUSD");
                        if (lblFechaUSD != null)
                            lblFechaUSD.Text = txtFechaOperacionUSA.Text.S().Dt().ToString("dd/MM/yyyy");

                        upaFechaUSD.Update();
                    }

                    //Label lblFechaUSD = (Label)gvMantenimientoUSA.Rows[iIdFila].FindControl("lblFechaUSD");
                    //if (lblFechaUSD != null)
                    //    lblFechaUSD.Text = txtFechaOperacionUSA.Text.S().Dt().ToString("dd/MM/yyyy");

                    UpdatePanel upaGridDetalle = (UpdatePanel)gvMantenimientoUSA.Rows[iIdFila].FindControl("upaDetGastosUSD");
                    if (upaGridDetalle != null)
                        upaGridDetalle.Update();
                }

                //timer1.Enabled = true;
                mpePiernasUSA.Hide();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnCancelarPiernaUSA_Click(object sender, EventArgs e)
        {

        }
        protected void lkbExportaUSD_Click(object sender, EventArgs e)
        {
            GridView gv = new GridView();
            gv.AutoGenerateColumns = true;

            DataTable dtPesos = new DataTable();
            dtPesos = ExportaGridExcelDolls(gvMantenimientoUSA, MonedaGasto.Dolares);
            gv.DataSource = dtPesos;
            gv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=MttoDolares_" + sMatricula + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void rblTipoFechaUSA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTipoFechaUSA.SelectedValue == "1")
            {
                pnlBusPiernasUSA.Visible = true;
                pnlFechaOpeUSA.Visible = false;
            }
            else
            {
                pnlBusPiernasUSA.Visible = false;
                pnlFechaOpeUSA.Visible = true;
            }
        }
        protected void lkbReferencia_Click(object sender, EventArgs e)
        {
            try
            {
                string sReferencia = ((LinkButton)sender).Text.S();
                string sUrl = sReferencia + ".pdf";

                DataTable dt = new DBMttoPDF().DBGetDetalleReferencia(sReferencia, sMatricula, iAnio.S(), iMes.S());
                if (dt.Rows.Count > 0)
                {
                    int iMesRef = dt.Rows[0]["Mes"].S().I();
                    int iAnioRef = dt.Rows[0]["Anio"].S().I();
                    string sMatriculaRef = dt.Rows[0]["Matricula"].S();
                    string sMoneda = dt.Rows[0]["TipoMoneda"].S();

                    String sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                    sRuta = sRuta.S().Replace("\\", "\\\\");
                    sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                    sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                    sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));
                    string sMon = sMoneda == "MXN" ? "MN" : "USD";
                    sRuta = sRuta.Replace("[moneda]", sMon);

                    if (File.Exists(sRuta + sUrl))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('frmVistaPreviaRef.aspx?sRuta=" + sRuta + sUrl + "',this.target, 'width=500,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
                    else
                        MostrarMensaje("No se encontró el archivo, favor de verificar", "Aviso");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnActualizarComprobantes_Click(object sender, EventArgs e)
        {
            if (eUpaComprobanteUSD != null)
                eUpaComprobanteUSD(sender, e);
        }
        #endregion

        #region METODOS
        public void LLenaClientes(DataTable dt)
        {
            try
            {
                gvClientes.DataSource = dt;
                gvClientes.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaGastosMEXUSA(DataSet ds)
        {
            try
            {
                if (ds != null)
                {
                    readTotal.Text = string.Empty;
                    dSumaImporteUSA = 0;
                    dsGastosUSD = null;
                    dsGastosUSD = ds;
                    dtGastosMEX = null;
                    dtGastosUSA = ds.Tables[0];
                    dtContratos = ds.Tables[1];

                    gvMantenimientoUSA.DataSource = dtGastosUSA;
                    gvMantenimientoUSA.DataBind();
                    ControlLlenadoDatos(gvMantenimientoUSA);

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[2].Rows[0][0].S()))
                        {
                            dSumaImporteUSA = ds.Tables[2].Rows[0][0].S().D();
                            readTotal.Text = dSumaImporteUSA.ToString("c");
                            pnlRubrosUSA.Visible = true;
                            upaTotales.Update();
                        }
                    }

                    
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int GetSortColumnIndex(GridView gv)
        {
            foreach (DataControlField field in gv.Columns)
            {
                if (field.SortExpression == gv.SortExpression)
                {
                    return gv.Columns.IndexOf(field);
                }
            }

            return -1;
        }
        private void AddSortImage(int columnIndex, GridViewRow headerRow, GridView gv)
        {
            System.Web.UI.WebControls.Image sortImage = new System.Web.UI.WebControls.Image();
            sortImage.Width = 16;
            sortImage.Height = 16;
            
            if (gv.SortDirection == SortDirection.Ascending)
            {
                sortImage.ImageUrl = "~/Images/icons/ascendente.png";
                sortImage.AlternateText = "Orden Ascendente";
            }
            else
            {
                sortImage.ImageUrl = "~/Images/icons/descendente.png";
                sortImage.AlternateText = "Orden Descendente";
            }

            headerRow.Cells[columnIndex].Controls.Add(sortImage);
        }        
        private string ArmaRutaComprobante(string sReferencia)
        {
            try
            {
                string sRuta = string.Empty;
                DataTable dt = new DBMttoPDF().DBGetDetalleReferencia(sReferencia, sMatricula, iAnio.S(), iMes.S());
                if (dt.Rows.Count > 0)
                {
                    int iMesRef = dt.Rows[0]["Mes"].S().I();
                    int iAnioRef = dt.Rows[0]["Anio"].S().I();
                    string sMatriculaRef = dt.Rows[0]["Matricula"].S();
                    string sMoneda = dt.Rows[0]["TipoMoneda"].S();

                    sRuta = System.Configuration.ConfigurationManager.AppSettings["PATH_FILES_S"].S();
                    sRuta = sRuta.S().Replace("\\", "\\\\");
                    sRuta = sRuta.Replace("[anio]", iAnioRef.S());
                    sRuta = sRuta.Replace("[matricula]", sMatriculaRef);
                    sRuta = sRuta.Replace("[mes]", ObtieneNombreMes(iMesRef));
                    string sMon = sMoneda == "MXN" ? "MN" : "USD";
                    sRuta = sRuta.Replace("[moneda]", sMon);
                }

                return sRuta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ObtieneNombreMes(int iMes)
        {
            string sMes = string.Empty;
            switch (iMes)
            {
                case 1:
                    sMes = "01 ENERO";
                    break;
                case 2:
                    sMes = "02 FEBRERO";
                    break;
                case 3:
                    sMes = "03 MARZO";
                    break;
                case 4:
                    sMes = "04 ABRIL";
                    break;
                case 5:
                    sMes = "05 MAYO";
                    break;
                case 6:
                    sMes = "06 JUNIO";
                    break;
                case 7:
                    sMes = "07 JULIO";
                    break;
                case 8:
                    sMes = "08 AGOSTO";
                    break;
                case 9:
                    sMes = "09 SEPTIEMBRE";
                    break;
                case 10:
                    sMes = "10 OCTUBRE";
                    break;
                case 11:
                    sMes = "11 NOVIEMBRE";
                    break;
                case 12:
                    sMes = "12 DICIEMBRE";
                    break;
            }

            return sMes;
        }
        private DataTable ExportaGridExcelDolls(GridView gv, MonedaGasto eMoneda)
        {
            try
            {
                DataTable dtPesos = new DataTable();
                //dtPesos.Columns.Add("Pierna");
                //dtPesos.Columns.Add("Trip");
                dtPesos.Columns.Add("FechaVuelo");
                dtPesos.Columns.Add("Referencia");
                dtPesos.Columns.Add("Importe");
                dtPesos.Columns.Add("ImporteO");
                dtPesos.Columns.Add("FijoVar");
                dtPesos.Columns.Add("Rubro");
                dtPesos.Columns.Add("TipoGasto");
                //dtPesos.Columns.Add("AmpliadoGasto");
                dtPesos.Columns.Add("Comentarios");

                if (dtContratos.Rows.Count > 1)
                {
                    for (int i = 0; i < dtContratos.Rows.Count; i++)
                    {
                        dtPesos.Columns.Add(dtContratos.Rows[i]["ClaveContrato"].S());
                        dtPesos.Columns.Add(dtContratos.Rows[i]["ClaveContrato"].S() + "Importe");
                    }
                }
                else
                {
                    dtPesos.Columns.Add("Porcentaje");
                    dtPesos.Columns.Add(dtContratos.Rows[0]["ClaveContrato"].S() + "Importe");
                }

                foreach (GridViewRow row in gv.Rows)
                {
                    DataRow dr = dtPesos.NewRow();

                    //dr["Pierna"] = ((TextBox)row.FindControl("txtNoPierna")).Text.S();
                    //dr["Trip"] = eMoneda == MonedaGasto.Pesos ? ((TextBox)row.FindControl("txtNoTripMEX")).Text.S() : ((TextBox)row.FindControl("txtNoTripUSA")).Text.S();
                    dr["FechaVuelo"] = ((Label)row.FindControl("lblFechaUSD")).Text.S();
                    dr["Referencia"] = ((Label)row.FindControl("lblReferenciaDlls")).Text.S();
                    dr["Importe"] = String.Format("{0:C}", Convert.ToDecimal(((TextBox)row.FindControl("txtImporte")).Text.S()));
                    Label lblImpOri = (Label)row.FindControl("lblImporteOriginalUSD");
                    dr["ImporteO"] = lblImpOri.Text;

                    //dr["ImporteO"] = String.Format("{0:C}", Convert.ToDecimal(row.Cells[6].Text.Replace("$", "").S()));
                    dr["FijoVar"] = ((DropDownList)row.FindControl("ddlFijoVar")).SelectedItem.Text.S();
                    dr["Rubro"] = ((DropDownList)row.FindControl("ddlRubro")).SelectedItem.Text.S();
                    dr["TipoGasto"] = ((DropDownList)row.FindControl("ddlTipoGasto")).SelectedItem.Text.S();
                    //dr["AmpliadoGasto"] = ""; // ((DropDownList)row.FindControl("ddlAcumulado1")).SelectedItem.Text.S();
                    dr["Comentarios"] = ((TextBox)row.FindControl("txtComentarios")).Text.S();

                    if (dtContratos.Rows.Count > 1)
                    {
                        for (int i = 0; i < dtContratos.Rows.Count; i++)
                        {
                            string sContrato = string.Empty;
                            sContrato = dtContratos.Rows[i]["ClaveContrato"].S().Replace("-", "");

                            DropDownList ddl = (DropDownList)gv.Rows[row.RowIndex].FindControl("ddl" + sContrato + "|" + row.RowIndex.S());
                            TextBox txt = (TextBox)gv.Rows[row.RowIndex].FindControl("txt" + sContrato);
                            dr[dtContratos.Rows[i]["ClaveContrato"].S()] = ddl.SelectedItem.Text.S();
                            dr[dtContratos.Rows[i]["ClaveContrato"].S() + "Imp"] = txt.Text.S();
                        }
                    }
                    else if (dtContratos.Rows.Count == 1)
                    {
                        DropDownList ddl = (DropDownList)gv.Rows[row.RowIndex].FindControl("ddlPorcentaje");
                        TextBox txt = (TextBox)gv.Rows[row.RowIndex].FindControl("txtImporte_2");
                        dr["Porcentaje"] = ddl.SelectedItem.Text.S();
                        dr[dtContratos.Rows[0]["ClaveContrato"].S() + "Importe"] = txt.Text.S();
                    }

                    dtPesos.Rows.Add(dr);
                }

                return dtPesos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ActualizaGridDolares()
        {
            try
            {
                oLstGastoE = new List<GastoEstimado>();
                oLstContratosGasto = new List<MantenimientoGastos>();

                oLstGastoE = (List<GastoEstimado>)Session["lstGridGastoEstimadoUsa"];
                oLstContratosGasto = (List<MantenimientoGastos>)Session["lstGridMantenimientoGastosUsa"];

                if (eSaveObj != null)
                    eSaveObj(null, EventArgs.Empty);

                if (eInsImpGasto != null)
                    eInsImpGasto(null, EventArgs.Empty);

                if (eObjSelectedUSD != null)
                    eObjSelectedUSD(null, EventArgs.Empty);

                return "Los gastos se modificaron correctamente.";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void RecuperaGridDolares()
        {
            try
            {
                DataTable dt = dtGastosUSA;
                int ifilas = gvMantenimientoUSA.Rows.Count;

                List<MantenimientoGastos> lista = new List<MantenimientoGastos>();
                List<GastoEstimado> lstGastoEstimado = new List<GastoEstimado>();
                List<List<GastoEstimado>> lstlstGastosEstimados = new List<List<GastoEstimado>>();
                Tuple<List<MantenimientoGastos>, List<List<GastoEstimado>>> tupleGrid = new Tuple<List<MantenimientoGastos>, List<List<GastoEstimado>>>(new List<MantenimientoGastos>(), new List<List<GastoEstimado>>());

                for (int i = 0; i < ifilas; i++)
                {
                    int iIdGasto = gvMantenimientoUSA.DataKeys[i.S().I()]["IdGasto"].S().I();

                    for (int j = 0; j < dtContratos.Rows.Count; j++)
                    {
                        string stxt = string.Empty;
                        string sContrato = string.Empty;
                        TextBox txt;
                        DropDownList ddl;

                        if (dtContratos.Rows.Count > 1)
                        {
                            sContrato = dtContratos.Rows[j]["ClaveContrato"].S().Replace("-", "");
                            stxt = "txt" + sContrato;
                            txt = (TextBox)gvMantenimientoUSA.Rows[i].FindControl(stxt);
                            ddl = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddl" + sContrato + "|" + i.S());
                        }
                        else
                        {
                            txt = (TextBox)gvMantenimientoUSA.Rows[i].FindControl("txtImporte_2");
                            ddl = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlPorcentaje");
                        }



                        MantenimientoGastos oM = new MantenimientoGastos();
                        oM.iIdGasto = iIdGasto.S().I();
                        oM.dImporte = txt.Text.S().D();
                        oM.sContrato = sContrato;
                        oM.iPorcentaje = ddl.SelectedValue.S().I();
                        oM.sUsuario = Utils.GetUser;
                        lista.Add(oM);
                    }

                    TextBox txtMonto = (TextBox)gvMantenimientoUSA.Rows[i].FindControl("txtImporte");
                    DropDownList ddlRubro = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlRubro");
                    DropDownList ddlTipoGasto = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlTipoGasto");
                    //DropDownList ddlAmpliadoGasto = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlAcumulado1");

                    if (txtMonto != null && ddlRubro != null && ddlTipoGasto != null)
                    {
                        GastoEstimado oG = new GastoEstimado();
                        oG.iIdGasto = iIdGasto;
                        oG.dImporte = txtMonto.Text.Replace(",", "").S().D();
                        oG.iNumeroTrip = 0;
                        oG.sUsuario = Utils.GetUser;
                        oG.iIdRubro = ddlRubro.SelectedValue.S().I();
                        oG.sTipoGasto = ddlTipoGasto.SelectedValue.S();
                        //oG.sAmpliadoGasto = ddlAmpliadoGasto.SelectedValue.S();
                        //oG.iNumeroPierna = txtPierna.Text.S().I();
                        Label lblFechaOper = (Label)gvMantenimientoUSA.Rows[i].FindControl("lblFechaUSD");
                        if (lblFechaOper.Text != string.Empty && lblFechaOper.Text != "&nbsp;")
                            oG.sFechaVueloOpe = lblFechaOper.Text;

                        if (oG.iNumeroPierna == 0)
                        {
                            Label lblNoPierna = (Label)gvMantenimientoUSA.Rows[i].FindControl("lblNoPierna");
                            if (lblNoPierna.Text != null)
                                oG.iNumeroPierna = lblNoPierna.Text.S().I();
                        }

                        TextBox txtComentarios = (TextBox)gvMantenimientoUSA.Rows[i].FindControl("txtComentarios");
                        if (txtComentarios != null)
                            oG.sComentarios = txtComentarios.Text.S();

                        DropDownList ddlTipoRubro = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlFijoVar");
                        if (ddlTipoRubro != null)
                            oG.iIdTipoRubro = ddlTipoRubro.SelectedValue.S().I();

                        DropDownList ddlProveedor = (DropDownList)gvMantenimientoUSA.Rows[i].FindControl("ddlProvGUS");
                        if (ddlProveedor != null)
                            oG.sProveedor = ddlProveedor.SelectedItem.Text;

                        lstGastoEstimado.Add(oG);
                    }
                }

                Session["lstGridMantenimientoGastosUsa"] = lista;
                Session["lstGridGastoEstimadoUsa"] = lstGastoEstimado;
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
        }
        private int ValidaPorcentajes(GridView gv)
        {
            try
            {
                int iban = 0;

                foreach (GridViewRow row in gv.Rows)
                {
                    int iPorcent = 0;
                    if (dtContratos.Rows.Count > 1)
                    {
                        for (int i = 1; i < dtContratos.Rows.Count; i++)
                        {
                            DropDownList ddl = (DropDownList)row.FindControl("ddl" + dtContratos.Rows[i]["ClaveContrato"].S() + "|" + row.RowIndex);
                            if (ddl != null)
                            {
                                iPorcent += ddl.SelectedValue.S().I();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtContratos.Rows.Count; i++)
                        {
                            DropDownList ddl = (DropDownList)row.FindControl("ddlPorcentaje");
                            if (ddl != null)
                                iPorcent += ddl.SelectedValue.S().I();
                        }
                    }

                    if (iPorcent == 0)
                    {
                        System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)row.FindControl("imgError");
                        System.Web.UI.WebControls.Image img2 = (System.Web.UI.WebControls.Image)row.FindControl("imgWarning");

                        img.Visible = false;
                        img2.Visible = true;

                        iban = 1;

                        row.BackColor = System.Drawing.Color.Beige;
                    }
                    else if ((iPorcent > 0 && iPorcent < 100) || iPorcent > 100)
                    {
                        System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)row.FindControl("imgError");
                        System.Web.UI.WebControls.Image img2 = (System.Web.UI.WebControls.Image)row.FindControl("imgWarning");

                        img.Visible = true;
                        img2.Visible = false;
                        iban = 2;

                        row.BackColor = System.Drawing.Color.Beige;
                    }
                    else
                    {
                        System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)row.FindControl("imgError");
                        System.Web.UI.WebControls.Image img2 = (System.Web.UI.WebControls.Image)row.FindControl("imgWarning");
                        if (img != null)
                        {
                            img.Visible = false;
                            img2.Visible = false;
                        }

                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                }

                return iban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaImportePorcentajes(GridView gv)
        {
            try
            {

                if (dtContratos != null && dtContratos.Rows.Count > 1)
                {
                    foreach (GridViewRow gvRow in gv.Rows)
                    {
                        TextBox txtImp = (TextBox)gvRow.FindControl("txtImporte");
                        if (txtImp != null)
                        {

                            foreach (DataRow row in dtContratos.Rows)
                            {
                                DropDownList ddlPor = (DropDownList)gvRow.FindControl("ddl" + row["ClaveContrato"].S() + "|" + gvRow.RowIndex);
                                if (ddlPor != null)
                                {
                                    TextBox txtCon = (TextBox)gvRow.FindControl("txt" + row["ClaveContrato"].S());
                                    if (txtCon != null)
                                    {
                                        txtCon.Text = Math.Round(txtImp.Text.S().D() * (ddlPor.SelectedValue.S().D() / 100), 2).S();
                                    }
                                }

                            }

                        }
                    }
                }
                else if (dtContratos != null && dtContratos.Rows.Count == 1)
                {
                    foreach (GridViewRow gvRow in gv.Rows)
                    {
                        TextBox txtImp = (TextBox)gvRow.FindControl("txtImporte");
                        if (txtImp != null)
                        {

                            foreach (DataRow row in dtContratos.Rows)
                            {
                                DropDownList ddlPor = (DropDownList)gvRow.FindControl("ddlPorcentaje");
                                if (ddlPor != null)
                                {
                                    TextBox txtCon = (TextBox)gvRow.FindControl("txt" + row["ClaveContrato"].S());
                                    if (txtCon != null)
                                    {
                                        txtCon.Text = Math.Round(txtImp.Text.S().D() * (ddlPor.SelectedValue.S().D() / 100), 2).S();
                                    }
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
        private void CargaComboAcumuladoGasto(DropDownList ddl, DataTable dt)
        {
            try
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "Descripcion";
                ddl.DataValueField = "Valor";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ObtieneAumuladosGasto1(string sConcepto)
        {
            try
            {
                return new DBMantenimiento().DBGetConsultaAcumuladosGastos(sConcepto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaRubros()
        {
            try
            {
                if (dtRubros != null)
                {
                    ddlRubro.DataSource = dtRubros;
                    ddlRubro.DataValueField = "IdRubro";
                    ddlRubro.DataTextField = "DescripcionRubro";
                    ddlRubro.DataBind();
                }

                if (dtProveedor != null)
                {
                    ddlProveedor.DataSource = dtProveedor;
                    ddlProveedor.DataValueField = "IdProveedor";
                    ddlProveedor.DataTextField = "Descripcion";
                    ddlProveedor.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiaCamposGastoEstimado()
        {
            try
            {
                txtNoReferencia.Text = string.Empty;
                txtImporte.Text = string.Empty;
                ddlRubro.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ControlLlenadoDatos(GridView gv)
        {
            try
            {
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    int iIdGasto = gv.DataKeys[i].Values["IdGasto"].S().I();
                    string sTipoGasto = gv.DataKeys[i].Values["IdTipoGasto"].S();
                    string sComprobante = gv.DataKeys[i].Values["Comprobante"].S();
                    ImageButton btnGastoE = (ImageButton)gv.Rows[i].FindControl("btnEliminarUSA");
                    ImageButton imbReferencia = (ImageButton)gv.Rows[i].FindControl("imbReferenciaDlls");
                    TextBox txtImp = (TextBox)gv.Rows[i].FindControl("txtImporte");
                    DropDownList ddlFijoVar = (DropDownList)gv.Rows[i].FindControl("ddlFijoVar");
                    TextBox txtComentarios = (TextBox)gv.Rows[i].FindControl("txtComentarios");
                    DropDownList ddlRubro = (DropDownList)gv.Rows[i].FindControl("ddlRubro");
                    DropDownList ddlProvGUS = (DropDownList)gv.Rows[i].FindControl("ddlProvGUS");
                    Label lblProvUS = (Label)gv.Rows[i].FindControl("lblProvUS");
                    DropDownList ddlTipoGasto = (DropDownList)gv.Rows[i].FindControl("ddlTipoGasto");
                    TextBox txtImp2 = (TextBox)gv.Rows[i].FindControl("txtImporte_2");
                    DropDownList ddlPorc = (DropDownList)gv.Rows[i].FindControl("ddlPorcentaje");

                    if (dtGastosUSA != null && dtGastosUSA.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtGastosUSA.Rows.Count; x++)
                        {
                            if (iIdGasto == dtGastosUSA.Rows[x]["IdGasto"].S().I())
                            {
                                if (txtImp != null)
                                    txtImp.Text = dtGastosUSA.Rows[x]["ImporteModificado"].S();

                                if (ddlFijoVar != null)
                                    ddlFijoVar.SelectedValue = dtGastosUSA.Rows[x]["TipoRubro"].S();

                                if (txtComentarios != null)
                                    txtComentarios.Text = dtGastosUSA.Rows[x]["Comentarios"].S();

                                if (ddlRubro != null)
                                {
                                    ddlRubro.DataSource = dtRubros;
                                    ddlRubro.DataTextField = "DescripcionRubro";
                                    ddlRubro.DataValueField = "IdRubro";
                                    ddlRubro.DataBind();
                                    ddlRubro.SelectedValue = dtGastosUSA.Rows[x]["IdRubro"].S();
                                }

                                if (ddlProvGUS != null)
                                {
                                    ddlProvGUS.DataSource = dtProveedor;
                                    ddlProvGUS.DataTextField = "Descripcion";
                                    ddlProvGUS.DataValueField = "IdProveedor";
                                    ddlProvGUS.DataBind();
                                    ddlProvGUS.SelectedItem.Text = lblProvUS.Text;
                                }

                                if (ddlTipoGasto != null)
                                {
                                    ddlTipoGasto.DataSource = dtTiposGasto;
                                    ddlTipoGasto.DataTextField = "Descripcion";
                                    ddlTipoGasto.DataValueField = "Valor";
                                    ddlTipoGasto.DataBind();

                                    if (dtGastosUSA.Rows[x]["TipoGasto"].S() != "")
                                        ddlTipoGasto.SelectedValue = dtGastosUSA.Rows[x]["TipoGasto"].S();
                                }

                                if (dtContratos.Rows.Count == 1)
                                {
                                    if (dtGastosUSA != null)
                                        txtImp2.Text = dtGastosUSA.Rows[x]["ImporteContGasto"].S();

                                    if (dtPorcentaje != null)
                                    {
                                        ddlPorc.DataSource = dtPorcentaje;
                                        ddlPorc.DataTextField = "Valor";
                                        ddlPorc.DataValueField = "Id";
                                        ddlPorc.DataBind();

                                        if (dtGastosUSA.Rows[x]["PorcParticipacion"].S() != "")
                                            ddlPorc.SelectedValue = dtGastosUSA.Rows[x]["PorcParticipacion"].S();
                                    }
                                }

                                if (btnGastoE != null)
                                {
                                    if (dtGastosUSA.Rows[x]["IdTipoGasto"].S() == "2")
                                        btnGastoE.Visible = true;
                                    else
                                        btnGastoE.Visible = false;
                                }

                                if (imbReferencia != null)
                                {
                                    if (dtGastosUSA.Rows[x]["Comprobante"].S().I() == 1)
                                        imbReferencia.Visible = true;
                                    else
                                        imbReferencia.Visible = false;
                                }

                                break;
                            }
                        }

                    }



                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            sMensaje = "alert('" + sMensaje + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", sMensaje, true);
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        Mantenimiento_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eUpaGastos;
        public event EventHandler eUpaGastosUSD;
        public event EventHandler eInsImpGasto;
        public event EventHandler eSearchLegs;
        public event EventHandler eNewGastoEstimado;
        public event EventHandler eUpaComprobanteUSD;
        public event EventHandler eUpaComprobanteMXN;
        public event EventHandler eGetCargaInicial;
        public event EventHandler eObjSelectedUSD;

        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["VClientes"]; }
            set { ViewState["VClientes"] = value; }
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
        public string sMatricula
        {
            get { return (string)ViewState["VMatricula"]; }
            set { ViewState["VMatricula"] = value; }
        }
        public string sContrato
        {
            get { return (string)ViewState["VContrato"]; }
            set { ViewState["VContrato"] = value; }
        }
        public List<string> lstCliente
        {
            get { return (List<string>)ViewState["VCliente"]; }
            set { ViewState["VCliente"] = value; }
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
        public object[] oArrGastos
        {
            get
            {
                return new object[] {
                    "@MES", iMes,
                    "@ANIO", iAnio,
                    "@MAT", sMatricula,
                    "@Cuentas", ""
                };
            }
        }
        public long iIdGasto
        {
            get { return (long)ViewState["VSIdGasto"]; }
            set { ViewState["VSIdGasto"] = value; }
        }

        public int iBanPre
        {
            get { return (int)ViewState["VBanPre"]; }
            set { ViewState["VBanPre"] = value; }
        }

        public DataTable dtGastosMEX
        {
            get { return (DataTable)ViewState["VSMex"]; }
            set { ViewState["VSMex"] = value; }
        }
        public DataTable dtGastosUSA
        {
            get { return (DataTable)ViewState["VSUsa"]; }
            set { ViewState["VSUsa"] = value; }
        }
        public DataTable dtContratos
        {
            get { return (DataTable)ViewState["VSContratos"]; }
            set { ViewState["VSContratos"] = value; }
        }
        public DataTable dtRubros
        {
            get { return (DataTable)ViewState["VSRubros"]; }
            set { ViewState["VSRubros"] = value; }
        }
        public DataTable dtTiposGasto
        {
            get { return (DataTable)ViewState["VSTiposGasto"]; }
            set { ViewState["VSTiposGasto"] = value; }
        }
        private DataTable dtPorcentaje
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Valor");

                for (int i = 0; i <= 100; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = i.S();
                    dr["Valor"] = i.S();

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }
        public List<GastoEstimado> oLstGastoE
        {
            get { return (List<GastoEstimado>)ViewState["VSoLstGastoE"]; }
            set { ViewState["VSoLstGastoE"] = value; }
        }
        public List<MantenimientoGastos> oLstContratosGasto
        {
            get { return (List<MantenimientoGastos>)ViewState["VSoLstContratosGasto"]; }
            set { ViewState["VSoLstContratosGasto"] = value; }
        }
        private int iIdFila
        {
            get { return ViewState["VSIFila"].S().I(); }
            set { ViewState["VSIFila"] = value; }
        }
        public int iTrip
        {
            get { return ViewState["VSiTrip"].S().I(); }
            set { ViewState["VSiTrip"] = value; }
        }
        public DataTable dtLegs
        {
            get { return (DataTable)ViewState["VSLegs"]; }
            set { ViewState["VSLegs"] = value; }
        }
        public GastoEstimado oGastoE
        {
            get { return (GastoEstimado)ViewState["VSGastoE"]; }
            set { ViewState["VSGastoE"] = value; }
        }
        private MonedaGasto eMoneda
        {
            get { return (MonedaGasto)ViewState["VSMonedaGasto"]; }
            set { ViewState["VSMonedaGasto"] = value; }
        }
        private enum MonedaGasto
        {
            Pesos,
            Dolares
        }
        public string sTipoMonedaG
        {
            get { return (string)ViewState["VSMonedaG"]; }
            set { ViewState["VSMonedaG"] = value; }
        }
        public string NombreMes
        {
            get
            {
                string sMes = string.Empty;
                switch (iMes)
                {
                    case 1:
                        sMes = "Enero";
                        break;

                    case 2:
                        sMes = "Febrero";
                        break;

                    case 3:
                        sMes = "Marzo";
                        break;

                    case 4:
                        sMes = "Abril";
                        break;

                    case 5:
                        sMes = "Mayo";
                        break;

                    case 6:
                        sMes = "Junio";
                        break;

                    case 7:
                        sMes = "Julio";
                        break;

                    case 8:
                        sMes = "Agosto";
                        break;

                    case 9:
                        sMes = "Septiembre";
                        break;

                    case 10:
                        sMes = "Octubre";
                        break;

                    case 11:
                        sMes = "Noviembre";
                        break;

                    case 12:
                        sMes = "Diciembre";
                        break;
                }

                return sMes;
            }
        }
        public DateTime dtFechaVlo
        {
            get { return (DateTime)ViewState["VSFechaVlo"]; }
            set { ViewState["VSFechaVlo"] = value; }
        }

        public DataTable dtProveedor
        {
            get { return (DataTable)ViewState["VSProveedor"]; }
            set { ViewState["VSProveedor"] = value; }
        }

        public DataSet dsGastosUSD
        {
            get { return (DataSet)ViewState["VSdsGastosUSD"]; }
            set { ViewState["VSdsGastosUSD"] = value; }
        }
        #endregion

    }
}