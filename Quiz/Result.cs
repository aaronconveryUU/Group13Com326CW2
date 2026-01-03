using System;
using System.Collections.Generic;

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
        public void AddQuizResult()
        {
            results.Add(this);
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
                if (result.Student == student)
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
    }
}