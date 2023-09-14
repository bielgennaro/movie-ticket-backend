using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MovieTicketApi.Models.Entity;

namespace MovieTicketApi.Models.Mapping
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable( "Users" );

            builder.HasKey( u => u.Id );

            builder.Property( u => u.Email )
                   .HasColumnName( "email" )
                   .HasMaxLength( 100 );

            builder.Property( u => u.Password )
                   .HasColumnName( "password" )
                   .HasMaxLength( 100 );

            builder.Property( u => u.IsAdmin )
                   .HasColumnName( "is_admin" );

            builder.Property(u => u.PasswordHash )
                   .HasColumnName( "password_hash" )
                   .HasMaxLength( 100 );
        }
    }
}
