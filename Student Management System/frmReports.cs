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
    public partial class frmReports : Form
    {

            DBConnection db = new DBConnection();

            public frmReports()
            {
                InitializeComponent();
            }

      
            private void frmReports_Load(object sender, EventArgs e)
            {
                LoadCourses();
                dtpStart.Value = DateTime.Today.AddMonths(-1);
                dtpEnd.Value = DateTime.Today;
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
                if (cboCourse.SelectedValue == null)
                    return;

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
        }

        private void cboCourseCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            LoadReportData();
        }


        private void LoadReportData()
        {
            if (cboCourse.SelectedValue == null || cboCourseCode.Text == "")
                return;

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            s.FullName AS [Student Name], 
                            a.Date AS [Date], 
                            a.Status AS [Attendance]
                        FROM Attendance a
                        INNER JOIN Students s ON s.StudentID = a.StudentID
                        WHERE 
                            s.CourseID = @CID
                            AND s.CourseCode = @Code
                            AND a.Date BETWEEN @Start AND @End
                        ORDER BY a.Date DESC;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CID", cboCourse.SelectedValue);
                        cmd.Parameters.AddWithValue("@Code", cboCourseCode.Text);
                        cmd.Parameters.AddWithValue("@Start", dtpStart.Value.Date);
                        cmd.Parameters.AddWithValue("@End", dtpEnd.Value.Date);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvReports.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report data: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblSubject_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void dgvReports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
