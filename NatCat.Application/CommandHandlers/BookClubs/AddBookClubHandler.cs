using System;
using AutoMapper;
using MediatR;
using NatCat.Application.Commands.BookClubs;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.BookClub;

namespace NatCat.Application.CommandHandlers.BookClubs
{
    public class AddBookClubHandler : IRequestHandler<AddBookClub>
    {
        public IRepository<BookClub, BookClubDetailDto, BookClubListDto> _bookClubRepository;
        public IMapper _mapper;
        public AddBookClubHandler(IRepository<BookClub, BookClubDetailDto, BookClubListDto> bookClubRepository, IMapper mapper)
        {
            _mapper = mapper;
            _bookClubRepository = bookClubRepository;
        }

        public async Task<Unit> Handle(AddBookClub request, CancellationToken cancellationToken)
        {
            var newBookClub = _mapper.Map<BookClub>(request);

            await _bookClubRepository.AddAsync(newBookClub);

            return Unit.Value;
        }
    }
}

