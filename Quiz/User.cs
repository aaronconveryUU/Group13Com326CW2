using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    // Base class for all users (students, admins, etc.)
    public class User
    {
        // Each user has a unique ID (can't change after creation)
        public int UserId { get; private set; }

        // User info that can be updated
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Constructor – called when we make a new user
        public User(int userId, string username, string password, string email, string role)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }

        // Makes it easy to print user info
        public override string ToString()
        {
            return $"ID: {UserId}, Username: {Username}, Email: {Email}, Role: {Role}";
        }

        // Check if a password matches this user's password
        public bool CheckPassword(string password)
        {
            return Password == password;
        }
    }
}
