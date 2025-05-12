using CallCenterSimulation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CallCenterSimulation.Hubs; // SignalR Hub'� kullanaca��m�z i�in ekledik

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�n� yap�land�r (�u anda kullanmasan bile dursun istiyorsan)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity yap�land�rmas�n� ekle (�u anda kullanm�yorsun, istersen sonra kald�r�r�z)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// MVC'yi ekle
builder.Services.AddControllersWithViews();

// SignalR'� ekle (sadece bir kez!)
builder.Services.AddSignalR();

var app = builder.Build();

// E�er geli�tirme ortam�ndaysak, hata sayfas� g�ster
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS y�nlendirmesi ve statik dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing ayarlar�
app.UseRouting();

// Authorization ve Authentication (�imdilik yoksa sadece routing kullan�l�yor)
app.UseAuthorization();

// SignalR Hub y�nlendirmesini yap
app.MapHub<CallCenterHub>("/callCenterHub"); // Buras� SignalR ba�lant�s� i�in �nemli!

// MVC routing yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
