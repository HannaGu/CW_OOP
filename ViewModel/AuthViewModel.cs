using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;
using CW_WPF.View;
using CW_WPF.DB;
using System.Windows;

namespace CW_WPF.ViewModel
{
    class AuthViewModel : ViewModelBase
    {
        public string login { get; set; }
        public string password { get; set; }
        private string errorMes;
        public string ErrorMes
        {
            get { return errorMes; }
            set
            {
                this.errorMes = value;
                RaisePropertiesChanged(nameof(ErrorMes));
            }
        }
        bool flag = false;
        bool canreg = true;
        public string Error => throw new NotImplementedException();
        public AuthViewModel()
        {

        }
        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand close => new DelegateCommand(Close);
        public ICommand registration => new DelegateCommand(Reg);

        public void Reg()
        {
            RegisterView r = new RegisterView();
            r.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            r.Show();
            Close();
        }

        public ICommand auth => new DelegateCommand(Auth);
        public void Auth()
        {
            bool fl = true;
            bool IsDone = true;
            ErrorMes = "";
            flag = true;
            login += " ";
            int x1 = login.Length - 1;
            login = login.Substring(0, x1);
            if (login == null || password == null || login == String.Empty)
                ErrorMes = Properties.Resources.emptyfield;
            else
            {
                if (fl && canreg)
                {
                    DataBaseUser spam = new DataBaseUser();
                    string Pass = DB.DB.Hash(password).ToString();
                    IsDone = spam.GiveUserByLoginAndPassword(login, Pass);
                    if (IsDone)
                    {
                        Properties.Settings.Default.User = login;
                        Properties.Settings.Default.IdUser = spam.GetIdUserByLogin(login);
                        Properties.Settings.Default.Save();
                        bool isAdmin;
                        isAdmin = spam.GetIsAdminUser(Properties.Settings.Default.IdUser);

                        if (isAdmin)
                        {
                            AdminWindow sp = new AdminWindow();
                            sp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            sp.Show();
                            Close();
                        }
                        else
                        {
                            MainWindow sp = new MainWindow();
                            sp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            sp.Show();
                            Close();
                        }

                    }
                }
                if (!IsDone)
                {
                    ErrorMes = Properties.Resources.nosuchuser;
                }

                flag = false;
                canreg = true;
            }
        }
    }

}
