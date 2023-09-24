using System;
namespace NatCat.Model.Dto.BookClub
{
	public class BookClubJoinRequestListDto : BaseDto
    {
        public Guid BookClubId { get; set; }
        public required string RequestingUserProfileName { get; set; }
        public DateTime RequestDate { get; set; }
        public string? BookClubName { get; set; }
        public float AverageRating { get; set; }
    }
}

