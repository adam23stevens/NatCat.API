using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NatCat.DAL.Configuration;
using NatCat.DAL.Entity;

namespace NatCat.DAL
{
    public class NatCatDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public NatCatDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
        { }
        public DbSet<StoryType> StoryTypes { get; set; }
        public DbSet<StoryPart> StoryParts { get; set; }
        public DbSet<StoryUser> StoryUsers { get; set; }
        public DbSet<KeyWord> KeyWords { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BookClub> BookClubs { get; set; }
        public DbSet<StoryPartKeyWord> StoryPartKeyWords { get; set; }
        public DbSet<BookClubJoinRequest> BookClubJoinRequests { get; set; }
        public DbSet<StoryJoinRequest> StoryJoinRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StoryConfiguration());
            modelBuilder.ApplyConfiguration(new KeyWordConfiguration());
            modelBuilder.ApplyConfiguration(new StoryPartConfiguration());
            modelBuilder.ApplyConfiguration(new BookClubConfiguration());
        }
    }
}
