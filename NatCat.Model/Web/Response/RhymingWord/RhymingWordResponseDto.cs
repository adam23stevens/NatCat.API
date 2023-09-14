namespace NatCat.Model.Web.Response.RhymingWord
{
    public class RhymingWordResponseDto
    {
        public string? Word { get; set; }
        public int Score { get; set; }
        public int NumSyllables { get; set; }
    }
}