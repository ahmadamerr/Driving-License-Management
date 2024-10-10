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
    public partial class frmLDLApplicationInfo : Form
    {
        private int _ApplicationId = -1;

        public frmLDLApplicationInfo(int ApplicationId)
        {
            InitializeComponent();

            _ApplicationId = ApplicationId; 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmLDLApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDLApplicationInfo1.LoadByLDLApplicationId(_ApplicationId);
        }
    }
}
