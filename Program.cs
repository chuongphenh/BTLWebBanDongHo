using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebBanDongHo.Models;

var builder = WebApplication.CreateBuilder(args);
//var services = builder.Services;
//var Configuration = builder.Configuration;



// Add services to the container.
builder.Services.AddControllersWithViews();

//var connectionString = builder.Configuration.GetConnectionString("DaQldongHoContext");
//builder.Services.AddDbContext<DaQldongHoContext>(x => x.UseSqlServer(connectionString));


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//services.AddAuthentication()
//    .AddFacebook(facebookOptions => {
//        // Đọc cấu hình
//        IConfigurationSection facebookAuthNSection = Configuration.GetSection("Authentication:Facebook");
//        facebookOptions.AppId = facebookAuthNSection["AppId"];
//        facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
//        // Thiết lập đường dẫn Facebook chuyển hướng đến
//        facebookOptions.CallbackPath = "/Admin/Access/Login";
//    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=DashBoard}/{id?}");

app.MapControllerRoute(
	name: "Admin",
	pattern: "{area:exists}/{controller=DatHang}/{action=DatHang}/{id?}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Access}/{action=Login}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=AcountRegeterEvent}/{action=AcountRegeter}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=C_QuanLyDongHo}/{action=QuanLyDongHo}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Product}/{id?}");

app.Run();
