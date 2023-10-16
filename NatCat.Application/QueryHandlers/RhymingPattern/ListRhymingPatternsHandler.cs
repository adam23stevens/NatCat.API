using System;
using MediatR;
using NatCat.Application.Queries.RhymingPattern;
using NatCat.DAL.Contracts;
using NatCat.Model.Dto.RhymingPattern;

namespace NatCat.Application.QueryHandlers.RhymingPattern
{
	public class ListRhymingPatternsHandler : IRequestHandler<ListRhymingPatterns, IEnumerable<RhymingPatternDto>>
	{
        private readonly IRepository<DAL.Entity.RhymingPattern, RhymingPatternDto, RhymingPatternDto> _rhymingPatternRepository;

        public ListRhymingPatternsHandler(IRepository<DAL.Entity.RhymingPattern, RhymingPatternDto, RhymingPatternDto> rhymingPatternRepository)
        {
            _rhymingPatternRepository = rhymingPatternRepository;
        }
        
        public async Task<IEnumerable<RhymingPatternDto>> Handle(ListRhymingPatterns request, CancellationToken cancellationToken)
        {
            var items = await _rhymingPatternRepository.ListAllAsync();
            return items;
        }
    }
}

