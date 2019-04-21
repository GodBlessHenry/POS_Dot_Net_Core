namespace POS.Core
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool CanUseBulkDiscount { get; set; }
    }
}