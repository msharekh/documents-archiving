using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentsArchiving.Web
{
    public class PagedList : IPagedList
    {
        public int PageCount => throw new NotImplementedException();

        public int TotalItemCount => throw new NotImplementedException();

        public int PageNumber => throw new NotImplementedException();

        public int PageSize => throw new NotImplementedException();

        public bool HasPreviousPage => throw new NotImplementedException();

        public bool HasNextPage => throw new NotImplementedException();

        public bool IsFirstPage => throw new NotImplementedException();

        public bool IsLastPage => throw new NotImplementedException();

        public int FirstItemOnPage => throw new NotImplementedException();

        public int LastItemOnPage => throw new NotImplementedException();
    }
}