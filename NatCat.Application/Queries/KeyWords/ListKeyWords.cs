using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Model.Dto.KeyWord;

namespace NatCat.Application.Queries.KeyWords
{
    public class ListKeyWords : IRequest<IEnumerable<KeyWordDetailDto>>
    {
        public ListKeyWords(Guid parentId)
        {
            ParentId = parentId;
        }
        public Guid ParentId { get; }
    }
}