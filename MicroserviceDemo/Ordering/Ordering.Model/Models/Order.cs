using Ordering.Model.Common;

namespace Ordering.Model.Models
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double TotalPrice { get; set; }
        public int AddressId { get; set; }
        public int PaymentId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set ; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Address Address { get; set; }
        public Payment Payment { get; set; }    
    }
}