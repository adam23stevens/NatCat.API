using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NatCat.DAL.Entity
{
    public class StoryPart : BaseGuidEntity
    {
        public StoryPart()
        {
        }

        public string? Text { get; set; }
        public bool IsFinalStoryPart { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public DateTime? DeadlineTime { get; set; }
        public DateTime DateCreated { get; set; }
        public int Order { get; set; }
        public bool IsRhymingRequired { get; set; }
        public string? RhymingWords { get; set; }
        public string? WordToRhymeWith { get; set; }
        public char? RhymingSet { get; set; }
        public string? VisibleTextFromPrevious { get; set; }
        public string? InvisibleTextFromPrevious { get; set; }

        [ForeignKey(nameof(StoryId))]
        public Guid StoryId { get; set; }
        public virtual Story? Story { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<StoryPartKeyWord>? StoryPartKeyWords { get; set; }
    }
}

