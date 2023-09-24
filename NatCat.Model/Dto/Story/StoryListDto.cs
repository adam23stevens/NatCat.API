namespace NatCat.Model.Dto.Story
{
    public class StoryListDto : BaseDto
    {
        public string? Title { get; set; }
        public int CurrentUserCount { get; set; }
        //public StoryPartDetailDto CurrentStoryPart { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? AssignedUserId { get; set;}
        public bool IsMyTurn { get; set; }
        public string? GenreName { get; set; }
        public string? StoryTypeName { get; set; }
        public string? BookClubName { get; set; }
        public string? Synopsis { get; set; } 
    }
}