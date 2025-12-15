namespace Online_shop
{
    /// <summary>
    /// Class represents an admin user; Can manage products and categories
    /// </summary>
    internal class Admin : User
    {
        /// <summary>
        /// Counstructor for Admin class
        /// </summary>
        /// <param name="login">Stores admin's login</param>
        /// <param name="password">Stores admin's password</param>
        /// <param name="email">Stores admin's email address</param>
        public Admin(string login, string password, string email)
            : base(login, password, email) { }

        /// <summary>
        /// Adds a product to a category
        /// </summary>
        /// <param name="category">Category to which product is added</param>
        /// <param name="product">Product to be added</param>
        public void AddProduct(Category category, Product product)
        {
            if (product == category.Products.Find(p => p.Name == product.Name))
            {
                Console.WriteLine($"Product '{product.Name}' already exists in '{category.Name}'");
                return;
            }
            category.AddProduct(product);
            Console.WriteLine($"Product '{product.Name}' added to '{category.Name}'");
        }

        /// <summary>
        /// Removes a product from a category
        /// </summary>
        /// <param name="category">Category from which product is removed</param>
        /// <param name="product">Product to be removed</param>
        public void RemoveProduct(Category category, Product product)
        {
            category.RemoveProduct(product);
            Console.WriteLine($"Product '{product.Name}' deleted from '{category.Name}'");
        }

        /// <summary>
        /// Adds a category to the list of categories
        /// </summary>
        /// <param name="categories">List of categories</param>
        /// <param name="category">Category to be added</param>
        public void AddCategory(List<Category> categories, Category category)
        {
            categories.Add(category);
            Console.WriteLine($"Category '{category.Name}' added");
        }

        /// <summary>
        /// Removes a category from the list of categories
        /// </summary>
        /// <param name="categories">List of categories</param>
        /// <param name="category">Category to be removed</param>
        public void RemoveCategory(List<Category> categories, Category category)
        {
            categories.Remove(category);
            Console.WriteLine($"Category '{category.Name}' deleted");
        }

        /// <summary>
        /// Updates the price of a product after editing
        /// </summary>
        /// <param name="product">Product to be updated</param>
        /// <param name="newPrice">New price of the product</param>
        public void UpdateProductPrice(Product product, int newPrice)
        {
            int oldPrice = product.Price;
            product.Price = newPrice;
            Console.WriteLine($"Product price '{product.Name}' edited: {oldPrice} to {newPrice} UAH");
        }
    }
}