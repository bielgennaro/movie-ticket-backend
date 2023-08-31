using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTicketApi.Models
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("id");

            builder.HasOne(t => t.Session)
                   .WithMany()
                   .HasForeignKey(t => t.SessionId)
                   .HasConstraintName("session_id");

            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .HasConstraintName("user_id");
        }
    }
}
