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
			return View();
		}

		// GET: NewUsersController/Details/5
		public ActionResult Details(int id)
		{
			return View();
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

				return RedirectToAction("Index", "CRUD");
			}
			catch
			{
				return View();
			}
		}

		// GET: NewUsersController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: NewUsersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			try
			{
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
			return View();
		}

		// POST: NewUsersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, User collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View(collection);
			}
		}
	}
}
