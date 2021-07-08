using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ClientesCasa.Objetos;
using System.IO;

namespace ClientesCasa.Views.CuentasPorPagar
{
    public partial class frmNominaFin : System.Web.UI.Page, IViewNominafin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new NominaFin_Presenter(this, new DBNominaFin());

            if (!IsPostBack)
            {
                CargaDatosIniciales();

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }

        //protected void gvArchivos_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            GridView gvDetalle = (GridView)e.Row.FindControl("gvDetalle");
        //            if (gvDetalle != null)
        //            {
        //                DataTable dtDetalle = dsDetalleNomina.Tables[1].Copy();
        //                dtDetalle.Columns.Add("CardCode");

        //                foreach (DataRow row in dtDetalle.Rows)
        //                {
        //                    string sCardCodeEmpresa = new DBAccesoSAP().DBGetCardCodePorCardName(row["Empresa"].S());
        //                    row["CardCode"] = sCardCodeEmpresa;
        //                }

        //                dtDetalle.AcceptChanges();


        //                gvDetalle.DataSource = dtDetalle;
        //                gvDetalle.DataBind();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int iStatus = dsDetalleNomina.Tables[0].Rows[e.Row.RowIndex]["EstatusDoc"].S().I();
                    if (iStatus == 2)
                    {
                        ImageButton btnCargar = (ImageButton)e.Row.FindControl("btnCrearPoliza");
                        ImageButton btnRechazar = (ImageButton)e.Row.FindControl("btnRechazarFactura");
                        Label lblCorr = (Label)e.Row.FindControl("lblEstatusFact");
                        LinkButton lblErr = (LinkButton)e.Row.FindControl("lblErrorFact");

                        if (btnCargar != null)
                        {
                            btnCargar.Enabled = false;
                        }
                        if (btnRechazar != null)
                        {
                            btnRechazar.Enabled = false;
                        }

                        if (lblCorr != null)
                        {
                            lblCorr.Visible = true;
                        }
                        if (lblErr != null)
                        {
                            lblErr.Visible = false;
                        }
                    }
                    else if (iStatus == 3)
                    {
                        Label lblCorr = (Label)e.Row.FindControl("lblEstatusFact");
                        LinkButton lblErr = (LinkButton)e.Row.FindControl("lblErrorFact");
                        if (lblCorr != null)
                        {
                            lblCorr.Visible = false;
                        }
                        if (lblErr != null)
                        {
                            lblErr.Visible = true;
                            lblErr.ToolTip = dsDetalleNomina.Tables[0].Rows[e.Row.RowIndex]["MsgSAP"].S();
                        }
                    }
                    else
                    {
                        Label lblCorr = (Label)e.Row.FindControl("lblEstatusFact");
                        LinkButton lblErr = (LinkButton)e.Row.FindControl("lblErrorFact");

                        if (lblCorr != null)
                        {
                            lblCorr.Visible = true;
                        }
                        if (lblErr != null)
                        {
                            lblErr.Visible = false;
                        }
                    }
                    
                    int iExisteXML = dsDetalleNomina.Tables[0].Rows[e.Row.RowIndex]["ExisteXML"].S().I();
                    if (iExisteXML == 0)
                    {
                        ImageButton btnXML = (ImageButton)e.Row.FindControl("imbXML");
                        if (btnXML != null)
                        {
                            btnXML.Enabled = false;
                            btnXML.ToolTip = "El archivo no se ha cargado";
                        }
                    }

                    int iExistePDF = dsDetalleNomina.Tables[0].Rows[e.Row.RowIndex]["ExistePDF"].S().I();
                    if (iExistePDF == 0)
                    {
                        ImageButton btnPDF = (ImageButton)e.Row.FindControl("imbPDF");
                        if (btnPDF != null)
                        {
                            btnPDF.Enabled = false;
                            btnPDF.ToolTip = "El archivo no se ha cargado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idx = e.CommandArgument.S().I();

                sEmpresa = dsDetalleNomina.Tables[0].Rows[idx]["Empresa"].S();
                iNoFact = dsDetalleNomina.Tables[0].Rows[idx]["NumeroFactura"].S().I();

                if (e.CommandName == "Poliza")
                {
                    Page.Validate("VGDoc");
                    if (Page.IsValid)
                    {
                        if (eSaveObj != null)
                            eSaveObj(sender, e);

                        if (eUpdEstatusFact != null)
                            eUpdEstatusFact(sender, e);

                        txtComentarios.Text = string.Empty;
                        txtNumAtCard.Text = string.Empty;
                        txtFechaDoc.Text = string.Empty;
                    }

                }
                if (e.CommandName == "RechazarFactura")
                {
                    if (eDeleteObj != null)
                        eDeleteObj(sender, e);
                }

                if (e.CommandName == "XML")
                {
                    DataRow[] rows = dsDetalleNomina.Tables[2].Select("IdFolio = " + iIdFolio.S() + " and Empresa = '" + sEmpresa + "' and NoFactura = " + iNoFact.S());

                    string sNombreArch = rows[0]["NombreXML"].S();
                    string sExtension = Path.GetExtension(sNombreArch);

                    byte[] bPDF = (byte[])rows[0]["XmlDoc"];
                    if (bPDF != null)
                    {
                        MemoryStream ms = new MemoryStream(bPDF);
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + sNombreArch);
                        Response.ContentType = "application/octet-stream";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(bPDF);
                        Response.Flush();
                        Response.End();
                    }
                }

                if (e.CommandName == "PDF")
                {
                    DataRow[] rows = dsDetalleNomina.Tables[2].Select("IdFolio = " + iIdFolio.S() + " and Empresa = '" + sEmpresa + "' and NoFactura = " + iNoFact.S());
                    string sNombreArch = rows[0]["NombrePDF"].S();
                    string sExtension = Path.GetExtension(sNombreArch);

                    byte[] bPDF = (byte[])rows[0]["XmlPdf"];
                    if (bPDF != null)
                    {
                        MemoryStream ms = new MemoryStream(bPDF);
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + sNombreArch);
                        Response.ContentType = "application/octet-stream";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(bPDF);
                        Response.Flush();
                        Response.End();
                    }
                }

