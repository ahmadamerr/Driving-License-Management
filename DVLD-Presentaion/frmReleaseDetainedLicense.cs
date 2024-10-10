using DVLD;
using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _LicenseId = -1;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseId)
        {
            InitializeComponent();

            _LicenseId = LicenseId;
            
            ctrlDriverLicenseInfoWithFilter1.LoadData(LicenseId);

            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void frmReleaseDetainedLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseId);
            frm.ShowDialog();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1) return;

            _LicenseId = obj;

            lblLicenseID.Text = _LicenseId.ToString();
            
            llShowLicenseHistory.Visible = true;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicense.IsDetained)
            {
                MessageBox.Show("Selected License i is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fee.ToString();
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;
            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseID.ToString();
            lblCreatedByUser.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.CreatedByUserInfo.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ApplicationId = -1;

            bool IsReleased = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.ReleaseDetainedLicense(clsGlobal.currentUser.UserID, ref ApplicationId);

            lblApplicationID.Text = ApplicationId.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }
    }
}
