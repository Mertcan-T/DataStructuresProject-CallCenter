using CallCenterSimulation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsýný yapýlandýr
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity yapýlandýrmasýný ekle
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// SignalR'ý yapýlandýr
builder.Services.AddSignalR();

// MVC'yi ekle
builder.Services.AddControllersWithViews();

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
app.MapHub<CallCenterHub>("/callCenterHub");

// MVC routing yapýlandýrmasý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
