using System;
using MediatR;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;
using NatCat.Model.Enum;
using NatCat.Model.Web.Request.Story;

namespace NatCat.Application.Queries.Stories
{
	public class SearchForStoriesQry : BasePagedQry, IRequest<PagedResult<StoryListDto>>
    {
		public SearchForStoriesReq _request;
		public SearchForStoriesQry(SearchForStoriesReq request)
		{
			_request = request;
		}
		
	}
}

