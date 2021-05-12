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
   public class UpdateViewModel:ViewModelBase
    {
        Book book = new Book();
        public UpdateViewModel(Book b)
        {
            book = b;
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
        
        ComboBoxItem cbi;
        public ComboBoxItem CBI
        {
            get
            {
                return cbi;
            }
            set
            {
                cbi = value;
                Ganre = cbi.Content.ToString();
                RaisePropertiesChanged(nameof(CBI));
            }
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

        public int Year_of_release
        {
            get
            {
                return book.year_of_release;
            }
            set
            {
                book.year_of_release = value;
                RaisePropertiesChanged(nameof(Year_of_release));
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

        #region AddImage
        public ICommand open_Image => new DelegateCommand(Open_Image);
        private void Open_Image()
        {
            string FilePath = "";
            if (OpenFile(ref FilePath))
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
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        #endregion

        public ICommand update_Book => new DelegateCommand(Update_Book);
        private void Update_Book()
        {
            try
            {
                DB_GetItems db = new DB_GetItems();
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
                if (book.Year_of_release > DateTime.Now.Year || book.Year_of_release < 0)
                {
                    fl = false;
                    ErrorMes = Properties.Resources.yearerr;
                }

                if (fl)
                {
                    foreach (Book b in UserBooksViewModel.All_UserBooks)
                    {
                        if (b.Isbn == book.Isbn)
                        {
                            b.Isbn = book.Isbn;                           
                            b.Author = book.Author;                            
                            b.Description = book.Description;                           
                            b.Image = book.Image;
                            b.Original_language = book.Original_language;
                            b.Rate = book.Rate;
                            b.Title = book.Title;
                            b.Year_of_release = book.Year_of_release; 
                                }

                    }
                    db.UpdateBook(book);
                }
            }
            catch (SystemException)
            {
                ErrorMes = Properties.Resources.errordata;
            }
        }
    }
}
