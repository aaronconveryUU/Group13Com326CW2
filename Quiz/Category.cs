using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    public class Category
    {
        // Private fields
        protected int categoryID;
        private string categoryName;
        private string categoryDescription;

        // Public properties
        public int CategoryID
        {
            get {return categoryID;}
        }

        public string CategoryName
        {
            get {return categoryName;}
            set {categoryName = value;}
        }

        public string CategoryDescription
        {
            get {return categoryDescription;}
            set {categoryDescription = value;}
        }

        // Constructor
        public Category(int categoryID, string categoryName, string categoryDescription)
        {
            this.CategoryID = categoryID;
            this.CategoryName = categoryName;
            this.CategoryDescription = categoryDescription;
        }

        // Methods
        public AddCategory()
        {

        }

        public UpdateCategory()
        {

        }

        public RemoveCategory()
        {

        }

        public FindCategoryByID()
        {

        }

        public GetAllCategories()
        {

        }

    }
}
