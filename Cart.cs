using System;
using System.Collections.Generic;
using System.Linq;

namespace Online_shop
{
    /// <summary>
    /// Class represents a shopping cart; manages products added by the buyer
    /// </summary>
    internal class Cart
    {
        /// <summary>
        /// List of products in the cart
        /// </summary>
        public List<Product> Items { get; set; }

        /// <summary>
        /// Indexator to acces products in the cart easily
        /// </summary>
        /// <param name="index">Index of the product</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Prevents access with wrong index</exception>
        public Product this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                if (index < 0 || index >= Items.Count)
                    throw new IndexOutOfRangeException("Wrong cart product index");
                Items[index] = value;
            }
        }

        /// <summary>
        /// Constructor for Cart class
        /// </summary>
        public Cart()
        {
            Items = new List<Product>();
        }

        /// <summary>
        /// Adds a product to the cart
        /// </summary>
        /// <param name="product">Product to be added</param>
        /// <exception cref="ArgumentNullException">Prevents adding null product</exception>
        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            Items.Add(product);
        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        /// <param name="product">Product to be removed</param>
        public void RemoveProduct(Product product)
        {
            if (product == null)
                return;
            Items.Remove(product);
        }

        /// <summary>
        /// Clears all products from the cart
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }

        /// <summary>
        /// Returns total price of all products in the cart
        /// </summary>
        /// <returns>Total price</returns>
        public int GetTotalPrice()
        {
            return Items.Sum(p => p.Price);
        }

        /// <summary>
        /// Shows content of the cart
        /// </summary>
        public void DisplayCart()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("\nCard it empty");
                return;
            }

            Console.WriteLine("Cart:");

            var grouped = Items
                .GroupBy(p => new { p.Name, p.Price })
                .Select(g => new
                {
                    ProductName = g.Key.Name,
                    Price = g.Key.Price,
                })
                .ToList();

            for (int i = 0; i < grouped.Count; i++)
            {
                var item = grouped[i];
                Console.WriteLine($"\n{i + 1}. {item.ProductName}");
                Console.WriteLine($"Price: {item.Price} UAH");
            }

            Console.WriteLine($"Total price: {GetTotalPrice()} UAH");
        }

        /// <summary>
        /// Sorts products in the cart by price
        /// </summary>
        public void SortByPrice()
        {
            Items = Items.OrderBy(p => p.Price).ToList();
            Console.WriteLine("Sorted by price");
        }

        /// <summary>
        /// Sorts products in the cart by name
        /// </summary>
        public void SortByName()
        {
            Items = Items.OrderBy(p => p.Name).ToList();
            Console.WriteLine("Sorted by name");
        }
    }
}