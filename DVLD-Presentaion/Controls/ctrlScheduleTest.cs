using DVLD;
using System;
using System.Windows.Forms;
using WFADVLD.Properties;

namespace WFADVLD.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        private clsLDLApplication _LApplication;
        private int _LApplicationId = -1;
        private clsTestAppointments _TestAppointment;
        private int _TestAppointmentId = -1;

        public clsTestTypes.enTestType TestType
        {
            get { return _TestType; }

            set 
            {
                _TestType = value;

                switch ( _TestType)
                {
                    case clsTestTypes.enTestType.VisionTest: 
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }

                }
            }
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointments.Find(_TestAppointmentId); ;

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentId.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationId == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationId.ToString();
            }

            return true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLDLApplication.IsThereAnActiveScheduledTest(_LApplicationId, _TestType))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }

            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblUserMessage.Visible = false;

                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    if (!_LApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

                case clsTestTypes.enTestType.StreetTest:
                    if (!_LApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

            }
            return true;

        }

        public void LoadInfo(int LDLApplicationId, int AppointmentId = -1)
        {
            if (AppointmentId == -1) _Mode = enMode.AddNew;
            else _Mode = enMode.Update;

            _LApplicationId = LDLApplicationId;
            _TestAppointmentId = AppointmentId;
            _LApplication = clsLDLApplication.FindById(LDLApplicationId);

            if (_LApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LApplicationId.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnSave.Enabled = false;

                return;
            }

            if (_LApplication.DoesAttendTestType(_TestType))
                _CreationMode = enCreationMode.RetakeTestSchedule;

            else _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fee.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }

            lblLocalDrivingLicenseAppID.Text = _LApplicationId.ToString();
            lblFullName.Text = _LApplication.ApplicationFullName;
            lblTrial.Text = _LApplication.TotalTrialsPerTest(_TestType).ToString();
            lblDrivingClass.Text = _LApplication.LicenseClassInfo.ClassName;

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypes.Find(_TestType).fees.ToString();
                clsTestAppointments test = clsTestAppointments.GetLastTestAppointment(_LApplicationId, _TestType);
                byte time = (byte)_TestType;

                dtpTestDate.MinDate = (test != null) ? test.AppointmentDate.AddDays(1) : (time - 1 == 0) ? DateTime.Now : clsTestAppointments.GetLastTestAppointment(_LApplicationId, (clsTestTypes.enTestType) time - 1).AppointmentDate.AddDays(1);
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointments();
            }
            else if (!_LoadTestAppointmentData()) return;

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint()) return;
            if (!_HandleAppointmentLockedConstraint()) return;
            if (!_HandlePrviousTestConstraint()) return;
        }

        private bool _HandleRetakeApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();

                Application.ApplicationPersonID = _LApplication.ApplicationPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fee;
                Application.CreatedByUserID = clsGlobal.currentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationId = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationId = Application.ApplicationID;
            }

            return true;
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication()) return;

            _TestAppointment.TestTypeId = _TestType;
            _TestAppointment.LDLApplicationId = _LApplicationId;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserId = clsGlobal.currentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
