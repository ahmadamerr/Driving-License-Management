using DVLD;
using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        private clsTestTypes.enTestType _TestType;

        private int _TestID = -1;
        private clsTest _Test;

        public frmTakeTest(int appointmentId, clsTestTypes.enTestType testType)
        {
            InitializeComponent();

            _AppointmentID = appointmentId;
            _TestType = testType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestType;

            ctrlScheduledTest1.LoadData(_AppointmentID);

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int _TestID = ctrlScheduledTest1.TestID;

            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
            }

            else
                _Test = new clsTest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            _Test.TestID = _TestID;
            _Test.TestAppointmentID = _AppointmentID;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.currentUser.UserID;
            _Test.TestResult = rbPass.Checked;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
