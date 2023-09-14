using System;
using System.ComponentModel.DataAnnotations;

namespace NatCat.Model.Web.Request.BookClub
{
	public class AddBookClubReq
	{
		[Required(ErrorMessage = "Title is required in request")]
		public required string Title { get; set; }
	}
}

