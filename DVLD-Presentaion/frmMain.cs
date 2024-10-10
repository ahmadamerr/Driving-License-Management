using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();

            _frmLogin = frmLogin;
        }

        private void PeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationsTypes frm = new frmManageApplicationsTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTypes frm = new frmTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLDLApplication frm = new frmAddUpdateLDLApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLDLApplication frm = new frmLDLApplication();
            frm.ShowDialog();
        }

        private void DriversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDrivers frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void internatinalDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIDLApplication frm = new frmIDLApplication();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddILApplication frm = new frmAddILApplication();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLDLApplication frm = new frmRenewLDLApplication();
            frm.ShowDialog();
        }

        private void replacmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseReplacment frm = new frmLicenseReplacment();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLDLApplication frm = new frmLDLApplication();
            frm.ShowDialog();
        }

        private void mangeDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicense frm = new frmManageDetainedLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }
    }
}
