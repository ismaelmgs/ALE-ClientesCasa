<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmCostoHoraVuelo.aspx.cs" EnableEventValidation="false" UICulture="es" Culture="es-MX" Inherits="ClientesCasa.Views.Reportes.frmCostoHoraVuelo" %>
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
            <%--var txtInicio = '<%=txtFechaInicio.ClientID%>';
            txtInicio.value = "";

            var txtFin = '<%=txtFechaFin.ClientID%>';
            txtFin.value = "";--%>

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
                <h4>&nbsp;&nbsp;Costo por Hora de Vuelo</h4>
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
                                    </table>
                                    <asp:Panel ID="pnlReporte" runat="server" Visible="false" Width="100%">
                                        <table style=" width: 100%; border: 0">
                                            <tr class="tdrlr">
                                                <td style="width: 20%">
                                                    <table border="0" style="width: 100%; text-align: center;">
                                                        <tr class="tdrlr">
                                                            <td>
                                                                <asp:Image ID="imgLogoReporte" runat="server" ImageUrl="http://www.adelnorte.com.mx/esp/pv/images/default/fbo_ale.jpg" Width="140" Height="90" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 80%" colspan="2">
                                                    <table style="width: 100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                        <tr class="tdrlr">
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                            <td style="width:8.33%"></td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="text-align: center; background-color:#0D2240" colspan="9">
                                                                <asp:Label ID="lblEmpresa" runat="server" CssClass="LabelReporteHead" Font-Size="X-Large" Text="Resumen de Estado de Cuenta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;""></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td style="text-align: center" colspan="9">
                                                                
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="color:black; text-align:center">
                                                                TC. promedio:
                                                            </td>
                                                            <td style="text-align: left; color:black">
                                                                <asp:Label ID="lblTipoCambioProm" runat="server" Text="[TipoCambio]"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="tdrlr">
                                                            <td></td>
                                                            <td style="text-align: center; background-color:#0D2240" colspan="2">
                                                                <asp:Label ID="lblMatricula" runat="server"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td style="text-align: center; background-color:#0D2240"">
                                                                <asp:Label ID="lblMoneda" runat="server"></asp:Label>
                                                            </td>
                                                            <td></td>
                                                            <td style="text-align: center; background-color:#0D2240"" colspan="2">
                                                                <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="color:black; text-align:center"></td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <%--<table style="width:100%">
                                            <tr style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; 
                                                font-size: 10pt; color: #ffffff; text-align:center; background-color: #0D2240;">
                                                <td style="width:8.82%"></td>
                                                <td style="width:7.00%">ENERO</td>
                                                <td style="width:7.00%">FEBRERO</td>
                                                <td style="width:7.0%">MARZO</td>
                                                <td style="width:7.00%">ABRIL</td>
                                                <td style="width:7.00%">MAYO</td>
                                                <td style="width:7.00%">JUNIO</td>
                                                <td style="width:7.00%">JULIO</td>
                                                <td style="width:7.00%">AGOSTO</td>
                                                <td style="width:7.00%">SEPTIEMBRE</td>
                                                <td style="width:7.00%">OCTUBRE</td>
                                                <td style="width:7.00%">NOVIEMBRE</td>
                                                <td style="width:7.00%">DICIEMBRE</td>
                                                <td style="width:7.00%">TOTAL</td>
                                            </tr>
                                        </table>--%>
                                        <asp:GridView ID="gvMeses" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" Font-Names="Lucina Sans Regular, 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif">
                                            <HeaderStyle BackColor="#0D2240" ForeColor="White" Font-Size="11pt" />
                                            <RowStyle BackColor="#DDE8EE" Font-Size="9pt" />
                                            <Columns>
                                                <asp:BoundField DataField="Rubro" HeaderText="" ItemStyle-Width="8.82%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Enero" HeaderText="ENERO" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Febrero" HeaderText="FEBRERO" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Marzo" HeaderText="MARZO" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Abril" HeaderText="ABRIL" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Mayo" HeaderText="MAYO" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Junio" HeaderText="JUNIO" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Julio" HeaderText="JULIO" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Agosto" HeaderText="AGOSTO" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Septiembre" HeaderText="SEPTIEMBRE" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Octubre" HeaderText="OCTUBRE" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Noviembre" HeaderText="NOVIEMBRE" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Diciembre" HeaderText="DICIEMBRE" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                        
                                        <br />
                                        <div id="pnlGastosPesos" runat="server">
                                        </div>
                                        <br />

                                        <table style="width:100%">
                                            <tr>
                                                <td style="width:20%"></td>
                                                <td style="width:25%">
                                                     <table style="width:100%">
                                                         <tr>
                                                             <td style="text-align: right; background-color:#0D2240" colspan="2">
                                                                 <asp:Label ID="lblTituloFijos" runat="server" Text="Fijos" ForeColor="White"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td style="width:50%"> Promedio: </td>
                                                             <td style="width:50%">
                                                                 <asp:Label ID="lblPromedioFijo" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td style="width:50%">Promedio de Horas voladas:</td>
                                                             <td style="width:50%">
                                                                 <asp:Label ID="lblPromedioVoladasFijo" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td style="width:50%">Costo por hora de vuelo MN:</td>
                                                             <td style="width:50%">
                                                                 <asp:Label ID="lblCostoVueloFijo" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                     </table>
                                                </td>
                                                <td style="width:10%"></td>
                                                <td style="width:25%">
                                                    <table style="width:100%">
                                                        <tr>
                                                             <td style="text-align: center; background-color:#0D2240" colspan="2">
                                                                 <asp:Label ID="lblTituloVariable" runat="server" Text="Variables" ForeColor="White"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td style="width:50%">Promedio:</td>
                                                             <td style="width:50%">
                                                                 <asp:Label ID="lblPromedioVar" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td style="width:50%">Promedio de Horas voladas:</td>
                                                             <td style="width:50%">
                                                                 <asp:Label ID="lblPromedioVoladasVar" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td style="width:50%">Costo por hora de vuelo MN:</td>
                                                             <td style="width:50%">
                                                                 <asp:Label ID="lblCostoVueloVar" runat="server"></asp:Label>
                                                             </td>
                                                         </tr>
                                                     </table>
                                                </td>
                                                <td style="width:20%"></td>
                                            </tr>
                                        </table>

                                        <table style="width:100%; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', 'Geneva', 'Verdana', 'sans-serif'">
                                            <tr>
                                                <td style="text-align:left" colspan="12">
                                                    <asp:Label ID="lblElaboro" runat="server" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td style="text-align:right" colspan="2">
                                                    <asp:Label ID="lblFechaReporte" runat="server" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
        </ContentTemplate>
    </asp:UpdatePanel>


    <%-- Modal de Periodo --%>
    <asp:HiddenField ID="hdTargetPeriodo" runat="server" />
    <cc1:ModalPopupExtender ID="mpePeriodo" runat="server" TargetControlID="hdTargetPeriodo"
        PopupControlID="pnlPeriodo" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPeriodo" runat="server" BorderColor="Black" BackColor="White" Height="180px"
        Width="420px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label2" runat="server" Text="Seleccione el periodo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left; width:30%">
                            <asp:Label ID="lblMonedaReporte" runat="server" Text="Moneda:"></asp:Label>
                        </td>
                        <td style="text-align:left; width:40%">
                            <asp:DropDownList ID="ddlMonedaReporte" runat="server" Width="100">
                                <asp:ListItem Value="MXN" Text="MXN"></asp:ListItem>
                                <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left; width:30%"></td>
                    </tr>
                    <tr>
                        <td style="text-align:left; width:30%">
                            <asp:Label ID="Label1" runat="server" Text="Año del reporte:"></asp:Label>
                        </td>
                        <td style="text-align:left; width:40%">
                            <asp:DropDownList ID="ddlAnio" runat="server" Width="100">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left; width:30%">
                            <asp:CheckBox ID="chkAnioCompleto" runat="server" Text="¿Año completo?" AutoPostBack="true" Checked="true" OnCheckedChanged="chkAnioCompleto_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left; width:30%">
                            <asp:Label ID="lblMesFinal" runat="server" Text="Mes final:" Visible="false"></asp:Label>
                        </td>
                        <td style="text-align:left; width:40%">
                            <asp:DropDownList ID="ddlMesFinal" runat="server" Width="100" Visible="false">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:left; width:30%"></td>
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
