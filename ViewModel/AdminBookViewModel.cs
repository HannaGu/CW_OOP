using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm;
using System.Windows.Input;

using CW_WPF.Model;
using CW_WPF.DB;
using CW_WPF.View;
using System.Windows.Controls;

namespace CW_WPF.ViewModel
{
    public class AdminBookViewModel: ViewModelBase
    {
        LibraryViewModel boss_page;
        public static Book selected_book;
        Progress p = new Progress();
        DataBaseUser dbu = new DataBaseUser();

        public AdminBookViewModel(Book obj1, LibraryViewModel obj2)
        {
            //p = dbu.GetProgress();
            selected_book = obj1;
            boss_page = obj2;

            Isbn = selected_book.isbn;
            Title = selected_book.title;
            Author = selected_book.author;
            Original_language = selected_book.original_language;
            Description = selected_book.description;
            Rate = selected_book.rate;
            Image = selected_book.image;
            Ganre = selected_book.ganre;
        }


        public string Status
        {
            get { return p.status; }
            set
            {
                p.status = value;
                RaisePropertiesChanged(nameof(Status));
            }
        }
        public bool IsAllowed
        {
            get { return selected_book.Is_custom == false; }
        }

        private Page currentpage;
        public Page CurrentPage
        {
            get
            {
                return currentpage;
            }
            set
            {
                this.currentpage = value;
                RaisePropertiesChanged(nameof(CurrentPage));
            }
        }


        #region Properties
        private string isbn;
        public string Isbn
        {
            get
            {
                return isbn;
            }
            set
            {
                this.isbn = value;
                RaisePropertiesChanged(nameof(Isbn));
            }
        }
        private string ganre;
        public string Ganre
        {
            get
            {
                return ganre;
            }
            set
            {
                this.ganre = value;
                RaisePropertiesChanged(nameof(Ganre));
            }
        }
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                this.title = value;
                RaisePropertiesChanged(nameof(Title));
            }
        }
        private string author;
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                this.author = value;
                RaisePropertiesChanged(nameof(Author));
            }
        }
        private string original_language;
        public string Original_language
        {
            get
            {
                return original_language;
            }
            set
            {
                this.original_language = value;
                RaisePropertiesChanged(nameof(Original_language));
            }
        }
        private int year_of_release;
        public int Year_of_release
        {
            get
            {
                return year_of_release;
            }
            set
            {
                this.year_of_release = value;
                RaisePropertiesChanged(nameof(Year_of_release));
            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                this.description = value;
                RaisePropertiesChanged(nameof(Description));
            }
        }
        private int rate;
        public int Rate
        {
            get
            {
                return rate;
            }
            set
            {
                this.rate = value;
                RaisePropertiesChanged(nameof(Rate));
            }
        }
        private byte[] image;
        public byte[] Image
        {
            get
            {
                return image;
            }
            set
            {
                this.image = value;
                RaisePropertiesChanged(nameof(Image));
            }
        }
        #endregion

        public ICommand back => new DelegateCommand(Back);
        private void Back()
        {
            Page AdminBooks_page = new AllBooksPage(boss_page);
            boss_page.CurrentPage = AdminBooks_page;
        }
        public ICommand delete => new DelegateCommand(Delete);
        private void Delete()
        {
            DB_GetItems db = new DB_GetItems();
            //db.DeleteUserBook(selected_book);
            //UserBooksViewModel.All_UserBooks.Remove(selected_book);
            Page AllBooks = new AllBooksPage(boss_page);
            boss_page.CurrentPage = AllBooks;

        }

        public ICommand update => new DelegateCommand(Update);
        private void Update()
        {
            UpdateAdminBook uw = new UpdateAdminBook(selected_book);
            uw.Show();
        }

    }
}
