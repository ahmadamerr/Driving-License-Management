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

namespace WFADVLD.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _user;
        private int _id = -1;

        public int id { get { return _id; } }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void _ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();

            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_user.PersonID);

            lblUserID.Text = _user.UserID.ToString();
            lblUserName.Text = _user.UserName.ToString();

            lblIsActive.Text = (_user.IsActive) ? "Yes" : "No";
        }

        public void LoadUserInfo(int userId)
        {
            _user = clsUser.FindByUserID(userId);

            if (_user == null)
            {
                MessageBox.Show("No User with UserID = " + userId.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _ResetUserInfo();
                return;
            }

            _FillUserInfo();
        }

    }
}
