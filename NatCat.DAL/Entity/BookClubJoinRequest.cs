using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NatCat.DAL.Entity
{
	public class BookClubJoinRequest : BaseGuidEntity
	{
		[ForeignKey(nameof(ApplicationUserId))]
		public required string ApplicationUserId { get; set; }
		public virtual required ApplicationUser ApplicationUser { get; set; }

		public required string RequestingUserProfileName { get; set; }

		[ForeignKey(nameof(BookClubId))]
		public Guid BookClubId { get; set; }
		public virtual required BookClub BookClub { get; set; }
	}
}

