using DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmUpdateApplication : Form
    {
        private int _personId = -1;
        private clsApplicationTypes _application;

        public frmUpdateApplication(int personId)
        {
            InitializeComponent();
            _personId = personId;
        }

        private void frmUpdateApplication_Load(object sender, EventArgs e)
        {
            _application = clsApplicationTypes.Find(_personId);

            if ( _application != null)
            {
                lblApplicationTypeID.Text = _personId.ToString();

                txtTitle.Text = _application.Name;
                txtFees.Text = _application.Fee.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _application.Fee = Convert.ToSingle(txtFees.Text);
            _application.Name = txtTitle.Text;

            if (_application.Save())
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtTitle, "This Field Cannot Be Empty");
            }
            else
                errorProvider1.SetError(txtTitle, "");
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtTitle, "This Field Cannot Be Empty");
            }
            else
                errorProvider1.SetError(txtTitle, "");

            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtFees, "This Field Accept Only Numbers");
            }
            else
                errorProvider1.SetError(txtFees, "");
        }
    }
}
