<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmAnalisisCostoOpe.aspx.cs" EnableEventValidation="false" UICulture="es" Culture="es-MX" Inherits="ClientesCasa.Views.Reportes.frmAnalisisCostoOpe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
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
            if (confirm("¿Realmente esta seguro de eliminar el gasto?"))
                return true;
            else
                return false;
        }

        function OcultarModal() {
            var txtTasa = '<%=txtPeriodo.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%=mpePeriodo.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

    </script>

    <style type="text/css">
        table.TablaReporte {
            border: 0;
            background-color: #ffffff;
            width: 100%;
            border-color: #000000;
            border-spacing: 0;
            font-size: 10.0pt;
            font-family: Calibri;
            background: white;
        }

            table.TablaReporte td {
                border-bottom: none;
                border-left: none;
                border-right: none;
                vertical-align: middle;
                background-color: #EEEFF0;
                color: black;
                padding: 4px 3px;
            }

            table.TablaReporte tr {
                background-color: #0D2240;
                color: #ffffff;
                font-weight: bold;
            }

            table.TablaReporte footer {
                font-size: 14px;
                font-weight: bold;
                color: #FFFFFF;
                background-color: #0D2240;
            }

        .tdrlr {
            background-color:#0D2240;
        }
        .tdrlr:hover {
            background-color:#0D2240;
        }

        .LabelReporteHead {
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size: 10pt;
            color: #ffffff;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>


            <div style="text-align: left">
                <h4>&nbsp;&nbsp;Estado de Cuenta</h4>
            </div>
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
                                <asp:Panel ID="pnlBusqueda" runat="server" DefaultButton="btnBuscarCliente">
                                    <table style="width: 100%" class="table table-striped">
                                        <tr>
                                            <td style="text-align: center">
                                                <div class="section group" style="margin-left: -30px;">
                                                    <div class="col span_1_of_4">
                                                        <asp:Label ID="lblBusqueda" runat="server" Text="Busqueda:" CssClass="titleCampo"></asp:Label>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:TextBox ID="txtBusqueda" runat="server" MaxLength="98" Width="90%"></asp:TextBox>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:DropDownList ID="ddlOpcBus" runat="server" Width="100%">
                                                            <asp:ListItem Text="Clave Cliente" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Clave Contrato" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Matrícula" Value="4" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Solo Activos" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Inactivos" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" OnClick="btnBuscarCliente_Click" CssClass="btn btn-success" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div class="table-responsive" style="margin: 5px;">
                                    <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" DataKeyNames="Matricula"
                                        AllowPaging="True" Width="100%" CssClass="table table-bordered table-striped table-hover"
                                        OnRowDataBound="gvClientes_RowDataBound" PageSize="6"
                                        OnPageIndexChanging="gvClientes_PageIndexChanging" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label50" runat="server" Text="No existen Registros."></asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="ClaveCliente" HeaderText="Clave del Cliente" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre del cliente" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="ClaveContrato" HeaderText="Clave del contrato" />
                                            <asp:BoundField DataField="Matricula" HeaderText="Aeronave matricula" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />

                    <div class="control-group">
                        <div class="controls">
                            <asp:UpdatePanel ID="upaReporte" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right; float: right">
                                                <asp:Button ID="btnGenerar" runat="server" Text="Exportar" Visible="false" CssClass="btn btn-success" OnClick="btnGenerar_Click" /><p>&nbsp;</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>

                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlReporte" runat="server" Visible="false" Width="100%">
                                        <table style=" width: 100%; border: 0; background-color: #0D2240;">
                                            <tr class="tdrlr">
                                                <td style="width: 20%">
                                                    <table border="0" style="width: 100%; text-align: center;">
                                                        <%--<tr class="tdrlr">--%>
                                                        <tr>
                                                            <td style="text-align:left; position:relative;">
                                                                <div>
                                                                    <asp:Image ID="imgLogoReporte" runat="server" Height="100" Width="176" ImageUrl="http://192.168.1.231/MexJet360_Produccion/img/colors/blue/logo-ale.png" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 80%" colspan="2">
                                                    <table style="width: 100%; background-color: #0D2240; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                        <tr class="tdrlr">
                                                            <td style="text-align: center; width: 60%">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="text-align: center; width: 60%;">
                                                                <asp:Label ID="lblEmpresa" runat="server" Text="Estado de Cuenta" CssClass="LabelReporteHead" Font-Size="12pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="text-align: center">
                                                                <asp:Label ID="lblNombreCliente" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="text-align: center">&nbsp;</td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="text-align: center">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="tdrlr">
                                                <td style="width:20%"> 
                                                    <table style="width:100%">
                                                        <tr class="tdrlr">
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                Matricula:
                                                            </td>
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                <asp:Label ID="lblMatricula" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                Clave contrato:
                                                            </td>
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                <asp:Label ID="lblContrato" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                                <td style="width:60%"></td>
                                                <td style="width:20%">
                                                    <table style="width:100%">
                                                        <tr class="tdrlr">
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                Elaboró:
                                                            </td>
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                <asp:Label ID="lblElaboro" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                Fecha:
                                                            </td>
                                                            <td style="width:10%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                                <asp:Label ID="lblFecha" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table style="width:100%" >
                                            <tr>
                                                <td style="width:25%; text-align:left">
                                                    <table style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt;">
                                                        <tr style="width: 100%; background-color: #0D2240; color: #ffffff;">
                                                            <td style="text-align: center; width: 20%" colspan="2">
                                                                <asp:Label ID="lblTituloMonedaMXP" runat="server" Text="MXN" CssClass="LabelReporteHead"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="color:black;">
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblSaldoAntMXP" runat="server" Text="Saldo Anterior:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespSaldoAntMXP" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblPagCreditMXP" runat="server" Text="Pagos y Créditos:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespPagCreditMXP" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblNuevosCargosMXP" runat="server" Text="Nuevos Cargos:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespNuevosCargosMXP" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: left">
                                                                <asp:Label ID="lblSaldoActMXP" runat="server" Text="Saldo Actual:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespSaldoActMXP" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width:50%"></td>
                                                <td style="width:25%; text-align:right">
                                                    <table style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt;">
                                                        <tr style="width: 100%; background-color: #0D2240; color: #ffffff;">
                                                            <td style="text-align: center; width: 20%" colspan="2">
                                                                <asp:Label ID="lblTituloMonedaUSD" runat="server" Text="USD" CssClass="LabelReporteHead"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblSaldoAntUSD" runat="server" Text="Saldo Anterior:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespSaldoAntUSD" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblPagCreditUSD" runat="server" Text="Pagos y Créditos:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespPagCreditUSD" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblNuevosCargosUSD" runat="server" Text="Nuevos Cargos:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespNuevosCargosUSD" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblSaldoActUSD" runat="server" Text="Saldo Actual:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="lblRespSaldoActUSD" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table style="width:100%">
                                            <tr style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff; background-color: #0D2240;">
                                                <td style="width:15%">Fecha</td>
                                                <td style="width:15%">No. Referencia</td>
                                                <td style="width:20%">Detalle</td>
                                                <td style="width:35%">Proveedor</td>
                                                <td style="width:15%">Importe</td>
                                            </tr>
                                        </table>
                                        <table style="width:100%">
                                            <tr style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; font-weight:bold ">
                                                <td style="width:80%; text-align:right">
                                                    Total de gastos MXN
                                                </td>
                                                <td style="width:20%; text-align:right">
                                                    <asp:Label ID="lblTotalMXN" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="pnlGastosPesos" runat="server">
                                        </div>
                                        <br />
                                        <br />
                                        <table style="width:100%">
                                            <tr style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff; background-color: #0D2240;">
                                                <td style="width:15%">Fecha</td>
                                                <td style="width:15%">No. Referencia</td>
                                                <td style="width:20%">Detalle</td>
                                                <td style="width:35%">Proveedor</td>
                                                <td style="width:15%">Importe</td>
                                            </tr>
                                        </table>
                                        <table style="width:100%">
                                            <tr style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; font-weight:bold">
                                                <td style="width:80%; text-align:right">
                                                    Total de gastos USD
                                                </td>
                                                <td style="width:20%; text-align:right">
                                                    <asp:Label ID="lblTotalUSD" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="pnlGastosUSD" runat="server">
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnGenerar" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />


    <%-- Modal de Periodo --%>
    <asp:HiddenField ID="hdTargetPeriodo" runat="server" />
    <cc1:ModalPopupExtender ID="mpePeriodo" runat="server" TargetControlID="hdTargetPeriodo"
        PopupControlID="pnlPeriodo" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPeriodo" runat="server" BackColor="White" Height="150px"
        Width="280px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" class="thGrv">
                            <asp:Label ID="Label2" runat="server" Text="Seleccione el periodo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30%">
                            <asp:Label ID="Label1" runat="server" Text="Periodo:" CssClass="lblInput"></asp:Label>
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtPeriodo" type="date" runat="server" placeholder="MM/YYYY" CssClass="form-control"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarPeriodo" runat="server" Text="Aceptar" OnClientClick="OcultarModal();" OnClick="btnAceptarPeriodo_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="OcultarModal();" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
