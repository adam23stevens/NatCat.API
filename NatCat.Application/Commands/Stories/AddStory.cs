using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Model.Web.Story;

namespace NatCat.Application.Commands.Stories
{
    public class AddStory : IRequest
    {
        public AddStoryReq AddStoryReq {get;}
        public string LoggedInUserId { get; }
        public AddStory(AddStoryReq addStoryReq, string loggedInUserId)
        {
            AddStoryReq = addStoryReq;
            LoggedInUserId = loggedInUserId;
        }
    }
}