namespace Online_shop
{
    /// <summary>
    /// Class represents the main menu of the shop; manages login and exit
    /// </summary>
    internal class Menu
    {
        private List<User> _users;
        private CategoryService _categoryService;
        private FileManager _fileManager;

        /// <summary>
        /// Constructor for Menu class
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="fileManager"></param>
        public Menu(CategoryService categoryService, FileManager fileManager)
        {
            _users = new List<User>();
            _categoryService = categoryService;
            _fileManager = fileManager;
            InitializeDefaultUsers();
        }

        /// <summary>
        /// Creates default users
        /// </summary>
        private void InitializeDefaultUsers()
        {
            _users.Add(new Admin("admin", "admin123", "admin@shop.com"));
            _users.Add(new Buyer("user", "user123", "user@example.com", "Some address"));
        }

        /// <summary>
        /// Starts main loop for the menu
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Online shop");
                Console.WriteLine("\n[1] Login");
                Console.WriteLine("[0] Quit");
                Console.Write("\nChoose: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "0":
                        Console.WriteLine("\nBye!");
                        return;
                    default:
                        Console.WriteLine("\nWrong choice!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Checks user input for login
        /// </summary>
        private void Login()
        {
            Console.Clear();

            Console.Write("Login: ");
            string? login = Console.ReadLine();

            Console.Write("Password: ");
            string? password = Console.ReadLine();

            var user = _users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("\nWrong login or password!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nLogged in as {user.Login}!");
            Console.ReadKey();

            if (user is Admin admin)
            {
                var adminMenu = new AdminMenu(admin, _categoryService, _fileManager);
                adminMenu.Run();
            }
            else if (user is Buyer buyer)
            {
                var buyerMenu = new BuyerMenu(buyer, _categoryService, _fileManager);
                buyerMenu.Run();
            }
        }
    }
}