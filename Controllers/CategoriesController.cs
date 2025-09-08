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
			return View(_context.Categories);
		}

		// GET: CategoriesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CategoriesController/Create
		public ActionResult Create()
		{
			return View(_context.Categories);
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
			return View();
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
	}
}
