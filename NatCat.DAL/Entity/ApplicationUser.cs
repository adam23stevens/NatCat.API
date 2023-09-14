using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NatCat.DAL.Entity;


public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        ProfileName = "";
        StoryParts = new List<StoryPart>();
        BookClubs = new List<BookClub>();
        Stories = new List<Story>();
        StoryUsers = new List<StoryUser>();
        Reviews = new List<Review>();
        AwardedBadges = new List<AwardedBadge>();
        BookClubJoinRequests = new List<BookClubJoinRequest>();
    }
    public required string ProfileName { get; set; }
    public virtual ICollection<StoryPart> StoryParts { get; set; }
    public virtual ICollection<BookClub> BookClubs { get; set; }
    public virtual ICollection<Story> Stories { get; set; }
    public virtual ICollection<StoryUser> StoryUsers { get; set; }
    public virtual ICollection<Review> Reviews {get;set;}
    public virtual ICollection<AwardedBadge> AwardedBadges {get;set;}
    public virtual ICollection<BookClubJoinRequest> BookClubJoinRequests { get; set; }
}