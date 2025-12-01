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
    public partial class frmManageCourses : Form
    {
        DBConnection db = new DBConnection();
        SqlConnection conn;

        public frmManageCourses()
        {
            InitializeComponent();
            LoadCourses();
        }

        private void LoadCourses()
        {
            conn = db.GetConnection();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Courses", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvCourses.DataSource = dt;
            conn.Close();
        }


    
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void lblCourseCode_Click(object sender, EventArgs e)
        {
     
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Clear all input fields to allow the user to add a new course
            txtCourseName.Clear();
            txtCourseCode.Clear();
            cboDuration.SelectedIndex = -1; // Reset the combo box selection
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn = db.GetConnection();
                conn.Open();

                // Insert a new course into the database
                string query = "INSERT INTO Courses (CourseName, CourseCode, Duration) VALUES (@Name, @Code, @Duration)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", txtCourseName.Text);
                cmd.Parameters.AddWithValue("@Code", txtCourseCode.Text);
                cmd.Parameters.AddWithValue("@Duration", cboDuration.Text);
                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Course added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload the courses to reflect the new addition
                LoadCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving course: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmManageCourses_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a course to update.");
                return;
            }

            int courseId = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CourseID"].Value);

            try
            {
                conn = db.GetConnection();
                conn.Open();

                string query =
                    @"UPDATE Courses SET 
                        CourseName=@Name,
                        CourseCode=@Code,
                        Duration=@Duration
                      WHERE CourseID=@ID";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", txtCourseName.Text);
                cmd.Parameters.AddWithValue("@Code", txtCourseCode.Text);
                cmd.Parameters.AddWithValue("@Duration", cboDuration.Text);
                cmd.Parameters.AddWithValue("@ID", courseId);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Course updated successfully!");
                LoadCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating course: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a course to delete.");
                return;
            }

            int courseId = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CourseID"].Value);

            if (MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                conn = db.GetConnection();
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Courses WHERE CourseID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", courseId);
                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Course deleted.");
                LoadCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting course: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn = db.GetConnection();
                conn.Open();

                string query =
                    @"SELECT * FROM Courses
                      WHERE CourseName LIKE @search
                         OR CourseCode LIKE @search
                         OR Duration LIKE @search";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvCourses.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        private void dgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCourseName.Text = dgvCourses.Rows[e.RowIndex].Cells["CourseName"].Value.ToString();
                txtCourseCode.Text = dgvCourses.Rows[e.RowIndex].Cells["CourseCode"].Value.ToString();
                cboDuration.Text = dgvCourses.Rows[e.RowIndex].Cells["Duration"].Value.ToString();
            }
        }
    }
}
