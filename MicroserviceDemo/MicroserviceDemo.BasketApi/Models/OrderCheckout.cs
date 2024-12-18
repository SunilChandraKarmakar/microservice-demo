namespace MicroserviceDemo.BasketApi.Models
{
    public class OrderCheckout
    {
        public OrderModel OrderModel { get; set; }
        public PaymentModel PaymentModel { get; set; }
        public AddressModel AddressModel { get; set; }
    }

    public class OrderModel
    {
        public string UserName { get; set; }
        public double TotalPrice { get; set; }
        public int AddressId { get; set; }
        public int PaymentId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class PaymentModel
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public DateTime Expiration { get; set; }
        public string PaymentMethod { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class AddressModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressLineOne { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}