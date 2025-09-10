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
		//veri gönderiminde input içindeki name kýsmý eþleþmez ise veri gönderimi doðru dönmez
		public async Task<IActionResult> Login(string email, string password) //await metodunu kullanabilmek için asenkron iþlemler yapýlmasýný saðlar (make)
		{
			var kullanici = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
			if (kullanici != null)
			{
				HttpContext.Session.SetString("Email", kullanici.Email);
				var haklar = new List<Claim>() //kullanýcý haklarý tanýmladýk
				{
					new(ClaimTypes.Email, kullanici.Email), //claim = hak (kullanýcýya tanýmlanan haklar)
						new(ClaimTypes.Role, "admin")
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); //kullanýcý için bir kimlik oluþturduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu sýnýftan bir nesne oluþturup bilgilerde saklý haklar ile kural oluþturulabilir
				await HttpContext.SignInAsync(claimsPrincipal); //yukarýdaki yetkilerle sisteme giriþ yaptýk
				return RedirectToAction("Index", "Products");
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

		public ActionResult LogOut()
		{
			//FormsAuthentication.SignOut(); //.net faramework mvc çýkýþ
			HttpContext.SignOutAsync();
			//Session.Abandon(); //býrakmak, terketmek
			return RedirectToAction("Index", "Home");
		}
	}
}
