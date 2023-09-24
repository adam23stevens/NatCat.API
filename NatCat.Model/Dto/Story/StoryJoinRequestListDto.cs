using System;
namespace NatCat.Model.Dto.Story
{
	public class StoryJoinRequestListDto : BaseDto
	{
        public Guid StoryId { get; set; }
        public required string RequestingUserProfileName { get; set; }
        public DateTime DateRequested { get; set; }
        public string? StoryTitle { get; set; }
        public string? StorySynopsis { get; set; }
    }
}

