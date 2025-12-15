using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class Student : User
    {
        public string Status { get; private set; }

        public Student(int userId, string username, string password, string email, string status)
            : base(userId, username, password, email, role: "Student")
        {
            Status = status;
        }

        public void SetStatus(string status)
        {
            Status = status;
        }

        public void ViewResults()
        {

        }
    }
}
