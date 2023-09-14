using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.Queries.Stories
{
    public class ListUserActiveStories : IRequest<PagedResult<StoryListDto>>
    {
        public string UserId { get; }
        public ListUserActiveStories(string userId)
        {
            UserId = userId;
        }
    }
}