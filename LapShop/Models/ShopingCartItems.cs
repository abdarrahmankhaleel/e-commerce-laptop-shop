namespace LapShop.Models
{
    public class ShopingCartItems
    {
        public int ItemId { get; set; }
        public string ImagName { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int Qty { get; set; }

    }
}
