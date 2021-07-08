<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCarga.aspx.cs" Inherits="Views_CuentasPorPagar_frmCarga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <div>
        <asp:UpdatePanel ID="upaGeneral" runat="server">
            <ContentTemplate>

                <div>
                    <h2><asp:Label ID="lblTitulo" runat="server" Text="Carga de Layout"></asp:Label></h2>

                    <table>
                        <tr>
                            <td>
                                
                                <asp:FileUpload ID="fluArchivo" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="btnProcesar" runat="server" Text="Procesar" OnClick="btnProcesar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
