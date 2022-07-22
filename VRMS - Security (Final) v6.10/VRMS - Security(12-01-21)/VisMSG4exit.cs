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
    public partial class VisMSG4exit : Form
    {
        public VisMSG4exit()
        {
            InitializeComponent();
        }

        private void VisMSG4exit_Load(object sender, EventArgs e)
        {
            lblChoice.Text = "OK";
            Visitor show = new Visitor();
            show.lblAns.Text = lblChoice.Text;
            this.TopMost = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            lblChoice.Text = "OK";
            Visitor show = new Visitor();
            show.lblAns.Text = lblChoice.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblChoice.Text = "CANCEL";
            Visitor show = new Visitor();
            show.lblAns.Text = lblChoice.Text;
            this.Close();
        }
    }
}
