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
    public partial class frmManageStaff : Form
    {
        DBConnection db = new DBConnection();
        SqlConnection conn;

        public frmManageStaff()
        {
            InitializeComponent();
        }
        private void frmManageStaff_Load(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void LoadStaff()
        {
            try
            {
                using (conn = db.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT s.StaffID, s.FullName, s.Email, s.Phone, s.Department
                                     FROM Staff s
                                     ORDER BY s.FullName";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvStaff.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading staff: " + ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtDepartment.Clear();
            txtPassword.Clear();
            txtEmail.Enabled = true; // allow new record
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = db.GetConnection())
                {
                    conn.Open();

                    // Insert into Users table
                    string insertUser = @"INSERT INTO Users (FullName, Email, Password, Role)
                                          VALUES (@Name, @Email, @Password, 'Staff');
                                          SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdUser = new SqlCommand(insertUser, conn);
                    cmdUser.Parameters.AddWithValue("@Name", txtFullName.Text);
                    cmdUser.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmdUser.Parameters.AddWithValue("@Password", txtPassword.Text);

                    int userId = Convert.ToInt32(cmdUser.ExecuteScalar());

                    // Insert into Staff table
                    string insertStaff = @"INSERT INTO Staff (UserID, FullName, Email, Phone, Department)
                                           VALUES (@UserID, @FullName, @Email, @Phone, @Department)";

                    SqlCommand cmdStaff = new SqlCommand(insertStaff, conn);
                    cmdStaff.Parameters.AddWithValue("@UserID", userId);
                    cmdStaff.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmdStaff.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmdStaff.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmdStaff.Parameters.AddWithValue("@Department", txtDepartment.Text);

                    cmdStaff.ExecuteNonQuery();

                    MessageBox.Show("Staff member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadStaff();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving staff: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a staff member first.");
                return;
            }

            int staffId = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["StaffID"].Value);

            try
            {
                using (conn = db.GetConnection())
                {
                    conn.Open();

                    // 1) Get UserID for the selected staff member
                    int userId = 0;
                    string getUserIdQuery = "SELECT UserID FROM Staff WHERE StaffID = @StaffID";
                    using (SqlCommand cmdGetUserId = new SqlCommand(getUserIdQuery, conn))
                    {
                        cmdGetUserId.Parameters.AddWithValue("@StaffID", staffId);
                        object result = cmdGetUserId.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            userId = Convert.ToInt32(result);
                    }

                    // 2) Update Users table
                    string updateUser = @"
                UPDATE Users SET
                    FullName = @FullName,
                    Email = @Email,
                    Password = @Password
                WHERE UserID = @UserID;";
                    using (SqlCommand cmdUser = new SqlCommand(updateUser, conn))
                    {
                        cmdUser.Parameters.AddWithValue("@FullName", txtFullName.Text);
                        cmdUser.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmdUser.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmdUser.Parameters.AddWithValue("@UserID", userId);
                        cmdUser.ExecuteNonQuery();
                    }

                    // 3) Update Staff table
                    string updateStaff = @"
                UPDATE Staff 
                SET FullName=@FullName, Email=@Email, Phone=@Phone, Department=@Department
                WHERE StaffID=@StaffID";
                    using (SqlCommand cmdStaff = new SqlCommand(updateStaff, conn))
                    {
                        cmdStaff.Parameters.AddWithValue("@FullName", txtFullName.Text);
                        cmdStaff.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmdStaff.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmdStaff.Parameters.AddWithValue("@Department", txtDepartment.Text);
                        cmdStaff.Parameters.AddWithValue("@StaffID", staffId);
                        cmdStaff.ExecuteNonQuery();
                    }

                    MessageBox.Show("Staff updated successfully!");
                    LoadStaff();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating staff: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStaff.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a staff member to delete.");
                    return;
                }
                DataGridViewRow row = dgvStaff.SelectedRows[0];
                int staffId = Convert.ToInt32(row.Cells["StaffID"].Value);

                // Fetch UserID from Staff table
                int userId = 0;
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string getUserIdQuery = "SELECT UserID FROM Staff WHERE StaffID = @StaffID";
                    using (SqlCommand cmdGetUserId = new SqlCommand(getUserIdQuery, conn))
                    {
                        cmdGetUserId.Parameters.AddWithValue("@StaffID", staffId);
                        object result = cmdGetUserId.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            userId = Convert.ToInt32(result);
                    }
                }

                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this staff member and their user account?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    // Delete from Staff
                    string delStaff = "DELETE FROM Staff WHERE StaffID = @SID;";
                    SqlCommand cmdStaff = new SqlCommand(delStaff, conn);
                    cmdStaff.Parameters.AddWithValue("@SID", staffId);
                    cmdStaff.ExecuteNonQuery();

                    // Delete from Users
                    if (userId != 0)
                    {
                        string delUser = "DELETE FROM Users WHERE UserID = @UID;";
                        SqlCommand cmdUser = new SqlCommand(delUser, conn);
                        cmdUser.Parameters.AddWithValue("@UID", userId);
                        cmdUser.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Staff member deleted successfully!");
                LoadStaff(); // Refresh your staff grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting staff: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = db.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT StaffID, FullName, Email, Phone, Department
                                     FROM Staff
                                     WHERE FullName LIKE @Search OR Email LIKE @Search";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvStaff.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtFullName.Text = dgvStaff.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
                txtEmail.Text = dgvStaff.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                txtPhone.Text = dgvStaff.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
                txtDepartment.Text = dgvStaff.Rows[e.RowIndex].Cells["Department"].Value.ToString();

                txtEmail.Enabled = false; // cannot change email for existing staff
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDepartment_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
