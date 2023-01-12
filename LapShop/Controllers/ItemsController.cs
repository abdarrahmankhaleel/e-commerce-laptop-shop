using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItems oClsItems;
        private readonly IItemImgs itemImgs;

        public ItemsController(IItems oClsItems,IItemImgs itemImgs)
        {
            this.oClsItems = oClsItems;
            this.itemImgs = itemImgs;
        }
        public IActionResult ItemDetail(int Itemid)
        {
            VmItemDetails vmItemDetails = new VmItemDetails();
            vmItemDetails.lstItemImges = itemImgs.GetByItemId(Itemid);
            vmItemDetails.item = oClsItems.GetVwItemById(Itemid);
            vmItemDetails.lstRelatedItems = oClsItems.GetRelatedItems(Itemid);
            return View(vmItemDetails);
        }

		public IActionResult ItemsList()
		{
			return View();
		}
	}
}
