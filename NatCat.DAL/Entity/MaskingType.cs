using System;
namespace NatCat.DAL.Entity
{
	public class MaskingType : BaseGuidEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<Story>? Stories { get; set; }
	}
}