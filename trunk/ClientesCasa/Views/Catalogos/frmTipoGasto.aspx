<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmTipoGasto.aspx.cs" Inherits="ClientesCasa.Views.Catalogos.frmTipoGasto" %>

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
    <h4>&nbsp;&nbsp;<asp:Label ID="lblTitulo" runat="server" Text="Administración de Tipo de Gasto" CssClass="company"></asp:Label></h4>
    <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Tipo de Gasto</h5>
            </div>
            <div class="widget-content nopadding">
    <table style="width: 100%">
        <tr>
            <td align="right" style="padding-right:8%;">
                <asp:Button ID="btnAgregarTipoG" runat="server" Text="Agregar Tipo de Gasto" CssClass="btn btn-success" OnClick="btnAgregarTipoG_Click" style="width:165px !important;" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <div>
                <asp:UpdatePanel ID="upaTipoG" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTipoG" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                            OnRowDataBound="gvTipoG_RowDataBound" Width="55%" DataKeyNames="IdConcepto" OnPageIndexChanging="gvTipoG_PageIndexChanging"
                            CssClass="table table-bordered table-striped table-hover">
                            <EmptyDataTemplate>
                                <asp:Label ID="Label50" runat="server" Text="No existen Registros."></asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                               
                                <asp:BoundField DataField="Concepto" HeaderText="Descripción" />
                                <asp:BoundField DataField="DescSts" HeaderText="Estatus" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:ImageButton ID="imbEditar" runat="server" ImageUrl="~/Images/icons/edit.png" ToolTip="Edita un Tipo de Gasto."
                                                OnClick="imbEditar_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24px" Width="24px" />
                                            <asp:ImageButton ID="imbEliminar" runat="server" ImageUrl="~/Images/icons/delete.png" ToolTip="Elimina un Tipo de Gasto."
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
    <asp:HiddenField ID="hdTargetTipoGAgrega" runat="server" />
    <cc1:ModalPopupExtender ID="mpeTipoGAgrega" runat="server" TargetControlID="hdTargetTipoGAgrega"
        PopupControlID="pnlTipoGAgrega" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlTipoGAgrega" runat="server" BackColor="White" Height="205px"
        Width="330" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaModalTipoG" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <h3><asp:Label ID="lblTituloTipoG" runat="server" Text=""></asp:Label></h3>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 35%; text-align: left">
                            <asp:Label ID="lblTipoG" runat="server" Text="Tipo de Gasto:"></asp:Label>&nbsp<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width: 60%; text-align: left">
                            <asp:TextBox ID="txtTipoG" runat="server" Width="95%"></asp:TextBox>
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
                                <asp:Button ID="btnAceptarTipoG" runat="server" Text="Aceptar" OnClick="btnAceptarTipoG_Click"
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
        <asp:UpdateProgress ID="upTipoG" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaModalTipoG">
            <ProgressTemplate>
                <div style="text-align:left">
                    <asp:Label ID="lblProgressTipoG" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>

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
