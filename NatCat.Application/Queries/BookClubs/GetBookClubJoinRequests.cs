using System;
using MediatR;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.Queries.BookClubs
{
	public class GetBookClubJoinRequests : IRequest<PagedResult<BookClubJoinRequestListDto>>
	{
		public string UserId { get; set; }
		public GetBookClubJoinRequests(string userId)
		{
			UserId = userId;
		}
	}
}

