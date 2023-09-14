using System;
using NatCat.Model.Dto.KeyWord;

namespace NatCat.Model.Dto.Genre
{
	public class GenreListDto : BaseDto
	{
		public GenreListDto()
		{
			KeyWordDetailDtos = new List<KeyWordDetailDto>();
		}
		public string? Name { get; set; }
		public IEnumerable<KeyWordDetailDto> KeyWordDetailDtos { get; set; }
	}
}

