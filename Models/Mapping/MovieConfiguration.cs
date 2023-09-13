using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTicketApi.Models.Mapping
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure( EntityTypeBuilder<Movie> builder )
        {
            builder.ToTable( "Movies" );

            builder.HasKey( m => m.Id );

            builder.Property( m => m.Gender ).HasColumnName( "gender" ).HasMaxLength( 255 );

            builder.Property( m => m.Synopsis ).HasColumnName( "synopsis" );

            builder.Property( m => m.Director ).HasColumnName( "director" ).HasMaxLength( 255 );

            builder.Property( m => m.BannerUrl )
                   .HasColumnName( "banner_url" )
                   .HasMaxLength( 200 );
        }
    }
}
