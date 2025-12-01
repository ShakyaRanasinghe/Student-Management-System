using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms; 

namespace Student_Management_System
{
    public partial class frmManageStudents : Form
    {
        DBConnection db = new DBConnection();

        public frmManageStudents()
        {
            InitializeComponent();
        }

        private void frmManageStudents_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadStudents();
            LoadCourseCodes();
        }


        // LOAD COURSES
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
                    cboCourse.DataSource = dt;
                    cboCourse.DisplayMember = "CourseName";
                    cboCourse.ValueMember = "CourseID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message);
            }
        }

        // LOAD COURSE CODES
        private void LoadCourseCodes()
        {
            if (cboCourse.SelectedValue == null) return;
            try
            {
                object selectedValue = cboCourse.SelectedValue;
                // If SelectedValue is a DataRowView, extract the CourseID
                if (selectedValue is DataRowView drv)
                {
                    selectedValue = drv["CourseID"];
                }
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT CourseCode FROM Courses WHERE CourseID = @CID", conn);
                    da.SelectCommand.Parameters.AddWithValue("@CID", selectedValue);
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

        // LOAD STUDENTS GRID
        private void LoadStudents()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            s.StudentID,
                            s.UserID,
                            s.FullName,
                            s.DateOfBirth,
                            s.Gender,
                            s.Email,
                            s.Phone,
                            s.Address,
                            s.CourseID,
                            s.CourseCode
                        FROM Students s";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvStudents.DataSource = dt;
                    if (dgvStudents.Rows.Count > 0)
                        dgvStudents.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }


        // VALIDATION
       
        private bool ValidateStudent()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(cboGender.Text) ||
                cboCourse.SelectedValue == null)
            {
                MessageBox.Show("Please fill ALL fields before saving.",
                    "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateStudent()) return;
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    // 1) Insert into Users
                    string addUser = @"
                        INSERT INTO Users (FullName, Email, Password, Role)
                        VALUES (@FullName, @Email, @Password, 'Student');
                        SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdUser = new SqlCommand(addUser, conn);
                    cmdUser.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmdUser.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmdUser.Parameters.AddWithValue("@Password", txtPassword.Text);
                    int userId = Convert.ToInt32(cmdUser.ExecuteScalar());
                    // 2) Insert into Students
                    string addStudent = @"
                        INSERT INTO Students
                            (UserID, FullName, DateOfBirth, Gender, Email, Phone, Address, CourseID, CourseCode)
                        VALUES
                            (@UserID, @FullName, @DOB, @Gender, @Email, @Phone, @Address, @CourseID, @CourseCode);";
                    SqlCommand cmdStud = new SqlCommand(addStudent, conn);
                    cmdStud.Parameters.AddWithValue("@UserID", userId);
                    cmdStud.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmdStud.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
                    cmdStud.Parameters.AddWithValue("@Gender", cboGender.Text);
                    cmdStud.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmdStud.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmdStud.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmdStud.Parameters.AddWithValue("@CourseID", Convert.ToInt32(cboCourse.SelectedValue));
                    cmdStud.Parameters.AddWithValue("@CourseCode", cboCourseCode.Text);
                    cmdStud.ExecuteNonQuery();
                }
                MessageBox.Show("Student added successfully!", "Success");
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving student: " + ex.Message);
            }
        }

        // =========================
        // NEW BUTTON
        // =========================
        private void btnNew_Click(object sender, EventArgs e)
        {
            txtFullName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            cboGender.SelectedIndex = -1;
            cboCourse.SelectedIndex = -1;
            cboCourseCode.DataSource = null;
            dtpDOB.Value = DateTime.Today;
            txtEmail.Enabled = true;
        }


        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudents.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a student to update.");
                    return;
                }
                DataGridViewRow row = dgvStudents.SelectedRows[0];
                if (row.IsNewRow) return;
                int studentId = Convert.ToInt32(row.Cells["StudentID"].Value);
                int userId = Convert.ToInt32(row.Cells["UserID"].Value);
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    // 1) Update Users
                    string updateUser = @"
                        UPDATE Users SET
                            FullName = @FullName,
                            Email    = @Email,
                            Password = @Password
                        WHERE UserID = @UserID;";
                    SqlCommand cmdUser = new SqlCommand(updateUser, conn);
                    cmdUser.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmdUser.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmdUser.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmdUser.Parameters.AddWithValue("@UserID", userId);
                    cmdUser.ExecuteNonQuery();
                    // 2) Update Students
                    string updateStudent = @"
                        UPDATE Students SET
                            FullName   = @FullName,
                            DateOfBirth= @DOB,
                            Gender     = @Gender,
                            Email      = @Email,
                            Phone      = @Phone,
                            Address    = @Address,
                            CourseID   = @CourseID,
                            CourseCode = @CourseCode
                        WHERE StudentID = @StudentID;";
                    SqlCommand cmdStud = new SqlCommand(updateStudent, conn);
                    cmdStud.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmdStud.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
                    cmdStud.Parameters.AddWithValue("@Gender", cboGender.Text);
                    cmdStud.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmdStud.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmdStud.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmdStud.Parameters.AddWithValue("@CourseID", Convert.ToInt32(cboCourse.SelectedValue));
                    cmdStud.Parameters.AddWithValue("@CourseCode", cboCourseCode.Text);
                    cmdStud.Parameters.AddWithValue("@StudentID", studentId);
                    cmdStud.ExecuteNonQuery();
                }
                MessageBox.Show("Student updated successfully!");
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message);
            }
        }


        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudents.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a student to delete.");
                    return;
                }
                DataGridViewRow row = dgvStudents.SelectedRows[0];
                int studentId = Convert.ToInt32(row.Cells["StudentID"].Value);
                int userId = Convert.ToInt32(row.Cells["UserID"].Value);
                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this student and their user account?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    // 1) Delete from Attendance (stu table)
                    string delAttendance = "DELETE FROM Attendance WHERE StudentID = @SID;";
                    SqlCommand cmdAtt = new SqlCommand(delAttendance, conn);
                    cmdAtt.Parameters.AddWithValue("@SID", studentId);
                    cmdAtt.ExecuteNonQuery();

                    // 2) Delete from Marks (stu table)
                    string delMarks = "DELETE FROM Marks WHERE StudentID = @SID;";
                    SqlCommand cmdMarks = new SqlCommand(delMarks, conn);
                    cmdMarks.Parameters.AddWithValue("@SID", studentId);
                    cmdMarks.ExecuteNonQuery();

                    // 3) Delete from Students
                    string delStudent = "DELETE FROM Students WHERE StudentID = @SID;";
                    SqlCommand cmdStud = new SqlCommand(delStudent, conn);
                    cmdStud.Parameters.AddWithValue("@SID", studentId);
                    cmdStud.ExecuteNonQuery();

                    // 4) Delete from Users
                    string delUser = "DELETE FROM Users WHERE UserID = @UID;";
                    SqlCommand cmdUser = new SqlCommand(delUser, conn);
                    cmdUser.Parameters.AddWithValue("@UID", userId);
                    cmdUser.ExecuteNonQuery();
                }
                MessageBox.Show("Student deleted successfully!");
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student: " + ex.Message);
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            s.StudentID,
                            s.UserID,
                            s.FullName,
                            s.DateOfBirth,
                            s.Gender,
                            s.Email,
                            s.Phone,
                            s.Address,
                            s.CourseID,
                            s.CourseCode
                        FROM Students s
                        WHERE s.FullName LIKE @Search
                           OR s.Email LIKE @Search
                           OR s.Phone LIKE @Search
                           OR s.Address LIKE @Search;";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvStudents.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        // =========================
        // EVENTS
        // =========================
        private void cboCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCourseCodes();
        }

        private void cboCourseCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optionally reload students or handle code changes here
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can leave this empty or add logic as needed
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }

        private void cboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Add logic if needed
        }
    }
}
