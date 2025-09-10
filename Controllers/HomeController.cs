using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCStokTakip.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MVCStokTakip.Controllers
{
	[AllowAnonymous] //authorize i�leminden muaf tutulacak sayfa
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly Context _context; //generate constructor

		public HomeController(ILogger<HomeController> logger, Context context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
        {
			return View();
        }

		[HttpPost]
		//veri g�nderiminde input i�indeki name k�sm� e�le�mez ise veri g�nderimi do�ru d�nmez
		public async Task<IActionResult> Login(string email, string password) //await metodunu kullanabilmek i�in asenkron i�lemler yap�lmas�n� sa�lar (make)
		{
			var kullanici = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
			if (kullanici != null)
			{
				HttpContext.Session.SetString("Email", kullanici.Email);
				var haklar = new List<Claim>() //kullan�c� haklar� tan�mlad�k
				{
					new(ClaimTypes.Email, kullanici.Email), //claim = hak (kullan�c�ya tan�mlanan haklar)
						new(ClaimTypes.Role, "admin")
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); //kullan�c� i�in bir kimlik olu�turduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu s�n�ftan bir nesne olu�turup bilgilerde sakl� haklar ile kural olu�turulabilir
				await HttpContext.SignInAsync(claimsPrincipal); //yukar�daki yetkilerle sisteme giri� yapt�k
				return RedirectToAction("Index", "Products");
			}
			else 
				@TempData["Message"] = "<div class='alert alert-danger'>Giri� Ba�ar�s�z</div>";
			
				return RedirectToAction("Index", "Home");
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

		public ActionResult LogOut()
		{
			//FormsAuthentication.SignOut(); //.net faramework mvc ��k��
			HttpContext.SignOutAsync();
			//Session.Abandon(); //b�rakmak, terketmek
			return RedirectToAction("Index", "Home");
		}
	}
}
