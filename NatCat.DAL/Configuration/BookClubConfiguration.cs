using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NatCat.DAL.Entity;

namespace NatCat.DAL.Configuration {
    public class BookClubConfiguration : IEntityTypeConfiguration<BookClub>
    {
        public void Configure(EntityTypeBuilder<BookClub> builder)
        {
            builder
                .HasMany(x => x.ApplicationUsers)
                .WithMany(x => x.BookClubs);
        }
    }

}