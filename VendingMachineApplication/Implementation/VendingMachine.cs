using System.Text;
using VendingMachineApplication.Interfaces;
using VendingMachineApplication.Models;

namespace VendingMachineApplication.Implementation
{
    public class VendingMachine : IVendingMachine
    {
        private Dictionary<int, Product> _products; // stores product details
        private Dictionary<int, int> _stock; // stores stock level for each product
        private int _credit; // stores the current credit inserted by the user

        public VendingMachine()
        {
            _products = new Dictionary<int, Product>();
            _stock = new Dictionary<int, int>();
            _credit = 0;
        }

        // Adds a product to the vending machine with an initial stock
        // Throws an exception if the initial stock is negative or if the product ID already exists
        public void AddProduct(Product product, int initialStock)
        {
            if (initialStock < 0)
            {
                throw new ArgumentException("Initial stock cannot be negative");
            }

            if (ProductExists(product.Id))
            {
                throw new ArgumentException("A product with the same ID already exists");
            }

            _products[product.Id] = product;
            _stock[product.Id] = initialStock;
        }

        // Displays all available products with their prices and current stock levels
        public string DisplayAllProducts()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var productId in _products.Keys)
            {
                var product = _products[productId];
                var availableStock = _stock[productId];
                sb.AppendLine($"{product.Id}. {product.Name} - {product.Price} (Available: {availableStock})");
            }

            return sb.ToString();
        }

        // Inserts a specified amount of money as credit
        // Returns a message indicating the current credit or an error if the amount is negative
        public string InsertMoney(int amount)
        {
            if (amount < 0)
            {
                return "Please enter valid credit";
            }

            _credit += amount;
            return $"Current credit is {_credit}";
        }

        // Returns all inserted money and resets the credit to zero
        public string RecallMoney()
        {
            int moneyToReturn = _credit;
            _credit = 0;
            return $"Giving out {moneyToReturn}";
        }

        // Processes the order for a product based on its id
        // Checks if the product exists, is in stock, and if sufficient credit is available
        // Deducts the product price from the credit and returns change if applicable
        public string OrderProduct(int productId)
        {
            if (!ProductExists(productId))
            {
                return "Product not found";
            }

            var product = _products[productId];

            if (_stock[productId] <= 0)
            {
                return $"{product.Name} is out of stock";
            }

            if (_credit < product.Price)
            {
                return $"Not enough credit, need {product.Price - _credit} more";
            }

            _credit -= product.Price;
            _stock[productId]--;
            string changeMessage = _credit > 0 ? $" Giving back change of {_credit}" : " No change to give back.";
            _credit = 0;
            return $"Giving out {product.Name}.{changeMessage}";
        }

        // Retrieves a product by its id
        // Returns the product if found, otherwise returns null
        public Product GetProductById(int productId)
        {
            if (_products.TryGetValue(productId, out Product product))
            {
                return product;
            }

            return null;
        }

        // Checks if a product exists in the vending machine based on its id
        public bool ProductExists(int productId)
        {
            return _products.ContainsKey(productId);
        }

        // Increases the stock of a specified product
        // Only increases stock if the product is already in the vending machine
        public void IncreaseStock(int productId, int additionalQuantity)
        {
            if (_stock.ContainsKey(productId))
            {
                _stock[productId] += additionalQuantity;
            }
        }
    }
}
