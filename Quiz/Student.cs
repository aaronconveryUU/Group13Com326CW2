using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    // Student class inherits from User
    public class Student : User
    {
        // Each student has a status (e.g. Active, Suspended)
        public string Status { get; private set; }

        // Constructor – sets up a new student
        public Student(int userId, string username, string password, string email, string status)
            : base(userId, username, password, email, role: "Student") // role is always "Student"
        {
            Status = status;
        }

        // Change the student's status
        public void SetStatus(string status)
        {
            Status = status;
        }

        // Shows the student's quiz results in the console
        public void ViewResults()
        {
            Console.WriteLine($"Results for {Username}");
            Console.WriteLine("----------------------------");

            // Get all results for this student
            List<Result> studentResults = Result.FindResultsByStudent(this);

            // If no results found, tell them
            if (studentResults.Count == 0)
            {
                Console.WriteLine("No results found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Loop through results and print info
            foreach (Result result in studentResults)
            {
                Console.WriteLine($"Quiz: {result.Quiz.QuizTitle}");
                Console.WriteLine($"Score: {result.Score}/{result.TotalQuestions}");
                Console.WriteLine($"Percentage: {result.CalculatePercentage():0.00}%");
                Console.WriteLine($"Feedback: {result.GetFeedbackMessage()}");
                Console.WriteLine($"Attempt Date: {result.AttemptDate}");
                Console.WriteLine("-----------------------------");
            }

            // Wait before exiting
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
