<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmDetailDash.aspx.cs" Inherits="ClientesCasa.Views.Principales.frmDetailDash" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .accordion .card .card-header {
            border-bottom: 0;
            padding: 1rem 1.5rem;
            background: 0 0;
        }

        .accordion .card .fa-angle-down {
            float: right;
        }

        .fa {
            display: inline-block;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: 20px;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .accordion .card {
            -webkit-box-shadow: none;
            box-shadow: none;
            border-bottom: 1px solid #e0e0e0!important;
            -webkit-border-radius: 0;
            border-radius: 0;
        }

        .accordion .card:last-of-type {
            border-top-left-radius: 0;
            border-top-right-radius: 0;
        }

        .card {
            border: 0;
            font-weight: 400;
        }

        .card {
            position: relative;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-direction: column;
            flex-direction: column;
            min-width: 0;
            word-wrap: break-word;
            background-color: #fff;
            background-clip: border-box;
            /*border: 1px solid rgba(0,0,0,.125);*/
            border-radius: .25rem;
        }

        .lbl_Pendientes{
            border-radius: 7px 2px;
            border: 1px solid #c30404f2;
            background-color: #ff0000;
            color: #fff;
            font-size: 8pt;
            font-weight: 600;
            padding-left: 5px;
            padding-right: 5px;
            padding-bottom: 2px;
            padding-top: 2px;
        }
    </style>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <div>

        <asp:UpdatePanel ID="upaPrincipal" runat="server">
            <ContentTemplate>

                <div class="col-md-12" align="left" style="padding-left:30px;padding-top:15px;">
                    <span>
                        <asp:Label ID="lblTitulo" runat="server" Text="" Font-Bold="true" Font-Size="14pt"></asp:Label>
                    </span>
                </div>

                <div class="col-md-3"></div>
                <div class="col-md-6" align="center">

                    <!--Sección Contratos-->
                    <asp:Panel ID="pnlContratos" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="lblContratos" runat="server" Text="Próximos Vencimientos" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvContratosPorVencer" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave Contrato" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave Cliente" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PorcParticipacion" HeaderText="% Participación" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DiasVencimiento" HeaderText="Días para vencimiento" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlContratosFinalizados" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label1" runat="server" Text="Contratos Finalizados" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvContratosFinalizados" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave Contrato" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave Cliente" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PorcParticipacion" HeaderText="% Participación" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DiasVencidos" HeaderText="Días Vencidos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlSegurosVencidos" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label3" runat="server" Text="Seguros Vencidos" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvSegurosVencidos" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave Contrato" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave Cliente" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PorcParticipacion" HeaderText="% Participación" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DiasVencimiento" HeaderText="Días Vencidos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlAeronaves" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label2" runat="server" Text="Próximos Vencimientos" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvProxVencimientos" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave Contrato" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave Cliente" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PorcParticipacion" HeaderText="% Participación" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DiasXVencer" HeaderText="Días para vencimiento" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <!--Sección Aeronaves-->
                    <asp:Panel ID="pnlAeronavesGenerales" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label9" runat="server" Text="Próximos Mantenimientos" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvProximosMttos" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="descripcion_gral" HeaderText="Des. Gral." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="lugar_icao" HeaderText="Lugar Mtto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="tipo_mtto" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlEstatusAeronaves" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label10" runat="server" Text="Estatus de Mantenimiento" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvEstatusMtto" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="descripcion_gral" HeaderText="Des. Gral." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="lugar_icao" HeaderText="Lugar Mtto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="tipo_mtto" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="status" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>
                    
                    

                    <!--Sección Gastos-->
                    <asp:Panel ID="pnlClientesPendientes" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label4" runat="server" Text="Clientes Pendientes" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvClientesPendientes" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Pendientes" HeaderText="Gastos Pendientes" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlClientesAlDia" runat="server" Visible="false">
                        <div style="padding-top:50px;">
                            <div class="col-md-12">
                                <asp:Label ID="Label5" runat="server" Text="Clientes al día" Font-Bold="true" Font-Size="12pt"></asp:Label>
                            </div>
                            <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                <asp:GridView ID="gvClientesAlDia" runat="server" AutoGenerateColumns="false" Width="70%"
                                    CssClass="table table-bordered table-striped table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave Cliente" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ClaveContrato" HeaderText="Clave Contrato" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="GastosSAP" HeaderText="Gastos en SAP" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="GastosCC" HeaderText="Gastos en Clientes Casa" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <!--Pilotos-->
                    
                    <asp:Panel ID="pnlMainPilotos" runat="server" Visible="true">
                        <!--Accordion wrapper-->
                        <div class="accordion col-md-12" id="accordionEx" role="tablist" aria-multiselectable="true">

                            <!-- Accordion card -->
                            <div class="card">

                                <!-- Card header -->
                                <div class="card-header" role="tab" id="headingOne">
                                    <a data-toggle="collapse" data-parent="#accordionEx" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                        <h5 class="mb-0">
                                            <Table style="width:100%;">
                                                <tr>
                                                    <td style="width:20%;">&nbsp;</td>
                                                    <td style="width:40%;text-align:center;">
                                                        ADIESTRAMIENTO
                                                    </td>
                                                    <td style="width:30%;text-align:left;">
                                                        <asp:Label ID="lblPendAdto" runat="server" Text="" CssClass="lbl_Pendientes" Visible="false" ToolTip="Próximos a vencer"></asp:Label>
                                                    </td>
                                                    <td style="text-align:center; width:10%;">
                                                        <i class="fa fa-angle-down rotate-icon"></i>
                                                    </td>
                                                </tr>
                                            </Table>
                                        </h5>
                                    </a>
                                </div>

                                <!-- Card body -->
                                <div id="collapseOne" class="collapse show" role="tabpanel" aria-labelledby="headingOne" data-parent="#accordionEx">
                                    <div class="card-body">
                                        <asp:Panel ID="pnlAdiestramientos" runat="server" Visible="false">
                                            <fieldset>
                                                <asp:Panel ID="pnlAdiestramiento" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label6" runat="server" Text="Por Vencer" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvAdiestramientoXVencer" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="lugar" HeaderText="Lugar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasvencimiento" HeaderText="Días por vencer" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlAdiestramientosVencidos" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label8" runat="server" Text="Vencidos" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvAdiestramientosVencidos" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigoPiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="lugar" HeaderText="Lugar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasvencimiento" HeaderText="Días Vencidos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <!-- Accordion card -->

                            <!-- Accordion card -->
                            <div class="card">

                                <!-- Card header -->
                                <div class="card-header" role="tab" id="headingTwo">
                                    <a class="collapsed" data-toggle="collapse" data-parent="#accordionEx" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                        <h5 class="mb-0">
                                            <Table style="width:100%;">
                                                <tr>
                                                    <td style="width:20%;">&nbsp;</td>
                                                    <td style="width:40%;text-align:center;">
                                                        EXÁMENES MÉDICOS
                                                    </td>
                                                    <td style="width:30%;text-align:left;">
                                                        <asp:Label ID="lblExaMed" runat="server" Text="" CssClass="lbl_Pendientes" Visible="false" ToolTip="Próximos a vencer"></asp:Label>
                                                    </td>
                                                    <td style="text-align:center; width:10%;">
                                                        <i class="fa fa-angle-down rotate-icon"></i>
                                                    </td>
                                                </tr>
                                            </Table>
                                        </h5>
                                    </a>
                                </div>

                                <!-- Card body -->
                                <div id="collapseTwo" class="collapse" role="tabpanel" aria-labelledby="headingTwo" data-parent="#accordionEx">
                                    <div class="card-body">
                                        <asp:Panel ID="pnlExamenesMedicos" runat="server" Visible="false">
                                            <fieldset>
                                                <asp:Panel ID="pnlExamenesPorVencer" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label7" runat="server" Text="Exámenes Médicos Por Vencer" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvExamenesXVencer" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechainicio" HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechafin" HeaderText="Fecha Fin" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="lugar" HeaderText="Lugar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasporvencer" HeaderText="Días por vencer" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <%--<asp:Panel ID="pnlExamenesVencidos" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label11" runat="server" Text="Exámenes Médicos Vencidos" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvExamenesVencidos" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechainicio" HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechafin" HeaderText="Fecha Fin" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="lugar" HeaderText="Lugar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasvencimiento" HeaderText="Días vencidos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>--%>


                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <!-- Accordion card -->

                            <!-- Accordion card -->
                            <div class="card">

                                <!-- Card header -->
                                <div class="card-header" role="tab" id="headingThree">
                                    <a class="collapsed" data-toggle="collapse" data-parent="#accordionEx" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                        <h5 class="mb-0">
                                            <Table style="width:100%;">
                                                <tr>
                                                    <td style="width:20%;">&nbsp;</td>
                                                    <td style="width:40%;text-align:center;">
                                                        LICENCIA DE VUELO
                                                    </td>
                                                    <td style="width:30%;text-align:left;">
                                                        <asp:Label ID="lblLicVuelo" runat="server" Text="" CssClass="lbl_Pendientes" Visible="false" ToolTip="Expedidos"></asp:Label>
                                                    </td>
                                                    <td style="text-align:center; width:10%;">
                                                        <i class="fa fa-angle-down rotate-icon"></i>
                                                    </td>
                                                </tr>
                                            </Table>
                                        </h5>
                                    </a>
                                </div>

                                <!-- Card body -->
                                <div id="collapseThree" class="collapse" role="tabpanel" aria-labelledby="headingThree" data-parent="#accordionEx">
                                    <div class="card-body">
                                        <asp:Panel ID="pnlLicVuelos" runat="server" Visible="false">
                                            <fieldset>
                                                <asp:Panel ID="Panel1" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label13" runat="server" Text="Licencias de Vuelo expedidas" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvLicVuelos" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="licvuelo" HeaderText="No. Licencia de Vuelo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <%--<asp:BoundField DataField="expedida" HeaderText="Fecha Expedición" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />--%>
                                                                    <asp:BoundField DataField="cdlicencia" HeaderText="Cd. Licencia" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="tipolic" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="cdtrabajo" HeaderText="Lugar de Trabajo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <!-- Accordion card -->

                            <!-- Accordion card -->
                            <div class="card">

                                <!-- Card header -->
                                <div class="card-header" role="tab" id="headingFour">
                                    <a class="collapsed" data-toggle="collapse" data-parent="#accordionEx" href="#collapseFour" aria-expanded="false" aria-controls="collapseThree">
                                        <h5 class="mb-0">
                                            <Table style="width:100%;">
                                                <tr>
                                                    <td style="width:20%;">&nbsp;</td>
                                                    <td style="width:40%;text-align:center;">
                                                        VISAS
                                                    </td>
                                                    <td style="width:30%;text-align:left;">
                                                        <asp:Label ID="lblVisa" runat="server" Text="" CssClass="lbl_Pendientes" Visible="false" ToolTip="Próximos a vencer"></asp:Label>
                                                    </td>
                                                    <td style="text-align:center; width:10%;">
                                                        <i class="fa fa-angle-down rotate-icon"></i>
                                                    </td>
                                                </tr>
                                            </Table>
                                        </h5>
                                    </a>
                                </div>

                                <!-- Card body -->
                                <div id="collapseFour" class="collapse" role="tabpanel" aria-labelledby="headingFour" data-parent="#accordionEx">
                                    <div class="card-body">
                                        <asp:Panel ID="pnlVisas" runat="server" Visible="false">
                                            <fieldset>

                                                <asp:Panel ID="pnlVisasVencidas" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label16" runat="server" Text="Vencidas" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvVisasVencidas" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="novisa" HeaderText="No. Visa" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechaexpira" HeaderText="Fecha Expiración" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="pais" HeaderText="País" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasvencidos" HeaderText="Días Vencidos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <%--<asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlVisasxvencer" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label15" runat="server" Text="Por Vencer" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvVisasXVencer" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="novisa" HeaderText="No. Visa" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechaexpira" HeaderText="Fecha Expiración" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="pais" HeaderText="País" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasporvencer" HeaderText="Días por vencer" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <%--<asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <!-- Accordion card -->


                            <!-- Accordion card -->
                            <div class="card">

                                <!-- Card header -->
                                <div class="card-header" role="tab" id="headingFive">
                                    <a class="collapsed" data-toggle="collapse" data-parent="#accordionEx" href="#collapseFive" aria-expanded="false" aria-controls="collapseThree">
                                        <h5 class="mb-0">
                                            <Table style="width:100%;">
                                                <tr>
                                                    <td style="width:20%;">&nbsp;</td>
                                                    <td style="width:40%;text-align:center;">
                                                        PASAPORTES
                                                    </td>
                                                    <td style="width:30%;text-align:left;">
                                                        <asp:Label ID="lblPassport" runat="server" Text="" CssClass="lbl_Pendientes" Visible="false" ToolTip="Próximos a vencer"></asp:Label>
                                                    </td>
                                                    <td style="text-align:center; width:10%;">
                                                        <i class="fa fa-angle-down rotate-icon"></i>
                                                    </td>
                                                </tr>
                                            </Table>
                                        </h5>
                                    </a>
                                </div>

                                <!-- Card body -->
                                <div id="collapseFive" class="collapse" role="tabpanel" aria-labelledby="headingFive" data-parent="#accordionEx">
                                    <div class="card-body">
                                        <asp:Panel ID="pnlPasaporte" runat="server" Visible="false">
                                            <fieldset>
                                                <asp:Panel ID="pnlPasaporteXVencer" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label18" runat="server" Text="Por Vencer" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvPasaportesXVencer" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="nopassport" HeaderText="No. Pasaporte" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechaexpira" HeaderText="Fecha Expiración" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="pais" HeaderText="País" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasporvencer" HeaderText="Días por vencer" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlPasaportesVencidos" runat="server" Visible="true">
                                                    <div style="padding-top:50px;">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label19" runat="server" Text="Vencidos" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12" style="max-height:200px; overflow-y:auto;">
                                                            <asp:GridView ID="gvPasaportesVencidos" runat="server" AutoGenerateColumns="false" Width="90%"
                                                                CssClass="table table-bordered table-striped table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="codigopiloto" HeaderText="Código Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="piloto" HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="nopassport" HeaderText="No. Pasaporte" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="fechaexpira" HeaderText="Fecha Expiración" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="pais" HeaderText="País" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="diasvencidos" HeaderText="Días Vencidos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <!-- Accordion card -->

                        </div>
                        <!--/.Accordion wrapper-->
                    </asp:Panel>                    

                </div>
                <div class="col-md-3" style="padding-bottom:20px;"></div>
                

            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="col-md-12" align="right" style="padding-right:20px;">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn btn-success" />
        </div>

        
    </div>

</asp:Content>
