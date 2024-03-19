using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Domain.Model.Dto
{
    public class CreUpdateItem
    {
        public long? Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal? Price { get; set; }
        public string CategoryId { get; set; } = "";
    }
}
