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
using CW_WPF.ViewModel;
using CW_WPF.Model;

namespace CW_WPF.View
{
    
    public partial class AdminBookPage : Page
    {
        AdminBookViewModel abvm;
        public AdminBookPage(Book obj1, LibraryViewModel obj2)
        {
            abvm = new AdminBookViewModel(obj1, obj2);
            InitializeComponent();
            DataContext = abvm;
        }
    }
}
