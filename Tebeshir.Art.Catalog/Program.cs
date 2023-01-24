using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Tebeshir.Art.Authentication.Infrastructure;
using Tebeshir.Art.Catalog.DataLayer;
using Tebeshir.Art.Catalog.DataLayer.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(CatalogDbContext));

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


var scope = app.Services.CreateScope();
using (var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>())
{
    context.Database.Migrate();
    if (!context.CatalogModels.Any()) {
        var catalog = new List<CatalogModel>() { 
            new CatalogModel() { Description="Lorem Ipsum", Name="Da Vinci" },
            new CatalogModel() { Description="Lorem Ipsum", Name="Picasso" },
            new CatalogModel() { Description="Lorem Ipsum", Name="Titian" }
        };
        context.CatalogModels.AddRange(catalog);
        await context.SaveChangesAsync();
    }
    
}


app.Run();
