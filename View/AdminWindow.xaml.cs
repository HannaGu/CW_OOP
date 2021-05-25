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

namespace CW_WPF.ViewModel
{
    public partial class AdminWindow : Window
    {
        AdminViewModel avm;
        public AdminWindow()
        {
            avm = new AdminViewModel();
            InitializeComponent();
            DataContext = avm;
        }
    }
}
