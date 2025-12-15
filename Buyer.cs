namespace Online_shop
{
    /// <summary>
    /// Class represents a buyer user; Can browse products, manage cart, and create orders
    /// </summary>
    internal class Buyer : User
    {
        /// <summary>
        /// Property stores buyer's address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Property represents buyer's shopping cart
        /// </summary>
        public Cart Cart { get; set; }

        /// <summary>
        /// Constructor for Buyer class
        /// </summary>
        /// <param name="login">Stores buyer's login</param>
        /// <param name="password">Stores buyer's password</param>
        /// <param name="email">Stores buyer's email address</param>
        /// <param name="address">Stores buyer's physical address</param>
        public Buyer(string login, string password, string email, string address)
            : base(login, password, email)
        {
            Address = address;
            Cart = new Cart();
        }

        /// <summary>
        /// Adds a product to the buyer's cart
        /// </summary>
        /// <param name="product">Product to be added to the cart</param>
        public void AddToCart(Product product)
        {
            Cart.AddProduct(product);
            Console.WriteLine($"Product {product.Name} added to cart!");
        }

        /// <summary>
        /// Shows content in buyer's cart
        /// </summary>
        public void ViewCart()
        {
            Cart.DisplayCart();
        }

        /// <summary>
        /// Creates an order from the items in cart
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Prevents order if cart is empty</exception>
        public Order CreateOrder()
        {
            if (Cart.Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty!");
            }

            var order = new Order(this, Cart.Items);
            Cart.Clear();
            return order;
        }
    }
}