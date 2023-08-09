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
        }
}
