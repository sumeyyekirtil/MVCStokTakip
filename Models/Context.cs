using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics; //pending hatası için ConfigureWarnings ile geldi

//Context classı model e bağlı olup bağlantı şemasını çizmeye yarar.
//override (ezici-farklı davranan metot) yapabileceğimiz iki on metodu var 1- OnConfiguring, 2- OnModelCreating
//1 -> Connection string oluşturulup builder tamamlanıyor
//2 -> Db için örnek veri bütünü yollanıyor
//3. Adım Tools - NuGet Packege - Console ile DB SqlServer bağlantı ile oluşturuluyor (create)
namespace MVCStokTakip.Models
{
	public class Context : DbContext  //DbContext sınıfı Nuget dan yüklediğimiz entity framework core paketleri ile gelmektedir ve ef ile vt işlemlerini yapabilmemizi sağlar
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //override on enter
		{
			optionsBuilder.UseSqlServer("server=ASUS-PRO; database=MVCStock; Integrated Security=True; TrustServerCertificate=True");
			optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); //vt oluştururken aldığımız PendingModelChangesWarning hatasının çözümü
		    //ignore - görmezden gel
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = 1,
					Email = "admin@gmail.com",
					Name = "User",
					Surname = "Admin",
					Birthday = DateTime.Now,
					Nickname = "admin",
					Password = "123",
					RepeatPassword = "123",
					IdentificationNumber = "12345678911",
					Phone = "26262626221"
				});
			base.OnModelCreating(modelBuilder);
		}
	}
}