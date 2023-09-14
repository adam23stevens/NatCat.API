using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.DAL.Web.Request.Base.NatCat.API.Model.Request.KeyWords;

namespace NatCat.Application.Commands.KeyWords
{
    public class AddKeyWord : IRequest
    {
        public AddKeyWord(AddKeyWordReq addKeyWordReq)
        {
            AddKeyWordReq = addKeyWordReq;
        }
        public AddKeyWordReq AddKeyWordReq { get; }
    }
}