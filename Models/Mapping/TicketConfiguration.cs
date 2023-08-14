using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models.Mapping;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.TicketId);

        builder.Property(t => t.Status);

        builder.Property(t => t.ChairCoord);

        builder.Property(t => t.Price);
        
    }
}