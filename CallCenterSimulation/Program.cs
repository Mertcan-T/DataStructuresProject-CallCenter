using CallCenterSimulation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CallCenterSimulation.Hubs; // SignalR Hub'ý kullanacaðýmýz için ekledik

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsýný yapýlandýr (þu anda kullanmasan bile dursun istiyorsan)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity yapýlandýrmasýný ekle (þu anda kullanmýyorsun, istersen sonra kaldýrýrýz)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// MVC'yi ekle
builder.Services.AddControllersWithViews();

// SignalR'ý ekle (sadece bir kez!)
builder.Services.AddSignalR();

var app = builder.Build();

// Eðer geliþtirme ortamýndaysak, hata sayfasý göster
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS yönlendirmesi ve statik dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing ayarlarý
app.UseRouting();

// Authorization ve Authentication (þimdilik yoksa sadece routing kullanýlýyor)
app.UseAuthorization();

// SignalR Hub yönlendirmesini yap
app.MapHub<CallCenterHub>("/callCenterHub"); // Burasý SignalR baðlantýsý için önemli!

// MVC routing yapýlandýrmasý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
