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
    public partial class frmDrivers : Form
    {
        private DataTable _Drivers;

        public frmDrivers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            _Drivers = clsDriver.GetAllDrivers();

            dgvDrivers.DataSource = _Drivers;

            cbFilterBy.SelectedIndex = 0;

            if (_Drivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 170;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 150;
            }

            lblRecordsCount.Text = _Drivers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

            txtFilterValue.Text = "";

            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColumnFilter = "";
            string FilterValue = txtFilterValue.Text;

            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    ColumnFilter = "DriverID";
                    break;
                case "Person ID":
                    ColumnFilter = "PersonID";
                    break;
                case "National No.":
                    ColumnFilter = "NationalNo.";
                    break;
                case "Full Name":
                    ColumnFilter = "FullName";
                    break;
                default:
                    ColumnFilter = "None";
                    break;
            }

            if (FilterValue.Trim() == "" || cbFilterBy.Text == "None")
            {
                _Drivers.DefaultView.RowFilter = "";

                lblRecordsCount.Text = _Drivers.Rows.Count.ToString();

                return;
            }

            if (ColumnFilter == "PersonID" || ColumnFilter == "DriverID")
                _Drivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnFilter, FilterValue.Trim());
            else
                _Drivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", ColumnFilter, FilterValue.Trim());

            lblRecordsCount.Text = _Drivers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "DriverID" || cbFilterBy.Text == "PersonID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            frmDrivers_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
    }
}
