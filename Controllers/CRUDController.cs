using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVCStokTakip.Models; //Context class import

//Program cs ye builder AddDBContext<> sınıfı ekleyelim. Bu kısım crud controller çalışmasını sağlıyor.
//hata :InvalidOperationException: Unable to resolve service for type '' while attempting to activate ''.

//SqlException: Cannot open database "" requested by the login. The login failed.
//Login failed for user ''. HATASI -> db de böyle bir veri tablosu yok
//ERROR resolution -> tools - package nuget console - add-migration InitialCreate - (program üzerinde yazdırdı) - db (sqlserver) alma - update-database - finish (ignore hatası durumunda) ->

//optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

//New controller - with read write action - add
namespace MVCStokTakip.Controllers
{
	//Get açıldığında gelen gönderim yolu, Post tıklandığındaki gönderim yoludur
	//Get metodunda liste tanımlanıp gösterime açılır
	//Post metodu olan kısma yapılacak işlem detayı girilir
	public class CRUDController : Controller
	{
		private readonly Context _context; // _context - sağ tık - generate constructor (yapıcı sınıfı kurduk)

		public CRUDController(Context context)
		{
			_context = context;
		}

		// GET: CRUDController
		public ActionResult Index() //add view - razor - list - user class - context ile görünüm sayfası oluşturuldu
		{
			return View(_context.Users); //kurucu metot ile User sınıfını index e bağladık
		}

		// GET: CRUDController/Details/5
		public ActionResult Details(int id) //add view - razor - details - user class - content ile görünüm sayfası oluşturuldu
		{
			var kayit = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(kayit);
		}

		// GET: CRUDController/Create
		public ActionResult Create() //add view - razor - create - user class - content ile görünüm sayfası oluşturuldu
		{
			return View();
		}

		// POST: CRUDController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(User collection) //user olarak değiştirildi sınıftan alınıp okunması için
		{
			try
			{
				_context.Users.Add(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CRUDController/Edit/5
		public ActionResult Edit(int id) //add view - razor - edit - user class - content ile görünüm sayfası oluşturuldu
		{
			var kayit = _context.Users.Find(id); //uyeler tablosundan route dan gelen id ile eşleşen kaydı bul ve ekrana gönder.
			return View(kayit);
		}

		// POST: CRUDController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			try
			{
				_context.Users.Update(collection); //ekrandan gelen modeli veritabanındaki kaydı değiştirecek şekilde ayarla
				_context.SaveChanges(); //değişiklikleri db kaydet

				return RedirectToAction(nameof(Index)); //Index isimli action metoduna yönlendir
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!"); //hata oluşursa yazdır
			}
			return View(collection);
		}

		// GET: CRUDController/Delete/5
		public ActionResult Delete(int id)
		{
			var kayit = _context.Users.Find(id); //id ye ulaşıp kayıt detaylarını yazdırma işlemi
			return View(kayit);
		}

		// POST: CRUDController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, User collection)
		{
			try
			{
				_context.Users.Remove(collection); //ekrandan gelen üye nesnesini silinecek olarak işaretle
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
