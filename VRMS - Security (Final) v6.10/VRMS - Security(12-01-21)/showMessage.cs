using System;
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
    public partial class showMessage : Form
    {
        public showMessage()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showMessage_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblAdminID2_Click(object sender, EventArgs e)
        {

        }

        private void pbQCU_Click(object sender, EventArgs e)
        {

        }

    }
}
