using ConfDomain.Model;
using ConfInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbconappContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//builder.Services.AddDefaultIdentity<ConfInfrastructure.Models.User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();



builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConnection")
    ));

builder.Services.AddIdentity<ConfInfrastructure.Models.User, IdentityRole>(opts => {
    opts.Password.RequireNonAlphanumeric = false;
    // other password / lockout settings
}).AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddControllersWithViews(); 
builder.Services.AddRazorPages(); // Add Razor Pages support


/*builder.Services
  .AddControllersWithViews()
  .AddRazorRuntimeCompilation();
*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapStaticAssets();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
