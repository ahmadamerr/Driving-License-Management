using DVLD;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmChangePassword : Form
    {
        private int _UserId = -1;
        private clsUser _User;

        public frmChangePassword(int userId)
        {
            InitializeComponent();

            _UserId = userId;
        }

        private void _ResetDefaultValues()
        {
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";

            txtCurrentPassword.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            _User = clsUser.FindByUserID(_UserId);

            if (_User == null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserId,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserId);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");
            }
            else
                errorProvider1.SetError(txtNewPassword, "");
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This Password Doesn't Match New Password");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txtNewPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                   "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };

            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password is wrong!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };
        }
    }
}
