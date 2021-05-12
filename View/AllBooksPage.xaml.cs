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

namespace CW_WPF.View
{
    /// <summary>
    /// Логика взаимодействия для AllBooksPage.xaml
    /// </summary>
    public partial class AllBooksPage : Page
    {
        AllBooksViewModel abvm;
        public AllBooksPage(LibraryViewModel obj)
        {
            abvm = new AllBooksViewModel(obj);
            InitializeComponent();
            DataContext = abvm;
        }

        private void bookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
