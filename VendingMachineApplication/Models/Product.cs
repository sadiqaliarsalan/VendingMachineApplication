namespace VendingMachineApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; }
        public int Price { get; }

        public Product(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}