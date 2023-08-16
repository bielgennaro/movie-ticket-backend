#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace MovieTicketApi.Models.Mapping;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name);

        builder.Property(m => m.Director);

        builder.Property(m => m.Genre);

        builder.HasMany(m => m.SessionsList)
            .WithOne(s => s.SessionMovies)
            .HasForeignKey(s => s.MovieId);
    }
}