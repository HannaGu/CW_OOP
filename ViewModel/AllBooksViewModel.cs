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
        public ObservableCollection<Book> All_Books { get; set; }
        DB_GetItems db_GetItems = new DB_GetItems();
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

        public ICommand open_BookPage => new DelegateCommand(OpenBookPage);

        private void OpenBookPage()
        {
            Page BookInfo = new BookPage(All_Books[Index], Obj);
            Obj.CurrentPage = BookInfo;
        }

        
    }
}
