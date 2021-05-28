using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CW_WPF.Model;
using LiveCharts;

namespace CW_WPF.DB
{
   public  class DataBaseUser
    {
        private const string StringConnection = @"Data Source=LAPTOP-NVMEQO2L;Initial Catalog=READER;Integrated Security=True";

        public ObservableCollection<Users> GetUsers()
        {
            ObservableCollection<Users> users = new ObservableCollection<Users>();
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"Select id_user, login, first_name, last_name, is_admin From Users";

                    SqlDataReader info = command.ExecuteReader();
                    object id = -1, log = -1, fn = -1, ln = -1, is_ad=-1;

                    while (info.Read())
                    {
                        id = info["id_user"];
                        log = info["login"];
                        fn = info["first_name"];
                        ln= info["last_name"];
                        is_ad= info["is_admin"];
                        users.Add(new Users( Convert.ToInt32(id), Convert.ToString(log), Convert.ToString(ln), Convert.ToString(fn), Convert.ToBoolean(is_ad)));
                    }
                    return users;
                }
                catch (Exception e)
                {
                    return users;
                }
            }
        }


        public bool AddProgress(Progress p, Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"INSERT INTO Statistic (id_user, isbn, status, date_change) VALUES (@id_user, @isbn, @status, @date_change)";

                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters.Add("@isbn", SqlDbType.Int);
                    command.Parameters.Add("@status", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@date_change", SqlDbType.DateTime);
                    
                    command.Parameters["@id_user"].Value = Convert.ToString(Properties.Settings.Default.IdUser);
                    command.Parameters["@isbn"].Value = b.Isbn;
                    command.Parameters["@status"].Value = p.status;
                    command.Parameters["@date_change"].Value = Convert.ToString(p.date_change);
                  
                    command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public ChartValues<int> GetProgressByTime()
        {
            ChartValues<int> p = new ChartValues<int>();
            for(int i=0;i<12;i++)
            {
                p.Add(0);
            }
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"select MONTH(date_change) as month_name, COUNT(*) as amount from Statistic where id_user=@id_user and
                                                                                                                             status='Прочитано' and 
                                                                                                                             YEAR(date_change)=YEAR(GETDATE()) 
                                                                                                                             GROUP BY MONTH(date_change) order by  month_name; ";
                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters["@id_user"].Value = Convert.ToString(Properties.Settings.Default.IdUser); ;
                    SqlDataReader info = command.ExecuteReader();
                    object am = -1, mn = -1;

                    while (info.Read())
                    {
                        am = info["amount"];
                        mn = info["month_name"];

                        p.Insert(Convert.ToInt32(mn)-1, am);
                    }


                    return p;
                }
                catch (Exception e)
                {
                    return p;
                }
            }
        }

        public Progress GetProgress(Book b)
        {
            Progress p = new Progress();
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"select TOP(1) status  from Statistic where id_user=@id_user and isbn=@isbn order by date_change desc";

                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters.Add("@isbn", SqlDbType.Int);
                    command.Parameters["@id_user"].Value = Convert.ToString(Properties.Settings.Default.IdUser);
                    command.Parameters["@isbn"].Value = b.isbn; 
                    SqlDataReader info = command.ExecuteReader();
                    object status = -1;

                    while (info.Read())
                    {
                        status = info["status"];
                        p.status = Convert.ToString(status);
                    }


                    return p;
                }
                catch (Exception e)
                {
                    return p;
                }
            }
        }

        public ChartValues<int> GetProgressByGanre()
        {
            ChartValues<int> p = new ChartValues<int>();
            for (int i = 0; i < 13; i++)
            {
                p.Add(0);
            }
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"select b.ganre as ganre, Count(*) as amount  from Books as b join Statistic as s on b.isbn=s.isbn where id_user=@id_user and s.status='Прочитано' and Year(s.date_change)=Year(GETDATE()) GROUP BY b.ganre order by b.ganre ";
                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters["@id_user"].Value = Convert.ToString(Properties.Settings.Default.IdUser); 
                    SqlDataReader info = command.ExecuteReader();
                    object am = -1, gr = -1;
                    string ganre;

                    while (info.Read())
                    {
                        am = info["amount"];
                        gr = info["ganre"];
                        ganre = Convert.ToString(gr);

                        switch (ganre)
                        {
                            case "Бизнес-литература": p.Insert(0, am); break;
                            case "Детектив": p.Insert(1, am); break;
                            case "Детская литература": p.Insert(2, am); break;
                            case "Исторический роман": p.Insert(3, am); break;
                            case "Классическая литература": p.Insert(4, am); break;
                            case "Комикс": p.Insert(5, am); break;
                            case "Любовный роман": p.Insert(6, am); break;
                            case "Научнпоп": p.Insert(7, am); break;
                            case "Ужасы": p.Insert(8, am); break;
                            case "Учебная литература": p.Insert(9, am); break;
                            case "Фантастика": p.Insert(10, am); break;
                            case "Фэнтези": p.Insert(11, am); break;
                            case "Юмор и сатира": p.Insert(12, am); break;
                        }
                    }


                    return p;
                }
                catch (Exception e)
                {
                    return p;
                }
            }
        }


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
