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
using System.Collections.ObjectModel;

namespace CW_WPF.ViewModel
{
    public class RecomendBooksViewModel : ViewModelBase
    {
        public ObservableCollection<Book> All_Books { get; set; }
        DB_GetItems db_GetItems = new DB_GetItems();
       RecomendViewModel Obj;
        public RecomendBooksViewModel(RecomendViewModel obj)
        {
            All_Books = GetRecomendBooks();
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

        public ICommand open_BookPage => new DelegateCommand(OpenBookPage);

        private void OpenBookPage()
        {
            Page BookInfo = new RecomendBookPage(All_Books[Index], Obj);
            Obj.CurrentPage = BookInfo;
        }

        public ObservableCollection<Book> GetRecomendBooks()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            ObservableCollection<Book> userBooks = new ObservableCollection<Book>();
            ObservableCollection<Book> result = new ObservableCollection<Book>();
            DB_GetItems db_GetItems = new DB_GetItems();
            books = db_GetItems.GetBook();
            //books = AllBooksViewModel.All_Books;
            userBooks = db_GetItems.GetUserBook();
            foreach (Book ub in userBooks)
            {
                var selectedBooks = from b in books
                                    where b.Ganre.Equals(ub.Ganre) || b.Author.ToUpper().Equals(ub.Author.ToUpper())
                                    select b;
                foreach (Book s in selectedBooks)
                {
                    result.Add(s);
                }
            }
            return result;
        }

    }
}

