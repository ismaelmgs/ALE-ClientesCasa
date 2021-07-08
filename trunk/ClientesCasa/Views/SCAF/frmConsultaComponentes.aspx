<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmConsultaComponentes.aspx.cs" Inherits="ClientesCasa.Views.SCAF.frmConsultaComponentes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <cc1:toolkitscriptmanager ID="ToolKitScriptManager" runat="server"></cc1:toolkitscriptmanager>
    <div style="text-align: left">
        <h4>&nbsp;&nbsp;Consulta de Componentes</h4>
    </div>

    <div class="widget-box"  style="width: 98%;margin-left: 10px;">
            <div class="widget-title">
                <span class="icon"><i class="icon-align-justify"></i></span>
                <h5>Búsqueda de Folios</h5>
            </div>
            <div class="widget-content nopadding">
                <div class="control-group">
                    <div class="controls">
                        <br />
                        <div class="table-responsive" style="margin:5px;">
                        <table style="width: 100%" class="table table-striped">
                            <tr>
                                <td style="text-align: left">
                                    <div class="section group" style="width: 92%;">
                                        <div class="col span_1_of_4">
                                            &nbsp; Folio del componente:
                                        </div>
	                                    <div class="col span_1_of_4">
                                            <asp:TextBox ID="txtFolioComponente" runat="server" ToolTip="Caja Editable" CssClass="span11" placeholder="Folio del componente"></asp:TextBox>
                                        </div>
                                        <div class="col span_2_of_4" style="text-align:center;">
                                            Rango de fechas: 
                                        </div>
                                    </div>
                                    <div class="section group" style="width: 92%;">
                                        <div class="col span_1_of_4">
                                            <br />&nbsp; Folio de discrepancia:
                                        </div>
	                                    <div class="col span_1_of_4">
                                            <br />
                                           <asp:TextBox ID="txtFolioDiscrepancia" runat="server" ToolTip="Caja Editable" CssClass="span11" placeholder="Folio de discrepancia"></asp:TextBox>
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
                                            <asp:Button ID="btnAceptar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                         </table>
                                        <div class="control-group">
                                          <asp:Panel ID="pnlReporteComponentes" runat="server">
                                            <div class="controls">
                                                <div class="widget-box">
                                                    <div class="widget-title" style="text-align:center;">
                                                        <span class="icon"><i class="icon-align-justify"></i></span>
                                                        <h4>Reporte de Componentes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h4>
                                                    </div>
                                                    <div class="section group" style="width: 100%;">
                                                        <%--<div class="col span_1_of_5">&nbsp;<img src="https://pbs.twimg.com/profile_images/633806355560706048/i-GaRFGe_400x400.jpg" width="150" height="150" style="width:150px;" /></div>--%>
                                                        <div class="col span_1_of_5"></div>
                                                        <div class="col span_3_of_5" style="text-align:center;margin-left: 40px;padding-top: 10px;"><strong>Aerolineas Ejecutivas S.A. de C.V.</strong></div>
                                                        <div class="col span_1_of_5">&nbsp;</div>
                                                    </div> 
                                                    <div style="overflow-x:auto; margin: 10px;">
                                                      <asp:GridView ID="gvConsultaComponentes" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                                                        DataKeyNames="IdComponente,IdDiscrepancia" AllowPaging="True" CssClass="table table-bordered table-striped table-hover"
                                                        Width="100%" PageSize="10" OnRowCommand="gvConsultaComponentes_RowCommand" OnPageIndexChanging="gvConsultaComponentes_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="IdDiscrepancia" HeaderText=" Id Discrepancia " />
                                                            <asp:BoundField DataField="IdComponente" HeaderText=" Id Componente " />
                                                            <asp:BoundField DataField="NombreComponente" HeaderText=" Nombre Componente " />
                                                            <asp:BoundField DataField="Descripcion" HeaderText=" Descripción "/>
                                                            <asp:BoundField DataField="FechaCreacion" HeaderText=" Fecha Alta " ItemStyle-HorizontalAlign="Center" DataFormatString="{0:g}"/>
                                                            <asp:BoundField DataField="TipoReporte" HeaderText=" Tipo Reporte " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Leg_Num" HeaderText=" Leg Num " ItemStyle-HorizontalAlign="Center"/>
                                                            <asp:BoundField DataField="AeronaveSerie" HeaderText=" Serie Aeronave " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="VueloTramoId" HeaderText=" Bitacora Vuelo Tramo Trip Num " ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div style="text-align: center">
                                                                        <asp:ImageButton ID="imEditarComponente" runat="server" ImageUrl="~/Images/icons/edit.png" Height="24" Width="24"
                                                                                        ToolTip="Editar Componente." CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                                                  <div style="text-align:right;padding-right:5%;">
                                         </div>
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
                                    <asp:label ID="lblIDCompRes" runat="server" CssClass="" style="font-weight:bold; margin-left:25%;"></asp:label>
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
                                    <%--<asp:Label ID="lblIDComp" runat="server" Text="ID:" Visible="false"></asp:Label> <asp:Label ID="Label32" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                                </div>
                                <div class="col span_1_of_4" style="text-align:left">
                                    <%--<asp:TextBox ID="txtIDComp" runat="server" CssClass="" MaxLength="50" placeholder="ID" Visible="false"></asp:TextBox>--%>
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
