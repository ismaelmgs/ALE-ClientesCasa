using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientesCasa.ControlesUsuario
{
    public partial class ucModalConfirm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnOk.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk.UniqueID, "");
            btnCancel.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnCancel.UniqueID, "");
        }

        public void ShowMessage(string Message, string Caption)
        {
            lblMessage.Text = Message;
            lblCaption.Text = Caption;
            tdCaption.Visible = true;
            mpext.Show();
        }

        public void Hide()
        {
            lblMessage.Text = "";
            lblCaption.Text = "";
            mpext.Hide();
        }

        public void btnOk_Click(object sender, EventArgs e)
        {
            OnOkButtonPressed(e);
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            onCancelButtonPressed(e);
        }

        public delegate void OkButtonPressedHandler(object sender, EventArgs args);
        public delegate void CancelButtonPressedHandler(object sender, EventArgs args);
        
        public event OkButtonPressedHandler OkButtonPressed;
        public event CancelButtonPressedHandler CancelButtonPressed;

        protected virtual void OnOkButtonPressed(EventArgs e)
        {
            if (OkButtonPressed != null)
                OkButtonPressed(btnOk, e);
        }

        protected virtual void onCancelButtonPressed(EventArgs e)
        {
            if (CancelButtonPressed != null)
                CancelButtonPressed(btnCancel, e);
        }
    }
}