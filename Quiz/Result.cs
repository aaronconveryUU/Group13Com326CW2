using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Quiz
{
    public class Result
    {
        // Private fields
        protected int resultID;
        private Student student;
        private Quiz quiz;
        private int score;
        private int totalQuestions;
        private DateTime attemptDate;

        // Simulated storage (since no database is used)
        private static readonly string filePath = Path.Combine(AppContext.BaseDirectory, "results.csv");
        private static List<Result> results = new List<Result>();

        // Public properties
        public int ResultID
        {
            get { return resultID; }
        }

        public Student Student
        {
            get { return student; }
            set { student = value; }
        }

        public Quiz Quiz
        {
            get { return quiz; }
            set { quiz = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int TotalQuestions
        {
            get { return totalQuestions; }
            set { totalQuestions = value; }
        }

        public DateTime AttemptDate
        {
            get { return attemptDate; }
            set { attemptDate = value; }
        }

        // Constructor
        public Result(int resultID, Student student, Quiz quiz, int score, int totalQuestions, DateTime attemptDate)
        {
            this.resultID = resultID;
            this.Student = student;
            this.Quiz = quiz;
            this.Score = score;
            this.TotalQuestions = totalQuestions;
            this.AttemptDate = attemptDate;
        }

        // Calculate percentage score
        public double CalculatePercentage()
        {
            if (totalQuestions == 0)
                return 0;

            return (double)score / totalQuestions * 100;
        }

        // Return feedback based on percentage
        public string GetFeedbackMessage()
        {
            double percentage = CalculatePercentage();

            if (percentage >= 80)
                return "Excellent performance!";
            else if (percentage >= 50)
                return "Good effort, you passed.";
            else
                return "Needs improvement. Try again.";
        }

        // Add this result to the list
        // Add this result to the list AND to results.csv
        public void AddQuizResult()
        {
            // In-memory
            results.Add(this);

            bool fileExists = File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("ResultID,StudentID,QuizID,Score,TotalQuestions,AttemptDate");
                }

                writer.WriteLine(string.Join(",",
                    ResultID,
                    Student.UserId,
                    Quiz.QuizID,
                    Score,
                    TotalQuestions,
                    AttemptDate.ToString("O"))); // ISO 8601
            }
        }


        // Remove this result from the list
        public void RemoveQuizResult()
        {
            results.Remove(this);
        }

        // Find all results for a specific student
        public static List<Result> FindResultsByStudent(Student student)
        {
            List<Result> studentResults = new List<Result>();

            foreach (Result result in results)
            {
                if (result.Student != null && result.Student.UserId == student.UserId)
                {
                    studentResults.Add(result);
                }
            }

            return studentResults;
        }

        // Return all results
        public static List<Result> GetAllResults()
        {
            return results;
        }

        // ===== Loading from CSV at startup =====

        public static void LoadResultsFromFile(UserManager userManager, List<Quiz> quizzes)
        {
            results.Clear();

            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath).Skip(1); // skip header

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                if (parts.Length < 6) continue;

                if (!int.TryParse(parts[0], out int parsedResultId)) continue;
                if (!int.TryParse(parts[1], out int parsedStudentId)) continue;
                if (!int.TryParse(parts[2], out int parsedQuizId)) continue;
                if (!int.TryParse(parts[3], out int parsedScore)) continue;
                if (!int.TryParse(parts[4], out int parsedTotalQuestions)) continue;
                if (!DateTime.TryParse(parts[5], out DateTime parsedAttemptDate)) continue;

                // Find matching student and quiz
                Student student = userManager
                    .GetAllUsers()
                    .OfType<Student>()
                    .FirstOrDefault(s => s.UserId == parsedStudentId);

                Quiz quiz = quizzes.FirstOrDefault(q => q.QuizID == parsedQuizId);

                if (student != null && quiz != null)
                {
                    results.Add(new Result(
                        parsedResultId,
                        student,
                        quiz,
                        parsedScore,
                        parsedTotalQuestions,
                        parsedAttemptDate));
                }
            }
        }

    }
}