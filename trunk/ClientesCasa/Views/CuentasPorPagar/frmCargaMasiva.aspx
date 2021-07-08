<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeBehind="frmCargaMasiva.aspx.cs" Inherits="ClientesCasa.Views.CuentasPorPagar.frmCargaMasiva" 
    UICulture="es" Culture="es-MX" EnableViewState="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>

    <style>
        .btn2 {
            width: auto !important;
            background-image: none;
            border-radius: 5px !important;
            box-shadow: none;
            min-height: 30px;
            -webkit-appearance: button;
            margin: 2px;
        }

        .tableCus th, .tableCus td {
            /* padding: 8px; */
            /* line-height: 20px; */
            /* text-align: left; */
            /* vertical-align: top; */
            /* border-top: 1px solid #ddd; */
        }
        td {
            padding: 1px 0px 0px 5px !important;
            vertical-align: top !important; 
        }
        textarea, .uneditable-input {
             width: 200px !important;
        }

        .columnFalse {
            visibility:hidden;
        }
    </style>


    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
<%--    <asp:UpdatePanel ID="upaPrincipal" runat="server">
     <ContentTemplate>--%>

     
    

    <asp:Panel ID="pnlBusqueda" runat="server">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Carga de Layout</h5>
            </div>
            <div class="widget-content nopadding">

                <div class="control-group">
                    <div class="controls">

                        <asp:Panel ID="pnlSeleccionGral" runat="server" style="padding-left:20px;">
                            <div class="col-md-12" align="left">
                                &nbsp; <b style="font-size:10pt;">Selecciona Proceso</b>
                            </div>
                            <div class="col-md-12" align="left">
                                <asp:RadioButtonList ID="rdnSeleccionGral" runat="server" AutoPostBack="true" 
                                    OnSelectedIndexChanged="rdnSeleccionGral_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="Simulación"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Carga Masiva"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlSeleccionFormato" runat="server" Visible="false" style="padding-left:20px;">
                            <div class="col-md-12" align="left">
                                &nbsp; <b style="font-size:10pt;">Selecciona Formato a procesar</b>
                            </div>

                            <div class="col-md-12" align="left">
                                <asp:RadioButtonList ID="rdnSeleccionFormato" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="rdnSeleccionFormato_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="Layout Genérico"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="OMA"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="AMAIT"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="ASA" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="ASUR" Enabled="true"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div> 
                            <br />
                            <div class="col-md-12">

                                <table width="100%" class="tableCus" border="0">
                                    <tr>
                                        <td width="20%" align="left">
                                            <asp:Label ID="lblBase" runat="server" Text="Base: " Visible="false" Font-Size="11pt"></asp:Label>
                                        </td>
                                        <td width="80%">
                                            <asp:DropDownList ID="ddlBase" runat="server" Visible="false">
                                                <asp:ListItem Value="0" Text=".:Seleccione:."></asp:ListItem>
                                                <asp:ListItem Value="1" Text="TLC"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MTY"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" align="left">
                                            <asp:Label ID="lblFechaContable" runat="server" Text="Fecha de Contabilización: " Visible="false" Font-Size="11pt"></asp:Label>
                                        </td>
                                        <td width="80%">
                                            <asp:TextBox ID="txtFechaContable" type="date" runat="server" CssClass="form-control" Visible="false" Placeholder="Fecha Contable"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" align="left" valign="top">
                                            <asp:Label ID="lblComentarios" runat="server" Text="Comentarios: " Visible="false" Font-Size="11pt"></asp:Label>
                                        </td>
                                        <td width="80%">
                                            <asp:TextBox ID="txtComentarios" TextMode="MultiLine" Rows="5" Width="310px" runat="server" CssClass="form-control" 
                                    Visible="false" Placeholder="Ingresar Comentarios"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" align="left">
                                            <asp:Label ID="lblAeropuerto" runat="server" Text="Aeropuerto IATA: " Visible="false" Font-Size="11pt"></asp:Label>
                                        </td>
                                        <td width="80%">
                                            <asp:TextBox ID="txtIATA" runat="server" CssClass="form-control" Visible="false" Placeholder="IATA" MaxLength="3" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                
                            </div>    
                        </asp:Panel>
                        <asp:Panel ID="pnlCargarArchivo" runat="server" Visible="false">
                            <div class="" align="center" style="padding-top:10px;">
                                <div style="width:100%; position:absolute; text-align:left; margin-left: 2.5%;">
                                    <asp:Label ID="lblDescarga" runat="server" Text="Click derecho sobre el link y Selecciona 'Guardar enlace como...'" Visible="false" Font-Size="9pt"></asp:Label><br />
                                    <asp:HyperLink ID="hylPlantilla" runat="server" Visible="false" NavigateUrl="~/Views/CuentasPorPagar/PLANTILLA_GENERAL_FINAL.xls" 
                                        ForeColor="#0066ff" Target="_blank" ToolTip="Click derecho y selecciona 'Guardar enlace como'.">Descargar Plantilla de Layout Genérico</asp:HyperLink>
                                </div>
                                <br />
                                
                                <asp:Label ID="lblProceso" runat="server" Text="" Font-Size="14pt" Font-Bold="true"></asp:Label>
                                <br />
                                
                                <div class="col-md-12">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblSelect" runat="server" Text="" Font-Size="10pt" Font-Bold="true" Visible="false"></asp:Label>
                                        <asp:FileUpload ID="fluArchivo" runat="server" CssClass="btn btn-success" style="width:325px !important;" />
                                        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" OnClick="btnProcesar_Click" CssClass="btn btn-info" OnClientClick="javascript:cargar();" />
                                    </div>
                                    <div class="col-md-3"></div>
                                </div>
                                

                                <asp:HiddenField ID="hdnSeleccionGral" runat="server" />
                                <asp:HiddenField ID="hdnSeleccionFormato" runat="server" />
                            </div>
                            <div class="alert alert-success" role="alert" id="msgSuccesss" runat="server" visible="false">
                                <strong>Correcto!&nbsp;</strong> <asp:Label ID="lblSuccess" runat="server" Text="" Font-Size="9pt" ForeColor="Green"></asp:Label>
                            </div>
                            <div class="alert alert-danger" role="alert" id="msgError" runat="server" visible="false">
                                <strong>Error!&nbsp;</strong><asp:Label ID="lblError" runat="server" Text="" Font-Size="9pt" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblArchivoSimulado" runat="server" Text="" Visible="false" Font-Size="9pt" ForeColor="Green"></asp:Label>
                                <br />
                                <asp:Button ID="btnProcesarCargaSimulada" runat="server" Text="Procesar a carga masiva" OnClick="btnProcesarCargaSimulada_Click"
                                    Visible="false" CssClass="btn2 btn-info" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlExplorarRepositorioASUR" runat="server" Visible="false">
                            <div class="alert alert-success" role="alert" id="msgSuccessAsur" runat="server" visible="false">
                                <strong>Correcto!&nbsp;</strong> <asp:Label ID="lblSuccessAsur" runat="server" Text="" Font-Size="9pt" ForeColor="Green"></asp:Label>
                            </div>
                            <div class="alert alert-danger" role="alert" id="msgErrorAsur" runat="server" visible="false">
                                <strong>Error!&nbsp;</strong><asp:Label ID="lblErrorAsur" runat="server" Text="" Font-Size="9pt" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnExplorarRepo" runat="server" Text="Explorar Repositorio de Facturas" OnClick="btnExplorarRepo_Click" 
                                    CssClass="btn2 btn-info" />
                            </div>
                        </asp:Panel>

                        <%--<asp:UpdatePanel ID="upaGeneralAsur" runat="server">
                            <ContentTemplate>--%>
                                <asp:Panel ID="pnlRegASUR" runat="server" Visible="false" EnableViewState="true">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend><div align="center"><h3><asp:Label ID="lblTituloRegAsur" runat="server" Text=""></asp:Label></h3></div></legend>
                                            <div align="center">
                                                <table width="100%" class="table table-striped">
                                                    <tr>
                                                        <td>
                                                            <h3><asp:Label ID="lblTituloASUR" runat="server" Text="Facturas No Encontradas" Font-Size="10pt"></asp:Label></h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow-y:auto; max-height:200px;">
                                                                <asp:GridView ID="gvFacturasASUR" AutoGenerateColumns="false" runat="server" Width="100%" 
                                                                    CssClass="table table-bordered table-striped table-hover" OnRowDataBound="gvFacturasASUR_RowDataBound">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="NoFactura" HeaderText="No. Factura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                                        <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                                        <asp:BoundField DataField="FechaFactura" HeaderText="Fecha de Facturación" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                                        <asp:BoundField DataField="Archivo" />
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

                                                                <asp:GridView ID="gvNoEncontrados" AutoGenerateColumns="false" runat="server" Width="100%" 
                                                                    CssClass="table table-bordered table-striped table-hover">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Clave_Aeropuerto" HeaderText="Clave Aeropuerto" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                                        <asp:BoundField DataField="Factura" HeaderText="Factura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                                        <asp:BoundField DataField="Archivo_Lectura" HeaderText="Archivo de Lectura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter"  />
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <h3><asp:Label ID="Label2" runat="server" Text="Facturas Encontradas" Font-Size="10pt"></asp:Label></h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow-y:auto; max-height:200px;">

                                                                <asp:GridView ID="gvEncontrados" AutoGenerateColumns="false" runat="server" Width="100%" 
                                                                    CssClass="table table-bordered table-striped table-hover">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Clave_Aeropuerto" HeaderText="Clave Aeropuerto" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                                        <asp:BoundField DataField="Factura" HeaderText="Factura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                                        <asp:BoundField DataField="Archivo_Lectura" HeaderText="Archivo de Lectura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter"  />
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div>

                                                                <asp:Button ID="btnProcesarASUR" runat="server" Text="Procesar Facturas" ToolTip="Procesar facturas seleccionadas" 
                                                                    OnClick="btnProcesarASUR_Click" CssClass="btn2 btn-info" EnableViewState="true" ViewStateMode="Inherit" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                         </fieldset>
                                    </div>
                                </asp:Panel>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                            
                        
                        <asp:Panel ID="pnlRegASA" runat="server" Visible="false">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend><h3><asp:Label ID="lblTituloASA" runat="server" Text="Remesas"></asp:Label></h3></legend>
                                    <div align="center">
                                        <table width="100%" class="table table-striped">
                                            <tr>
                                                <td>
                                                    <h3><asp:Label ID="lblNoProcesados" runat="server" Text="Facturas No Encontradas" Font-Size="10pt"></asp:Label></h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="overflow-y:auto; max-height:200px;">
                                                    <asp:GridView ID="gvNoProcesados" AutoGenerateColumns="false" runat="server" Width="100%" 
                                                        CssClass="table table-bordered table-striped table-hover">
                                                        <Columns>
                                                            <asp:BoundField DataField="Unidad_Operativa" HeaderText="Unidad Operativa" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="Factura" HeaderText="Factura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                            <asp:BoundField DataField="Fecha_Transaccion" HeaderText="Fecha Transacción" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                            <asp:BoundField DataField="Fecha_Contable" HeaderText="Fecha Contable" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                            <asp:BoundField DataField="Base_Site" HeaderText="Tipo Transacción" ItemStyle-Width="50" HeaderStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-Width="50" HeaderStyle-CssClass="gvCenter" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:LinkButton ID="lkbExport" runat="server" OnClick="lkbExport_Click" ForeColor="Green" Font-Bold="true" Font-Size="Medium">Exportar a Excel</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h3><asp:Label ID="lblProcesados" runat="server" Text="Facturas Encontradas" Font-Size="10pt"></asp:Label></h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="overflow-y:auto; max-height:200px;">
                                                    <asp:GridView ID="gvProcesados" AutoGenerateColumns="false" runat="server" Width="100%" 
                                                        CssClass="table table-bordered table-striped table-hover">
                                                        <Columns>
                                                            <asp:BoundField DataField="Unidad_Operativa" HeaderText="Unidad Operativa" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="Factura" HeaderText="Factura" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                            <asp:BoundField DataField="Fecha_Transaccion" HeaderText="Fecha Transacción" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                            <asp:BoundField DataField="Fecha_Contable" HeaderText="Fecha Contable" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                            <asp:BoundField DataField="Base_Site" HeaderText="Tipo Transacción" ItemStyle-Width="50" HeaderStyle-CssClass="gvCenter" />
                                                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-Width="50" HeaderStyle-CssClass="gvCenter" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                        <%--<asp:UpdatePanel ID="upaProcesarFacturas" runat="server">
                                                            <ContentTemplate>--%>
                                                                <asp:Button ID="btnProcesarFacturas" runat="server" Text="Procesar Facturas" ToolTip="Procesar facturas seleccionadas" 
                                                                    OnClick="btnProcesarFacturas_Click" CssClass="btn2 btn-info" />
                                                            <%--</ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                        
                                                        <%--<asp:UpdateProgress id="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaProcesarFacturas">
                                                            <ProgressTemplate>
                                                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/icons/w_load.gif" AlternateText="Loading ..." ToolTip="Loading ..." Width="90" Height="90" 
                                                                        style="padding: 10px;position:fixed;top:45%;left:50%;" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>--%>
                                                    </div>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlSimulacion" runat="server" Visible="false">
                            <div class="col-md-12">
                            <fieldset>
                                <legend><h3><asp:Label ID="lblTituloProceso" runat="server" Text=""></asp:Label></h3></legend>
                                <div align="center" style="overflow-y:auto; max-height:450px;">
                                    <table width="100%" class="table table-striped">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvResultado" AutoGenerateColumns="false" runat="server" Width="100%" 
                                                    CssClass="table table-bordered table-striped table-hover">
                                                    <Columns>
                                                        <asp:BoundField DataField="Fila" HeaderText="FILA" ItemStyle-Width="50" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />
                                                        <asp:BoundField DataField="Campo" HeaderText="CAMPO" ItemStyle-Width="150" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
                                                        <asp:BoundField DataField="Valor" HeaderText="VALOR" ItemStyle-Width="100" HeaderStyle-CssClass="gvCenter" ItemStyle-CssClass="gvCenter" />  
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
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                            </div>
                        </asp:Panel>

                        <div style="text-align:right">
                            <asp:Button ID="btnRegresar" runat="server" Text=" Regresar " OnClick="btnRegresar_Click" Visible="false" CssClass="btn btn-success" />
                        </div>


                        <br />
                        
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

<%--    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProcesar"/>
        </Triggers>
    </asp:UpdatePanel>--%>

    <%--<div class="loader-page"></div>--%>


    <asp:UpdateProgress ID="prgLoadingStatus" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress">
                    <div id="theprogress">
                        <asp:Image ID="imgWaitIcon" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/icons/loading-animated.gif" />
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>


