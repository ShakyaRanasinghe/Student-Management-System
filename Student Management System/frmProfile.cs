using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmProfile : Form
    {
        private readonly DBConnection db = new DBConnection();
        private readonly string studentEmail;

        public frmProfile(string email)
        {
            InitializeComponent();
            studentEmail = email;
        }


        private void frmProfile_Load(object sender, EventArgs e)
        {
            LoadStudentProfile();
        }



     private void LoadStudentProfile()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            s.StudentID,
                            s.FullName,
                            s.DateOfBirth,
                            s.Gender,
                            s.Email,
                            s.Phone,
                            s.Address,
                            s.CourseID,
                            s.CourseCode,
                            c.CourseName
                        FROM Students s
                        LEFT JOIN Courses c ON s.CourseID = c.CourseID
                        WHERE LOWER(s.Email) = LOWER(@Email);";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", studentEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    txtFullName.Text = reader["FullName"]?.ToString() ?? "";
                    txtEmail.Text = reader["Email"]?.ToString() ?? "";
                    txtGender.Text = reader["Gender"]?.ToString() ?? "";
                    txtPhone.Text = reader["Phone"]?.ToString() ?? "";
                    txtCourse.Text = reader["CourseName"]?.ToString() ?? "";
                    txtCourseCode.Text = reader["CourseCode"]?.ToString() ?? "";
                    txtAddress.Text = reader["Address"]?.ToString() ?? "";

                    if (reader["DateOfBirth"] != DBNull.Value)
                        txtDOB.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    else
                        txtDOB.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message);
            }
        }


        private void lblFullName_Click(object sender, EventArgs e)
        {
            
        }

        private void lblFullNameTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtCourse_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_Click(object sender, EventArgs e)
        {

        }

        private void txtPhone_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_Click(object sender, EventArgs e)
        {

        }

        private void txtGender_Click(object sender, EventArgs e)
        {

        }

        private void txtDOB_Click(object sender, EventArgs e)
        {

        }

        private void lblCourseTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblAddressTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblPhoneTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblEmailTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblGenderTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblDOBTitle_Click(object sender, EventArgs e)
        {

        }
    }
}

