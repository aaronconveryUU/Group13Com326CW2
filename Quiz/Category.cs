using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quiz
{
    public class Category
    {
        // Private fields
        protected int categoryID;
        private string categoryName;
        private string categoryDescription;

        // Public properties
        public int CategoryID => categoryID;

        public string CategoryName
        {
            get => categoryName;
            set => categoryName = value;
        }

        public string CategoryDescription
        {
            get => categoryDescription;
            set => categoryDescription = value;
        }

        // Constructor
        public Category(int id, string name, string description)
        {
            categoryID = id;
            categoryName = name;
            categoryDescription = description;
        }

        // ===== File Handling =====
        private static readonly string filePath = Path.Combine(AppContext.BaseDirectory, "categories.csv");

        private static void LoadFromCsv(List<Category> categories)
        {
            categories.Clear();

            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);

            // Skip header if present
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                if (!int.TryParse(parts[0], out int id)) continue;

                string name = parts[1];
                string desc = parts[2];

                categories.Add(new Category(id, name, desc));
            }
        }

        private static void SaveToCsv(List<Category> categories)
        {
            var lines = new List<string> { "ID,Name,Description" };

            lines.AddRange(
                categories.Select(c => $"{c.CategoryID},{c.CategoryName},{c.CategoryDescription}")
            );

            File.WriteAllLines(filePath, lines);
        }

        // ===== Admin Methods =====

        public static void AddCategory(List<Category> categories)
        {
            Console.Clear();
            Console.WriteLine("ADD NEW CATEGORY\n");

            // ensure we have up-to-date list
            LoadFromCsv(categories);

            Console.Write("Enter Category ID: ");
            int id = int.Parse(Console.ReadLine());

            if (categories.Any(c => c.CategoryID == id))
            {
                Console.WriteLine("A category with this ID already exists.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Category Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Category Description: ");
            string description = Console.ReadLine();

            Category newCat = new Category(id, name, description);
            categories.Add(newCat);

            SaveToCsv(categories);

            Console.WriteLine("\nCategory created successfully.");
            Console.ReadKey();
        }

        public static void UpdateCategory(List<Category> categories)
        {
            Console.Clear();
            Console.WriteLine("UPDATE CATEGORY\n");

            LoadFromCsv(categories);

            Console.Write("Enter Category ID to update: ");
            int id = int.Parse(Console.ReadLine());

            Category cat = categories.FirstOrDefault(c => c.CategoryID == id);

            if (cat == null)
            {
                Console.WriteLine("Category not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCurrent Category Details:");
            Display(cat);

            Console.Write("\nNew name (leave blank to keep): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) cat.CategoryName = input;

            Console.Write("New description (leave blank to keep): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) cat.CategoryDescription = input;

            SaveToCsv(categories);

            Console.WriteLine("\nCategory updated successfully.");
            Console.ReadKey();
        }

        public static void RemoveCategory(List<Category> categories)
        {
            Console.Clear();
            Console.WriteLine("REMOVE CATEGORY\n");

            LoadFromCsv(categories);

            Console.Write("Enter Category ID to remove: ");
            int id = int.Parse(Console.ReadLine());

            Category cat = categories.FirstOrDefault(c => c.CategoryID == id);

            if (cat == null)
            {
                Console.WriteLine("Category not found.");
            }
            else
            {
                categories.Remove(cat);
                SaveToCsv(categories);
                Console.WriteLine("Category removed.");
            }

            Console.ReadKey();
        }

        public static Category FindCategoryByID(List<Category> categories)
        {
            Console.Clear();
            Console.WriteLine("FIND CATEGORY\n");

            LoadFromCsv(categories);

            Console.Write("Enter Category ID: ");
            int id = int.Parse(Console.ReadLine());

            Category cat = categories.FirstOrDefault(c => c.CategoryID == id);

            if (cat == null)
            {
                Console.WriteLine("Category not found.");
                Console.ReadKey();
                return null;
            }

            Display(cat);
            Console.ReadKey();
            return cat;
        }

        public static List<Category> GetAllCategories(List<Category> categories)
        {
            LoadFromCsv(categories);
            return categories;
        }


        // ===== Helper display =====
        private static void Display(Category c)
        {
            Console.WriteLine($"ID: {c.CategoryID}");
            Console.WriteLine($"Name: {c.CategoryName}");
            Console.WriteLine($"Description: {c.CategoryDescription}");
        }

        public override string ToString()
        {
            return $"ID: {CategoryID}, Name: {CategoryName}, Description: {CategoryDescription}";
        }

    }
}
