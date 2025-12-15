using System.Runtime.InteropServices;

namespace Online_shop
{
    /// <summary>
    /// Main program class; entry point of the app
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Method to set console title
        /// </summary>
        /// <param name="title">Title to be set</param>
        /// <returns>Boolean result of the operation</returns>  
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleTitle(string title);

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetConsoleTitle("Online shop");

            try
            {
                var fileManager = new FileManager();

                var categoryService = new CategoryService();

                InitializeData(categoryService, fileManager);

                Order.OrderCreated += OnOrderCreated;

                var mainMenu = new Menu(categoryService, fileManager);
                mainMenu.Run();

                fileManager.SaveCategories(categoryService.GetAllCategories(), "C:\\Users\\user\\Desktop\\STEP\\.net\\Online-shop\\categories.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any button to exit...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Event handler for new order creation
        /// </summary>
        /// <param name="order">Order object</param>
        private static void OnOrderCreated(Order order)
        {
            Console.WriteLine($"\nNew order: {order.OrderId}");
            Console.WriteLine($"Buyer {order.BuyerLogin}");
            Console.WriteLine($"Sum: {order.TotalPrice} UAH");
        }

        /// <summary>
        /// Initialize basic data
        /// </summary>
        /// <param name="categoryService">Category service object</param>
        /// <param name="fileManager">File manager object</param>
        private static void InitializeData(CategoryService categoryService, FileManager fileManager)
        {
            var categories = fileManager.LoadCategories("C:\\Users\\user\\Desktop\\STEP\\.net\\Online-shop\\categories.xml");

            if (categories != null && categories.Count > 0)
            {
                categoryService.LoadCategories(categories);
                return;
            }
            var electronics = new Category("Electronics");
            electronics.AddProduct(new Product("iPhone 15 Pro", 45000, "Apple smartphone with 256GB memory"));
            electronics.AddProduct(new Product("Samsung Galaxy S24", 38000, "Androind smartphone with 256GB memory"));
            electronics.AddProduct(new Product("Lenovo ThinkPad E14 Gen 6", 52000, "Black laptop with Ryzen 5 7535HS CPU"));
            electronics.AddProduct(new Product("iPad Pro 12.9", 48000, "Apple tablet"));

            var clothing = new Category("Clothes");
            clothing.AddProduct(new Product("Jeans", 2500, "Very cool blue jeans"));
            clothing.AddProduct(new Product("Nike Tshirt", 800, "Sports t-short with Nike logo"));
            clothing.AddProduct(new Product("Puma shoes", 2100, "Runner shoes"));

            var books = new Category("Books");
            books.AddProduct(new Product("Kobzar", 250, "Poetry of a famous ukrainian writer Taras Shevchenko"));
            books.AddProduct(new Product("Harry Potrer", 450, "Full series of books by JK Rowling"));
            books.AddProduct(new Product("1984", 320, "Dystopian novel about totalitarism by George Orwell"));
            books.AddProduct(new Product("CLR via C#", 680, "Book for .NET by Richter"));

            categories.Add(electronics);
            categories.Add(clothing);
            categories.Add(books);

            categoryService.LoadCategories(categories);
            fileManager.SaveCategories(categoryService.GetAllCategories(), "C:\\Users\\user\\Desktop\\STEP\\.net\\Online-shop\\categories.xml");
        }
    }
}