using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_WPF.Model;
using CW_WPF.View;
using CW_WPF.DB;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Controls;
using LiveCharts;
using System.Windows.Media;
using LiveCharts.Wpf;

namespace CW_WPF.ViewModel
{
    public class StatisticsViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public DataBaseUser dbu = new DataBaseUser();
        
        public StatisticsViewModel()
        {
            SeriesCollection = new SeriesCollection
            {

                new ColumnSeries
                {
                    Title = "Прочитано",
                    Values = GetParam()
                }
            };
            Labels = new[] { "январь", "февраль", "март", "апрель", "май","июнь","июль","август","сентябрь","октябрь","ноябрь","декабрь" };
            YFormatter = value => value.ToString("C");
            }

            private ChartValues<int> GetParam()
            {
                ChartValues<int> Item = dbu.GetProgressByTime();
                return Item;
            }
       
    }    
}
