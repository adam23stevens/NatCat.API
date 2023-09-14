using System.ComponentModel.DataAnnotations.Schema;

namespace NatCat.DAL.Entity
{
    public class Review : BaseGuidEntity
    {
        public string? ReviewText { get; set; }
        public int RatingStars { get; set; }
        public DateTime? DatePublished { get; set; }
        [ForeignKey(nameof(StoryId))]
        public Guid StoryId { get; set; }
        public virtual Story? Story { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }


    }
}