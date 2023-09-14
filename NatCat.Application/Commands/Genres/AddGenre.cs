using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NatCat.Application.Commands.Genres
{
    public class AddGenre : IRequest
    {
        public AddGenre(string genreName)
        {
            GenreName = genreName;
        }
        public string GenreName { get; }
    }
}