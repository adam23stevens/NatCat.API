using System;
using MediatR;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.Queries.Stories
{
	public class ListRecentlyPublishedStories : IRequest<PagedResult<StoryListDto>>
    {
		public ListRecentlyPublishedStories()
		{
		}
	}
}

