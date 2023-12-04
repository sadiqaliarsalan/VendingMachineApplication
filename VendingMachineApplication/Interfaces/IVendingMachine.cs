using VendingMachineApplication.Models;

namespace VendingMachineApplication.Interfaces
{
    public interface IVendingMachine
    {
        void AddProduct(Product product, int initialStock);
        string DisplayAllProducts();
        string InsertMoney(int amount);
        string RecallMoney();
        string OrderProduct(int productId);
        Product GetProductById(int productId);
        bool ProductExists(int productId);
        void IncreaseStock(int productId, int additionalQuantity);
    }
}
