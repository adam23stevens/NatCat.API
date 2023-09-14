using System;
namespace NatCat.Model.Dto.BookClub
{
	public class BookClubJoinRequestListDto : BaseDto
    { 
        public required string RequestingUserProfileName { get; set; }
    }
}

