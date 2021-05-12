using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using CW_WPF.View;
using CW_WPF.DB;
using System.Windows;

namespace CW_WPF.ViewModel
{
    public class MainViewModel:ViewModelBase
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
        public MainViewModel()
        {
            Main = new MainPage();
            Library = new LibraryPage();
            UserLibrary = new UserLibraryPage();
            Statictic = new StatisticsPage();
            Recomendations = new RecomendationsPage();

            CurrentPage = Main;
        }
        private Page Main;
        private Page Library;
        private Page UserLibrary;
        private Page Statictic;
        private Page Recomendations;

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
            CurrentPage = Library;
        }

        public ICommand open_UserLibrary => new DelegateCommand(Open_UserLibrary);

        private void Open_UserLibrary()
        {
          CurrentPage = UserLibrary;
        }

        public ICommand open_Main => new DelegateCommand(Open_Main);

        private void Open_Main()
        {
            Page Main = new MainPage();
            CurrentPage = Main;
        }
        #endregion
        public ICommand open_Stat => new DelegateCommand(Open_Stat);

        private void Open_Stat()
        {
            Statictic = new StatisticsPage();
            CurrentPage = Statictic;
        }

        public ICommand open_Recom => new DelegateCommand(Open_Recom);

        private void Open_Recom()
        {
            Recomendations = new RecomendationsPage();
            CurrentPage = Recomendations;
        }

        public ICommand open_AddWindow=> new DelegateCommand(Open_AddWindow);

        private void Open_AddWindow()
        {
            AddWindow aw= new AddWindow();
           aw.Show();
            
        }
    }
}
