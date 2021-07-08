<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmExtraccionGastos.aspx.cs" Inherits="ClientesCasa.Views.Gastos.frmExtraccionGastos" UICulture="es" Culture="es-MX" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>


    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Extracción de Gastos</h4>
    </div>
    <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upaBusqueda" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlBusqueda" runat="server">
        <div class="widget-box"  style="width: 98%;margin-left: 10px;">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de Gastos</h5>
            </div>
            <div class="widget-content nopadding">

                <div class="control-group">
                    <div class="controls">
                        <br />
                        <div class="table-responsive" style="margin:5px;">
                        <table style="width: 100%" class="table table-striped">
                            <tr>
                                <td style="text-align: center">
                                    <div class="section group" style="margin-left:-30px;">
                                        <div class="col span_1_of_4">
                                            &nbsp; Periodo:<asp:Label ID="lblRequPeriodo" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                        </div>
	                                    <div class="col span_1_of_4">
                                            <asp:TextBox ID="txtPeriodo" type="date" runat="server" placeholder="MM/YYYY" CssClass="form-control"></asp:TextBox>
                                            
                                        </div>
                                        <div class="col span_1_of_4">
                                            Matrícula: <asp:Label ID="lblRequMatricula" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="col span_1_of_4">
                                            <asp:UpdatePanel ID="upaMatricula" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlMatricula" runat="server" placeholder="Seleccione">
                                                    </asp:DropDownList>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="section group" style="margin-left:-30px;">
                                        <div class="col span_1_of_4">
                                        </div>
	                                    <div class="col span_1_of_4">
                                            <asp:Label ID="lblReqPeriodo" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                                Visible="false"></asp:Label>
                                        </div>
                                        <div class="col span_1_of_4">
                                        </div>
                                        <div class="col span_1_of_4" style="text-align:right">
                                            <asp:Label ID="lblReqMatricula" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                                Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="section group" style="margin-left:-30px;">
                                        <div class="col span_4_of_4" style="text-align:right">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                                        </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div style="text-align:right;padding-right:10px;">
        <asp:Button ID="btnExport" runat="server" Text="Exportar XLS" OnClick="btnExport_Click" CssClass="btn btn-info" />
    </div>
    
    <asp:UpdatePanel ID="upaGastos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <asp:Panel ID="pnlGastos" runat="server" Width="98%" Style="margin-left:10px; overflow:auto;">
                <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="true"
                    CssClass="table table-bordered table-striped table-hover">
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <br />

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
