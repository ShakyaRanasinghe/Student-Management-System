using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmStudentMarks : Form
    {
        private readonly DBConnection db = new DBConnection();
        private readonly string studentEmail;

        public frmStudentMarks(string email)
        {
            InitializeComponent();
            studentEmail = email;
        }

        private void frmStudentMarks_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }


        private void LoadCourses()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT CourseID, CourseName FROM Courses", conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboCourse.DisplayMember = "CourseName";
                    cboCourse.ValueMember = "CourseID";
                    cboCourse.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message);
            }
        }


        private void LoadCourseCodes()
        {
            if (cboCourse.SelectedValue == null) return;

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT CourseCode FROM Courses WHERE CourseID = @CID", conn);

                    cmd.Parameters.AddWithValue("@CID", cboCourse.SelectedValue);

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    cboCourseCode.DisplayMember = "CourseCode";
                    cboCourseCode.ValueMember = "CourseCode";
                    cboCourseCode.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading course codes: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvStudentMarks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboCourse_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadCourseCodes();
        }

        private void cboCourseCode_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadMarks();
        }



        private void LoadMarks()
        {
            if (cboCourse.SelectedValue == null || cboCourseCode.Text == "")
                return;

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    // Step 1 – Find StudentID using Email
                    int studentId;

                    using (SqlCommand cmdFind = new SqlCommand(
                        "SELECT StudentID FROM Students WHERE Email = @Email", conn))
                    {
                        cmdFind.Parameters.AddWithValue("@Email", studentEmail);
                        object result = cmdFind.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Student not found.");
                            return;
                        }

                        studentId = Convert.ToInt32(result);
                    }

                    // Step 2 – Load marks
                    string query = @"
                        SELECT Subject, MarksObtained, Grade
                        FROM Marks
                        WHERE StudentID = @SID
                          AND CourseID = @CID
                          AND CourseCode = @Code";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SID", studentId);
                        cmd.Parameters.AddWithValue("@CID", cboCourse.SelectedValue);
                        cmd.Parameters.AddWithValue("@Code", cboCourseCode.Text);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvStudentMarks.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading marks: " + ex.Message);
            }
        }
    }
}