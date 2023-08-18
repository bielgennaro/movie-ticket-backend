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

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Genre)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.Synopsis)
            .HasMaxLength(255);

        builder.Property(m => m.Director)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.BannerUrl)
            .HasMaxLength(200);

        builder.HasMany(m => m.SessionsList)  // Relação com a entidade Session
            .WithOne(s => s.Movie)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}