using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class QuizSystem
    {

        private UserManager userManager;
        private List<Category> categories;
        private List<Quiz> quizzes;
        private List<Result> results;
        private List<Question> questions;

        private Student currentStudent;

        public static void Main(string[] args)
        {
            QuizSystem app = new QuizSystem();
            app.Run();
        }

        public QuizSystem()
        {
            userManager = new UserManager(new List<User>());
            categories = new List<Category>();
            quizzes = new List<Quiz>();
            questions = new List<Question>();
            results = new List<Result>();

            LoadSampleData();
        }

        public void Run()
        {
            ShowMainMenu();
        }

        public void LoadSampleData()
        {
            userManager.LoadUsersFromFile();
            
            Category.GetAllCategories(categories);
            Quiz.GetAllQuizzes(quizzes);


            if (userManager.GetAllUsers().Count == 0)
            {
                userManager.AddUser(new Admin(1, "admin1", "admin123", "admin@email.com", DateTime.Now));
                userManager.AddUser(new Student(2, "student1", "student123", "student@email.com", "Active"));
            }

            Result.LoadResultsFromFile(userManager, quizzes);
        }

        public void ShowMainMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("WELCOME TO THE ONLINE QUIZ SYSTEM");
                Console.WriteLine("Please select a login option or press 0 to exit.");
                Console.WriteLine("1. Student");
                Console.WriteLine("2. Admin");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AuthenticateStudent();
                        break;
                    case "2":
                        AuthenticateAdmin();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Thank you for using the Online Quiz System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void AuthenticateAdmin()
        {
            Console.Clear();
            Console.WriteLine("Admin Login");

            Console.WriteLine("Enter username:");
            string uNameInput = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string pwdInput = Console.ReadLine();

            List<User> allUsers = userManager.GetAllUsers();

            User foundUser = allUsers.FirstOrDefault(u => u.Username == uNameInput);

            if (foundUser == null)
            {
                Console.WriteLine("Username not found. Press any key to return to main menu.");
                Console.ReadKey();
                return;
            }

            bool isSuccess = foundUser.CheckPassword(pwdInput);

            if (!isSuccess)
            {
                Console.WriteLine("Incorrect password. Press any key to return to main menu.");
                Console.ReadKey();
                return;
            }

            if (foundUser is Admin)
            {
                ShowAdminMenu();
            }
            else
            {
                Console.WriteLine("Access denied. You are not an admin. Press any key to return to main menu.");
                Console.ReadKey();
            }
        }

        public void AuthenticateStudent()
        {
            Console.Clear();
            Console.WriteLine("Student Login");

            Console.WriteLine("Enter username:");
            string uNameInput = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string pwdInput = Console.ReadLine();

            List<User> allUsers = userManager.GetAllUsers();

            User foundUser = allUsers.FirstOrDefault(u => u.Username == uNameInput);

            if (foundUser == null)
            {
                Console.WriteLine("Username not found. Press any key to return to main menu.");
                Console.ReadKey();
                return;
            }

            bool isSuccess = foundUser.CheckPassword(pwdInput);

            if (!isSuccess)
            {
                Console.WriteLine("Incorrect password. Press any key to return to main menu.");
                Console.ReadKey();
                return;
            }

            if (foundUser is Student student)
            {
                currentStudent = student;
                ShowStudentMenu();
            }
            else
            {
                Console.WriteLine("Access denied. You are not a student. Press any key to return to main menu.");
                Console.ReadKey();
            }
        }

        public void ShowAdminMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("ADMIN MENU");
                Console.WriteLine("1. Manage Users");
                Console.WriteLine("2. Manage Categories");
                Console.WriteLine("3. Manage Quizzes");
                Console.WriteLine("4. Manage Questions");
                Console.WriteLine("0. Logout");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("USER MANAGER");
                        Console.WriteLine("1. Add User");
                        Console.WriteLine("2. Update User");
                        Console.WriteLine("3. Remove User");
                        Console.WriteLine("4. Show All Users");
                        Console.WriteLine("0. Back");

                        string choiceUM = Console.ReadLine();

                        switch (choiceUM)
                        {
                            case "1":
                                userManager.AddUser(userManager.CreateUser());
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "2":
                                userManager.UpdateUserDetails();
                                break;
                            case "3":
                                userManager.RemoveUser();
                                break;
                            case "4":
                                Console.Clear();
                                Console.WriteLine("All Users:");
                                userManager.GetAllUsers().ForEach(u => Console.WriteLine(u));
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "0":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("CATEGORY MANAGER");
                        Console.WriteLine("1. Add Category");
                        Console.WriteLine("2. Update Category");
                        Console.WriteLine("3. Remove Category");
                        Console.WriteLine("4. Show All Categories");
                        Console.WriteLine("0. Back");

                        string choiceCM = Console.ReadLine();

                        switch (choiceCM)
                        {
                            case "1":
                                Category.AddCategory(categories);
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "2":
                                Category.UpdateCategory(categories);
                                break;
                            case "3":
                                Category.RemoveCategory(categories);
                                break;
                            case "4":
                                Console.Clear();
                                Console.WriteLine("All Categories:");
                                Category.GetAllCategories(categories).ForEach(c => Console.WriteLine(c));
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "0":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("QUIZ MANAGER");
                        Console.WriteLine("1. Add Quiz");
                        Console.WriteLine("2. Update Quiz");
                        Console.WriteLine("3. Remove Quiz");
                        Console.WriteLine("4. Show All Quizzes");
                        Console.WriteLine("0. Back");

                        string choiceQuizM = Console.ReadLine();

                        switch (choiceQuizM)
                        {
                            case "1":
                                Quiz.AddQuiz(quizzes);
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "2":
                                Quiz.UpdateQuiz(quizzes);
                                break;
                            case "3":
                                Quiz.RemoveQuiz(quizzes);
                                break;
                            case "4":
                                Console.Clear();
                                Console.WriteLine("All Quizzes:");
                                Quiz.GetAllQuizzes(quizzes).ForEach(q => Console.WriteLine(q));
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "0":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("QUESTION MANAGER");
                        Console.WriteLine("1. Add Question");
                        Console.WriteLine("2. Update Question");
                        Console.WriteLine("3. Remove Question");
                        Console.WriteLine("4. Show All Questions");
                        Console.WriteLine("0. Back");

                        string choiceQuestionM = Console.ReadLine();

                        switch (choiceQuestionM)
                        {
                            case "1":
                                Question.AddQuestion(questions);
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "2":
                                Question.UpdateQuestion(questions);
                                break;
                            case "3":
                                Question.RemoveQuestion(questions);
                                break;
                            case "4":
                                Console.Clear();
                                Console.WriteLine("All Questions:");
                                Question.ViewAllQuestions(questions);
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "0":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void ShowStudentMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("STUDENT MENU");
                Console.WriteLine("1. Take Quiz");
                Console.WriteLine("2. View Results");
                Console.WriteLine("0. Logout");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayQuiz();
                        break;
                    case "2":
                        ShowStudentResults();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void PlayQuiz()
        {
            Console.Clear();
            Console.WriteLine("Available Quizzes:");

            if (quizzes.Count == 0)
            {
                Console.WriteLine("No quizzes available. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < quizzes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quizzes[i].QuizTitle}");
            }

            Console.Write("\nEnter quiz number to take the quiz: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            int index = choice - 1;

            if (index < 0 || index >= quizzes.Count)
            {
                Console.WriteLine("Invalid choice. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            Quiz selectedQuiz = quizzes[index];

            int score = Quiz.AskQuiz(selectedQuiz.QuizID);

            int totalQuestions = File
                .ReadAllLines($"Qz{selectedQuiz.QuizID}.csv")
                .Skip(1)
                .Count(line => !string.IsNullOrWhiteSpace(line));

            Result result = new Result(
                results.Count + 1,
                currentStudent,
                selectedQuiz,
                score,
                totalQuestions,
                DateTime.Now
            );
            result.AddQuizResult();

            Console.WriteLine($"You scored {score} out of {totalQuestions}.");
            Console.WriteLine("press any key to return to the menu.");
            Console.ReadKey();
        }


        public void ShowStudentResults()
        {
            Console.Clear();
            Console.WriteLine("Your Quiz Results:");

            if (currentStudent == null)
            {
                Console.WriteLine("No student is currently logged in. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            List<Result> studentResults = Result.FindResultsByStudent(currentStudent);

            if (studentResults.Count == 0)
            {
                Console.WriteLine("No results found. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            foreach (Result r in studentResults)
            {
                double percentage = r.CalculatePercentage();
                string feedback = r.GetFeedbackMessage();
                Console.WriteLine($"Quiz: {r.Quiz.QuizTitle}");
                Console.WriteLine($"Score: {r.Score}/{r.TotalQuestions} ({percentage:0}%)");
                Console.WriteLine($"Date: {r.AttemptDate}");
                Console.WriteLine($"Feedback: {feedback}");
                Console.WriteLine();

                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
            }
        }
    }
}
