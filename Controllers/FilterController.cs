using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCStokTakip.Filters; //filters file and class import
using MVCStokTakip.Models; //user import
using System.Configuration;
using System.Security.Claims;

namespace MVCStokTakip.Controllers
{
	public class FilterController : Controller
	{
		private readonly Context _context; //generate constructor

		public FilterController(Context context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[UserControl]
		public IActionResult UyelikBilgilerim()
		{//UserControl sınıfında filter ile override metot kullandığımız için bu kısımda kod sadeliğine yapılır
			var id = HttpContext.Session.GetInt32("kullaniciId");
			var kullanici = _context.Users.Find(id);
			return View(kullanici);
		}

		[UserControl] //aşağıdaki action metoduna UserControl filter içinde uyguladığımız kontrolü yap.
		[Authorize] //kullanıcı sisteme girişini kontrol eden metot
		public IActionResult Edit()
		{
			var id = HttpContext.Session.GetInt32("kullaniciId");
			var kullanici = _context.Users.Find(id);
			return View(kullanici);
		}

		[HttpPost]
		[UserControl]
		[Authorize]
		public IActionResult Edit(User uye)
		{
			_context.Users.Update(uye);
			_context.SaveChanges();
			return RedirectToAction("UyelikBilgilerim");
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(); //kullanıcı oturumunu kapat
			HttpContext.Session.Clear(); //sessionları temizle
			return View("Index"); //yönlendir
		}
	}
}