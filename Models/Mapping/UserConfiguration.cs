#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace MovieTicketApi.Models.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email);

        builder.Property(u => u.Password);

        builder.Property(u => u.IsAdmin);

        builder.HasMany(u => u.TicketsList)
                   .WithOne(t => t.User)
                   .HasForeignKey(t => t.UserId);
    }
}