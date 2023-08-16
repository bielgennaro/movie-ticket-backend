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
            builder.HasKey(s => s.SessionId);

            builder.Property(s => s.DateTime);

            builder.Property(s => s.Room);

            builder.Property(s => s.MovieId);

            builder.HasOne(s => s.SessionMovies)
                .WithMany(m => m.SessionsList)
                .HasForeignKey(s => s.SessionId);
        }
    }
}