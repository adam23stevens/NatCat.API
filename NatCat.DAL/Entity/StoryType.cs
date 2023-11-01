using System;
namespace NatCat.DAL.Entity
{
    public class StoryType : BaseGuidEntity
    {
        public string? Name { get; set; }
        public string? RuleDescription { get; set; }
        
        public string? RhymingPatternId {get;set;}
        public string? MaskingTypeId {get;set;} 
        public int DisplayOrder { get; set; }
        public int MinCharLengthPerStoryPart { get; set; }
        public int MaxCharLengthPerStoryPart { get; set; }
        public virtual ICollection<Story>? Stories { get; set; }
    }
}

