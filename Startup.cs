using System.Text.Json;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MovieTicketApi.Data;
using MovieTicketApi.Services;

namespace MovieTicketApi
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {

            services.AddDbContextPool<MovieTicketApiContext>( options =>
                options.UseNpgsql( this.Configuration.GetConnectionString( "LOCAL_CONNECTION_STRING" ) ) );

            services.AddScoped<TokenService>();
            services.AddScoped<PasswordHash>();



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
                    {
                        Title = "Movie Ticket",
                        Description = "Projeto integrado do 3/4° semestre",
                        Version = "v1"
                    } );
                c.ResolveConflictingActions( apiDescriptions => apiDescriptions.First() );
            } );

            services.AddDataProtection();
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            app.UseSwagger();
            app.UseSwaggerUI( c => { c.SwaggerEndpoint( "/swagger/v1/swagger.json", "v1" ); } );

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
}
