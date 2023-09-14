using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Model.Request.Story;

namespace NatCat.Application.Commands.Stories
{
    public class AddStoryPart : IRequest
    {
        public AddStoryPartReq AddStoryPartReq {get;}
        public string ApplicationUserId {get;}
        public AddStoryPart(AddStoryPartReq addStoryPartReq, string applicationUserId)
        {
            AddStoryPartReq = addStoryPartReq;
            ApplicationUserId = applicationUserId;
        }
    }
}