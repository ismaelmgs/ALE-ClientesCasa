<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="frmConsultaClientes.aspx.cs" Inherits="ClientesCasa.Views.Catalogos.frmConsultaClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="gvMatriculas" runat="server" AutoGenerateColumns="False" EnableViewState="true"
        CssClass="table table-bordered table-striped table-hover">
        <Columns>
            <asp:BoundField DataField="AeronaveSerie" HeaderText=" No. Serie " />
            <asp:BoundField DataField="AeronaveMatricula" HeaderText=" Matricula " />
            <asp:BoundField DataField="AeronaveColor" HeaderText=" Color " />
            <asp:TemplateField HeaderText=" Acciones ">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron registros para mostrar
        </EmptyDataTemplate>
    </asp:GridView>

</asp:Content>
