using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NatCat.Application.Commands.Stories
{
    public class PublishStory : IRequest
    {
        public Guid Id { get; }
        public PublishStory(Guid id)
        {
            Id = id;
        }
    }
}