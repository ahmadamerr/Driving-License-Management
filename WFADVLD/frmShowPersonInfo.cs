using System;
using System.Windows.Forms;

namespace WFADVLD
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int id)
        {
            InitializeComponent();

            ctrlPersonCard1.LoadPersonInfo(id);
        }

        public frmShowPersonInfo(string natinalNo)
        {
            InitializeComponent();

            ctrlPersonCard1.LoadPersonInfo(natinalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

            return;
        }
    }
}
