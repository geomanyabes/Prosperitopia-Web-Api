using Prosperitopia.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Model.Dto
{
    public class SearchFilter
    {
        public string Search { get; set; }
        public SearchType SearchType { get; set; }
    }
}
