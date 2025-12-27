using System;
using System.Collections.Generic;
using System.Text;

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

        // Public properties
        public int ResultID 
        {
            get {return resultID;}
        }

        public Student Student
        {
            get {return student;}
            set {student = value;}
        }

        public Quiz Quiz
        {
            get {return quiz;}
            set {quiz = value;}
        }

        public int Score
        {
            get {return score;}
            set {score = value;}
        }

        public int TotalQuestions
        {
            get {return totalQuestions;}
            set {totalQuestions = value;}
        }

        public DateTime AttemptDate
        {
            get {return attemptDate;}
            set {attemptDate = value;}
        }

        // Constructor
        public Result(int resultID, Student student, Quiz quiz, int score, int totalQuestions, DateTime attemptDate)
        {
            this.ResultID = resultID;
            this.Student = student;
            this.Quiz = quiz;
            this.Score = score;
            this.TotalQuestions = totalQuestions;
            this.AttemptDate = attemptDate;
        }

        // Methods
        public CalculatePercentage()
        {

        }

        public GetFeedbackMessage()
        {

        }

        public AddQuizResult()
        {

        }

        public RemoveQuizResult()
        {

        }

        public FindResultsByStudent()
        {

        }

        public GetAllResults()
        {

        }
    }
}
