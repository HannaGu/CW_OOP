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
    public class UserBooksViewModel:ViewModelBase
    {
        public static ObservableCollection<Book> All_UserBooks { get; set; }
        DB_GetItems db_GetItems = new DB_GetItems();
        UserLibraryViewModel Obj;
      

        public UserBooksViewModel(UserLibraryViewModel obj)
        {

            All_UserBooks = db_GetItems.GetUserBook();
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

        public ICommand open_UserBookPage => new DelegateCommand(OpenUserBookPage);

        private void OpenUserBookPage()
        {
            if (Index >= 0)
            {
                Page BookInfo = new UserBookPage(All_UserBooks[Index], Obj);
                Obj.CurrentPage = BookInfo;
            }
        }

        public ICommand name_Sorting => new DelegateCommand(NameSorting);

        private void NameSorting()
        {
            var nameOrdered = from b in All_UserBooks orderby b.Title select b;
            int i = 0;
            foreach (Book b in nameOrdered)
            {
                All_UserBooks.RemoveAt(i);
                All_UserBooks.Insert(i, b);
                i++;
            }
        }

        public ICommand author_Sorting => new DelegateCommand(AuthorSorting);

        private void AuthorSorting()
        {
            var nameOrdered = from b in All_UserBooks orderby b.Author select b;
            int i = 0;
            foreach (Book b in nameOrdered)
            {
                All_UserBooks.RemoveAt(i);
                All_UserBooks.Insert(i, b);
                i++;
            }
        }

        public ICommand open_AddWindow => new DelegateCommand(Open_AddWindow);

        private void Open_AddWindow()
        {
            AddWindow aw = new AddWindow();
            aw.Show();
        }
    }
}
