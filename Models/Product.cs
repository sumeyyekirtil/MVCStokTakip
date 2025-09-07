namespace MVCStokTakip.Models
{//kullanılacak ürün sınıf
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Photo { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public DateTime RecordDate { get; set; } = DateTime.Now;
	}
}