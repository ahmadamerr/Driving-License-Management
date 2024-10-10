using DVLD;
using System;
using System.Data;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmAddUpdateLDLApplication : Form
    {
        enum enMode { AddNew = 0, Update = 1};

        private enMode Mode;
        private int _Id = -1;
        private int _PersonId = -1;
        clsLDLApplication _app;
        public frmAddUpdateLDLApplication()
        {
            InitializeComponent();

            Mode = enMode.AddNew;
        }

        public frmAddUpdateLDLApplication(int id)
        {
            InitializeComponent();

            _Id = id;

            Mode = enMode.Update;
        }

        private void FillComboBox()
        {
            DataTable dt = clsLicenseClasses.GetAllLicenseClasses();

            foreach (DataRow row in dt.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }

        private void Reset()
        {
            FillComboBox();

            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";

                _app = new clsLDLApplication();

                ctrlPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fee.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = "???";

                return;
            }

            lblTitle.Text = "Update Local Driving License Application";
            this.Text = "Update Local Driving License Application";

            tpApplicationInfo.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseClassId = clsLicenseClasses.Find(cbLicenseClass.Text).LicenseClassID;

            int ActiveAppId = clsApplication.GetActiveApplicationIDForLicenseClass(_PersonId, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassId);

            if (ActiveAppId != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveAppId, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(_PersonId, LicenseClassId))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _app.ApplicationPersonID = ctrlPersonCardWithFilter1.PersonID; ;
            _app.ApplicationDate = DateTime.Now;
            _app.ApplicationTypeID = 1;
            _app.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _app.LastStatusDate = DateTime.Now;
            _app.PaidFees = Convert.ToSingle(lblFees.Text);
            _app.CreatedByUserID = clsGlobal.currentUser.UserID;
            _app.LicenseClassID = LicenseClassId;

            if (_app.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _app.ApplicationID.ToString();

                Mode = enMode.Update;

                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            _PersonId = PersonID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(PersonID);
        }

        private void LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            _app = clsLDLApplication.FindById(_Id);

            if (_app == null)
            {
                MessageBox.Show("No Application with ID = " + _Id, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_app.ApplicationPersonID);

            lblLocalDrivingLicebseApplicationID.Text = _Id.ToString();
            lblApplicationDate.Text = _app.ApplicationDate.ToString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClasses.Find(_app.LicenseClassID).ClassName); 
            lblFees.Text = _app.PaidFees.ToString();
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;
        }

        private void frmAddUpdateLDLApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void frmAddUpdateLDLApplication_Load(object sender, EventArgs e)
        {
            Reset();

            if (Mode == enMode.Update) LoadData();
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonId = obj;
        }


    }
}
