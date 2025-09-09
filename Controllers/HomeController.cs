using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCStokTakip.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MVCStokTakip.Controllers
{
	[AllowAnonymous] //authorize iþleminden muaf tutulacak sayfa
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
		public async Task<IActionResult> Login(string kullaniciAdi, string sifre) //await metodunu kullanabilmek için asenkron iþlemler yapýlmasýný saðlar (make)
		{
			var kullanici = _context.Users.FirstOrDefault(u => u.Email == kullaniciAdi && u.Password == sifre);
			if (kullanici != null)
			{
				HttpContext.Session.SetInt32("kullaniciId", kullanici.Id);
				var haklar = new List<Claim>() //kullanýcý haklarý tanýmladýk
				{
					new(ClaimTypes.Email, kullanici.Email), //claim = hak (kullanýcýya tanýmlanan haklar)
						new(ClaimTypes.Role, "admin@gmail.com")
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Index"); //kullanýcý için bir kimlik oluþturduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu sýnýftan bir nesne oluþturup bilgilerde saklý haklar ile kural oluþturulabilir
				await HttpContext.SignInAsync(claimsPrincipal); //yukarýdaki yetkilerle sisteme giriþ yaptýk
				return RedirectToAction("Index", "Product");
			}
			else 
				@TempData["Message"] = "<div class='alert alert-danger'>Giriþ Baþarýsýz</div>";
			
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
