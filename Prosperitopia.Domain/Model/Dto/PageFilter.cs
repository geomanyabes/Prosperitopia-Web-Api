using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Model.Dto
{
    public class PageFilter
    {
        [Range(10, 500)]
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string SortProperty { get; set; } = "Id";
        public string SortDirection { get; set; } = "ASC";
    }
}
