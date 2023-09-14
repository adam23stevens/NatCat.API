using System;
using MediatR;

namespace NatCat.Application.Commands.BookClubs
{
	public class AcceptRequestToJoinBookClub : IRequest
	{
		public AcceptRequestToJoinBookClub(Guid bookClubJoinRequestId)
		{
			BookClubJoinRequestId = bookClubJoinRequestId;
		}
		public Guid BookClubJoinRequestId { get; set; }
	}
}

