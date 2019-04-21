namespace POS.Core
{
    public class OrderItem 
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public ProductUnit Unit { get; set; }
        public int Quantity { get; set; }
    }
}
