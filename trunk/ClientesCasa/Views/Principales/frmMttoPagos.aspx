<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmMttoPagos.aspx.cs" EnableEventValidation="false" UICulture="es" Culture="es-MX" Inherits="ClientesCasa.Views.Principales.frmMttoPagos" %>

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
            if (confirm("¿Realmente esta seguro de eliminar el pago?"))
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

        function OcultarModalE() {
            var txtTasa = '<%=ddlFacturante.ClientID%>';
            txtTasa.value = "";
            var txtTasa = '<%=txtReferencia.ClientID%>';
            txtTasa.value = "";
            var txtTasa = '<%=txtImporte.ClientID%>';
            txtTasa.value = "";
            var txtTasa = '<%=ddlTipoMoneda.ClientID%>';
            txtTasa.value = "";
            var txtTasa = '<%=txtFechaP.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%=mpeEstimados.ClientID%>';
            var modal = $find(modalId);
            modal.hide();

        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>
            <div style="text-align: left">
                <h4>&nbsp;&nbsp;Mantenimiento de pagos</h4>
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
                                    <table style="width:100%">
                                        <tr>
                                            <td>
                                                <div class="section group">
                                                    <div class="col span_1_of_4" style="text-align:center;">
                                                        <asp:Label ID="lblBusqueda" runat="server" Text="Búsqueda:"></asp:Label>
                                                    </div>
                                                    <div class="col span_1_of_4" style="text-align:center;">
                                                        <asp:TextBox ID="txtBusqueda" runat="server" MaxLength="98" Width="90%"></asp:TextBox>
                                                    </div>
                                                    <div class="col span_1_of_4" style="text-align:center;">
                                                        <asp:DropDownList ID="ddlOpcBus" runat="server" Width="100%">
                                                            <asp:ListItem Text="Clave Cliente" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Clave Contrato" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Matrícula" Value="4" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Solo Activos" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Inactivos" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col span_1_of_4" style="text-align:center;">
                                                        <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" OnClick="btnBuscarCliente_Click" CssClass="btn btn-success" style="margin-top:0px;" />
                                                    </div>
                                                    
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%"
                                                    OnRowDataBound="gvClientes_RowDataBound" CssClass="table table-bordered table-striped table-hover" PageSize="6"
                                                    OnPageIndexChanging="gvClientes_PageIndexChanging" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                                                    <EmptyDataTemplate>
                                                        No existen Registros para mostrar.
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave del Cliente" />
                                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre del cliente" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave del contrato" />
                                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula Aeronave" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <br />

            <asp:Panel ID="pnlFacturantes" runat="server" Visible="false">

                <table style="width: 100%">
                    <tr>
                        <td style="text-align:center !important;">
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar lista de pagos" CssClass="btn btn-success" OnClick="btnActualizar_Click" Visible="false" style="width:180px !important;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center !important;">
                            <br />
                            <strong><asp:Label ID="lblMex" runat="server" Text="Mes:" Font-Bold="true"></asp:Label></strong>
                            <asp:Label ID="lblReqMes" runat="server" Text="" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <strong><asp:Label ID="lblAnio" runat="server" Text="Año:" Font-Bold="true"></asp:Label></strong>
                            <asp:Label ID="lblReqAnio" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>

                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-align-justify"></i></span>
                        <h5>Pagos del cliente</h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="control-group">
                            <div class="controls">
                                <div style="text-align: right; margin-right: 15px;">
                                    <asp:Button ID="btnEstimados" runat="server" Text="Agregar Pago Estimado" OnClick="btnEstimados_Click" CssClass="btn btn-info" style="width:180px !important;"  />
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align:center !important;">
                                                <strong><asp:Label ID="lblCustNum" runat="server" Text="Clave del cliente: "></asp:Label>
                                                <asp:Label ID="lblRespCustNum" runat="server"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center !important;">
                                                <strong><asp:Label ID="lblNombreCliente" runat="server" Text="Nombre del cliente: "></asp:Label>
                                                <asp:Label ID="lblRespNombreCliente" runat="server"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center !important;">
                                                <strong><asp:Label ID="Label9" runat="server" Text="Matrícula:"></asp:Label>
                                                <asp:Label ID="lblRespMatricula" runat="server" Text=""></asp:Label></strong>
                                            </td>
                                        </tr>
                                        </table>
                                    <br />
                                    <table style=" width: 100%;">
                                        <tr>
                                            <td style="text-align: center;">
                                               
                                                <asp:Panel ID="pnlGrid" runat="server" Width="100%" Height="500" ScrollBars="Auto">
                                                    <asp:GridView ID="gvFacturantes" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        DataKeyNames="IdPago" CssClass="table table-bordered table-striped table-hover"
                                                        OnRowDataBound="gvFacturantes_RowDataBound" OnRowCommand="gvFacturantes_RowCommand" OnRowCreated="gvFacturantes_RowCreated">
                                                        <EmptyDataTemplate>
                                                            "No existen pagos del periodo seleccionado."
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            
                                                            <asp:BoundField DataField="ClaveFacturante" HeaderText="Clave del Facturante" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Facturante" HeaderText="Nombre del Facturante" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="DocNum" HeaderText="Número de pago SAP (Referencia)" />
                                                            <asp:BoundField DataField="DocDate" HeaderText="Fecha de Recibo" ItemStyle-HorizontalAlign="Center" />

                                                            <asp:TemplateField HeaderText="Importe Nuevo">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtImporteNuevo" runat="server" Text='<%# Bind("ImporteNuevo") %>' HeaderText="Importe Nuevo" Width="100" Style="text-align: right"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="ftbImporteNuevo" runat="server" FilterMode="ValidChars" ValidChars="0123456789.-," TargetControlID="txtImporteNuevo"></cc1:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="ImporteOriginal" HeaderText="Importe Original" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:BoundField DataField="Moneda" HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" />

                                                            <asp:TemplateField HeaderText="Pago estimado" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Images/icons/delete.png" OnClientClick="return UserDeleteConfirmation();"
                                                                        CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24" Width="24" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br /><asp:Button ID="btnGuardar" runat="server" Text=" Modificar " CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>


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
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="Label2" runat="server" Text="Seleccione el periodo"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="Label1" runat="server" Text="Periodo:"></asp:Label>
                        </td>
                        <td style="width: 70%; text-align: left">
                            <asp:TextBox ID="txtPeriodo" type="date" runat="server" placeholder="MM/YYYY" Width="80%" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblReqPeriodo" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left">
                            <asp:Label ID="lblValPeriodo" runat="server" Text="" ForeColor="Red" Font-Italic="true"></asp:Label>
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

    <%-- Modal de Estimados --%>
    <asp:HiddenField ID="hdTargetEstimados" runat="server" />
    <cc1:ModalPopupExtender ID="mpeEstimados" runat="server" TargetControlID="hdTargetEstimados"
        PopupControlID="pnlEstimados" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlEstimados" runat="server" BackColor="White"
        Width="370px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="Label3" runat="server" Text="Guardar Pago Estimado"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <asp:Label ID="Label4" runat="server" Text="Facturante:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:DropDownList ID="ddlFacturante" runat="server" Width="84%" OnSelectedIndexChanged="ddlFacturante_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%"></td>
                        <td style="text-align: left; width: 70%">
                            <asp:Label ID="lblValFacturante" runat="server" Text="" ForeColor="Red" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <asp:Label ID="Label5" runat="server" Text="Referencia:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txtReferencia" runat="server" Width="80%"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbReferencia" runat="server" TargetControlID="txtReferencia" FilterMode="ValidChars" ValidChars="0123456789 ABCDEFGHIJKLMNÑOPQRSTUVWXYZ abcdefghijklmnñopqrstuvwxyz -/">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%"></td>
                        <td style="text-align: left; width: 70%"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <asp:Label ID="Label6" runat="server" Text="Importe:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txtImporte" runat="server" Width="80%"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbImporte" runat="server" FilterMode="ValidChars" ValidChars="0123456789.-"
                                TargetControlID="txtImporte">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%"></td>
                        <td style="text-align: left; width: 70%">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtImporte" 
                            ForeColor="Red" Font-Italic="true" Display="Dynamic" ErrorMessage="El monto  debe ser un valor monetario." 
                            ValidationExpression="^[+-]?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$">
                        </asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <asp:Label ID="Label7" runat="server" Text="Tipo Moneda:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:DropDownList ID="ddlTipoMoneda" runat="server" Width="84%">
                                <asp:ListItem Text="MXN" Value="MXN" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <asp:Label ID="Label8" runat="server" Text="Fecha de pago:" CssClass="lblInput"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txtFechaP" type="date" runat="server" placeholder="MM/YYYY" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%"></td>
                        <td style="text-align: left; width: 70%">
                            <asp:Label ID="lblValFechaP" runat="server" Text="" ForeColor="Red" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="float: right">
                                <asp:Button ID="btnAceptarEstimado" runat="server" Text="Aceptar" OnClientClick="OcultarModalE();" OnClick="btnAceptarEstimado_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="float: left">
                                <asp:Button ID="btnCancelarEstimado" runat="server" Text="Cancelar" OnClientClick="OcultarModalE();" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
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
