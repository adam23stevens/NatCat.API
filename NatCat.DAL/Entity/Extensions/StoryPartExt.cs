namespace NatCat.DAL.Entity.Extensions {
    public static class StoryPartExt{
        public static string LastWord(this StoryPart part) => part.Text?.Split(' ')?.Last() ?? string.Empty;
    }
}