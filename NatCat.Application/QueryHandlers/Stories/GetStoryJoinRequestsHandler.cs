using System;
using MediatR;
using NatCat.Application.Queries.Stories;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.QueryHandlers.Stories
{
    public class GetStoryJoinRequestsHandler : IRequestHandler<GetStoryJoinRequests, PagedResult<StoryJoinRequestListDto>>
	{
        private IRepository<StoryJoinRequest, StoryJoinRequestDetailDto, StoryJoinRequestListDto> _repository;

		public GetStoryJoinRequestsHandler(IRepository<StoryJoinRequest, StoryJoinRequestDetailDto, StoryJoinRequestListDto> repository)
		{
            _repository = repository;
		}
        
        public async Task<PagedResult<StoryJoinRequestListDto>> Handle(GetStoryJoinRequests request, CancellationToken cancellationToken)
        {
            QueryParameters<StoryJoinRequest> qry = new()
            {
                wc = s => s.ApplicationUserId == request.UserId
            };

            var items = await  _repository.PagedOrderAsync(
                qry,
                s => s.DateRequested, true);

            return items;
        }
    }
}

