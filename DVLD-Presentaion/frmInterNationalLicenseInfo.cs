using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmInterNationalLicenseInfo : Form
    {
        private int _InternationalId = -1;

        public frmInterNationalLicenseInfo(int InternationalLicenseId)
        {
            InitializeComponent();

            _InternationalId = InternationalLicenseId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmInterNationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseInfo1.LoadInfo(_InternationalId);
        }
    }
}
