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
    public partial class frmChangePassword : Form
    {
        DBConnection db = new DBConnection();
        SqlConnection conn;

        private string userEmail; 

        public frmChangePassword(string email)
        {
            InitializeComponent();
            userEmail = email.Trim();
            txtEmail.Text = userEmail;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            lblEmail.Text = "Email: " + userEmail;
        }


        private void lblFullName_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmChangePassword_Load_1(object sender, EventArgs e)
        {
            lblEmail.Text = "Email: " + userEmail;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (txtCurrentPassword.Text.Trim() == "" ||
         txtNewPassword.Text.Trim() == "" ||
         txtConfirmPassword.Text.Trim() == "")
            {
                MessageBox.Show("All fields are required.", "Error");
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New passwords do not match.", "Error");
                return;
            }

            try
            {
                using (conn = db.GetConnection())
                {
                    conn.Open();

                    
                    SqlCommand cmd = new SqlCommand(
                        "SELECT Password FROM Users WHERE Email = @Email", conn);

                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    string dbPassword = Convert.ToString(cmd.ExecuteScalar());

                    if (dbPassword == null)
                    {
                        MessageBox.Show("Email not found in database: " + userEmail);
                        return;
                    }

                    if (dbPassword != txtCurrentPassword.Text.Trim())
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    
                    SqlCommand update = new SqlCommand(
                        "UPDATE Users SET Password = @P WHERE Email=@Email", conn);

                    update.Parameters.AddWithValue("@Email", userEmail);
                    update.Parameters.AddWithValue("@P", txtNewPassword.Text.Trim());
                    update.ExecuteNonQuery();

                    MessageBox.Show("Password updated successfully!", "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
