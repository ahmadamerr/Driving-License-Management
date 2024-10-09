using DVLD;
using System.IO;
using System.Windows.Forms;
using WFADVLD.Properties;

namespace WFADVLD.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseId = -1;

        private clsLicense _License;

        public int LicenseId { get { return _LicenseId; } }

        public clsLicense License { get { return _License; } }

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void LoadPersonImage()
        {
            string FilePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (FilePath != "")
            {
                if (File.Exists(FilePath)) pbPersonImage.Load(FilePath);
                else
                    MessageBox.Show("Could not find this image: = " + FilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_License.DriverInfo.PersonInfo.Gender == 0)
                    pbPersonImage.Image = Resources.Male_512;
                else
                    pbPersonImage.Image = Resources.Female_512;
            }
        }

        public void LoadData(int LicenseId)
        {
            _LicenseId = LicenseId;

            _License = clsLicense.Find(LicenseId);

            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseId.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseId = -1;
                return;
            }

            LoadPersonImage();

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassIfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNum;
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
        }
    }
}