                if (eSearchObj != null)
                    eSearchObj(sender, e);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region METODOS
        public void LoadParametros(DataTable dt)
        {
            try
            {
                DataRow[] rows = dt.Select("Clave = '1003'");
                if (rows != null && rows.Length > 0)
                {
                    sTaxCode = rows[0]["Valor"].S();
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
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "alert", sMensaje, true);
        }
        private string ObtieneNombreMes(int iMes)
        {
            string smes = string.Empty;

            switch (iMes)
            {
                case 1:
                    smes = "Enero";
                    break;
                case 2:
                    smes = "Febrero";
                    break;
                case 3:
                    smes = "Marzo";
                    break;
                case 4:
                    smes = "Abril";
                    break;
                case 5:
                    smes = "Mayo";
                    break;
                case 6:
                    smes = "Junio";
                    break;
                case 7:
                    smes = "Julio";
                    break;
                case 8:
                    smes = "Agosto";
                    break;
                case 9:
                    smes = "Septiembre";
                    break;
                case 10:
                    smes = "Octubre";
                    break;
                case 11:
                    smes = "Noviembre";
                    break;
                case 12:
                    smes = "Diciembre";
                    break;
            }

            return smes;
        }
        private void CargaDatosIniciales()
        {
            try
            {
                dtExiste = new DataTable();
                dtExiste = new DBPolizaNomina().DBGetObtieneSiPeriodoABACOyaExiste;

                if (dtExiste.Rows.Count > 0)
                {
                    string sMes = string.Empty;
                    if (dtExiste.Rows[0]["Resultado"].S() == "1")
                    {
                        sMes = ObtieneNombreMes(dtExiste.Rows[0]["Mes"].S().I());
                        iIdFolio = dtExiste.Rows[0]["IdFolio"].S().I();
                    }

                }
                else
                {
                    MostrarMensaje("No hay información de ABACO que visualizar.", "Aviso");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadDetalleNomina()
        {
            dsDetalleNomina.Tables[0].Columns.Add("CardCode");
            foreach (DataRow row in dsDetalleNomina.Tables[0].Rows)
            {
                string sCardCodeEmpresa = new DBAccesoSAP().DBGetCardCodePorCardName(row["Empresa"].S());
                row["CardCode"] = sCardCodeEmpresa;
            }
            dsDetalleNomina.Tables[0].AcceptChanges();

            gvDetalle.DataSource = dsDetalleNomina.Tables[0];
            gvDetalle.DataBind();

            lblTituloInfo.Text = "Información de " + dsDetalleNomina.Tables[1].Rows[0]["Mes"].S() + " " + dsDetalleNomina.Tables[1].Rows[0]["Anio"].S();
        }
        #endregion


        #region VARIABLES Y PROPIEDADES
        private const string sClase = "frmNominaFin.aspx.cs";
        private const string sPagina = "frmNominaFin.aspx";

        NominaFin_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eUpdEstatusFact;

        public int iIdFolio
        {
            get { return ViewState["VSIdFolio"].S().I(); }
            set { ViewState["VSIdFolio"] = value; }
        }
        public string sTaxCode
        {
            get { return ViewState["VSTaxCode"].S(); }
            set { ViewState["VSTaxCode"] = value; }
        }
        public DataTable dtExiste
        {
            set { ViewState["VSdtExiste"] = value; }
            get { return (DataTable)ViewState["VSdtExiste"]; }
        }
        public int iFolioExistente
        {
            get { return (int)ViewState["VSFolioExistente"]; }
            set { ViewState["VSFolioExistente"] = value; }
        }
        public DataSet dsDetalleNomina
        {
            get { return (DataSet)ViewState["VSDetalleNomina"]; }
            set { ViewState["VSDetalleNomina"] = value; }
        }
        public string sEmpresa
        {
            get { return ViewState["VSNombreEmpresa"].S(); }
            set { ViewState["VSNombreEmpresa"] = value; }
        }
        public int iNoFact
        {
            get { return ViewState["VSNoFact"].S().I(); }
            set { ViewState["VSNoFact"] = value; }
        }
        public int iEstatus
        {
            get { return ViewState["VSiEstatus"].S().I(); }
            set { ViewState["VSiEstatus"] = value; }
        }

        //public int iIdFactura
        //{
        //    get { return ViewState["VSiIdFactura"].S().I(); }
        //    set { ViewState["VSiIdFactura"] = value; }
        //}
                    

        public HeaderFactura oHeader
        {
            get
            {
                HeaderFactura oHF = new HeaderFactura();
                oHF.dtFechaDoc = txtFechaDoc.Text.S().Dt();
                oHF.Referencia = txtNumAtCard.Text.S();
                oHF.Comentarios = txtComentarios.Text.S();

                return oHF;
            }
        }

        #endregion
    }
}