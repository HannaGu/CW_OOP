using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_WPF.Model;
using CW_WPF.DB;
using DevExpress.Mvvm;
using System.Windows.Controls;


using CW_WPF.View;

namespace CW_WPF.ViewModel
{
   public class UserLibraryViewModel:ViewModelBase
    {
        private Page User_BooksPage;
        public UserLibraryViewModel()
        {
            User_BooksPage = new UserBooksPage(this);
            CurrentPage = User_BooksPage;
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
    }
}
