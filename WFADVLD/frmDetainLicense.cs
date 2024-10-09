using DVLD;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmDetainLicense : Form
    {
        private int _DetainId = -1;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (txtFineFees.Text == "")
                errorProvider1.SetError(txtFineFees, "Fees Can Not Be Empty!");
            else
                errorProvider1.SetError(txtFineFees, "");
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1) return;

            lblLicenseID.Text = obj.ToString();
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);

            llShowLicenseHistory.Enabled = true;

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicense.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            txtFineFees.Focus();

            btnDetain.Enabled = true;
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
            frmLicenseInfo frm = new frmLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseId);
            frm.ShowDialog();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            _DetainId = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.currentUser.UserID);

            if (_DetainId == -1) return;

            MessageBox.Show("License Detained Successfully with ID=" + _DetainId.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblDetainID.Text = _DetainId.ToString();

            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
            txtFineFees.Enabled = false;
        }

        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }
    }
}
