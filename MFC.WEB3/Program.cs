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

var builder = WebApplication.CreateBuilder(args);

// MVC en API controller services toevoegen
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// CORS toevoegen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:7082")
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

// Repository en services toevoegen
builder.Services.AddScoped<IDailyAffirmationRepository, DailyAffirmationRepository>();
builder.Services.AddScoped<IAffirmationService, AffirmationService>();

var app = builder.Build();

// Statische bestanden gebruiken
app.UseStaticFiles();

// CORS beleid gebruiken
app.UseCors("AllowSpecificOrigins");

// Middleware voor routing
app.UseRouting();

// Authenticatie en autorisatie middleware gebruiken
app.UseAuthentication();
app.UseAuthorization();

// Endpoints configureren
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
