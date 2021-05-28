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
using System.Windows;
using CW_WPF.Model;
using CW_WPF.ViewModel;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace CW_WPF.DB
{
    class DB_GetItems
    {

        private string StringConnection = @"Data Source=LAPTOP-NVMEQO2L;Initial Catalog=READER;Integrated Security=True";


        public ObservableCollection<Book> GetBook()
        {

            //string str;
            //SqlConnection myConn = new SqlConnection("Server=LAPTOP-NVMEQO2L;Integrated security=True;database=master");
            //str = "CREATE DATABASE NEW_READER";
            //SqlCommand myCommand = new SqlCommand(str, myConn);
            //try
            //{
            //    myConn.Open();
            //    myCommand.ExecuteNonQuery();
            //    myConn.Close();
            //    myConn = new SqlConnection("Data Source=LAPTOP-NVMEQO2L;Initial Catalog=NEW_READER;Integrated Security=True");
            //    str = "CREATE TABLE Books(isbn int PRIMARY KEY identity(1,1) NOT NULL, " +
            //        "title nvarchar(40) NOT NULL, " +
            //        "author nvarchar(50) NOT NULL, " +
            //        "original_language nvarchar(20), " +
            //        "year_of_release int, " +
            //        "description nvarchar(500), " +
            //        "rate int, " +
            //        "image nvarchar(MAX)); ";
            //    myCommand = new SqlCommand(str, myConn);
            //    myConn.Open();
            //    myCommand.ExecuteNonQuery();

            //    SqlTransaction transaction = myConn.BeginTransaction();
            //    myCommand.Transaction = transaction;
            //    myCommand.CommandText = "Insert into Books(title, author, image) values ('Ромео и Джульетта', 'Уильям Шекспир' , (select * from openrowset(bulk N'D:\\rats.jpg', SINGLE_BLOB) as T1 ) )";
            //    myCommand.ExecuteNonQuery();
            //    myCommand.CommandText = "Insert into Books(title, author, image) values ('Руслан и Людмила', 'Александр Пушкин' , (select * from openrowset(bulk N'D:\\rats.jpg', SINGLE_BLOB) as T1 ) )";
            //    myCommand.ExecuteNonQuery();
            //    transaction.Commit();
            //}
            //catch (Exception ex)
            //{
            //}



            // SqlConnection sqlCon = new SqlConnection(StringConnection);

            //    try
            //    {
            //    sqlCon.Open();
            //}
            //catch (Exception e)
            //{

            //        string str = "CREATE DATABASE NEW_READER";
            //        sqlCon = new SqlConnection("Server=LAPTOP-NVMEQO2L;Integrated security=True;database=master");
            //        command = new SqlCommand(str, sqlCon);
            //        sqlCon.Open();
            //        command.ExecuteNonQuery();
            //        sqlCon.Close();

            //        sqlCon = new SqlConnection("Data Source=LAPTOP-NVMEQO2L;Initial Catalog=NEW_READER;Integrated Security=True");
            //        str = "CREATE TABLE Books(isbn int PRIMARY KEY identity(1,1) NOT NULL, " +
            //        "title nvarchar(40) NOT NULL, " +
            //        "author nvarchar(50) NOT NULL, " +
            //        "is_custom bit," +
            //        "ganre nvarchar(40)," +
            //        "original_language nvarchar(20), " +
            //        "description nvarchar(500), " +
            //        "rate int, " +
            //        "image varbinary(MAX)); ";
            //        command = new SqlCommand(str, sqlCon);
            //        sqlCon.Open();
            //        command.ExecuteNonQuery();

            //        SqlTransaction transaction = sqlCon.BeginTransaction();
            //        command.Transaction = transaction;
            //        command.CommandText = @"Insert into Books(title, author, original_language,rate, description, is_custom, ganre, image) values ('Ромео и Джульетта', 'Уильям Шекспир', 'Английский', 6,'Поэма', 0, 'Классическая литература' , (select * from openrowset(bulk N'D:\mouse.jpg', SINGLE_BLOB) as T1  ))";

            //        command.ExecuteNonQuery();
            //        command.CommandText = @"Insert into Books(title, author, original_language,rate, description, is_custom,ganre, image) values ('Руслан и Людмила', 'Александр Пушкин', 'Русский', 6,'Поэма', 0, 'Классическая литература',(select * from openrowset(bulk N'D:\mouse.jpg', SINGLE_BLOB) as T1)  )";
            //        command.ExecuteNonQuery();
            //        transaction.Commit();
            //    StringConnection = @"Data Source=LAPTOP-NVMEQO2L;Initial Catalog=NEW_READER;Integrated Security=True";

            //}
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {

                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();

                    command.Connection = sqlCon;
                    command.CommandText = @"Select isbn, title, original_language, description, rate, image, author, ganre, link From Books where is_custom=0";

                    SqlDataReader info = command.ExecuteReader();
                    object isbn = -1, t = -1, a = -1, o_l = -1, y_r = -1, desc = -1, rate = -1, im = -1, gr = -1, link=-1;
                    while (info.Read())
                    {

                        t = info["title"];
                        o_l = info["original_language"];
                        desc = info["description"];
                        rate = info["rate"];
                        if (!info.IsDBNull(info.GetOrdinal("image")))
                        {
                            im = (byte[])info["image"];
                        }
                        a = info["author"];
                        gr = info["ganre"];
                        isbn = info["isbn"];
                        link = info["link"];

                        Book b = new Book(Convert.ToString(isbn), Convert.ToString(t), Convert.ToString(a), Convert.ToString(o_l), Convert.ToString(desc), Convert.ToInt32(rate), (byte[])im, Convert.ToString(gr), Convert.ToString(link));
                        books.Add(b);
                    }
                    return books;
                }
                catch (Exception e)
                {
                    return books;
                }

            } }



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
                    command.CommandText = @"Select B.isbn, B.title, B.original_language, B.year_of_release, B.description, B.rate, B.image, B.author , B.ganre, B.is_custom, B.link
                                            from Books as B join User_Books as UB ON B.isbn=UB.isbn where UB.id_user= @id_user";
                    command.Parameters.Add("@id_user", SqlDbType.Int);
                    command.Parameters["@id_user"].Value = Properties.Settings.Default.IdUser;
                    SqlDataReader info = command.ExecuteReader();
                    object isbn = -1, t = -1, a = -1, o_l = -1, y_r = -1, desc = -1, rate = -1, im = -1, gr = -1, is_c=-1, link=-1;

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
                        is_c = info["is_custom"];
                        link = info["link"];

                        Book b = new Book(Convert.ToString(isbn), Convert.ToString(t), Convert.ToString(a), Convert.ToString(o_l), Convert.ToString(desc), Convert.ToInt32(rate), (byte[])(im),Convert.ToString(gr), Convert.ToString(link));
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

        internal void DeleteBook(Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"Delete TOP(1) FROM Books WHERE isbn=@isbn ";
                    command.Parameters.Add("@isbn", SqlDbType.Int);
                    command.Parameters["@isbn"].Value =b.Isbn;
                    command.ExecuteNonQuery();
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
                    command.CommandText = @"INSERT INTO Books (title, author, original_language, description, rate, image, is_custom, ganre) VALUES (@title, @author, @original_language, @description, @rate, @image, @is_custom, @ganre)";

                    command.Parameters.Add("@title", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@author", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@original_language", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@ganre", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 500);
                    command.Parameters.Add("@rate", SqlDbType.Int);
                    command.Parameters.Add("@image", SqlDbType.Image, 1000000);
                    command.Parameters.Add("@is_custom", SqlDbType.Bit);

                    command.Parameters["@title"].Value = b.Title;
                    command.Parameters["@author"].Value = b.Author;
                    command.Parameters["@original_language"].Value = b.Original_language;
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


        internal void AddBook(Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;
                    command.CommandText = @"INSERT INTO Books (title, author, original_language, description, rate, image, is_custom, ganre) VALUES (@title, @author, @original_language, @description, @rate, @image, @is_custom, @ganre)";

                    command.Parameters.Add("@title", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@author", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@original_language", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@ganre", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 500);
                    command.Parameters.Add("@rate", SqlDbType.Int);
                    command.Parameters.Add("@image", SqlDbType.Image, 1000000);
                    command.Parameters.Add("@is_custom", SqlDbType.Bit);

                    command.Parameters["@title"].Value = b.Title;
                    command.Parameters["@author"].Value = b.Author;
                    command.Parameters["@original_language"].Value = b.Original_language;
                    command.Parameters["@description"].Value = b.description;
                    command.Parameters["@rate"].Value = b.Rate;
                    command.Parameters["@image"].Value = b.Image;
                    command.Parameters["@is_custom"].Value =0;
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

        public void UpdateAdminBook(Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;

                    command.CommandText = @"Update Books set title=@title, author=@author, original_language=@original_language, description=@description, rate=@rate, 
                                            image=@image, ganre=@ganre Where isbn = @isbn and is_custom=0";

                    command.Parameters.Add("@title", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@ganre", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@author", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@original_language", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 500);
                    command.Parameters.Add("@rate", SqlDbType.Int);
                    command.Parameters.Add("@image", SqlDbType.Image, 1000000);
                    command.Parameters.Add("@isbn", SqlDbType.Int);

                    command.Parameters["@title"].Value = b.Title;
                    command.Parameters["@author"].Value = b.Author;
                    command.Parameters["@original_language"].Value = b.Original_language;
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


        public void UpdateBook(Book b)
        {
            using (SqlConnection sqlCon = new SqlConnection(StringConnection))
            {
                try
                {
                    sqlCon.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sqlCon;

                    command.CommandText = @"Update Books set title=@title, author=@author, original_language=@original_language, description=@description, rate=@rate, 
                                            image=@image, ganre=@ganre Where isbn = @isbn and is_custom=1";

                    command.Parameters.Add("@title", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@ganre", SqlDbType.NVarChar, 40);
                    command.Parameters.Add("@author", SqlDbType.NVarChar, 50);
                    command.Parameters.Add("@original_language", SqlDbType.NVarChar, 20);
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 500);
                    command.Parameters.Add("@rate", SqlDbType.Int);
                    command.Parameters.Add("@image", SqlDbType.Image, 1000000);
                    command.Parameters.Add("@isbn", SqlDbType.Int);

                    command.Parameters["@title"].Value = b.Title;
                    command.Parameters["@author"].Value = b.Author;
                    command.Parameters["@original_language"].Value = b.Original_language;
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
    } }


