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
    public partial class frmUpdateTestType : Form
    {
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private clsTestTypes _TestType;

        public frmUpdateTestType(clsTestTypes.enTestType id)
        {
            InitializeComponent();

            _TestTypeID = id;
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestTypes.Find(_TestTypeID);

            if (_TestType != null)
            {
                lblID.Text = _TestTypeID.ToString();
                txtTitle.Text = _TestType.title;
                txtDescription.Text = _TestType.description;
                txtFees.Text = _TestType.fees.ToString();
            }
            else 
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _TestType.fees = Convert.ToSingle(txtFees.Text);
            _TestType.title = txtTitle.Text;
            _TestType.description = txtDescription.Text;    
            
            if (_TestType.Save())
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            };
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            };
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            };


            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };
        }
    }
}
