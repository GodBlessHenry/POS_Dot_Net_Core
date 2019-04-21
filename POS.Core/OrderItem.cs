namespace POS.Core
{
    public class OrderItem 
    {
        public Product Product { get; set; }
        public ProductUnit Unit { get; set; }
        public int Quantity { get; set; }
    }
}
