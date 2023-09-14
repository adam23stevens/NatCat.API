using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Model.DataGroup;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.Queries.Stories
{
    public class ListAllPublishedStories : IRequest<PagedResult<StoryListDto>>
    {
        public string QueryText { get; }
        public ListAllPublishedStories(string queryText)
        {
            QueryText = queryText;
        }
    }
}