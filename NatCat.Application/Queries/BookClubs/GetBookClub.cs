using System;
using MediatR;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.Queries.BookClubs
{
	public class GetBookClub: IRequest<BookClubDetailDto>
	{
		public Guid BookClubId { get; set; }
		public GetBookClub(Guid bookClubId)
		{
			BookClubId = bookClubId;
		}
	}
}

