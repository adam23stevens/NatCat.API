using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NatCat.Application.Commands.KeyWords;
using NatCat.DAL.Contracts;
using NatCat.DAL.Entity;
using NatCat.Model.Dto.KeyWord;

namespace NatCat.Application.CommandHandlers.KeyWords
{
    public class AddKeyWordHandler : IRequestHandler<AddKeyWord>
    {
        private IMapper _mapper;
        private IRepository<KeyWord, KeyWordDetailDto, KeyWordDetailDto> _keyWordRepository;
        public AddKeyWordHandler(IMapper mapper, IRepository<KeyWord, KeyWordDetailDto, KeyWordDetailDto> keyWordRepository)
        {
            _keyWordRepository = keyWordRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AddKeyWord request, CancellationToken cancellationToken)
        {
            var newKeyWord = _mapper.Map<KeyWord>(request.AddKeyWordReq);
            var dupWords = await _keyWordRepository.ListAllAsync(x => x.Word == newKeyWord.Word);
            if (dupWords.Any(x => x.GenreId == request.AddKeyWordReq.ParentId))
            {
                throw new InvalidDataException($"Keyword {newKeyWord.Word} already exists in GenreId {newKeyWord.GenreId}");
            }
            await _keyWordRepository.AddAsync(newKeyWord);
            
            return Unit.Value;
        }
    }
}