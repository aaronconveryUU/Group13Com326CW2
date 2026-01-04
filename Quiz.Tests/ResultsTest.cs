using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Quiz;

namespace Quiz.Tests
{
    // Simple stub classes for testing
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
    }

    public class Quiz
    {
        public int QuizID { get; set; }
        public string Title { get; set; }
    }

    [TestClass]
    public class ResultTests
    {
        private Student student;
        private Quiz quiz;
        private Result result;

        [TestInitialize]
        public void Setup()
        {
            student = new Student { StudentID = 1, Name = "Adam" };
            quiz = new Quiz { QuizID = 1, Title = "OOP Quiz" };
            result = new Result(1, student, quiz, 8, 10, DateTime.Now);
        }

        // -------------------------
        // CalculatePercentage Tests
        // -------------------------

        [TestMethod]
        public void CalculatePercentage_ValidScore_ReturnsCorrectPercentage()
        {
            // Act
            double percentage = result.CalculatePercentage();

            // Assert
            Assert.AreEqual(80, percentage);
        }

        [TestMethod]
        public void CalculatePercentage_ZeroTotalQuestions_ReturnsZero()
        {
            // Arrange
            Result zeroTotal = new Result(2, student, quiz, 5, 0, DateTime.Now);

            // Act
            double percentage = zeroTotal.CalculatePercentage();

            // Assert
            Assert.AreEqual(0, percentage);
        }

        // -------------------------
        // GetFeedbackMessage Tests
        // -------------------------

        [TestMethod]
        public void GetFeedbackMessage_HighScore_ReturnsExcellent()
        {
            // Arrange
            Result highScore = new Result(3, student, quiz, 9, 10, DateTime.Now);

            // Act
            string feedback = highScore.GetFeedbackMessage();

            // Assert
            Assert.AreEqual("Excellent performance!", feedback);
        }

        [TestMethod]
        public void GetFeedbackMessage_PassScore_ReturnsGoodEffort()
        {
            // Arrange
            Result passScore = new Result(4, student, quiz, 6, 10, DateTime.Now);

            // Act
            string feedback = passScore.GetFeedbackMessage();

            // Assert
            Assert.AreEqual("Good effort, you passed.", feedback);
        }

        [TestMethod]
        public void GetFeedbackMessage_FailScore_ReturnsNeedsImprovement()
        {
            // Arrange
            Result failScore = new Result(5, student, quiz, 3, 10, DateTime.Now);

            // Act
            string feedback = failScore.GetFeedbackMessage();

            // Assert
            Assert.AreEqual("Needs improvement. Try again.", feedback);
        }

        // -------------------------
        // Result List Tests
        // -------------------------

        [TestMethod]
        public void AddQuizResult_AddsResultToList()
        {
            // Act
            result.AddQuizResult();
            List<Result> allResults = Result.GetAllResults();

            // Assert
            Assert.IsTrue(allResults.Contains(result));
        }

        [TestMethod]
        public void RemoveQuizResult_RemovesResultFromList()
        {
            // Arrange
            result.AddQuizResult();

            // Act
            result.RemoveQuizResult();
            List<Result> allResults = Result.GetAllResults();

            // Assert
            Assert.IsFalse(allResults.Contains(result));
        }

        [TestMethod]
        public void FindResultsByStudent_ReturnsOnlyThatStudentsResults()
        {
            // Arrange
            Student otherStudent = new Student { StudentID = 2, Name = "Other" };

            Result r1 = new Result(6, student, quiz, 7, 10, DateTime.Now);
            Result r2 = new Result(7, otherStudent, quiz, 5, 10, DateTime.Now);

            r1.AddQuizResult();
            r2.AddQuizResult();

            // Act
            List<Result> studentResults = Result.FindResultsByStudent(student);

            // Assert
            Assert.AreEqual(1, studentResults.Count);
            Assert.AreEqual(student, studentResults[0].Student);
        }
    }
}