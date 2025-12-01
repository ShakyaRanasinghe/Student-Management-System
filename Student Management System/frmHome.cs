using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            // timed splash screen
            Timer t = new Timer();
            t.Interval = 3000; // 3 seconds
            t.Tick += (s, ev) => {
                t.Stop();
                new frmLogin().Show();
                this.Hide();
            };
            t.Start();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }
    }
}
