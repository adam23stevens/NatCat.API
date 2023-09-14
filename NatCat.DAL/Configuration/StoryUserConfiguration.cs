using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NatCat.DAL.Entity;

namespace NatCat.DAL.Configuration
{
    public class StoryUserConfiguration : IEntityTypeConfiguration<StoryUser>
    {
        public void Configure(EntityTypeBuilder<StoryUser> builder)
        {
            builder
                .HasOne(x => x.Story)
                .WithMany(x => x.StoryUsers)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.ApplicationUser)
                .WithMany(x => x.StoryUsers)
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}