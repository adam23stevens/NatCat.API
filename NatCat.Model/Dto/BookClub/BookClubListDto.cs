using NatCat.Model.Auth;

namespace NatCat.Model.Dto.BookClub
{
    public class BookClubListDto : BaseDto
    {
        public string? Name { get; set; }
        public int ApplicationUsersCount { get; set; }
        public float AverageRating {get;set;}
        public IEnumerable<UserDto> UserDtos { get; set; }
    }
}