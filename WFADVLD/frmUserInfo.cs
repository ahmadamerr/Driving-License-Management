﻿using System;
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
    public partial class frmUserInfo : Form
    {
        public frmUserInfo(int UserID)
        {
            InitializeComponent();

            ctrlUserCard1.LoadUserInfo(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
    }
}