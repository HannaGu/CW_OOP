using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using CW_WPF.Model;

namespace CW_WPF.DB
{
    class DB_GetItems
    {
       
        private const string StringConnection = @"Data Source=LAPTOP-NVMEQO2L;Initial Catalog=READER;Integrated Security=True";
        //public ObservableCollection<User> GetUser()
        //{
        //    ObservableCollection<User> users = new ObservableCollection<User>();
        //    using (SqlConnection sqlCon = new SqlConnection(StringConnection))
        //    {
        //        try
        //        {
        //            sqlCon.Open();
        //            SqlCommand command = new SqlCommand();
        //            command.Connection = sqlCon;
        //            command.CommandText = @"Select username, password, first_name, last_name From Users";

        //            SqlDataReader info = command.ExecuteReader();
        //            object un = -1, pw = -1, f_n = -1, l_n = -1;

        //            while (info.Read())
        //            {
        //                un = info["username"];
        //                pw = info["password"];
        //                f_n = info["first_name"];
        //                l_n = info["last_name"];
        //                users.Add(new User() { Username = Convert.ToString(un), Password = Convert.ToString(pw), First_name = Convert.ToString(f_n), Last_name = Convert.ToString(l_n) });
        //            }
        //            return users;
        //        }
        //        catch (Exception e)
        //        {
        //            return users;
        //        }
        //    }
        //}

        public ObservableCollection<Book> GetBook()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"Select isbn, title, original_language, year_of_release, description, rate, image, author, ganre From Books where is_custom=0";

