#region

using System.Text.Json;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MovieTicketApi.Data;
using MovieTicketApi.Services;

#endregion

namespace MovieTicketApi;

public class Startup
{
    public Startup( IConfiguration configuration )
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices( IServiceCollection services )
    {
        services.AddDbContext<MovieTicketApiContext>( options =>
            options.UseNpgsql( this.Configuration.GetConnectionString( "CloudConn" ) ??
                              throw new InvalidOperationException( "Connection string not found." ) ) );

        services.AddHealthChecks().AddDbContextCheck<MovieTicketApiContext>();

        services.AddTransient<TokenService>();

        services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
            .AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            } );

        services.AddControllers()
        .AddJsonOptions( options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        } );

        services.AddSwaggerGen( c =>
        {
            c.SwaggerDoc( "v1",
                new OpenApiInfo
                { Title = "Movie Ticket", Description = "Projeto integrado do 3/4°semestre", Version = "v1" } );
        } );

        services.AddCors( options => options.AddPolicy( "AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
        .AllowAnyHeader() ) );

        services.AddDataProtection();
    }

    public void Configure( IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration )
    {

        if( env.IsDevelopment() )
        {
            app.UseSwagger();
            app.UseSwaggerUI( c => { c.SwaggerEndpoint( "/swagger/v1/swagger.json", "v1" ); } );
        }

        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseEndpoints( endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}" );
        } );
    }
}