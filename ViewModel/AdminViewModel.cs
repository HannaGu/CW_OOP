 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using CW_WPF.View;
using CW_WPF.Model;
using CW_WPF.DB;
using System.Windows;
using System.Collections.ObjectModel;


namespace CW_WPF.ViewModel
{
   public  class AdminViewModel: ViewModelBase
    {
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

        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand close => new DelegateCommand(Close);
        #region 
        public AdminViewModel()
        {
            Main = new MainPage();
            Library = new LibraryPage();
            AdminView_Users = new AdminView_Users();            

            CurrentPage =Library;
        }
        private Page Main;
        private Page Library;
        private Page AdminView_Users;
     

        public ICommand logout => new DelegateCommand(Logout);

        private void Logout()
        {

            AuthView _win = new AuthView();
            _win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _win.Show();
            Close();
        }


        public ICommand open_Library => new DelegateCommand(Open_Library);

        private void Open_Library()
        {
            Library = new LibraryPage();
            CurrentPage = Library;
        }

        public ICommand open_Users => new DelegateCommand(Open_Users);

        private void Open_Users()
        {
            CurrentPage = AdminView_Users;
        }

        public ICommand open_Main => new DelegateCommand(Open_Main);

        private void Open_Main()
        {
            Page Main = new MainPage();
            CurrentPage = Main;
        }
        #endregion
       
        public ICommand open_AddWindow => new DelegateCommand(Open_AddWindow);

        private void Open_AddWindow()
        {
            AddWindow aw = new AddWindow();
            aw.Show();
        }
     
    }
}
