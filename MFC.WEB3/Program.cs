using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MFC.DAL.Database;
using MFC.CORE.Interfaces.Services;
using MFC.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using MFC.CORE.Interfaces;
using MFC.DAL.Services;
using MFC.CORE.Interfaces.Repositories;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// MVC en API controller services toevoegen
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// CORS toevoegen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:3000")  // Adjust if your React app will run on a different port in production
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Authenticatie toevoegen
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect("Auth0", options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.CallbackPath = new PathString("/callback");
});

// DbContext toevoegen
builder.Services.AddDbContext<MFCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MFCDatabase")));

// Repository and service dependencies
builder.Services.AddScoped<IDailyAffirmationRepository, DailyAffirmationRepository>();
builder.Services.AddScoped<IAffirmationService, AffirmationService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

var app = builder.Build();

app.UseStaticFiles();  // Serve wwwroot files, important for any static assets

app.UseStaticFiles(new StaticFileOptions  // Serve React app build directory
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "client/build")),
    RequestPath = ""
});

app.UseCors("AllowSpecificOrigins");  // Apply CORS as configured

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); 
    endpoints.MapRazorPages();  

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    // Fallback to React app's entry point for SPA routing
    endpoints.MapFallbackToFile("index.html");
});

app.Run();

