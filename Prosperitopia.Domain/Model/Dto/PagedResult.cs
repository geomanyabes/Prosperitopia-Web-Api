using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Model.Dto
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public List<T> Result { get; set; } = new List<T>();
        public PagedResult() 
        {

        }
        public PagedResult(int pageSize, int totalCount, int page, List<T> result)
        {
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.Page = page;
            this.Result = result;
        }
    }
}
