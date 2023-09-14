using System;
using MediatR;
using NatCat.Application.Commands.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.CommandHandlers.BookClubs
{
	public class SendRequestToJoinBookClubHandler : IRequestHandler<SendRequestToJoinBookClub>
	{
        private readonly IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> _joinBookClubRepository;
		public SendRequestToJoinBookClubHandler(
            IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> joinBookClubRepository)
		{
            _joinBookClubRepository = joinBookClubRepository;
		}

        public async Task<Unit> Handle(SendRequestToJoinBookClub request, CancellationToken cancellationToken)
        {
            await _joinBookClubRepository.AddAsync(new()
            {
                ApplicationUserId = request.Request.ApplicationUserId,
                BookClubId = request.Request.BookClubId,
                RequestingUserProfileName = request.RequestingApplicationUserProfileName
            });

            return Unit.Value;
        }
    }
}

