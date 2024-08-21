using Ordering.Model.Common;

namespace Ordering.Model.Models
{
    public class Address : BaseEntity
    {
        public Address()
        {
            Orders = new HashSet<Order>();    
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressLineOne { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Order> Orders { get; set; }  
    }
}