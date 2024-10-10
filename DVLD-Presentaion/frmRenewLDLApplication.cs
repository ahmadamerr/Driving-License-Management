using DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmRenewLDLApplication : Form
    {
        private int _Id = -1;

        public frmRenewLDLApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1) return;

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fee.ToString();
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;
            lblOldLicenseID.Text = obj.ToString();
            llShowLicenseHistory.Enabled = true;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseClassIfo.DefaultValidityLength));
            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseClassIfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.Notes;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicense.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.ExpirationDate)
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenewLicense.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicense.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }

        private void frmRenewLDLApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_Id);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLicense License = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.RenewLicense(txtNotes.Text.Trim(), clsGlobal.currentUser.UserID);

            if (License == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = License.ApplicationID.ToString();
            _Id = License.LicenseID;
            lblRenewedLicenseID.Text = _Id.ToString();

            MessageBox.Show("Licensed Renewed Successfully with ID=" + _Id.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }
    }
}
