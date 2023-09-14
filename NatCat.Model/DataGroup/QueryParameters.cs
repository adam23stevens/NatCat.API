using System;
using System.Linq.Expressions;

namespace NatCat.Model.DataGroup
{
    public class QueryParameters<TEntity>
    {
        private int _pageSize = 5;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public int StartIndex { get; set; }
        public int PageNumber { get; set; }
        public required Expression<Func<TEntity, bool>> wc;
    }
}

