using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStokTakip.Models;

namespace MVCStokTakip.Controllers
{
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
			return View(_context.Products);
		}

		// GET: ProductsController/Details/5
		public ActionResult Details(int id)
		{
			return View();
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
			return View();
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

		// GET: ProductsController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: ProductsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Product collection)
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
	}
}
