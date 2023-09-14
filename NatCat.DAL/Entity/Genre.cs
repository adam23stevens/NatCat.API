using System;
namespace NatCat.DAL.Entity
{
	public class Genre : BaseGuidEntity
	{
		public string? Name { get; set; }
		public bool IsActive { get; set; }

		public virtual ICollection<KeyWord>? KeyWords { get; set; }
		public virtual ICollection<Story>? Stories { get; set; }
	}
}

