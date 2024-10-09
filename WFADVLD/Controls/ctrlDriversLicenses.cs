using DVLD;
using System;
using System.Data;
using System.Windows.Forms;

namespace WFADVLD.Controls
{
    public partial class ctrlDriversLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;

        public ctrlDriversLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLLicenseInfo()
        {
            _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;

            if (_dtDriverLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;
            }

            lblLocalLicensesRecords.Text = _dtDriverLocalLicensesHistory.Rows.Count.ToString();
        }

        private void _LoadILicenseInfo()
        {
            _dtDriverInternationalLicensesHistory = clsInternaionalLicense.GetDriverInternationalLicenses(_DriverID);

            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;

            if (_dtDriverInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;
            }

            lblInternationalLicensesRecords.Text = _dtDriverInternationalLicensesHistory.Rows.Count.ToString();
        }

        public void LoadInfoByDriver(int DriverId)
        {
            _DriverID = DriverId;
            _Driver = clsDriver.FindByDriverID(DriverId);

            _LoadLLicenseInfo();
            
            _LoadILicenseInfo();
        }

        public void LoadInfoByPerson(int PersonId)
        {
            _Driver = clsDriver.FindByPersonID(PersonId);

            if (_Driver != null) _DriverID = _Driver.DriverID;

            _LoadLLicenseInfo();

            _LoadILicenseInfo();
        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInterNationalLicenseInfo frm = new frmInterNationalLicenseInfo((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
