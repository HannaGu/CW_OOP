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
using System.Windows.Shapes;
using CW_WPF.ViewModel;

namespace CW_WPF.View
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        AddViewModel avm;
        public AddWindow()
        {
            avm = new AddViewModel();
            InitializeComponent();
            DataContext = avm;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
        }
    }
}
