using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.Authentication;

namespace SMSSendingSystem.World
{
    public partial class PasswordForm : BaseForm
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            lblUserName.Text = DSAServices.UserAccount;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MsgBox.Show("請輸入密碼!");
                return;
            }

            bool pass = false;
            try
            {
                pass = DSAServices.ConfirmPassword(txtPassword.Text, null);
            }
            catch (Exception ex)
            {
                MsgBox.Show(FISCA.ErrorReport.Generate(ex));
                return;
            }

            if (pass)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
            else
            {
                MsgBox.Show("密碼錯誤");
                return;
            }

            this.Close();
        }
    }
}
