using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Model.Dto.Story;

namespace NatCat.Application.Queries.Stories
{
    public class GetStory : IRequest<ReadStoryResponseDto>
    {
        public Guid Id {get;}
        public GetStory(Guid id) {
            Id = id;
        }
    }
}