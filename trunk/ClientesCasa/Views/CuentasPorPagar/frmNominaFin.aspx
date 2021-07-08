<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmNominaFin.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ClientesCasa.Views.CuentasPorPagar.frmNominaFin" %>

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
    </script>

    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Procesamiento de Nómina</h4>
    </div>
    <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:Panel ID="pnlBusqueda" runat="server">
<%--        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de archivos procesados</h5>
            </div>
            <div class="widget-content nopadding">--%>
                
                <table style="width: 100%">
                    <tr>        
                        <td style="width: 5%"></td>
                        <td style="width: 90%">
                            <h3>
                                <asp:Label ID="lblTituloInfo" runat="server"></asp:Label>
                            </h3>
                        </td>
                        <td style="width: 5%"></td>
                    </tr>
                </table>

                <table style="width: 100%">
                    <tr>
                        <td style="width: 5%"></td>
                        <td style="width: 15%"></td>
                        <td style="width: 5%"></td>
                        <td style="width: 15%"></td>
                        <td style="width: 5%"></td>
                        <td style="width: 55%"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Fecha documento:</td>
                        <td></td>
                        <td>Referencia:</td>
                        <td></td>
                        <td>Comentarios:</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtFechaDoc" runat="server" type="date" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            <label for="txtFechaInicio" style="height: 24px; width: 24px" class="input-group-addon generic_btn"></label>
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtNumAtCard" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvFechaDoc" runat="server" ControlToValidate="txtFechaDoc" ValidationGroup="VGDoc" ForeColor="Red"
                                ErrorMessage="La fecha documento es requerida"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvNumAtCard" runat="server" ControlToValidate="txtNumAtCard" ValidationGroup="VGDoc" ForeColor="Red"
                                ErrorMessage="El campo referencia es requerido"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                        <td colspan="3">
                            <asp:RequiredFieldValidator ID="rfvComentarios" runat="server" ControlToValidate="txtComentarios" ValidationGroup="VGDoc" ForeColor="Red"
                                ErrorMessage="El campo comentarios es requerido"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>

                <asp:Panel ID="pnlEmpresas" runat="server" Width="100%">
                    <asp:UpdatePanel ID="upaEmpresas" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 90%">
                                        <div class="table-responsive" style="margin: 5px;">

                                            <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false" EnableViewState="true"
                                                CssClass="table table-bordered table-striped table-hover" OnRowDataBound="gvDetalle_RowDataBound"
                                                OnRowCommand="gvDetalle_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="CardCode" HeaderText="CardCode" />
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                                                    <asp:BoundField DataField="Importe" HeaderText="Importe" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                    <asp:BoundField DataField="IVA" HeaderText="IVA" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                    <asp:BoundField DataField="SubTotalFactura" HeaderText="Sub Total Factura" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                    <asp:BoundField DataField="Retencion" HeaderText="Retención" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                    <asp:BoundField DataField="Total" HeaderText="Total factura" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                    <asp:BoundField DataField="NumeroFactura" HeaderText="Numero Factura" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                                                    <%--<asp:BoundField DataField="DescEstatus" HeaderText="Estatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />--%>
                                                    <asp:TemplateField HeaderText="Estatus">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstatusFact" runat="server" Text='<%# Eval("DescEstatus") %>'></asp:Label>
                                                            <asp:LinkButton ID="lblErrorFact" runat="server" Text='<%# Eval("DescEstatus") %>' ForeColor="Red"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="upaDocs" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="imgRechazarFact" runat="server" ImageUrl="~/Images/icons/denied.png" Width="30px" Height="30px" OnClientClick="return confirm('¿Realmente deseas rechazar la factura?');"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="RechazarFactura" ToolTip="Rechazar factura" />
                                                                    <asp:ImageButton ID="btnCrearPoliza" runat="server" ImageUrl="~/Images/icons/document.png" Width="25px" Height="25px" OnClientClick="return confirm('¿Realmente deseas crear la poliza en SAP?');"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Poliza" ToolTip="Crear poliza en SAP" />
                                                                    <asp:ImageButton ID="imbXML" runat="server" ImageUrl="~/Images/icons/pdf.png" Width="35px" Height="35px"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="PDF" ToolTip="Clic aqui para descargar el archivo PDF" />
                                                                    <asp:ImageButton ID="imbPDF" runat="server" ImageUrl="~/Images/icons/xml.png" Width="28px" Height="28px"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="XML" ToolTip="Clic aqui para descargar el archivo XML" />
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="imbXML" />
                                                                    <asp:PostBackTrigger ControlID="imbPDF" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </td>
                                    <td style="width: 5%"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <div style="width: 100%; text-align: right">
                                            <%--<asp:Button ID="btnProcesar" runat="server" Text="Cargar a SAP" OnClick="btnProcesar_Click" CssClass="btn btn-info" />--%>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <br />
            <%--</div>
        </div>--%>
    </asp:Panel>
</asp:Content>
