using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using QuizWebsite.Web.Authentication;

//-- Configuration
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var builder = WebApplication.CreateBuilder(args);

//-- For accessing appsettings.json
builder.Services.AddSingleton(configuration); //TODO: See if this works

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorOptions(options =>
{
    options.ViewLocationFormats.Add("/Views/{0}" + RazorViewEngine.ViewExtension);
});

//-- Turn on Auth
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = configuration.GetSection("Customizable").GetSection("AuthenticationCookieName").Value;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(configuration.GetSection("Customizable").GetSection("CookieTimeOutDurationInMinutes").Value));
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/";
    });
builder.Services.AddScoped<IUserManager, UserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
