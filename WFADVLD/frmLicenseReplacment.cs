using DVLD;
using System;
using System.Windows.Forms;
using static DVLD.clsLicense;
using static DVLD.clsApplication;

namespace WFADVLD
{
    public partial class frmLicenseReplacment : Form
    {
        private int _LicenseId = -1;

        public frmLicenseReplacment()
        {
            InitializeComponent();
        }

        private int _GetApplicationTypeID()
        {
            return rbLostLicense.Checked ? (int)enApplicationType.ReplaceLostDrivingLicense : (int)enApplicationType.ReplaceDamagedDrivingLicense;
        }

        private enIssueReason _IssueReason()
        {
            return rbDamagedLicense.Checked ? enIssueReason.DamagedReplacement : enIssueReason.LostReplacement;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_LicenseId);
            frm.ShowDialog();
        }

        private void frmLicenseReplacment_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1) return;

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;
            lblOldLicenseID.Text = obj.ToString();
            llShowLicenseHistory.Enabled = true;

            rbDamagedLicense.Checked = true;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicense.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssueReplacement.Enabled = false;

                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replace For Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationTypes.Find(_GetApplicationTypeID()).Fee.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replace For Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationTypes.Find(_GetApplicationTypeID()).Fee.ToString();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLicense license = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.Replace(_IssueReason(), clsGlobal.currentUser.UserID);

            if (license == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _LicenseId = license.LicenseID;

            MessageBox.Show("Licensed Replaced Successfully with ID=" + _LicenseId.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblApplicationID.Text = license.ApplicationID.ToString();
            lblRreplacedLicenseID.Text = _LicenseId.ToString();

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseHistory.Enabled = true;
        }
    }
}
