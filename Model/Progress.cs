using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_WPF.Model
{
    public class Progress : INotifyPropertyChanged
    {
        public int id_user;
        public int isbn;
        public DateTime date_change;
        public string status;

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion
}
}
