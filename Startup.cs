#region

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieTicketApi.Data;
using Newtonsoft.Json;

#endregion

namespace MovieTicketApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MovieTicketApiContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("MovieTicketApiContext") ??
                              throw new InvalidOperationException("Connection string 'ConnectionDb' not found.")));

        services.AddControllersWithViews();

        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        );

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "Movie Ticket", Description = "Cineminha dos parsero", Version = "v1" });
        });

        services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });

        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}