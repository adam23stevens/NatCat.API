namespace NatCat.Model.Helper
{
    public static class GuidExtensions
    {
        public static Guid ToGuid(this Guid? source)
        {
            return source ?? Guid.Empty;
        }
    }
}