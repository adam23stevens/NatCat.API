namespace NatCat.Model.Dto.Story
{
    public class StoryDetailDto : BaseDto
    {
        public required string Title { get; set; }
        public int CurrentStoryPartIndex { get; set; }
        public int StoryUsersCount { get; set; }
        public string? PreviousTextDisplay { get; set; }
        public int MaxUsers { get; set; }
    }
}