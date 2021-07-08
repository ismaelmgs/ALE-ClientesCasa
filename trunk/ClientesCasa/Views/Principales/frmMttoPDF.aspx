<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmMttoPDF.aspx.cs" Inherits="ClientesCasa.Views.Principales.frmMttoPDF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .tableCus th, .tableCus td {
        }

        td {
            padding: 1px 0px 0px 5px !important;
            vertical-align: top !important;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>

            <br />
            <asp:Label ID="lblResult" runat="server" />
            <br />

            <div class="col-md-12" align="center">
                <asp:TextBox ID="txtReferencia" runat="server" placeholder="Referencia Concurso"></asp:TextBox>
                <asp:ImageButton ID="imgbtnBuscar" runat="server" ImageUrl="~/Images/icons/find.png" OnClick="imgbtnBuscar_Click" />
            </div>
            <div class="col-md-12" style="text-align:center">
                <asp:Label ID="lblReferencia" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            <div class="col-md-3"></div>
            <div class="col-md-6" align="center">
                <table width="90%" class="tableCus" border="0">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnCrearPoliza" runat="server" Text="Crear Poliza" CssClass="btn btn-info" OnClick="btnCrearPoliza_Click" Visible="false" />
                        </td>
                    </tr>
                </table>

            </div>
            <div class="col-md-3"></div>

            <div class="col-md-12" align="center">
                <asp:UpdatePanel ID="upaFiles" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="false" Width="90%"
                            CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvFiles_RowCommand"
                            OnRowDataBound="gvFiles_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imbUp" runat="server" CommandName="Up" ImageUrl="~/Images/icons/up.png"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24" Width="24" />
                                        <asp:ImageButton ID="imbDown" runat="server" CommandName="Down" ImageUrl="~/Images/icons/down.png"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="24" Width="24" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FileName" HeaderText="Nombre Archivo" />
                                <asp:BoundField DataField="TipoArchivo" HeaderText="Tipo Archivo" />
                                <asp:BoundField DataField="Url" HeaderText="URL" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnView" runat="server" CommandName="ViewDoc" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            OnClick="imgbtnView_Click" ImageUrl="~/Images/icons/view.png" Width="25px" Height="25px" ToolTip="Mostrar Documento" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="DeleteReg" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            OnClick="imgbtnDelete_Click" ImageUrl="~/Images/icons/delete.png" Width="25px" Height="25px" ToolTip="Eliminar Registro"
                                            OnClientClick="return confirm('¿Realmente desea eliminar el documento?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <div class="col-md-12" align="center">
                <table width="90%" class="tableCus" border="0">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnUnir" runat="server" Text="Unir" OnClick="btnUnir_Click" CssClass="btn btn-info" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="pnlDocuments" runat="server">
        <ContentTemplate>
            <div class="col-md-12" style="text-align:center">
                <asp:FileUpload ID="fuDocuments" runat="server" AllowMultiple="true" CssClass="btn btn-success" Style="width: 325px !important;" />
    
                <asp:Button ID="btnUploadImages" Text="Subir Imágenes" runat="server" OnClick="UploadMultipleImages" Visible="false"
                    accept="document/pdf" CssClass="btn btn-info" Style="width: 130px !important; height: 38px;" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    


    <%--<a href="../CuentasPorPagar/frmCargaMasiva.aspx" target="_blank" onclick="window.open(this.href,this.target,'width=400,height=250,top=120,left=100,toolbar=no,location=no,status=no,menubar=no');return false;">Mi popup mal hecho</a>--%>




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
