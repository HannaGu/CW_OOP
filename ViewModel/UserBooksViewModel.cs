﻿using System;
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
            Page BookInfo = new UserBookPage(All_UserBooks[Index], Obj);
            Obj.CurrentPage = BookInfo;
        }
    }
}