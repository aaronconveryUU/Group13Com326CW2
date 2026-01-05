using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiz;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuizQuestionTest
{
    [TestClass]
    public class QuizQuestionTest
    {

        [TestMethod]
        public void IsCorrect_ReturnsTrue_ForCorrectAnswer()
        {
            Question q = new Question(
                1,
                "Test?",
                "a",
                "E",
                new List<string> { "Answer A", "Answer B" }
            );

            Assert.IsTrue(q.IsCorrect("A"));
        }


        [TestMethod]
        public void IsCorrect_ReturnsFalse_ForWrongAnswer()
        {
            Question q = new Question(
                1,
                "Test?",
                "a",
                "E",
                new List<string> { "Answer A", "Answer B" }
            );

            Assert.IsFalse(q.IsCorrect("b"));
        }



        [TestMethod]
        public void LoadQuiz_LoadsMetadataCorrectly()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Qz98.csv");

            File.WriteAllLines(path, new[]
            {
        "98,Test Quiz,Test description,1,Summary,01/01/2025",
        "1,Test Question?,a,E,\"Answer A\",\"Answer B\""
    });

            Quiz.Quiz quiz = Quiz.Quiz.LoadQuiz(98);

            Assert.AreEqual("Test Quiz", quiz.QuizTitle);
            Assert.AreEqual(1, quiz.QuizCategory);
            Assert.AreEqual(new DateTime(2025, 1, 1), quiz.QuizDate);
        }

        [TestMethod]
        public void LoadQuestions_SkipsMetadataLine()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Qz99.csv");

            File.WriteAllLines(path, new[]
            {
        "99,Another Quiz,Desc,1,Summary,01/01/2025",
        "1,Test Question?,a,E,\"Answer A\",\"Answer B\""
    });

            var questions = Quiz.Quiz.LoadQuestionsForQuiz(99);

            Assert.AreEqual(1, questions.Count);
            Assert.AreEqual("Test Question?", questions[0].QuestionText);
        }
    }
}