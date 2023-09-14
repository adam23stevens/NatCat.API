namespace NatCat.Model.Dto.BookClub
{
    public class BookClubDetailDto : BaseDto
    {
        public required string Name { get; set; }
        public float AverageRating { get; set; }
        public virtual ICollection<string>? ApplicationUserIds { get; set; }
        public virtual ICollection<string>? ApplicationUserProfileNames { get; set; }
    }
}