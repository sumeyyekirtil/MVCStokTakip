using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStokTakip.Models;

namespace MVCStokTakip.Controllers
{
	[Authorize] //kullanıcı login yapmadan erişemez (her metota tek tek tanımlama yapılmaması için)
	public class NewUsersController : Controller
	{
		private readonly Context _context;

		public NewUsersController(Context context)
		{
			_context = context;
		}

		// GET: NewUsersController
		public ActionResult Index()
		{
			return View(_context.Users);
		}

		// GET: NewUsersController/Details/5
		public ActionResult Details(int id)
		{
			var bilgi = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(bilgi);
		}

		// GET: NewUsersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: NewUsersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(User collection)
		{
			try
			{
				//crud ekleme
				_context.Users.Add(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: NewUsersController/Edit/5
		public ActionResult Edit(int id)
		{
			var kayit = _context.Users.Find(id); //uyeler tablosundan route dan gelen id ile eşleşen kaydı bul ve ekrana gönder.
			return View(kayit);
		}

		// POST: NewUsersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			try
			{
				//crud güncelleme
				_context.Users.Update(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: NewUsersController/Delete/5
		public ActionResult Delete(int id)
		{
			var kayit = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(kayit);
		}

		// POST: NewUsersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, User collection)
		{
				try
				{
					//crud silme
					_context.Users.Remove(collection); //ekrandan gelen üye nesnesini silinecek olarak işaretle
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			return View(collection);
		}
	}
}
