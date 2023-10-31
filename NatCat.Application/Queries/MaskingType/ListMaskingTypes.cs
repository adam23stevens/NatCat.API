using System;
using MediatR;
using NatCat.Model.Dto.MaskingType;

namespace NatCat.Application.Queries.MaskingType
{
	public class ListMaskingTypes : IRequest<IEnumerable<MaskingTypeDto>>
    {
		public ListMaskingTypes()
		{
		}
	}
}

