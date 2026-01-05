using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    // Admin class inherits from User
    public class Admin : User
    {
        // Keeps track of last login time
        public DateTime LoginDate { get; private set; }

        // Constructor – sets up a new admin
        public Admin(int userId, string username, string password, string email, DateTime loginDate)
            : base(userId, username, password, email, role: "Admin") // role is always "Admin"
        {
            LoginDate = loginDate;
        }

        // Update the login date to now
        public void UpdateLoginDate()
        {
            LoginDate = DateTime.Now;
        }
    }
}
