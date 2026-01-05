using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Quiz
{
    public class UserManager
    {
        // Path to the CSV file where users are stored
        private static readonly string filePath =
            Path.Combine(AppContext.BaseDirectory, "users.csv");

        // List that stores all users in memory
        public List<User> Users { get; private set; }

        // Constructor – makes sure the Users list is never null
        public UserManager(List<User> users)
        {
            Users = users ?? new List<User>();
        }

        // Adds a new user and saves the updated list to the CSV file
        public void AddUser(User user)
        {
            // Stops the program if a null user is passed in
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Checks if the user ID already exists
            if (Users.Any(u => u.UserId == user.UserId))
                throw new InvalidOperationException(
                    $"A user with ID {user.UserId} already exists.");

            // Add user to the list
            Users.Add(user);

            // Create CSV header
            var lines = new List<string>
            {
                "UserID,Username,Password,Email,Role,Status,LoginDate"
            };

            // Write each user to the CSV
            foreach (var u in Users)
            {
                if (u is Student student)
                {
                    lines.Add(
                        $"{student.UserId}, {student.Username}, {student.Password}, {student.Email}, Student, {student.Status},");
                }
                else if (u is Admin admin)
                {
                    lines.Add(
                        $"{admin.UserId}, {admin.Username}, {admin.Password}, {admin.Email}, Admin, {admin.LoginDate:O}");
                }
            }

            // Save everything to the file
            File.WriteAllLines(filePath, lines);
        }

        // Updates an existing user’s details
        public void UpdateUser(User updatedUser)
        {
            if (updatedUser == null)
                throw new ArgumentNullException(nameof(updatedUser));

            // Find the user by ID
            var existing = FindUserByID(updatedUser.UserId);

            // If user doesn’t exist, stop
            if (existing == null)
                throw new InvalidOperationException(
                    $"User with ID {updatedUser.UserId} not found.");

            // Update details
            existing.Username = updatedUser.Username;
            existing.Password = updatedUser.Password;
            existing.Email = updatedUser.Email;
            existing.Role = updatedUser.Role;
        }

        // Removes a user based on ID entered in the console
        public void RemoveUser()
        {
            Console.WriteLine("Remove User");

            Console.Write("Enter User ID: ");
            int id = int.Parse(Console.ReadLine());

            // Try to find the user
            User user = FindUserByID(id);

            // If not found, show message and exit
            if (user == null)
            {
                Console.WriteLine("User not found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Remove user from the list
            Users.Remove(user);

            // Rebuild CSV file
            var lines = new List<string>
            {
                "UserId,Username,Password,Email,Role,Status,LoginDate"
            };

            foreach (var u in Users)
            {
                if (u is Student student)
                {
                    lines.Add(
                        $"{student.UserId},{student.Username},{student.Password},{student.Email},Student,{student.Status},");
                }
                else if (u is Admin admin)
                {
                    lines.Add(
                        $"{admin.UserId},{admin.Username},{admin.Password},{admin.Email},Admin,,{admin.LoginDate:O}");
                }
            }

            // Save updated file
            File.WriteAllLines(filePath, lines);

            Console.WriteLine("User removed successfully.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Changes the status of a student (e.g. Active / Suspended)
        public void ChangeStudentStatus(int studentUserId, string newStatus)
        {
            // Find student by ID
            var student = FindUserByID(studentUserId) as Student;

            // If not found or not a student, stop
            if (student == null)
                throw new InvalidOperationException(
                    $"Student with ID {studentUserId} not found.");

            // Update status
            student.SetStatus(newStatus);

            // Rewrite CSV file
            var lines = new List<string>
            {
                "UserId,Username,Password,Email,Role,Status,LoginDate"
            };

            foreach (var u in Users)
            {
                if (u is Student s)
                {
                    lines.Add(
                        $"{s.UserId},{s.Username},{s.Password},{s.Email},Student,{s.Status},");
                }
                else if (u is Admin admin)
                {
                    lines.Add(
                        $"{admin.UserId},{admin.Username},{admin.Password},{admin.Email},Admin,,{admin.LoginDate:O}");
                }
            }

            File.WriteAllLines(filePath, lines);
        }

        // Finds a user by their ID
        public User FindUserByID(int userId)
        {
            return Users.FirstOrDefault(u => u.UserId == userId);
        }

        // Returns a copy of all users
        public List<User> GetAllUsers()
        {
            return new List<User>(Users);
        }

        // Allows admin to update user details through the console
        public void UpdateUserDetails()
        {
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

            // Apply changes only if input was given
            if (!string.IsNullOrWhiteSpace(newUsername))
                user.Username = newUsername;

            if (!string.IsNullOrWhiteSpace(newEmail))
                user.Email = newEmail;

            user.Password = newPassword;

            // Rewrite CSV with updated info
            var lines = new List<string>
            {
                "UserId,Username,Password,Email,Role,Status,LoginDate"
            };

            foreach (var u in Users)
            {
                if (u is Student student)
                {
                    lines.Add(
                        $"{student.UserId},{student.Username},{student.Password},{student.Email},Student,{student.Status},");
                }
                else if (u is Admin admin)
                {
                    lines.Add(
                        $"{admin.UserId},{admin.Username},{admin.Password},{admin.Email},Admin,,{admin.LoginDate:O}");
                }
            }

            File.WriteAllLines(filePath, lines);

            Console.WriteLine("User updated successfully.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Creates a new user by asking for details in the console
        public User CreateUser()
        {
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

        // Loads all users from the CSV file into the Users list
        public void LoadUsersFromFile()
        {
            Users.Clear();

            // If file doesn’t exist, just exit
            if (!File.Exists(filePath))
                return;

            // Skip header line
            var lines = File.ReadAllLines(filePath).Skip(1);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',', StringSplitOptions.TrimEntries);
                if (parts.Length < 5)
                    continue;

                if (!int.TryParse(parts[0], out int id))
                    continue;

                string username = parts[1];
                string password = parts[2];
                string email = parts[3];
                string role = parts[4];

                if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    string status = parts.Length > 5 ? parts[5] : "Active";
                    Users.Add(new Student(id, username, password, email, status));
                }
                else if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    DateTime loginDate = DateTime.Now;

                    if (parts.Length > 6 &&
                        DateTime.TryParse(parts[6], out DateTime parsed))
                    {
                        loginDate = parsed;
                    }

                    Users.Add(new Admin(id, username, password, email, loginDate));
                }
            }
        }
    }
}
