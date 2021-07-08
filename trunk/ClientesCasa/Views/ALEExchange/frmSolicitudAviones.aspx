<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmSolicitudAviones.aspx.cs" Inherits="ClientesCasa.Views.ALEExchange.frmSolicitudAviones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function DeleteConfirmation() {
            if (confirm("¿Realmente esta seguro de cancelar la solicitud?"))
                return true;
            else
                return false;
        }
        function OcultarSolicitud() {
            var modalId = '<%=mpeAltaSolicitud.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
    </script>
    <style type="text/css">
        .Comentarios {
            width:97%
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <h4>&nbsp;&nbsp;<asp:Label ID="lblTitulo" runat="server" Text="Solicitudes de Avión" CssClass="company"></asp:Label></h4>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-align-justify"></i></span>
            <h5>Solicitudes</h5>
        </div>
        <div class="widget-content nopadding">
            <table style="width: 100%">
                <tr>
                    <td align="right" style="padding-right:8%;">
                        <asp:Button ID="btnAgregarSolicitud" runat="server" Text="Solicitar Avión" CssClass="btn btn-success" OnClick="btnAgregarSolicitud_Click" style="width:130px !important;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <div>
                        <asp:UpdatePanel ID="upaRubros" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="False" Width="85%" DataKeyNames="IdSolicitud"
                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvSolicitudes_RowCommand">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label50" runat="server" Text="No existen Registros para mostrar."></asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="Matricula" HeaderText="Descripción" />
                                        <asp:BoundField DataField="Persona" HeaderText="Persona contacto" />
                                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                        <asp:BoundField DataField="Acepta" HeaderText="¿Acepto?" />
                                        <asp:BoundField DataField="EstatusDesc" HeaderText="Estatus" />
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:ImageButton ID="imbEditar" runat="server" ImageUrl="~/Images/icons/edit.png" ToolTip="Edita una solicitud."
                                                        CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24px" Width="24px" />
                                                    <asp:ImageButton ID="imbCancelar" runat="server" ImageUrl="~/Images/icons/delete.png" ToolTip="Cancela una solicitud."
                                                        CommandName="Cancelar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24px" Width="24px" OnClientClick="return DeleteConfirmation();"/>
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
        </div>
    </div>



    <asp:HiddenField ID="hdTargetAltaSolicitud" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAltaSolicitud" runat="server" TargetControlID="hdTargetAltaSolicitud"
        PopupControlID="pnlAltaSolicitud" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlAltaSolicitud" runat="server" BackColor="White" Height="380px"
        Width="600px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaAltaSolicitud" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="4">
                            <h3><asp:Label ID="lblTituloSol" runat="server" Text="Solicitud de Avión"></asp:Label></h3>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left">
                            <asp:Label ID="lblFlota" runat="server" Text="Flota:"></asp:Label>
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:DropDownList ID="ddlFlota" runat="server" OnSelectedIndexChanged="ddlFlota_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%; text-align: left">
                            <asp:Label ID="lblMatricula" runat="server" Text="Matricula:" />
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:DropDownList ID="ddlMatricula" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblPersona" runat="server" Text="Persona:"></asp:Label>
                        </td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtPersona" runat="server" Width="97%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFechaInicio" runat="server" type="date" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Fin:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFechaFin" runat="server" type="date" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblPrestamo" runat="server" Text="¿Acepto?"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPrestamo" runat="server">
                                <asp:ListItem Text="Aprobado" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Negado" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="4">
                            <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="4">
                            <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" Rows="3" CssClass="Comentarios"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnGuardarSol" runat="server" Text="Guardar" OnClick="btnGuardarSol_Click"
                                     CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="OcultarSolicitud();"
                                     CssClass="btn btn-default"/>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upAltaSol" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaAltaSolicitud">
            <ProgressTemplate>
                <div style="text-align:left">
                    <asp:Label ID="lblProgressAlta" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>


</asp:Content>
