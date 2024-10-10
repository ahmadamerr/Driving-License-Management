using DVLD;
using System;
using System.Data;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmManagePeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetPeople();

        public frmManagePeople()
        {
            InitializeComponent();
        }   

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _dtAllPeople = clsPerson.GetPeople();

            dataGridView1.DataSource = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email"); 

            comboBox1.SelectedIndex = 0;

            lblRecordsCount.Text = _dtAllPeople.Rows.Count.ToString();

            if (_dtAllPeople.Rows.Count > 0) 
            {
                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[0].Width = 110;

                dataGridView1.Columns[1].HeaderText = "National No.";
                dataGridView1.Columns[1].Width = 120;

                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[2].Width = 120;

                dataGridView1.Columns[3].HeaderText = "Second Name";
                dataGridView1.Columns[3].Width = 140;

                dataGridView1.Columns[4].HeaderText = "Third Name";
                dataGridView1.Columns[4].Width = 120;

                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[5].Width = 120;

                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[6].Width = 120;

                dataGridView1.Columns[7].HeaderText = "Date Of Birth";
                dataGridView1.Columns[7].Width = 140;

                dataGridView1.Columns[8].HeaderText = "Nationality";
                dataGridView1.Columns[8].Width = 120;

                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[9].Width = 120;

                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[10].Width = 170;

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (comboBox1.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None") 
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllPeople.Rows.Count.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Enabled = (comboBox1.Text != "None");

            if (txtFilterValue.Visible) 
            {
                txtFilterValue.Text = "";

                txtFilterValue.Focus();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEdit addEdit = new frmAddEdit();
            addEdit.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ShowPersonInfo = new frmShowPersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);

            ShowPersonInfo.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEdit();
            frm.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEdit((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.Delete((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmManagePeople_Load(null, null);
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Form ShowPersonInfo = new frmShowPersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            ShowPersonInfo.ShowDialog();
        }
    }
}
