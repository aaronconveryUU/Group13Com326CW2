using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class Quiz
    {
                // Private fields
        protected int quizID;
        private string quizTitle;
        private string quizDescription;
        private Category quizCategory;
        private List <Question> quizQuestions;
        private DateTime quizDate;

        // Public properties
        public int QuizID
        {
            get {return quizID;}
        }

        public string QuizTitle
        {
            get {return quizTitle;}
            set {quizTitle = value;}
        }

        public string QuizDescription
        {
            get {return quizDescription;}
            set {quizDescription = value;}
        }

        public Category QuizCategory
        {
            get {return quizCategory;}
            set {quizCategory = value;}
        }

        public List <Question> QuizQuestions
        {
            get {return quizQuestions;}
            set {quizQuestions = value;}
        }

        public DateTime QuizDate
        {
            get {return quizDate;}
            set {quizDate = value;}
        }  

        // Constructor
        public Quiz(int quizID, string quizTitle, string quizDescription, Category quizCategory, List <Question> quizQuestions, DateTime quizDate)
        {
            this.QuizID = quizID;
            this.QuizTitle = quizTitle;
            this.QuizDescription = quizDescription;
            this.QuizCategory = quizCategory;
            this.QuizQuestions = quizQuestions;
            this.QuizDate = quizDate;
        }

        // Methods
        public AddQuestionToQuiz()
        {

        }

        public RempoveQuestionFromQuiz()
        {

        }

        public AddQuiz()
        {

        }

        public UpdateQuiz()
        {

        }

        public RemoveQuiz()
        {

        }

        public FindQuizByID()
        {

        }

        public GetAllQuizzes()
        {

        }

        public SaveQuizzesToCSV()
        {

        }
    }
}
