namespace EventBus.Messages.Events
{
    public class OrderCheckoutEvent : BaseEvent
    {
        // For order
        public string UserName { get; set; }
        public double TotalPrice { get; set; }
        public int AddressId { get; set; }
        public int PaymentId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // For payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public DateTime Expiration { get; set; }
        public string PaymentMethod { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string? UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }

        // For address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressLineOne { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string? UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
    }
}