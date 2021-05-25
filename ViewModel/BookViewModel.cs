 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;

using CW_WPF.Model;
using CW_WPF.View;
using CW_WPF.DB;
using System.Windows.Controls;
using System.Windows;

namespace CW_WPF.ViewModel
{
    public class BookViewModel: ViewModelBase
    {
        LibraryViewModel boss_page;
        Book selected_book;
        public BookViewModel(Book obj1, LibraryViewModel obj2 )
        {
            selected_book = obj1;
            boss_page = obj2;

            Isbn = selected_book.isbn;
            Title = selected_book.title;
            Author = selected_book.author;
            Original_language = selected_book.original_language;
            Description = selected_book.description;
            Rate = selected_book.rate;
            Image = selected_book.image;
            Ganre= selected_book.ganre;

        }
       
        //public ICommand back => new DelegateCommand(Back);
        //private void Back()
        //{
        //    Page Library= new LibraryPage();
        //    boss_page.CurrentPage = Library;
        //}

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
                this.isbn= value;
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
            Page Books_page = new AllBooksPage(boss_page);
            boss_page.CurrentPage = Books_page;
        }

        public ICommand to_Favourites => new DelegateCommand(ToFavourites);

        private void ToFavourites()
        {
            DB_GetItems db = new DB_GetItems();
            db.InsertToUserLib(Convert.ToInt32(selected_book.Isbn));
            UserBooksViewModel.All_UserBooks.Add(selected_book);
            MessageBox.Show("Книга добавлена в 'Избрранное'", "Все хорошо!");
        }
    }
}

