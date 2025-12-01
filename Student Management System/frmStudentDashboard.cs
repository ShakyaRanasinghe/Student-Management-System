using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmStudentDashboard : Form
    {
        private string studentName;
        private string studentEmail;

        public frmStudentDashboard(string fullName, string email)
        {
            InitializeComponent();
            studentName = fullName;
            studentEmail = email;
        }

        private void frmStudentDashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {studentName}";
        }

        private void btnProfile_Click_1(object sender, EventArgs e)
        {
            frmProfile profileForm = new frmProfile(studentEmail);
            profileForm.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            frmStudentAttendance attendanceForm = new frmStudentAttendance(studentEmail);
            attendanceForm.Show();
        }

        private void btnMarks_Click(object sender, EventArgs e)
        {
            frmStudentMarks marksForm = new frmStudentMarks(studentEmail);
            marksForm.Show();
        }

        private void frmStudentDashboard_Load_1(object sender, EventArgs e)
        {
            // This function is redundant as `frmStudentDashboard_Load` already handles the load event.
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