namespace Online_shop
{
    /// <summary>
    /// Delegate for order created event
    /// </summary>
    /// <param name="order">Created order</param>
    internal delegate void OrderCreated(Order order);

    /// <summary>
    /// Class represents an order; stores order details and triggers order created event
    /// </summary>
    internal class Order
    {
        /// <summary>
        /// Ivent when a new order is created
        /// </summary>
        public static event OrderCreated? OrderCreated;

        /// <summary>
        /// Property stores unique order id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Property stores buyer's login
        /// </summary>
        public string BuyerLogin { get; set; }

        /// <summary>
        /// Property stores buyer's address
        /// </summary>
        public string BuyerAddress { get; set; }

        /// <summary>
        /// Property stores order date
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Property represents list of ordered items
        /// </summary>
        public List<Product> OrderedItems { get; set; }

        /// <summary>
        /// Property stores total price of the order
        /// </summary>
        public int TotalPrice { get; set; }

        /// <summary>
        /// Constructor for Order class
        /// </summary>
        /// <param name="buyer">User who created the order</param>
        /// <param name="items">Items in order</param>
        public Order(Buyer buyer, List<Product> items)
        {
            Random random = new Random();

            OrderId = random.Next(10000, 99999).ToString();
            BuyerLogin = buyer.Login;
            BuyerAddress = buyer.Address;
            OrderDate = DateTime.Now;
            OrderedItems = new List<Product>(items);
            TotalPrice = items.Sum(item => item.Price);

            OrderCreated?.Invoke(this);
        }

        /// <summary>
        /// Show order details
        /// </summary>
        /// <returns>String with order information</returns>
        public string DisplayOrder()
        {
            string info = $"Order {OrderId}\n";
            info += $"Buyer: {BuyerLogin}\n";
            info += $"Address: {BuyerAddress}\n";
            info += $"Date: {OrderDate}\n";
            info += "Products:\n";

            foreach (var item in OrderedItems)
            {
                info += $"- {item.Name}\n";
            }
            info += $"Total price: {TotalPrice} UAH\n";

            return info;
        }
    }
}