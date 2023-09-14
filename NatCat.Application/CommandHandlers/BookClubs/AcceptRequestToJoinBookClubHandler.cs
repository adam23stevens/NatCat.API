using System;
using MediatR;
using NatCat.Application.Commands.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.CommandHandlers.BookClubs
{
	public class AcceptRequestToJoinBookClubHandler : IRequestHandler<AcceptRequestToJoinBookClub>
	{
		private readonly IRepository<BookClub, BookClubDetailDto, BookClubListDto> _bookClubRepository;
		private readonly IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> _joinRepository;
		public AcceptRequestToJoinBookClubHandler(
			IRepository<BookClub, BookClubDetailDto, BookClubListDto> bookClubRepository,
            IRepository<BookClubJoinRequest, BookClubJoinRequestDetailDto, BookClubJoinRequestListDto> joinRepository
			)
		{
			_bookClubRepository = bookClubRepository;
			_joinRepository = joinRepository;
		}

        public async Task<Unit> Handle(AcceptRequestToJoinBookClub request, CancellationToken cancellationToken)
        {
			var joinReq = await _joinRepository.GetEntityAsync(request.BookClubJoinRequestId);
			var bookClub = await _bookClubRepository.GetEntityAsync(joinReq.BookClubId);
			bookClub.ApplicationUsers.Add(joinReq.ApplicationUser);
			await _bookClubRepository.UpdateAsync(bookClub.Id, bookClub);
			await _joinRepository.DeleteAsync(joinReq.Id);

			return Unit.Value;
        }
    }
}

