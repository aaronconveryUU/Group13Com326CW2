using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class User
    {
        public int UserId { get; private set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public User(int userId, string username, string password, string email, string role)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }

        public override string ToString()
        {
            return $"ID: {UserId}, Username: {Username}, Email: {Email}, Role: {Role}";
        }
        public bool CheckPassword(string password)
        {
            return Password == password;
        }
    }
}
