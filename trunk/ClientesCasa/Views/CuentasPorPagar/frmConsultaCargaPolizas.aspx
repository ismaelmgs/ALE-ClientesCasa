<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmConsultaCargaPolizas.aspx.cs" Inherits="ClientesCasa.Views.CuentasPorPagar.frmConsultaCargaPolizas" 
    UICulture="es" Culture="es-MX" UnobtrusiveValidationMode="None" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .validar {
            color:#ff0000;
            font-family:'Arial Rounded MT';
            font-size:10pt;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>

    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card" style="">
                        
                        <div class="card-header" data-background-color="blue">
                            <h4 class="title">Consulta Cargas de Pólizas</h4>    
                        </div>

                        <asp:UpdatePanel ID="upaPrincipal" runat="server">
                            <ContentTemplate>

                                <div class="card-content table-responsive">

                                    <fieldset>
                                        <legend><p class="category">Búsqueda</p></legend>

                                        <div class="row">
                                            <div class="col-md-12">

                                                <div style="width:100%; padding-bottom:20px;" align="center">
                                                    <table border="0" class="table-responsive" width="70%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha inicial"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFechaInicio" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                                <label for="txtFechaInicio" style="height:24px; width:24px" class="input-group-addon generic_btn"></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFechaFinal" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                                <label for="txtFechaFinal" style="height:24px; width:24px" class="input-group-addon generic_btn"></label>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info" ValidationGroup="groupFechas" OnClick="btnBuscar_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:RequiredFieldValidator ID="rqFechaIni" runat="server" ErrorMessage="*Ingresa la Fecha de Inicio"
                                                                    ControlToValidate="txtFechaInicio" Display="Dynamic" ValidationGroup="groupFechas" CssClass="validar">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td colspan="2" align="center">
                                                                <asp:RequiredFieldValidator ID="rqFechaFin" runat="server" ErrorMessage="*Ingresa la Fecha Final"
                                                                    ControlToValidate="txtFechaFinal" Display="Dynamic" ValidationGroup="groupFechas" CssClass="validar">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </div>

                                            </div>
                                        </div>

                                    </fieldset>

                                </div>

                                <div class="alert alert-success" role="alert" id="msgSuccesss" runat="server" style="width:95%; height:auto;" visible="false">
                                    <strong>Correcto!&nbsp;</strong> <asp:Label ID="lblSuccess" runat="server" Text="" Font-Size="9pt" ForeColor="Green"></asp:Label>
                                </div>
                                <div class="alert alert-danger" role="alert" id="msgError" runat="server"  style="width:95%; height:auto;" visible="false">
                                    <strong>Error!&nbsp;</strong><asp:Label ID="lblError" runat="server" Text="" Font-Size="9pt" ForeColor="Red"></asp:Label>
                                </div>

                                <div class="card-content table-responsive">
                                    <fieldset>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div align="center" style="overflow-y:auto; max-height:400px; width:100%; overflow-x:auto;">
                                                    <asp:GridView ID="gvDatosCargas" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover"
                                                        OnRowCommand="gvDatosCargas_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="Folio" HeaderText="Folio" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="Archivo" HeaderText="Nombre Archivo" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvLeft" />
                                                            <asp:BoundField DataField="FechaInicial" HeaderText="Fecha Inicial" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="FechaFinal" HeaderText="Fecha Final" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="Responsable" HeaderText="Responsable" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvLeft" />
                                                            <asp:BoundField DataField="FechaCarga" HeaderText="Fecha Carga" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="Procesado" HeaderText="Estatus" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="gvCenter" HeaderStyle-CssClass="gvCenter">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="upaLinkRef" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:ImageButton ID="imgVisualizar" runat="server" ImageUrl="~/Images/icons/view.png" OnClick="imgVisualizar_Click"
                                                                                style="width:20px; height:20px;" ToolTip="Visualizar datos de la carga." CommandName="Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                            <asp:ImageButton ID="imgDescargar" runat="server" style="width:20px; height:20px;" ImageUrl="~/Images/icons/download.png" 
                                                                                CommandName="Descargar" ToolTip="Descargar datos a Excel." CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:ImageButton>
                                                                            <asp:ImageButton ID="imbProcesar" runat="server" style="width:20px; height:20px;" ImageUrl="~/Images/icons/ProcessInfo.png" 
                                                                                CommandName="Procesar" ToolTip="Procesar archivo." CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:ImageButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="imgDescargar" />                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="gvHeader" />
                                                        <AlternatingRowStyle CssClass="gvAlternate" />
                                                        <RowStyle CssClass="gvItemsRows" />
                                                        <FooterStyle CssClass="gvFooter" />
                                                        <PagerStyle CssClass="gvFooter" />
                                                        <EmptyDataTemplate>
                                                            No existen registros para mostrar.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <asp:Panel ID="pnlConsulta" runat="server" Visible="false">
                                                

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h6>Archivo: <asp:Label ID="lblNombreArchivo" runat="server" Text=""></asp:Label></h6> 
                                                        <h6>Fecha de Carga: <asp:Label ID="lblFechaCarga" runat="server" Text=""></asp:Label></h6> 
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12" align="right">
                                                        <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn btn-primary" OnClick="btnExportar_Click" />
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12" style="padding-bottom:20px;" align="center">
                                                        <div align="center" style="overflow-y:auto; max-height:400px; max-width:1080px; overflow-x:auto;">
                                                            <asp:GridView ID="gvConsultaCarga" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped table-hover"
                                                                Width="100%">
                                                                <%--<Columns>
                                                                    <asp:BoundField DataField="Folio" HeaderText="Folio" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvLeft" />
                                                                </Columns>--%>
                                                                <HeaderStyle CssClass="gvHeader" />
                                                                <AlternatingRowStyle CssClass="gvAlternate" />
                                                                <RowStyle CssClass="gvItemsRows" />
                                                                <FooterStyle CssClass="gvFooter" />
                                                                <PagerStyle CssClass="gvFooter" />
                                                                <EmptyDataTemplate>
                                                                    No existen registros para mostrar.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>

                                                    
                                            </asp:Panel>
                                        </div>

                                    </fieldset>
                                </div>


                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportar" />
                            </Triggers>
                        </asp:UpdatePanel>


                    </div>
                </div>
            </div>
        </div>
    </div>

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
