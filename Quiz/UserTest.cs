using Quiz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz
{
    public class UserTest
    {
        public static void Main(string[] args)
        {
            UserManager userManager = new UserManager(new List<User>());

            Console.WriteLine("=== USER & RESULT TESTING ===\n");

            Admin admin1 = new Admin(1, "admin1", "pass123", "admin1@test.com", DateTime.Now);
            Student student1 = new Student(2, "student1", "stud123", "student1@test.com", "Active");
            Student student2 = new Student(3, "student2", "stud234", "student2@test.com", "Inactive");

            userManager.AddUser(admin1);
            userManager.AddUser(student1);
            userManager.AddUser(student2);

            Console.WriteLine("Added 1 Admin and 2 Students.\n");

            Console.WriteLine("All users in the system:");
            foreach (var u in userManager.GetAllUsers())
            {
                Console.WriteLine($"{u.Username} ({u.Role})");
            }
            Console.WriteLine();

            Console.WriteLine("Testing password checks:");
            TestLogin(userManager, "admin1", "pass123");   
            TestLogin(userManager, "admin1", "wrongpass"); 
            TestLogin(userManager, "student1", "stud123"); 
            TestLogin(userManager, "student2", "stud234"); 
            TestLogin(userManager, "student2", "wrong");   
            TestLogin(userManager, "nonexistent", "pass"); 
            Console.WriteLine();

            Console.WriteLine("Testing Admin functionality:");
            Console.WriteLine($"Original login date: {admin1.LoginDate}");
            admin1.UpdateLoginDate();
            Console.WriteLine($"Updated login date: {admin1.LoginDate}\n");

            Console.WriteLine("Testing Student functionality:");
            Console.WriteLine($"Original status for {student2.Username}: {student2.Status}");
            student2.SetStatus("Active");
            Console.WriteLine($"Updated status for {student2.Username}: {student2.Status}\n");

            Console.WriteLine("Testing Result functionality:\n");

            Quiz quiz = new Quiz(1, "Test Quiz", "Dummy quiz for testing", 101, "10 question", DateTime.Now);

            Result result1 = new Result(1, student1, quiz, 8, 10, DateTime.Now);
            Result result2 = new Result(2, student1, quiz, 5, 10, DateTime.Now);

            result1.AddQuizResult();
            result2.AddQuizResult();

            Console.WriteLine("Added quiz results for student1.\n");

            Console.WriteLine("Viewing results for student1:");
            student1.ViewResults();

            Console.WriteLine("Viewing results for student2 (no results expected):");
            student2.ViewResults();

            Console.WriteLine("Testing percentage & feedback:");
            Console.WriteLine($"Score: {result1.Score}/{result1.TotalQuestions}");
            Console.WriteLine($"Percentage: {result1.CalculatePercentage():0.00}%");
            Console.WriteLine($"Feedback: {result1.GetFeedbackMessage()}\n");

            Console.WriteLine("Removing user...");
            userManager.RemoveUser();

            Console.WriteLine("\nRemaining users:");
            foreach (var u in userManager.GetAllUsers())
            {
                Console.WriteLine($"{u.Username} ({u.Role})");
            }

            Console.WriteLine("\n=== TESTING COMPLETE ===");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void TestLogin(UserManager userManager, string username, string password)
        {
            var user = userManager.GetAllUsers().FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                Console.WriteLine($"Login attempt: {username} - User not found.");
                return;
            }

            bool success = user.CheckPassword(password);
            Console.WriteLine($"Login attempt: {username} with password '{password}' -> {(success ? "SUCCESS" : "FAIL")}");
        }
    }
}

