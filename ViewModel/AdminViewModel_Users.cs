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
    public class AdminViewModel_Users:ViewModelBase
    {
        public ObservableCollection<Users> All_Users { get; set; }
        DataBaseUser db_GetUser = new DataBaseUser();
        
        public AdminViewModel_Users()
        {
            All_Users = db_GetUser.GetUsers();
         
        }

       

    }
}
