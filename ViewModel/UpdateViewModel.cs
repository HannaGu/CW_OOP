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
        DataBaseUser db_User = new DataBaseUser();
        Book book = new Book();
        Progress progress = new Progress();
        DateTime today = DateTime.Today;
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
        public string Status
        {
            get
            {
                return progress.status;
            }
            set { progress.status = Convert.ToString(value);
                RaisePropertiesChanged(nameof(Status));
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

        ComboBoxItem cbi_progress;
        public ComboBoxItem CBI_Progress
        {
            get
            {
                return cbi_progress;
            }
            set
            {
                cbi_progress = value;
                progress.date_change = today;
                Status = cbi_progress.Content.ToString();
                RaisePropertiesChanged(nameof(CBI_Progress));
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
                DataBaseUser dbu = new DataBaseUser();
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
                        db.UpdateAdminBook(book);
                    }
                    else {
                        dbu.AddProgress(progress, book);
                        db.UpdateBook(book); }
                }
            }
            catch (SystemException)
            {
                ErrorMes = Properties.Resources.errordata;
            }
        }
    }
}
