using bl;
using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LapShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IItems itemService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISalesInvoice salesInvoiceService;

        public OrderController(IItems items,UserManager<ApplicationUser> userManager
            , ISalesInvoice salesInvoice)
        {
            this.itemService = items;
            _userManager = userManager;
            this.salesInvoiceService = salesInvoice;
        }

        public IActionResult Cart()
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<VmShopingCart>(sesstionCart);
            return View(cart);
        }
        public IActionResult AddToCart(int itemId)
        {
            VmShopingCart cart;

            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<VmShopingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new VmShopingCart();

            var item = itemService.GetById(itemId);

            var itemInList = cart.lstShopingCartItems.Where(a => a.ItemId == itemId).FirstOrDefault();

            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.Total = itemInList.Qty * itemInList.Price;
            }
            else
            {
                cart.lstShopingCartItems.Add(new ShopingCartItems
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice
                });
            }
            cart.Total = cart.lstShopingCartItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Cart");
        }


        public IActionResult MyOrders() 
        {

            return View();
        }




        [Authorize] /// يعني لو انته لوقن هيخشن علاكشن ويرجع الفيهاية بتاعتها لونه مش لوقن هيروح على اكشن اللوقن
        public async Task<IActionResult> OrderSeccess() 
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<VmShopingCart>(sesstionCart);
            await SaveOrder(cart);
            return View();
        }



        async Task SaveOrder(VmShopingCart oShopingCart)
        {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
                foreach (var item in oShopingCart.lstShopingCartItems)
                {
                    lstInvoiceItems.Add(new TbSalesInvoiceItem()
                    {
                        ItemId = item.ItemId,
                        Qty = item.Qty,
                        InvoicePrice = item.Price
                    });
                }

                var user = await _userManager.GetUserAsync(User);

                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now,
                };

                salesInvoiceService.Save(oSalesInvoice, lstInvoiceItems, true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
