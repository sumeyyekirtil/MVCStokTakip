using Microsoft.AspNetCore.Mvc;

namespace MVCStokTakip.Controllers
{
	public class PhotoUploadController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(IFormFile? dosya) //? null gelebilir
													 //Mvc de dosya yükleme IFormFile interface i ile yapılıyor. Burada dosya isminin ekrandaki file upload name i ile aynı olması gerekir yoksa dosya yüklenmez!
		{
			if (dosya != null)
			{
				var uzanti = Path.GetExtension(dosya.FileName); //yüklenecek dosya uzantısı
				string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/";
				var klasorVarmi = Directory.Exists(klasor);
				TempData["Message"] = "klasorVarmi: " + klasorVarmi;
				if (klasorVarmi == false) //eğer sunucuda bu konumda klasör yoksa
				{
					var sonuc = Directory.CreateDirectory(klasor); //ana dizine Images klasörü oluştur
					TempData["Message"] += " - Klasör Oluşturuldu.. " + sonuc;
				}
				if (uzanti == ".jpg" || uzanti == ".jpeg" || uzanti == ".png" || uzanti == ".gif") //sadece bu uzantılardaki dosyaları kabul et
				{
					//1.Yöntem Random (Rastgele) İsimle Dosya Yükleme
					/*
					var randomFileName = Path.GetRandomFileName(); //rastgele dosya ismi oluşturma metodu
					var fileName = Path.ChangeExtension(randomFileName, ".jpg"); //dosya adı ve uzantısını değiştirip birleştirdik
					var path = Path.Combine(klasor, fileName); //klasör ve resim adını birleştirdik
					using var stream = new FileStream(path, FileMode.Create); //resmi sunucuya yükledik
					TempData["Resim"] = dosya.FileName;
					*/

					//2.yöntem - Resmi Kendi Adıyla Yükleme
					var dosyaAdi = Path.GetFileName(dosya.FileName);
					var yol = Path.Combine(klasor, dosyaAdi);

					using var stream = new FileStream(yol, FileMode.Create); //Buradaki using ifadesi stream isimli değişkenin işinin bittikten sonra bellekten atık
					dosya.CopyTo(stream); //resmi sunucuya yükledik
					TempData["Resim"] = dosyaAdi;
				}
				else
				{
					TempData["Message"] += "Sadece Resim Dosyası Yükleyebilirsiniz!";
				}
			}
			return View();
		}

		[HttpPost]
		public IActionResult ResimSil(string resimYolu)
		{
			if (System.IO.File.Exists(resimYolu)) //eğer gelen adreste böyle bir dosya varsa
			{
				System.IO.File.Delete(resimYolu);
			}
			return View("Index");
		}
	}
}