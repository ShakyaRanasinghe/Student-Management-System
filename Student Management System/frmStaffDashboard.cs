using System;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmStaffDashboard : Form
    {
        string staffName;

        public frmStaffDashboard(string name)
        {
            InitializeComponent();
            staffName = name;
        }

  
        private void btnAttendance_Click(object sender, EventArgs e)
        {
           
            frmAttendance attendanceForm = new frmAttendance();
            attendanceForm.Show();
        }

        private void btnMarks_Click(object sender, EventArgs e)
        {
                
            frmMarks marksForm = new frmMarks();
            marksForm.Show();
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            frmManageStudents manageStudentsForm = new frmManageStudents();
            manageStudentsForm.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            
            frmReports reportsForm = new frmReports();
            reportsForm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
          
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
            this.Close();
        }

        private void frmStaffDashboard_Load_1(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {staffName} (Staff)";
        }

        private void btnAttendance_Click_1(object sender, EventArgs e)
        {
            frmAttendance attendanceForm = new frmAttendance();
            attendanceForm.Show();
        }

        private void btnMarks_Click_1(object sender, EventArgs e)
        {
            frmMarks marksForm = new frmMarks();
            marksForm.Show();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
        }
    }
}
