using System;
using NatCat.Model.Dto.KeyWord;

namespace NatCat.Model.Dto.Genre
{
	public class GenreDetailDto : BaseDto
	{
		public GenreDetailDto()
		{
			KeyWordDetailDtos = new List<KeyWordDetailDto>();
		}
		public string? Name { get; set; }
		public IEnumerable<KeyWordDetailDto> KeyWordDetailDtos { get; set; }
	}
}

