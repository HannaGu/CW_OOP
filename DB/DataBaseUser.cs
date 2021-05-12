using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CW_WPF.Model;

namespace CW_WPF.DB
{
    class DataBaseUser
    {
        private const string StringConnection = @"Data Source=LAPTOP-NVMEQO2L;Initial Catalog=READER;Integrated Security=True";

        public bool GiveUserByLoginAndPassword(string login, string password)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"Select count(*) From Users Where login = @login and password = @password";
                    command.Parameters.Add("@login", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 100);

                    command.Parameters["@login"].Value = login;
                    command.Parameters["@password"].Value = password;
                    object count = command.ExecuteScalar();
                    if (Convert.ToInt32(count) > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }

        internal bool GetIsAdminUser(string id_user)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"Select is_admin From Users Where id_user = @id_user";
                    command.Parameters.Add("@id_user", SqlDbType.Int);

                    command.Parameters["@id_user"].Value = id_user;
                    SqlDataReader info = command.ExecuteReader();
                    object id = -1;
                    while (info.Read())
                    {
                        id = info["is_admin"];
                        break;
                    }
                    if (Convert.ToInt32(id) == 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }


        }

        public bool AddUser(string login, string password, string firstname, string lastname)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"INSERT INTO Users (login, password, is_admin, first_name, last_name) VALUES (@login,@password,@is_admin,@first_name,@last_name)";

                    command.Parameters.Add("@login", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 100);
                    command.Parameters.Add("@is_admin", SqlDbType.Bit);
                    command.Parameters.Add("@first_name", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@last_name", SqlDbType.NVarChar, 20);
                   

                    command.Parameters["@login"].Value = login;
                    command.Parameters["@password"].Value = password;
                    command.Parameters["@is_admin"].Value = 0;
                    command.Parameters["@first_name"].Value = firstname;
                    command.Parameters["@last_name"].Value = lastname;
                    
                    command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public string GetIdUserByLogin(string login)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"Select id_user From Users Where login = @login";
                    command.Parameters.Add("@login", SqlDbType.NVarChar, 20);

                    command.Parameters["@login"].Value = login;
                    SqlDataReader info = command.ExecuteReader();
                    object id = -1;
                    while (info.Read())
                    {
                        id = info["id_user"];
                        break;
                    }
                    return Convert.ToString(id);
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public Users GetUserInfo(string id_user)
        {
            Users spam = new Users();
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"select  login, first_name, last_name From Users Where id_user=@id_user ;";
                    command.Parameters.Add("@id_user", SqlDbType.Int);

                    command.Parameters["@id_user"].Value = id_user;
                    SqlDataReader info = command.ExecuteReader();
                    object l = -1, fn = -1, ln = -1, h = 1, w = -1, dc = -1, a = -1;
                    string g = "", activ = "", p = "";

                    while (info.Read())
                    {
                        l = info["login"];
                        fn = info["first_name"];
                        ln = info["last_name"];
                      

                        spam = new Users
                        {
                            Id_user = Convert.ToInt32(id_user),
                            Login = Convert.ToString(l),
                            LastName = Convert.ToString(ln),
                            FirstName = Convert.ToString(fn),
                        };
                    }
                    return spam;
                }
                catch (Exception e)
                {
                    return spam;
                }
            }
        }


    }
}
