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
            Console.WriteLine($"Results for {Username}");
            Console.WriteLine("----------------------------");

            List<Result> studentResults = Result.FindResultsByStudent(this);

            if (studentResults.Count == 0)
            {
                Console.WriteLine("No results found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            foreach (Result result in studentResults)
            {
                Console.WriteLine($"Quiz: {result.Quiz.QuizTitle}");
                Console.WriteLine($"Score: {result.Score}/{result.TotalQuestions}");
                Console.WriteLine($"Percentage: {result.CalculatePercentage():0.00}%");
                Console.WriteLine($"Feedback: {result.GetFeedbackMessage()}");
                Console.WriteLine($"Attempt Date: {result.AttemptDate}");
                Console.WriteLine("-----------------------------");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
