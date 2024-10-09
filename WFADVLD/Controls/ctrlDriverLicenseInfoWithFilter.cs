using DVLD;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WFADVLD.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;

            if (handler != null) handler(LicenseID);
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get { return _FilterEnabled; }

            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseId = -1;

        public int LicenseId
        {
            get { return ctrlDriverLicenseInfo1.LicenseId; }
        }

        public clsLicense SelectedLicense
        {
            get { return ctrlDriverLicenseInfo1.License; }
        }

        public void LoadData(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadData(LicenseID);
            _LicenseId = LicenseID;

            if (OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(LicenseID);
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));

            if (e.KeyChar == (char)13) btnFind.PerformClick();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else errorProvider1.SetError(txtLicenseID, "");
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();

                return;
            }

            _LicenseId = int.Parse(txtLicenseID.Text.Trim());

            LoadData(_LicenseId);
        }
    }
}
