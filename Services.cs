namespace Online_shop
{
    /// <summary>
    /// Class manages categories in the online shop
    /// </summary>
    internal class CategoryService
    {
        private List<Category> _categories;

        /// <summary>
        /// Constructor for CategoryService class
        /// </summary>
        public CategoryService()
        {
            _categories = new List<Category>();
        }

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>List of all categories</returns>
        public List<Category> GetAllCategories()
        {
            return _categories;
        }

        /// <summary>
        /// Shows all categories with their product count
        /// </summary>
        public void DisplayAllCategories()
        {
            if (_categories.Count == 0)
            {
                Console.WriteLine("\nNo categories");
                return;
            }

            Console.WriteLine("Categories:");

            for (int i = 0; i < _categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_categories[i].Name} with {_categories[i].Products.Count} products");
            }
        }

        /// <summary>
        /// Loads categories from a list
        /// </summary>
        /// <param name="categories">List of categories from XML file</param>
        public void LoadCategories(List<Category> categories)
        {
            _categories = categories;
        }
    }

    /// <summary>
    /// Class manages cart operations like checkout
    /// </summary>
    internal class CartService
    {
        /// <summary>
        /// Creates an orderd from the buyer's cart
        /// </summary>
        /// <param name="buyer">Buyer who is checking out</param>
        /// <param name="fileManager">File manager for saving/loading data</param>
        public static void ProcessCheckout(Buyer buyer, FileManager fileManager)
        {
            try
            {
                if (buyer.Cart.Items.Count == 0)
                {
                    Console.WriteLine("\nCart is empty!");
                    return;
                }

                Console.WriteLine("Order:");

                buyer.ViewCart();

                Console.Write("\nAccept order? yes/no: ");
                string? confirmation = Console.ReadLine();

                if (confirmation?.ToLower() == "yes")
                {
                    var order = buyer.CreateOrder();

                    Console.WriteLine("\nOrder succesfully completed!");
                    Console.WriteLine(order.DisplayOrder());
                }
                else
                {
                    Console.WriteLine("\nOrder canceled");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError of order: {ex.Message}");
            }
        }
    }
}