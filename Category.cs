namespace Online_shop
{
    /// <summary>
    /// Class represents a product category; manages products within the category
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Property stores category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property represents list of products in the category
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Constructor for Category class
        /// </summary>
        public Category() { }

        /// <summary>
        /// Constructor for Category class with name parameter
        /// </summary>
        /// <param name="name">Stores category name</param>
        public Category(string name)
        {
            Name = name;
            Products = new List<Product>();
        }

        /// <summary>
        /// Adds a product to the category
        /// </summary>
        /// <param name="product">Product to be added</param>
        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        /// <summary>
        /// Removes a product from the category
        /// </summary>
        /// <param name="product">Product to be removed</param>
        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

        /// <summary>
        /// Shows information about the category and its products
        /// </summary>
        /// <returns>String with category and product details</returns>
        public string DisplayInfo()
        {
            string info = "\nCategory: " + Name + "\nProducts:\n";

            foreach (var product in Products)
            {
                info += "\n" + product.DisplayInfo() + "\n";
            }
            info += "-";
            return info;
        }
    }
}
