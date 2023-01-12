using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using Microsoft.EntityFrameworkCore;
using LapShop.Bl;

namespace LapShop.Controllers
{

    public class HomeController : Controller
    {

            IItems oclsitems;
        public HomeController(IItems items)
        {
            oclsitems = items;
        }
        public IActionResult Index()
        {
            VmHomePage vmHomePage =new VmHomePage();
            vmHomePage.lstRecemonededIttems = oclsitems.GetAllDataForIt(null).Skip(20).Take(3).ToList();
            vmHomePage.lstAllItems=oclsitems.GetAllDataForIt(null).Skip(30).Take(2).ToList();
            vmHomePage.lstFreeDilevryItems=oclsitems.GetAllDataForIt(null).Skip(40).Take(2).ToList();
            return View(vmHomePage);
          
        }
    }
}
