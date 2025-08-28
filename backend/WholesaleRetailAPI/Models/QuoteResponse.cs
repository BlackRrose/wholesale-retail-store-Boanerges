namespace WholesaleRetailAPI.Models
{
    public class QuoteResponse
    {
        public int CustomerId { get; set; }
        public List<QuoteItem> Items { get; set; } = new();
        public decimal Subtotal { get; set; }
        public List<string> DiscountsApplied { get; set; } = new();
        public decimal FinalTotal { get; set; }
    }
}
