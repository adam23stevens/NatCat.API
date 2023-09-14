namespace NatCat.Model.Dto.BookClub
{
    public class BookClubListDto : BaseDto
    {
        public string? Name { get; set; }
        public int ApplicationUsersCount { get; set; }
        public float AverageRating {get;set;}
    }
}