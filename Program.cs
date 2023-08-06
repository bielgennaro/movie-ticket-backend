using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieTicketApi.Controllers;
using MovieTicketApi.Data;
using MovieTicketApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

new ServiceCollection()
    .AddLogging()
    .AddMvc()
    .AddNewtonsoftJson()
    .Services.BuildServiceProvider();

builder.Services.AddControllers();

builder.Services.AddDbContext<MovieTicketApiContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieTicketApiContext")));

var app = builder.Build(); ;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();