using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStokTakip.Models;

namespace MVCStokTakip.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly Context _context;

		public CategoriesController(Context context)
		{
			_context = context;
		}

		// GET: CategoriesController
		public ActionResult Index()
		{
			return View(_context.Categories); //model view e görünüm vermesi için sadece index context den sınıf kullanılır
											  //edit, create, delete, details de kullanılırsa InvalidOperationException: The model item passed into the ViewDataDictionary is of type 'Microsoft.EntityFrameworkCore.Internal.InternalDbSet`1[ .Models.Category]', but this ViewDataDictionary instance requires a model item of type ' .Models.Category'. error
		}

		// GET: CategoriesController/Details/5
		public ActionResult Details(int id)
		{
			var bilgi = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(bilgi);
		}

		// GET: CategoriesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Category collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//crud ekleme
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: CategoriesController/Edit/5
		public ActionResult Edit(int id)
		{
			var kayit = _context.Users.Find(id); //uyeler tablosundan route dan gelen id ile eşleşen kaydı bul ve ekrana gönder.
			return View(kayit);
		}

		// POST: CategoriesController/Edit/5
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Category collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//crud güncelleme
					_context.Categories.Update(collection);
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: CategoriesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Category collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//crud silme
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(); //kullanıcı oturumunu kapat
			HttpContext.Session.Clear(); //sessionları temizle
			return View("Index"); //yönlendir
		}
	}
}
