using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class frmStudentAttendance : Form
    {
        private readonly DBConnection db = new DBConnection();
        private readonly string studentEmail;

        public frmStudentAttendance(string email)
        {
            InitializeComponent();
            studentEmail = email;
        }

        private void frmStudentAttendance_Load(object sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void LoadAttendance()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    // Step 1 - Convert Email → StudentID
                    int studentId;
                    using (SqlCommand getIdCmd = new SqlCommand(
                        "SELECT StudentID FROM Students WHERE Email = @Email", conn))
                    {
                        getIdCmd.Parameters.AddWithValue("@Email", studentEmail);
                        object result = getIdCmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("No student found linked to this account.");
                            return;
                        }

                        studentId = Convert.ToInt32(result);
                    }

                    // Step 2 - Load attendance
                    string query = @"
                        SELECT Date, Status
                        FROM Attendance
                        WHERE StudentID = @SID
                        ORDER BY Date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SID", studentId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dvgAttendance.DataSource = dt; //  dvgAttendance
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {
        }

        private void dvgAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
