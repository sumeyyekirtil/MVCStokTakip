using Microsoft.AspNetCore.Authentication.Cookies;
using MVCStokTakip.Models;

namespace MVCStokTakip
{
	//ManageNuget : install ->
	//1 -> EntityFrameworkCore.SqlServer (for connection)
	//2 -> EntityFrameworkCore.Tools (for tools)
    //3 -> VisualStudio.Web.CodeGeneration.Desing (for desing)
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			//Bu kýsým crud controller çalýþmasýný saðlýyor hata :InvalidOperationException: Unable to resolve service for type '' while attempting to activate ''.

			//projede kullanacaðýmýz dbcontext sýnýfýmýzý uygulamaya tanýtýyoruz.
			builder.Services.AddDbContext<Context>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			//Schema hatasý çözümü
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
			{
				x.LoginPath = "/Filter/Login"; //admin oturum açma sayfamýzý belirttik
			});
			var app = builder.Build(); //builder nesnesi üzerinden eklenen servislerle beraber app nesnesi oluþturuluyor

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
