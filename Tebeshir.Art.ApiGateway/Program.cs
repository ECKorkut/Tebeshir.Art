using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using Tebeshir.Art.ApiGateway.Services.Interface;
using Tebeshir.Art.ApiGateway.Services;
using Tebeshir.Art.Authentication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("ocelot.json");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOcelot().Wait();

app.UseAuthentication();

app.MapControllers();

app.UseCors(options =>
 options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
) ; 
app.Run();
