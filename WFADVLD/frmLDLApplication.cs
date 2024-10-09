using DVLD;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmLDLApplication : Form
    {
        private DataTable Applications;

        public frmLDLApplication()
        {
            InitializeComponent();
        }

        private void frmLDLApplication_Load(object sender, EventArgs e)
        {
            Applications = clsLDLApplication.GetAllApplications();
            dgvLocalDrivingLicenseApplications.DataSource = Applications;

            lblRecordsCount.Text = Applications.Rows.Count.ToString();

            if (Applications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 200;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 250;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;
            }

            cbFilterBy.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLDLApplicationInfo frm = new frmLDLApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmLDLApplication_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLDLApplication frm = new frmAddUpdateLDLApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmLDLApplication_Load(null, null);
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLDLApplication app = clsLDLApplication.FindById((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            if (app != null)
            {
                if (app.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmLDLApplication_Load(null, null);
                }
                else
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to Cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLDLApplication app = clsLDLApplication.FindById((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            if (app != null)
            {
                if (app.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmLDLApplication_Load(null, null);
                }
                else
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicense frm = new frmIssueDriverLicense((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmLDLApplication_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseId = clsLDLApplication.FindById((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).GetActiveLicenseID();

            if (LicenseId != -1)
            {
                frmLicenseInfo frm = new frmLicenseInfo(LicenseId);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        private void ScheduleTest(clsTestTypes.enTestType testType)
        {
            frmTests frm = new frmTests((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, testType);
            frm.ShowDialog();

            frmLDLApplication_Load(null, null);
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleTest(clsTestTypes.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleTest(clsTestTypes.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleTest(clsTestTypes.enTestType.StreetTest);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LDLApplicationId = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLDLApplication ldl = clsLDLApplication.FindById(LDLApplicationId);

            int totalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = ldl.IsLicenseIssued();

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = !LicenseExists && (totalPassedTests == 3);
            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editToolStripMenuItem.Enabled = !LicenseExists && (ldl.ApplicationStatus == clsApplication.enApplicationStatus.New);
            ScheduleTestsMenue.Enabled = !LicenseExists;
            CancelApplicaitonToolStripMenuItem.Enabled = (ldl.ApplicationStatus == clsApplication.enApplicationStatus.New);
            DeleteApplicationToolStripMenuItem.Enabled = (ldl.ApplicationStatus == clsApplication.enApplicationStatus.New);

            bool passedVision = ldl.DoesPassTestType(clsTestTypes.enTestType.VisionTest);
            bool passedWritten = ldl.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool passedStreet = ldl.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!passedVision || !passedWritten || !passedStreet) && (ldl.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !passedVision;
                scheduleWrittenTestToolStripMenuItem.Enabled = !passedWritten && passedVision;
                scheduleStreetTestToolStripMenuItem.Enabled = !passedStreet && passedVision && passedWritten;
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLDLApplication frm = new frmAddUpdateLDLApplication();
            frm.ShowDialog();

            frmLDLApplication_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";

                txtFilterValue.Focus();
            }

            Applications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = Applications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            string FilterValue = txtFilterValue.Text.Trim();

            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || FilterValue == "")
            {
                Applications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = Applications.Rows.Count.ToString();

                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                Applications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            else
                Applications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, FilterValue);

            lblRecordsCount.Text = Applications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            else return;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLApplication l = clsLDLApplication.FindById((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(l.ApplicationPersonID);
            frm.ShowDialog();
        }
    }
}
