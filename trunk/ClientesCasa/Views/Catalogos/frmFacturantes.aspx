<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmFacturantes.aspx.cs" EnableEventValidation="false" Inherits="ClientesCasa.Views.Catalogos.frmFacturantes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .overlayy
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            filter: alpha(opacity=80);
            opacity: 0.8;
            background: rgba(0,0,0,0.8);
        }
    </style>

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

        function MatriculaDeleteConfirmation() {
            if (confirm("¿Realmente esta seguro de eliminar la matricula?"))
                return true;
            else
                return false;
        }

        function OcultarModalMatricula(modal) {
            var modalId = '<%=mpeMatricula.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
    </script>

    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Mantenimiento de Facturantes</h4>
    </div>
    <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlBusqueda" runat="server" DefaultButton="btnBuscarCliente">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de Clientes</h5>
            </div>
            <div class="widget-content nopadding">
        <table width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="section group">
	                    <div class="col span_1_of_4" style="text-align:center;">
                            <asp:Label ID="lblBusqueda" runat="server" Text="Busqueda:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div class="col span_1_of_4" style="text-align:center;">
                            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="txtInput" MaxLength="98" Width="90%"></asp:TextBox>
                        </div>
                        <div class="col span_1_of_4" style="text-align:center;">
                            <asp:DropDownList ID="ddlOpcion" runat="server" placeholder="Seleccione">
                                <asp:ListItem Text="Clave Cliente" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Nombre Cliente" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col span_1_of_4" style="text-align:center;">
                            <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" OnClick="btnBuscarCliente_Click" CssClass="btn btn-success" style="margin-top: -1px;" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        CssClass="table table-bordered table-striped table-hover"
                        Width="98%" OnRowDataBound="gvClientes_RowDataBound" PageSize="6"
                        OnPageIndexChanging="gvClientes_PageIndexChanging" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged" style="margin-left: 1%;">
                        <EmptyDataTemplate>
                            <asp:Label ID="Label50" runat="server" Text="No existen Registros."></asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="cust_num" HeaderText=" Código " />
                            <asp:BoundField DataField="CardCode" HeaderText=" Código " />
                            <asp:BoundField DataField="CardName" HeaderText=" Nombre " />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

    </asp:Panel>

    <br />
    <div style="width: 95%; margin-left:2%;" >
        <center>
        <asp:Panel ID="pnlFacturantes" runat="server" Visible="false">
            <fieldset style="width: 100%">
<div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Facturantes - Contratos</h5>
            </div>
            <div class="widget-content nopadding">  
                <table style="width:100%;">
                    <tr>
                        <td style="width:100%">
                            <div class="section group">
	                            <div class="col span_2_of_4">
                                    <strong><asp:Label ID="lblCustNum" runat="server" Text="Clave del cliente: "></asp:Label></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRespCustNum" runat="server"></asp:Label><br />
                                    <strong><asp:Label ID="lblNombreCliente" runat="server" Text="Nombre del cliente: "></asp:Label></strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRespNombreCliente" runat="server"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                                <div class="col span_1_of_4" style="text-align:right;">
                                    <asp:Button ID="btnAgregarMatricula" runat="server" Text="Agregar Matricula" OnClick="btnAgregarMatricula_Click" CssClass="btn btn-success" style="width:130px !important;" />
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_4_of_4">
                                    <asp:GridView ID="gvContratos" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="IdFacturante" AllowPaging="True" Width="100%"
                                        CssClass="table table-bordered table-striped table-hover"
                                        PageSize="6" OnRowCommand="gvContratos_RowCommand">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label50" runat="server" Text="No existen Registros para mostrar."></asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="cust_num" HeaderText="Nombre del cliente" />
                                            <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                            <asp:BoundField DataField="ClaveContrato" HeaderText="Clave Contrato" />
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:ImageButton ID="imbEliminar" runat="server" CommandName="Eliminar" OnClientClick="return MatriculaDeleteConfirmation();" Height="24" Width="24"
                                                            ImageUrl="~/Images/icons/delete.png" ToolTip="Elimina un contrato." CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        </center>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>


    <%-- Modal de Matriculas --%>
    <asp:HiddenField ID="hdTargetMatricula" runat="server" />
    <cc1:ModalPopupExtender ID="mpeMatricula" runat="server" TargetControlID="hdTargetMatricula"
        PopupControlID="pnlMatricula" BackgroundCssClass="overlayy" >
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlMatricula" runat="server" BackColor="White" Height="400px"
        Width="450px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <%--<asp:UpdatePanel ID="upaMatricula" runat="server">
            <ContentTemplate>--%>
        <table style="width: 100%">
            <tr>
                <td colspan="2" class="thGrv">
                    <h4><asp:Label ID="lblTituloMatricula" runat="server" Text="Lista de matriculas"></asp:Label></h4>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlMatriculas" runat="server" Width="100%" Height="300" ScrollBars="Auto">
                        <asp:GridView ID="gvMatriculas" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="table table-bordered table-striped table-hover" EnableViewState="true">
                            <Columns>
                                <asp:TemplateField HeaderText=" Seleccione ">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelecciona" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Matricula" HeaderText=" Matricula " />
                                <asp:BoundField DataField="ClaveContrato" HeaderText=" Clave Contrato " />
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros para mostrar
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblErrorMat" runat="server" Text="" CssClass=""></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td width="50%">
                    <div style="text-align: right; float: right">
                        <asp:Button ID="btnAceptarMatriculas" runat="server" Text="Aceptar" OnClick="btnAceptarMatriculas_Click" CssClass="btn btn-primary" />
                    </div>
                </td>
                <td width="50%">
                    <div style="text-align: left; float: left">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="OcultarModalMatricula();" CssClass="btn btn-default" />
                    </div>
                </td>
            </tr>
        </table>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </asp:Panel></div></div>

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
