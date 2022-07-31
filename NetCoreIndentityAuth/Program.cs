using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetCoreIndentityAuth.Entities;
using NetCoreIndentityAuth.Models;
using Microsoft.EntityFrameworkCore;
using NetCoreIndentityAuth.Services.IRepository;
using NetCoreIndentityAuth.Services.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthTest");

// Add services to the container.

var razor = builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddDbContext<AplicationDbContext>();


builder.Services.AddIdentity<User, UserRole>(opt =>
{

    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;


})
.AddEntityFrameworkStores<AplicationDbContext>();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.Cookie.Name = "user_cookie";
    opt.AccessDeniedPath = "/account/login";
    opt.ExpireTimeSpan = TimeSpan.FromHours(2);


});



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//register repository services
builder.Services.AddScoped<IAuthenticate, Authenticate>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    razor.AddRazorRuntimeCompilation();


}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
