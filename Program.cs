using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieTicketApi.Controllers;
using MovieTicketApi.Data;
using MovieTicketApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieTicketApiContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieTicketApiContext")));

var app = builder.Build();

var context = app.Services.GetRequiredService<MovieTicketApiContext>();

context.Database.EnsureCreated();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

app.Run();