using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using CW_WPF.ViewModel ;
using CW_WPF.Model;
using CW_WPF.DB;
using System.Windows;

namespace CW_WPF.View
{
    public partial class AdminView_Users : Page
    {
        AdminViewModel_Users avm;
        public AdminView_Users()
        {
            avm = new AdminViewModel_Users();
            InitializeComponent();
            DataContext = avm;
        }
    }
}
