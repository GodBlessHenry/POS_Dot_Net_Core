namespace POS.Core
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool CanUseBulkDiscount { get; set; }
    }
}