using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NatCat.DAL.Entity;

namespace NatCat.DAL.Configuration {
    public class AwardedBadgeConfiguration : IEntityTypeConfiguration<AwardedBadge>
    {
        public void Configure(EntityTypeBuilder<AwardedBadge> builder)
        {
            builder
                .HasOne(x => x.ApplicationUser)
                .WithMany(x => x.AwardedBadges)
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(x => x.Badge)
                .WithMany(x => x.AwardedBadges)
                .HasForeignKey(x => x.BadgeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}