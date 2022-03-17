using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult Secret()
		{
			return View();
		}

		public IActionResult Authenticate()
		{
			var basicClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,"Ron"),
				new Claim(ClaimTypes.Email,"Ron@yopmail.com"),
				new Claim("Auth.Saying","Very nice user."),
			};

			var licenseClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email,"Ron@yopmail.com"),
				new Claim("DrivingLicense","A+"),
			};

			var authIdentity = new ClaimsIdentity(basicClaims, "Basic Identity");
			var lincenseIdentity = new ClaimsIdentity(licenseClaims, "Goverment");

			var userPrincipal = new ClaimsPrincipal(new[] { authIdentity, lincenseIdentity });

			HttpContext.SignInAsync(userPrincipal);

			return RedirectToAction("Index"); 
		}

	}
}
