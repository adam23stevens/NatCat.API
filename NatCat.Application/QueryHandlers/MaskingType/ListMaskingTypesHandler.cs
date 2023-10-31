using System;
using MediatR;
using NatCat.Application.Queries.MaskingType;
using NatCat.DAL.Contracts;
using NatCat.Model.Dto.MaskingType;

namespace NatCat.Application.QueryHandlers.MaskingType
{
	public class ListMaskingTypesHandler : IRequestHandler<ListMaskingTypes, IEnumerable<MaskingTypeDto>>
	{
        private IRepository<DAL.Entity.MaskingType, MaskingTypeDto, MaskingTypeDto> _maskingTypeRepository;

		public ListMaskingTypesHandler(IRepository<DAL.Entity.MaskingType, MaskingTypeDto, MaskingTypeDto> maskingTypeRepository)
		{
            _maskingTypeRepository = maskingTypeRepository;
		}

		public async Task<IEnumerable<MaskingTypeDto>> Handle(ListMaskingTypes request, CancellationToken cancellationToken) =>
			await _maskingTypeRepository.ListAllAsync();
    }
}

