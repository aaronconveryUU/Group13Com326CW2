using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Quiz;

namespace ResultsTest
{
    // Simple stub classes for testing

    [TestClass]
    public class ResultTests
    {
        private Student student1;
        private Student student2;
        private Quiz.Quiz quiz;
        private Result result;

        [TestInitialize]
        public void Setup()
        {
            Result.GetAllResults().Clear();

            student1 = new Student(2, "student1", "stud123", "student1@test.com", "Active");
            quiz = new Quiz.Quiz(1, "OOP", "Covers basics of OOP", 1, "List of 10 questions", DateTime.Now);
            result = new Result(1, student1, quiz, 8, 10, DateTime.Now);
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
            Result zeroTotal = new Result(2, student1, quiz, 5, 0, DateTime.Now);

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
            Result highScore = new Result(3, student1, quiz, 9, 10, DateTime.Now);

            // Act
            string feedback = highScore.GetFeedbackMessage();

            // Assert
            Assert.AreEqual("Excellent performance!", feedback);
        }

        [TestMethod]
        public void GetFeedbackMessage_PassScore_ReturnsGoodEffort()
        {
            // Arrange
            Result passScore = new Result(4, student1, quiz, 6, 10, DateTime.Now);

            // Act
            string feedback = passScore.GetFeedbackMessage();

            // Assert
            Assert.AreEqual("Good effort, you passed.", feedback);
        }

        [TestMethod]
        public void GetFeedbackMessage_FailScore_ReturnsNeedsImprovement()
        {
            // Arrange
            Result failScore = new Result(5, student1, quiz, 3, 10, DateTime.Now);

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
            student2 = new Student(3, "student2", "stud234", "student2@test.com", "Inactive");

            Result r1 = new Result(6, student1, quiz, 7, 10, DateTime.Now);
            Result r2 = new Result(7, student2, quiz, 5, 10, DateTime.Now);

            r1.AddQuizResult();
            r2.AddQuizResult();

            // Act
            List<Result> studentResults = Result.FindResultsByStudent(student1);

            // Assert
            Assert.AreEqual(1, studentResults.Count);
            Assert.AreEqual(student1, studentResults[0].Student);
        }
    }
}