<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmAprobacionFacturas.aspx.cs" Inherits="ClientesCasa.Views.ASC.frmAprobacionFacturas"
    UICulture="es" Culture="es-MX" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript">

        $("[src*=read_moreR]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../Images/icons/not_moreR.png");
        });
        $("[src*=not_moreR]").live("click", function () {
            $(this).attr("src", "../../Images/icons/read_moreR.png");
            $(this).closest("tr").next().remove();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=chkAll]').click(function () {
                alert('Entra evento check');
                $("[id*='chkFactura']").attr('checked', this.checked);
            });
        });
    </script>
    <style>
        .btn2 {
            /*width: auto !important;*/
            background-image: none;
            border-radius: 5px !important;
            box-shadow: none;
            min-height: 30px;
            -webkit-appearance: button;
            margin: 2px;

            color: #fff;
            text-shadow: 0 -1px 0 rgba(0,0,0,0.25);
            background-color: #49afcd;
            border-color: rgba(0,0,0,0.1) rgba(0,0,0,0.1) rgba(0,0,0,0.25);
            border: 1px solid #ccc;
        }

        .btn2:hover {
            color: #fff;
            background-color: #2f96b4;
            text-decoration: none;
            background-position: 0 -15px;
            transition: background-position .1s linear;
        }

        .hideColumn{
            display:none;
        }

        .table-bordered {
            border: 1px solid #ddd !important;
            border-collapse: separate;
            *border-collapse: collapse;
            border-left: 0;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
        }
        .table2 {
            width: 100%;
            margin-bottom: 20px;
        }

        table {
            max-width: 100%;
            background-color: transparent;
            border-collapse: collapse;
            border-spacing: 0;
        }

        .table2 th, .table2 td {
            padding: 8px;
            line-height: 20px;
            /*text-align: left;*/
            vertical-align: top;
            border-top: 1px solid #ddd;
        }

        .table2 th {
            height: auto;
            font-size: 10px;
            padding: 5px 10px 2px;
            border-bottom: 0;
            text-align: center;
            color: #666666;
            background-color: #e6e6e6 !important;
        }

        .table-bordered th, .table-bordered td {
            border-left: 1px solid #ddd;
        }
    </style>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>

            <div>
                <asp:Panel ID="pnlBusqueda" runat="server">
                    <fieldset>
                        <legend>
                            <div style="padding-left: 30px;">
                                <asp:Label ID="lblTitleBusqueda" runat="server" Text="Contabilizar Facturas"></asp:Label>
                            </div>
                        </legend>

                        <div class="col-md-12" align="center">
                            <table width="90%">
                                <tr>
                                    <td><%--<asp:Label ID="lblTitleEstatus" runat="server" Text="Estatus:"></asp:Label>--%></td>
                                    <td>
                                        <%--<asp:DropDownList ID="ddlEstatus" runat="server" OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="" Text=".:Selecciona:." Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Pendientes"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Enviados"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Abrobados"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                    <td colspan="2"><asp:Label ID="lblTitlePeriodo" runat="server" Text="Seleccione un período:"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblTitleTipoMtto" runat="server" Text="Tipo de Mtto:"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoMtto" runat="server">
                                            <asp:ListItem Value="" Text=".:Selecciona:." Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Preventivo"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Correctivo"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Reserva Motores"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Reserva Interiores"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label1" runat="server" Text="De:"></asp:Label>
                                        <asp:TextBox ID="txtDe" type="date" runat="server" CssClass="form-control" Placeholder="De"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label2" runat="server" Text="Hasta:"></asp:Label>
                                        <asp:TextBox ID="txtHasta" type="date" runat="server" CssClass="form-control" Placeholder="Hasta"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td><asp:Label ID="lblTitleFlota" runat="server" Text="Flota:"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlFlota" runat="server">
                                            <asp:ListItem Value="" Text=".:Selecciona:." Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="ALE" Text="Clientes Casa"></asp:ListItem>
                                            <asp:ListItem Value="MEX" Text="MexJet"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td colspan="2" align="left">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar Facturas" CssClass="btn2 bnt-info"
                                           OnClick="btnBuscar_Click" widht="150px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblTitleNoDoc" runat="server" Text="No. Documento:"></asp:Label></td>
                                    <td><asp:TextBox ID="txtNoDocumento" runat="server"></asp:TextBox></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </asp:Panel>
            </div>

            <div class="col-md-12" align="center">
                <asp:UpdatePanel ID="upaGridFacturas" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlFacturasEncontradas" runat="server">
                            <div style="overflow-y:auto; max-height:400px;">
                                <div style="height:30px;"></div>
                                <asp:GridView ID="gvFacturas" AutoGenerateColumns="false" runat="server" Width="95%" 
                                    CssClass="table2 table-bordered table-striped table-hover" OnRowDataBound="gvFacturas_RowDataBound"
                                    DataKeyNames="DocNum" OnRowCommand="gvFacturas_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvCenter">
                                            <HeaderTemplate>
                                                <label></label>
                                                <%--<asp:CheckBox ID="chkAll" runat="server" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFactura" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                
                                                <a href="#" onclick="SelectRowIndex(this);">
                                                <img alt="" style="cursor: pointer" width="24" height="24" src="../../Images/icons/read_moreR.png" />
                                                <asp:Panel ID="pnlPartesFactura" runat="server" Style="display: none">
                                                    <div id="div<%# Eval("DocEntry") %>" style="text-align: left; padding-top:30px;">
                                                        <asp:UpdatePanel ID="upaDetFactura" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvDetalleFactura" runat="server" AutoGenerateColumns="false" Width="50%"
                                                                    DataKeyNames="DocEntry" CssClass="table2 table-bordered table-striped table-hover">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Descripcion" HeaderText="Articulo" />
                                                                        <asp:BoundField DataField="Importe" DataFormatString="{0:C}" HeaderText="Importe" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField DataField="CentroCostosD" HeaderText="Estatus" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                                                        <asp:BoundField DataField="Id_Particion" HeaderText="IdDetalle" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                                                        <asp:BoundField DataField="ImporteSinIva" HeaderText="ImporteSinIva" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate> 
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </asp:Panel>
                                                </a>
                                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Factura" HeaderText="Factura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="NumAtCard" HeaderText="Orden de Trabajo" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DocDate" DataFormatString="{0:dd-M-yyyy}" HeaderText="Fecha" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />  
                                        <asp:BoundField DataField="Importe" DataFormatString="{0:C}" HeaderText="Importe" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Right" />  
                                        <asp:BoundField DataField="Moneda" HeaderText="Moneda" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />  
                                        <asp:BoundField DataField="Matricula" HeaderText="Centro de Costos" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />  
                                        <asp:BoundField DataField="TipoMtto" HeaderText="Mantenimiento" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />  
                                        <%--<asp:BoundField DataField="TipoMtto2" HeaderText="Tipo Mtto2" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />  --%>
                                        <%--<asp:BoundField DataField="ContDate" DataFormatString="{0:dd-M-yyyy}" HeaderText="Fecha Contable" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" />  --%>
                                        <asp:TemplateField HeaderText="Fecha Contable" ItemStyle-HorizontalAlign="Center" SortExpression="ContDate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFechaConta" type="date" runat="server" Text='<%# Eval("ContDate", "{0:dd/MM/yyyy}") %>' CssClass="form-control" Width="130px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" ItemStyle-Width="200" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Left" />  
                                        
                                        <asp:BoundField DataField="DocRate" HeaderText="Tipo Cambio" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="CardCode" HeaderText="Codigo Proveedor" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="DocCur" HeaderText="Moneda" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="DocEntry" HeaderText="DocEntry" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="DocNum" HeaderText="DocNum" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle CssClass="gvAlternate" />
                                    <RowStyle CssClass="gvItemsRows" />
                                    <FooterStyle CssClass="gvFooter" />
                                    <PagerStyle CssClass="gvFooter" />
                                    <EmptyDataTemplate>
                                        No hay registros.
                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </div>

                            <div class="col-md-12" align="center">
                                <asp:Button ID="btnAprobarFacturas" runat="server" Text="Contabilizar" CssClass="btn2" Visible="false" OnClick="btnAprobarFacturas_Click" />
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlErrores" runat="server" Visible="false">
                            <div style="overflow-y:auto; max-height:400px;">
                                <div style="height:30px;"></div>
                                <h5><asp:Label ID="lblTituloProceso" runat="server" Text="Existen errores que no permiten aprobar las facturas seleccionadas" ForeColor="Red"></asp:Label></h5>
                                <asp:GridView ID="gvResultado" AutoGenerateColumns="false" runat="server" Width="95%" 
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="Campo" HeaderText="CAMPO" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                        <asp:BoundField DataField="Valor" HeaderText="VALOR" ItemStyle-Width="150" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                        <asp:BoundField DataField="Status" HeaderText="STATUS" ItemStyle-Width="50" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                        <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" ItemStyle-Width="150" HeaderStyle-CssClass="gvCenter" />
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <AlternatingRowStyle CssClass="gvAlternate" />
                                    <RowStyle CssClass="gvItemsRows" />
                                    <FooterStyle CssClass="gvFooter" />
                                    <PagerStyle CssClass="gvFooter" />
                                    <EmptyDataTemplate>
                                        No hay registros.
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


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
