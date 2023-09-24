using System;
using MediatR;
using NatCat.Application.Queries.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.QueryHandlers.BookClubs
{
	public class GetBookClubJoinRequestsHandler : IRequestHandler<GetBookClubJoinRequests, PagedResult<BookClubJoinRequestListDto>>
	{
        private IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> _bookClubRepository;

        public GetBookClubJoinRequestsHandler(IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> bookClubRepository)
        {
            _bookClubRepository = bookClubRepository;
        }

        public async Task<PagedResult<BookClubJoinRequestListDto>> Handle(GetBookClubJoinRequests request, CancellationToken cancellationToken)
        {
            //var requests = await _bookClubRepository
            //    .ListAllAsync(q => q.ApplicationUserId == request.UserId);
            QueryParameters<BookClubJoinRequest> qry = new()
            {
                wc = b => b.ApplicationUserId == request.UserId
            };

            var requests = await _bookClubRepository
                .PagedOrderAsync(qry, b => b.DateRequested, true, b => b.ApplicationUser);

            return requests;
        }
    }
}

