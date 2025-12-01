using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmAttendance : Form
    {
        DBConnection db = new DBConnection();
        SqlConnection conn;

        public frmAttendance()
        {
            InitializeComponent();
        }

        private void SetupAttendanceGrid()
        {
            dgvAttendance.Columns.Clear();

            dgvAttendance.Columns.Add("StudentID", "Student ID");
            dgvAttendance.Columns["StudentID"].ReadOnly = true;

            dgvAttendance.Columns.Add("FullName", "Full Name");
            dgvAttendance.Columns["FullName"].ReadOnly = true;

            DataGridViewComboBoxColumn statusCol = new DataGridViewComboBoxColumn();
            statusCol.HeaderText = "Status";
            statusCol.Name = "Status";
            statusCol.Items.Add("Present");
            statusCol.Items.Add("Absent");

            dgvAttendance.Columns.Add(statusCol);
        }


        private void LoadCourseCodes()
        {
            try
            {
                if (cboCourse.SelectedValue == null || cboCourse.SelectedValue is DataRowView)
                    return;

                using (conn = db.GetConnection())
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT CourseCode FROM Courses WHERE CourseID = @CID", conn);

                    da.SelectCommand.Parameters.AddWithValue("@CID", (int)cboCourse.SelectedValue);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboCourseCode.DataSource = dt;
                    cboCourseCode.DisplayMember = "CourseCode";
                    cboCourseCode.ValueMember = "CourseCode";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading course codes: " + ex.Message);
            }
        }
        private void LoadCourses()
        {
            try
            {
                using (conn = db.GetConnection())
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT CourseID, CourseName FROM Courses", conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboCourse.SelectedIndexChanged -= cboCourse_SelectedIndexChanged_1;

                    cboCourse.DataSource = dt;
                    cboCourse.DisplayMember = "CourseName";
                    cboCourse.ValueMember = "CourseID";

                    cboCourse.SelectedIndexChanged += cboCourse_SelectedIndexChanged_1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message);
            }
        }
        private void LoadStudentsForCourse()
        {
            try
            {
                if (cboCourse.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid course.");
                    return;
                }

                using (conn = db.GetConnection())
                {
                    conn.Open();
                    dgvAttendance.Rows.Clear();

                    SqlCommand cmd = new SqlCommand(
                        @"SELECT s.StudentID, s.FullName,
                                 ISNULL(a.Status, 'Absent') AS Status
                          FROM Students s
                          LEFT JOIN Attendance a 
                            ON s.StudentID = a.StudentID AND a.Date = @Date
                          WHERE s.CourseID = @CourseID", conn);

                    
                    int courseId = Convert.ToInt32(cboCourse.SelectedValue);
                    DateTime selectedDate = dtpDate.Value.Date;

                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.Parameters.AddWithValue("@Date", selectedDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        MessageBox.Show($"No students found for CourseID: {courseId} on Date: {selectedDate.ToShortDateString()}.");
                        return;
                    }

                    while (reader.Read())
                    {
                        dgvAttendance.Rows.Add(
                            reader["StudentID"],
                            reader["FullName"],
                            reader["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }




        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadStudentsForCourse();
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvAttendance.Rows.Count == 0)
            {
                MessageBox.Show("No attendance data to save.");
                return;
            }

            try
            {
                using (conn = db.GetConnection())
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgvAttendance.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int sid = Convert.ToInt32(row.Cells["StudentID"].Value);
                        string status = row.Cells["Status"].Value.ToString();

                        SqlCommand cmd = new SqlCommand(
                            @"IF EXISTS (SELECT 1 FROM Attendance WHERE StudentID=@SID AND Date=@Date)
                                      UPDATE Attendance SET Status=@Status 
                                      WHERE StudentID=@SID AND Date=@Date
                                  ELSE
                                      INSERT INTO Attendance (StudentID, Date, Status)
                                      VALUES (@SID, @Date, @Status)", conn);

                        cmd.Parameters.AddWithValue("@SID", sid);
                        cmd.Parameters.AddWithValue("@Date", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Status", status);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Attendance saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving attendance: " + ex.Message);
            }
        }

        private void btnAllPresent_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["Status"].Value = "Present";
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvAttendance.Rows.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAttendance_Load_1(object sender, EventArgs e)
        {
            LoadCourses();
            SetupAttendanceGrid();
        }
        private void dgvAttendance_CellContentClick(object sender, EventArgs e)
        {
            
        }

        private void cboCourse_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadCourseCodes();
        }

        private void cboCourseCode_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadStudentsForCourse();
        }
    }
}
