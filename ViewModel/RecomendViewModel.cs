using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Controls;
using System.Windows.Input;

using CW_WPF.Model;
using CW_WPF.View;
using CW_WPF.ViewModel;
using CW_WPF.DB;


namespace CW_WPF.ViewModel
{
    public class RecomendViewModel:ViewModelBase
    {
        private Page All_BooksPage;

        public RecomendViewModel()
        {
            All_BooksPage = new RecomendBooks(this);
            CurrentPage = All_BooksPage;
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

