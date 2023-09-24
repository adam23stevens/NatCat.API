using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NatCat.DAL.Entity;

namespace NatCat.DAL.Configuration
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder
                .HasOne(x => x.Genre)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.BookClub)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => x.BookClubId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.StoryType)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => x.StoryTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(x => x.AuthorApplicationUser)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => x.AuthorApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}