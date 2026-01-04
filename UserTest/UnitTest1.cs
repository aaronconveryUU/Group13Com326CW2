using Quiz;

namespace UserTest
{
    [TestClass]
    public class UserTests
    {
        private UserManager userManager;
        private Admin admin1;
        private Student student1;
        private Student student2;
        private Quiz.Quiz quiz;

        [TestInitialize]
        public void Setup()
        {
            userManager = new UserManager(new List<User>());
            admin1 = new Admin(1, "admin1", "pass123", "admin1@test.com", DateTime.Now);
            student1 = new Student(2, "student1", "stud123", "student1@test.com", "Active");
            student2 = new Student(3, "student2", "stud234", "student2@test.com", "Inactive");

            userManager.AddUser(admin1);
            userManager.AddUser(student1);
            userManager.AddUser(student2);

            quiz = new Quiz.Quiz(1, "Test Quiz", "Dummy quiz for testing", 101, "10 questions", DateTime.Now);
        }

        [TestMethod]
        public void TestUsersAdded()
        {
            Assert.AreEqual(3, userManager.GetAllUsers().Count, "All 3 users should be added to UserManager.");
        }

        [TestMethod]
        public void TestLogin()
        {
            Assert.IsTrue(CheckLogin("admin1", "pass123"));
            Assert.IsFalse(CheckLogin("admin1", "wrongpass"));
            Assert.IsTrue(CheckLogin("student1", "stud123"));
            Assert.IsTrue(CheckLogin("student2", "stud234"));
            Assert.IsFalse(CheckLogin("student2", "wrong"));
            Assert.IsFalse(CheckLogin("nonexistent", "pass"));
        }

        [TestMethod]
        public void TestAdminLoginDateUpdate()
        {
            DateTime originalLogin = admin1.LoginDate;
            admin1.UpdateLoginDate();
            Assert.AreNotEqual(originalLogin, admin1.LoginDate, "Admin login date should update.");
        }

        [TestMethod]
        public void TestStudentStatusUpdate()
        {
            student2.SetStatus("Active");
            Assert.AreEqual("Active", student2.Status, "Student status should update to Active.");
        }

        [TestMethod]
        public void TestAddResults()
        {
            Result result1 = new Result(1, student1, quiz, 8, 10, DateTime.Now);
            Result result2 = new Result(2, student1, quiz, 5, 10, DateTime.Now);

            result1.AddQuizResult();
            result2.AddQuizResult();

            Assert.AreEqual(80, result1.CalculatePercentage(), "Result1 percentage should be 80%");
            Assert.AreEqual(50, result2.CalculatePercentage(), "Result2 percentage should be 50%");
        }

        private bool CheckLogin(string username, string password)
        {
            var user = userManager.GetAllUsers().FirstOrDefault(u => u.Username == username);
            if (user == null) return false;
            return user.CheckPassword(password);
        }
    }
}