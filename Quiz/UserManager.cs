using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class UserManager
    {
        public List<User> Users {  get; private set; }

        public UserManager(List<User> users)
        {
            Users = users ?? new List<User>();
        }

        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (Users.Any(u => u.UserId == user.UserId))
                throw new InvalidOperationException($"A user with ID {user.UserId} already exists.");

            Users.Add(user);
        }

        public void UpdateUser(User updatedUser)
        {
            if (updatedUser == null) throw new ArgumentNullException(nameof(updatedUser));

            var existing = FindUserByID(updatedUser.UserId);
            if (existing == null)
                throw new InvalidOperationException($"User with ID {updatedUser.UserId} not found.");

            existing.Username = updatedUser.Username;
            existing.Password = updatedUser.Password;
            existing.Email = updatedUser.Email;
            existing.Role = updatedUser.Role;
        }

        public void RemoveUser(int userId)
        {
            var user = FindUserByID(userId);
            if (user != null)
            {
                Users.Remove(user);
            }
        }

        public void ChangeStudentStatus(int studentUserId, string newStatus)
        {
            var student = FindUserByID(studentUserId) as Student;
            if (student == null)
                throw new InvalidOperationException($"Student with ID {studentUserId} not found.");

            student.SetStatus(newStatus);
        }

        public User FindUserByID(int userId)
        {
            return Users.FirstOrDefault(u => u.UserId == userId);
        }

        public List<User> GetAllUsers()
        {
            return new List<User>(Users);
        }
    }
}
