using MediatR;
using NatCat.Model.Dto.RhymingPattern;

namespace NatCat.Application.Queries.RhymingPattern
{
	public class ListRhymingPatterns : IRequest<IEnumerable<RhymingPatternDto>>
	{
		public ListRhymingPatterns()
		{
		}
	}
}

