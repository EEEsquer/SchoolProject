using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolProject.Repositories
{
    internal class UserRepository
    {
        SqlConnection _Connection = new SqlConnection();
        public UserRepository()
        {
            _Connection.ConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        }

        public async Task<User> GetUser(string username, string password)
        {
            try
            {
                User user = new User();
                //Open the connection for doing commands
                await _Connection.OpenAsync();

                if (_Connection.State == ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand("sp_Get_User", _Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", username);
                        command.Parameters.AddWithValue("@PassWord", password);

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.UserName = reader["UserName"].ToString();
                            user.Password = reader["Password"].ToString();
                            user.Email = reader["Email"].ToString();
                            user.DOB = Convert.ToDateTime(reader["DateOfBirth"]);
                            user.Gender = reader["Gender"].ToString();
                        }

                        reader.Close();

                    } //end sqlcommand
                }//end if 
                return user;
            }
            catch (Exception err)
            {
                MessageBox.Show($"A problem has ocurred: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
        }



    }


}
