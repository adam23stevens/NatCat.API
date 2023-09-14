using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NatCat.Model.Dto.Genre;

namespace NatCat.Application.Queries.Genres
{
    public class GenresList : IRequest<IEnumerable<GenreListDto>>
    {
        
    }
}