using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_shop
{
    /// <summary>
    /// Class represents admin menu; Manages admin operations like editing products and categories
    /// </summary>
    internal class AdminMenu
    {
        /// <summary>
        /// Private fields for AdminMenu class
        /// </summary>
        private Admin _admin;
        private CategoryService _categoryService;
        private FileManager _fileManager;

        /// <summary>
        /// Constructor for AdminMenu class
        /// </summary>
        /// <param name="admin">User with admin privileges</param>
        /// <param name="categoryService">Service for managing categories</param>
        /// <param name="fileManager">File manager for managing xml data</param>
        public AdminMenu(Admin admin, CategoryService categoryService, FileManager fileManager)
        {
            _admin = admin;
            _categoryService = categoryService;
            _fileManager = fileManager;
        }

        /// <summary>
        /// Runs the admin menu loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n[1] Add category");
                Console.WriteLine("[2] Add product");
                Console.WriteLine("[3] Edit product");
                Console.WriteLine("[4] Delete product");
                Console.WriteLine("[5] Show category");
                Console.WriteLine("[6] Save data");
                Console.WriteLine("[0] Exit");
                Console.Write("\nChoose: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCategory();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        EditProduct();
                        break;
                    case "4":
                        RemoveProduct();
                        break;
                    case "5":
                        ViewCatalog();
                        break;
                    case "6":
                        SaveData();
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
        /// Adds a new category to the catalog
        /// </summary>
        private void AddCategory()
        {
            Console.Clear();
            Console.Write("New category name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name can not be empty!");
                Console.ReadKey();
                return;
            }

            var category = new Category(name);
            _admin.AddCategory(_categoryService.GetAllCategories(), category);
            Console.ReadKey();
        }

        /// <summary>
        /// Adds a new product to a selected category
        /// </summary>
        private void AddProduct()
        {
            Console.Clear();
            _categoryService.DisplayAllCategories();

            Console.Write("\nChoose category number: ");
            if (!int.TryParse(Console.ReadLine(), out int catIndex) || catIndex < 1)
            {
                Console.WriteLine("Wrong number!");
                Console.ReadKey();
                return;
            }

            var categories = _categoryService.GetAllCategories();
            if (catIndex > categories.Count)
            {
                Console.WriteLine("Category not found!");
                Console.ReadKey();
                return;
            }

            Console.Write("\nProduct name: ");
            string? name = Console.ReadLine();

            Console.Write("Price: ");
            if (!int.TryParse(Console.ReadLine(), out int price) || price < 0)
            {
                Console.WriteLine("Wrong price!");
                Console.ReadKey();
                return;
            }

            Console.Write("Description: ");
            string? description = Console.ReadLine();

            try
            {
                var product = new Product(name, price, description);
                _admin.AddProduct(categories[catIndex - 1], product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Edits an existing product's price in a selected category
        /// </summary>
        private void EditProduct()
        {
            Console.Clear();
            _categoryService.DisplayAllCategories();

            Console.Write("\nChoose category: ");
            if (!int.TryParse(Console.ReadLine(), out int catIndex) || catIndex < 1)
                return;

            var categories = _categoryService.GetAllCategories();
            if (catIndex > categories.Count) return;

            var category = categories[catIndex - 1];
            Console.WriteLine(category.DisplayInfo());

            Console.Write("\nChoose product to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int prodIndex) || prodIndex < 1)
                return;

            if (prodIndex > category.Products.Count) return;

            var product = category.Products[prodIndex - 1];

            Console.Write($"\nNew price (current: {product.Price}): ");
            if (int.TryParse(Console.ReadLine(), out int newPrice))
            {
                _admin.UpdateProductPrice(product, newPrice);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Removes a product from a selected category
        /// </summary>
        private void RemoveProduct()
        {
            Console.Clear();
            _categoryService.DisplayAllCategories();

            Console.Write("\nChoose category: ");
            if (!int.TryParse(Console.ReadLine(), out int catIndex) || catIndex < 1)
                return;

            var categories = _categoryService.GetAllCategories();
            if (catIndex > categories.Count) return;

            var category = categories[catIndex - 1];
            Console.WriteLine(category.DisplayInfo());

            Console.Write("\nChoose product to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int prodIndex) || prodIndex < 1)
                return;

            if (prodIndex > category.Products.Count) return;

            var product = category.Products[prodIndex - 1];
            _admin.RemoveProduct(category, product);
            Console.ReadKey();
        }

        /// <summary>
        /// Shows the entire catalog of categories and products
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
                    Console.WriteLine(categories[catIndex-1].DisplayInfo());
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Saves all categories and products to an XML file
        /// </summary>
        private void SaveData()
        {
            _fileManager.SaveCategories(_categoryService.GetAllCategories(), "data\\categories.xml");

            Console.WriteLine("\nAll data saved!");
            Console.ReadKey();
        }
    }
}
