
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tebeshir.Art.Authentication.DataLayer;

using System.Security.Claims;
using Tebeshir.Art.Authentication.Infrastructure.DataLayer;
using Tebeshir.Art.Authentication.Infrastructure;
using Tebeshir.Art.Authentication.Domain.Models;

internal class Progam
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        var scope = app.Services.CreateScope();
        using (var context = scope.ServiceProvider.GetRequiredService<UserDatabaseContext>())
        {

            context.Database.Migrate();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            UserContextSeed.SeedRolesAsync(context, roleManager).GetAwaiter().GetResult();
            UserContextSeed.SeedUsersAsync(context, userManager).GetAwaiter().GetResult();

        }





        app.UseCors(options =>
   options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);

        app.Run();
    }
}