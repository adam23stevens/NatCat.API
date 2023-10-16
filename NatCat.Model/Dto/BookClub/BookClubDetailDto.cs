using NatCat.Model.Auth;

namespace NatCat.Model.Dto.BookClub
{
    public class BookClubDetailDto : BaseDto
    {
        public required string Name { get; set; }
        public float AverageRating { get; set; }
        public virtual ICollection<UserDto>? UserDtos { get; set; }
    }
}