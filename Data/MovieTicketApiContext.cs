#region

using Microsoft.EntityFrameworkCore;

using MovieTicketApi.Mapping;
using MovieTicketApi.Resources.Movies.Models;
using MovieTicketApi.Resources.Sessions.Models;
using MovieTicketApi.Resources.Tickets.Models;
using MovieTicketApi.Resources.Users.Models;

#endregion

namespace MovieTicketApi.Data;

public class MovieTicketApiContext : DbContext
{
    public MovieTicketApiContext( DbContextOptions<MovieTicketApiContext> options )
        : base( options )
    {
        this.Database.Migrate();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Movie> Movies { get; set; }


    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.HasDefaultSchema( "develop" );

        modelBuilder.ApplyConfiguration( new MovieConfiguration() );
        modelBuilder.ApplyConfiguration( new SessionConfiguration() );
        modelBuilder.ApplyConfiguration( new TicketConfiguration() );
        modelBuilder.ApplyConfiguration( new UserConfiguration() );
    }
}