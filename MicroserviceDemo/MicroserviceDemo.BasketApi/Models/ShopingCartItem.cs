namespace MicroserviceDemo.BasketApi.Models
{
    public class ShopingCartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
    }
}