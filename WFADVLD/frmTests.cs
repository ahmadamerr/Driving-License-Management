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
using WFADVLD.Properties;

namespace WFADVLD
{
    public partial class frmTests : Form
    {
        private int _LDLApplication = -1;
        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        private DataTable _dtTestsAppointments;

        public frmTests(int LDLApp, clsTestTypes.enTestType type)
        {
            InitializeComponent();

            _LDLApplication = LDLApp;
            _TestType = type;
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestTypes.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestTypes.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestTypes.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void frmTests_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            ctrlDLApplicationInfo1.LoadByLDLApplicationId(_LDLApplication);

            _dtTestsAppointments = clsTestAppointments.GetApplicationTestAppointmentsPerTestType(_LDLApplication, _TestType);

            dgvLicenseTestAppointments.DataSource = _dtTestsAppointments;   

            lblRecordsCount.Text = _dtTestsAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLDLApplication LApplication = clsLDLApplication.FindById(_LDLApplication);

            if (LApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = LApplication.GetLastTestPerTestType(_TestType);

            if (LastTest != null && LastTest.TestResult)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm = new frmScheduleTest(_LDLApplication, _TestType);
            frm.ShowDialog();

            frmTests_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LDLApplication, _TestType, (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmTests_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value, _TestType);
            frm.ShowDialog();

            frmTests_Load(null, null);
        }
    }
}
