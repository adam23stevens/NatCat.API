namespace NatCat.Model.Dto.Story
{
    public class StoryTypeListDto : BaseDto
    {
        public string? Name { get; set; }
        public string? RuleDescription { get; set; }
        public bool IsRhymingRequired { get; set; }
        public string? RhymingPattern { get; set; }
        public int DisplayOrder { get; set; }
        public int MinCharLengthPerStoryPart { get; set; }
        public int MaxCharLengthPerStoryPart { get; set; }
    }
}