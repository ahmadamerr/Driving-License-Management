using DVLD;
using System;
using System.IO;
using System.Windows.Forms;
using WFADVLD.Properties;

namespace WFADVLD
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;

        private int _id = -1;

        public int id { get { return _id; } } 

        public clsPerson selectedPerson{get{ return _Person; } }

        public void LoadPersonInfo(int personID) 
        {
            _Person = clsPerson.Find(personID);
            
            if (_Person == null)
            {
                ResetPersonInfo();

                MessageBox.Show("No Person with PersonID = " + personID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string nationalNo)
        {
            _Person = clsPerson.Find(nationalNo);

            if (_Person == null)
            {
                ResetPersonInfo();

                MessageBox.Show("No Person with NationalNO = " + nationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();
        }

        private void _FillPersonInfo() 
        {
            llEditPersonInfo.Enabled = true;
            _id = _Person.ID;
            lblPersonID.Text = _Person.ID.ToString();
            lblNationalNo.Text = _Person.NationalNum;
            lblFullName.Text = _Person.FullName;
            lblGendor.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblCountry.Text = clsCountry.GetCountryName(_Person.NationalityCountryID);
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
        }

        private void _LoadPersonImage() 
        {
            string ImagePath = _Person.ImagePath;

            if (ImagePath == "") 
            {
                if (_Person.Gender == 0)
                    pbPersonImage.Image = Resources.Male_512;
                else
                    pbPersonImage.Image = Resources.Female_512;
            }

            else 
            { 
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ResetPersonInfo()
        {
            _id = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;

        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEdit frm = new frmAddEdit(_id);
            frm.ShowDialog();

            LoadPersonInfo(_id);
        }
    }
}
