using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonId = -1;

        public frmShowPersonLicenseHistory(int PersonId)
        {
            InitializeComponent();

            _PersonId = PersonId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonId != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonId);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlDriversLicenses1.LoadInfoByPerson(_PersonId);

                return;
            }
            ctrlPersonCardWithFilter1.Enabled = false;
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonId = obj;

            if (_PersonId == -1) ctrlDriversLicenses1.Clear();
            else ctrlDriversLicenses1.LoadInfoByPerson(obj);
        }
    }
}
