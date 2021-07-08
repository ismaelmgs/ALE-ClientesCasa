
<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmConsultaBitacoras.aspx.cs" Inherits="ClientesCasa.Views.SCAF.frmConsultaBitacoras" %>
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
            $("[id$=txtFechaIni]").datepicker({ clearBtn: true });
        });
        $(function () {
            $("[id$=txtFechaFin]").datepicker({});
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
 

       /* $(document).ready(function () {
            $("[id$=txtMotor1]").mask("999999.99");
            $("[id$=txtMotor2]").mask("999999.99");
            //$("[id$=txtMotor1]").mask("999999.99");
            //$("[id$=txtMotor2]").mask("999999.99");
            $("[id$=txtPlaneador]").mask("999999.99");
            $("[id$=txtAPU]").mask("999999.99");
            $("[id$=txtCMotor1]").mask("999999.99");
            $("[id$=txtCMotor2]").mask("99999.99");
            $("[id$=txtAterrizajes]").mask("999999.99");
            $("[id$=txtMecanicoResp]").mask("999999.99");

        });*/


        //$(function ($) {
        //    $("[id$=txtMotor1]").mask("99999.99");
        //})
        //$(function ($) {
        //    $("[id$=txtMotor2]").mask("99999.99");
        //})
        //$(function ($) {
        //    $("[id$=txtPlaneador]").mask("99999.99");
        //})
        //$(function ($) {
        //    $("[id$=txtAPU]").mask("99999.99");
        //})
        //$(function ($) {
        //    $("[id$=txtCMotor1]").mask("99999.99");
        //})
        //$(function ($) {
        //    $("[id$=txtCMotor2]").mask("99999.99");
        //})  
        //$(function ($) {
        //    $("[id$=txtAterrizajes]").mask("99999.99");
        //})
        //$(function ($) {
        //    $("[id$=txtMecanicoResp]").mask("99999.99");
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
        function OcultarModalDiscrepancias() {
            var modalId = '<%=mpeDiscrepancia.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
        function OcultarModalComponentes() {
            var modalId = '<%=mpeComponente.ClientID%>';
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
    </script>

    <%--<script type ="text/javascript" >          
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

    </script>  --%>

    <div style="text-align: left">
        <h3>&nbsp;&nbsp;Consulta Bitácora</h3>
    </div>
    <cc1:toolkitscriptmanager ID="ToolKitScriptManager" runat="server"></cc1:toolkitscriptmanager>
    <asp:Panel ID="pnlBusqueda" runat="server">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de Bitácora</h5>
            </div>
            <div class="widget-content nopadding">

                <div class="control-group">
                    <div class="controls">
                        <br />
                        <div class="table-responsive" style="margin:5px;">
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    Folio:
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFolio" runat="server" CssClass="span11" placeholder="Folio"></asp:TextBox>
                                </div>
                                <div class="col span_2_of_4" style="text-align:center;">
                                    Rango de Fechas:
                                </div>
                            </div>
                            <div class="section group">
                                <div class="col span_1_of_4">
                                    Matrícula:
                                 </div>
                                <div class="col span_1_of_4">
                                    <asp:DropDownList ID="ddlMatricula" runat="server" placeholder="Seleccione"></asp:DropDownList>
                                 </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFechaIni" type="date" runat="server"  placeholder="Fecha de Inicio" CssClass="form-control"></asp:TextBox>
                                 </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtFechaFin" type="date" runat="server" placeholder="Fecha Fin" CssClass="form-control"></asp:TextBox>
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
                                <div class="col span_1_of_4" style="text-align:center;">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                                    <%--<asp:Button ID="btnAgregarCliente" runat="server" Text="Agregar" CssClass="btn btn-info" OnClick="btnAgregarCliente_Click" />--%>
                                </div>
                             </div>
                       </div>
                    </div>
                </div>
                
                    <table style="width: 100%">
                    <tr>
                        <td>
                            <div class="diva1">
                                <table style="width:100%"><tr><td>
                                    <asp:GridView ID="gvPiernas" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                                    DataKeyNames="IdBitacora,FolioReal,AeronaveMatricula,PilotoId, CopilotoId" AllowPaging="True" CssClass="table table-bordered table-striped table-hover"
                                    Width="100%" PageSize="10" OnPageIndexChanging="gvPiernas_PageIndexChanging" OnRowCommand="gvPiernas_RowCommand"
                                    OnSelectedIndexChanged="gvPiernas_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="FolioReal" HeaderText=" Folio " />
                                        <asp:BoundField DataField="AeronaveMatricula" HeaderText=" Matrícula " />
                                        <asp:BoundField DataField="Origen" HeaderText=" Origen " />
                                        <asp:BoundField DataField="Destino" HeaderText=" Destino " ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="LegId" HeaderText=" Leg Id " ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="TripNum" HeaderText=" Trip " ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:ImageButton ID="imbEditarBitacora" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24"
                                                        ToolTip="Edita bitácora." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    <%--<asp:ImageButton ID="imbEliminarContrato" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24"
                                                        ToolTip="Elimina un contrato." OnClientClick="return DeleteConfirmation();" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron registros para mostrar
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                </td></tr></table>
                            </div>
                        </td>
                    </tr>
                </table>
                
                <br />
                <br />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlAltaLeg" runat="server" Visible="false">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Datos de la Bitácora</h5>
            </div>
            <div class="widget-content nopadding">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <h5>Información Principal</h5>
                            <div class="section group">
	                            <div class="col span_1_of_4">
	                            <strong><asp:Label ID="lblFolio" runat="server" Text="Folio:" CssClass=""></asp:Label> <%--<asp:Label ID="lblReqClaveCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblFolioResp" runat="server" CssClass="" AutoPostBack="true" MaxLength="5" placeholder="Folio de la Bitácora"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4">
	                            <strong><asp:Label ID="lblMatricula2" runat="server" Text="Matrícula:" CssClass=""><%--</asp:Label> <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblMatriculaResp" runat="server" CssClass="" MaxLength="60" placeholder="Matrícula de la Bitácora"></asp:Label>
                         	    </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <strong><asp:Label ID="lblSerie" runat="server" Text="No. Serie:" ></asp:Label></strong>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblSerieResp" runat="server" CssClass="izquierdarlr" MaxLength="75" placeholder="Serie de Aeronave"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <strong><strong><asp:Label ID="lblPIC" runat="server" Text="PIC:" ></asp:Label></strong></strong>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblPICResp" runat="server" CssClass="izquierdarlr" MaxLength="75" placeholder="PIC"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <strong><asp:Label ID="lblSIC" runat="server" Text="SIC: " CssClass=""></asp:Label></strong>
                                </div>
                                 <div class="col span_1_of_4">
                                    <asp:Label ID="lblSICREsp" runat="server" CssClass="izquierdarlr" MaxLength="75" placeholder="SIC"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">&nbsp;</div>
                                <div class="col span_1_of_4">&nbsp;</div>
                            </div>
                        </td>
                    </tr>
                </table>
                       
            </div>
            <div class="widget-content nopadding">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <h5>Información Técnica</h5>
                            <div class="col span_1_of_4">
	                        <asp:Label ID="lblTiempos" runat="server" Text="Tiempos" CssClass=""></asp:Label>
	                        </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
	                            <asp:Label ID="lblMotor1" runat="server" Text="Motor I:" CssClass=""></asp:Label> <asp:Label ID="lblReqMotorI" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">   
	                            <asp:TextBox ID="txtMotor1" runat="server" CssClass="" MaxLength="9" placeholder="######.##" pattern="[0-9]{3,6}([.][0-9]{0,2})"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbMotor1" runat="server" TargetControlID="txtMotor1" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
	                            </div>
	                            <div class="col span_1_of_4">
	                            <asp:Label ID="lblMotor2" runat="server" Text="Motor II:" CssClass=""></asp:Label> <asp:Label ID="lblReqMotorII" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtMotor2" runat="server" CssClass="" MaxLength="9" placeholder="######.##" pattern="[0-9]{3,6}([.][0-9]{0,2})"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbMotor2" runat="server" TargetControlID="txtMotor2" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                         	    </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqMotorIAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqMotorIIAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblPlaneador" runat="server" Text="Planeador:" CssClass=""></asp:Label><asp:Label ID="lblReqPlaneador" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtPlaneador" runat="server" CssClass="izquierdarlr" MaxLength="9" placeholder="######.##" pattern="[0-9]{3,6}([.][0-9]{0,2})"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbPlaneador" runat="server" TargetControlID="txtPlaneador" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                </div>
                                <div class="col span_1_of_4">
	                            <asp:Label ID="lblAPU" runat="server" Text="APU:" CssClass=""></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtAPU" runat="server" CssClass="" MaxLength="9" placeholder="######.##" pattern="[0-9]{3,6}([.][0-9]{0,2})"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbAPU" runat="server" TargetControlID="txtAPU" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                         	    </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqPlaneadorAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
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
                            <div class="col span_1_of_4">
	                        <strong><asp:Label ID="lblCiclos" runat="server" Text="Ciclos" CssClass=""></asp:Label></strong>
	                        </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
	                            <asp:Label ID="lblCMotor1" runat="server" Text="Motor I:" CssClass=""></asp:Label> <asp:Label ID="lblReqCMotorI" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtCMotor1" runat="server" CssClass="" MaxLength="9" placeholder="#########" ></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbCMotor1" runat="server" TargetControlID="txtCMotor1" ValidChars="1234567890" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
	                            </div>
	                            <div class="col span_1_of_4">
	                            <asp:Label ID="lblCMotor2" runat="server" Text="Motor II:" CssClass=""></asp:Label> <asp:Label ID="lblReqCMotorII" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtCMotor2" runat="server" CssClass="" MaxLength="9" placeholder="#########" ></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbCMotor2" runat="server" TargetControlID="txtCMotor2" ValidChars="1234567890" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                         	    </div>
                            </div>
                                <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqCMotorIAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqCMotorIIAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                           <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblAterrizajes" runat="server" Text="Aterrizajes:" ></asp:Label> <asp:Label ID="lblReqAterrizajes" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtAterrizajes" runat="server" CssClass="izquierdarlr" MaxLength="9" placeholder="#########" ></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftbAterrizajes" runat="server" TargetControlID="txtAterrizajes" ValidChars="1234567890" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                </div>
                                <div class="col span_1_of_4">&nbsp;</div>
                                <div class="col span_1_of_4">&nbsp;</div>
                            </div>
                                <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqAterrizajesAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
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
                                <strong><h5><asp:Label ID="lblMantto" runat="server" Text="Información de taller de mantenimiento" CssClass=""></asp:Label></strong></h5>
                             <div class="col span_1_of_4">
	                        </div>
                            <div class="section group">
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblMecanico" runat="server" Text="Mecánico:" ></asp:Label> <asp:Label ID="lblReqMecanico" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
	                             <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtMecanicoResp" runat="server" CssClass="izquierdarlr" MaxLength="9" placeholder="Mecánico"></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">&nbsp;</div>
                                <div class="col span_1_of_4" style="text-align:center;"><asp:Button ID="btnGuardarCliente" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarCliente_Click" /></div>
                            </div>
                                </div>
                                <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqMecanicoAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    </table>

                    <table style="width:100%;text-align:center;">
                    <tr>
                        <td>
                            <table style="width:100%">
                                    <td>
                                        <div class="widget-box">
                                            <div class="widget-title">
                                                <span class="icon"><i class="icon-align-justify"></i></span>
                                                <h5>Listado de Piernas</h5>
                                            </div>
                                            <div id="diva1" style="padding-top:15px;">
                                                <table align="center" style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <div class="controls">
                                                                <asp:GridView ID="gvLegs" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="IdBitacora,FolioReal" AllowPaging="True" Width="100%"
                                                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvLegs_RowCommand">
                                                                    <EmptyDataTemplate>
                                                                        No existen discrepancias para mostrar.
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:BoundField DataField="LegId" HeaderText="Leg Id" />
                                                                        <asp:BoundField DataField="Origen" HeaderText="Origen" />
                                                                        <asp:BoundField DataField="Destino" HeaderText="Destino" />
                                                                        <asp:BoundField DataField="OrigenVuelo" HeaderText="Out" />
                                                                        <asp:BoundField DataField="DestinoVuelo" HeaderText="In" />
                                                                        <asp:BoundField DataField="ConsumoOri" HeaderText="Combustible Out" />
                                                                        <asp:BoundField DataField="ConsumoDes" HeaderText="Combustible In" />
                                                                        <asp:BoundField DataField="ConsumoUsed" HeaderText="Combustible Used" />
                                                                        <asp:BoundField DataField="Discrepancias" HeaderText="Discrepancias" />
                                                                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <div style="text-align: center">
                                                                                    <asp:ImageButton ID="imbAgregarDiscrepancias" runat="server" ImageUrl="~/Images/icons/add2.png" Height="24" Width="24"
                                                                                        ToolTip="Agregar Discrepancias." CommandName="Agregar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                    <%--<asp:ImageButton ID="imbEliminarContrato" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24"
                                                                                        ToolTip="Elimina un contrato." OnClientClick="return DeleteConfirmation();" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="text-align:right;padding-right:5%;">
                                            <asp:Button ID="btnRegresar2" runat="server" Text="Regresar" CssClass="btn btn-info" OnClick="btnRegresar2_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
      
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlAgregarDiscrepancias" runat="server" Visible="false">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Datos de la Pierna</h5>
            </div>
            <div class="widget-content nopadding">
                <table style="width: 90%">
                    <tr>
                        <td>
                            <div class="section group" style="width: 100%;">
	                            <div class="col span_1_of_8">
	                            <strong><asp:Label ID="lblFolioDis" runat="server" Text="Folio:" CssClass=""></asp:Label> <%--<asp:Label ID="lblReqClaveCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_8" style="text-align:left">
	                            <asp:Label ID="lblFolioDisRes" runat="server" CssClass="" AutoPostBack="true" MaxLength="5" placeholder="Folio de la Bitácora"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_8">
	                            <strong><asp:Label ID="lblOrigenDis" runat="server" Text="Origen:" CssClass=""><%--</asp:Label> <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_8" style="text-align:left">
	                            <asp:Label ID="lblOrigenDisRes" runat="server" CssClass="" MaxLength="60" placeholder="Origen"></asp:Label>
                         	    </div>
                                <div class="col span_1_of_8">
	                            <strong><asp:Label ID="lblInDis" runat="server" Text="In:" CssClass=""><%--</asp:Label> <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_8" style="text-align:left">
	                            <asp:Label ID="lblInDisRes" runat="server" CssClass="" MaxLength="60" placeholder="Origen"></asp:Label>
                         	    </div>
                                <div class="col span_1_of_8">
	                            <strong><asp:Label ID="lblCombInDis" runat="server" Text="Comb In:" CssClass=""><%--</asp:Label> <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_8" style="text-align:left">
	                            <asp:Label ID="lblCombInDisRes" runat="server" CssClass="" MaxLength="60" placeholder="Origen"></asp:Label>
                         	    </div>
                            </div>
                            <div class="section group" style="width: 100%;">
	                            <div class="col span_1_of_8">
                                    <strong><asp:Label ID="lblLegIdDis" runat="server" Text="Leg Id:" ></asp:Label></strong>
                                </div>
                                <div class="col span_1_of_8">
                                    <asp:Label ID="lblLegIdDisRes" runat="server" CssClass="izquierdarlr" MaxLength="75" placeholder="Serie de Aeronave"></asp:Label>
                                </div>
                                <div class="col span_1_of_8">
                                    <strong><asp:Label ID="lblDestinoDis" runat="server" Text="Destino:" ></asp:Label></strong>
                                </div>
                                <div class="col span_1_of_8">
                                    <asp:Label ID="lblDestinoDisRes" runat="server" CssClass="izquierdarlr" MaxLength="75" placeholder="PIC"></asp:Label>
                                </div>
                                <div class="col span_1_of_8">
                                    <strong><asp:Label ID="lblOutDis" runat="server" Text="Out:" ></asp:Label></strong>
                                </div>
                                <div class="col span_1_of_8">
                                    <asp:Label ID="lblOutDisRes" runat="server" CssClass="" MaxLength="75" placeholder="PIC"></asp:Label>
                                </div>
                                <div class="col span_1_of_8">
                                    <strong><asp:Label ID="lblCombOutDis" runat="server" Text="Comb Out:" ></asp:Label></strong>
                                </div>
                                <div class="col span_1_of_8">
                                    <asp:Label ID="lblCombOutDisRes" runat="server" CssClass="izquierdarlr" MaxLength="75" placeholder="PIC"></asp:Label>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                       
            </div>
            <div class="widget-content nopadding">
                    <table style="width:100%;text-align:center;">
                    <tr>
                        <td>
                            <table style="width:100%;text-align:center;">
                                    <td>
                                        <div class="section group" style="width:100%;">
                                        <div class="col span_3_of_4">&nbsp;</div>
                                            <div class="col span_1_of_4"><asp:Button ID="btnAgregarDis" runat="server" Text="Agregar Discrepancia" CssClass="btn btn-success" OnClick="btnAgregarDis_Click" style="width: 60% !important;" /></div>
                                        </div>
                                            <div class="widget-box">                                      
                                            <div class="widget-title">
                                                <span class="icon"><i class="icon-align-justify"></i></span>
                                                <h5>Listado de Discrepancias Asociadas</h5>
                                            </div>
                                            <div id="diva1" style="padding-top:15px;">
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <div class="controls">
                                                                <asp:GridView ID="gvDiscrepancias" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="IdDiscrepancia,IdBitacora" AllowPaging="True" Width="100%"
                                                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvDiscrepancias_RowCommand">
                                                                    <EmptyDataTemplate>
                                                                        No existen discrepancias para mostrar.
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:BoundField DataField="IdDiscrepancia" HeaderText="Folio" />
                                                                        <asp:BoundField DataField="TipoReporte" HeaderText="Tipo Reporte" />
                                                                        <asp:BoundField DataField="Base" HeaderText="Base" />
                                                                        <asp:BoundField DataField="FechaDiscrepancia" HeaderText="Fecha Discrepancia" />
                                                                        <asp:BoundField DataField="ATA" HeaderText="Codigo Ata"/>
                                                                        <asp:BoundField DataField="NumComponentes" HeaderText="Componentes"/>
                                                                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <div style="text-align: center">
                                                                                    <asp:ImageButton ID="imEditarDiscrepancias" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24"
                                                                                        ToolTip="Editar Discrepancia." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                    <asp:ImageButton ID="imbEliminarDiscrepancias" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24"
                                                                                        ToolTip="Eliminar Discrepancia." OnClientClick="return DeleteConfirmation();" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                    <asp:ImageButton ID="imbAgregarComponente" runat="server" ImageUrl="~/Images/icons/attachmen.png" Height="24" Width="24"
                                                                                        ToolTip="Ver Componentes." CommandName="Componente" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="text-align:right;padding-right:5%;">
                                            <asp:Button ID="btnRegresar3" runat="server" Text="Regresar" CssClass="btn btn-info" OnClick="btnRegresar3_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
      
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlAgregarComponente" runat="server" Visible="false">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Datos de la Discrepancia</h5>
            </div>
            <div class="widget-content nopadding">
                <table style="width: 90%">
                    <tr>
                        <td>
                            <div class="section group" style="width: 100%;">
	                            <div class="col span_1_of_8">
	                            <strong><asp:Label ID="lblTipoReporte" runat="server" Text="Tipo de Reporte:" CssClass=""></asp:Label> <%--<asp:Label ID="lblReqClaveCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_8" style="text-align:left">
	                            <asp:Label ID="lblTipoReporteRes" runat="server" CssClass="" MaxLength="50" placeholder="Tipo de Reporte"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_8">
	                            <strong><asp:Label ID="lblCodigoATADis" runat="server" Text="Codigo ATA:" CssClass=""><%--</asp:Label> <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_8" style="text-align:left">
	                            <asp:Label ID="lblCodigoATARes" runat="server" CssClass="" MaxLength="60" placeholder="Codigo ATA"></asp:Label>
                         	    </div>
                                <div class="col span_1_of_8">
	                            <strong><asp:Label ID="lblFechaDiscrepancia" runat="server" Text="Fecha Discrepancia:" CssClass=""><%--</asp:Label> <asp:Label ID="lblReqNombreCliente" runat="server" Text="*" ForeColor="Red">--%></asp:Label></strong>
	                            </div>
	                            <div class="col span_1_of_8" style="text-align:left">
	                            <asp:Label ID="lblFechaDiscrepanciaRes" runat="server" CssClass="" MaxLength="60" placeholder="Fecha Discrepancia"></asp:Label>
                         	    </div>
                            </div>
                        </td>
                    </tr>
                </table>
                       
            </div>
            <div class="widget-content nopadding">
                    <table style="width:100%;text-align:center;">
                    <tr>
                        <td>
                            <table style="width:100%;text-align:center;">
                                    <td>
                                        <div class="section group" style="width:100%;">
                                        <div class="col span_3_of_4">&nbsp;</div>
                                            <div class="col span_1_of_4"><asp:Button ID="btnAgregarComponente" runat="server" Text="Agregar Componente" CssClass="btn btn-success" OnClick="btnAgregarComponente_Click" style="width:150px;" /></div>
                                        </div>
                                            <div class="widget-box">                                      
                                            <div class="widget-title">
                                                <span class="icon"><i class="icon-align-justify"></i></span>
                                                <h5>Listado de Componentes Asociados</h5>
                                            </div>
                                            <div id="diva1">
                                                <br />
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <div class="controls">
                                                                <asp:GridView ID="gvComponentes" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="IdComponente,IdDiscrepancia" AllowPaging="True" Width="100%"
                                                                    CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvComponentes_RowCommand">
                                                                    <EmptyDataTemplate>
                                                                        No existen componentes para mostrar.
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:BoundField DataField="IdComponente" HeaderText="Folio" />
                                                                        <asp:BoundField DataField="NombreComponente" HeaderText="Tipo Reporte" />
                                                                        <asp:BoundField DataField="Descripcion" HeaderText="Base" />
                                                                        <asp:BoundField DataField="NoParteRemovido" HeaderText="No Parte Removido" />
                                                                        <asp:BoundField DataField="NoSerieRemovido" HeaderText="No Serie Removido" />
                                                                        <asp:BoundField DataField="NoParteInstalado" HeaderText="No Parte Instalado" />
                                                                        <asp:BoundField DataField="NoSerieInstalado" HeaderText="No Serie Instalado" />
                                                                        <asp:BoundField DataField="PosicionComponente" HeaderText="Posicion Componente" />
                                                                        <asp:BoundField DataField="Id" HeaderText="Id" />
                                                                        <asp:BoundField DataField="RazonServicio" HeaderText="Razón Servicio" />
                                                                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <div style="text-align: center">
                                                                                    <asp:ImageButton ID="imEditarComponente" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24"
                                                                                        ToolTip="Editar Componente." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                    <asp:ImageButton ID="imbEliminarComponente" runat="server" ImageUrl="~/Images/icons/delete.png" Height="24" Width="24"
                                                                                        ToolTip="Eliminar Componente." OnClientClick="return DeleteConfirmation();" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="text-align:right;padding-right:5%;">
                                            <asp:Button ID="btnRegresarComp" runat="server" Text="Regresar" CssClass="btn btn-info" OnClick="btnRegresarComp_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
      
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
                                    <asp:Label ID="lblIntercambioFactor" runat="server" Text="Factor:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioFactor" runat="server" CssClass=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioEspera" runat="server" Text="Espera:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioEspera" runat="server" CssClass=""></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioPernocta" runat="server" Text="Pernocta:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioPernocta" runat="server" CssClass=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioValor" runat="server" Text="Valor:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioValor" runat="server" CssClass=""></asp:TextBox>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:Label ID="lblIntercambioGalones" runat="server" Text="Galones:" CssClass=""></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                    <asp:TextBox ID="txtIntercambioGalones" runat="server" CssClass=""></asp:TextBox>
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
    <asp:HiddenField ID="hdTargetDiscrepancia" runat="server" />
    <cc1:modalpopupextender ID="mpeDiscrepancia" runat="server" TargetControlID="hdTargetDiscrepancia" 
        PopupControlID="pnlDiscrepancia" BackgroundCssClass="overlayy">
    </cc1:modalpopupextender>
    <asp:Panel ID="pnlDiscrepancia" runat="server" BackColor="White" Width="85%"
        HorizontalAlign="Center" Style="display: none; margin-top:-100px; height:550px !important; overflow: auto;left: 98px !important; width:85% !iportant;" CssClass="modalrlr">
        <%--<asp:UpdatePanel ID="upaMatricula" runat="server">
            <ContentTemplate>--%>
             <table style="width:100%">
                <tr>
                    <td>
                        <h4><asp:Label ID="lblTituloDiscrepancia" runat="server" Text="Agregar Discrepancia"></asp:Label></h4>
                    </td>
                </tr>
                <tr>
                     <td>
                         <asp:Panel ID="pnlDiscrepancias" runat="server" Width="100%" ScrollBars="Auto">
                                <table style="width: 100%">
                    <tr>
                        <td>
                            <h5>Datos de la Discrepancia</h5>
                            <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblOrigenDisAdd" runat="server" Text="Origen:" CssClass=""></asp:Label> <asp:Label ID="lblReqOrigenDis" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:DropDownList id="ddlOrigen" runat="server" >
                                    <asp:ListItem Value ="0">Seleccione un Origen</asp:ListItem>
                                    <asp:ListItem Value ="1" Selected="True">Durante Operación</asp:ListItem>
                                    <asp:ListItem Value ="2">Durante Mantenimiento</asp:ListItem>
	                            </asp:DropDownList>
                                
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblTipoRep" runat="server" Text="Tipo Reporte:" ></asp:Label><asp:Label ID="lblReqTipoRepDis" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
                                <div class="col span_1_of_4" style="text-align:left"><asp:DropDownList id="ddlTipoRep" runat="server" >
                                    <asp:ListItem Value ="0" Selected="True">Seleccione un Tipo de Reporte</asp:ListItem>
                                    <asp:ListItem Value ="1">PIREP</asp:ListItem>
                                    <asp:ListItem Value ="2">MAREP</asp:ListItem>
	                            </asp:DropDownList></div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqOrigenDisAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqTipoRepDisAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
                                <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" CssClass=""></asp:Label> <asp:Label ID="lblReqDisDescripcion" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="" TextMode="MultiLine" Rows="2" MaxLength="50" placeholder="Descripcion"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtAPU" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                         	    </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblAccionCorrectiva" runat="server" Text="Acción Correctiva:" CssClass=""></asp:Label> <asp:Label ID="lblReqAccionCorrectiva" runat="server" Text="*" ForeColor="Red" ></asp:Label>
                                </div>
                               <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtAccionCorrectiva" runat="server" CssClass="" TextMode="MultiLine" Rows="2" MaxLength="100" placeholder="Acción Correctiva"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCMotor1" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>                           
	                            </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqAccionCorrectivaAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqDisDescripcionAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:Label ID="lblBase" runat="server" Text="Base:" ></asp:Label> <asp:Label ID="lblReqDisBase" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtBase" runat="server" CssClass="" MaxLength="50" placeholder="Base"></asp:TextBox>
                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtAterrizajes" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblCodigoAta" runat="server" Text="Codigo ATA:" CssClass=""></asp:Label> <asp:Label ID="lblReqCodigoAta" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtCodigoAta" runat="server" CssClass="" MaxLength="100" placeholder="Codigo ATA"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCMotor2" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                         	    </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqDisBaseAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqCodigoAtaAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                           <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:Label ID="lblMecanicoDis" runat="server" Text="Mecánico:" CssClass=""></asp:Label> <asp:Label ID="lblReqMecanicoDis" runat="server" Text="*" ForeColor="Red" ></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtMecanicoDis" runat="server" CssClass="" MaxLength="100" placeholder="Mecanico"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCMotor2" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                         	    </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblReferenciaRep" runat="server" Text="Referencia de Rep:" ></asp:Label> <asp:Label ID="lblReqReferenciaRep" runat="server" Text="*" ForeColor="Red" ></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <asp:TextBox ID="txtReferenciaRep" runat="server" CssClass="" MaxLength="50" placeholder="Referencia de Rep"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtAterrizajes" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                                </div>                            
                           </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqMecanicoDisAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqReferenciaRepAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblFechaDis" runat="server" Text="Fecha Discrepancia:" ></asp:Label> <asp:Label ID="lblReqFechaDis" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <asp:TextBox ID="txtFechaDis" type="date" runat="server" placeholder="Fecha Discrepancia" CssClass="form-control" ></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="ftbFechaDis" runat="server" TargetControlID="txtFechaDis" ValidChars="1234567890/" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                                </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblFechaAtencion" runat="server" Text="Fecha Atención:" CssClass=""></asp:Label> <asp:Label ID="lblReqFechaAtencion" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtFechaAtencion" type="date" runat="server" placeholder="Fecha Atención" CssClass="form-control"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="ftbFechaAtencion" runat="server" TargetControlID="txtFechaAtencion" ValidChars="1234567890/" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                         	    </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqFechaDisAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqFechaAtencionAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
                                 <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblId" runat="server" Text="Folio Discrepancia:" Visible="true" style="font-weight:bold;"></asp:Label> <asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblIdDisc" runat="server" CssClass="" Visible="true" style="font-weight:bold; margin-left:25%;"></asp:Label>
                                    <%--<cc1:FilteredTextBoxExtender ID="ftbId" runat="server" TargetControlID="txtId" ValidChars="1234567890" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                                </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblDiagnostico" runat="server" Text="Diagnóstico:" CssClass=""></asp:Label> <asp:Label ID="lblReqDiagnostico" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:DropDownList id="ddlDiagnostico" runat="server" >
                                    <asp:ListItem Value ="0" Selected="True">Seleccione un diagnóstico</asp:ListItem>
                                    <asp:ListItem Value ="1">Verificada</asp:ListItem>
                                    <asp:ListItem Value ="2">Descartada</asp:ListItem>
	                            </asp:DropDownList>
	                            </div>
                            </div>
                             <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqDiagnosticoAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group" style="display:none;">
	                            <div class="col span_1_of_4" style="text-align:left">
                                    
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    
                                </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblComponente" runat="server" Text="Componente:" CssClass=""></asp:Label> <asp:Label ID="Label13" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Button ID="btnComponente" runat="server" Text="Componente" CssClass="btn btn-primary"/>
                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCMotor2" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                         	    </div>
                            </div>
                            <table style="width:100%">
                                <tr>
                                     <td colspan="2">
                                         <asp:Label ID="lblErrorMat" runat="server" Text="" CssClass="validar"></asp:Label>
                                     </td>
                                </tr>
                             <tr>
                                 <td width="50%">
                                    <div style="text-align:right; float:right">
                                        <asp:Button ID="btnGuardarDiscrepancia" runat="server" Text="Guardar" CssClass="btn btn-primary"
                                            OnClick="btnGuardarDiscrepancia_Click"/>
                                    </div>
                                 </td>
                                 <td width="50%">
                                    <div style="text-align:left; float:left">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default"
                                            OnClientClick="OcultarModalDiscrepancias();"/>
                                    </div>
                                 </td>
                             </tr>  
                            </table>
                            </td>
                        </tr>
                    </table>
                        </asp:Panel>
                     </td>
                 </tr>
                 
                </table>
                
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </asp:Panel>
    <asp:HiddenField ID="hdTargetComponente" runat="server" />
    <cc1:modalpopupextender ID="mpeComponente" runat="server" TargetControlID="hdTargetComponente" 
        PopupControlID="pnlComponente" BackgroundCssClass="overlayy">
    </cc1:modalpopupextender>
    <asp:Panel ID="pnlComponente" runat="server" BackColor="White" Width="85%"
        HorizontalAlign="Center" Style="display: none; margin-top:-100px; height:550px !important; overflow: auto;left: 98px !important; width:85% !iportant;" CssClass="modalrlr">
        <%--<asp:UpdatePanel ID="upaMatricula" runat="server">
            <ContentTemplate>--%>
             <table style="width:100%">
                <tr>
                    <td>
                        <h4><asp:Label ID="lblTituloComponente" runat="server" Text="Agregar Componente"></asp:Label></h4>
                    </td>
                </tr>
                <tr>
                     <td>
                         <asp:Panel ID="pnlComponentes" runat="server" Width="100%" ScrollBars="Auto">
                                <table style="width: 100%">
                    <tr>
                        <td>
                            <h5>Datos del Componente</h5>
                            <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:Label ID="lblNombreComponente" runat="server" Text="Nombre del Componente:" CssClass=""></asp:Label> <asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtNombreComponente" runat="server" CssClass="" MaxLength="50" placeholder="Nombre del Componente"></asp:TextBox>              
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblIDComp" runat="server" Text="Folio Componente:" style="font-weight:bold;"></asp:Label> <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                    <asp:Label ID="lblDescripcionComp" runat="server" Text="Descripción:" Visible="false"></asp:Label><asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
	                            </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <asp:label ID="lblIDCompRes" runat="server" CssClass="" style="font-weight:bold;margin-left:25%;"></asp:label>
                                    <asp:TextBox ID="txtDescripcionComp" runat="server" CssClass="" MaxLength="100" placeholder="Descripción" Visible="false"></asp:TextBox>              
                                </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqNombreComponenteAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqDescripcionCompAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblNoParteRemovido" runat="server" Text="Número de Parte Removido:" CssClass=""></asp:Label> <asp:Label ID="Label20" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                               <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtNoParteRemovido" runat="server" CssClass="" MaxLength="50" placeholder="Número de Parte Removido"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCMotor1" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>                           
	                            </div>
                                 <div class="col span_1_of_4" style="text-align:left">
	                                <asp:Label ID="lblNoParteInstalado" runat="server" Text="Número de Parte Instalado:" ></asp:Label> <asp:Label ID="Label24" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtNoParteInstalado" runat="server" CssClass="" MaxLength="50" placeholder="Número de Parte Instalado"></asp:TextBox>
                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtAterrizajes" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
	                            </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqNoParteRemovidoAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqNoParteInstaladoAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:Label ID="lblNoSerieRemovido" runat="server" Text="Número de Serie Removido:" CssClass=""></asp:Label> <asp:Label ID="Label22" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtNoSerieRemovido" runat="server" CssClass="" MaxLength="50" placeholder="Número de Serie Removido"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtAPU" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                         	    </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:Label ID="lblNoSerieInstalado" runat="server" Text="Número de Serie Instalado:" CssClass=""></asp:Label> <asp:Label ID="Label26" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtNoSerieInstalado" runat="server" CssClass="" MaxLength="100" placeholder="Número de Serie Instalado"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCMotor2" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                         	    </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqNoSerieRemovidoAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqNoSerieInstaladoAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                           <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
                                    <asp:Label ID="lblPosicionComponente" runat="server" Text="Posición del Componente:" ></asp:Label> <asp:Label ID="Label28" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <asp:TextBox ID="txtPosicionComponente" runat="server" placeholder="Posición del Componente" CssClass="form-control" ></asp:TextBox>
                                </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:Label ID="lblRazonServicio" runat="server" Text="Razón de Servicio:" CssClass=""></asp:Label> <asp:Label ID="Label30" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtRazonServicio" runat="server" placeholder="Razón de Servicio" CssClass="form-control"></asp:TextBox>
                         	    </div>
                            </div>
                            <div class="section group" style="margin-left:-30px;">
                                <div class="col span_1_of_4">
                                </div>
	                            <div class="col span_1_of_4">
                                    <asp:Label ID="lblReqPosicionComponenteAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                                <div class="col span_1_of_4">
                                </div>
                                <div class="col span_1_of_4" >
                                    <asp:Label ID="lblReqRazonServicioAvi" runat="server" Text="El campo es requerido" Font-Italic="true" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="section group">
	                            <div class="col span_1_of_4" style="text-align:left">
                                    <%--<asp:Label ID="lblIDComp" runat="server" Text="Folio Componente:"></asp:Label> <asp:Label ID="Label32" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <%--<asp:label ID="lblIDCompRes" runat="server" CssClass=""></asp:label>--%>
                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtAterrizajes" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    
                                </div>
                            </div>
                            <table style="width:100%">
                                <tr>
                                     <td colspan="2">
                                         <asp:Label ID="Label41" runat="server" Text="" CssClass="validar"></asp:Label>
                                     </td>
                                </tr>
                             <tr>
                                 <td width="50%">
                                    <div style="text-align:right; float:right">
                                        <asp:Button ID="btnGuardarComponente" runat="server" Text="Guardar" CssClass="btn btn-primary"
                                            OnClick="btnGuardarComponente_Click"/>
                                    </div>
                                 </td>
                                 <td width="50%">
                                    <div style="text-align:left; float:left">
                                        <asp:Button ID="btnCancelarComp" runat="server" Text="Cancelar" CssClass="btn btn-default"
                                            OnClientClick="OcultarModalComponentes();"/>
                                    </div>
                                 </td>
                             </tr>  
                            </table>
                            </td>
                        </tr>
                    </table>
                        </asp:Panel>
                     </td>
                 </tr>
                 
                </table>
                
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
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
