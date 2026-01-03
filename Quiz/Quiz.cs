using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quiz
{
    public class Quiz
    {
        // Private Fields
        protected int quizID;
        private string quizTitle;
        private string quizDescription;
        private int quizCategory;
        private string quizQuestions;
        private DateTime quizDate;

        // Public Properties
        public int QuizID => quizID;

        public string QuizTitle
        {
            get => quizTitle;
            set => quizTitle = value;
        }

        public string QuizDescription
        {
            get => quizDescription;
            set => quizDescription = value;
        }

        public int QuizCategory
        {
            get => quizCategory;
            set => quizCategory = value;
        }

        public string QuizQuestions
        {
            get => quizQuestions;
            set => quizQuestions = value;
        }

        public DateTime QuizDate
        {
            get => quizDate;
            set => quizDate = value;
        }

        // Constructor
        public Quiz(int quizID, string quizTitle, string quizDescription, int quizCategory, string quizQuestions, DateTime quizDate)
        {
            this.quizID = quizID;
            this.quizTitle = quizTitle;
            this.quizDescription = quizDescription;
            this.quizCategory = quizCategory;
            this.quizQuestions = quizQuestions;
            this.quizDate = quizDate;
        }

        //Grab the file method
        private static string GetFilePath(int quizID)
        {
            return $"Qz{quizID}.txt";
        }

        //Public Methods for Admins
        public static void AddQuiz(List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("ADD NEW QUIZ\n");

            Console.Write("Enter Quiz ID: ");
            int id = int.Parse(Console.ReadLine());

            string path = GetFilePath(id);
            if (File.Exists(path))
            {
                Console.WriteLine("Quiz already exists.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Quiz Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Quiz Description: (Remember to put it inside speech marks)");
            string description = Console.ReadLine();

            Console.Write("Enter Quiz Category ID: ");
            int category = int.Parse(Console.ReadLine());

            Console.Write("Enter Quiz Question Summary (e.g. '10 OOP questions'): ");
            string questions = Console.ReadLine();

            Console.Write("Enter Quiz Date (dd/MM/yyyy): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Quiz quiz = new Quiz(id, title, description, category, questions, date);

            // Write ONLY the metadata line
            File.WriteAllText(path, quiz.ToCsvLine() + Environment.NewLine);

            Console.WriteLine("\nQuiz created successfully.");
            Console.ReadKey();
        }

        public static void UpdateQuiz(List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("UPDATE QUIZ\n");

            Console.Write("Enter Quiz ID to update: ");
            int id = int.Parse(Console.ReadLine());

            string path = GetFilePath(id);
            if (!File.Exists(path))
            {
                Console.WriteLine("Quiz not found.");
                Console.ReadKey();
                return;
            }

            Quiz quiz = LoadQuiz(id);

            Console.WriteLine("\nCurrent Quiz Details:");
            Display(quiz);

            Console.Write("\nNew title (leave blank to keep): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) quiz.QuizTitle = input;

            Console.Write("New description (Remember to put it inside speech marks)(leave blank to keep): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) quiz.QuizDescription = input;

            Console.Write("New category ID (leave blank to keep): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) quiz.QuizCategory = int.Parse(input);

            Console.Write("New question summary (leave blank to keep): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) quiz.QuizQuestions = input;

            Console.Write("New date (dd/MM/yyyy, leave blank to keep): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) quiz.QuizDate = DateTime.Parse(input);

            SaveQuiz(id, quiz);

            Console.WriteLine("\nQuiz updated successfully.");
            Console.ReadKey();
        }

        public static void RemoveQuiz(List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("REMOVE QUIZ\n");

            Console.Write("Enter Quiz ID to remove: ");
            int id = int.Parse(Console.ReadLine());

            string path = GetFilePath(id);

            if (!File.Exists(path))
            {
                Console.WriteLine("Quiz not found.");
            }
            else
            {
                File.Delete(path);
                Console.WriteLine("Quiz removed.");
            }

            Console.ReadKey();
        }

        public static Quiz FindQuizByID(List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("FIND QUIZ\n");

            Console.Write("Enter Quiz ID: ");
            int id = int.Parse(Console.ReadLine());

            if (!File.Exists(GetFilePath(id)))
            {
                Console.WriteLine("Quiz not found.");
                Console.ReadKey();
                return null;
            }

            Quiz quiz = LoadQuiz(id);
            Display(quiz);

            Console.ReadKey();
            return quiz;
        }

        public static List<Quiz> GetAllQuizzes(List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("ALL QUIZZES\n");

            List<Quiz> quizzesList = new List<Quiz>();

            foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory(), "Qz*.txt"))
            {
                string line = File.ReadLines(file).First();
                quizzes.Add(ParseCsvLine(line));
            }

            foreach (Quiz quiz in quizzes)
            {
                Display(quiz);
                Console.WriteLine();
            }

            Console.ReadKey();
            return quizzes;
        }

        public static int AskQuiz(int quizID)
        {
            Console.Clear();

            string path = $"Qz{quizID}.txt";

            if (!File.Exists(path))
            {
                Console.WriteLine("Quiz not found.");
                Console.ReadKey();
                return 0;
            }

            // Load Quiz details
            Quiz quiz = LoadQuiz(quizID);

            Console.WriteLine("QUIZ START\n");
            Console.WriteLine($"Title: {quiz.QuizTitle}");
            Console.WriteLine($"Description: {quiz.QuizDescription}");
            Console.WriteLine($"Category ID: {quiz.QuizCategory}");
            Console.WriteLine($"Questions: {quiz.QuizQuestions}");
            Console.WriteLine($"Date: {quiz.QuizDate:dd/MM/yyyy}");
            Console.WriteLine("\nPress any key to begin...");
            Console.ReadKey();
            Console.Clear();

            // Load Questions
            List<Question> questions = LoadQuestionsForQuiz(quizID);

            int score = 0;
            int questionNumber = 1;

            foreach (Question q in questions)
            {
                Console.WriteLine($"Question {questionNumber}/{questions.Count}\n");
                q.AskQuestion(ref score);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();

                questionNumber++;
            }

            Console.ReadKey();
            return score;
        }

        // Private methods for splitting, assembling and displaying
        private static Quiz LoadQuiz(int quizID)
        {
            string firstLine = File.ReadLines(GetFilePath(quizID)).First();
            return ParseCsvLine(firstLine);
        }

        private static void SaveQuiz(int quizID, Quiz quiz)
        {
            string path = GetFilePath(quizID);
            var lines = File.ReadAllLines(path).ToList();

            lines[0] = quiz.ToCsvLine(); // overwrite metadata only
            File.WriteAllLines(path, lines);
        }

        private static Quiz ParseCsvLine(string line)
        {
            var parts = line.Split(',');

            return new Quiz(
                int.Parse(parts[0]),
                parts[1],
                parts[2],
                int.Parse(parts[3]),
                parts[4],
                DateTime.Parse(parts[5])
            );
        }

        private string ToCsvLine()
        {
            return string.Join(",",
                QuizID,
                QuizTitle,
                QuizDescription,
                QuizCategory,
                QuizQuestions,
                QuizDate.ToString("dd/MM/yyyy"));
        }

        private static void Display(Quiz q)
        {
            Console.WriteLine($"Quiz ID: {q.QuizID}");
            Console.WriteLine($"Title: {q.QuizTitle}");
            Console.WriteLine($"Description: {q.QuizDescription}");
            Console.WriteLine($"Category ID: {q.QuizCategory}");
            Console.WriteLine($"Questions: {q.QuizQuestions}");
            Console.WriteLine($"Date: {q.QuizDate:dd/MM/yyyy}");
        }

        // Private Methods assisting AskQuiz
        private static List<Question> LoadQuestionsForQuiz(int quizID)
        {
            List<Question> questions = new List<Question>();
            string path = $"Qz{quizID}.txt";

            if (!File.Exists(path))
            {
                return questions; // return empty list
            }

            var lines = File.ReadAllLines(path);

            // Skip first line (quiz metadata)
            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    questions.Add(ParseQuestionLine(lines[i]));
                }
            }

            return questions;
        }

        private static Question ParseQuestionLine(string line)
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
    }
}