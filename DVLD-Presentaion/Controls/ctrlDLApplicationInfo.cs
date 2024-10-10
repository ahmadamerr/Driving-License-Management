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

namespace WFADVLD.Controls
{
    public partial class ctrlDLApplicationInfo : UserControl
    {
        private clsLDLApplication LDLApplication;

        private int _id = -1;
        private int _licenseId = -1;

        public int Id { get { return _id; } }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _id = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
        }

        public ctrlDLApplicationInfo()
        {
            InitializeComponent();
        }

        private void _FillData()
        {
            _licenseId = LDLApplication.GetActiveLicenseID();

            llShowLicenceInfo.Enabled = (_licenseId != -1);


            lblLocalDrivingLicenseApplicationID.Text = LDLApplication.LDLApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClasses.Find(LDLApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = LDLApplication.GetPassedTestCount().ToString() + "/3";
            ctrlApplicationBasicInfo1.LoadData(LDLApplication.ApplicationID);

        }

        public void LoadByAppId(int AppId)
        {
            LDLApplication = clsLDLApplication.GetByAppId(AppId);

            if (LDLApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + AppId.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillData();
        }

        public void LoadByLDLApplicationId(int LDLAppId)
        {
            LDLApplication = clsLDLApplication.FindById(LDLAppId);

            if (LDLApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LDLAppId.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillData();
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(LDLApplication.ApplicationPersonID);
            frm.ShowDialog();

            _FillData();
        }
    }
}
