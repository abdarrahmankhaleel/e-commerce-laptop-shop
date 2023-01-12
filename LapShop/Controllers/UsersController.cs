using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using Microsoft.AspNetCore.Identity;

namespace LapShop.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
      
        public IActionResult Register()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
        
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);///register

                if (result.Succeeded)
                {
                    var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);//login
                    //var myUser = await _userManager.FindByEmailAsync(user.Email);
                    //await _userManager.AddToRoleAsync(myUser, "Customer");
                    if (loginResult.Succeeded)
                        Redirect("/Order/OrderSuccess");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            return View(new UserModel());
        }


      
        public IActionResult Login(string returnUrl)// الرترن يو ار ال بيجي هنا فاحنا فانه كتبته كدا باكشن اللوقن بوست مش هيستقبله  بما انه بيجي هنا فلازم تحتفظ فيه
        {// الوقن بتاعت البوست بتستقبل الفورم الي هي المادل فعشان هيك حطينا بروبرتي بالمادل وخزنا فيها الريترن يو ار ال
            UserModel userModel = new UserModel()/// فيو اللوقن بتوخذ فيو ادل فورمة عادية عشان اليوز يعبيها 
            {
                ReturnUrl = returnUrl
            };
            return View(userModel);/// بعثته للفيو لكن الفيو مش هتتفظ فيه الا اذا حطيته بالفوم واس بي فور
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);//login
                if (loginResult.Succeeded)
                {
                    if(string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect("~/");
                    return Redirect(model.ReturnUrl);

                }
            }
            catch (Exception ex)
            {

            }
            return View(new UserModel());
        }

        public async Task <IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();    
            return Redirect("~/");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        
    }
}
