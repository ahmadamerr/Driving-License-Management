using DVLD;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmAddUpdateUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1};

        private enMode _mode;
        private clsUser _User;
        private int _id;

        public frmAddUpdateUser()
        {
            InitializeComponent();

            _mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _mode = enMode.Update;

            _id = UserID;
        }

        private void _ResetDefaultValues()
        {
            if (_mode == enMode.AddNew)
            {
                this.Name = "Add New User";
                lblTitle.Text = "Add New User";

                _User = new clsUser();

                tbLoginInfo.Enabled = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                this.Name = "Update User";
                lblTitle.Text = "Update User";

                tbLoginInfo.Enabled = true;

                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_id);

            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                this.Close();
                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName.ToString();
            txtPassword.Text = _User.Password.ToString();
            txtConfirmPassword.Text = _User.Password.ToString();
            chkIsActive.Checked = _User.IsActive;

            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_mode == enMode.Update) _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tbLoginInfo.Enabled = true;

                tbUserInfo.SelectedTab = tbUserInfo.TabPages["tbLoginInfo"];

                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistWithPersonId(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tbLoginInfo.Enabled = true;

                    tbUserInfo.SelectedTab = tbUserInfo.TabPages["tbLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person In System", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");

                return;
            }
            else
                errorProvider1.SetError(txtUserName, "");

            if (_mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                    errorProvider1.SetError(txtUserName, "");
            }
            else
            {
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                        errorProvider1.SetError(txtUserName, "");
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password Cannot Be Blank");
            }
            else
                errorProvider1.SetError(txtPassword, "");
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
                errorProvider1.SetError(txtConfirmPassword, "");
        }

        private void btnPersonInfoNext_Click_1(object sender, EventArgs e)
        {
            if (_mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tbLoginInfo.Enabled = true;
                tbUserInfo.SelectedTab = tbUserInfo.TabPages["tbLoginInfo"];

                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExist(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tbLoginInfo.Enabled = true;
                    tbUserInfo.SelectedTab = tbUserInfo.TabPages["tbLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                lblTitle.Text = "Update User";
                _mode = enMode.Update;
                this.Text = "Update User";

                lblUserID.Text = _User.UserID.ToString();

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
