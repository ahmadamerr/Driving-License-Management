using DVLD;
using System.Windows.Forms;

namespace WFADVLD.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplication _Application;

        private int _id = -1;

        public int id { get { return _id; } }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void ResetApplicationInfo()
        {
            _id = -1;

            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
        }

        public void LoadData(int id)
        {
            _Application = clsApplication.Find(id);

            if (_Application == null)
            {
                ResetApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + _id.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else FillData();
        }

        private void FillData()
        {
            _id = _Application.ApplicationID;

            lblApplicationID.Text = _id.ToString();
            lblStatus.Text = _Application.StatusText;
            lblFees.Text = _Application.PaidFees.ToString();
            lblApplicant.Text = _Application.ApplicationFullName;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblCreatedByUser.Text = _Application.CreatedByUserInfo.UserName;
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicationPersonID);
            frm.ShowDialog();

            LoadData(_id);
        }
    }
}
