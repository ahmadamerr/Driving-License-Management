using DVLD;
using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
            }
        }


        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;

                groupBox1.Enabled = value;
            }
        }

        public int PersonID
        {
            get { return ctrlPersonCard1.id; }
        }

        public clsPerson PersonInfo
        {
            get { return ctrlPersonCard1.selectedPerson; }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int personId)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = personId.ToString();

            FindPerson();
        }

        private void FindPerson()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text)); break;
                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text); break;
            }

            if (OnPersonSelected != null)
                OnPersonSelected(ctrlPersonCard1.id);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindPerson();
        }

        private void DataBackEvent(object sender, int PersonId)
        {
            cbFilterBy.SelectedIndex = 1;

            txtFilterValue.Text = PersonId.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonId);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) btnFind.PerformClick();

            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }
    }
}
