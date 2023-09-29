using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NatCat.DAL.Entity
{
    public class Story : BaseGuidEntity
    {
        public Story()
        {
            StoryUsers = new List<StoryUser>();
            StoryParts = new List<StoryPart>();
            Reviews = new List<Review>();
        }

        public string? Title { get; set; }
        public bool IsBeingWritten { get; set; }
        public string? Synopsis { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? DatePublished { get; set; }
        public int MaxUsers { get; set; }
        public int CurrentStoryRound { get; set; }
        public int TotalStoryRounds { get; set; }
        public bool IsVisibleOnLibrary { get; set; }
        public bool AllowPublicToJoinStory { get; set; }
        public int MinCharLengthPerStoryPart { get; set; }
        public int MaxCharLengthPerStoryPart { get; set; }

        public int MaxStoryParts => StoryUsers.Count() * TotalStoryRounds;

        [ForeignKey(nameof(GenreId))]
        public Guid GenreId { get; set; }
        public virtual Genre? Genre { get; set; }

        [ForeignKey(nameof(StoryTypeId))]
        public Guid StoryTypeId { get; set; }
        public virtual StoryType? StoryType { get; set; }

        [ForeignKey(nameof(BookClubId))]
        public Guid BookClubId { get; set; }
        public virtual BookClub? BookClub { get; set; }

        [ForeignKey(nameof(AuthorApplicationUserId))]
        public string? AuthorApplicationUserId { get; set; }
        public virtual ApplicationUser? AuthorApplicationUser { get; set; }
        public virtual ICollection<StoryUser> StoryUsers { get; set; }
        public virtual ICollection<StoryPart> StoryParts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

