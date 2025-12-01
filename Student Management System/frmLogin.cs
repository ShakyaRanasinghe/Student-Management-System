using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmLogin : Form
    {
        DBConnection db = new DBConnection();
        SqlConnection conn;

        public frmLogin()
        {
            InitializeComponent();
        }


        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please enter Email and Password");
                return;
            }

            conn = db.GetConnection();
            conn.Open();
            string query = "SELECT Role, FullName FROM Users WHERE Email=@Email AND Password=@Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string role = dr["Role"].ToString();
                string name = dr["FullName"].ToString();
                MessageBox.Show($"Welcome, {name}");

                if (role == "Admin")
                    new frmAdminDashboard(name).Show();
                else if (role == "Staff")
                    new frmStaffDashboard(name).Show();
                else
                    new frmStudentDashboard(name, txtEmail.Text).Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid email or password");
            }

            conn.Close();
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
