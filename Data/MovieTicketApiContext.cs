using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketApi.Models;

namespace MovieTicketApi.Data
{
    public class MovieTicketApiContext : DbContext
    {
        public MovieTicketApiContext(DbContextOptions<MovieTicketApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieSession> MovieSessions { get; set; }
        
        void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieSession>()
                .HasKey(ms => new { ms.MovieId, ms.SessionId });

            modelBuilder.Entity<MovieSession>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieSessions)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<MovieSession>()
                .HasOne(ms => ms.Session)
                .WithMany(s => s.MovieSessions)
                .HasForeignKey(ms => ms.SessionId);
        }
    }
}
