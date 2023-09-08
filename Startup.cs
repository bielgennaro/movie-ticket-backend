#region

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieTicketApi.Data;
using System.Text.Json;

#endregion

namespace MovieTicketApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
        }


        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MovieTicketApiContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("MovieTicketApiCloud") ??
                              throw new InvalidOperationException("Connection string not found.")));

        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddScoped<HashingService>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                { Title = "Movie Ticket", Description = "Projeto integrado do 3/4°semestre", Version = "v1" });
        });

        services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));
    }
}