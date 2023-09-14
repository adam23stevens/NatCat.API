using System;
namespace NatCat.Model.Dto.KeyWord
{
	public class KeyWordDetailDto : BaseDto
	{
		public string? Word { get; set; }
		public string? Difficulty { get; set; }
		public Guid GenreId { get; set; }
	}
}

