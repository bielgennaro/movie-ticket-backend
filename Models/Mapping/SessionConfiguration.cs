#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace MovieTicketApi.Models.Mapping
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.DateTime)
                .IsRequired();

            builder.Property(s => s.Room)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(s => s.Movie)  // Relação com a entidade Movie
                .WithMany(m => m.SessionsList)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}