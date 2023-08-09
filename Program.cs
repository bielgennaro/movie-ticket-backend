using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieTicketApi.Controllers;
using MovieTicketApi.Data;
using MovieTicketApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieTicketApiContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieTicketApiContext")));

builder.Services.AddControllers()
    .AddNewtonsoftJson();

new ServiceCollection()
    .AddLogging()
    .AddMvc()
    .AddNewtonsoftJson()
    .Services.BuildServiceProvider();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

/*
 *Teste CORS*
 * 
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("CorsPolyci",
            policy =>
            {
                policy.WithOrigins("http://localhost:7266")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

app.UseCors();
*/

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

app.Logger.LogInformation("Starting application");
