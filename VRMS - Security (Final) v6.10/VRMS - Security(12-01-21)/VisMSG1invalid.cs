﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRMS___Security__12_01_21_
{
    public partial class VisMSG1invalid : Form
    {
        public VisMSG1invalid()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VisMSG1invalid_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
