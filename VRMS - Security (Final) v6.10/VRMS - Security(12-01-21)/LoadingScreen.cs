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
    public partial class LoadingScreen : Form
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }

        int seconds;

        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            seconds = 5;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = seconds--.ToString();
            if (seconds < 0)
            {
                timer1.Stop();
                this.Hide();
            }
        }
    }
}
