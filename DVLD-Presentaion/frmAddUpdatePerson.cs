using DVLD;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WFADVLD.Properties;

namespace WFADVLD
{
    public partial class frmAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);


        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };

        public enum enGender { Male = 0, Female = 1 };


        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;

        public frmAddEdit()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddEdit(int PersonID) 
        {
            InitializeComponent();

            _PersonID = PersonID;

            _Mode = enMode.Update;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"].ToString());
            }
        }

        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew) 
            {
                _Person = new clsPerson();

                lblTitle.Text = "Add New Person";
            }
            else lblTitle.Text = "Update Person";

            if (rbMale.Checked)
                pbImage.Image = Resources.Male_512;
            if (rbFemale.Checked)
                pbImage.Image = Resources.Female_512;

            llRemoveImage.Enabled = (pbImage.ImageLocation != null);

            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-90);

            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private bool _HandlePersonImage() 
        {
            if (_Person.ImagePath != pbImage.ImageLocation) 
            {
                if (_Person.ImagePath != string.Empty) 
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (Exception) 
                    {

                    }
                }
            }

            if (pbImage.ImageLocation != null) 
            {
                string destFile = pbImage.ImageLocation.Trim();

                if (clsUtil.CopyImageToProjectImagesFolder(ref destFile)) 
                {
                    pbImage.ImageLocation = destFile;

                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage()) return;

            int nationalityCountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNum = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dateTimePicker1.Value;
            if (rbMale.Checked) _Person.Gender = (byte)enGender.Male;
            else _Person.Gender = (byte)enGender.Female;
            _Person.NationalityCountryID = nationalityCountryID;
            if (pbImage.ImageLocation != null) _Person.ImagePath = pbImage.ImageLocation;
            else _Person.ImagePath = String.Empty;


            if (_Person.Save()) 
            {
                lblPersonID.Text = _Person.ID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.ID);
            }
            else MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _LoadData() 
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNum;
            dateTimePicker1.Value = _Person.DateOfBirth;
            if (_Person.Gender == 0) rbMale.Checked = true;
            else rbFemale.Checked = true;
            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.countryInfo.CountryName);
            if (_Person.ImagePath != null) pbImage.ImageLocation = _Person.ImagePath;

            llRemoveImage.Visible = (_Person.ImagePath != "");
            llRemoveImage.Enabled = (_Person.ImagePath != "");
        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update) _LoadData();
        }

        private void rbMale_Clicked(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.Male_512;
        }

        private void rbFemale_Clicked(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.Female_512;
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;

            if (rbMale.Checked) pbImage.Image = Resources.Male_512;
            else pbImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            pbImage.ImageLocation = null;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pbImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                llRemoveImage.Enabled = true;
            }
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e) 
        {
            TextBox temp = (TextBox)sender;

            if (string.IsNullOrEmpty(temp.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(temp, "This Field Is Required");
            }
            else errorProvider1.SetError(temp, string.Empty);
        }

        private void ValidateNationalNum(object sender, CancelEventArgs e) 
        {

            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim())) 
            {
                e.Cancel = true;

                errorProvider1.SetError(txtNationalNo, "This Field Is Required");
                return;
            }
            else errorProvider1.SetError(txtNationalNo, string.Empty);

            if (txtNationalNo.Text != _Person.NationalNum && clsPerson.IsPersonExist(txtNationalNo.Text)) 
            {
                e.Cancel = true;

                errorProvider1.SetError(txtNationalNo, "This National Number Is Already Used For Another Person");
            }
            else errorProvider1.SetError(txtNationalNo, string.Empty);
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e) 
        {
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtEmail, "Invalid Email Address Format");
            }
            else errorProvider1.SetError(txtEmail, string.Empty);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
    }
}
