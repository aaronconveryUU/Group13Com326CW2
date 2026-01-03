using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace Quiz
{


    public class Category
    {
        private static string filePath = "categories.csv";
        private static List<Category> categories = new List<Category>();

        public int CategoryID { get; private set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public Category(int id, string name, string description)
        {
            CategoryID = id;
            CategoryName = name;
            CategoryDescription = description;
        }


        // CSV Methods
        public static void LoadCategories()
        {
            categories.Clear();

            if (!File.Exists(filePath))
                return;

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(',');
                categories.Add(new Category(
                    int.Parse(parts[0]),
                    parts[1],
                    parts[2]
                ));
            }
        }

        private static void SaveCategories()
        {
            List<string> lines = categories
                .Select(c => $"{c.CategoryID},{c.CategoryName},{c.CategoryDescription}")
                .ToList();

            File.WriteAllLines(filePath, lines);
        }

        public void AddCategory()
        {
            categories.Add(this);
            SaveCategories();
        }

        public void UpdateCategory(string name, string description)
        {
            CategoryName = name;
            CategoryDescription = description;
            SaveCategories();
        }

        public void RemoveCategory()
        {
            categories.Remove(this);
            SaveCategories();
        }

        public static Category FindCategoryByID(int id)
        {
            return categories.FirstOrDefault(c => c.CategoryID == id);
        }

        public static List<Category> GetAllCategories()
        {
            return categories;
        }
    }
}

