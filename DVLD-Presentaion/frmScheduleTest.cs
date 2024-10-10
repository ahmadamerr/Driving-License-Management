using DVLD;
using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmScheduleTest : Form
    {
        private int _LApplicationId = -1;
        private int _AppointmentId = -1;
        private clsTestTypes.enTestType TestType = clsTestTypes.enTestType.VisionTest;

        private void FormName()
        {
            switch (TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    this.Text = "Vision Test";
                    break;
                case clsTestTypes.enTestType.WrittenTest:
                    this.Text = "Written Test";
                    break;
                case clsTestTypes.enTestType.StreetTest:
                    this.Text = "Street Test";
                    break;
            }
        }

        public frmScheduleTest(int LDLApplicationId, clsTestTypes.enTestType testType, int appointmentId = -1)
        {
            InitializeComponent();

            TestType = testType;
            _LApplicationId = LDLApplicationId;
            _AppointmentId = appointmentId;

            FormName();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestType = TestType;
            ctrlScheduleTest1.LoadInfo(_LApplicationId, _AppointmentId);
        }
    }
}
