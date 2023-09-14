using System;
using System.ComponentModel.DataAnnotations;

namespace NatCat.Model.Web.Request.BookClub
{
	public class SendRequestToJoinBookClubReq
	{
		[Required(ErrorMessage = "ApplicationUserId recipient is required")]
		public required string ApplicationUserId { get; set; }
		public Guid BookClubId { get; set; }
	}
}

