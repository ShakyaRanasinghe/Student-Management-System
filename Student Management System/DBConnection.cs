using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    internal class DBConnection
    {
        public SqlConnection GetConnection()
        {
            
            SqlConnection conn = new SqlConnection(@"Data Source=MSI;Initial Catalog=StudentManagementSystem;Integrated Security=True");
            return conn;
        }
    }
}
