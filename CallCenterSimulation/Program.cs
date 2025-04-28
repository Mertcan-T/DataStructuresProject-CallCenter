using CallCenterSimulation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�n� yap�land�r
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity yap�land�rmas�n� ekle
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// SignalR'� yap�land�r
builder.Services.AddSignalR();

// MVC'yi ekle
builder.Services.AddControllersWithViews();

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
app.MapHub<CallCenterHub>("/callCenterHub");

// MVC routing yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
