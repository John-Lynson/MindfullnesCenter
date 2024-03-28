using Microsoft.EntityFrameworkCore;
using MFC.DAL; // Zorg ervoor dat je dit toevoegt om MFCContext te kunnen gebruiken

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Toevoegen van MFCContext met de connectiestring uit appsettings.json
builder.Services.AddDbContext<MFCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MFCDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
