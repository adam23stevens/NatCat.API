namespace Natcat.Web.Response.Story
{
    public class LatestStoryPartResponse
    {
        public string? StoryTitle { get; set; }
        public string? VisibleTextFromPrevious { get; set; }
        public string? InvisibleTextFromPrevious { get; set; }
        public int PercentageComplete { get; set; }
        public IEnumerable<string>? RequiredKeyWords { get; set; }
        public bool IsRhymingRequired { get; set; }
        public string? RhymingWords { get; set; }
        public string? WordToRhymeWith { get; set; }
        public bool IsFinalStoryPart { get; set; }
        public int MinCharLength { get; set; }
        public int MaxCharLength { get; set; }
        public DateTime? DeadlineTime { get; set; }
        public int Order { get; set; }
        public int MaxStoryParts { get; set; }
        public string? ApplicationUserId { get; set; }
    }
}