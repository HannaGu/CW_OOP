using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevExpress.Mvvm;


using CW_WPF.Model;
using CW_WPF.ViewModel;


namespace CW_WPF.View
{
    public partial class UserBookPage : Page
    {
        UserBookViewModel ubvm;
        public UserBookPage(Book obj1, UserLibraryViewModel obj2)
        {
            ubvm = new UserBookViewModel(obj1, obj2);
            InitializeComponent();
            DataContext = ubvm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
