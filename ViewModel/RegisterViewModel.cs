using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;

using CW_WPF.DB;
using CW_WPF.View;

namespace CW_WPF.ViewModel
{
    class RegisterViewModel :ViewModelBase
    {

        public RegisterViewModel()
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
        private string _login;
        public string login
        {
            get { return _login; }
            set
            {
                this._login = value;
                RaisePropertiesChanged(nameof(login));
            }
        }
        private string _password;
        public string password
        {
            get { return _password; }
            set
            {
                this._password = value;
                RaisePropertiesChanged(nameof(password));
            }
        }
        
        
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

        private string _firstname;
        public string firstname
        {
            get { return _firstname; }
            set
            {
                this._firstname = value;
                RaisePropertiesChanged(nameof(firstname));
            }
        }

        private string _lastname;
        public string lastname
        {
            get { return _lastname; }
            set
            {
                this._lastname = value;
                RaisePropertiesChanged(nameof(lastname));
            }
        }

        bool flag;
        bool canreg = true;
        public ICommand register => new DelegateCommand(RegisterCommand);
        public void RegisterCommand()
        {
            try
            {
                ErrorMes = "";
                flag = true;
                login += " ";
                int x1 = login.Length - 1;
                login = login.Substring(0, x1);
                bool fl = true;

                
                if (password == String.Empty || password == null || lastname == String.Empty || lastname == null || firstname == null || firstname == String.Empty)
                {
                    fl = false;
                    ErrorMes = Properties.Resources.emptyfield;
                }
                else if(password.Length < 8)
                {
                    fl = false;
                    ErrorMes = Properties.Resources.charac;
                }

                bool IsDone = true;
                if (fl && canreg)
                {
                    DataBaseUser spam = new DataBaseUser();
                    string Pass = DB.DB.Hash(password).ToString();
                    IsDone = spam.AddUser(login, Pass, firstname, lastname);
                    if (IsDone)
                    {
                        AuthView t = new AuthView();
                        t.Show();
                        Close();
                    }
                }

                if (!IsDone)
                {

                    ErrorMes = Properties.Resources.existserr;
                    login = "";
                }
                canreg = true;
                flag = false;
            }
            catch (SystemException)
            {
                ErrorMes = Properties.Resources.errordata;
            }
        }

        public ICommand back => new DelegateCommand(Back);

        public void Back()
        {
            AuthView t = new AuthView();
            t.Show();
            Close();
        }
    }
}

