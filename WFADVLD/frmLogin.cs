using DVLD;
using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.Find(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (user != null)
            {
                if (chkRememberMe.Checked) clsGlobal.RememberLoginInfo(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                else clsGlobal.RememberLoginInfo("", "");

                clsGlobal.currentUser = user;
                this.Hide();
                
                frmMain frm = new frmMain(this);    
                frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string userName = "", password = "";

            if (clsGlobal.GetRestoredLoginInfo(ref userName, ref password))
            {
                txtUserName.Text = userName;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }

            else chkRememberMe.Checked = false;
        }
    }
}
