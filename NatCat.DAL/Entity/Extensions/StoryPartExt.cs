namespace NatCat.DAL.Entity.Extensions {
    public static class StoryPartExt{
        public static string LastWord(this string text) => text?.Split(' ')?.Last() ?? string.Empty;
    }
}