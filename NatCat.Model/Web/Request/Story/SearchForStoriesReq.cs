using System;
using NatCat.Model.Enum;

namespace NatCat.Model.Web.Request.Story
{
	public class SearchForStoriesReq
	{
        public string? TitleSearchText { get; set; }
        public IEnumerable<Guid>? GenreIds { get; set; }
        public int MinAuthorCount { get; set; }
        public int MaxAuthorCount { get; set; }
        public StoryPosition storyPosition { get; set; }
        public IEnumerable<Guid>? StoryTypeIds { get; set; }
    }
}

