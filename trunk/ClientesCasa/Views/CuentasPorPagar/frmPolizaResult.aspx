<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmPolizaResult.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ClientesCasa.Views.CuentasPorPagar.frmPolizaResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    <style type="text/css">
        .hideColumn {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function chooseFile() {
            document.getElementById("FileUpload1").click();
        }

        function OcultarModalArchicos() {
            var modalId = '<%= mpeXML.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function OcultarModalArchivosPDF() {
            var modalId = '<%= mpePDF.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function checkFileExtension(elem) {
            var filePath = elem.value;

            if (filePath.indexOf('.') == -1)
                return false;

            var validExtensions = new Array();
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

            validExtensions[0] = 'xml';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i])
                    return true;
            }

            alert('Solo se permiten archivos con extensión XML.');
            return false;
        }

        function checkFileExtensionPDF(elem) {
            var filePath = elem.value;

            if (filePath.indexOf('.') == -1)
                return false;

            var validExtensions = new Array();
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

            validExtensions[0] = 'pdf';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i])
                    return true;
            }

            alert('Solo se permiten archivos con extensión PDF.');
            return false;
        }
        
    </script>
    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Procesamiento de Nómina</h4>
    </div>
    <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:FileUpload ID="FileUpload1" runat="Server" Style="display: none;" />
    <asp:Panel ID="pnlBusqueda" runat="server">

        <%--<div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de archivos procesados</h5>
            </div>
            <div class="widget-content nopadding">--%>
                <asp:UpdatePanel ID="upaArchivos" runat="server">
                    <ContentTemplate>
                        
                        <asp:Panel ID="pnlEmpresas" runat="server" Width="100%">
                            <table style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td>
                                        <h3>
                                            <asp:Label ID="lblTituloInfo" runat="server"></asp:Label>
                                        </h3>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table style="width:100%">
                                            <tr>
                                                <td style="width:20%;"><strong>Fecha de carga</strong></td>
                                                <td style="width:20%"><strong>Usuario</strong></td>
                                                <td style="width:20%"><strong>Estatus General</strong></td>
                                                <td style="width:20%"></td>
                                                <td style="width:20%"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFechaCarga" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUsuarioCarga" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEstatusCarga" runat="server"></asp:Label>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="3"><br /></td>
                                </tr>
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 90%">
                                        <%--<div class="table-responsive" style="margin: 5px;">--%>


                                            <%--<asp:GridView ID="gvArchivos" runat="server" AutoGenerateColumns="False" EnableViewState="true" DataKeyNames="IdFolio"
                                                AllowPaging="True" CssClass="table table-bordered table-striped table-hover" Width="100%"
                                                OnRowDataBound="gvArchivos_RowDataBound">
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="Label50" runat="server" Text="No existen Registros."></asp:Label>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Image ID="imEmpresas" runat="server" ToolTip="Ver detalle de la nómina" Style="cursor: pointer" ImageUrl="../../Images/icons/down.png" Width="24" Height="24" />
                                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 5%"></td>
                                                                        <td style="width: 90%">
                                                                            <div id="div<%# Eval("IdFolio") %>">--%>
                                                                                <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false" EnableViewState="true"
                                                                                    CssClass="table table-bordered table-striped table-hover" OnRowDataBound="gvDetalle_RowDataBound"
                                                                                    OnRowCommand="gvDetalle_RowCommand">
                                                                                    <Columns>
                                                                                        <%--<asp:BoundField DataField="CardCode" HeaderText="CardCode" />--%>
                                                                                        <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                                                                                        <asp:BoundField DataField="NoEmpleados" HeaderText="No. empleados" />
                                                                                        <asp:BoundField DataField="Markup" HeaderText="Markup" Visible="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                                                        <asp:BoundField DataField="CostoEmpleado" HeaderText="Costo empleados" Visible="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />

                                                                                        <asp:BoundField DataField="Importe" HeaderText="Importe" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                                                        <asp:BoundField DataField="IVA" HeaderText="IVA" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                                                        <asp:BoundField DataField="SubTotalFactura" HeaderText="Sub Total Factura" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                                                        <asp:BoundField DataField="Retencion" HeaderText="Retención" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />

                                                                                        <asp:BoundField DataField="Total" HeaderText="Total factura" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />
                                                                                        <asp:BoundField DataField="NumeroFactura" HeaderText="Numero Factura" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="Aprobada" HeaderText="Sts" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                                                                        <asp:BoundField DataField="EstatusFactura" HeaderText="Estatus Factura" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100" />
                                                                                        
                                                                                        <asp:TemplateField HeaderText="Acciones" HeaderStyle-Width="130">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imbVerDetalle" runat="server" ImageUrl="../../Images/icons/review.png" Width="30px" Height="30px" ToolTip="Ver detalle"
                                                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Detalle"></asp:ImageButton>
                                                                                                <asp:ImageButton ID="imbAprobar" runat="server" ImageUrl="../../Images/icons/aproved.png" Width="24px" Height="24px" ToolTip="Aprobar factura"
                                                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Aprobar"></asp:ImageButton>
                                                                                                <asp:ImageButton ID="imbModal" runat="server" ImageUrl="../../Images/icons/xml.png" Width="27px" Height="27px" ToolTip="Subir XML"
                                                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="UploadXML"></asp:ImageButton>
                                                                                                <asp:ImageButton ID="imbPDF" runat="server" ImageUrl="../../Images/icons/pdf.png" Width="32px" Height="32px" ToolTip="Subir PDF"
                                                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="UploadPDF"></asp:ImageButton>

                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            <%--</d>
                                                                        </td>
                                                                        <td style="width: 5%"></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="FechaCreacion" HeaderText=" Fecha creación " />
                                                    <asp:BoundField DataField="UsuarioCreacion" HeaderText=" Usuario creación " />
                                                    <asp:BoundField DataField="Procesado" HeaderText=" Estatus " ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron registros para mostrar
                                                </EmptyDataTemplate>
                                            </asp:GridView>--%>


                                        <%--</div>--%>
                                    </td>
                                    <td style="width: 5%"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <div style="width: 100%; text-align: right">
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <asp:UpdatePanel ID="upaPrincipal" runat="server">
                    <ContentTemplate>

                        <asp:Panel ID="pnlDetEmpresas" runat="server" Width="100%" Visible="false">
                            <br />
                            <br />
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="font-style: inherit">
                                        <h4><strong>CardCode:</strong></h4>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRespCardCode" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 5%">
                                        <h4><strong>No Factura:  </strong></h4>
                                    </td>
                                    <td Style="text-align:left; width: 20%">
                                        <asp:Label ID="lblRespNoFactura" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="font-size: medium">
                                        <strong>Empresa:</strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRespNombreEmpresa" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRegresar1" runat="server" Text="Regresar" OnClick="btnRegresar1_Click" CssClass="btn btn-success" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="4">
                                        <asp:GridView ID="gvEmpleadosEmpresa" runat="server" Width="100%" AutoGenerateColumns="false"
                                            OnRowCommand="gvEmpleadosEmpresa_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="EmpresaFacturante" HeaderText="Empresa" />
                                                <asp:BoundField DataField="RFCEmpresaFacturante" HeaderText="RFC Empresa" />
                                                <asp:BoundField DataField="NombreEmpleado" HeaderText="Empleado" />
                                                <asp:BoundField DataField="RFCEmpleado" HeaderText="RFC Empleado" />
                                                <asp:BoundField DataField="CostoEmpleado" HeaderText="Costo Empleado" DataFormatString="{0:C}" />
                                                <asp:BoundField DataField="MarkUpMV" HeaderText="MarkUpMV" DataFormatString="{0:C}" />
                                                <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGeneralExcel" runat="server" Text="Excel" CommandName="Excel" CssClass="btn btn-primary"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <%--<asp:Button ID="btnCrearEnSAP" runat="server" Text="SAP" CommandName="SAP"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <%--<td style="width: 10%"></td>--%>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnRegresar2" runat="server" Text="Regresar" OnClick="btnRegresar1_Click" CssClass="btn btn-success" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <br />
            <%--</div>
        </div>--%>


    </asp:Panel>

    <%-- Modal XML --%>
    <asp:HiddenField ID="hdTargetXML" runat="server" />
    <cc1:ModalPopupExtender ID="mpeXML" runat="server" TargetControlID="hdTargetXML"
        PopupControlID="pnlXML" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlXML" runat="server" BorderColor="" BackColor="White" Height="230px"
        Width="400px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaXML" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="lblTituloMatricula" runat="server" Text="Seleccione su XML de factura"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 10%">
                            <asp:Label ID="lblXml" runat="server" Text="XML:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 80%"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left">
                            <asp:FileUpload ID="fuXML" runat="server" accept=".xml"/>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarXML" runat="server" Text="Aceptar" OnClick="btnAceptarXML_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarXML" runat="server" Text="Cancelar" OnClientClick="OcultarModalArchicos();" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAceptarXML" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- Modal PDF --%>
    <asp:HiddenField ID="hdTargetPDF" runat="server" />
    <cc1:ModalPopupExtender ID="mpePDF" runat="server" TargetControlID="hdTargetPDF"
        PopupControlID="pnlPDF" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPDF" runat="server" BorderColor="" BackColor="White" Height="230px"
        Width="400px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaPDF" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="lblTitlePDF" runat="server" Text="Selecciones el pdf de su factura"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 10%">
                            <asp:Label ID="lblPDF" runat="server" Text="PDF:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 80%"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left">
                            <asp:FileUpload ID="fuPDF" runat="server" accept=".pdf"/>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarPDF" runat="server" Text="Aceptar" OnClick="btnAceptarPDF_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarPDF" runat="server" Text="Cancelar" OnClientClick="OcultarModalArchivosPDF();" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAceptarPDF" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>
