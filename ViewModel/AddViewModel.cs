using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_WPF.Model;
using CW_WPF.View;
using CW_WPF.DB;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows;


namespace CW_WPF.ViewModel
{
    public class AddViewModel:ViewModelBase
    {
        Book book=new Book();
             
        public byte[] Bin_Image
        {
            get
            {
                return book.image; 
            }
            set
            {
                book.image = value;
                RaisePropertiesChanged(nameof(Bin_Image));
            }
        }
        ComboBoxItem cbi;
        public ComboBoxItem CBI
        {
            get
            {
                return cbi;
            }
            set { cbi = value;
                Ganre = cbi.Content.ToString();
                    RaisePropertiesChanged(nameof(CBI)); }
        }

      public AddViewModel() 
        {
            book.Is_Custom = true;
        }

        #region Properties
        
       
        public string Title
        {
            get
            {
                return book.title;
            }
            set
            {
                book.title = value;
                RaisePropertiesChanged(nameof(Title));
            }
        }
        public string Ganre
        {
            get
            {
                return book.ganre;
            }
            set
            {
                book.ganre = Convert.ToString(value);
                RaisePropertiesChanged(nameof(Ganre));
            }
        }

        public string Author
        {
            get
            {
                return book.author;
            }
            set
            {
                book.author = value;
                RaisePropertiesChanged(nameof(Author));
            }
        }

        public string Original_language
        {
            get
            {
                return book.original_language;
            }
            set
            {
                book.original_language = value;
                RaisePropertiesChanged(nameof(Original_language));
            }
        }
       
     
        public string Description
        {
            get
            {
                return book.description;
            }
            set
            {
                book.description = value;
                RaisePropertiesChanged(nameof(Description));
            }
        }
      
        public int Rate
        {
            get
            {
                return book.rate;
            }
            set
            {
                book.rate = value;
                RaisePropertiesChanged(nameof(Rate));
            }
        }


        #endregion

        private string errorMes;
        public string ErrorMes
        {
            get { return errorMes; }
            set
            {
                this.errorMes = value;
                RaisePropertiesChanged(nameof(ErrorMes));
            }
        }

        public ICommand add_Book => new DelegateCommand(Add_Book);
        private void Add_Book()
        {
            try
            {
                DB_GetItems db = new DB_GetItems();
                DataBaseUser db_User = new DataBaseUser();
                bool fl = true;
                ErrorMes = "";
                if (book.Title == String.Empty || book.Title == null || book.Author == String.Empty || book.Author == null || book.Description == null || book.Description == String.Empty || book.Image == null)
                {
                    fl = false;
                    ErrorMes = Properties.Resources.emptyfield;
                }
                else if (book.Rate > 10 || book.Rate < 0)
                {
                    fl = false;
                    ErrorMes = Properties.Resources.rateerr;
                }
               

                if (fl)
                {
                    if (db_User.GetIsAdminUser(Properties.Settings.Default.IdUser))
                    {
                        db.AddBook(book);
                        AllBooksViewModel.All_Books.Add(book);
                    }
                    else
                    {
                        db.AddUserBook(book);
                        UserBooksViewModel.All_UserBooks.Add(book);
                    }
                    MessageBox.Show("Книга добавлена!", "Все хорошо!");
                }
            }
            catch (SystemException)
            {
                ErrorMes = Properties.Resources.errordata;
            }
        }


        public ICommand open_File => new DelegateCommand(Open_File);
        private void Open_File()
        {
            string FilePath = "";
            if (OpenFile(ref FilePath))
            {
                try
                {
                    book.Link = FilePath;
                    MessageBox.Show("Файл добавлен.", "Все хорошо!");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        #region AddImage
        public ICommand open_Image => new DelegateCommand(Open_Image);
        private void Open_Image()
        {
            string FilePath = "";
            if(OpenFile(ref FilePath))
            {
                try
                {
                    this.Bin_Image = File.ReadAllBytes(FilePath);
                   

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private bool OpenFile(ref string FilePath)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog()==true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        #endregion
    }
}
