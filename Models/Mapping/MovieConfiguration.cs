using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models.Mapping;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.MovieId);

        builder.Property(m => m.Sessions);
        
        builder.Property(m => m.Name);
        
        builder.Property(m => m.Synopsis);
        
        builder.Property(m => m.Director);
        
        builder.Property(m => m.BannerUrl);
        
        builder.HasMany(m => m.Sessions)
            .WithOne(s => s.Movie)
            .HasForeignKey(s => s.MovieId);
    }
}