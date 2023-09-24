using System;
using MediatR;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.Queries.Stories
{
	public class GetStoryJoinRequests : IRequest<PagedResult<StoryJoinRequestListDto>>
	{
		public string UserId { get; set; }
		public GetStoryJoinRequests(string userId)
		{
			UserId = userId;
		}
	}
}

