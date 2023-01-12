namespace LapShop.Models
{
    public class VmShopingCart
    {
        public VmShopingCart()
        {
            lstShopingCartItems = new List<ShopingCartItems>();
        }
        public List<ShopingCartItems> lstShopingCartItems { get; set; }

        public decimal Total { get; set; }
    }
}
