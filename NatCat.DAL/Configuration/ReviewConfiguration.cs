using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NatCat.DAL.Entity;

namespace NatCat.DAL.Configuration {
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasOne(x => x.Story)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.StoryId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne(x => x.ApplicationUser)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}