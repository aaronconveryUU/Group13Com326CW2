using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quiz
{
    public class Question
    {
        // Private Fields
        protected int questionID;
        private string questionText;
        private List<string> questionOptions;
        private string questionCorrectAnswer;
        private string questionDifficultyLevel;

        // Private Properties
        public int QuestionID => questionID;
        public string QuestionText { get => questionText; set => questionText = value; }
        public List<string> QuestionOptions { get => questionOptions; set => questionOptions = value; }
        public string QuestionCorrectAnswer { get => questionCorrectAnswer; set => questionCorrectAnswer = value.ToLower(); }
        public string QuestionDifficultyLevel { get => questionDifficultyLevel; set => questionDifficultyLevel = value.ToUpper(); }

        // Constructor
        public Question(int id, string text, string correct, string difficulty, List<string> options)
        {
            questionID = id;
            questionText = text;
            QuestionCorrectAnswer = correct;
            QuestionDifficultyLevel = difficulty;
            questionOptions = options;
        }

        //Grab the file method
        private static string GetFilePath(int quizID)
        {
            return $"Qz{quizID}.txt";
        }

        //Public Methods for Admins
        public static void AddQuestion(List<Question> questionList)
        {
            Console.Clear();
            Console.WriteLine("ADD NEW QUESTION\n");

            Console.Write("Enter Quiz ID: ");
            int quizID = int.Parse(Console.ReadLine());
            
            Console.Write("Enter Question ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Question Text: (Remember to put it inside speech marks)");
            string text = Console.ReadLine();

            Console.Write("Enter Difficulty (E/M/H): ");
            string difficulty = Console.ReadLine();

            Console.Write("How many answer options? ");
            int optionCount = int.Parse(Console.ReadLine());

            List<string> options = new List<string>();
            for (int i = 0; i < optionCount; i++)
            {
                Console.Write($"Enter option {(char)('a' + i)}: (Remember to put it inside speech marks)");
                options.Add(Console.ReadLine());
            }

            Console.Write("Enter correct answer letter: ");
            string correct = Console.ReadLine();

            Question q = new Question(id, text, correct, difficulty, options);

            string path = GetFilePath(quizID);

            
            File.AppendAllText(path, ToCsvLine(q) + Environment.NewLine);

            Console.WriteLine("\nQuestion added successfully.");
            Console.ReadKey();
        }

        public static void UpdateQuestion(List<Question> questionList)
        {
            Console.Clear();
            Console.WriteLine("UPDATE QUESTION\n");

            Console.Write("Enter Quiz ID: ");
            int quizID = int.Parse(Console.ReadLine());

            Console.Write("Enter Question ID to update: ");
            int id = int.Parse(Console.ReadLine());

            var questions = LoadAll(quizID);
            Question q = questions.FirstOrDefault(x => x.QuestionID == id);

            if (q == null)
            {
                Console.WriteLine("Question not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCurrent Question:");
            Display(q);

            Console.Write("\nNew question text (Remember to put it inside speech marks)(leave blank to keep): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) q.QuestionText = input;

            Console.Write("New difficulty (E/M/H, leave blank to keep): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) q.QuestionDifficultyLevel = input;

            Console.Write("Change options? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.Write("How many options? ");
                int count = int.Parse(Console.ReadLine());

                List<string> options = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    Console.Write($"Enter option {(char)('a' + i)}: (Remember to put it inside speech marks)");
                    options.Add(Console.ReadLine());
                }

                q.QuestionOptions = options;

                Console.Write("Correct answer letter: ");
                q.QuestionCorrectAnswer = Console.ReadLine();
            }

            SaveAll(quizID, questions);

            Console.WriteLine("\nQuestion updated successfully.");
            Console.ReadKey();
        }

        public static void RemoveQuestion(List<Question> questionList)
        {
            Console.Clear();
            Console.WriteLine("REMOVE QUESTION\n");

            Console.Write("Enter Quiz ID: ");
            int quizID = int.Parse(Console.ReadLine());

            Console.Write("Enter Question ID to remove: ");
            int id = int.Parse(Console.ReadLine());

            var questions = LoadAll(quizID);
            int removed = questions.RemoveAll(q => q.QuestionID == id);

            SaveAll(quizID, questions);

            Console.WriteLine(removed > 0 ? "Question removed." : "Question not found.");
            Console.ReadKey();
        }

        public static void ViewAllQuestions(List<Question> questionList)
        {
            Console.Clear();
            Console.WriteLine("ALL QUESTIONS\n");

            Console.Write("Enter Quiz ID: ");
            int quizID = int.Parse(Console.ReadLine());

            var questions = LoadAll(quizID);

            if (questions.Count == 0)
            {
                Console.WriteLine("No questions found.");
            }

            foreach (var q in questions)
            {
                Display(q);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        // Public Method for Students
        public void AskQuestion(ref int score)
        {
            Console.WriteLine(QuestionText);

            char letter = 'a';
            foreach (string option in QuestionOptions)
            {
                Console.WriteLine($"{letter}) {option}");
                letter++;
            }

            Console.Write("\nYour answer: ");
            string answer = Console.ReadLine().Trim().ToLower();

            if (IsCorrect(answer))
            {
                Console.WriteLine("Correct!\n");
                score++;
            }
            else
            {
                Console.WriteLine($"Wrong. Correct answer was '{QuestionCorrectAnswer}'.\n");
            }
        }

        // Private methods for splitting, assembling and displaying
        private static List<Question> LoadAll(int quizID)
        {
            List<Question> questions = new List<Question>();
            string path = GetFilePath(quizID);

            if (!File.Exists(path)) return questions;

            var lines = File.ReadAllLines(path);

            // Start from index 1 to skip the quiz line
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                    questions.Add(ParseCsvLine(lines[i]));
            }

            return questions;
        }

        private static void SaveAll(int quizID, List<Question> questions)
        {
            string path = GetFilePath(quizID);
            List<string> lines = new List<string>();

            if (File.Exists(path))
            {
                // Preserve the quiz data (first line)
                lines.Add(File.ReadLines(path).First());
            }

            lines.AddRange(questions.Select(q => ToCsvLine(q)));

            File.WriteAllLines(path, lines);
        }

        private static Question ParseCsvLine(string line)
        {
            List<string> parts = SplitCsv(line);

            return new Question(
                int.Parse(parts[0]),
                parts[1],
                parts[2],
                parts[3],
                parts.Skip(4).ToList()
            );
        }

        private static string ToCsvLine(Question q)
        {
            List<string> parts = new List<string>
            {
                q.QuestionID.ToString(),
                q.QuestionText,
                q.QuestionCorrectAnswer,
                q.QuestionDifficultyLevel
            };

            foreach (string opt in q.QuestionOptions)
                parts.Add($"\"{opt}\"");

            return string.Join(",", parts);
        }

        private static List<string> SplitCsv(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            string current = "";

            foreach (char c in line)
            {
                if (c == '"') inQuotes = !inQuotes;
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current);
                    current = "";
                }
                else current += c;
            }

            result.Add(current);
            return result.Select(s => s.Trim('"')).ToList();
        }

        private static void Display(Question q)
        {
            Console.WriteLine($"ID: {q.QuestionID}");
            Console.WriteLine(q.QuestionText);
            Console.WriteLine($"Difficulty: {q.QuestionDifficultyLevel}");

            char letter = 'a';
            foreach (string opt in q.QuestionOptions)
            {
                Console.WriteLine($"{letter}) {opt}");
                letter++;
            }

            Console.WriteLine($"Correct Answer: {q.QuestionCorrectAnswer}");
        }

        //Private Method assisting AskQuestion
        private bool IsCorrect(string userAnswer)
        {
            if (string.IsNullOrWhiteSpace(userAnswer))
                return false;

            return userAnswer.Trim().ToLower() == questionCorrectAnswer.Trim().ToLower();
        }
    }
}