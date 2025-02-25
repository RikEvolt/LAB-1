using Microsoft.AspNetCore.Mvc;
using PRN222.ProductStore.Service.BusinessModels;
using PRN222.ProductStore.Service.Services;

namespace PRN222.ProductStore.WEB.Controllers
{
    public class AccountController : Controller
    {
		private readonly IAccountService _accountService;
		private readonly IProductService _productService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public async Task<IActionResult> Index()
		{
			if (HttpContext.Session.GetString("UserId") == null)
			{
				// Redirect to the login page or display an error message
				return RedirectToAction("Login", "Account");
			}
			var myStoreContext = await _productService.GetProductsAsync();
			return View(myStoreContext);
		}


		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(AccountMemberDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = await _accountService.GetAccountByIdAsync(model.EmailAddress);

				if (user != null && user.MemberPassword == model.MemberPassword)
				{
					// Store user information in session
					HttpContext.Session.SetString("UserId", user.MemberId);
					HttpContext.Session.SetString("Username", user.FullName);

					return RedirectToAction("Index", "Products");
				}
				else
				{
					ModelState.AddModelError("", "Invalid username or password.");
				}
			}

			return View(model);
		}


		public IActionResult Logout()
		{
			HttpContext.Session.Clear(); // Clear session data
			return RedirectToAction("Login");
		}
	}

}
