<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmMantenimiento.aspx.cs" EnableEventValidation="false" UICulture="es" Culture="es-MX" Inherits="ClientesCasa.Views.Gastos.frmMantenimiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=read_more]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../Images/icons/not_more.png");
        });
        $("[src*=not_more]").live("click", function () {
            $(this).attr("src", "../../Images/icons/read_more.png");
            $(this).closest("tr").next().remove();
        });

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

        function UserDeleteConfirmation() {
            if (confirm("¿Realmente esta seguro de eliminar el gasto?"))
                return true;
            else
                return false;
        }

        function OcultarModal() {
            var txtTasa = '<%=txtPeriodo.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%=mpePeriodo.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function EnterEvent(e) {

            if (e.keyCode == 13) {
                var obj = document.getElementById('<%=btnBuscarCliente.ClientID%>');

                if (obj) {
                    obj.click();
                }
            }
        }

        function visible(element) {
            var elemento = document.getElementById(element);
            elemento.style.display = 'block';
        }

        function invisible(element) {
            var elemento = document.getElementById(element);
            elemento.style.display = 'none';
        }

        function Selrdbtn(id) {
            var rdBtn = document.getElementById(id);
            var List = document.getElementsByTagName("input");
            for (i = 0; i < List.length; i++) {
                if (List[i].type == "radio" && List[i].id != rdBtn.id) {
                    List[i].checked = false;
                }
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function OcultarModalPiernasMXN() {
            var txtTasa = '<%=txtTripPiernas.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%= mpePierna.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function OcultarModalPiernasUSD() {
            var txtTasa = '<%=txtTrioUSA.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%=mpePiernasUSA.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function OcultarModalEstimados() {
            var modalId = '<%=mpeGastosEstimados.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
    </script>

    <style type="text/css">
        .AlineadoDerecha {
            text-align: right;
        }

        .ColumnaOculta {
            display: none;
        }
    </style>


    <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upaPrincipal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <div style="text-align: left">
                    <h4>&nbsp;&nbsp;Mantenimiento de datos</h4>
                </div>
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-align-justify"></i></span>
                        <h5>Búsqueda de Clientes</h5>
                    </div>
                    <div class="widget-content nopadding">

                        <div class="control-group">
                            <div class="controls">
                                <br />
                                <div class="table-responsive" style="margin: 5px;">
                                    <table style="width: 100%" class="table table-striped">
                                        <tr>
                                            <td style="text-align: center">
                                                <div class="section group" style="margin-left: -30px;">
                                                    <div class="col span_1_of_4">
                                                        &nbsp; Búsqueda:
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:TextBox ID="txtBusqueda" runat="server" MaxLength="98" Width="90%"
                                                            ClientIDMode="Static" onkeypress="return EnterEvent(event);"></asp:TextBox>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:DropDownList ID="ddlOpcBus" runat="server" placeholder="Seleccione">
                                                            <asp:ListItem Text="Clave Cliente" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Clave Contrato" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Matrícula" Value="4" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Solo Activos" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Inactivos" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                        <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" OnClick="btnBuscarCliente_Click" ClientIDMode="Static" CssClass="btn btn-success" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <div class="table-responsive" style="margin: 5px;">
                                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" DataKeyNames="IdContrato"
                                            AllowPaging="True" Width="100%" CssClass="table table-bordered table-striped table-hover"
                                            PageSize="2" OnRowDataBound="gvClientes_RowDataBound" OnPageIndexChanging="gvClientes_PageIndexChanging" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                                            <EmptyDataTemplate>
                                                No existen Registros para mostrar.
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ClaveCliente" HeaderText="Clave del Cliente" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre del cliente" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="ClaveContrato" HeaderText="Clave del contrato" />
                                                <asp:BoundField DataField="Matricula" HeaderText="Matrícula Aeronave" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <br />
                        <br />
                    </div>
                </div>
            </asp:Panel>

            <br />
            <asp:Panel ID="pnlActualizar" runat="server" Visible="false">
                <table style="width: 100% !important;">
                    <tr>
                        <td style="text-align: center !important;">
                            <asp:Button ID="btnActualizarComprobantes" runat="server" Text="Actualizar lista comprobantes" OnClick="btnActualizarComprobantes_Click" CssClass="btn btn-success" Style="min-width: 215px !important;" />
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar lista de gastos" OnClick="btnActualizar_Click" CssClass="btn btn-success" Style="width: 190px !important;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center !important;">
                            <br />
                            <strong>
                                <asp:Label ID="lblMex" runat="server" Text="Mes:" Font-Bold="true"></asp:Label></strong>
                            <asp:Label ID="lblReqMes" runat="server" Text="" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <strong>
                                <asp:Label ID="lblAnio" runat="server" Text="Año:" Font-Bold="true"></asp:Label></strong>
                            <asp:Label ID="lblReqAnio" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Timer ID="timer1" runat="server" OnTick="timer1_Tick" Interval="500" Enabled="false"></asp:Timer>
            <asp:Panel ID="pnlRubros" runat="server" Visible="false">
                <asp:UpdatePanel ID="upaGastosPesos" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="widget-box">
                            <div class="widget-title">
                                <span class="icon"><i class="icon-align-justify"></i></span>
                                <h5>Gastos en Pesos (Mexicanos)</h5>
                            </div>
                            <div class="widget-content nopadding">
                                <div class="control-group">
                                    <div class="controls">
                                        <div style="text-align: right; margin-right: 15px;">
                                            <asp:Button ID="btnAgregarEstimado" runat="server" Text="Agregar Gasto Estimado"
                                                OnClick="btnAgregarEstimado_Click" CssClass="btn btn-success" Style="margin-right: 20px !important; width: 180px !important;" />
                                        </div>
                                        <div style="text-align: center;">
                                            <strong>
                                                <asp:Label ID="lblClaveCliente" runat="server"></asp:Label></strong><br />
                                            <strong>
                                                <asp:Label ID="lblNombreCliente" runat="server"></asp:Label></strong><br />
                                            <strong>
                                                <asp:Label ID="lblMatriculaMEX" runat="server"></asp:Label></strong><br />
                                        </div>
                                        <div style="text-align: right; margin-right: 15px;">
                                            <asp:LinkButton ID="lbkExportaMXN" runat="server" Text="Exportar a Excel" OnClick="lbkExportaMXN_Click" CssClass="btn btn-success"
                                                Font-Underline="false" Style="margin-right: 20px; height: 10px;"></asp:LinkButton>
                                        </div>
                                        <br />
                                        <div>
                                            <div class="table-responsive" style="width: 100%; border: 1px solid #dddddd;">
                                                <table class="table">
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="upaGridGastosMXN" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlRubrosMat" runat="server" ScrollBars="Auto" Height="400">
                                                                        <asp:GridView ID="gvMantenimiento" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                                                            CssClass="table table-bordered table-striped table-hover" OnRowDataBound="gvMantenimiento_RowDataBound"
                                                                            DataKeyNames="IdGasto" OnRowCommand="gvMantenimiento_RowCommand" OnRowCreated="gvMantenimiento_RowCreated"
                                                                            OnPreRender="gvMantenimiento_PreRender">
                                                                            <EmptyDataTemplate>
                                                                                No existen Registros para mostrar.
                                                                            </EmptyDataTemplate>
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                                                                                    <ItemTemplate>
                                                                                        <img alt="" style="cursor: pointer; margin-left: 5%;" width="24" height="24" src="../../Images/icons/read_more.png" />
                                                                                        <asp:Panel ID="pnlGastosMXN" runat="server" Style="display: none">
                                                                                            <asp:UpdatePanel ID="upaDetGastosMXN" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <div id="div<%# Eval("IdGasto") %>" style="text-align: left">
                                                                                                        <asp:GridView ID="gvDetalleGastoMXN" runat="server" AutoGenerateColumns="false" Width="80%" Style="margin: 0 auto; border: 2px solid #547ba6;">
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="LegId" HeaderText="No pierna" />
                                                                                                                <asp:BoundField DataField="Ruta" HeaderText="Ruta" />
                                                                                                                <asp:BoundField DataField="FechaVuelo" HeaderText="Fecha vuelo" />
                                                                                                                <asp:BoundField DataField="TiempoCalzo" HeaderText="Tiempo calzo" />
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </div>

                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </asp:Panel>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Estimado">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnEliminarMEX" runat="server" ToolTip="Elimina un gasto estimado" OnClientClick="return UserDeleteConfirmation();"
                                                                                            CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/Images/icons/delete.png" Width="23" Height="23" Style="margin-left: 25%;" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Buscar Pierna">
                                                                                    <ItemTemplate>
                                                                                        <asp:UpdatePanel ID="upaBusquedaPiernaMXN" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:ImageButton ID="btnBuscarPierna" runat="server" OnClick="btnBuscarPierna_Click" ImageUrl="~/Images/icons/searchdate.png" ToolTip="De clic para buscar una pierna." Width="18" Height="18" Style="margin: 0 auto; margin-left: 25%;" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Fecha Vuelo">
                                                                                    <ItemTemplate>
                                                                                        <asp:UpdatePanel ID="upaFechaMXN" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblFechaMXN" runat="server" Text='<%# Bind("FechaVuelo") %>'></asp:Label>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="No.Referencia" SortExpression="Referencia" ControlStyle-Width="150">
                                                                                    <ItemTemplate>
                                                                                        <asp:UpdatePanel ID="upaLinkRefPesos" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblReferenciaPesos" runat="server" Text='<%# Bind("Referencia") %>' Font-Size="X-Small"></asp:Label>
                                                                                                <asp:ImageButton ID="imbReferenciaPesos" runat="server" Width="16" Height="16" ImageUrl="~/Images/icons/searchdate.png" CommandName="ViewReference" Style="margin: 0 auto; margin-left: 25%;"
                                                                                                    CommandArgument='<%# Bind("Referencia") %>' ToolTip="De clic para visualizar el documento." Visible="false"></asp:ImageButton>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Importe">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtImporte" runat="server" Style="width: 70px !important; text-align: right;"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Importe Original" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblImporteOriginal" Text='<%# Bind("Importe", "{0:c}") %>' runat="server" Style="display: block; text-align: right;"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Fijo / Variable">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlFijoVar" runat="server" Width="90px">
                                                                                            <asp:ListItem Text="FIJO" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="VARIABLE" Value="2"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Rubro">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlRubro" runat="server" Width="100px"></asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Tipo Gasto">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlTipoGasto" runat="server" Width="100px" EnableViewState="true">
                                                                                        </asp:DropDownList>
                                                                                        <%--<cc1:CascadingDropDown ID="cdlTipoGasto" TargetControlID="ddlTipoGasto" PromptText="Seleccione" LoadingText="Cargando..."
                                                                                            PromptValue="Descripcion" ServicePath="ServiceCS.asmx" ServiceMethod="GetTipoGasto" runat="server"
                                                                                            Category="TipoGasto" EnableViewState="true" />
                                                                                        <asp:HiddenField ID="hfTipoGasto" runat="server" />--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Comentarios">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtComentarios" runat="server" Width="100px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Proveedor" HeaderStyle-Width="120px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblProv" runat="server" Text='<%# Bind("Proveedor") %>' Visible="false"></asp:Label>
                                                                                        <asp:DropDownList ID="ddlProvG" runat="server" Width="100px" >
                                                                                        </asp:DropDownList>
                                                                                        
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" FooterStyle-CssClass="ColumnaOculta">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNoPierna" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="text-align: right; margin-right: 15px;">
                                                <br />
                                                <asp:Button ID="btnModificar" runat="server" Text="Guardar" OnClientClick="visible('divMessage');" OnClick="btnModificar_Click" CssClass="btn btn-success" Style="margin-right: 20px;"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lbkExportaMXN" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>

            <asp:Panel ID="pnlRubrosUSA" runat="server" Visible="false">
                <asp:UpdatePanel ID="upaDolares" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="widget-box">
                            <div class="widget-title">
                                <span class="icon"><i class="icon-align-justify"></i></span>
                                <h5>Gastos en Dolares</h5>
                            </div>
                            <div class="widget-content nopadding">
                                <div class="control-group">
                                    <div class="controls">
                                        <div style="text-align: right; margin-right: 15px;">
                                            <asp:Button ID="btnAgregarEstimadoUSA" runat="server" Text="Agregar Gasto Estimado" CssClass="btn btn-success" OnClick="btnAgregarEstimadoUSA_Click" Style="margin-right: 20px !important; width: 180px !important;" />
                                        </div>
                                        <div style="text-align: center;">
                                            <strong>
                                                <asp:Label ID="lblClaveClienteUSD" runat="server"></asp:Label></strong><br />
                                            <strong>
                                                <asp:Label ID="lblNombreClienteUSD" runat="server"></asp:Label></strong><br />
                                            <strong>
                                                <asp:Label ID="lblMatriculaUSD" runat="server"></asp:Label></strong><br />
                                        </div>
                                        <br />
                                        <div style="text-align: right; margin-right: 15px;">
                                            <asp:LinkButton ID="lkbExportaUSD" runat="server" Text="Exportar a Excel" OnClick="lkbExportaUSD_Click" CssClass="btn btn-success"
                                                Font-Underline="false" Style="margin-right: 20px; height: 20px;"></asp:LinkButton>
                                        </div>
                                        <br />
                                        <div class="table-responsive" style="width: 100%; border: 1px solid #dddddd;">
                                            <table class="table">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="upaGastosDolares" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnlGastosUSA" runat="server" ScrollBars="Auto" Height="400">
                                                                    <asp:GridView ID="gvMantenimientoUSA" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                                                        CssClass="table table-bordered table-striped table-hover" DataKeyNames="IdGasto"
                                                                        OnRowDataBound="gvMantenimientoUSA_RowDataBound" OnRowCommand="gvMantenimientoUSA_RowCommand"
                                                                        OnRowCreated="gvMantenimientoUSA_RowCreated" OnPreRender="gvMantenimientoUSA_PreRender">
                                                                        <EmptyDataTemplate>
                                                                            No existen Registros.
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
                                                                                <ItemTemplate>
                                                                                    <img alt="" style="cursor: pointer; margin-left: 5%;" width="24" height="24" src="../../Images/icons/read_more.png" />
                                                                                    <asp:Panel ID="pnlGastosUSD" runat="server" Style="display: none">
                                                                                        <div id="divUSA<%# Eval("IdGasto") %>">
                                                                                            <asp:UpdatePanel ID="upaDetGastosUSD" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:GridView ID="gvDetalleGastoUSD" runat="server" AutoGenerateColumns="false" Width="80%" Style="margin: 0 auto; border: 2px solid #547ba6;">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="LegId" HeaderText="No pierna" />
                                                                                                            <asp:BoundField DataField="Ruta" HeaderText="Ruta" />
                                                                                                            <asp:BoundField DataField="FechaVuelo" HeaderText="Fecha vuelo" />
                                                                                                            <asp:BoundField DataField="TiempoCalzo" HeaderText="Tiempo calzo" />
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Estimado">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminarUSA" runat="server" ToolTip="Elimina un gasto estimado" OnClientClick="return UserDeleteConfirmation();"
                                                                                        CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/Images/icons/delete.png" Width="23" Height="23" Style="margin-left: 25%;" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Buscar Pierna">
                                                                                <ItemTemplate>
                                                                                    <asp:UpdatePanel ID="upaBusquedaPiernaUSD" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>
                                                                                            <asp:ImageButton ID="btnBuscarPiernaUSA" runat="server" OnClick="btnBuscarPierna_Click1" ImageUrl="~/Images/icons/searchdate.png" Width="18" Height="18" ImageAlign="Middle" Style="margin: 0 auto; margin-left: 25%;" />
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Fecha Vuelo">
                                                                                <ItemTemplate>
                                                                                    <asp:UpdatePanel ID="upaFechaUSD" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>
                                                                                            <asp:Label ID="lblFechaUSD" runat="server" Text='<%# Bind("FechaVuelo") %>'></asp:Label>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="No.Referencia" SortExpression="Referencia">
                                                                                <ItemTemplate>
                                                                                    <asp:UpdatePanel ID="upaLinkRefDlls" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>
                                                                                            <asp:Label ID="lblReferenciaDlls" runat="server" Text='<%# Bind("Referencia") %>' Font-Size="X-Small"></asp:Label>
                                                                                            <asp:ImageButton ID="imbReferenciaDlls" runat="server" Width="16" Height="16" ImageUrl="~/Images/icons/searchdate.png" CommandName="ViewReference" Style="margin: 0 auto; margin-left: 25%;"
                                                                                                CommandArgument='<%# Bind("Referencia") %>' ToolTip="De clic para visualizar el documento." Visible="false"></asp:ImageButton>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Importe">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtImporte" runat="server" Style="width: 70px !important; text-align: right;"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Importe Original" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblImporteOriginalUSD" Text='<%# Bind("Importe", "{0:c}") %>' runat="server" Style="display: block; text-align: right;"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Fijo / Variable">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlFijoVar" runat="server" Width="90px">
                                                                                        <asp:ListItem Text="FIJO" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="VARIABLE" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Rubro">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlRubro" runat="server" Width="100px"></asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Tipo Gasto">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlTipoGasto" runat="server" Width="100px" EnableViewState="true">
                                                                                    </asp:DropDownList>
                                                                                    <%--<cc1:CascadingDropDown ID="cdlTipoGasto" TargetControlID="ddlTipoGasto" PromptText="Seleccione" LoadingText="Cargando..."
                                                                                        PromptValue="Descripcion" ServicePath="ServiceCS.asmx" ServiceMethod="GetTipoGasto" runat="server"
                                                                                        Category="TipoGasto" EnableViewState="true" />--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Comentarios">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtComentarios" runat="server" Width="100px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Proveedor" HeaderStyle-Width="100px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblProvUS" runat="server" Text='<%# Bind("Proveedor") %>' Visible="false"></asp:Label>
                                                                                        <asp:DropDownList ID="ddlProvGUS" runat="server" Width="100px" >
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            <asp:TemplateField ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" FooterStyle-CssClass="ColumnaOculta">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoPierna" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                        <div style="text-align: right; margin-right: 15px;">
                                            <asp:Button ID="btnModificar1" runat="server" Text="Guardar" OnClick="btnModificar1_Click" CssClass="btn btn-success" Style="margin-right: 20px;"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lkbExportaUSD" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>


        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Modal de Periodo --%>
    <asp:HiddenField ID="hdTargetPeriodo" runat="server" />
    <cc1:ModalPopupExtender ID="mpePeriodo" runat="server" TargetControlID="hdTargetPeriodo"
        PopupControlID="pnlPeriodo" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPeriodo" runat="server" BorderColor="" BackColor="White" Height="150px"
        Width="280px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="Label2" runat="server" Text="Seleccione el periodo"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="Label1" runat="server" Text="Periodo:" CssClass="lblInput"></asp:Label>
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtPeriodo" type="date" runat="server" placeholder="MM/YYYY" CssClass="form-control"></asp:TextBox>
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
                                <asp:Button ID="btnAceptarPeriodo" runat="server" Text="Aceptar" OnClientClick="OcultarModal();" OnClick="btnAceptarPeriodo_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="OcultarModal();" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- Modal Confirm --%>
    <asp:HiddenField ID="hdTargetConfirm" runat="server" />
    <cc1:ModalPopupExtender ID="mpeConfirm" runat="server" TargetControlID="hdTargetConfirm"
        PopupControlID="pnlConfirm" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirm" runat="server" BackColor="White" Style="display: none;" CssClass="modalrlr">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" runat="server" id="tdCaption">&nbsp;
                            <center>
                                <h4>
                                    <asp:Label ID="lblCaption" runat="server"></asp:Label></h4>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px; vertical-align: middle; text-align: center">
                            <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Images/icons/information.png" Height="24" Width="24" />
                        </td>
                        <td style="text-align: left; vertical-align: middle">
                            <asp:Label ID="lblMessageConfirm" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Button ID="btnAceptConfirm" runat="server" Text="Si" OnClientClick="OcultarModalConfir();" OnClick="btnAceptConfirm_Click" CssClass="btn btn-primary" />
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="btnCancelConfirm" runat="server" Text="No" OnClick="btnCancelConfirm_Click" CssClass="btn btn-default" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <%-- Modal de Piernas --%>
    <asp:HiddenField ID="hdTargetPiernas" runat="server" />
    <cc1:ModalPopupExtender ID="mpePierna" runat="server" TargetControlID="hdTargetPiernas"
        PopupControlID="pnlPiernasModal" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPiernasModal" runat="server" BorderColor="" BackColor="White" Height="370px"
        Width="520px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaBuscaPiernas" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <h4>
                                <asp:Label ID="lblTituloPierna" runat="server" Text="Búsqueda de Piernas / Fecha Operación"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <center>
                                <asp:RadioButtonList ID="rblTipoFecha" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblTipoFecha_SelectedIndexChanged">
                                    <asp:ListItem Text="Fecha Vuelo" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Fecha Operación" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </center>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlPiernasMXN" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td width="30%">
                                <asp:Label ID="lblPierna" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="txtTripPiernas" runat="server" type="date" placeholder="Fecha de Inicio" Width="80%" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblReqFechaVlo" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </td>
                            <td width="40%">
                                <asp:Button ID="btnBuscarPiernas" runat="server" Text="Buscar" CssClass="btn btn-info" OnClick="btnBuscarPiernas_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="lblValFechaVlo" runat="server" Text="El campo es requerido." Visible="false" ForeColor="Red"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <center>
                                    <asp:Panel ID="pnlPiernas" runat="server" ScrollBars="Auto" Width="90%" Height="200">
                                        <asp:GridView ID="gvPiernas" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="table table-bordered table-striped table-hover" DataKeyNames="LegId">
                                            <emptydatatemplate>
                                                No existen registros para mostrar.
                                            </emptydatatemplate>
                                            <columns>
                                                <asp:TemplateField HeaderText=" Seleccione ">
                                                    <itemtemplate>
                                                        <asp:RadioButton ID="rbSelecciona" runat="server" OnClick="javascript:Selrdbtn(this.id)" />
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AeronaveMatricula" HeaderText="Matricula" />
                                                <asp:BoundField DataField="VueloClienteId" HeaderText="Clave del cliente" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="VueloContratoId" HeaderText="Clave del contrato" />
                                                <asp:BoundField DataField="Ruta" HeaderText="Ruta" />
                                                <asp:BoundField DataField="FechaVuelo" HeaderText="Fecha Vuelo" />
                                            </columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblMensajePiernas" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlFechaOpeMXN" runat="server" Height="240px" Visible="false">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 40%; text-align: right">Fecha Operación:
                            </td>
                            <td style="width: 60%; text-align: left">
                                <asp:TextBox ID="txtFechaOperacionMXN" runat="server" type="date"
                                    Width="80%" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table style="width: 100%">
                    <tr>
                        <td width="50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarPierna" runat="server" Text="Aceptar" OnClientClick="OcultarModalPiernasMXN();" OnClick="btnAceptarPierna_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td width="50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarPierna" runat="server" Text="Cancelar" OnClientClick="OcultarModalPiernasMXN();" OnClick="btnCancelarPierna_Click" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscarPiernas" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upgBuscarPiernas" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaBuscaPiernas">
            <ProgressTemplate>
                <div style="text-align: left">
                    <asp:Label ID="lblProgresBuscarPiernas" runat="server" Text="Por favor espere..." Font-Italic="true"></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>


    <%-- Modal de Piernas Dolares --%>
    <asp:HiddenField ID="hdTargetPiernasUSA" runat="server" />
    <cc1:ModalPopupExtender ID="mpePiernasUSA" runat="server" TargetControlID="hdTargetPiernasUSA"
        PopupControlID="pnlPiernasUSA" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPiernasUSA" runat="server" BorderColor="" BackColor="White" Height="370px"
        Width="520px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaPiernasUSA" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <h4>
                                <asp:Label ID="Label3" runat="server" Text="Búsqueda de Piernas / Fecha Operación"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <center>
                                <asp:RadioButtonList ID="rblTipoFechaUSA" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblTipoFechaUSA_SelectedIndexChanged">
                                    <asp:ListItem Text="Fecha Vuelo" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Fecha Operación" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </center>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlBusPiernasUSA" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblTripUSA" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td style="width: 30%">
                                <asp:TextBox ID="txtTrioUSA" runat="server" type="date" placeholder="Fecha de Inicio" Width="80%" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblReqFechaVloU" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="width: 40%">
                                <asp:Button ID="btnBuscarPiernasUSA" runat="server" Text="Buscar" CssClass="btn btn-info" OnClick="btnBuscarPiernasUSA_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="lblValFechaVloU" runat="server" Text="El campo es requerido." Visible="false" ForeColor="Red"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <center>
                                    <asp:Panel ID="pnlPiernasGridUSA" runat="server" ScrollBars="Auto" Width="90%" Height="200">
                                        <asp:GridView ID="gvPiernasUSA" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="table table-bordered table-striped table-hover" DataKeyNames="LegId">
                                            <emptydatatemplate>
                                                No existen registros para mostrar.
                                            </emptydatatemplate>
                                            <columns>
                                                <asp:TemplateField HeaderText=" Seleccione ">
                                                    <itemtemplate>
                                                        <asp:RadioButton ID="rbSeleccionaUSA" runat="server" OnClick="javascript:Selrdbtn(this.id)" />
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AeronaveMatricula" HeaderText="Matricula" />
                                                <asp:BoundField DataField="VueloClienteId" HeaderText="Clave del cliente" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="VueloContratoId" HeaderText="Clave del contrato" />
                                                <asp:BoundField DataField="Ruta" HeaderText="Ruta" />
                                                <asp:BoundField DataField="FechaVuelo" HeaderText="Fecha Vuelo" />
                                            </columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblMensajePiernasUSA" runat="server" Text="" CssClass="validar"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlFechaOpeUSA" runat="server" Height="240px" Visible="false">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 40%; text-align: right">Fecha Operación:
                            </td>
                            <td style="width: 60%; text-align: left">
                                <asp:TextBox ID="txtFechaOperacionUSA" runat="server" type="date"
                                    Width="80%" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarPiernaUSA" runat="server" Text="Aceptar" OnClientClick="OcultarModalPiernasUSD();" OnClick="btnAceptarPiernaUSA_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarPiernaUSA" runat="server" Text="Cancelar" OnClientClick="OcultarModalPiernasUSD();" OnClick="btnCancelarPiernaUSA_Click" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscarPiernasUSA" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAceptarPiernaUSA" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaPiernasUSA">
            <ProgressTemplate>
                <div style="text-align: left">
                    <asp:Label ID="lblProgresBuscarPiernasUSA" runat="server" Text="Por favor espere..." Font-Italic="true"></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>


    <%-- Modal de Gastos Estimados --%>
    <asp:HiddenField ID="hdTargetGastosEstimados" runat="server" />
    <cc1:ModalPopupExtender ID="mpeGastosEstimados" runat="server" TargetControlID="hdTargetGastosEstimados"
        PopupControlID="pnlGastosEstimados" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlGastosEstimados" runat="server" BorderColor="" BackColor="White" Height="230px"
        Width="400px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaGastosEstimados" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="lblTituloMatricula" runat="server" Text="Agregar gasto estimado"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <asp:Label ID="lblNoReferencia" runat="server" Text="Referencia:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txtNoReferencia" runat="server" Width="85%"></asp:TextBox><asp:Label ID="lblReqNoReferencia" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <asp:Label ID="Label4" runat="server" Text="Proveedor:"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:DropDownList ID="ddlProveedor" runat="server" Width="90%"></asp:DropDownList><asp:Label ID="lblReqProveedor" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblImporte" runat="server" Text="Importe:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtImporte" runat="server" Width="85%"></asp:TextBox><asp:Label ID="lblReqImporte" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            <cc1:FilteredTextBoxExtender ID="ftbImporte" runat="server" TargetControlID="txtImporte" FilterMode="ValidChars"
                                ValidChars="0123456789.-">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%"></td>
                        <td style="text-align: left; width: 70%"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblRubro" runat="server" Text="Rubro:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlRubro" runat="server" Width="90%"></asp:DropDownList><asp:Label ID="lblReqRubro" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErrorGastoEstimado" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarEstimado" runat="server" Text="Aceptar" OnClick="btnAceptarEstimado_Click" CssClass="btn btn-primary" />
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarEstimado" runat="server" Text="Cancelar" OnClientClick="OcultarModalEstimados();" CssClass="btn btn-default" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upgGastosEstimados" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaGastosEstimados">
            <ProgressTemplate>
                <div style="text-align: left">
                    <asp:Label ID="lblProgresGastosEstimados" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
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
