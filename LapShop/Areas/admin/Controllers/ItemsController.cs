using Microsoft.AspNetCore.Mvc;
using LapShop.Bl;
using LapShop.Models;
using LapShop.Utalties;
using Microsoft.AspNetCore.Authorization;

namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin,data entry")]

    [Area("admin")]
    public class ItemsController : Controller
    {
        public ItemsController(IItems item,ICategories categories,
            IItemType itemType,IO os)
        {
            oclsItems=item;
            oClsCategories=categories;

            oClsItemTypes=itemType;

            oClsOs = os;

        }
        IItems oclsItems;

        ICategories oClsCategories;

        IItemType oClsItemTypes ;

        IO oClsOs;
        public IActionResult List()
        {
            ViewBag.lstCateg = oClsCategories.GetAll();
            var items = oclsItems.GetAllDataForIt(null);
            return View(items);
        }

        public IActionResult Search(int id)
        {
            ViewBag.lstCateg = oClsCategories.GetAll();
            var items = oclsItems.GetAllDataForIt(id);
            return View("List",items);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? itemId)
        {
            var item = new TbItem();
            ViewBag.lstCateg = oClsCategories.GetAll();
            ViewBag.lstItemType = oClsItemTypes.GetAll();
            ViewBag.lstOs = oClsOs.GetAll();
            if (itemId != null)
            {

                item = oclsItems.GetById(Convert.ToInt32(itemId));
            }

            return View(item);
        }


        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {

            if (!ModelState.IsValid)
                return View("Edit", item);
            item.ImageName = await Helper.UploadImage(Files, "items");
            oclsItems.Save(item);
            return RedirectToAction("List");
        }


        public IActionResult Delete(int ItemId)
        {
            oclsItems.Dekete(ItemId);
            return RedirectToAction("List");

        }

		
	}
}
