namespace WholesaleRetailAPI.Models
{
    public class PricingRule
    {
        public int PricingRuleId { get; set; }
        public string CustomerType { get; set; } // Retail or Wholesale
        public int MinQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool Active { get; set; }
    }
}
