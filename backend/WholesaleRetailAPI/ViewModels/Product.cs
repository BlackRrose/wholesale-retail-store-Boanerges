namespace WholesaleRetailAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        required
        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Category { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
