using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiz;
using System;
using System.Collections.Generic;
using System.IO;

namespace CategoriesTest
{
    [TestClass]
    public class CategoryTests
    {
        private string csvPath;

        [TestInitialize]
        public void Setup()
        {
            csvPath = Path.Combine(AppContext.BaseDirectory, "categories.csv");


            if (File.Exists(csvPath))
                File.Delete(csvPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(csvPath))
                File.Delete(csvPath);
        }



        [TestMethod]
        public void Constructor_ShouldSetAllValuesCorrectly()
        {
            var category = new Category(1, "Math", "Numbers");

            Assert.AreEqual(1, category.CategoryID);
            Assert.AreEqual("Math", category.CategoryName);
            Assert.AreEqual("Numbers", category.CategoryDescription);
        }

        [TestMethod]
        public void CategoryName_ShouldBeSettable()
        {
            var category = new Category(1, "Old", "Desc");

            category.CategoryName = "New";

            Assert.AreEqual("New", category.CategoryName);
        }

        [TestMethod]
        public void CategoryDescription_ShouldBeSettable()
        {
            var category = new Category(1, "Name", "Old");

            category.CategoryDescription = "New";

            Assert.AreEqual("New", category.CategoryDescription);
        }


        [TestMethod]
        public void ToString_ShouldReturnExpectedFormat()
        {
            var category = new Category(2, "Science", "Physics");

            string result = category.ToString();

            Assert.AreEqual(
                "ID: 2, Name: Science, Description: Physics",
                result
            );
        }



        [TestMethod]
        public void GetAllCategories_ShouldLoadAllCategoriesFromCsv()
        {
            File.WriteAllLines(csvPath, new[]
            {
                "ID,Name,Description",
                "1,Math,Numbers and calculations",
                "2,Science,Physics Chemistry Biology",
                "3,History,World History and Events",
                "4,Literature,Books and Poetry",
                "5,Geography,Books and Poetry"
            });

            var categories = new List<Category>();

            var result = Category.GetAllCategories(categories);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("Math", result[0].CategoryName);
            Assert.AreEqual("Science", result[1].CategoryName);
            Assert.AreEqual("History", result[2].CategoryName);
            Assert.AreEqual("Literature", result[3].CategoryName);
            Assert.AreEqual("Geography", result[4].CategoryName);
        }

        [TestMethod]
        public void GetAllCategories_WhenFileDoesNotExist_ShouldReturnEmptyList()
        {
            var categories = new List<Category>();

            var result = Category.GetAllCategories(categories);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}