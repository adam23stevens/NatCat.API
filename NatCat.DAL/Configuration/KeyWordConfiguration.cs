using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NatCat.DAL.Entity;

namespace NatCat.DAL.Configuration
{
    public class KeyWordConfiguration : IEntityTypeConfiguration<KeyWord>
    {
        public void Configure(EntityTypeBuilder<KeyWord> builder)
        {
            builder
                .HasOne(x => x.Genre)
                .WithMany(x => x.KeyWords)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}