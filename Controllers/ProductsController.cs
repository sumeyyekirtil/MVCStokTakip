using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStokTakip.Models;

namespace MVCStokTakip.Controllers
{
	[Authorize] //kullanıcı login yapmadan erişemez (her metota tek tek tanımlama yapılmaması için)
	public class ProductsController : Controller
	{
		private readonly Context _context;

		public ProductsController(Context context)
		{
			_context = context;
		}

		// GET: ProductsController
		public ActionResult Index()
		{
			return View(_context.Products); //model view e görünüm vermesi için sadece index context den sınıf kullanılır
											  //edit, create, delete, details de kullanılırsa InvalidOperationException: The model item passed into the ViewDataDictionary is of type 'Microsoft.EntityFrameworkCore.Internal.InternalDbSet`1[ .Models.Category]', but this ViewDataDictionary instance requires a model item of type ' .Models.Category'. error
		}

		// GET: ProductsController/Details/5
		public ActionResult Details(int id)
		{
			var bilgi = _context.Products.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(bilgi);
		}

		// GET: ProductsController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ProductsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//crud ekleme
					_context.Products.Add(collection);
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

		// GET: ProductsController/Edit/5
		public ActionResult Edit(int id)
		{
			var urun = _context.Products.Find(id); //uyeler tablosundan route dan gelen id ile eşleşen kaydı bul ve ekrana gönder.
			return View(urun);
		}

		// POST: ProductsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Product collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//crud güncelleme
					_context.Products.Update(collection);
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

		// GET: ProductsController/Delete/5
		public ActionResult Delete(int id)
		{
			var urun = _context.Products.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(urun);
		}

		// POST: ProductsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Product collection)
		{
				try
				{
					//crud silme
					_context.Products.Remove(collection); //ekrandan gelen üye nesnesini silinecek olarak işaretle
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
