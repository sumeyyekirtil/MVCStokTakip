using Microsoft.AspNetCore.Mvc;
using MVCStokTakip.Models;

namespace MVCStokTakip.Controllers
{
	//Kullanıcıdan alınan veri kontrolleri yapılacak sayfa

	public class NewUserController : Controller
	{   //Hangi ad ile view oluşturulursa o ad ile action (olay) oluşturulmalı

		public IActionResult Index() //New controller - ındex(create) - add view - razor view - create - User.class - Context ile sayfada görünüm hazırlıyoruz
		{
			return View();
		}
		//verileri şifreli alma
		[HttpPost]
		public IActionResult Index(User user) //User class import
		{
			if (ModelState.IsValid) //Eğer modeldeki validasyon kurallarına uyulmuşsa, tersi için !ModelState.IsValid kullanılır
			{
				//kayıt ekle-güncelle-sil vb
			}
			else
			{
				ModelState.AddModelError("", "Lütfen Tüm Zorunlu Doldurunuz!"); //Ekrandaki validasyonkontrol alanına mesaj gönderebiliyoruz
			}
			return View(user);
		}
	}
}