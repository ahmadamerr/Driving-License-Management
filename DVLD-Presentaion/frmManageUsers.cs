using DVLD;
using System;
using System.Data;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmManageUsers : Form
    {
        private static DataTable _DtAllUsers = clsUser.GetAllUsers();

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

            return;
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _DtAllUsers = clsUser.GetAllUsers();

            dataGridView1.DataSource = _DtAllUsers;

            if (_DtAllUsers.Rows.Count > 0 )
            {
                dataGridView1.Columns[0].HeaderText = "User ID";
                dataGridView1.Columns[0].Width = 110;

                dataGridView1.Columns[1].HeaderText = "Person ID";
                dataGridView1.Columns[1].Width = 120;

                dataGridView1.Columns[2].HeaderText = "Full Name";
                dataGridView1.Columns[2].Width = 350;

                dataGridView1.Columns[3].HeaderText = "UserName";
                dataGridView1.Columns[3].Width = 120;

                dataGridView1.Columns[4].HeaderText = "Is Active";
                dataGridView1.Columns[4].Width = 120;
            }

            lblRecordsCount.Text = _DtAllUsers.Rows.Count.ToString();

            cbFilter.SelectedIndex = 0;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddNew_Click(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = (int)dataGridView1.CurrentRow.Cells[0].Value;

            DialogResult result = MessageBox.Show(
            $"Do you want to Delete this User With ID: {UserId}",
            "Deleting User",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                if (clsUser.Delete(UserId))
                {
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmManageUsers_Load(null, null);
                }
                else
                    MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dataGridView1.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterValue = cbIsActive.Text;

            switch (filterValue)
            {
                case "All":
                    break;
                case "Yes":
                    filterValue = "1";
                    break;
                case "No":
                    filterValue = "0";
                    break;
            }

            if (filterValue == "All")
                _DtAllUsers.DefaultView.RowFilter = "";
            else
                _DtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", filterValue);

            lblRecordsCount.Text = _DtAllUsers.Rows.Count.ToString();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
            {
                cbIsActive.Visible = true;
                txtFilterValue.Visible = false;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            else
            {
                txtFilterValue.Visible = (cbFilter.Text != "None");

                cbIsActive.Visible = false;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _DtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _DtAllUsers.Rows.Count.ToString();

                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                _DtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _DtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _DtAllUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID" || cbFilter.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }
    }
}
