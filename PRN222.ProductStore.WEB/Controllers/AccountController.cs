using Microsoft.AspNetCore.Mvc;
using PRN222.ProductStore.Service.BusinessModels;
using PRN222.ProductStore.Service.Services;
using PRN222.ProductStore.WEB.Models;
using System.Diagnostics;

namespace PRN222.ProductStore.WEB.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly IProductService _productService;

		private const string CookieUserId = "UserId";
		private const string CookieUserName = "UserName";

		public AccountController(IAccountService accountService, IProductService productService)
		{
			_accountService = accountService;
			_productService = productService;
		}

		public async Task<IActionResult> Index()
		{
			// Kiểm tra xem cookie có tồn tại không
			if (!Request.Cookies.ContainsKey(CookieUserId))
			{
				return RedirectToAction("Login", "Account");
			}

			// Lấy dữ liệu từ cookie
			string userId = Request.Cookies[CookieUserId];
			string username = Request.Cookies[CookieUserName];

			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
			{
				return RedirectToAction("Login", "Account");
			}

			// Cập nhật lại thời gian sống của Cookie
			SetCookie(CookieUserId, userId, 120);
			SetCookie(CookieUserName, username, 120);

			// Lấy danh sách sản phẩm
			return View(await _productService.GetProductsAsync());
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
				var user = await _accountService.GetAccountByEmailAsync(model.EmailAddress, model.MemberPassword);

				if (user != null && user.MemberPassword == model.MemberPassword)
				{
					// Lưu thông tin vào Cookie
					SetCookie(CookieUserId, user.MemberId, 120);
					SetCookie(CookieUserName, user.FullName, 120);

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
			// Xóa cookie khi đăng xuất
			Response.Cookies.Delete(CookieUserId);
			Response.Cookies.Delete(CookieUserName);
			return RedirectToAction("Login");
		}

		private void SetCookie(string key, string value, int expireTimeInMinutes)
		{
			var options = new CookieOptions
			{
				Domain = "localhost",
				Path = "/",
				Expires = DateTime.UtcNow.AddMinutes(expireTimeInMinutes),
				Secure = false, // Chỉnh thành `true` khi deploy lên HTTPS
				HttpOnly = true,
				IsEssential = true
			};
			Response.Cookies.Append(key, value, options);
		}
	}
}
