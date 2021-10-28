<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmContratos.aspx.cs" UICulture="es" Culture="es-MX" EnableEventValidation="false" Inherits="ClientesCasa.Views.Catalogos.frmContratos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="~/Scripts/jquery.validate.js"></script>

    
    <style type="text/css">
        .overlayy
        {
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
            display:inline-flex ;                 
        }  
    </style>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    
    <!-- <script type="text/javascript">
        $(function () {
            $("[id$=txtFechaContrato]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'http://www.cbtis46.edu.mx/wp-content/uploads/2014/09/calendario-150x150.png',
                format: 'dd/mm/yyyy',
                language: 'ru'
            });
        });
        $(function () {
            $("[id$=txtFechaFinContrato]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'http://www.cbtis46.edu.mx/wp-content/uploads/2014/09/calendario-150x150.png',
                format: 'dd/mm/yyyy',
                language: 'ru'
            });
        });
        $(function () {
            $("[id$=txtFechaInicioSeguro]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'http://www.cbtis46.edu.mx/wp-content/uploads/2014/09/calendario-150x150.png',
                format: 'dd/mm/yyyy',
                language: 'ru'
            });
        });
        $(function () {
            $("[id$=txtFechaFinSeguro]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'http://www.cbtis46.edu.mx/wp-content/uploads/2014/09/calendario-150x150.png',
                format: 'dd/mm/yyyy',
                language: 'ru'
            });
        });
        
    </script> -->

    <script type="text/javascript">
 
        //$(function(){
        //    $("[id$=txtAnticipoContrato]").mask("999,999,999.99");
        //})
    </script>


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
        function OcultarModalArchivo() {

            var modalId = '<%=mpeArchivo.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
    </script>

    <script type ="text/javascript" >          
        $(document).ready(function () {
            //custom rule to check regular expression   
            $.validator.addMethod("match", function (value, element) {
                return this.optional(element) || /^[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+\.[a-zA-Z.]{2,5}$/i.test(value);
            }, "Please enter a valid email address.");
            $("#form1").validate({
                rules: {

                     <%=txtCorreoElectronico.UniqueID %>: {
                    match: true
                },  
                },
            messages: {
                <%=txtCorreoElectronico.UniqueID %>: {
                required: "Email Id is Required",
                remote: "This Email Id is already in use",
                //This section we need to place our custom validation message for each control.  

            },  
                 
            });  
        });   

    </script>  

    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Catálogo de Clientes</h4>
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
                        <div class="table-responsive" style="margin:5px;">
                        <table style="width: 100%" class="table table-striped">
                            <tr>
                                <td style="text-align: center">
                                    <div class="section group" style="margin-left:-30px;">
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
                                                <asp:ListItem Text="Matrícula" Value="4" Selected="True"></asp:ListItem>
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
                            <div class="table-responsive" style="margin:5px;">
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
	                            <asp:Label ID="lblClaveCliente" runat="server" Text="Clave del cliente:" CssClass=""></asp:Label> <asp:Label ID="lblReqClaveCliente" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtClaveCliente" runat="server" CssClass="" AutoPostBack="true" MaxLength="5" placeholder="Clave del cliente"></asp:TextBox>
	                            </div>
	                            <div class="col span_1_of_4">
	                            <asp:Label ID="lblNombreClienteA" runat="server" Text="Nombre del cliente:" CssClass=""></asp:Label> <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtNombreClienteA" runat="server" CssClass="" MaxLength="60" placeholder="Nombre del cliente"></asp:TextBox>
                         	    </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social:" ></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="izquierdarlr" MaxLength="75" placeholder="Razón social"></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">&nbsp;</div>
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
                                <div class="col span_1_of_4"><asp:Label ID="lblRFC" runat="server" Text="RFC:" CssClass=""></asp:Label></div>
                                <div class="col span_1_of_4"><asp:TextBox ID="txtRFC" runat="server" CssClass="celda1" MaxLength="13" placeholder="RFC"></asp:TextBox></div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblTipoContribuyente" runat="server" Text="Tipo de contribuyente:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlTipoContribuyente" runat="server" CssClass="dropizq">
                                    <asp:ListItem Text="Fisico" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Moral" Value="M"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblActivo" runat="server" Text="Activo:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" style="margin-right:10%;" />
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblTelefonoCliente" runat="server" Text="Teléfono:" CssClass="" style="margin-left: 5%;"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="" MaxLength="20" placeholder="Teléfono cliente"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbTelefonoCliente" runat="server" TargetControlID="txtTelefonoCliente"
                                         FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblFax" runat="server" Text="Fax:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFax" runat="server" CssClass="" MaxLength="20" placeholder="Fax"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbFax" runat="server" TargetControlID="txtFax"
                                        FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblCorreoElectronico" runat="server" Text="Correo electrónico:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <div class="controls">
                                        <asp:TextBox ID="txtCorreoElectronico" runat="server"
                                            placeholder="Correo electrónico" CausesValidation="True" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblSector" runat="server" Text="Sector:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlSector" runat="server" CssClass=""></asp:DropDownList>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblPais" runat="server" Text="País:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlPais" runat="server" CssClass="" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass=""></asp:DropDownList>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="" MaxLength="80" placeholder="Dirección"></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblCiudad" runat="server" Text="Ciudad:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtCiudad" runat="server" CssClass="" MaxLength="40" placeholder="Ciudad"></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblCP" runat="server" Text="C.P.:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtCP" runat="server" CssClass="izquierdarlr" placeholder="CP"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbCP" runat="server" TargetControlID="txtCP"
                                        FilterType="Numbers"></cc1:FilteredTextBoxExtender>

                                </div>
                                <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                                <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                            </div>
                            <hr />
                            <span><h4>Dirección de envío</h4></span>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblPaisDE" runat="server" Text="País:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlPaisDE" runat="server" CssClass="" AutoPostBack="true" OnSelectedIndexChanged="ddlPaisDE_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblEstadoDE" runat="server" Text="Estado:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlEstadoDE" runat="server" CssClass=""></asp:DropDownList>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblDireccionDE" runat="server" Text="Dirección:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtDireccionDE" runat="server" CssClass="" MaxLength="80" placeholder="Dirección"></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblCiudadDE" runat="server" Text="Ciudad:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtCiudadDE" runat="server" CssClass="" MaxLength="40" placeholder="Ciudad"></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblCPDE" runat="server" Text="C.P.:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtCPDE" runat="server" CssClass="izquierdarlr" placeholder="CP"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbCPDE" runat="server" TargetControlID="txtCPDE"
                                        FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                </div>
                                <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                                <div class="col span_1_of_4" style="text-align:center;">
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
                            <table style="width:100%">
                                <tr>
                                    <td style="text-align:right">
                                        <asp:Button ID="btnAgregarContrato" runat="server" Text="Agregar contrato" CssClass="btn btn-success" OnClick="btnAgregarContrato_Click" style="width:130px !important;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="widget-box">
                                            <div class="widget-title">
                                                <span class="icon"><i class="icon-align-justify"></i></span>
                                                <h5>Listado de Contratos Asociados</h5>
                                            </div>
                                            <div class="controls" style="width:100%; overflow:auto;">
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
                                        <div style="text-align:right">
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

    <asp:Panel ID="pnlContratos" runat="server" Visible="false">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Datos del Contrato</h5>
            </div>
            <div class="widget-content nopadding">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblClaveContrato" runat="server" Text="Clave del contrato:">
                                    </asp:Label> <asp:Label ID="lblReqClaveContrato" runat="server" Text="*" ForeColor="Red"></asp:Label>                      
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
                                        FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblHorasContratadas" runat="server" Text="Horas contratadas anuales:"></asp:Label>
                                    <asp:Label ID="lblReqHorasContratadas" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtHorasContratadas" runat="server" CssClass="span11" placeholder="Horas contratadas"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbHorasContratadas" runat="server" TargetControlID="txtHorasContratadas"
                                        ValidChars="0123456789" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
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
                                        <asp:RadioButtonList ID="rblFactorIntercambio" runat="server" RepeatDirection="Horizontal"  CssClass="btn-group">
                                            <asp:ListItem Text="Tiempo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Tarifa" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Ambos" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblFechaContrato" runat="server"  Text="Fecha inicio contrato:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFechaContrato" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    <label for="txtFechaContrato" style="height:24px; width:24px" class="input-group-addon generic_btn">
                                    </label>
                                </div>
                                <div class="col span_1_of_4">

                                    <asp:Label ID="lblFechaFinContrato" runat="server"  Text="Fecha fin contrato:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFechaFinContrato" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    <label for="txtFechaFinContrato" style="height:24px; width:24px" class="input-group-addon generic_btn">
                                    </label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblAnticipoContrato" runat="server" Text="Anticipo contrato:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <div style="vertical-align:middle; width:100%">
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
                                    <asp:Label ID="lblTipoCosto" runat="server" Text="Tiempo para costo por Hr:" ></asp:Label>
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
                                    <asp:Label ID="lblServicioConsultoria" runat="server" Text="Servicio de consultoría:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align:right;">
                                    <div class="controls" style="margin-left: -30%;">
                                        <asp:RadioButtonList ID="rblServicioConsultoria" runat="server" RepeatDirection="Horizontal" CssClass="radiorlr">
                                            <asp:ListItem Text="Flight Fast" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="AOC" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="No Aplica" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblTarifas" runat="server" Text="Tarifas:" ></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <div class="controls" style="margin-left: -30%;">
                                        <asp:RadioButtonList ID="rblTarifas" runat="server" RepeatDirection="Horizontal" CssClass="radiorlr">
                                            <asp:ListItem Text="Fija" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Light" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Flex" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblDetalleTarifa" runat="server" Text="Detalle tarifa:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align:right;">
                                    <div class="controls" style="margin-left: -30%;">
                                        <asp:RadioButtonList ID="rblDetalleTarifa" runat="server" RepeatDirection="Horizontal" CssClass="radiorlr">
                                        <asp:ListItem Text="A" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="section group">
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblRepEdoCuenta" runat="server" Text="Reporte Estado de Cuenta:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlRepEdoCuenta" runat="server" CssClass=""></asp:DropDownList>
                                </div>

                            </div>
                        </td>
                    </tr>
                </table>

                <fieldset style="width: 100%">
                    <legend>
                        <span>
                            Seguro Aeronave
                        </span>
                    </legend>
                    <table style="width:100%">
                        <tr>
                            <td>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblNoPoliza" runat="server" Text="No. de Póliza:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtNoPoliza" runat="server" plcaceholder="No. de póliza"></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblEmpresaAseguradora" runat="server" Text="Empresa Aseguradora:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtEmpresaAseguradora" runat="server" placeholder="Empresa Aseguradora"></asp:TextBox>
                                </div>
                            </div>

                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblFechaInicioSeguro" runat="server" Text="Fecha inicio:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFechaInicioSeguro" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    <label for="txtFechaInicioSeguro" style="height:24px; width:24px" class="input-group-addon generic_btn">
                                    </label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblFechaFinSeguro" runat="server" Text="Fecha fin:"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFechaFinSeguro" type="date" runat="server" placeholder="dd/mm/aaaa" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    <label for="txtFechaFinSeguro" style="height:24px; width:24px" class="input-group-addon generic_btn">
                                    </label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" style="text-align:right">
                                    <asp:Button ID="btnContratoAceptar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnContratoAceptar_Click"/>
                                </div>
                            </div>
                        </tr>
                    </table>
                </fieldset>

                <fieldset style="width: 100%">
                    <legend>
                        <span>
                            Asociar documentos al contrato
                        </span>
                    </legend>
                    <table style="width:100%">
                        <tr>
                            <td>
                                <div class="section group">
	                                <div class="section group">
	                                <div style="width:100%; text-align:right">
                                        <asp:Button ID="btnAgregarDocumento" runat="server" Text="Agregar Documento" CssClass="btn btn-success" OnClick="btnAgregarDocumento_Click" style="width:150px !important;" />
                                    </div>
                                </div>
                                <div class="section group"  style="margin:30px;">
                                    <%--<asp:UpdatePanel ID="upaDocumentos" runat="server">
                                        <ContentTemplate>--%>
                                            <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="false" DataKeyNames="IdDocumento"
                                                Width="100%" CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvDocumentos_RowCommand">
                                                <EmptyDataTemplate>
                                                    No se encontraron documentos para mostrar.
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="DescArchivo" HeaderText="Tipo de Archivo" />
                                                    <asp:BoundField DataField="NombreArchivo" HeaderText="Tipo de Archivo" />
                                                    <asp:BoundField DataField="Extension" HeaderText="Extensión" />
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <div style="text-align:center">
                                                                <asp:ImageButton ID="imgbtnView" runat="server" CommandName="ViewDoc" ImageUrl="~/Images/icons/view.png"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Width="24px" Height="24px" ToolTip="Mostrar Documento" />
                                                                <asp:ImageButton ID="imbDescargar" runat="server" CommandName="Descargar" Width="24px" Height="24px" ToolTip="Descargar el documento"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/Images/icons/download.png"  />
                                                                <asp:ImageButton ID="imbEliminar" runat="server" CommandName="Eliminar" Width="24" Height="24" ToolTip="Elimina el documento"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return UserDeleteConfirmation();" ImageUrl="~/Images/icons/delete.png"  />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        <%--</ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="gvDocumentos" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <div class="control-group">
                    <asp:Panel ID="pnlIntercambios" runat="server">
                        <div class="widget-box">
                            <div class="widget-title">
                                <span class="icon"><i class="icon-align-justify"></i></span>
                                <h5>Intercambios del Contrato</h5>
                            </div>
                            <div class="widget-content nopadding">
                                <br />
                                <br />
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblIntercambios" runat="server" Text="Listado de Intercambios" CssClass="lblInputTitulo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="float: right">
                                                <asp:Button ID="btnAgregaIntercambio" runat="server" Text="Agrega Intercambio" CssClass="btn btn-success btnrlr" OnClick="btnAgregaIntercambio_Click" style="width:150px !important;" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="div1" style="text-align: center;">
                                                <asp:GridView ID="gvIntercambio" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="IdIntercambio" OnRowCommand="gvIntercambio_RowCommand"
                                                    CssClass="table table-bordered table-striped table-hover" PageSize="3" style="overflow-x:auto;">
                                                    <EmptyDataTemplate>
                                                        No existen intercambios para mostrar.
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave de Contrato" />
                                                        <asp:BoundField DataField="GrupoModelo" HeaderText="Grupo de Modelo" />
                                                        <asp:BoundField DataField="Factor" HeaderText="Factor Intercambio" />
                                                        <asp:BoundField DataField="TarifaNal" HeaderText="Tarifa Nacional" DataFormatString="{0:C}" />
                                                        <asp:BoundField DataField="TarifaInt" HeaderText="Tarifa Internacional" DataFormatString="{0:C}"/>
                                                        <asp:TemplateField HeaderText="Acciones">
                                                            <ItemTemplate>
                                                                <div style="text-align: center">
                                                                    <asp:ImageButton ID="imbEditarIntercambio" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24" 
                                                                        ToolTip="Edita un intercambio." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                                    <asp:ImageButton ID="imbEliminaIntercambio" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24" 
                                                                        ToolTip="Elimina un intercambio." OnClientClick="return DeleteConfirmation();" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Button ID="btnRegresarContratos" runat="server" Text="Regresar" CssClass="btn btn-info" OnClick="btnRegresarContratos_Click" style="margin-right:7% !important;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:Panel ID="pnlAgregaintercambios" runat="server" Visible="false">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Alta de Intercambios</h5>
            </div>
            <div class="widget-content nopadding">
                <br />
                <table style="width: 100%">
                    <tr>
                        <td>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblGrupoModeloId" runat="server" Text="Grupo modelo:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlGrupoModelo" runat="server" CssClass="slcList"></asp:DropDownList>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioFactor" runat="server" Text="Factor de Intercambio:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioFactor" runat="server" CssClass=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioEspera" runat="server" Text="Factor de Espera:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioEspera" runat="server" CssClass=""></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioPernocta" runat="server" Text="Factor de Pernocta:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioPernocta" runat="server" CssClass=""></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioTarifaInter" runat="server" Text="Tarifa internacional:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioTarifaInter" runat="server" CssClass=""></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioTarifaNac" runat="server" Text="Tarifa nacional:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioTarifaNac" runat="server" CssClass=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioFerry" runat="server" Text="Ferry:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioFerry" runat="server" CssClass=""></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioAplicaFerry" runat="server" Text="Aplica Ferry:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:RadioButtonList ID="rblAplicaFerry" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Aplica" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No Aplica" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="section group">
	                            
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioGalones" runat="server" Text="Galones:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioGalones" runat="server" CssClass=""></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioValor" runat="server" Text="Valor:" Visible="false" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioValor" runat="server" Text="0" CssClass="" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                                <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                                <div class="col span_1_of_4">
                                    &nbsp;
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Button ID="btnIntercambioAceptar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnIntercambioAceptar_Click"/>
                                    <asp:Button ID="btnCancelarIntercambio" runat="server" Text="Regresar" CssClass="btn btn-info" OnClick="btnCancelarIntercambio_Click" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
    

    <%-- Modal de Matriculas --%>
    <asp:HiddenField ID="hdTargetMatricula" runat="server" />
    <cc1:modalpopupextender ID="mpeMatricula" runat="server" TargetControlID="hdTargetMatricula" 
        PopupControlID="pnlMatricula" BackgroundCssClass="overlayy">
    </cc1:modalpopupextender>
    <asp:Panel ID="pnlMatricula" runat="server" BorderColor="Black" BackColor="White" Height="450px"
        Width="450px" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <%--<asp:UpdatePanel ID="upaMatricula" runat="server">
            <ContentTemplate>--%>
             <table style="width:100%">
                <tr>
                    <td colspan="2">
                        <h4><asp:Label ID="lblTituloMatricula" runat="server" Text="Lista de matrículas"></asp:Label></h4>
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
                             <asp:GridView ID="gvMatriculas" runat="server" AutoGenerateColumns="False"  Width="100%" 
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
                <table style="width:100%">
                 <tr>
                     <td width="50%">
                        <div style="text-align:right; float:right">
                            <asp:Button ID="btnAceptarMatriculas" runat="server" Text="Aceptar" CssClass="btn btn-primary"
                                OnClick="btnAceptarMatriculas_Click"/>
                        </div>
                     </td>
                     <td width="50%">
                        <div style="text-align:left; float:left">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default"
                                OnClientClick="OcultarModalMatriculas();"/>
                        </div>
                     </td>
                 </tr>
             </table>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </asp:Panel>

    <%--Modal para cargar archivo--%>
    <asp:HiddenField ID="hdTargetArchivo" runat="server" />
    <cc1:ModalPopupExtender ID="mpeArchivo" runat="server" TargetControlID="hdTargetArchivo"
        PopupControlID="pnlArchivo" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlArchivo" runat="server" BackColor="White" Height="200"
        Width="500" HorizontalAlign="Center" Style="display: none" CssClass="modalrlr">
        <asp:UpdatePanel ID="upaArchivo" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <h4><asp:Label ID="Label3" runat="server" Text="Seleccione el archivo"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label1" runat="server" Text="Descripcion:"></asp:Label>
                        </td>
                        <td width="75%" style="text-align: left">
                                <asp:TextBox ID="txtDescripcionDoc" runat="server" Style="width:94%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label4" runat="server" Text="Archivo:"></asp:Label>
                        </td>
                        <td width="75%" style="text-align: left">
                                <asp:FileUpload ID="fuArchivo" runat="server" Width="90%" CssClass="btn btn-success" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="lblErrorArchivo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%">
                            <p>&nbsp;</p>
                            <div style="text-align: center;">
                                <asp:Button ID="btnAceptarArchivo" runat="server" Text="Subir Archivo" OnClick="btnAceptarArchivo_Click" OnClientClick="OcultarModalArchivo();" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancelarArchivo" runat="server" Text="Cancelar" OnClientClick="OcultarModalArchivo();" CssClass="btn btn-default" />&nbsp;&nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%"></td></tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAceptarArchivo" />
            </Triggers>
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
