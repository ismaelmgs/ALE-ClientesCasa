<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmAEContratos.aspx.cs" UICulture="es" Culture="es-MX" EnableEventValidation="false" Inherits="ClientesCasa.Views.ALEExchange.frmAEContratos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="~/Scripts/jquery.validate.js"></script>

    <style type="text/css">
        .overlayy {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            filter: alpha(opacity=80);
            opacity: 0.8;
            background: rgba(0,0,0,0.8);
        }

        label.error {
            color: red;
            display: inline-flex;
        }

    </style>



    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript" lang="javascript">

        function MostrarMensaje(mensaje, titulo) {
            var ventana = $('<div id="errortitulo" title="' + titulo + '"><span id="errormensaje">' + mensaje + '</span></div>');

            ventana.dialog({
                modal: true,
                buttons: { "Aceptar": function () { $(this).dialog("close"); } },
                show: "fold",
                hide: "scale",
            });
        }
    </script>
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

        function DeleteConfirmation() {
            if (confirm("¿Realmente esta seguro de eliminar el registro?"))
                return true;
            else
                return false;
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
        function OcultarModalMatriculas() {
            var modalId = '<%=mpeMatricula.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
        function validarEmail(valor) {
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(valor)) {
                alert("La dirección de email " + valor + " es correcta.");
            } else {
                alert("La dirección de email es incorrecta.");
            }
        }
        function OcultarModalIntercambio() {
            var modalId = '<%=mpeAltaIntercambio.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
        
    </script>


    <style type="text/css">
        .headerCssClass{  
            padding:10px;  
            border: 1px solid #efefef;
            border-radius: 4px 4px;
            background: #efefef;
            color: #666;
            cursor: pointer;
            /*border-bottom: 1px solid #CDCDCD;*/
        }  
        .contentCssClass{  
            background-color:#ffffff;  
            color:black;  
            border:1px solid #abc;  
            padding:10px;  
            border-radius: 2px 2px;
            border-top: 1px solid none;
        }  
        .headerSelectedCss{  
            background-color:#0c1f33;  
            color:white;  
            border:1px solid #0c1f33;  
            padding:10px;  
            border-radius: 4px 4px;
            font-weight: 600;
            cursor: pointer;
        }  
    </style>  

    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Clientes - Contratos</h4>
    </div>
    <cc1:ToolkitScriptManager ID="ToolKitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:Panel ID="pnlBusqueda" runat="server">
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
                                                <asp:TextBox ID="txtBusqueda" runat="server" CssClass="span11" placeholder="Buscar"></asp:TextBox>
                                            </div>
                                            <div class="col span_1_of_4">
                                                <asp:DropDownList ID="ddlOpcion" runat="server" placeholder="Seleccione">
                                                    <asp:ListItem Text="Clave Cliente" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Clave Contrato" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Matrícula" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Solo Activos" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inactivos" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col span_1_of_4">
                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                                                <asp:Button ID="btnAgregarCliente" runat="server" Text="Agregar" CssClass="btn btn-info" OnClick="btnAgregarCliente_Click" />
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
                        <td style="width: 15%"></td>
                        <td style="width: 60%">
                            <div class="table-responsive" style="margin: 5px;">
                                <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                                    DataKeyNames="IdCliente" AllowPaging="True" CssClass="table table-bordered table-striped table-hover"
                                    Width="100%" PageSize="5" OnPageIndexChanging="gvClientes_PageIndexChanging"
                                    OnRowDataBound="gvClientes_RowDataBound" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="ClaveCliente" HeaderText=" Clave de Cliente " />
                                        <asp:BoundField DataField="ClaveContrato" HeaderText=" Clave de Contrato " />
                                        <asp:BoundField DataField="Nombre" HeaderText=" Nombre Cliente " />
                                        <asp:BoundField DataField="Matricula" HeaderText=" Matrícula " />
                                        <asp:BoundField DataField="Descripcion" HeaderText=" Estatus " ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron registros para mostrar
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </td>
                        <td style="width: 15%"></td>
                    </tr>
                </table>

                <br />
                <br />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlAltaClientes" runat="server" Visible="false">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Datos del Cliente</h5>
            </div>
            <div class="widget-content nopadding">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <div class="section group">
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblClaveCliente" runat="server" Text="Clave del cliente:" CssClass=""></asp:Label>
                                    <asp:Label ID="lblReqClaveCliente" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align: left">
                                    <asp:TextBox ID="txtClaveCliente" runat="server" CssClass="" AutoPostBack="true" MaxLength="5" placeholder="Clave del cliente"></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblNombreClienteA" runat="server" Text="Nombre del cliente:" CssClass=""></asp:Label>
                                    <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align: left">
                                    <asp:TextBox ID="txtNombreClienteA" runat="server" CssClass="" MaxLength="60" placeholder="Nombre del cliente"></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social:"></asp:Label>
                                </div>
                                <div class="col span_1_of_8">
                                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="izquierdarlr" MaxLength="200" placeholder="Razón social"></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">&nbsp;</div>
                            </div>
                            <div class="section group">
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblTieneRFC" runat="server" Text="Tiene RFC: " CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:RadioButtonList ID="rblTieneRFC" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Si" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblRFC" runat="server" Text="RFC:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtRFC" runat="server" CssClass="celda1" MaxLength="13" placeholder="RFC"></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblTelefonoCliente" runat="server" Text="Teléfono:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="" MaxLength="20" placeholder="Teléfono cliente"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbTelefonoCliente" runat="server" TargetControlID="txtTelefonoCliente"
                                        FilterType="Numbers">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblActivo" runat="server" Text="Activo:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" Style="margin-right: 10%;" />
                                </div>
                            </div>

                            <div class="section group">
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                                <div class="col span_1_of_4" style="text-align: center;">
                                    <asp:Button ID="btnGuardarCliente" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarCliente_Click" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnAgregarContrato" runat="server" Text="Agregar contrato" CssClass="btn btn-success" OnClick="btnAgregarContrato_Click" Style="width: 130px !important;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="widget-box">
                                            <div class="widget-title">
                                                <span class="icon"><i class="icon-align-justify"></i></span>
                                                <h5>Listado de Contratos Asociados</h5>
                                            </div>
                                            <div class="controls" style="width: 100%; overflow: auto;">
                                                <asp:GridView ID="gvContratos" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="IdContrato,Matricula" AllowPaging="True" Width="100%"
                                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvContratos_RowCommand">
                                                    <EmptyDataTemplate>
                                                        No existen contratos para mostrar.
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave contrato" />
                                                        <asp:BoundField DataField="Matricula" HeaderText="Aeronave Matrícula" />
                                                        <asp:BoundField DataField="Serie" HeaderText="Aeronave serie" />
                                                        <asp:BoundField DataField="PorcParticipacion" HeaderText="% Participación" />
                                                        <asp:BoundField DataField="HorasContratadasTotal" HeaderText="Horas contratadas" />
                                                        <asp:BoundField DataField="EstatusContrato" HeaderText="Estatus" />
                                                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <div style="text-align: center">
                                                                    <asp:ImageButton ID="imbEditarContrato" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24"
                                                                        ToolTip="Edita un contrato." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                    <asp:ImageButton ID="imbEliminarContrato" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24"
                                                                        ToolTip="Elimina un contrato." OnClientClick="return DeleteConfirmation();" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="text-align: right">
                                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-info" OnClick="btnRegresar_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>

            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="upaContratos" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlContratos" runat="server" Visible="false">

                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-align-justify"></i></span>
                        <h5>Datos del Contrato</h5>
                    </div>
                    <div class="widget-content nopadding">
                        <div class="container">
                            <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="headerCssClass" ContentCssClass="contentCssClass" HeaderSelectedCssClass="headerSelectedCss" 
                                FadeTransitions="true" TransitionDuration="500" AutoSize="None">  
                                <Panes>

                                    <cc1:AccordionPane ID="AccGenerales" runat="server">  
                                        <Header>
                                            Generales 
                                        </Header>  
                                        <Content>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblClaveContrato" runat="server" Text="Clave del contrato:">
                                                    </asp:Label>
                                                    <asp:Label ID="lblReqClaveContrato" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:TextBox ID="txtClaveContrato" runat="server" CssClass="span11" MaxLength="10" placeholder="Clave de contrato"
                                                        AutoPostBack="true" OnTextChanged="txtClaveContrato_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblCleveCliente" runat="server" Text="Clave del Cliente:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="readClaveCliente" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblErrorClaveContrato" runat="server" Text="" ForeColor="Red" Visible="true" Font-Size="Small"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblAeronaveSerie" runat="server" Text="Aeronave serie:"></asp:Label>
                                                    <asp:Label ID="lblReqAeronaveSerie" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:TextBox ID="txtAeronaveSerie" runat="server" CssClass="span11" MaxLength="20" placeholder="Serie" ReadOnly="true" Width="60%"></asp:TextBox>
                                                    <asp:ImageButton ID="imbAeronave" runat="server" ImageUrl="~/Images/icons/find.png" ImageAlign="Middle" Height="30" OnClick="imbAeronave_Click" />
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblMatricula" runat="server" Text="Aeronave Matrícula:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="ReadMatricula" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblPorcentPart" runat="server" Text="Porcentaje participación:"></asp:Label>
                                                    <asp:Label ID="lblReqPorcentPart" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:TextBox ID="txtPorcentPart" runat="server" CssClass="span11" placeholder="Porcentaje"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftbPorcentPart" runat="server" TargetControlID="txtPorcentPart" ValidChars="0123456789."
                                                        FilterMode="ValidChars">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblHorasContratadas" runat="server" Text="Horas contratadas anuales:"></asp:Label>
                                                    <asp:Label ID="lblReqHorasContratadas" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:TextBox ID="txtHorasContratadas" runat="server" CssClass="span11" placeholder="Horas contratadas"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftbHorasContratadas" runat="server" TargetControlID="txtHorasContratadas"
                                                        ValidChars="0123456789" FilterMode="ValidChars">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblAplicaIntercambios" runat="server" Text="Aplica intercambios:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <div class="controls">
                                                        <asp:RadioButtonList ID="rblAplcaIntercambios" runat="server" CssClass="btn-group" AutoPostBack="true" OnSelectedIndexChanged="rblAplcaIntercambios_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Aplica" Value="1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="No Aplica" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblFactorIntercambio" runat="server" Text="Factor intercambio:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <div class="controls">
                                                        <asp:RadioButtonList ID="rblFactorIntercambio" runat="server" RepeatDirection="Horizontal" CssClass="btn-group">
                                                            <asp:ListItem Text="Tiempo" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Tarifa" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Ambos" Value="3"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblFechaContrato" runat="server" Text="Fecha inicio contrato:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:TextBox ID="txtFechaContrato" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                    <label for="txtFechaContrato" style="height: 24px; width: 24px" class="input-group-addon generic_btn">
                                                    </label>
                                                </div>
                                                <div class="col span_1_of_4">

                                                    <asp:Label ID="lblFechaFinContrato" runat="server" Text="Fecha fin contrato:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:TextBox ID="txtFechaFinContrato" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                    <label for="txtFechaFinContrato" style="height: 24px; width: 24px" class="input-group-addon generic_btn">
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblAnticipoContrato" runat="server" Text="Anticipo contrato:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <div style="vertical-align: middle; width: 100%">
                                                        <asp:TextBox ID="txtAnticipoContrato" runat="server" placeholder="Anticipo contrato" Width="100"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlMonedaAnticipo" runat="server" Width="80">
                                                            <asp:ListItem Text="USD" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="MXN" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblEstatusContrato" runat="server" Text="Estatus del contrato:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <asp:DropDownList ID="ddlEstatusContrato" runat="server" CssClass=""></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                    <asp:Label ID="lblTipoCosto" runat="server" Text="Tiempo para costo por Hr:"></asp:Label>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    <div class="controls" style="margin-left: -30%;">
                                                        <asp:RadioButtonList ID="rbtnTRipoCosto" runat="server" RepeatDirection="Horizontal" CssClass="radiorlr">
                                                            <asp:ListItem Text="Calzo" Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Vuelo" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="col span_1_of_4">
                                                    
                                                </div>
                                                <div class="col span_1_of_4" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="section group">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                                        </legend>
                                                        <div class="row">
                                                            <div style="margin-left: -12px;">
                                                                <div class="col-md-12">
                                                                    <div class="col-sm-10">
                                                                        <asp:GridView ID="gvBase" runat="server" KeyFieldName="IdBase" AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Aeropuerto" HeaderText="Base" />
                                                                                <asp:BoundField DataField="TipoBase" HeaderText="Tipo Base" />
                                                                                <asp:TemplateField HeaderText="Acciones">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imbEliminaBase" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <%--<div class="row">

                                                            <div class="col-sm-10">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:ASPxLabel runat="server" ID="lblGeneralesNotas" Text="Notas:" Theme="Office2010Black"></asp:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxMemo runat="server" Native="false" ID="txtGeneralesMemo" Width="100%" Theme="Office2003Blue" Height="80px" MaxLength="500">
                                                                            </dx:ASPxMemo>
                                                                        </td>
                                                                        <td style="width: 10px;"></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="col-sm-2"></div>
                                                        </div>--%>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            


                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4" style="text-align: right;">
                                                    <asp:Button ID="btnGuardarGen" runat="server" Text="Guardar" OnClick="btnGuardarGen_Click" CssClass="btn btn-primary"/>
                                                </div>
                                            </div>

                                        </Content>  
                                    </cc1:AccordionPane>  

                                    <cc1:AccordionPane ID="AccordionPane1" runat="server">  
                                        <Header>
                                            Tarifas 
                                        </Header>  
                                        <Content>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Vuelo</span>
                                                        </legend>
                                                        <div class="col-sm-12">
                                                            <asp:UpdatePanel ID="upaTarVuelos" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0">
                                                                        <tr>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lblTarifasCostoDirNac" runat="server" Text="Costo Directo Nacional(USD):"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTarifasCostoDirNac" runat="server" MaxLength="15">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftbTarifasCostoDirNac" runat="server" TargetControlID="txtTarifasCostoDirNac" FilterMode="ValidChars"
                                                                                    ValidChars="0123456789.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td style="text-align: left">
                                                                                <asp:Label ID="lblTarifasCostoDirInter" runat="server" Text="Costo Directo Internacional(USD):"></asp:Label>
                                                                                <br />
                                                                                <br />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTarifasCostoDirInter" runat="server" MaxLength="15">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftbTarifasCostoDirInter" runat="server" TargetControlID="txtTarifasCostoDirInter" FilterMode="ValidChars"
                                                                                    ValidChars="0123456789.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lblTarifasCombustible" runat="server" Text="Combustible:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <div class="col-sm-6" style="margin-left: -20px;">
                                                                                    <asp:RadioButtonList ID="rblTarifasCombustible" runat="server" AutoPostBack="true">
                                                                                        <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </div>
                                                                                <div class="col-sm-6">
                                                                                    
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lblTarifasCalculoPrecioCombustible" runat="server" Text="Cálculo precio de combustible:*"></asp:Label>
                                                                                <br />
                                                                                <br />
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="cboTarifaCalculoPrecioCombustible" AutoPostBack="true" OnSelectedIndexChanged="cboTarifaCalculoPrecioCombustible_SelectedIndexChanged" runat="server">
                                                                                    <Items>
                                                                                        <asp:ListItem Text="Ninguno" Value="-1" Selected="true" />
                                                                                        <asp:ListItem Text="Rango" Value="0" />
                                                                                        <asp:ListItem Text="Promedio" Value="1" />
                                                                                        <asp:ListItem Text="Real" Value="2" />
                                                                                        <asp:ListItem Text="Horas Descuento" Value="3" />
                                                                                    </Items>
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lblTarifaConsumo" runat="server" Text="Consumo Galones/Hr:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTarifaConsumo" runat="server" MaxLength="15">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftbTarifaConsumo" runat="server" TargetControlID="txtTarifaConsumo" FilterMode="ValidChars"
                                                                                    ValidChars="0123456789.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lblFactorTramosNal" runat="server" Text="Factor a Tramos Nacionales:"></asp:Label>
                                                                                <br />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFactorTramNal" runat="server" MaxLength="15">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftbFactorTramNal" runat="server" TargetControlID="txtFactorTramNal" FilterMode="ValidChars"
                                                                                    ValidChars="0123456789.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lblFactorTramosInt" runat="server" Text="Factor a Tramos Internacionales:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFactorTramInt" runat="server" MaxLength="15">
                                                                                </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="ftbFactorTramInt" runat="server" TargetControlID="txtFactorTramInt" FilterMode="ValidChars"
                                                                                    ValidChars="0123456789.">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Label ID="lblTarifaPrecioCombustibleInternacional" runat="server" Text="¿Precio Combustible internacional Especial?:"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2" style="text-align: center;">
                                                                                <div class="col-sm-1">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkTarifasPrecioEspecial" runat="server" AutoPostBack="true" OnCheckedChanged="chkTarifasPrecioEspecial_CheckedChanged"></asp:CheckBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-sm-12">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <asp:GridView ID="gvTarifaCombustible" Visible="false" runat="server" AutoGenerateColumns="False"
                                                                                    DataKeyNames="IdCombustible" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                                                                        <asp:BoundField DataField="Importe" HeaderText="Importe" />
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imbDeleteTarComb" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Tiempo de Espera</span>
                                                        </legend>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:UpdatePanel ID="upaTarTiempoEspera" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <table style="width:100%">
                                                                            <tr>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lblTarifaCobroEspera" runat="server" Text="Cobro:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <div style="margin-left: 110px;">
                                                                                        <asp:RadioButtonList ID="rdlListTarifaCoroEspera" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rdlListTarifaCoroEspera_SelectedIndexChanged">
                                                                                            <Items>
                                                                                                <asp:ListItem Selected="true" Text="Si" Value="1" />
                                                                                                <asp:ListItem Text="No" Value="0" />
                                                                                            </Items>
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                                <td>
                                                                                    <div class="col-sm-7">
                                                                                        <asp:Label ID="lblTarifaTarifa" runat="server" Text="Tarifa en Dlls(fija):"></asp:Label>
                                                                                    </div>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <div class="col-sm-7">
                                                                                        <asp:Label ID="lblTarifaPorcentaje" runat="server" Text="% de Tarifa de Vuelo (Variable)"></asp:Label>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lblTarifaTiempoEsperaNaciona" runat="server" Text="Nacional:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-sm-1"></div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:TextBox ID="txtTarifaTiempoEsperaNacionaFija" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtTarifaTiempoEsperaNacionaFija_TextChanged">
                                                                                        </asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="ftbTarifaTiempoEsperaNacionaFija" runat="server" TargetControlID="txtTarifaTiempoEsperaNacionaFija"
                                                                                            FilterMode="ValidChars" ValidChars="0123456789.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </div>
                                                                                    <br />
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <div class="col-sm-1"></div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:TextBox ID="txtTarifaTiempoEsperaNacionaVariable" Text="0" runat="server" AutoPostBack="true" OnTextChanged="txtTarifaTiempoEsperaNacionaVariable_TextChanged">
                                                                                        </asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="ftbTarifaTiempoEsperaNacionaVariable" runat="server" TargetControlID="txtTarifaTiempoEsperaNacionaVariable"
                                                                                            FilterMode="ValidChars" ValidChars="0123456789.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </div>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lblTarifaIntercional" runat="server" Text="Internacional:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-sm-1"></div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:TextBox ID="txtTarifaTiempoEsperaInternacinalFija" Text="0" runat="server" OnTextChanged="txtTarifaTiempoEsperaInternacinalFija_TextChanged" AutoPostBack="true">
                                                                                        </asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="ftbTarifaTiempoEsperaInternacinalFija" runat="server" TargetControlID="txtTarifaTiempoEsperaInternacinalFija"
                                                                                            FilterMode="ValidChars" ValidChars="0123456789.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </div>
                                                                                </td>
                                                                                <td></td>
                                                                                <td>
                                                                                    <div class="col-sm-1"></div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:TextBox ID="txtTarifaTiempoEsperaInternacinalNacional" Text="0" runat="server" OnTextChanged="txtTarifaTiempoEsperaInternacinalNacional_TextChanged" AutoPostBack="true">
                                                                                        </asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="ftbTarifaTiempoEsperaInternacinalNacional" runat="server" TargetControlID="txtTarifaTiempoEsperaInternacinalNacional"
                                                                                            FilterMode="ValidChars" ValidChars="0123456789.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="rdlListTarifaCoroEspera" EventName="SelectedIndexChanged" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Pernoctas</span>
                                                        </legend>
                                                        <div class="col-sm-12">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lblTarifaPernoctasCobro" runat="server" Text="Cobro:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <div style="margin-left: 110px;">
                                                                            <asp:RadioButtonList ID="rdlListTarifaCobro" AutoPostBack="true" OnSelectedIndexChanged="rdlListTarifaCobro_SelectedIndexChanged" runat="server">
                                                                                <Items>
                                                                                    <asp:ListItem Selected="True" Text="Si" Value="1" />
                                                                                    <asp:ListItem Text="No" Value="0" />
                                                                                </Items>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblTarifaTaridaDlls" runat="server" Text="Tarifa en Dlls(fija):"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="txtTarifaTarifaDll" runat="server" Text="% de Tarifa de Vuelo (Variable):"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lblTarifaNacional" runat="server" Text="Nacional:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-sm-1"></div>
                                                                        <div class="col-sm-1">
                                                                            <asp:TextBox ID="txtTarifaNacionalFija" AutoPostBack="true" OnTextChanged="txtTarifaNacionalFija_TextChanged" Text="0" runat="server">
                                                                            </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="ftbTarifaNacionalFija" runat="server" TargetControlID="txtTarifaNacionalFija"
                                                                                FilterMode="ValidChars" ValidChars="0123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <br />
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <div class="col-sm-1"></div>
                                                                        <div class="col-sm-1">
                                                                            <asp:TextBox ID="txtTarifaNacionalVarable" AutoPostBack="true" OnTextChanged="txtTarifaNacionalVarable_TextChanged" Text="0" runat="server" />
                                                                            <cc1:FilteredTextBoxExtender ID="ftbTarifaNacionalVarable" runat="server" TargetControlID="txtTarifaNacionalVarable"
                                                                                FilterMode="ValidChars" ValidChars="0123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lblTarifaInternacional" runat="server" Text="Internacional:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-sm-1"></div>
                                                                        <div class="col-sm-1">
                                                                            <asp:TextBox ID="txtTarifaInternacionalFija" AutoPostBack="true" OnTextChanged="txtTarifaInternacionalFija_TextChanged" Text="0" runat="server" />
                                                                            <cc1:FilteredTextBoxExtender ID="ftbTarifaInternacionalFija" runat="server" TargetControlID="txtTarifaInternacionalFija"
                                                                                FilterMode="ValidChars" ValidChars="0123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <div class="col-sm-1"></div>
                                                                        <div class="col-sm-1">
                                                                            <asp:TextBox ID="txtTarifaInternacionalVariable" Text="0" runat="server" AutoPostBack="true" OnTextChanged="txtTarifaInternacionalVariable_TextChanged" />
                                                                            <cc1:FilteredTextBoxExtender ID="ftbTarifaInternacionalVariable" runat="server" TargetControlID="txtTarifaInternacionalVariable"
                                                                                FilterMode="ValidChars" ValidChars="0123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>

                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4" style="text-align: right;">
                                                    <asp:Button ID="btnRegresarTar" runat="server" Text="Regresar" OnClick="btnRegresarTar_Click" CssClass="btn btn-default"/>
                                                    <asp:Button ID="btnGuardarTar" runat="server" Text="Guardar" OnClick="btnGuardarTar_Click" CssClass="btn btn-primary"/>
                                                </div>
                                            </div>
                                        </Content>  
                                    </cc1:AccordionPane>  

                                    <cc1:AccordionPane ID="AccordionPane2" runat="server">  
                                        <Header>
                                            Descuentos 
                                        </Header>  
                                        <Content>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                                        </legend>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosDescClaveCleinte" Text="Clave del Cliente:"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosClaveCliente"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosDescClaveContrato" Text="Clave del Contrato:"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosClaveContrato"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosDescTipoPaquete" Text="Tipo de Paquete:"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosTipoPaquete"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosDescGrupoAeronave" Text="Grupo de Aeronave:"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label runat="server" ID="lblCobrosGrupoAeronave"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br />
                                                            </div>
                                                        </div>

                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Descuento de horas de paquete</span>
                                                        </legend>
                                                        <div class="col-md-12">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblFerrysConCargo" runat="server" Text="Ferrys con cargo">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td style="width: 40px">&nbsp;
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblEsperalibre" runat="server" Text="Espera Libre"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label5" runat="server" Text="Ninguno">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:RadioButton ID="rdlCobroFerrysNinguno" runat="server"></asp:RadioButton>
                                                                    </td>
                                                                    <td style="width: 40px">&nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label6" runat="server" Text="¿Aplica espera libre?">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:CheckBox ID="chkCobroAplicaEsperaLibre" runat="server"></asp:CheckBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label7" runat="server"
                                                                            Text="Todos">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:RadioButton ID="rdlCobroFerrysTodos" runat="server" Checked="false"></asp:RadioButton>
                                                                    </td>
                                                                    <td style="width: 40px">&nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label8" runat="server" Text="Horas por vuelo"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:TextBox ID="txtCobroHorasVuelo" runat="server" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbCobroHorasVuelo" runat="server" TargetControlID="txtCobroHorasVuelo" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblReposicionamiento" runat="server" Text="Ferrys de Reposicionamiento"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:RadioButton ID="rdlCobroFerrysReposicionamiento" runat="server" Checked="false"></asp:RadioButton>
                                                                    </td>
                                                                    <td style="width: 40px">&nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblFactorHrsVuelo" runat="server" Text="Factor de hrs de vuelo"></asp:Label>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:TextBox ID="txtFactorHrsVuelo" runat="server" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbFactorHrsVuelo" runat="server" TargetControlID="txtFactorHrsVuelo" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Pernoctas</span>
                                                        </legend>
                                                        <div class="col-md-12">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div class="col-sm-3"></div>
                                                                        <asp:Label ID="lblPernoctasNacional" runat="server" Text="Nacional">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <div class="col-sm-4">
                                                                            <asp:Label ID="lblPernoctasInternacional" runat="server" Text="Internacional">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblDescuentanPernoctasNacional" runat="server" Text="Se descuentan">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlDescuentanPernoctasNacional" runat="server" AutoPostBack="true" OnCheckedChanged="rdlDescuentanPernoctasNacional_CheckedChanged" Checked="false" Text="Si"></asp:RadioButton>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlNoDescuentanPernoctasNacional" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="rdlNoDescuentanPernoctasNacional_CheckedChanged" Text="No"></asp:RadioButton>
                                                                        </div>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlDescuentanPernoctasInternacional" runat="server" AutoPostBack="true"
                                                                                OnCheckedChanged="rdlDescuentanPernoctasInternacional_CheckedChanged" Checked="false" Text="Si"></asp:RadioButton>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlNoDescuentanPernoctasInternacional"
                                                                                AutoPostBack="true" OnCheckedChanged="rdlNoDescuentanPernoctasInternacional_CheckedChanged" runat="server" Checked="true" Text="No"></asp:RadioButton>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblPernoctaFactorConversion" runat="server"
                                                                            Text="Factor de conversión a hrs de vuelo">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPernoctaFactorConversionNacional" runat="server" Text="0" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbPernoctaFactorConversionNacional" runat="server" TargetControlID="txtPernoctaFactorConversionNacional" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txtPernoctaFactorConversionInternacional" Text="0" runat="server" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbPernoctaFactorConversionInternacional" runat="server" TargetControlID="txtPernoctaFactorConversionInternacional" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="lblDescuentoPernoctasLibres" Text="Número de pernoctas libres anual"></asp:Label>
                                                                    </td>
                                                                    <td colspan="2" valign="top">
                                                                        <asp:TextBox runat="server" ID="txtDescuentoPernoctasLibres" Text="0" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbDescuentoPernoctasLibres" runat="server" TargetControlID="txtDescuentoPernoctasLibres" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                    <td style="padding-left: 40px;">
                                                                        <br />
                                                                        <asp:CheckBoxList runat="server" ID="chkListDescuentosPrecnoctas">
                                                                            <Items>
                                                                                <asp:ListItem Text="Descuento" Value="1" />
                                                                                <asp:ListItem Text="Cobro" Value="2" />
                                                                            </Items>
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Tiempo de Espera</span>
                                                        </legend>
                                                        <div class="col-md-12">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div class="col-sm-3"></div>
                                                                        <asp:Label ID="Label1" runat="server" Text="Nacional"></asp:Label>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <div class="col-sm-4">
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Internacional"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label3" runat="server" Text="Se descuentan"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlDescuentanTiempoEsperaNacional" AutoPostBack="true" OnCheckedChanged="rdlDescuentanTiempoEsperaNacional_CheckedChanged" runat="server" Checked="false" Text="Si"></asp:RadioButton>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlNoDescuentanTiempoEsperaNacional" AutoPostBack="true" OnCheckedChanged="rdlNoDescuentanTiempoEsperaNacional_CheckedChanged" runat="server" Checked="true" Text="No"></asp:RadioButton>
                                                                        </div>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlDescuentanTiempoEsperaInternaacional" AutoPostBack="true" OnCheckedChanged="rdlDescuentanTiempoEsperaInternaacional_CheckedChanged" runat="server" Checked="false" Text="Si"></asp:RadioButton>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:RadioButton ID="rdlNoDescuentanTiempoEsperaInternaacional" AutoPostBack="true" OnCheckedChanged="rdlNoDescuentanTiempoEsperaInternaacional_CheckedChanged" runat="server" Checked="true" Text="No"></asp:RadioButton>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label4" runat="server" Text="Factor de conversión a hrs de vuelo"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTiempoEsperaFactorHrsVueloNal" Text="0" runat="server" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbTiempoEsperaFactorHrsVueloNal" runat="server" TargetControlID="txtTiempoEsperaFactorHrsVueloNal" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txtTiempoEsperaFactorHrsVueloInt" Text="0" runat="server" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbTiempoEsperaFactorHrsVueloInt" runat="server" TargetControlID="txtTiempoEsperaFactorHrsVueloInt" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Tiempo a Facturar</span>
                                                        </legend>
                                                        <div class="col-md-12">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 25%">&nbsp;</td>
                                                                    <td style="width: 25%">
                                                                        <asp:RadioButton ID="rdlTiempoFacturarCalzo" runat="server" Text="Calzo"></asp:RadioButton>
                                                                    </td>
                                                                    <td style="width: 25%">&nbsp;
                                                                    </td>
                                                                    <td style="width: 25%">&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">&nbsp;</td>
                                                                    <td style="width: 25%">
                                                                        <asp:RadioButton ID="rdlTiempoFacturarVuelo" runat="server" Text="Vuelo"></asp:RadioButton>
                                                                        <br />
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <asp:Label ID="lblMasMinutos" runat="server" Text="+Minutos"></asp:Label>
                                                                        <asp:TextBox ID="txtTiempoFacturarMasMinutos" runat="server" />
                                                                        <cc1:FilteredTextBoxExtender ID="ftbTiempoFacturarMasMinutos" runat="server" TargetControlID="txtTiempoFacturarMasMinutos" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="width: 25%">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Cobro Servicio con Cargo</span>
                                                        </legend>
                                                        <div class="col-md-12">
                                                            <asp:GridView ID="gvServicioConCargo" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="IdServicioConCargo">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ServicioConCargoDescripcion" HeaderText="Servicio con cargo" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkServCargo" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <fieldset class="Personal1">
                                                        <legend>
                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Tramo Pactado</span>
                                                        </legend>
                                                        <table>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblCobrosAplicaTramo" runat="server" Text="¿Aplica tramo pactado?"></asp:Label>
                                                                </td>
                                                                <td colspan="2" style="text-align: center;">
                                                                    <div class="col-sm-1">
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkCobrosAplicaTramo" runat="server" AutoPostBack="true" TextAlign="Left" OnCheckedChanged="chkCobrosAplicaTramo_CheckedChanged"></asp:CheckBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <div class="col-md-12">
                                                            <asp:HiddenField ID="hdnOrigen" runat="server"></asp:HiddenField>
                                                            <asp:GridView ID="gvTramoPactado" runat="server" DataKeyNames="IdTramo" AutoGenerateColumns="false" Width="100%">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Origen" HeaderText="Origen" />
                                                                    <asp:BoundField DataField="Destino" HeaderText="Destino" />
                                                                    <asp:BoundField DataField="Tiempo" HeaderText="Tiempo" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnEditarTramoPactado" runat="server" Text="Editar" />
                                                                            <asp:Button ID="btneliminarTramoPactado" runat="server" Text="Eliminar" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <br />
                                                    </fieldset>
                                                </div>
                                            </div>

                                            <div class="section group">
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4">
                                                </div>
                                                <div class="col span_1_of_4" style="text-align: right;">
                                                    <asp:Button ID="btnRegresarDesc" runat="server" Text="Regresar" OnClick="btnRegresarDesc_Click" CssClass="btn btn-default"/>
                                                    <asp:Button ID="btnGuardarDesc" runat="server" Text="Guardar" OnClick="btnGuardarDesc_Click" CssClass="btn btn-primary"/>
                                                </div>
                                            </div>

                                        </Content>
                                    </cc1:AccordionPane> 

                                    <cc1:AccordionPane ID="AccordionPane3" runat="server">  
                                        <Header>
                                            Intercambios 
                                        </Header>  
                                        <Content>
                                            <asp:Panel ID="pnlIntercambios" runat="server">
                                                <fieldset class="Personal1">
                                                <legend>
                                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Listado de Intercambios</span>
                                                </legend>
                                                <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <div style="float: right">
                                                                        <asp:Button ID="btnAgregaIntercambio" runat="server" Text="Agrega Intercambio" CssClass="btn btn-success btnrlr" 
                                                                            OnClick="btnAgregaIntercambio_Click" Style="width: 150px !important;" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="div1" style="text-align: center;">
                                                                        <asp:GridView ID="gvIntercambio" runat="server" AutoGenerateColumns="False"
                                                                            DataKeyNames="IdIntercambio" OnRowCommand="gvIntercambio_RowCommand"
                                                                            CssClass="table table-bordered table-striped table-hover" PageSize="3" Style="overflow-x: auto;">
                                                                            <EmptyDataTemplate>
                                                                                No existen intercambios para mostrar.
                                                                            </EmptyDataTemplate>
                                                                            <Columns>
                                                                                <asp:BoundField DataField="GrupoModelo" HeaderText="Grupo de Modelo" />
                                                                                <asp:CheckBoxField DataField="AplicaFerry" HeaderText="Aplica Ferry" />
                                                                                <asp:BoundField DataField="Factor" HeaderText="Factor Intercambio" />
                                                                                <asp:BoundField DataField="TarifaNal" HeaderText="Tarifa Nacional Dlls" DataFormatString="{0:C}" />
                                                                                <asp:BoundField DataField="TarifaInt" HeaderText="Tarifa Internacional Dlls" DataFormatString="{0:C}" />
                                                                                <asp:BoundField DataField="Galones" HeaderText="Galones / Hr de vuelo"/>
                                                                                <asp:BoundField DataField="CostoDirectoNal" HeaderText="Costo Directo Nacional Dlls" DataFormatString="{0:C}" />
                                                                                <asp:BoundField DataField="CostoDirectoInt" HeaderText="Costo Directo Internacional Dlls" DataFormatString="{0:C}" />

                                                                                <asp:TemplateField HeaderText="Acciones">
                                                                                    <ItemTemplate>
                                                                                        <div style="text-align: center">
                                                                                            <asp:ImageButton ID="imbEditarIntercambio" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24"
                                                                                                ToolTip="Edita un intercambio." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                            <asp:ImageButton ID="imbEliminaIntercambio" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24"
                                                                                                ToolTip="Elimina un intercambio." OnClientClick="return DeleteConfirmation();" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </fieldset>
                                                <div class="section group">
                                                    <div class="col span_1_of_4">
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                    </div>
                                                    <div class="col span_1_of_4">
                                                    </div>
                                                    <div class="col span_1_of_4" style="text-align: right;">
                                                        <asp:Button ID="btnRegresarContratos" runat="server" Text="Regresar" CssClass="btn btn-info" OnClick="btnRegresarContratos_Click" Style="margin-right: 7% !important;" />
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </Content>  
                                    </cc1:AccordionPane>

                                </Panes>
                            </cc1:Accordion> 

                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:HiddenField ID="hdTargetMatricula" runat="server" />
    <cc1:ModalPopupExtender ID="mpeMatricula" runat="server" TargetControlID="hdTargetMatricula"
        PopupControlID="pnlMatricula" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlMatricula" runat="server" BorderColor="Black" BackColor="White" Height="450px"
        Width="450px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <h4>
                        <asp:Label ID="lblTituloMatricula" runat="server" Text="Lista de matrículas"></asp:Label></h4>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlMatriculas" runat="server" Width="440" Height="310" ScrollBars="Auto">
                        <asp:GridView ID="gvMatriculas" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="table table-bordered table-striped table-hover" EnableViewState="true">
                            <Columns>
                                <asp:TemplateField HeaderText=" Seleccione ">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rbSelecciona" runat="server" OnClick="javascript:Selrdbtn(this.id)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Serie" HeaderText=" No. Serie " />
                                <asp:BoundField DataField="Matricula" HeaderText=" Matrícula " />
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros para mostrar
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblErrorMat" runat="server" Text="" CssClass="validar"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td width="50%">
                    <div style="text-align: right; float: right">
                        <asp:Button ID="btnAceptarMatriculas" runat="server" Text="Aceptar" CssClass="btn btn-primary"
                            OnClick="btnAceptarMatriculas_Click" />
                    </div>
                </td>
                <td width="50%">
                    <div style="text-align: left; float: left">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default"
                            OnClientClick="OcultarModalMatriculas();" />
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:HiddenField ID="htTargetAltaIntercambio" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAltaIntercambio" runat="server" TargetControlID="htTargetAltaIntercambio"
        PopupControlID="pnlModalIntercambios" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlModalIntercambios" runat="server" BorderColor="Black" BackColor="White" Height="370px"
        Width="380px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaAltaIntercambio" runat="server">
            <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td colspan="2">
                            <h4><asp:Label ID="lblTituloAltaInter" runat="server" Text=""></asp:Label></h4>
                        </td>
                    </tr>
                </table>
                <table style="width:100%; text-align:center">
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <fieldset>
                                <legend>
                                    Tipo de intercambio
                                </legend>
                                <asp:RadioButtonList ID="rblTipoIntercambio" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" 
                                    OnSelectedIndexChanged="rblTipoIntercambio_SelectedIndexChanged" CssClass="btn-group">
                                    <asp:ListItem Text="Factor" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Tarifa" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Costo directo + galones" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width:100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel1" runat="server">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblGrupoModeloId" runat="server" Text="Grupo modelo:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:DropDownList ID="ddlGrupoModelo" runat="server" CssClass="slcList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlFactor" runat="server" Visible="false">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblIntercambioFactor" runat="server" Text="Factor de Intercambio:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:TextBox ID="txtIntercambioFactor" runat="server" CssClass=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblAplicaFerry" runat="server" Text="Aplica Ferry:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:CheckBox ID="chkAplicaFerry" runat="server" CssClass=""></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlTarifas" runat="server" Visible="false">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblTarifaNalDlls" runat="server" Text="Tarifa nacional dlls:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:TextBox ID="txtTarifaNalDlls" runat="server" CssClass=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblTarifaInterDlls" runat="server" Text="Aplica Ferry:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:TextBox ID="txtTarifaInterDlls" runat="server" CssClass=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlCostoDir" runat="server" Visible="false">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblGalonesHrVuelo" runat="server" Text="Galones / Hr de vuelo:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:TextBox ID="txtGalnesHrVuelo" runat="server" CssClass=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblCostoDirNalDlls" runat="server" Text="Tarifa nacional dlls:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:TextBox ID="txtCostoDirNalDlls" runat="server" CssClass=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:50%; text-align:left">
                                            <asp:Label ID="lblCostoDirIntDlls" runat="server" Text="Tarifa internacional dlls:" CssClass=""></asp:Label>
                                        </td>
                                        <td style="width:50%; text-align:left">
                                            <asp:TextBox ID="txtCostoDirIntDlls" runat="server" CssClass=""></asp:TextBox>
                                        </td>
                                    </tr>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width:50%">
                            <div style="text-align: right; float: right">
                                <asp:Button ID="btnAceptarAltaIntercambio" runat="server" Text="Aceptar" CssClass="btn btn-primary"
                                    OnClick="btnAceptarAltaIntercambio_Click" />
                            </div>
                        </td>
                        <td style="width:50%">
                            <div style="text-align: left; float: left">
                                <asp:Button ID="btnCancelarAltaIntercambio" runat="server" Text="Cancelar" CssClass="btn btn-default"
                                    OnClientClick="OcultarModalIntercambio();" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upgIntercambios" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaAltaIntercambio">
            <ProgressTemplate>
                <div style="text-align: left">
                    <asp:Label ID="lblProgresIntercambios" runat="server" Text="Por favor espere..."></asp:Label>
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
