using VendingMachineApplication.Interfaces;

namespace VendingMachineApplication.Services
{
    public class StockService
    {
        private IVendingMachine _vendingMachine;

        public StockService(IVendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }

        // To refill the stock of a specific product in the vending machine
        // It checks whether the product exists and whether the refill quantity is valid before proceeding
        public void RefillStock(int productId, int quantity)
        {
            if (!_vendingMachine.ProductExists(productId))
            {
                Console.WriteLine("Product not found.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Quantity must be positive.");
                return;
            }

            _vendingMachine.IncreaseStock(productId, quantity);
        }
    }
}
