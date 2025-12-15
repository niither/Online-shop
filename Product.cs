namespace Online_shop
{
    /// <summary>
    /// Class represents a product in the online shop
    /// </summary>
    public class Product
    {
        private int _price;

        /// <summary>
        /// Property stores product name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Property stores product price; cannot be < 0
        /// </summary>
        public int Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be < 0");
                }
                _price = value;
            }
        }

        /// <summary>
        /// Property stores product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Parameterless constructor for Product class
        /// </summary>
        public Product() { }

        /// <summary>
        /// Constructor for Product class
        /// </summary>
        /// <param name="name">Stores product name</param>
        /// <param name="price">Stores product price</param>
        /// <param name="description">Stores product description</param>
        public Product(string name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        /// <summary>
        /// Shows product information
        /// </summary>
        /// <returns></returns>
        public string DisplayInfo()
        {
            return $"Product Name: {Name}\nPrice: {Price}\nDescription: {Description}";
        }

        /// <summary>
        /// Operator overload to compare two products
        /// </summary>
        /// <param name="p1">Object of Product class</param>
        /// <param name="p2">Object of Product class</param>
        /// <returns>Boolean; true if equal, false if not equal</returns>
        public static bool operator ==(Product? p1, Product? p2)
        {
            if (p1 is null || p2 is null) return false;
            return p1.Name == p2.Name;
        }

        /// <summary>
        /// Operator overload to compare two products
        /// </summary>
        /// <param name="p1">Object of Product class</param>
        /// <param name="p2">Object of Product class</param>
        /// <returns>Boolean; true if not equal, false if equal</returns>
        public static bool operator !=(Product? p1, Product? p2)
        {
            return !(p1 == p2);
        }
    }
}
