namespace WholesaleRetailAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CustomerType { get; set; } // Retail or Wholesale

        public ICollection<Order> Orders { get; set; }
    }
}
