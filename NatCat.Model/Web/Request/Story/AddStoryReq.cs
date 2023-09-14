using NatCat.Model.Enum;

namespace NatCat.Model.Web.Story
{
    public class AddStoryReq
    {
        public string? Title { get; set; }
        public Guid GenreId { get; set; }
        public Guid StoryTypeId { get; set; }
        public StoryJoinType StoryJoinType { get; set; }
        public Guid? BookClubId { get; set; }
        public int MaxAuthors { get; set; }
        public int TotalStoryRounds { get; set; }
        public int VisibleWordCntPerPart { get; set; }
        public string? Synopsis { get; set; }
    }
}