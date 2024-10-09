using DVLD;
using System;
using System.Data;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmIDLApplication : Form
    {
        private DataTable _Applications;

        public frmIDLApplication()
        {
            InitializeComponent();
        }

        private void frmIDLApplication_Load(object sender, EventArgs e)
        {
            _Applications = clsInternaionalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;

            dgvInternationalLicenses.DataSource = _Applications;
            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (_Applications.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInterNationalLicenseInfo frm = new frmInterNationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddILApplication frm = new frmAddILApplication();
            frm.ShowDialog();
        }
    }
}
