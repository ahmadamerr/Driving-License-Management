using DVLD;
using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmIssueDriverLicense : Form
    {
        private int _AppId = -1;

        private clsLDLApplication _clsApplication;

        public frmIssueDriverLicense(int AppId)
        {
            InitializeComponent();

            _AppId = AppId;
        }

        private void frmIssueDriverLicense_Load(object sender, EventArgs e)
        {
            _clsApplication = clsLDLApplication.FindById(_AppId);

            if (_clsApplication == null)
            {
                MessageBox.Show("No Applicaiton with ID=" + _AppId.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            if (!_clsApplication.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            int LicenseId = _clsApplication.GetActiveLicenseID();

            if (LicenseId != -1)
            {
                MessageBox.Show("Person already has License before with License ID=" + LicenseId.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlDLApplicationInfo1.LoadByLDLApplicationId(_AppId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int LicenseId = _clsApplication.IssueLicenseForTheFirstTime(txtNotes.Text.Trim(), clsGlobal.currentUser.UserID);

            if (LicenseId != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseId.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                return;
            }
            else
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
