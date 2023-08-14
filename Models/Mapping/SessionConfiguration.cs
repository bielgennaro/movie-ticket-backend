using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTicketApi.Models.Mapping
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.SessionId);

            builder.Property(s => s.MovieId);
            
            builder.HasOne(s => s.Movie)
                .WithMany()
                .HasForeignKey(s => s.MovieId);
        }
    }
}