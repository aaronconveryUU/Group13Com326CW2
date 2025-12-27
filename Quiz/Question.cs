using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class Question
    {
        // Private fields
        protected int questionID;
        private string questionText;
        private List <string> questionOptions;
        private string questionCorrectAnswer;
        private string questionDifficultyLevel;

        // Public properties
        public int QuestionID
        {
            get {return questionID;}
        }

        public string QuestionText
        {
            get {return questionText;}
            set {questionText = value;}
        }

        public List <string> QuestionOptions
        {
            get {return questionOptions;}
            set {questionOptions = value;}
        }

        public string QuestionCorrectAnswer
        {
            get {return questionCorrectAnswer;}
            set {questionCorrectAnswer = value;}
        }

        public string QuestionDifficultyLevel
        {
            get {return questionDifficultyLevel;}
            set {questionDifficultyLevel = value;}
        }

        // Constructor
        public Question(int questionID, string questionText, List <string> questionOptions, string questionCorrectAnswer, string questionDifficultyLevel)
        {
            this.QuestionID = questionID;
            this.QuestionText = questionText;
            this.QuestionOptions = questionOptions;
            this.QuestionCorrectAnswer = questionCorrectAnswer;
            this.QuestionDifficultyLevel = questionDifficultyLevel;
        }

        // Methods
        public IsCorrect()
        {

        }

        public AddQuestion()
        {

        }

        public UpdateQuestion()
        {

        }

        public RemoveQuestion()
        {

        }

        public FindQuestionByID()
        {

        }

        public GetAllQuestions()
        {

        }
    }
}
