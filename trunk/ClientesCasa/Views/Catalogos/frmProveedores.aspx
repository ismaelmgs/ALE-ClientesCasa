<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmProveedores.aspx.cs" Inherits="ClientesCasa.Views.Catalogos.frmProveedores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=down]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../Images/icons/up.png");
        });
        $("[src*=up]").live("click", function () {
            $(this).attr("src", "../../Images/icons/down.png");
            $(this).closest("tr").next().remove();
        });

        function OcultarModalCuentas()
        {
            var modalId = '';
            var modal = $find(modalId);
            modal.hide();
        }

        function DeleteConfirmation() {
            if (confirm("¿Realmente esta seguro de eliminar el registro?"))
                return true;
            else
                return false;
        }

    </script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <h4>&nbsp;&nbsp;<asp:Label ID="lblTitulo" runat="server" Text="Administración de Provedores" CssClass="company"></asp:Label></h4>
    <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Proveedores</h5>
            </div>
            <div class="widget-content nopadding">
    <table style="width: 100%">
        <tr>
            <td align="right" style="padding-right:8%;">
                <asp:Button ID="btnAgregarProv" runat="server" Text="Agregar Proveedor" CssClass="btn btn-success" OnClick="btnAgregarProv_Click" style="width:150px !important;" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <div>
                <asp:UpdatePanel ID="upaRubros" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="gvProveedores_RowDataBound" Width="55%" DataKeyNames="IdProveedor"
                            CssClass="table table-bordered table-striped table-hover">
                            <EmptyDataTemplate>
                                <asp:Label ID="Label50" runat="server" Text="No existen Registros."></asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                               
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="DescSts" HeaderText="Estatus" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <%--<asp:ImageButton ID="imbAgrgar" runat="server" ImageUrl="~/Images/icons/add.png" ToolTip="Agrega cuentas al rubro"
                                                OnClick="imbAgrgar_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24px" Width="24px" />--%>
                                            <asp:ImageButton ID="imbEditar" runat="server" ImageUrl="~/Images/icons/edit.png" ToolTip="Edita un rubro."
                                                OnClick="imbEditar_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24px" Width="24px" />
                                            <asp:ImageButton ID="imbEliminar" runat="server" ImageUrl="~/Images/icons/delete.png" ToolTip="Elimina un rubro."
                                                OnClick="imbEliminar_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24px" Width="24px" OnClientClick="return DeleteConfirmation();"/>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div></td>
            <td></td>
        </tr>
    </table>

    <%--Modal de Proveedores--%>
    <asp:HiddenField ID="hdTargetProveedoresAgrega" runat="server" />
    <cc1:ModalPopupExtender ID="mpeProvAgrega" runat="server" TargetControlID="hdTargetProveedoresAgrega"
        PopupControlID="pnlProveeoresAgrega" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlProveeoresAgrega" runat="server" BackColor="White" Height="205px"
        Width="330" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaModalproveedores" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <h3><asp:Label ID="lblTituloProveedor" runat="server" Text=""></asp:Label></h3>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 35%; text-align: left">
                            <asp:Label ID="lblProveedores" runat="server" Text="Proveedor:"></asp:Label>&nbsp<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtProveedor" runat="server" Width="95%"></asp:TextBox>
                        </td>
                        <td style="width: 5%; text-align: left">
                            <div style="text-align: left; float: left">
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            
                        </td>
                        <td style="text-align: left">
                            
                        </td>
                        <td></td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarProveedor" runat="server" Text="Aceptar" OnClick="btnAceptarProveedor_Click"
                                     CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" 
                                     CssClass="btn btn-default"/>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upProveedores" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaModalproveedores">
            <ProgressTemplate>
                <div style="text-align:left">
                    <asp:Label ID="lblProgressProv" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>


    <%--Modal de Cuentas--%>
    <%--<asp:HiddenField ID="hdTargetCuentas" runat="server" />
    <cc1:ModalPopupExtender ID="mpeCuentas" runat="server" TargetControlID="hdTargetCuentas"
        PopupControlID="pnlCuentas" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlCuentas" runat="server" BackColor="White" Height="310px"
        Width="480px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaModalCuentas" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" class="thGrv">
                            <h3><asp:Label ID="lblTituloAltaCuenta" runat="server" Text="Alta de cuentas"></asp:Label></h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblbPrefijo" runat="server" Text="Prefijo:"></asp:Label>

                        </td>
                        <td style="text-align: left; vertical-align:middle">
                            <asp:TextBox ID="txtPrefijo" runat="server" Width="85%" MaxLength="7" OnTextChanged="txtPrefijo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align:left; vertical-align:top">
                            <asp:Label ID="lblEjemploCuenta" runat="server" Text="Ejemplo: 1101-01" Font-Size="XX-Small" ForeColor="Gray" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td style="width: 20%; text-align: left">
                            <asp:Label ID="lblCuentas" runat="server" Text="Cuenta:"></asp:Label>
                        </td>
                        <td style="width: 80%; text-align:left; vertical-align:middle">
                            <asp:DropDownList ID="ddlCuentas" runat="server" AutoPostBack="true" Width="87%"
                                OnSelectedIndexChanged="ddlCuentas_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>

                        <td style="width: 20%; text-align: left">
                            <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
                        </td>
                        <td style="width: 80%; text-align:left; vertical-align:middle">
                            <asp:Label ID="lblNombreCuenta" runat="server" ></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left"></td>
                        <td style="text-align:left; vertical-align:middle">
                            <asp:TextBox ID="txtCuentas" runat="server" ReadOnly="true" Width="85%"></asp:TextBox>
                        </td>
                        <tr>
                            <td></td>
                            <td>
                            </td>
                        </tr>
                    <tr>
                        <td></td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%;">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarCuenta" runat="server" Text="Aceptar" OnClick="btnAceptarCuenta_Click"
                                    CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%;">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarCuenta" runat="server" Text="Cancelar" OnClick="btnCancelarCuenta_Click"
                                    CssClass="btn btn-default" OnClientClick="OcultarModalCuentas();" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblidRubros" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppCuentas" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaModalCuentas">
            <ProgressTemplate>
                <div style="text-align:left">
                    <asp:Label ID="lblProgresGastosEstimados" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>--%>
                </div></div>

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
