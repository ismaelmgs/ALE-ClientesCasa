<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmGastosSinAsignar.aspx.cs" Inherits="ClientesCasa.Views.Reportes.frmGastosSinAsignar" %>
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

        .tdrlr {
            background-color:#0D2240;
        }
        .tdrlr:hover {
            background-color:#0D2240;
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
                <h4>&nbsp;&nbsp;Gastos Sin Asignar</h4>
            </div>
            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-align-justify"></i></span>
                    <h5>Búsqueda de Gastos</h5>
                </div>
                <div class="widget-content nopadding">

                    <div class="control-group">
                        <div class="controls">

                            <br />
                            <div class="table-responsive" style="margin: 5px;">
                                <asp:Panel ID="pnlBusqueda" runat="server">

                                    <table style="width: 100%" class="table table-striped">
                                        <tr>
                                            <td style="text-align: center">
                                                <div class="section group" style="margin-left: -30px;">
                                                    <div class="col span_1_of_4">
                                                        <asp:Label ID="lblBusqueda" runat="server" Text="Busqueda:" CssClass="titleCampo"></asp:Label>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:DropDownList ID="ddlMes" runat="server">
                                                            <asp:ListItem Value="" Text=".:Seleccione:."></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                                            <asp:ListItem Value="9" Text="Septiembre"></asp:ListItem>
                                                            <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                                                            <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                                                            <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:DropDownList ID="ddlAnio" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:Button ID="btnBuscarGastos" runat="server" Text="Buscar" OnClick="btnBuscarGastos_Click" 
                                                            CssClass="btn btn-success" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>
                            </div>

                        </div>
                    </div>

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
                                        <table style=" width: 100%; border: 0; background-color: #0D2240;">
                                            <tr class="tdrlr" style="height:20px;">
                                                <td colspan="10"></td>
                                            </tr>
                                            <tr class="tdrlr">
                                                <td width="30%">
                                                    <asp:Image ID="imgLogoReporte" runat="server" ImageUrl="http://aerolineasejecutivas.com/img/logo-ale.png" />   
                                                </td>
                                                <td colspan="9" width="70%">
                                                    <div style="margin-left: 270px;">
                                                        <table border="0" style="background-color: #0D2240; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                            <tr class="tdrlr">
                                                                <td style="text-align: center;">&nbsp;</td>
                                                            </tr>
                                                            <tr class="tdrlr">
                                                                <td style="text-align: center;" colspan="9">
                                                                    <div style="margin-left:-170px;">
                                                                        <asp:Label ID="Label1" runat="server" Text="Aerolineas Ejecutivas S.A. de C.V." CssClass="LabelReporteHead" Font-Size="12pt"></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr class="tdrlr">
                                                                <td style="text-align: center;" colspan="9">
                                                                    <div style="margin-left:-200px;">
                                                                        <asp:Label ID="lblEmpresa" runat="server" Text="Gastos Sin Asignar" CssClass="LabelReporteHead" Font-Size="12pt"></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="tdrlr">
                                                <td width="80%" colspan="10">
                                                    <div style="text-align: left;">
                                                        <table style="width: 100%; background-color: #0D2240; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #ffffff;">
                                                            <tr class="tdrlr">
                                                                <td align="left" width="80%" colspan="8">
                                                                    <asp:Label ID="lblTituloMes" runat="server" Text="" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td width="20%" colspan="2">
                                                                    <asp:Label ID="lblTituloElaboro" runat="server" Text="" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr class="tdrlr">
                                                                <td align="left" colspan="8">
                                                                    <asp:Label ID="lblTituloAnio" runat="server" Text="" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblTituloFecha" runat="server" Text="" Font-Size="10pt"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="tdrlr">
                                                <td colspan="10"></td>
                                            </tr>
                                            <tr class="tdrlr" style="height:20px;">
                                                <td colspan="10"></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div id="pnlGastos" runat="server"></div>
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

</asp:Content>
