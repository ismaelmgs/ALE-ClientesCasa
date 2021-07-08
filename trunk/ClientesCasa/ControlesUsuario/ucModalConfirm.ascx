<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucModalConfirm.ascx.cs" Inherits="ClientesCasa.ControlesUsuario.ucModalConfirm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<ajax:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="overlayy"
    TargetControlID="pnlPopup" PopupControlID="pnlPopup" OkControlID="btnOk" CancelControlID="btnOk">
</ajax:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" BackColor="White" Style="display: none;" DefaultButton="btnOk">
    <table style="width:100%">
        <tr class="modal-header">
            <td colspan="2" align="left" runat="server" id="tdCaption" style="background-color:#01609F">
                &nbsp; <asp:Label ID="lblCaption" runat="server" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr class="modal-body">
            <td style="width: 60px; vertical-align:central; text-align:center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Images/icons/information.png" />
            </td>
            <td style="vertical-align:central; text-align:left">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="modal-footer">
            <td colspan="2" style="text-align:right">
                <asp:Button ID="btnOk" runat="server" Text="Aceptar" OnClick="btnOk_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" CssClass="btn btn-default" />
            </td>
        </tr>
    </table>
</asp:Panel>

<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>