                    SqlDataReader info = command.ExecuteReader();
                    object isbn = -1, t = -1, a = -1, o_l = -1, y_r = -1, desc = -1, rate = -1, im = -1, gr=-1;
                    while (info.Read())
                    {
                       
                        t = info["title"];
                        o_l = info["original_language"];
                        y_r = info["year_of_release"];
                        desc = info["description"];
                        rate = info["rate"];
                        if (info["image"] != null)
                        {
                            im = (byte[])info["image"];
                        }
                        a = info["author"];
                        gr = info["ganre"];
                        isbn = info["isbn"];

                        Book b = new Book(Convert.ToString(isbn), Convert.ToString(t), Convert.ToString(a), Convert.ToString(o_l), Convert.ToInt32(y_r), Convert.ToString(desc), Convert.ToInt32(rate), (byte[])(im), Convert.ToString(gr));
                        books.Add(b);
                      }
                    return books;
                }
                catch (Exception e)
                {
                    return books;
                }
            }
        }

        public ObservableCollection<Book> GetUserBook()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"Select B.isbn, B.title, B.original_language, B.year_of_release, B.description, B.rate, B.image, B.author , B.ganre
                                            from Books as B join User_Books as UB ON B.isbn=UB.isbn where UB.id_user= @id_user";
                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters["@id_user"].Value = Properties.Settings.Default.IdUser;
                    SqlDataReader info = command.ExecuteReader();
                    object isbn = -1, t = -1, a = -1, o_l = -1, y_r = -1, desc = -1, rate = -1, im = -1, gr = -1;
                    
                    while (info.Read())
                    {
                        isbn = info["isbn"];
                        t = info["title"];
                        o_l = info["original_language"];
                        y_r = info["year_of_release"];
                        desc = info["description"];
                        rate = info["rate"];
                        im = info["image"];
                        a = info["author"];
                        gr = info["ganre"];

                        Book b = new Book(Convert.ToString(isbn), Convert.ToString(t), Convert.ToString(a), Convert.ToString(o_l), Convert.ToInt32(y_r), Convert.ToString(desc), Convert.ToInt32(rate), (byte[])(im), Convert.ToString(gr));
                        books.Add(b);
                    }
                    return books;
                }
                catch (Exception e)
                {
                    return books;
                }
            } 
        }

        internal void DeleteUserBook(Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"SELECT TOP(1) isbn FROM Books WHERE title=@title ";
                    command.Parameters.Add("@title", SqlDbType.NVarChar, 40);
                    command.Parameters["@title"].Value = b.Title;
                    SqlDataReader info = command.ExecuteReader();
                    object isbn = -1; int id;
                    while (info.Read())
                    {
                        isbn = info["isbn"];
                    }
                    id = Convert.ToInt32(isbn);

                    DeleteHelper(id);
                }
                catch (Exception e)
                {

                }
            }
        }

        public void DeleteHelper(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"DELETE FROM User_Books WHERE id_user=@id_user AND isbn=@isbn";
                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters.Add("@isbn", SqlDbType.Int);
                    command.Parameters["@id_user"].Value = Convert.ToString(Properties.Settings.Default.IdUser);
                    command.Parameters["@isbn"].Value = id;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
            }
        }
        internal void AddUserBook(Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"INSERT INTO Books (title, author, original_language, year_of_release, description, rate, image, is_custom, ganre) VALUES (@title, @author, @original_language, @year_of_release, @description, @rate, @image, @is_custom, @ganre)";

                    command.Parameters.Add("@title", SqlDbType.NVarChar,40);
                    command.Parameters.Add("@author", SqlDbType.NVarChar,50);
                    command.Parameters.Add("@original_language", SqlDbType.NVarChar,20);
                    command.Parameters.Add("@ganre", SqlDbType.NVarChar,40);
                    command.Parameters.Add("@year_of_release", SqlDbType.Int);
                    command.Parameters.Add("@description", SqlDbType.NVarChar,500);
                    command.Parameters.Add("@rate", SqlDbType.Int);
                    command.Parameters.Add("@image", SqlDbType.Image, 1000000);
                    command.Parameters.Add("@is_custom", SqlDbType.Bit);

                    command.Parameters["@title"].Value = b.Title;
                    command.Parameters["@author"].Value =b.Author;
                    command.Parameters["@original_language"].Value = b.Original_language;
                    command.Parameters["@year_of_release"].Value = b.Year_of_release;
                    command.Parameters["@description"].Value = b.description;
                    command.Parameters["@rate"].Value = b.Rate;
                    command.Parameters["@image"].Value = b.Image;
                    command.Parameters["@is_custom"].Value = b.is_custom;
                    command.Parameters["@ganre"].Value = b.Ganre;

                    command.ExecuteNonQuery();

                    command.CommandText = @"SELECT TOP 1 isbn FROM Books ORDER BY isbn DESC";
                    object isbn = -1; int id;
                    SqlDataReader info = command.ExecuteReader();
                    while (info.Read())
                    {
                        isbn = info["isbn"];
                    }
                    id = (int)isbn;
                    InsertToUserLib(id);
                }
                catch (Exception e)
                {

                }
            }
        }
        public void InsertToUserLib(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"INSERT INTO User_Books (id_user, isbn) VALUES (@id_user, @isbn)";
                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters.Add("@isbn", SqlDbType.Int);

                    command.Parameters["@id_user"].Value = Convert.ToString(Properties.Settings.Default.IdUser);
                    command.Parameters["@isbn"].Value = id;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
            }
             
        }

        public void UpdateBook(Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;

                    command.CommandText = @"Update Books set title=@title, author=@author, original_language=@original_language, year_of_release=@year_of_release, description=@description, rate=@rate, 
                                            image=@image, ganre=@ganre Where isbn = @isbn and is_custom=1";
                   
                    command.Parameters.Add("@title", SqlDbType.NVarChar,40);
                    command.Parameters.Add("@ganre", SqlDbType.NVarChar,40);
                    command.Parameters.Add("@author", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@original_language", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@year_of_release", SqlDbType.Int);
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 500);
                    command.Parameters.Add("@rate", SqlDbType.Int);
                    command.Parameters.Add("@image", SqlDbType.Image, 1000000);
                    command.Parameters.Add("@isbn", SqlDbType.Int);
               
                    command.Parameters["@title"].Value = b.Title;
                    command.Parameters["@author"].Value = b.Author;
                    command.Parameters["@original_language"].Value = b.Original_language;
                    command.Parameters["@year_of_release"].Value = b.Year_of_release;
                    command.Parameters["@description"].Value = b.description;
                    command.Parameters["@rate"].Value = b.Rate;
                    command.Parameters["@image"].Value = b.Image;
                    command.Parameters["@isbn"].Value = b.Isbn;
                    command.Parameters["@ganre"].Value = b.Ganre;
                

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                }

            }
        }


    }
}
