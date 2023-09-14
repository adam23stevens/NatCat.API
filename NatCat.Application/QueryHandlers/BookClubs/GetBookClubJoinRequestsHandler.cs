using System;
using MediatR;
using NatCat.Application.Queries.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.QueryHandlers.BookClubs
{
	public class GetBookClubJoinRequestsHandler : IRequestHandler<GetBookClubJoinRequests, IEnumerable<BookClubJoinRequestListDto>>
	{
        private IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> _bookClubRepository;

        public GetBookClubJoinRequestsHandler(IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> bookClubRepository)
        {
            _bookClubRepository = bookClubRepository;
        }

        public async Task<IEnumerable<BookClubJoinRequestListDto>> Handle(GetBookClubJoinRequests request, CancellationToken cancellationToken)
        {
            var requests = await _bookClubRepository.ListAllAsync(q => q.ApplicationUserId == request.UserId);

            return requests;
        }
    }
}

