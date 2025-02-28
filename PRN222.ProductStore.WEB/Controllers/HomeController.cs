using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PRN222.ProductStore.WEB.Models;

namespace PRN222.ProductStore.WEB.Controllers;

public class HomeController : Controller
{
	const string CookieUserId = "UserId";
	const string CookieUserName = "UserName";

	private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
		CookieOptions options = new CookieOptions()
		{
			Domain = "localhost", // Set the domain for the cookie
			Path = "/", // Cookie is available within the entire application
			Expires = DateTime.Now.AddDays(7), // Set cookie expiration to 7 days from now
			Secure = false, // Ensure the cookie is only sent over HTTPS (set to false for local development)
			HttpOnly = true, // Prevent client-side scripts from accessing the cookie
			IsEssential = true // Indicates the cookie is essential for the application to function
		};
		// Append UserId to the cookies
		Response.Cookies.Append(CookieUserId, "1234567", options);

		// Append UserName to the cookies
		Response.Cookies.Append(CookieUserName, "pranaya@dotnettutotials.net", options);

		return View();
    }

	public string About()
	{
		// Accessing the Cookie Data inside a Method
		// Try to get the UserName from the cookies, if it does not exist, default to null
		string? UserName = Request.Cookies.ContainsKey(CookieUserName) ? Request.Cookies[CookieUserName] : null;
		// Try to get the UserId from the cookies and convert it to an integer
		// If it does not exist or conversion fails, default to null
		int? UserId = null;
		if (Request.Cookies.ContainsKey(CookieUserId))
		{
			bool isValidInt = int.TryParse(Request.Cookies[CookieUserId], out int parsedUserId);
			if (isValidInt)
			{
				UserId = parsedUserId;
			}
		}
		// Create a message with the UserName and UserId
		string Message = $"UserName: {UserName}, UserId: {UserId}";
		return Message;
	}

	public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

	public string DeleteCookie()
	{
		// Create CookieOptions to match the domain and path used when setting the cookies
		CookieOptions options = new CookieOptions
		{
			Domain = "localhost", // Match the domain used when setting the cookie
			Path = "/" // Match the path used when setting the cookie
		};
		// Delete the cookies from the browser
		Response.Cookies.Delete(CookieUserId, options);
		Response.Cookies.Delete(CookieUserName, options);
		return "Cookies are Deleted";
	}
}
