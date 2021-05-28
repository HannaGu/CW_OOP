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
            Users = new UserLibraryPage();
            Recomend = new RecomendationsPage();
            Statistics = new StatisticsPage();
            GanreStatistics = new GanreStatView();
           
            CurrentPage = Library;
        }
        private Page Main;
        private Page Library;
        private Page Users;
        private Page Recomend;
        private Page Statistics;
        private Page GanreStatistics;
      

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

        public ICommand open_Recom => new DelegateCommand(Open_Recom);

        private void Open_Recom()
        {
            CurrentPage = Recomend;
        }

        public ICommand open_Stat => new DelegateCommand(Open_Stat);

        private void Open_Stat()
        {
            CurrentPage = Statistics;
        }

        public ICommand open_GanreStat => new DelegateCommand(Open_GanreStat);

        private void Open_GanreStat()
        {
            CurrentPage = GanreStatistics;
        }


        public ICommand open_UserLibrary => new DelegateCommand(Open_UserLibrary);

        private void Open_UserLibrary()
        {
            CurrentPage = Users;
        }



        public ICommand open_Main => new DelegateCommand(Open_Main);

        private void Open_Main()
        {
            Page Main = new MainPage();
            CurrentPage = Main;
        }
        #endregion
       
    }
}
