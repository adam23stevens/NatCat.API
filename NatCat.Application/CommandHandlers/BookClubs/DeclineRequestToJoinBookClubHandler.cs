using System;
using MediatR;
using NatCat.Application.Commands.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.CommandHandlers.BookClubs
{
    public class DeclineRequestToJoinBookClubHandler : IRequestHandler<DeclineRequestToJoinBookClub>
    {
        private IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> _joinRequestRepository;
        public DeclineRequestToJoinBookClubHandler(
            IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> joinRequestRepository
            )
        {
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<Unit> Handle(DeclineRequestToJoinBookClub request, CancellationToken cancellationToken)
        {
            await _joinRequestRepository.DeleteAsync(request.BookClubJoinRequestId);
            return Unit.Value;
        }
    }
}

