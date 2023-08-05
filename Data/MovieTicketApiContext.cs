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

        public DbSet<MovieTicketApi.Models.User> User { get; set; }
        public DbSet<MovieTicketApi.Models.Session> Session { get; set; }
        public DbSet<MovieTicketApi.Models.Ticket> Ticket { get; set; }
        public DbSet<MovieTicketApi.Models.Movie> Movie { get; set; }
    }
}