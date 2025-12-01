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
    public partial class frmMarks : Form
    {
        DBConnection db = new DBConnection();

        public frmMarks()
        {
            InitializeComponent();
        }



        private void LoadCourses()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT CourseID, CourseName FROM Courses", conn);
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



        private void cboCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCourseCodes();
            LoadStudents();
        }

        private void LoadStudents()
        {
            if (cboCourse.SelectedValue == null || cboCourseCode.Text == "")
                return;

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"
                        SELECT StudentID, FullName 
                        FROM Students 
                        WHERE CourseID = @CID AND CourseCode = @Code", conn);

                    cmd.Parameters.AddWithValue("@CID", cboCourse.SelectedValue);
                    cmd.Parameters.AddWithValue("@Code", cboCourseCode.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMarks.DataSource = dt;

                   
                    if (!dgvMarks.Columns.Contains("MarksObtained"))
                        dgvMarks.Columns.Add("MarksObtained", "Marks Obtained");

                    if (!dgvMarks.Columns.Contains("Grade"))
                        dgvMarks.Columns.Add("Grade", "Grade");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboCourse.SelectedValue == null || cboCourseCode.Text == "")
            {
                MessageBox.Show("Select Course and Course Code first!");
                return;
            }

            if (txtSubject.Text == "")
            {
                MessageBox.Show("Enter subject name!");
                return;
            }

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgvMarks.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Cells["MarksObtained"].Value == null) continue;
                        if (row.Cells["Grade"].Value == null) continue;

                        SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO Marks
                               (StudentID, CourseID, CourseCode, Subject, MarksObtained, Grade)
                            VALUES
                               (@SID, @CID, @Code, @Sub, @Marks, @Grade)", conn);

                        cmd.Parameters.AddWithValue("@SID",
                            Convert.ToInt32(row.Cells["StudentID"].Value));
                        cmd.Parameters.AddWithValue("@CID",
                            Convert.ToInt32(cboCourse.SelectedValue));
                        cmd.Parameters.AddWithValue("@Code",
                            cboCourseCode.Text);
                        cmd.Parameters.AddWithValue("@Sub",
                            txtSubject.Text.Trim());
                        cmd.Parameters.AddWithValue("@Marks",
                            Convert.ToInt32(row.Cells["MarksObtained"].Value));
                        cmd.Parameters.AddWithValue("@Grade",
                            row.Cells["Grade"].Value.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Marks saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving marks: " + ex.Message);
            }
        }

        private void btnAutoGrade_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvMarks.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["MarksObtained"].Value == null) continue;

                    int marks = Convert.ToInt32(row.Cells["MarksObtained"].Value);

                    string grade =
                        marks >= 75 ? "A" :
                        marks >= 65 ? "B" :
                        marks >= 55 ? "C" :
                        marks >= 45 ? "D" : "F";

                    row.Cells["Grade"].Value = grade;
                }

                MessageBox.Show("Grades calculated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Auto grade error: " + ex.Message);
            }
        }

        private void dgvMarks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboCourseCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSubject.Clear();
            dgvMarks.DataSource = null;

            MessageBox.Show("Cleared form.");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMarks_Load_1(object sender, EventArgs e)
        {
            LoadCourses();
        }
    }
}