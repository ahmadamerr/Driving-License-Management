using DVLD;
using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmLicenseInfo : Form
    {
        private int _LicenseId = -1;


        public frmLicenseInfo(int LicenseId)
        {
            InitializeComponent();

            _LicenseId = LicenseId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadData(_LicenseId);
        }
    }
}
