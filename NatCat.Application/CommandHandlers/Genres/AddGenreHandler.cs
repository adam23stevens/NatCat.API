using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Application.Commands.Genres;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Genre;

namespace NatCat.Application.CommandHandlers.Genres
{
    public class AddGenreHandler : IRequestHandler<AddGenre>
    {
        private IRepository<Genre, GenreDetailDto, GenreListDto> _repository;

        public AddGenreHandler(IRepository<Genre, GenreDetailDto, GenreListDto> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddGenre request, CancellationToken cancellationToken)
        {
            Genre genre = new()
            {
                Name = request.GenreName,
                IsActive = true
            };
            await _repository.AddAsync(genre);

            return Unit.Value;
        }
    }
}