using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Quiz
{
    public class UserManager
    {
        public List<User> Users { get; private set; }

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

        // may not be needed
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

        public void RemoveUser()
        {
            Console.Clear();
            Console.WriteLine("Remove User");

            Console.Write("Enter User ID: ");
            int id = int.Parse(Console.ReadLine());

            User user = FindUserByID(id);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Users.Remove(user);

            Console.WriteLine("User removed successfully.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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

        public void UpdateUserDetails()
        {
            Console.Clear();
            Console.WriteLine("Update User");

            Console.Write("Enter User ID: ");
            int id = int.Parse(Console.ReadLine());

            User user = FindUserByID(id);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write($"New username (leave blank to keep '{user.Username}'): ");
            string newUsername = Console.ReadLine();

            Console.Write($"New email (leave blank to keep '{user.Email}'): ");
            string newEmail = Console.ReadLine();

            Console.Write("Change password? (Y/N): ");
            string changePwd = Console.ReadLine().ToUpper();

            string newPassword = user.Password;
            if (changePwd == "Y")
            {
                Console.Write("Enter new password: ");
                newPassword = Console.ReadLine();
            }

            // apply changes to the existing user object
            if (!string.IsNullOrWhiteSpace(newUsername))
                user.Username = newUsername;

            if (!string.IsNullOrWhiteSpace(newEmail))
                user.Email = newEmail;

            user.Password = newPassword;

            Console.WriteLine("User updated successfully.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public User CreateUser()
        {
            Console.Clear();
            Console.WriteLine("Create New User");

            Console.Write("Enter User ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Is this user a Student or Admin? (S/A): ");
            string type = Console.ReadLine().ToUpper();

            User newUser;

            if (type == "S")
            {
                Console.Write("Enter Status: ");
                string status = Console.ReadLine();

                newUser = new Student(id, username, password, email, status);
            }
            else
            {
                DateTime loginDate = DateTime.Now;
                newUser = new Admin(id, username, password, email, loginDate);
            }

            return newUser;
        }
    }

}
