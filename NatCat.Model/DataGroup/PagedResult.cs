using System;
namespace NatCat.Model.DataGroup
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int RecordNumber { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}

