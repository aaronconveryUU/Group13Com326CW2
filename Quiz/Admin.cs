using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class Admin : User
    {
        public DateTime LoginDate { get; private set; }

        public Admin(int userId, string username, string password, string email, DateTime loginDate)
            : base(userId, username, password, email, role : "Admin")
        {
            LoginDate = loginDate;
        }

        public void UpdateLoginDate()
        {
            LoginDate = DateTime.Now;
        }
    }
}
