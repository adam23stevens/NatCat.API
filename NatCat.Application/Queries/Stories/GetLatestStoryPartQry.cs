using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Natcat.Web.Response.Story;
using NatCat.Model.Web.Story;

namespace NatCat.Application.Queries.Stories
{
    public class GetLatestStoryPartQry : IRequest<LatestStoryPartResponse>
    {
        public LatestStoryPartReq LatestStoryPartReq;
        public GetLatestStoryPartQry(LatestStoryPartReq latestStoryPartReq)
        {
            LatestStoryPartReq = latestStoryPartReq;
        }
    }
}