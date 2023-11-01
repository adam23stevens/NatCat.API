using NatCat.Model.Enum;

namespace NatCat.Model.Web.Story
{
    public class AddStoryReq
    {
        public string? Title { get; set; }
        public Guid GenreId { get; set; }
        public Guid StoryTypeId { get; set; }
        public Guid BookClubId { get; set; }
        public Guid RhymingPatternId { get; set; }
        public int MaxUsers { get; set; }
        public int TotalStoryRounds { get; set; }
        public int MinCharLengthPerStoryPart { get; set; }
        public int MaxCharLengthPerStoryPart { get; set; }
        public int PreviousTextVisibilityPercentage { get; set; }
        public Guid MaskingTypeId { get; set; }
        public string? Synopsis { get; set; }
    }
}