using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PRG272_Project.DataLayer
{
    internal class DataHandler
    {
        public DataHandler() { }

        string conn = "Server= LAPTOP-RQ490KSC\\SQLEXPRESS02; Initial Catalog= StudentAdministration; Integrated Security= SSPI";

        public bool SignUp(string username, string password)
        {

            using (SqlConnection connection = new SqlConnection(conn))
            {
                string query = "INSERT INTO Users (Username, Password) VALUES(@Username, @Password)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;

                    }
                    catch (Exception err)
                    {

                        MessageBox.Show($"Database error has occured: {err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
        }

        public bool Validate(string username, string password)
        {

            using (SqlConnection connect = new SqlConnection(conn))
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connect.Open();
                        int count = (int)command.ExecuteScalar();
                        return count == 1;
                    }
                    catch (Exception err)
                    {

                        MessageBox.Show($"Database error has occured: {err.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
        }

    }
}
