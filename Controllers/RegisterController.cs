using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenTable.Models;
using OpenTable.Models.DataLayer;

namespace OpenTable.Controllers
{
    public class RegisterController : Controller
    {
        private OpenTableDbContext context;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public RegisterController(OpenTableDbContext ctx, UserManager<User> userMngr,
            SignInManager<User> signInMngr)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            context = ctx;
        }

        [HttpGet]
        public IActionResult Index() => View();

        //[HttpPost]
        //public IActionResult Index(Customer customer)
        //{
        //    if (TempData["okEmail"] == null)
        //    {
        //        string msg = Check.EmailExists(context, customer.EmailAddress);
        //        if (!String.IsNullOrEmpty(msg))
        //        {
        //            ModelState.AddModelError(nameof(Customer.EmailAddress), msg);
        //            TempData["SuccessMessage"] = "Please fix the error";
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        context.Customers.Add(customer);
        //        context.SaveChanges();
        //        TempData["SuccessMessage"] = "Your registration is completed. Welcome to Open Table!";
        //        return RedirectToAction("Welcome");
        //    }
        //    else
        //    {
        //        return View(customer);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Welcome() => View();
    }
}
