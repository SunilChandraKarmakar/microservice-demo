namespace MicroserviceDemo.BasketApi.Models
{
    public class ShopingCart
    {
        public ShopingCart()
        {
            ShopingCartItems = new List<ShopingCartItem>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public double TotalPrice
        {
            get
            {
                var price = 0.00;
                foreach (var item in ShopingCartItems)
                    price += price + item.Price;

                return price;
            }

            private set { }
        }

        public ICollection<ShopingCartItem> ShopingCartItems { get; set; }
    }
}