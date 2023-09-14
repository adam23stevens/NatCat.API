using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Application.Queries.KeyWords;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.KeyWord;

namespace NatCat.Application.QueryHandlers.KeyWords
{
    public class ListKeyWordsHandler : IRequestHandler<ListKeyWords, IEnumerable<KeyWordDetailDto>>
    {
        private IRepository<KeyWord, KeyWordDetailDto, KeyWordDetailDto> _keyWordRepository;

        public ListKeyWordsHandler(IRepository<KeyWord, KeyWordDetailDto, KeyWordDetailDto> keyWordRepository)
        {
            _keyWordRepository = keyWordRepository;
        }

        public async Task<IEnumerable<KeyWordDetailDto>> Handle(ListKeyWords request, CancellationToken cancellationToken)
        {
            var genreKeywords = await _keyWordRepository.ListAllAsync
            (k => k.GenreId == request.ParentId);

            return genreKeywords;
        }
    }
}