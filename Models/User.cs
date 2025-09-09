using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //mesajları türkçeleştirmek için kullanılır.

namespace MVCStokTakip.Models
{//kullanılacak kullanıcı sınıf
	public class User
	{
		public int Id { get; set; }
		
		[Required(ErrorMessage = "Ad alanı boş geçilemez!"), StringLength(50)] //her attribute altındaki property için geçerlidir 
																			   //String uzunluğu en fazla 50 karakter olsun
		public string Name { get; set; }
		
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(50)]
		public string? Surname { get; set; }
		
		[EmailAddress(ErrorMessage = "Geçersiz Email Adresi!"), StringLength(50)]
		public string? Email { get; set; }
		
		[Phone(ErrorMessage = "Geçersiz Telefon Formatı!")]
		public string? Phone { get; set; }
		
		[Display(Name = "TC Kimlik Numarası"), StringLength
			(11)] //ekranda TcKimlikNo yerine kullanıcıya göre görünüm sağlanır.
		[MinLength(11, ErrorMessage = "{0} 11 karakter olmalıdır!")] //en kısı ve uzun uzunluk durumunda da 11 karakterden aşağıda olmamalı
		[MaxLength(11, ErrorMessage = "{0} 11 karakter olmalıdır!")]
		public string? IdentificationNumber { get; set; }

		[Display(Name = "Doğum Tarihi")]
		public DateTime? Birthday { get; set; }
		
		[Display(Name = "Kullanıcı Adı")]
		public string Nickname { get; set; }
		
		[Display(Name = "Şifre")]
		[StringLength(15, ErrorMessage = "{0} {2} Karakterden Az Olamaz!", MinimumLength = 3)]
		[Compare("Password")] //sifre property si ile karşılaştır
		public string Password { get; set; }
		public string? RepeatPassword { get; set; }
	}
}