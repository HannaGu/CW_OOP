using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_WPF.DB;
using DevExpress.Mvvm;
using LiveCharts;
using System.Windows.Media;
using LiveCharts.Wpf;


namespace CW_WPF.ViewModel
{
   public  class GanreStatViewModel:ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public DataBaseUser dbu = new DataBaseUser();

        public GanreStatViewModel()
        {
            SeriesCollection = new SeriesCollection
            {

                new ColumnSeries
                {
                    Title = "Прочитано",
                    Values = GetParam()
                }
            };
            Labels = new[] { "бизнес-литература", "детектив	", "детская литература", "исторический роман", "классическая литература", "комикс", "любовный роман", "научнпоп", "ужасы", "учебная литература", "фантастика", "фэнтези", "юмор и сатира" };
            YFormatter = value => value.ToString("C");
        }

        private ChartValues<int> GetParam()
        {
            ChartValues<int> Item = dbu.GetProgressByGanre();
            return Item;
        }
    }
}
