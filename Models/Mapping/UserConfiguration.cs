using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.Email);

        builder.Property(u => u.Password);

        builder.Property(u => u.IsAdmin);
    }
}