#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace MovieTicketApi.Models.Mapping;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.TicketId);

        builder.Property(t => t.Status);

        builder.Property(t => t.ChairCoord);

        builder.Property(t => t.Price);

    }
}