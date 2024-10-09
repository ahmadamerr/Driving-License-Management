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
    public partial class frmAddILApplication : Form
    {
        private int _ILId = -1;

        public frmAddILApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1) return;

            lblLocalLicenseID.Text = obj.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));//add one year.
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fee.ToString();
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;

            llShowLicenseHistory.Enabled = true;

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveId = clsInternaionalLicense.GetActiveInternationalLicenseId(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.DriverID);

            if (ActiveId != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveId.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _ILId = ActiveId;
                llShowLicenseInfo.Enabled = true;
                btnIssueLicense.Enabled = false;

                return;
            }

            btnIssueLicense.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmAddILApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternaionalLicense InternationalLicense = new clsInternaionalLicense();

            InternationalLicense.ApplicationPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fee;
            InternationalLicense.CreatedByUserID = clsGlobal.currentUser.UserID;
            InternationalLicense.DriverId = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.CreatedByUserID = clsGlobal.currentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _ILId = InternationalLicense.InternationalLicenseId;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseId.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseId.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInterNationalLicenseInfo frm = new frmInterNationalLicenseInfo(_ILId);
            frm.ShowDialog();
        }
    }
}
