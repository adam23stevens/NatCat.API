namespace NatCat.DAL.Entity
{
	public class RhymingPattern : BaseGuidEntity
    {
		public string Name { get; set; }
		public string Description { get; set; }
        public string PatternStr { get; set; }

        public virtual ICollection<Story>? Stories { get; set; }
    }
}