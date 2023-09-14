using System;
namespace NatCat.DAL.Entity
{
    public class StoryType : BaseGuidEntity
    {
        public string? TypeName { get; set; }
        public string? RuleDescription { get; set; }
        public bool IsRhymingRequired { get; set; }
        public string? RhymingPattern { get; set; }
        public int DisplayOrder { get; set; }
        public int MinWordsPerStoryPart { get; set; }
        public int MaxWordsPerStoryPart { get; set; }
        public int MinCharLengthPerStoryPart { get; set; }
        public int MaxCharLengthPerStoryPart { get; set; }
        public virtual ICollection<Story>? Stories { get; set; }
    }
}

