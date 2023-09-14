namespace NatCat.Model.Dto.Story
{
    public class ReadStoryResponseDto
    {
        public string? Title { get; set; }
        public string? GenreName { get; set; }
        public IEnumerable<StoryPartDetailDto>? StoryPartDetailDtos { get; set; }
    }
}