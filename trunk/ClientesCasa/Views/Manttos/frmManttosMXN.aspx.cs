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

namespace ClientesCasa.Views.Manttos
{
    public partial class frmManttosMXN : System.Web.UI.Page, IViewMantenimiento
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            //se inicia el presentador
            oPresenter = new Mantenimiento_Presenter(this, new DBMantenimiento());

            if (!IsPostBack)
            {
                //txtTrioUSA.Attributes["onfocus"] = "javascript:this.select();";
                //txtTripPiernas.Attributes["onfocus"] = "javascript:this.select();";

                if (eGetCargaInicial != null)
                    eGetCargaInicial(sender, e);
            }
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
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lstCliente = new List<string>();
                string sMatriculag = string.Empty;
                string sContratog = string.Empty;
                string sClaveCliente = string.Empty;
                string sNombreCliente = string.Empty;
                string sRazonSocial = string.Empty;
                string sRFC = string.Empty;

                bool ban = false;
                foreach (GridViewRow row in gvClientes.Rows)
                {
                    if (row.RowIndex == gvClientes.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#D9E1E4");
                        row.ToolTip = string.Empty;

                        sMatriculag = gvClientes.Rows[row.RowIndex].Cells[3].Text.S();
                        sContratog = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        sClaveCliente = gvClientes.Rows[row.RowIndex].Cells[0].Text.S();
                        sRazonSocial = gvClientes.Rows[row.RowIndex].Cells[2].Text.S();
                        ban = true;
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    }

                    if (ban)
                    {
                        sNombreCliente = gvClientes.Rows[row.RowIndex].Cells[1].Text.S();
                        txtPeriodo.Text = string.Empty;
                        sMatricula = sMatriculag;
                        row.ToolTip = "Clic para seleccionar esta fila.";


                        lstCliente.Add(sClaveCliente);
                        lstCliente.Add(sNombreCliente);
                        lstCliente.Add(sMatriculag);

                        sContrato = sContratog;
                        mpePeriodo.Show();
                        //Utils.GuardarBitacora("MANTTO_DATOS  --> Muestra el Calendario");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAgregarEstimado_Click(object sender, EventArgs e)
        {

        }

        protected void lbkExportaMXN_Click(object sender, EventArgs e)
        {

        }

        decimal dSumaImporte = 0;
        decimal dSumaImporteO = 0;
        protected void gvMantenimiento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string sRubroSelect = string.Empty;
            string sProvGSelect = string.Empty;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataTable dt = dtGastosMEX;

                    ImageButton btnGastoE = (ImageButton)e.Row.FindControl("btnEliminarMEX");
                    if (btnGastoE != null)
                    {
                        btnGastoE.Visible = dt.Rows[e.Row.RowIndex]["IdTipoGasto"].S() == "2" ? true : false;
                    }

                    TextBox txtNoPierna = (TextBox)e.Row.FindControl("txtNoPierna");
                    TextBox txtTrip = (TextBox)e.Row.FindControl("txtNoTripMEX");
                    TextBox txtImp = (TextBox)e.Row.FindControl("txtImporte");
                    if //(txtNoPierna != null && txtTrip != null && 
                        (txtImp != null)
                    {
                        if (dt != null)
                        {
                            txtImp.Text = dt.Rows[e.Row.RowIndex]["ImporteModificado"].S();
                            txtImp.Attributes["onfocus"] = "javascript:this.select();";
                        }
                    }

                    dSumaImporte += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"));
                    dSumaImporteO += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ImporteModificado"));

                    DropDownList ddlTipoGasto = (DropDownList)e.Row.FindControl("ddlTipoGasto");
                    //DropDownList ddlAcumulado1 = (DropDownList)e.Row.FindControl("ddlAcumulado1");

                    if (ddlTipoGasto != null)
                    {
                        ddlTipoGasto.DataSource = dtTiposGasto;
                        ddlTipoGasto.DataTextField = "Descripcion";
                        ddlTipoGasto.DataValueField = "Valor";
                        ddlTipoGasto.DataBind();

                        if (dt.Rows[e.Row.RowIndex]["TipoGasto"].S() != "")
                        {
                            ddlTipoGasto.SelectedValue = dt.Rows[e.Row.RowIndex]["TipoGasto"].S();
                            //CargaComboAcumuladoGasto(ddlAcumulado1, ObtieneAumuladosGasto1(dt.Rows[e.Row.RowIndex]["TipoGasto"].S()));
                            //ddlAcumulado1.SelectedValue = dt.Rows[e.Row.RowIndex]["AmpliadoGasto"].S();
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

                    DropDownList ddlProvG = (DropDownList)e.Row.FindControl("ddlProvG");
                    if (ddlProvG != null)
                    {
                        ddlProvG.DataSource = dtProveedor;
                        ddlProvG.DataTextField = "Descripcion";
                        ddlProvG.DataValueField = "IdProveedor";
                        ddlProvG.DataBind();

                        //sProvGSelect = dt.Rows[e.Row.RowIndex]["lblProv"].S();
                        Label lblProv = (Label)e.Row.FindControl("lblProv");
                        ddlProvG.SelectedItem.Text = lblProv.Text;
                    }

                    if (dtContratos != null)
                    {
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
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

                    ImageButton imbReferencia = (ImageButton)e.Row.FindControl("imbReferenciaPesos");
                    if (imbReferencia != null)
                    {
                        if (dt.Rows[e.Row.RowIndex]["Comprobante"].S().I() == 1)
                            imbReferencia.Visible = true;
                        else
                            imbReferencia.Visible = false;
                    }

                    Label lblNoPierna = (Label)e.Row.FindControl("lblNoPierna");
                    if (lblNoPierna != null)
                    {
                        lblNoPierna.Text = dt.Rows[e.Row.RowIndex]["NumeroPierna"].S();
                    }
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    //DataTable dt = dtGastosMEX;

                    e.Row.Cells[5].Text = dSumaImporteO.ToString("c");
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Text = dSumaImporte.ToString("c");
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[6].Font.Bold = true;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvMantenimiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMantenimiento_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMantenimiento_PreRender(object sender, EventArgs e)
        {

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
                    dtGastosMEX = ds.Tables[0];
                    dtGastosUSA = null;
                    dtContratos = ds.Tables[1];

                    gvMantenimiento.DataSource = dtGastosMEX;
                    gvMantenimiento.DataBind();

                    pnlRubros.Visible = true;
                }
                ds.Dispose();
                dtGastosMEX.Dispose();
                GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public event EventHandler eUpaComprobanteMXN;
        public event EventHandler eUpaComprobanteUSD;
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
        #endregion

        protected void btnActualizarComprobantes_Click(object sender, EventArgs e)
        {

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

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

                    lblClaveCliente.Text = "Clave cliente: " + lstCliente[0];
                    lblNombreCliente.Text = "Nombre cliente: " + lstCliente[1];
                    lblMatriculaMEX.Text = "Matrícula: " + lstCliente[2];

                    lblReqMes.Text = NombreMes;
                    lblReqAnio.Text = iAnio.S();

                    pnlActualizar.Visible = true;
                    pnlRubros.Visible = true;

                    if (eObjSelected != null)
                        eObjSelected(sender, e);

                    //lblCentroCostosMEX.Text = "Centro de costos: " + sCentroCostos;

                    upaPrincipal.Update();
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

        protected void btnBuscarPierna_Click(object sender, ImageClickEventArgs e)
        {

        }

        

        
    }
}