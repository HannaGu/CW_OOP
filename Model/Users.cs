using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_WPF.Model
{
    public class Users
    {
        public int Id_user { get; set; }
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool Is_admin { get; set; }

        public Users(int id_user, string login, string ln, string fn, bool is_admin)
        {
            Id_user = id_user;
            Login = login;
            LastName = ln;
            FirstName = fn;
            Is_admin = is_admin;
        }
     
        public Users() { }
    }
}
