<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmEnvioFacturas.aspx.cs" Inherits="ClientesCasa.Views.ASC.frmEnvioFacturas"
    UICulture="es" Culture="es-MX" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">

        $("[src*=read_moreR]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../Images/icons/not_moreR.png");
        });
        $("[src*=not_moreR]").live("click", function () {
            $(this).attr("src", "../../Images/icons/read_moreR.png");
            $(this).closest("tr").next().remove();
        });

        function SelectRowIndex(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            document.getElementById("<%=hdnIndexGridCC.ClientID %>").value = rowIndex - 1;
            //alert(document.getElementById("<%=hdnIndexGridCC.ClientID %>").value);
        }

        function setArticulo() {
            var list = document.getElementById("<%= ddlArticulo.ClientID %>");
            document.getElementById("<%=hdnArticuloTexto.ClientID %>").value = list.options[list.selectedIndex].text;
            document.getElementById("<%=hdnArticuloValor.ClientID %>").value = list.options[list.selectedIndex].value;
        }


        

    </script>
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

        function OcultarModal() {
            var txtParticion = '<%=txtNumParticiones.ClientID%>';
            txtParticion.value = "";

            var modalId = '<%=mpeParticionar.ClientID%>';
            var modalIdCC = '<%=mpeAddCC.ClientID%>';
            var modal = $find(modalId);
            var modalCC = $find(modalIdCC);
            modal.hide();
            modalCC.hide();
        }
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

        .itemAlignCenter {
            text-align: center;
        }

        .itemAlignLeft {
            text-align: left;
        }

        .itemAlignRight {
            text-align: right;
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

        .table3 th, .table3 td {
            padding: 8px;
            line-height: 20px;
            /*text-align: left;*/
            /*vertical-align: top;
            border-top: 1px solid #ddd;*/
        }
        
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=chkAll]').click(function () {
                $("[id*='chkFactura']").attr('checked', this.checked);
            });
        });
    </script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>

    <asp:UpdatePanel ID="upaPrincipal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <%--<div class="col-md-12">
                <asp:Label ID="lblTitulo" runat="server" Text="Envío de Facturas"></asp:Label>
            </div>--%>

            <div>
                <asp:Panel ID="pnlBusqueda" runat="server">
                    <fieldset>
                        <legend>
                            <div style="padding-left: 30px;">
                                <asp:Label ID="lblTitleBusqueda" runat="server" Text="Aprobación de Facturas"></asp:Label>
                            </div>
                        </legend>

                        <div class="col-md-12" align="center">
                            <table width="90%">
                                <tr>
                                    <td><asp:Label ID="lblTitleEstatus" runat="server" Text="Estatus:"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstatus" runat="server" OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="" Text=".:Seleccione:." Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Pendiente"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Aprobada"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Contabilizada"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Todos"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2"><asp:Label ID="lblTitlePeriodo" runat="server" Text="Seleccione un período:"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblTitleTipoMtto" runat="server" Text="Tipo de Mantenimiento:"></asp:Label></td>
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
                                <tr>
                                    <td><asp:Label ID="lblTitleCC" runat="server" Text="Matricula:"></asp:Label></td>
                                    <td><asp:TextBox ID="txtMatriculaCC" runat="server"></asp:TextBox></td>
                                    <td></td>
                                    <td>
                                        <div class="col-md-12" align="right">
                                            <asp:Button ID="btnGuardarFacturas" runat="server" Text="Enviar" CssClass="btn2" Visible="false" 
                                                OnClick="btnGuardarFacturas_Click" style="width:120px;" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div class="col-md-12" align="right">
                                            <asp:Button ID="btnGenerarReporte" runat="server" Text="Exportar a Excel" CssClass="btn2" OnClick="btnGenerarReporte_Click" 
                                                style="width:120px;"  />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </fieldset>
                </asp:Panel>
                
            </div>

            <div class="col-md-12" align="center" style="padding-bottom:50px;">
                <asp:UpdatePanel ID="upaGridFacturas" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlFacturasEncontradas" runat="server">
                            <%--<div class="col-md-12" align="right" style="padding-right: 4%;">
                                <asp:Button ID="btnGuardarFacturas" runat="server" Text="Enviar" CssClass="btn2" Visible="false" 
                                    OnClick="btnGuardarFacturas_Click" Width="100px" />
                            </div>--%>
                            <div style="overflow-y:auto; max-height:400px; padding-bottom: 50px; width: 98%; margin: 0 auto 0 auto;">
                                <%--<div class="col-md-12" align="right" style="padding-right: 4%;">
                                    <asp:Button ID="btnGenerarReporte" runat="server" Text="Exportar a Excel" CssClass="btn2" OnClick="btnGenerarReporte_Click" Width="120px" />
                                </div>--%>
                                <asp:GridView ID="gvFacturas" AutoGenerateColumns="false" runat="server" Width="95%" 
                                    CssClass="table2 table-bordered table-striped table-hover" OnRowDataBound="gvFacturas_RowDataBound"
                                    OnRowCommand="gvFacturas_RowCommand" OnPreRender="gvFacturas_PreRender" OnSorting="gvFacturas_Sorting" AllowSorting="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvCenter">
                                            <HeaderTemplate>
                                                <%--<asp:CheckBox ID="chkAll" runat="server" />--%>
                                                <label></label>
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
                                                                    OnRowCommand="gvDetalleFactura_RowCommand" OnRowDataBound="gvDetalleFactura_RowDataBound"
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
                                        <asp:BoundField DataField="Factura" HeaderText="Factura" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" SortExpression="Factura" />
                                        <asp:BoundField DataField="NumAtCard" HeaderText="Orden de Trabajo" ItemStyle-Width="" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" SortExpression="NumAtCard" />
                                        <asp:BoundField DataField="DocDate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha" ItemStyle-Width="" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Center" SortExpression="DocDate" />  
                                        <asp:BoundField DataField="Importe" DataFormatString="{0:C}" HeaderText="Importe" ItemStyle-Width="" HeaderStyle-CssClass="gvCenter" ItemStyle-HorizontalAlign="Right" SortExpression="Importe" />  
                                        <asp:BoundField DataField="DocCur" HeaderText="Moneda" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="itemAlignCenter" />  
                                        <asp:TemplateField HeaderText="Centro Costos" ItemStyle-HorizontalAlign="Center" SortExpression="Matricula">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCentroCostos" runat="server" Text='<%# Bind("Matricula") %>' CssClass="form-control"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mantenimiento" HeaderStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTipoMtto1" runat="server" Width="130px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Fecha Contable" ItemStyle-HorizontalAlign="Center" SortExpression="ContDate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFechaConta" type="date" runat="server" Text='<%# Eval("ContDate", "{0:dd/MM/yyyy}") %>' CssClass="form-control" Width="130px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Height="40px" Columns="3" Text='<%# Bind("Observaciones") %>' Font-Size="9pt"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Estatus" HeaderText="Estatus" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="TipoMtto" HeaderText="TipoMtto" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="TipoMtto2" HeaderText="TipoMtto2" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="DocNum" HeaderText="DocNum" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                        <asp:BoundField DataField="TaxCode" HeaderText="TaxCode" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />

                                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:ImageButton ID="btnParticionar" runat="server" OnClick="btnParticionar_Click" ImageUrl="~/Images/icons/split.png" 
                                                    Width="24" Height="24" ToolTip="Partir" CommandName="Particionar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                                <asp:UpdatePanel ID="upaLinkRef" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgBtnAddCentroCostos" runat="server" OnClick="imgBtnAddCentroCostos_Click" ImageUrl="~/Images/icons/addMat2.png" 
                                                            style="width:20px; height:20px;" ToolTip="Agregar Centro de Costos" CommandName="AgregarCC" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:ImageButton ID="imbReferenciaDoc" runat="server" style="width:20px; height:20px;" ImageUrl="~/Images/icons/view.png" 
                                                            CommandName="ViewReference" ToolTip="De clic para visualizar el documento." CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:ImageButton>
                                                    </ContentTemplate>
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


    <%-- Modal de Particionar Factura --%>
    <asp:HiddenField ID="hdTargetParticionar" runat="server" />
    <cc1:ModalPopupExtender ID="mpeParticionar" runat="server" TargetControlID="hdTargetParticionar"
        PopupControlID="pnlParticionar" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlParticionar" runat="server" BorderColor="" BackColor="White" Height="150px"
        Width="280px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="Label3" runat="server" Text="Particionar Factura"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="Label4" runat="server" Text="Particionar en:" CssClass="lblInput"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:TextBox ID="txtNumParticiones" type="text" runat="server" CssClass="form-control" MaxLength="5" TextMode="Number" width="70px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" ControlToValidate="txtPeriodo" Display="Dynamic"
                                ErrorMessage="El campo es requerido" ValidationGroup="VPeriodo" CssClass="validar"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarParticion" runat="server" Text="Aceptar" OnClientClick="OcultarModal();" 
                                    OnClick="btnAceptarParticion_Click" CssClass="btn2 btn-primary" Width="100px" />
                                <asp:HiddenField ID="hdnNumDoc" runat="server" Value="" />
                                <asp:HiddenField ID="hdnIndexGrid" runat="server" Value="" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarParticion" runat="server" Text="Cancelar" OnClientClick="OcultarModal();" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- Modal de Agregar Centro de Costos --%>
    <asp:HiddenField ID="hdTargetCC" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAddCC" runat="server" TargetControlID="hdTargetCC"
        PopupControlID="pnlCC" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlCC" runat="server" BorderColor="" BackColor="White" Height=""
        Width="250px" HorizontalAlign="Center" Style="display: none; left:25%; right:25%;" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="width: 100%" class="table3">
                    <tr>
                        <td colspan="2" align="center">
                            <h4><asp:Label ID="Label7" runat="server" Text="Agregar Centro de Costos"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label5" runat="server" Text="Seleccionar Flota:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rdnLstFlota" runat="server" RepeatDirection="Horizontal" 
                                OnSelectedIndexChanged="rdnLstFlota_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:left;">
                            <div style="padding-top:10px;">
                                <asp:Label ID="Label6" runat="server" Text="Selecciona Centro Costos:"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        
                        <td colspan="2">
                            <%--<asp:DropDownList ID="ddlCentroCostos" runat="server" ></asp:DropDownList>--%>
                            <%--<asp:ListBox runat="server" ID="lstCCMultiple" SelectionMode="Multiple" Height="50px">
                            </asp:ListBox>--%>

                            <div style="text-align: left; padding-top:10px; overflow-y:scroll; max-height:150px;">
                                <asp:UpdatePanel ID="upaMatriculas" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvMatriculas" runat="server" AutoGenerateColumns="false" Width="40%"
                                            OnRowDataBound="gvMatriculas_RowDataBound"
                                            CssClass="table2 table-bordered table-striped table-hover">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvCenter" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMatricula" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Matricula" HeaderText="Matricula" HeaderStyle-Width="90%" />
                                                <asp:BoundField DataField="DesFlota" HeaderStyle-CssClass="hideColumn" ItemStyle-CssClass="hideColumn" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate> 
                                </asp:UpdatePanel>
                            </div>

                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="Label11" runat="server" Text="Nota: Seleccione varias matrículas con la tecla 'Ctrl + Click'" Font-Size="9pt" ForeColor="#0033cc"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarCC" runat="server" Text="Aceptar" OnClientClick="OcultarModal();" 
                                    CssClass="btn2 btn-primary" Width="100px" OnClick="btnAceptarCC_Click" />
                                <asp:HiddenField ID="hdnOpcion" runat="server" Value="" />
                                <asp:HiddenField ID="hdnNumDocCC" runat="server" Value="" />
                                <asp:HiddenField ID="hdnNumDocCCD" runat="server" Value="" />
                                <asp:HiddenField ID="hdnIndexGridCC" runat="server" Value="" />
                                <asp:HiddenField ID="hdnIndexGridCCD" runat="server" Value="" />
                                <asp:HiddenField ID="hdnMatriculaDefault" runat="server" Value="" />
                                
                                <asp:HiddenField ID="hdnIdDetalleCC" runat="server" Value="" />
                                <asp:HiddenField ID="hdnDocEntryCC" runat="server" Value="" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarCC" runat="server" Text="Cancelar" OnClientClick="OcultarModal();" 
                                    CssClass="btn btn-default" OnClick="btnCancelarCC_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- Modal de Agregar Articulo a partir del articulo seleccionado --%>
    <asp:HiddenField ID="hdTargetAgregarArticulo" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAgregarArticulo" runat="server" TargetControlID="hdTargetAgregarArticulo"
        PopupControlID="pnlAgregarArticulo" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlAgregarArticulo" runat="server" BorderColor="" BackColor="White" Height="150px"
        Width="280px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="Label8" runat="server" Text="Agregar Articulo"></asp:Label>
                            </h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="Label9" runat="server" Text="Articulo:" CssClass="lblInput"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:DropDownList ID="ddlArticulo" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlArticulo_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--<asp:RequiredFieldValidator ID="rqArticulo" runat="server" ErrorMessage="*Seleccione un articulo" InitialValue="0"
                                ControlToValidate="ddlArticulo" Display="Dynamic" ForeColor="Red" ValidationGroup="groupArticulo">
                            </asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="Label10" runat="server" Text="Importe:" CssClass="lblInput"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:TextBox ID="txtImporteArticulo" type="text" runat="server" CssClass="form-control" width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rqImporteArticulo" runat="server" ErrorMessage="*Se requiere el importe"
                                ControlToValidate="txtImporteArticulo" Display="Dynamic" ForeColor="Red" ValidationGroup="groupArticulo">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationExpression="\d*\.?\d*" ControlToValidate="txtImporteArticulo" 
                                ErrorMessage="*No es cantidad numerica" runat="server" Display="Dynamic" ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnGuardarArticulo" runat="server" Text="Guardar" OnClientClick="OcultarModal(); setArticulo();" 
                                    OnClick="btnGuardarArticulo_Click" CssClass="btn2 btn-primary" Width="100px" ValidationGroup="groupArticulo"
                                    CausesValidation="true" />
                                <asp:HiddenField ID="hdnIdDetalle" runat="server" Value="" />
                                <asp:HiddenField ID="hdnDocEntry" runat="server" Value="" />
                                <asp:HiddenField ID="hdnIndexGridArt" runat="server" Value="" />
                                <asp:HiddenField ID="hdnArticulo" runat="server" />
                                <asp:HiddenField ID="hdnImporteArticulo" runat="server" />
                                <asp:HiddenField ID="hdnArticuloTexto" runat="server" />
                                <asp:HiddenField ID="hdnArticuloValor" runat="server" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarArticulo" runat="server" Text="Cancelar" OnClientClick="OcultarModal();" 
                                    OnClick="btnCancelarArticulo_Click" CssClass="btn btn-default" CausesValidation="false" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


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
