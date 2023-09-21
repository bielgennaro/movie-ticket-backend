using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MovieTicketApi.Models.Entity;

namespace MovieTicketApi.Models.Mapping
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure( EntityTypeBuilder<Session> builder )
        {
            builder.ToTable( "Sessions" );

            builder.HasKey( s => s.Id );

            builder.Property( s => s.Id ).HasColumnName( "id" );

            builder.Property( s => s.Room ).HasColumnName( "room" ).HasMaxLength( 50 );

            builder.Property( s => s.DateTime ).HasColumnName( "date_time" );

            builder.HasOne( s => s.Movie )
                    .WithMany()
                   .HasConstraintName( "movie_id" )
                   .HasForeignKey( s => s.MovieId );
        }
    }
}
