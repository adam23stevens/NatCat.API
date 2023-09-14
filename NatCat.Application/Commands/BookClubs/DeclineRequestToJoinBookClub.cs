using System;
using MediatR;

namespace NatCat.Application.Commands.BookClubs
{
	public class DeclineRequestToJoinBookClub : IRequest
	{
		public DeclineRequestToJoinBookClub(Guid bookClubJoinRequestId)
		{
			BookClubJoinRequestId = bookClubJoinRequestId;
		}
		public Guid BookClubJoinRequestId { get; set; }
	}
}

