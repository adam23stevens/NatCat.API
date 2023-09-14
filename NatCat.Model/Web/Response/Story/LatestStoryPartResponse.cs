namespace Natcat.Web.Response.Story
{
    public class LatestStoryPartResponse
    {
        public string? VisibleTextFromPrevious { get; set; }
        public string? InvisibleTextFromPrevious { get; set; } //displays x's instead of words etc
        public IEnumerable<string>? RequiredKeyWords { get; set; }
        public bool IsRhymingRequired { get; set; }
        public string? RhymingWord { get; set; }
        public string? WordToRhymeWith { get; set; }
        public bool IsFinalStoryPart { get; set; }
        public int MinWords { get; set; }
        public int MaxWords { get; set; }
        public int MinCharLength { get; set; }
        public int MaxCharLength { get; set; }
        public DateTime? DeadlineTime { get; set; }
        public int Order { get; set; }
        public int MaxStoryParts { get; set; }
        public string? ApplicationUserId { get; set; }
    }
}