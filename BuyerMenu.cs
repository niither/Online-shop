using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_shop
{
    /// <summary>
    /// Class represents buyer menu; manages buyer operations like browsing categories and managing cart
    /// </summary>
    internal class BuyerMenu
    {
        private Buyer _buyer;
        private CategoryService _categoryService;
        private FileManager _fileManager;

        /// <summary>
        /// Constructor for BuyerMenu class
        /// </summary>
        /// <param name="buyer">User with buyer privileges</param>
        /// <param name="categoryService">Category service for managing categories</param>
        /// <param name="fileManager">File manager for managing xml serialization</param>
        public BuyerMenu(Buyer buyer, CategoryService categoryService, FileManager fileManager)
        {
            _buyer = buyer;
            _categoryService = categoryService;
            _fileManager = fileManager;
        }

        /// <summary>
        /// Starts main loop for buyer menu
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n[1] Show catalog");
                Console.WriteLine("[2] Cart");
                Console.WriteLine("[0] Exit");
                Console.Write("\nChoose: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewCatalog();
                        break;
                    case "2":
                        ManageCart();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("\nWrong choice!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Shows product catalog
        /// </summary>
        private void ViewCatalog()
        {
            Console.Clear();
            _categoryService.DisplayAllCategories();

            Console.Write("\nChoose category (0 - exit): ");
            if (int.TryParse(Console.ReadLine(), out int catIndex) && catIndex > 0)
            {
                var categories = _categoryService.GetAllCategories();
                if (catIndex <= categories.Count)
                {
                    ShowCategoryProducts(categories[catIndex - 1]);
                }
            }
        }

        /// <summary>
        /// Shows products in a category and allows adding them to cart
        /// </summary>
        /// <param name="category">Category to show products from</param>
        private void ShowCategoryProducts(Category category)
        {
            Console.Clear();
            Console.WriteLine(category.DisplayInfo());

            if (category.Products.Count == 0)
            {
                Console.ReadKey();
                return;
            }

            Console.Write("\nAdd to cart? (exit - 0): ");
            if (int.TryParse(Console.ReadLine(), out int prodIndex) && prodIndex > 0
                && prodIndex <= category.Products.Count)
            {
                _buyer.AddToCart(category.Products[prodIndex - 1]);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Manager for cart operations; sorting, deleting, ordering, and clearing cart
        /// </summary>
        private void ManageCart()
        {
            while (true)
            {
                Console.Clear();
                _buyer.ViewCart();

                Console.WriteLine("\n[1] Sort by price");
                Console.WriteLine("[2] Sort by name");
                Console.WriteLine("[3] Delete");
                Console.WriteLine("[4] Order");
                Console.WriteLine("[5] Clear cart");
                Console.WriteLine("[0] Exit");
                Console.Write("\nChoose: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _buyer.Cart.SortByPrice();
                        Console.ReadKey();
                        break;
                    case "2":
                        _buyer.Cart.SortByName();
                        Console.ReadKey();
                        break;
                    case "3":
                        RemoveFromCart();
                        break;
                    case "4":
                        CartService.ProcessCheckout(_buyer, _fileManager);
                        Console.ReadKey();
                        break;
                    case "5":
                        _buyer.Cart.Clear();
                        Console.WriteLine("Cart cleared");
                        Console.ReadKey();
                        break;
                    case "0":
                        return;
                }
            }
        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        private void RemoveFromCart()
        {
            if (_buyer.Cart.Items.Count == 0) return;

            Console.Write("\nEnter product index to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0
                && index <= _buyer.Cart.Items.Count)
            {
                var product = _buyer.Cart[index - 1];
                _buyer.Cart.RemoveProduct(product);
                Console.WriteLine("Product deleted");
            }
            Console.ReadKey();
        }
    }
}
