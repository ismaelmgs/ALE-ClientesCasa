<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmConsultaDiscrepancias.aspx.cs" Inherits="ClientesCasa.Views.SCAF.frmConsultaDiscrepancias" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <cc1:toolkitscriptmanager ID="ToolKitScriptManager" runat="server"></cc1:toolkitscriptmanager>
    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Consulta de Discrepancias</h4>
    </div>

    <div class="widget-box"  style="width: 98%;margin-left: 10px;">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de Folios</h5>
            </div>
            <div class="widget-content nopadding">
                <div class="control-group">
                    <div class="controls">
                        <div style="margin:5px;">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left">
                                    <div class="section group" style="width: 92%;">
                                        <div class="col span_1_of_4">
                                            &nbsp; Folio de la discrepancia:
                                        </div>
	                                    <div class="col span_1_of_4">
                                            <asp:TextBox ID="txtFolioDiscrepancia" runat="server" ToolTip="Caja Editable" CssClass="span11" placeholder="Folio de la discrepancia"></asp:TextBox>
                                        </div>
                                        <div class="col span_2_of_4" style="text-align:center;">
                                            Rango de fechas: 
                                        </div>
                                    </div>
                                    <div class="section group" style="width: 92%;">
                                        <div class="col span_1_of_4">
                                            <br />&nbsp; Matrícula:
                                        </div>
	                                    <div class="col span_1_of_4">
                                            <br />
                                            <asp:DropDownList ID="ddlMatricula" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col span_1_of_4" style="text-align:center;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inicio:<br /><asp:TextBox ID="txtFechaIni" type="date" runat="server"  placeholder="Fecha de Inicio" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col span_1_of_4" style="text-align:center;">
                                            Fin: <br /> <asp:TextBox ID="txtFechaFin" type="date" runat="server" placeholder="Fecha Fin" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="section group" style="width: 92%;">
                                        <div class="col span_1_of_4">
                                            &nbsp;
                                        </div>
                                        <div class="col span_1_of_4">
                                            &nbsp;
                                        </div>
                                        <div class="col span_1_of_4">
                                            &nbsp;
                                        </div>
                                        <div class="col span_1_of_4" style="text-align: right;">
                                            <asp:Button ID="btnAceptar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                         </table>

                                        <div class="control-group">
                                          <asp:Panel ID="pnlReporteDiscrepancias" runat="server">
                                            <div class="controls">
                                                <div class="widget-box">
                                                    <div class="widget-title" style="text-align:center;">
                                                        <span class="icon"><i class="icon-align-justify"></i></span>
                                                        <h4>Reporte de Discrepancias</h4>
                                                    </div>
                                                    <table align="center">
                                                        <tr><td colspan="6" align="center">&nbsp;</td>
                                                            <%--<td colspan="1" align="center"><img src="https://pbs.twimg.com/profile_images/633806355560706048/i-GaRFGe_400x400.jpg" width="60" height="60" align="center" /></td>--%>
                                                            <td colspan="1" align="center"></td>
                                                            <td colspan="2" align="center" valign="middle">Aerolineas Ejecutivas S.A. de C.V.</td>
                                                            <td colspan="7" align="center">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <br /><br />
                                                    <div style="overflow-x:auto; margin: 10px;">
                                                    <asp:GridView ID="gvConsultaDiscrepancias" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                                                        DataKeyNames="IdDiscrepancia, IdBitacora" AllowPaging="True" CssClass="table table-bordered table-striped table-hover"
                                                        Width="100%" PageSize="10" OnRowCommand="gvConsultaDiscrepancias_RowCommand" OnPageIndexChanging="gvConsultaDiscrepancias_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="IdDiscrepancia" HeaderText=" Folio " />
                                                            <asp:BoundField DataField="GrupoModelo" HeaderText=" Grupo Modelo " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="AeronaveMatricula" HeaderText=" Matricula " />
                                                            <asp:BoundField DataField="AeronaveMatricula" HeaderText=" Modelo "/>
                                                            <asp:BoundField DataField="AeronaveSerie" HeaderText=" Serie Aeronave"/>
                                                            <asp:BoundField DataField="FolioBitacora" HeaderText=" Folio Bitacora" />
                                                            <asp:BoundField DataField="TipoReporteDesc" HeaderText=" Tipo de Reporte " />
                                                            <asp:BoundField DataField="Anio" HeaderText=" Año " />
                                                            <asp:BoundField DataField="Mes" HeaderText=" Mes " />
                                                            <asp:BoundField DataField="FechaDiscrepancia" HeaderText=" Fecha Discrepancia " />
                                                            <asp:BoundField DataField="ATA" HeaderText=" Código ATA " />
                                                            <asp:BoundField DataField="OrigenDiscrepancia" HeaderText=" Origen Discrepancia " ItemStyle-HorizontalAlign="Center" DataFormatString="{0:g}" />
                                                            <asp:BoundField DataField="DescripcionDiscrepancia" HeaderText=" Descripción Discrepancia " ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:g}"/>
                                                            <asp:BoundField DataField="AccionCorrectiva" HeaderText=" Acción Correctiva " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="DiagnoticoDesc" HeaderText=" Diagnotico Discrepancia " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="NombreComponente" HeaderText=" Nombre Componente " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="DescripcionComponente" HeaderText=" Descripcion Componente " ItemStyle-HorizontalAlign="Center" />
                                                            
                                                            
                                                            <asp:BoundField DataField="NoParteRemovido" HeaderText=" Num Parte Removido " />
                                                            <asp:BoundField DataField="NoParteInstalado" HeaderText=" Num Parte Instalado " />
                                                            <asp:BoundField DataField="PosicionComponente" HeaderText=" Posicion Componente " ItemStyle-HorizontalAlign="Center" DataFormatString="{0:g}" />
                                                            <asp:BoundField DataField="NoSerieRemovido" HeaderText=" Num Serie Removido " ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:g}"/>
                                                            <asp:BoundField DataField="NoSerieInstalado" HeaderText=" Num Serie Instalado " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="RazonServicio" HeaderText=" Razon Servicio " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="ReferenciaRep" HeaderText=" Referencia Reportes " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="TiempoPlaneador" HeaderText=" Tiempo Planeador " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="TiempoMotorI" HeaderText=" Tiempo Motor I " />
                                                            <asp:BoundField DataField="TiempoMotorII" HeaderText=" Tiempo Motor II " />
                                                            <asp:BoundField DataField="CiclosMotorI" HeaderText=" Ciclos Motor I " ItemStyle-HorizontalAlign="Center" DataFormatString="{0:g}" />
                                                            <asp:BoundField DataField="CiclosMotorII" HeaderText=" Ciclos Motor II " ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:g}"/>
                                                            <asp:BoundField DataField="Aterrizajes" HeaderText=" Aterrizajes " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="CicloAPU" HeaderText=" Ciclo APU " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="TripNum" HeaderText=" Num Trip " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="PilotoId" HeaderText=" Piloto " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="CopilotoId" HeaderText=" Copiloto " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Base" HeaderText=" Base " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Mecanico" HeaderText=" Mecanico " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Origen" HeaderText=" Origen " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Destino" HeaderText=" Destino " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="TiempoVuelo" HeaderText=" Tiempo Vuelo " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="TiempoCalzo" HeaderText=" Tiempo Calzo " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="UsuarioCreacion" HeaderText=" Usuario Creacion " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="FechaCreacion" HeaderText=" Fecha Creacion " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="EstatusBitacora" HeaderText=" Estatus Bitacora " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="EstatusDiscrepancia" HeaderText=" Estatus Discrepancia " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="EstatusComponente" HeaderText=" Estatus Componente " ItemStyle-HorizontalAlign="Center" />
                                                            
                                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div style="text-align: center">
                                                                        <asp:ImageButton ID="imbEditarBitacora" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24"
                                                                            ToolTip="Edita Discrepancia." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron registros para mostrar
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>  
                                                    </div>
                                                    <br />
                                        </div>
                                    </div> 
                                </asp:Panel>
                           </div>
                             <div style="text-align:right;padding-right:5%;">
                                <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar a Excel" CssClass="btn btn-info" OnClick="btnExportarExcel_Click" style="width:130px !important;" />
                            </div>           
                       </div>
                    </div>
                </div>
            </div>
        </div>
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
                        <h4><asp:Label ID="lblTituloDiscrepancia" runat="server" Text="Modificar Discrepancia"></asp:Label></h4>
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
                                    <asp:Label ID="lblAccionCorrectiva" runat="server" Text="Acción Correctiva:" CssClass=""></asp:Label> <asp:Label ID="lblReqAccionCorrectiva" runat="server" Text="*" ForeColor="Red" ></asp:Label>
                                </div>
                               <div class="col span_1_of_4" style="text-align:left">
	                                <asp:TextBox ID="txtAccionCorrectiva" runat="server" CssClass="" TextMode="MultiLine" Rows="2" MaxLength="100" placeholder="Acción Correctiva"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCMotor1" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>                           
	                            </div>
                                <div class="col span_1_of_4" style="text-align:left">
	                            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" CssClass=""></asp:Label> <asp:Label ID="lblReqDisDescripcion" runat="server" Text="*" ForeColor="Red"></asp:Label>
	                            </div>
	                            <div class="col span_1_of_4" style="text-align:left">
	                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="" TextMode="MultiLine" Rows="2" MaxLength="50" placeholder="Descripcion"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtAPU" ValidChars="1234567890." FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>--%>
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
                                    <asp:Label ID="lblIdDisc" runat="server" CssClass="" Visible="true" style="font-weight:bold;margin-left:25%;"></asp:Label>
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
