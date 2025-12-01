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
    public partial class frmAdminDashboard : Form
    {
        private string adminName;

        
        public frmAdminDashboard(string name)
        {
            InitializeComponent();
            adminName = name; 
        }

        public frmAdminDashboard()
        {
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
          
        }

        // Manage Students
        private void btnManageStudents_Click_1(object sender, EventArgs e)
        {
            frmManageStudents frm = new frmManageStudents();
            frm.Show();
        }

        // Manage Staff
        private void btnManageStaff_Click_1(object sender, EventArgs e)
        {
            frmManageStaff frm = new frmManageStaff();
            frm.Show();
        }

        // Manage Courses
        private void btnManageCourses_Click_1(object sender, EventArgs e)
        {
            frmManageCourses frm = new frmManageCourses();
            frm.Show();
        }

        // Attendance
        private void btnAttendance_Click_1(object sender, EventArgs e)
        {
            frmAttendance frm = new frmAttendance();
            frm.Show();
        }

        // Marks
        private void btnMarks_Click_1(object sender, EventArgs e)
        {
            frmMarks frm = new frmMarks();
            frm.Show();
        }

        // Reports
        private void btnReports_Click_1(object sender, EventArgs e)
        {
            frmReports frm = new frmReports();
            frm.Show();
        }

        // Change Password
        private void btnChangePassword_Click_1(object sender, EventArgs e)
        {

            frmChangePassword frm = new frmChangePassword(adminName);
            frm.Show();
        }

        // Logout
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
