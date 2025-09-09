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
		public async Task<IActionResult> Login(string kullaniciAdi, string sifre) //await metodunu kullanabilmek i�in asenkron i�lemler yap�lmas�n� sa�lar (make)
		{
			var kullanici = _context.Users.FirstOrDefault(u => u.Email == kullaniciAdi && u.Password == sifre);
			if (kullanici != null)
			{
				HttpContext.Session.SetInt32("kullaniciId", kullanici.Id);
				var haklar = new List<Claim>() //kullan�c� haklar� tan�mlad�k
				{
					new(ClaimTypes.Email, kullanici.Email), //claim = hak (kullan�c�ya tan�mlanan haklar)
						new(ClaimTypes.Role, "admin@gmail.com")
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Index"); //kullan�c� i�in bir kimlik olu�turduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu s�n�ftan bir nesne olu�turup bilgilerde sakl� haklar ile kural olu�turulabilir
				await HttpContext.SignInAsync(claimsPrincipal); //yukar�daki yetkilerle sisteme giri� yapt�k
				return RedirectToAction("Index", "Product");
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
	}
}
