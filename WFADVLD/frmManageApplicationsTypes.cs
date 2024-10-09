using DVLD;
using System;
using System.Data;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmManageApplicationsTypes : Form
    {
        private DataTable _dtAllApplications = clsApplicationTypes.GetAllApplications();

        public frmManageApplicationsTypes()
        {
            InitializeComponent();
        }

        private void frmManageApplicationsType_Load(object sender, EventArgs e)
        {
            _dtAllApplications = clsApplicationTypes.GetAllApplications();
            dataGridView1.DataSource = _dtAllApplications;  

            lblRecordsCount.Text = _dtAllApplications.Rows.Count.ToString();

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 110;

            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[1].Width = 400;

            dataGridView1.Columns[2].HeaderText = "Fees";
            dataGridView1.Columns[2].Width = 100;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplication frm = new frmUpdateApplication((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmManageApplicationsType_Load(null, null);
        }
    }
}
