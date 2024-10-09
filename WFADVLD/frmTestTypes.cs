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
    public partial class frmTestTypes : Form
    {
        DataTable _DtAllTestTypes = clsTestTypes.GetAllTestTypes();

        public frmTestTypes()
        {
            InitializeComponent();
        }

        private void frmTestTypes_Load(object sender, EventArgs e)
        {
            _DtAllTestTypes = clsTestTypes.GetAllTestTypes();

            dataGridView1.DataSource = _DtAllTestTypes;

            lblRecordsCount.Text = _DtAllTestTypes.Rows.Count.ToString();

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 120;

            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[1].Width = 200;

            dataGridView1.Columns[2].HeaderText = "Description";
            dataGridView1.Columns[2].Width = 400;

            dataGridView1.Columns[3].HeaderText = "Fees";
            dataGridView1.Columns[3].Width = 100;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((clsTestTypes.enTestType)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmTestTypes_Load(null, null);
        }
    }
}
