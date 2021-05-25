using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_WPF.Model;
using CW_WPF.View;
using CW_WPF.DB;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Controls;

namespace CW_WPF.ViewModel
{
    public class AllBooksViewModel : ViewModelBase
    {
        public static ObservableCollection<Book> All_Books { get; set; }
        DB_GetItems db_GetItems = new DB_GetItems();
        DataBaseUser db_User = new DataBaseUser();
        LibraryViewModel Obj;

       
       public AllBooksViewModel(LibraryViewModel obj)
        {
            All_Books = db_GetItems.GetBook();           
            Obj = obj;
        }

        private int index;
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                this.index = value;
                RaisePropertiesChanged(nameof(Index));
            }
        }
        private string searchRequest="";
        public string SearchRequest
        {
            get
            {
                return searchRequest;
            }
            set
            {
                searchRequest = value;
                RaisePropertiesChanged(nameof(SearchRequest));
            }
        }

        public ICommand open_BookPage => new DelegateCommand(OpenBookPage);

        private void OpenBookPage()
        {
            if (db_User.GetIsAdminUser(Properties.Settings.Default.IdUser))
            {
                Page AdminBook = new AdminBookPage(All_Books[Index], Obj );
                Obj.CurrentPage = AdminBook;
            }
            else
            {
                Page BookInfo = new BookPage(All_Books[Index], Obj);
                Obj.CurrentPage = BookInfo;
            }
        }

        public ICommand search_Command => new DelegateCommand(SearchCommand);

        private void SearchCommand()
        {
            ObservableCollection<Book> Items = new ObservableCollection<Book>();
            Items= db_GetItems.GetBook();

            if (!SearchRequest.Equals(""))
            {
                var titleSearch = from b in Items where b.Title.ToUpper().Equals(SearchRequest.ToUpper()) select b;
                if (titleSearch.Count() != 0)
                {
                    All_Books.Clear();
                    foreach (Book no in titleSearch)
                    {
                        All_Books.Add(no);
                    }
                }
                else
                {
                    var authorSearch = from b in Items where b.Author.ToUpper().Equals(SearchRequest.ToUpper()) select b;
                    if (authorSearch.Count() != 0)
                    {
                        All_Books.Clear();
                        foreach (Book no in authorSearch)
                        {
                            All_Books.Add(no);
                        }
                    }
                }
            }

        }

        public ICommand name_Sorting => new DelegateCommand(NameSorting);

        private void NameSorting()
        {
            var nameOrdered = from b in All_Books orderby b.Title select b;
            int i = 0;
            foreach(Book b in nameOrdered)
            {
                All_Books.RemoveAt(i);
                All_Books.Insert(i, b);
                i++;
            }
        }

        public ICommand author_Sorting => new DelegateCommand(AuthorSorting);

        private void AuthorSorting()
        {
            var nameOrdered = from b in All_Books orderby b.Author select b;
            int i = 0;
            foreach (Book b in nameOrdered)
            {
                All_Books.RemoveAt(i);
                All_Books.Insert(i, b);
                i++;
            }
        }

    }
}
