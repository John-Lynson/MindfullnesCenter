using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MFC.CORE;
using MFC.CORE.Interfaces;
using MFC.DAL;
using MFC.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using MFC.DAL.Database; // Zorg ervoor dat dit pad klopt na je namespace aanpassingen

var builder = WebApplication.CreateBuilder(args);

// Voeg MVC services toe aan de DI container
builder.Services.AddControllersWithViews(); // Dit vervangt AddControllers in een typische MVC setup

// Voeg de authenticatieservices toe
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect("Auth0", options =>
{
    // Configureer Auth0-instellingen
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.ResponseType = OpenIdConnectResponseType.Code;

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");

    options.CallbackPath = new PathString("/callback");
    options.ClaimsIssuer = "Auth0";

    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProviderForSignOut = (context) =>
        {
            var logoutUri = $"https://{builder.Configuration["Auth0:Domain"]}/v2/logout?client_id={builder.Configuration["Auth0:ClientId"]}";

            var postLogoutUri = context.Properties.RedirectUri;
            if (!string.IsNullOrEmpty(postLogoutUri))
            {
                if (postLogoutUri.StartsWith("/"))
                {
                    var request = context.Request;
                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                }
                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
            }

            context.Response.Redirect(logoutUri);
            context.HandleResponse();

            return Task.CompletedTask;
        }
    };
});

// Voeg DbContext toe
builder.Services.AddDbContext<MFCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MFCDatabase")));

// Voeg de repository toe aan de DI container
builder.Services.AddScoped<IDailyAffirmationRepository, DailyAffirmationRepository>();

var app = builder.Build();

// Gebruik de authenticatiemiddleware
app.UseAuthentication();

// Configureer de HTTP request pipeline specifiek voor MVC
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Zorg voor een Error actie in je HomeController
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Voor het serveren van statische bestanden zoals CSS, JavaScript, en afbeeldingen

app.UseRouting();

app.UseAuthorization();

// Configureer routes voor MVC
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
