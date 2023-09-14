using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Application.Queries.Genres;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.Genre;

namespace NatCat.Application.QueryHandlers.Genres
{
    public class GenresListHandler : IRequestHandler<GenresList, IEnumerable<GenreListDto>>
    {
        private readonly IRepository<Genre, GenreDetailDto, GenreListDto> _repository;

        public GenresListHandler(IRepository<Genre, GenreDetailDto, GenreListDto> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GenreListDto>> Handle(GenresList request, CancellationToken cancellationToken)
        {
            return await _repository.ListAllAsync(p => p.KeyWords);
        }
    }
}