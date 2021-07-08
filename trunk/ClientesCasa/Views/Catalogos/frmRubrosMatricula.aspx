<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmRubrosMatricula.aspx.cs" UICulture="es" Culture="es-MX" EnableEventValidation="false" Inherits="ClientesCasa.Views.Catalogos.frmRubrosMatricula" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Configuración de Matricula</h4>
    </div>
    <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                &nbsp;
            </div>
            <div class="widget-content nopadding">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:Panel ID="pnlBusqueda" runat="server" DefaultButton="btnBuscarMatricula">
        <table style="width:100%">
                <tr>
                    <td>
                        <div class="section group" style="width:98%;">
	                        <div class="col span_1_of_4" style="text-align:center;">
                                <asp:Label ID="lblBusqueda" runat="server" Text="Busqueda:"></asp:Label>
                            </div>
                            <div class="col span_1_of_4" style="text-align:center;">
                                <asp:TextBox ID="txtBusqueda" runat="server" Width="90%"></asp:TextBox>
                            </div>
                            <div class="col span_1_of_4" style="text-align:center;">
                                <asp:DropDownList ID="ddlOpcBus" runat="server" CssClass="slcCombobox" Width="100%">
                                    <asp:ListItem Text="Matricula" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Serie" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Clave Cliente" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Clave Contrato" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Activos" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inactivos" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col span_1_of_4" style="text-align:center;">
                                <asp:Button ID="btnBuscarMatricula" runat="server" Text="Buscar" OnClick="btnBuscarMatricula_Click" CssClass="btn btn-success" />
                            </div>
                         </div>                       
                    </td>
                </tr>
            </table>
        <div>
        <table style="width:98%;margin-left:1%;">
                <tr>
                    <td>
                        <div class="table-responsive">
                            <asp:GridView ID="gvMatricula" runat="server" AutoGenerateColumns="False" DataKeyNames="IdAeronave" AllowPaging="True" Width="100%"
                                OnRowDataBound="gvMatricula_RowDataBound" CssClass="table table-bordered table-striped table-hover" PageSize="5"
                                OnPageIndexChanging="gvMatricula_PageIndexChanging" OnSelectedIndexChanged="gvMatricula_SelectedIndexChanged">
                                <EmptyDataTemplate>
                                    No existen Registros para mostrar.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Serie" HeaderText="Aeronave Serie"/>
                                    <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                    <asp:BoundField DataField="ClaveCliente" HeaderText="Número Cliente"/>
                                    <asp:BoundField DataField="ClaveContrato" HeaderText="Contratro" />
                                    <asp:BoundField DataField="NombreCliente" HeaderText="Nombre del Cliente" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <br />
    
        <asp:Panel ID="pnlPrincipalRubros" runat="server" Visible="false">
            <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Configuración - Rubros</h5>
            </div>
            <div class="widget-content nopadding">
            <fieldset style="width: 100%">
                <div class="section group">
                        <div class="col span_1_of_4" style="text-align:Left;">&nbsp;</div>
	                    <div class="col span_1_of_4" style="text-align:Left;">
                            <strong><asp:Label ID="lblSerie" runat="server" Text="Serie:"></asp:Label></strong>
                        </div>
                        <div class="col span_1_of_4" style="text-align:left;">
                            <asp:Label ID="readSerie" runat="server" Text="" ></asp:Label>
                        </div>
                        <div class="col span_1_of_4" style="text-align:Left;">&nbsp;</div>
                </div>
                <div class="section group">
                        <div class="col span_1_of_4" style="text-align:Left;">&nbsp;</div>
	                    <div class="col span_1_of_4" style="text-align:left;">
                            <strong><asp:Label ID="lblMatricula" runat="server" Text="Matricula:"></asp:Label></strong>
                        </div>
                        <div class="col span_1_of_4" style="text-align:left;">
                            <asp:Label ID="readMatricula" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col span_1_of_4" style="text-align:Left;">&nbsp;</div>
                </div>
                <div class="section group">
                        <div class="col span_1_of_4" style="text-align:Left;">&nbsp;</div>
	                    <div class="col span_1_of_4" style="text-align:left;">
                            <strong><asp:Label ID="lblCliente" runat="server" Text="Cliente:"></asp:Label></strong>
                        </div>
                        <div class="col span_1_of_4" style="text-align:left;">
                            <asp:Label ID="readCliente" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col span_1_of_4" style="text-align:Left;">&nbsp;</div>
                </div>
                <div class="section group">
                        <div class="col span_4_of_4" style="text-align:center;">
                            <asp:Panel ID="pnlRubros" runat="server" Width="100%" HorizontalAlign="Center" Style="margin-left: 5%;">
                                    <asp:GridView ID="gvRubros" runat="server" AutoGenerateColumns="False" DataKeyNames="IdRubro" 
                                        Width="90%" CssClass="table table-bordered table-striped table-hover">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblContratosEmpty" runat="server" Text="No existen Registros."></asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="IdRubro" HeaderText="IdRubro" Visible="false" />
                                            <asp:BoundField DataField="DescripcionRubro" HeaderText="Rubro" />
                                            <asp:TemplateField HeaderText="Seleccione">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblParticipacion" runat="server">
                                                        <asp:ListItem Selected="true" Text="Total" Value="1" />
                                                        <asp:ListItem Selected="false" Text="% Participación" Value="2" />
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            <br />
                            </div>
                </div>
                <div>
                    <p>&nbsp;</p>
                    <table style="width: 100%">
                        <tr>
                            <td style="width:100%">
                                <div style="text-align: right; float: right">
                                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>
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
