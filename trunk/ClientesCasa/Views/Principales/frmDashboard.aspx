<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmDashboard.aspx.cs" Inherits="ClientesCasa.Views.Principales.frmDashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--Action boxes-->
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>

            <div class="container-fluid">
        <div class="quick-actions_homepage">
            <ul class="quick-actions">

                <li class="bg_lo">
                    <a href="frmDetailDash.aspx?opc=1">
                        <i class="icon-star"></i> 
                        <span class="label label-important"><asp:Label ID="lblCountAeronaves" runat="server"></asp:Label></span>Aeronaves
                    </a>
                </li>

                <li class="bg_lg">
                    <a href="frmDetailDash.aspx?opc=2">
                        <i class="icon-user"></i>
                        <span class="label label-important"><asp:Label ID="lblCountPilotos" runat="server"></asp:Label></span>Pilotos
                    </a>
                </li>

                <li class="bg_lb">
                    <a href="frmDetailDash.aspx?opc=3">
                        <i class="icon-dashboard"></i>
                        <span class="label label-important"><asp:Label ID="lblCountNewGastos" runat="server"></asp:Label></span> Nuevos Gastos
                    </a>
                </li>

                <li class="bg_lg">
                    <a href="frmDetailDash.aspx?opc=4">
                        <i class="icon-list-alt"></i>
                        <span class="label label-important"><asp:Label ID="lblCountVenContratos" runat="server"></asp:Label></span>Vencimiento Contratos
                    </a>
                </li>

                <li class="bg_ly">
                    <a href="frmDetailDash.aspx?opc=5">
                        <i class="icon-inbox"></i>
                        <%--<span class="label label-success">101</span> --%>
                        <span class="label label-important"><asp:Label ID="lblCountAclaraciones" runat="server"></asp:Label></span>Aclaraciones
                    </a>
                </li>

                <li class="bg_lo">
                    <a href="frmDetailDash.aspx?opc=6">
                        <i class="icon-th"></i> 
                        <span class="label label-important"><asp:Label ID="lblCountCobranza" runat="server"></asp:Label></span>Cobranza
                    </a>
                </li>

                <li class="bg_ls">
                    <a href="frmDetailDash.aspx?opc=7">
                        <i class="icon-signal"></i>
                        <span class="label label-important"><asp:Label ID="lblCountIncrementoTarifas" runat="server"></asp:Label></span>Incremento Tarifas
                    </a>
                </li>

                <li class="bg_lb">
                    <a href="frmDetailDash.aspx?opc=8">
                        <i class="icon-arrow-right"></i>
                        <span class="label label-important"><asp:Label ID="lblCountEnvioAlertas" runat="server"></asp:Label></span>Envío de Alertas
                    </a>
                </li>

                <li class="bg_lr">
                    <a href="frmDetailDash.aspx?opc=9">
                        <i class="icon-info-sign"></i> 
                        <span class="label label-important"><asp:Label ID="lblCountAlertas" runat="server"></asp:Label></span>Alertas
                    </a>
                </li>

            </ul>
        </div>
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>
<!--End-Action boxes-->   
    
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
