<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmComprobanteGastos.aspx.cs" EnableEventValidation="false" Inherits="ClientesCasa.Views.Gastos.frmComprobanteGastos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function On(GridView) {
            if (GridView != null) {
                GridView.originalBgColor = GridView.style.backgroundColor;
                GridView.style.backgroundColor = "#C0BCBC";
            }
        }

        function Off(GridView) {
            if (GridView != null) {
                GridView.style.backgroundColor = GridView.originalBgColor;
            }
        }

        function UserDeleteConfirmation() {
            if (confirm("¿Realmente esta seguro de eliminar el comprobante?"))
                return true;
            else
                return false;
        }
        function OcultarModalArchivo() {

            var modalId = '<%=mpeArchivo.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function Redirecciona(cad) {
            location.href = cad;
        }
    </script>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>

    <!-- <script type="text/javascript">
        $(function () {
            $("[id$=txtPeriodo]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'http://www.cbtis46.edu.mx/wp-content/uploads/2014/09/calendario-150x150.png',
                format: 'mm/yyyy',
                language: 'ru'
            });
        });
    </script> -->
    <script type="text/javascript">
        $("[src*=down]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../Images/icons/up.png");
        });
        $("[src*=up]").live("click", function () {
            $(this).attr("src", "../../Images/icons/down.png");
            $(this).closest("tr").next().remove();
        });
    </script>


    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Comprobante de Gastos</h4>
    </div>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>--%>

    <asp:Panel ID="pnlBusqueda" runat="server">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de Clientes</h5>
            </div>
            <div class="widget-content nopadding">
                <div class="control-group">
                    <div class="controls">
                        <br />
                        <div class="table-responsive" style="margin: 5px;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div class="section group">
                                            <div class="col span_1_of_4" style="text-align: center;">
                                                <asp:Label ID="lblBusqueda" runat="server" Text="Busqueda:"></asp:Label>
                                            </div>
                                            <div class="col span_1_of_4" style="text-align: center;">
                                                <asp:TextBox ID="txtBusqueda" runat="server" MaxLength="98" Width="90%"></asp:TextBox>
                                            </div>
                                            <div class="col span_1_of_4" style="text-align: center;">
                                                <asp:DropDownList ID="ddlOpcBus" runat="server" Width="100%">
                                                    <asp:ListItem Text="Clave del cliente" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Clave de contrato" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Matricula" Value="3" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col span_1_of_4" style="text-align: center;">
                                                <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" OnClick="btnBuscarCliente_Click" CssClass="btn btn-success" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="table-responsive" style="margin: 5px;">
                                            <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="IdContrato"
                                                OnRowDataBound="gvClientes_RowDataBound" CssClass="table table-bordered table-striped table-hover" PageSize="5"
                                                OnPageIndexChanging="gvClientes_PageIndexChanging" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="Label50" runat="server" Text="No existen Registros."></asp:Label>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="ClaveCliente" HeaderText="Clave del cliente" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre del cliente" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="ClaveContrato" HeaderText="Contrato" />
                                                    <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlPeriodo" runat="server" Visible="false">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Comprobante de gastos</h5>
            </div>
            <div class="widget-content nopadding">
                <div class="control-group">
                    <div class="controls">
                        <br />
                        <div class="table-responsive" style="margin: 5px;">
                            <asp:Label ID="Label2" runat="server" Text="Seleccione el periodo"></asp:Label>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <div class="section group">
                                            <div class="col span_1_of_4">
                                                &nbsp;
                                            </div>
                                            <div class="col span_2_of_4" style="text-align: left;">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Text="Periodo:"></asp:Label></strong>
                                                <asp:TextBox ID="txtPeriodo" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                <label for="txtPeriodo" style="height: 24px; width: 24px" class="input-group-addon generic_btn">
                                                </label>
                                            </div>
                                            <div class="col span_1_of_4" style="text-align: left;">
                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-success" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="section group">
                                            <div class="col span_1_of_4">
                                                &nbsp;
                                            </div>
                                            <div class="col span_1_of_4">
                                                <strong>
                                                    <asp:Label ID="lblCustNum" runat="server" Text="Clave del cliente: "></asp:Label></strong>
                                                <asp:Label ID="lblRespCustNum" runat="server"></asp:Label>
                                            </div>
                                            <div class="col span_1_of_4">
                                                <strong>
                                                    <asp:Label ID="lblNombreCliente" runat="server" Text="Nombre del cliente: "></asp:Label></strong>
                                                <asp:Label ID="lblRespNombreCliente" runat="server"></asp:Label>
                                            </div>
                                            <div class="col span_1_of_4">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div class="section group">
                                            <div class="col span_1_of_4">
                                                &nbsp;
                                            </div>
                                            <div class="col span_1_of_4">
                                                <strong>
                                                    <asp:Label ID="lblClaveContrato" runat="server" Text="Clave del contrato: "></asp:Label></strong>
                                                <asp:Label ID="lblRespClaveContrato" runat="server"></asp:Label>
                                            </div>
                                            <div class="col span_1_of_4">
                                                <strong>
                                                    <asp:Label ID="lblMatricula" runat="server" Text="Matrícula: "></asp:Label></strong>
                                                <asp:Label ID="lblRespMatricula" runat="server"></asp:Label>
                                            </div>
                                            <div class="col span_1_of_4">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <br />
                                        <div>
                                            <table style="width: 100% !important;">
                                                <tr>
                                                    <td style="text-align: left !important;">
                                                        <strong>
                                                            <asp:Label ID="lblGastosPesos" runat="server" Text="Gastos en Pesos" Style="width: 190px !important;" /></strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center !important;">
                                                        <asp:Panel ID="pnlMXN" runat="server" Height="400px" ScrollBars="Vertical">
                                                            <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdGasto" Width="100%"
                                                                CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvGastos_RowCommand"
                                                                OnRowDataBound="gvGastos_RowDataBound">
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="Label50" runat="server" Text="No existen gastos para mostrar."></asp:Label>
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <img alt="Ver comprobantes de gastos" style="cursor: pointer" src="../../Images/icons/down.png" width="24" height="24" />
                                                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                                <div class="table-responsive" style="margin: 5px;">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="div<%# Eval("IdGasto") %>">
                                                                                                    <asp:GridView ID="gvArchivos" runat="server" AutoGenerateColumns="False" Font-Names="verdana"
                                                                                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                                                                                                        ForeColor="Black" GridLines="Vertical" Width="100%" DataKeyNames="Extension,Nombre"
                                                                                                        CssClass="tblGrid" OnRowCommand="gvArchivos_RowCommand">
                                                                                                        <RowStyle HorizontalAlign="Center" CssClass="rowGrvMain" />
                                                                                                        <AlternatingRowStyle CssClass="rowGrvAlterno" />
                                                                                                        <EmptyDataTemplate>
                                                                                                            <asp:Label ID="Label50" runat="server" Text="No existen comprobantes para mostrar."></asp:Label>
                                                                                                        </EmptyDataTemplate>
                                                                                                        <FooterStyle CssClass="rowGrvFooter" />
                                                                                                        <HeaderStyle CssClass="thGrv" />
                                                                                                        <PagerStyle CssClass="pagerStyle" />
                                                                                                        <SelectedRowStyle CssClass="selectRowStyle" />
                                                                                                        <EmptyDataRowStyle CssClass="rowGrvAlterno" />
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre del archivo" />
                                                                                                            <asp:BoundField DataField="Extension" HeaderText="Extensión" />
                                                                                                            <asp:BoundField DataField="Ruta" HeaderText="URL" />
                                                                                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                   <table style="width:100%">
                                                                                                                       <tr>
                                                                                                                           <td style="width:50%">
                                                                                                                               <asp:ImageButton ID="imbDescargar" runat="server" CommandName="Descargar" Width="24px" Height="24px" ToolTip="Descargar el comprobante"
                                                                                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/Images/icons/download.png" />
                                                                                                                           </td>
                                                                                                                           <td style="width:50%">
                                                                                                                               <asp:UpdatePanel ID="upaImgView" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:ImageButton ID="imgbtnView" runat="server" CommandName="ViewDoc" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                                                                                            ImageUrl="~/Images/icons/view.png" Width="25px" Height="25px" ToolTip="Mostrar Documento" />
                                                                                                                                    </ContentTemplate>
                                                                                                                                </asp:UpdatePanel>
                                                                                                                           </td>
                                                                                                                       </tr>
                                                                                                                   </table>
                                                                                                                    

                                                                                                                    
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                                <td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" />
                                                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                                                    <asp:BoundField DataField="Contrato" HeaderText="Contrato" />
                                                                    <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                                                    <asp:BoundField DataField="Importe" HeaderText="Monto" DataFormatString="{0:c}" />
                                                                    <asp:BoundField DataField="TipoMoneda" HeaderText="Moneda" />
                                                                    <asp:BoundField DataField="Concur" HeaderText="Número de Gasto de Concur" />
                                                                    <asp:TemplateField HeaderText="Agregar">
                                                                        <ItemTemplate>
                                                                            <div style="text-align: center">
                                                                                <asp:ImageButton ID="imbAgregar" runat="server" CommandName="Agregar" ImageUrl="~/Images/icons/upload.png" Width="24px" Height="24px"
                                                                                    ToolTip="Agrega un comprobante." CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <table style="width: 100% !important;">
                                                <tr>
                                                    <td style="text-align: left !important;">
                                                        <strong>
                                                            <asp:Label ID="lblGastosDolares" runat="server" Text="Gastos en Dolares" Style="width: 190px !important;" /></strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center !important;">
                                                        <asp:Panel ID="pnlUSD" runat="server" Height="400px" ScrollBars="Vertical">
                                                            <asp:GridView ID="gvGastosUSD" runat="server" AutoGenerateColumns="False" DataKeyNames="IdGasto" Width="100%"
                                                                CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvGastosUSD_RowCommand"
                                                                OnRowDataBound="gvGastosUSD_RowDataBound">
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="Label50" runat="server" Text="No existen gastos para mostrar."></asp:Label>
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <img alt="Ver comprobantes de gastos" style="cursor: pointer" src="../../Images/icons/down.png" width="24" height="24" />
                                                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                                <div class="table-responsive" style="margin: 5px;">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="div<%# Eval("IdGasto") %>">
                                                                                                    <asp:GridView ID="gvArchivosUSD" runat="server" AutoGenerateColumns="False" Font-Names="verdana"
                                                                                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0"
                                                                                                        ForeColor="Black" GridLines="Vertical" Width="100%" DataKeyNames="Extension,Nombre"
                                                                                                        CssClass="tblGrid" OnRowCommand="gvArchivosUSD_RowCommand">
                                                                                                        <RowStyle HorizontalAlign="Center" CssClass="rowGrvMain" />
                                                                                                        <AlternatingRowStyle CssClass="rowGrvAlterno" />
                                                                                                        <EmptyDataTemplate>
                                                                                                            <asp:Label ID="Label50" runat="server" Text="No existen comprobantes para mostrar."></asp:Label>
                                                                                                        </EmptyDataTemplate>
                                                                                                        <FooterStyle CssClass="rowGrvFooter" />
                                                                                                        <HeaderStyle CssClass="thGrv" />
                                                                                                        <PagerStyle CssClass="pagerStyle" />
                                                                                                        <SelectedRowStyle CssClass="selectRowStyle" />
                                                                                                        <EmptyDataRowStyle CssClass="rowGrvAlterno" />
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre del archivo" />
                                                                                                            <asp:BoundField DataField="Extension" HeaderText="Extensión" />
                                                                                                            <asp:BoundField DataField="Ruta" HeaderText="URL" />
                                                                                                            <asp:TemplateField HeaderText="Acciones">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:ImageButton ID="imbDescargar" runat="server" CommandName="Descargar" Width="24px" Height="24px" ToolTip="Descargar el comprobante"
                                                                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/Images/icons/download.png" />

                                                                                                                    <asp:ImageButton ID="imgbtnView" runat="server" CommandName="ViewDoc" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                                                                        OnClick="imgbtnView_Click" ImageUrl="~/Images/icons/view.png" Width="25px" Height="25px" ToolTip="Mostrar Documento" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                                <td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Referencia" HeaderText="Referencia" />
                                                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                                                    <asp:BoundField DataField="Contrato" HeaderText="Contrato" />
                                                                    <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                                                    <asp:BoundField DataField="Importe" HeaderText="Monto" DataFormatString="{0:c}" />
                                                                    <asp:BoundField DataField="TipoMoneda" HeaderText="Moneda" />
                                                                    <asp:BoundField DataField="Concur" HeaderText="Número de Gasto de Concur" />
                                                                    <asp:TemplateField HeaderText="Agregar">
                                                                        <ItemTemplate>
                                                                            <div style="text-align: center">
                                                                                <asp:UpdatePanel ID="upaAddComprobante" runat="server" UpdateMode="Conditional">
                                                                                    <ContentTemplate>
                                                                                        <asp:ImageButton ID="imbAgregar" runat="server" CommandName="Agregar" ImageUrl="~/Images/icons/upload.png" Width="24px" Height="24px"
                                                                                        ToolTip="Agrega un comprobante." CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <%--Modal para cargar archivo--%>
    <asp:HiddenField ID="hdTargetArchivo" runat="server" />
    <cc1:ModalPopupExtender ID="mpeArchivo" runat="server" TargetControlID="hdTargetArchivo"
        PopupControlID="pnlArchivo" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlArchivo" runat="server" BackColor="White" Height="150"
        Width="500" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaArchivo" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="Label3" runat="server" Text="Seleccione el archivo"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label4" runat="server" Text="Archivo:" CssClass="lblInput"></asp:Label>
                        </td>
                        <td width="75%" style="text-align: left">
                            <asp:FileUpload ID="fuArchivo" runat="server" CssClass="btn btn-success" Style="width: 321px !important;" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="lblErrorArchivo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 18%">&nbsp;</td>
                        <td style="width: 40%">
                            <div style="text-align: center; float: center">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAceptarArchivo" runat="server" Text="Subir Archivo" OnClick="btnAceptarArchivo_Click" OnClientClick="OcultarModalArchivo();" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 37%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarArchivo" runat="server" Text="Cancelar" OnClientClick="OcultarModalArchivo();" CssClass="btn btn-default" />
                            </div>
                        </td>
                        <td style="width: 10%">&nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAceptarArchivo" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdateProgress ID="prgLoadingStatus" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress">
                    <div id="theprogress">
                        <asp:Image ID="imgWaitIcon" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/icons/loading-animated.gif" Width="150" Height="130" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
