using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NatCat.DAL.Entity
{
	public class BookClubJoinRequest : BaseGuidEntity
	{
		[ForeignKey(nameof(ApplicationUserId))]
		public string? ApplicationUserId { get; set; }
		public virtual ApplicationUser? ApplicationUser { get; set; }

		public string? RequestingUserProfileName { get; set; }

		[ForeignKey(nameof(BookClubId))]
		public Guid BookClubId { get; set; }
		public virtual BookClub? BookClub { get; set; }
	}
}

