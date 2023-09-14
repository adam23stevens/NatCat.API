namespace NatCat.Model.Request.Story
{
    public class AddStoryPartReq
    {
        public Guid StoryId { get; set; }
        public string? Text { get; set; }
    }
}