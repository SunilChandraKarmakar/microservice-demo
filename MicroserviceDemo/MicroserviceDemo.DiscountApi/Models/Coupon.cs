namespace MicroserviceDemo.DiscountApi.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Discription { get; set; }
        public double Amount { get; set; }
    }
}