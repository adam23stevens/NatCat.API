using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NatCat.DAL.Entity;

namespace NatCat.DAL.Configuration {
    public class StoryPartConfiguration : IEntityTypeConfiguration<StoryPart>
    {
        public void Configure(EntityTypeBuilder<StoryPart> builder)
        {
            builder
                .HasOne(x => x.Story)
                .WithMany(x => x.StoryParts)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.ApplicationUser)
                .WithMany(x => x.StoryParts)
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}